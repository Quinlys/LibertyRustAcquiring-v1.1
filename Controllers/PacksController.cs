using LibertyRustAcquiring.Packs.GetPack;
using LibertyRustAcquiring.Packs.GetPacks;
using Microsoft.AspNetCore.Mvc;

namespace LibertyRustAcquiring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacksController : ControllerBase
    {
        private readonly ISender _sender;
        public PacksController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] GetPacksRequest request)
        {
            var result = await _sender.Send(new GetPacksQuery(request.Culture));

            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string culture)
        {
            var result = await _sender.Send(new GetPackQuery(id, culture));

            return Ok(result);
        }
    }
}
