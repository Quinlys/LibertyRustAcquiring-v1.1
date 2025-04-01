using Microsoft.AspNetCore.Mvc;
using LibertyRustAcquiring.Order.CreateOrder;
using LibertyRustAcquiring.Order.UpdateOrderStatus;
using LibertyRustAcquiring.DTOs.Monobank;
using LibertyRustAcquiring.Order.GetOrderPrice;
using System.Text.Json;
using LibertyRustAcquiring.Order.FindOrderByInvoiceId;
using System.Security.Cryptography;
using System.Text;
using LibertyRustAcquiring.Order.GetOrders;
using LibertyRustAcquiring.Models.Constants;
using Microsoft.AspNetCore.Authorization;

namespace LibertyRustAcquiring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;
        private readonly IPubKeyProvider _pubKeyProvider;
        private readonly string _xtoken;

        public OrderController(
            IHttpClientFactory httpClientFactory,
            IPubKeyProvider pubKeyProvider,
            IConfiguration configuration,
            ISender sender,
            ILogger<OrderController> logger
            )
        {
            _sender = sender;
            _httpClientFactory = httpClientFactory;
            _pubKeyProvider = pubKeyProvider;
            _configuration = configuration;
            _logger = logger;
            _xtoken = configuration["Monobank:XToken"]!;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var orders = await _sender.Send(new GetOrdersQuery());

            return Ok(orders);
        }

        [HttpPost("create-invoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateOrderRequest request)
        {
            using var client = _httpClientFactory.CreateClient();

            var baseUrl = _configuration["Monobank:BaseAddress"] ?? "https://api.monobank.ua/api/merchant";

            var packIds = request.Packs
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (!packIds.Any()) return BadRequest("No packs were sent to process the payment");           

            var orderData = await _sender.Send(new GetPreOrderDataQuery(request.Server, request.SteamId, packIds));

            if (!orderData.CanBeCreated) return BadRequest(
                new GetPreOrderResponse
                {
                    Message = "Order cannot be created since player choosed packs that he cannot purchase while having not enough space in inventory or being offline.",
                    Notify = orderData.ErrorCaused
                }
            );


            var requestContent = JsonContent.Create(new CreateInvoiceRequest
            {
                Amount = (long)orderData.TotalPrice * 100,
                Ccy = int.Parse(_configuration["Monobank:Ccy"]!),
                RedirectUrl = _configuration["Monobank:RedirectUrl"]!,
                WebhookUrl = _configuration["Monobank:WebhookUrl"]!,
                Validaty = long.Parse(_configuration["Monobank:Validaty"]!),
            });

            requestContent.Headers.Add("X-Token", _configuration["Monobank:XToken"]);

            var response = await client.PostAsync($"{baseUrl}/invoice/create", requestContent);

            if (!response.IsSuccessStatusCode) return BadRequest("Monobank return null as a response or response was not successfull.");
            
            InvoiceCreateResponse invoiceResponse = await response.Content.ReadFromJsonAsync<InvoiceCreateResponse>()
                ?? throw new ObjectIsNullException<InvoiceCreateResponse>();

            var result = await _sender.Send(new CreateOrderCommand(request.SteamId, request.Server, invoiceResponse.InvoiceId, packIds));

            if (!result.IsSuccess) return BadRequest();

            return Ok(invoiceResponse);
        }
        
        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook()
        {
            string requestBody;

            using (var reader = new StreamReader(Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            if (!Request.Headers.TryGetValue("X-Sign", out var xSignHeader))
            {
                _logger.LogWarning("Webhook request missing X-Sign header.");
                return BadRequest("Missing X-Sign header.");
            }

            var xSign = xSignHeader.ToString();

            if (string.IsNullOrWhiteSpace(xSign))
            {
                _logger.LogWarning("X-Sign header is empty.");
                return BadRequest("Empty signature.");
            }

            //string pubKeyPem;

            //try
            //{
            //    pubKeyPem = await _pubKeyProvider.GetPublicKeyAsync();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error fetching public key from IPubKeyProvider.");
            //    return StatusCode(500, "Internal server error.");
            //}

            //bool isValidSignature = VerifySignature(requestBody, xSign, pubKeyPem);

            //if (!isValidSignature)
            //{
            //    _logger.LogWarning("Invalid signature for webhook.");
            //    return Unauthorized("Invalid signature.");
            //}

            MonobankWebhookPayload payload;

            try
            {
                payload = JsonSerializer.Deserialize<MonobankWebhookPayload>(requestBody)
                    ?? throw new ObjectIsNullException<MonobankWebhookPayload>();

                if (payload == null)
                {
                    _logger.LogWarning("Webhook payload is null.");
                    return BadRequest("Invalid payload.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing webhook payload.");
                return BadRequest("Invalid JSON payload.");
            }

            _logger.LogInformation("Webhook received. InvoiceId: {InvoiceId}, Status: {Status}", payload.InvoiceId, payload.Status);

            var order = await _sender.Send(new FindOrderByInvoiceIdQuery(payload.InvoiceId));

            if (order.Id == Guid.Empty)
            {
                _logger.LogWarning("Order with invoiceId {InvoiceId} not found.", payload.InvoiceId);
                return NotFound("Order not found.");
            }


            if(order.Status != OrderStatuses.Success)
            {
                var updateResult = await _sender.Send(new UpdateOrderStatusCommand(order.Id, payload.Status));

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError("Failed to update order status for OrderId: {OrderId}", order);
                    return BadRequest("Failed to update order.");
                }
            }

            _logger.LogError("Payment error, status: {status}", payload.Status);
            
            return Ok("Webhook processed successfully.");
        }
        private bool VerifySignature(string requestBody, string xSign, string pubKeyPem)
        {
            try
            {
                byte[] signatureBytes = Convert.FromBase64String(xSign);
                byte[] dataBytes = Encoding.UTF8.GetBytes(requestBody);

                using ECDsa ecdsa = ECDsa.Create();
                ecdsa.ImportFromPem(pubKeyPem.ToCharArray());

                return ecdsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying signature.");
                return false;
            }
        }

    }
}
