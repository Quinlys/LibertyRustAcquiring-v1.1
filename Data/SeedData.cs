using LibertyRustAcquiring.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace LibertyRustAcquiring.Data
{
    public class SeedData
    {
        public static void Init(ApplicationDbContext context)
        {
            if (context.Packs.Any() || context.PackItems.Any())
                return;

            var packs = new List<Pack>();

            // -----------------------------
            // 1) ELITE ПРИВІЛЕЯ (один item elite c Quantity = 14)
            // -----------------------------

            var elitePrivileya = new Pack
            {
                Name = "ELITE привілея",
                NameENG = "ELITE PRIVILEGE",
                Description = "Найкращий привілей, щоб почати грати на нашому сервері.",
                DescriptionENG = "The best privilege to start playing on our server.",
                Details = "Rate x3. В три рази більше ресурсів!\r\nПосилений метаболізм. Прокидайтесь ситими та з повним HP.\r\nПрефікс [ELITE] на нашому сервері. Пишайтесь своєю підтримкою українського комьюніті!\r\nПропуск черги на сервер, адже ви тепер ELITE персона ;)\r\nТепер вам доступна команда /skin. Всі доступні ігрові скіни тепер у вас на нашому сервері.\r\nВи можете використовувати любі скіни для будівель. Автоматичне застосування скінів DLC для будівельних блоків. Команда /bskin\r\nНезламні інструменти. Тож більше не потрібно крафтити десятки кирок та сокир, можна обійтись лише однією. (Відбійний молоток не входить до списку незламних інструментів)\r\nНезламна броня та зброя.\r\nНезламні картки доступу. Тепер вам не потрібно запасатися ними, адже потрібна лише одна.\r\nНескінченний балон для дайвінгу.\r\nМожливість швидкого сортування за допомогою кнопки в меню інтерфейсу ящиків та холодильників.\r\nУНІКАЛЬНА команда /setgenes дозволяє швидко отримати потрібний ген. Для цього вам потрібен саджанець.\r\nЗавантажте зображення з URL-адреси на сервер і відобразіть його на табличці, яку ви зараз дивитеся. (/sil)",
                DetailsENG = "Rate x3. Three times more resources!\r\nEnhanced metabolism. Wake up full and with full HP.\r\nThe [ELITE] prefix on our server. Be proud of your support for the Ukrainian community!\r\nSkip the queue to the server, because you are now an ELITE person ;)\r\nThe /skin command is now available to you. All available game skins are now on our server.\r\nYou can use any skins for buildings. Automatic application of DLC skins for building blocks. Command /bskin\r\nUnbreakable tools. So you don't need to craft dozens of picks and axes anymore, you can get by with just one. (The jackhammer is not included in the list of indestructible tools).\r\nUnbreakable armor and weapons.\r\nUnbreakable access cards. Now you don't need to stock up on them, because you only need one\r\nEndless diving cylinder.\r\nThe ability to quickly sort using a button in the interface menu of drawers and refrigerators.\r\nThe unique /setgenes command allows you to quickly get the desired gene. For this you need a seedling.\r\nUpload an image from a URL to the server and display it on the plate you are currently viewing. (/sil)",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635272/case12_iscewu.png",
                                            "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635272/el_hover_txurem.png"},
                Price = 399m,
                SalePrice = 199m,
                Type = PackType.Privilege,
                Items = new List<PackItem>()
            };
            elitePrivileya.Items.Add(CreatePackItem(elitePrivileya.Id, "elite", 14));
            packs.Add(elitePrivileya);
            
            // -----------------------------
            // 2) VIP ПРИВІЛЕЯ (один item vip c Quantity = 14)
            // -----------------------------

            var vipPrivileya = new Pack
            {
                Name = "VIP привілея",
                NameENG = "VIP PRIVILEGE",
                Description = "Для початку гри на сервері - це гарний привілей.",
                DescriptionENG = "It is a good privilege to start playing on the server.",
                Details = "Метаболізм. Прокидайтесь завжди ситими.\r\nПрефікс [VIP] на нашому сервері. Пишайтесь своєю підтримкою українського комьюніті!\r\nПропуск черги на сервер, адже ви тепер VIP персона ;)\r\nНезламні інструменти. Тож більше не потрібно крафтити десятки кирок та сокир, можна обійтись лише однією. ( Відбійний молоток не входить до списку незламних інструментів )\r\nНезламна броня та зброя.\r\nНезламні картки доступу. Тепер вам не потрібно запасатися ними, адже потрібна лише одна.\r\nНескінченний балон для дайвінгу.\r\nМожливість швидкого сортування за допомогою кнопки в меню інтерфейсу ящиків та холодильників.",
                DetailsENG = "Metabolism. Wake up always full.\r\nPrefix [VIP] on our server. Be proud of your support for the Ukrainian community!\r\nSkip the queue to the server, because you are now a VIP ;)\r\nUnbreakable tools. So you don't need to craft dozens of picks and axes anymore, you can do with just one. (The jackhammer is not included in the list of unbreakable tools).\r\nUnbreakable armor and weapons.\r\nUnbreakable access cards. Now you don't need to stock up on them, because you only need one\r\nEndless diving cylinder.\r\nThe ability to quickly sort using the button in the interface menu of drawers and refrigerators.",
                Images = new List<string> {"https://res.cloudinary.com/dai2q2olh/image/upload/v1741635317/case11_pbiwdp.png",
                                           "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635317/vip_hover_mkismj.png"},
                Price = 199m,
                SalePrice = 99m,
                Type = PackType.Privilege,
                Items = new List<PackItem>()
            };
            vipPrivileya.Items.Add(CreatePackItem(vipPrivileya.Id, "vip", 14));
            packs.Add(vipPrivileya);

            // -----------------------------
            // 3) Станьте спонсором (один item sponsor c Quantity = 14)
            // -----------------------------

            var stanSponsorom = new Pack
            {
                Name = "Станьте спонсором",
                NameENG = "BECOME A SPONSOR",
                Description = "Ця привілея - для підтримки нашого сервера, щоб ми могли розвиватися і ставати кращими.",
                DescriptionENG = "It is a privilege to support our server so that we can develop and be better.",
                Details = "Стань спонсором українського проекту!\r\nЩодня ми розвиваємо нашу спільноту та рухаємось вперед до відкриття нових серверів. На жаль утримання серверів не надто дешеве задоволення, що вже говорити про рекламу.\r\nОсновні витрати проeкту Liberty Rust:\r\nОренда серверів: 350$ на місяць.\r\nПокупка плагінів та команда розробки: 400$ на місяць.\r\nРеклама: 400$ на місяць.\r\nПриєднуйся до команди Liberty Rust та стань нашим спонсором в цьому місяці!\r\nТепер ви як гравець будете відображатись з тегом [SPONSOR], а ваше ім'я назавжди буде викарбуване в історії нашого проeкту в діскорд сервері.\r\nТакож додатково спонсор получає статус [ELITE] протягом двох тижнів! Для отримання привілеї зверніться до адміністратора безпосередньо в грі або ж у Discord каналі проекту.\r\nВперед розвивати українську спільноту в RUST!",
                DetailsENG = "Become a sponsor of the Ukrainian project!\r\nEvery day we develop our community and move forward to open new servers. Unfortunately, maintaining servers is not a very cheap pleasure, not to mention advertising.\r\nMain expenses of the Liberty Rust project:\r\nServer rental: $350 per month.\r\nThe cost of the server.\r\nPurchase of plugins and development team: $400 per month.\r\nAdvertising: 400$ per month.\r\nJoin the Liberty Rust team and become our sponsor this month!\r\nNow you as a player will be displayed with the tag [SPONSOR], and your name will be forever engraved in the history of our project in the discord server\r\nIn addition, the sponsor will receive [ELITE] status for two weeks! To get this privilege, contact the administrator directly in the game or in the project's Discord channel.\r\nLet's develop the Ukrainian community in RUST!",
                Images = new List<string> {"https://res.cloudinary.com/dai2q2olh/image/upload/v1741635394/case10_inv3ao.png",
                                           "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635394/sp_hover_flmidy.png"},
                Price = 599m,
                SalePrice = 299m,
                Type = PackType.Privilege,
                Items = new List<PackItem>()
            };
            stanSponsorom.Items.Add(CreatePackItem(stanSponsorom.Id, "sponsor", 14));
            packs.Add(stanSponsorom);


            // -----------------------------
            // 4) ВІДКРИТТЯ ВСІХ СКІНІВ
            // -----------------------------

            var skinsAllPack = new Pack
            {
                Name = "ВІДКРИТТЯ ВСІХ СКІНІВ",
                NameENG = "UNLOCK ALL SKINS",
                Description = "Дає вам доступ до всіх скінів, щоб ви мали гарний вигляд.",
                DescriptionENG = "Gives you access to all skins so you can look good.",
                Details = "При покупці набору вам стають доступні всі існуючі скіни в грі Rust на 30 днів. Щоб використовувати меню вибору скінів введіть команду /skins в чат.\r\nЗ покупкою вам доступні всі варіанти скінів на будівлі. ( Кам'яні та металеві стіни ). Команда /bskin\r\nТакож додатково ви получаєте доступ до деяких DLC наприклад таке як літнє.",
                DetailsENG = "When you purchase a bundle, you get access to all existing skins in Rust for 30 days.To use the skin selection menu, enter the /skins command in the chat.\r\nWith the purchase, you have access to all building skins (stone and metal walls). Command /bskin\r\nAlso, you additionally get access to some DLC, such as summer.",
                Images = new List<string> {"https://res.cloudinary.com/dai2q2olh/image/upload/v1741635539/case9_axaehk.png",
                                           "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635540/skins_hover_nhlimm.png" },
                Price = 159m,
                SalePrice = 79m,
                Type = PackType.Skins,
                Items = new List<PackItem>()
            };
            skinsAllPack.Items.Add(CreatePackItem(skinsAllPack.Id, "SkinsALL", 1));
            packs.Add(skinsAllPack);

            // -----------------------------
            // 5) ВІДКРИТТЯ ВИВЧЕНЬ
            // -----------------------------
            var bpUnlockPack = new Pack
            {
                Name = "ВІДКРИТТЯ ВИВЧЕНЬ",
                NameENG = "UNLOCK BLUPRINTS",
                Description = "Придбайте це щоб моментально, дослідити всі вивчення.",
                DescriptionENG = "Purchase this to instantly, explore all the studies.",
                Details = "Придбавши цей набір, вам миттєво буде доступний крафт любого можливого предмету на сервері Liberty Rust. Всі вивчення будуть відкриті до наступного глобального вайпу. Календар вайпів ви можете переглянути у нас в діскорд каналі.",
                DetailsENG = "By purchasing this set, you will instantly be able to craft any possible item on the Liberty Rust server. All studies will be open until the next global vype. You can check out the vape calendar in our discord channel.",
                Images = new List<string> {"https://res.cloudinary.com/dai2q2olh/image/upload/v1741635429/case8_qadx4q.png",
                                           "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635429/unlock_hover_hng8ha.png" },
                Price = 399m,
                SalePrice = 199m,
                Type = PackType.Blueprints,
                Items = new List<PackItem>()
            };
            bpUnlockPack.Items.Add(CreatePackItem(bpUnlockPack.Id, "bpulockall", 1));
            packs.Add(bpUnlockPack);

            // -----------------------------
            // 6) Набір Компонентів
            // -----------------------------
            var naborKomponentiv = new Pack
            {
                Name = "Набір Компонентів",
                NameENG = "PACK 'COMPONENTS'",
                Description = "Ідеальний набір для крафту предметів.",
                DescriptionENG = "Perfect set for crafting items.",
                Details = "60 наборів для шиття\r\n60 шестерень\r\n45 металевих лез\r\n45 дорожніх знаків\r\n45 пружин\r\n45 металевих труб\r\n30 мотузок\r\n15 корпусів напівавтомату\r\n10 корпусів пістолету-гвинтівки\r\n10 корпусів гвинтівки\r\n10 старих мікросхем",
                DetailsENG = "60 sewing kits\r\n60 gears\r\n45 metal blades\r\n45 road signs\r\n45 springs\r\n45 metal pipes\r\n30 ropes\r\n15 semi-automatic rifle cases\r\n10 submachine gun cases\r\n10 rifle cases\r\n10 old microcircuits",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635615/case1_sapbso.png" },
                Price = 159m,
                SalePrice = 79m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "sewingkit", 60));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "gears", 60));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "metalblade", 45));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "roadsigns", 45));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "metalspring", 45));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "metalpipe", 45));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "rope", 30));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "semibody", 15));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "smgbody", 10));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "riflebody", 10));
            naborKomponentiv.Items.Add(CreatePackItem(naborKomponentiv.Id, "techparts", 10));
            packs.Add(naborKomponentiv);

            // -----------------------------
            // 7) Набір з ресурсами
            // -----------------------------
            var naborZResursamy = new Pack
            {
                Name = "Набір з ресурсами",
                NameENG = "PACK 'RESOURCES'",
                Description = "Всі види ресурсів в одному наборі саме те що потрібно для будівництва будиночка.",
                DescriptionENG = "All types of resources in one set, exactly what you need to build a house.",
                Details = "630 000 каменю\r\n20 000 дерева\r\n10 000 металевих фрагментів\r\n300 металу високої якості\r\n1000 тканини",
                DetailsENG = "630 000 stone\r\n20,000 pieces of wood\r\n10,000 metal fragments\r\n300 high quality metal\r\n1000 fabric",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635615/case2_egxnw0.png" },
                Price = 139m,
                SalePrice = 69m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborZResursamy.Items.Add(CreatePackItem(naborZResursamy.Id, "stones", 30000));
            naborZResursamy.Items.Add(CreatePackItem(naborZResursamy.Id, "wood", 20000));
            naborZResursamy.Items.Add(CreatePackItem(naborZResursamy.Id, "metal.fragments", 10000));
            naborZResursamy.Items.Add(CreatePackItem(naborZResursamy.Id, "metal.refined", 300));
            naborZResursamy.Items.Add(CreatePackItem(naborZResursamy.Id, "cloth", 1000));
            packs.Add(naborZResursamy);

            // -----------------------------
            // 8) Набір фармера
            // -----------------------------
            var naborFarmera = new Pack
            {
                Name = "Набір фармера",
                NameENG = "PACK FOR FARM",
                Description = "Шикарний набір для легкого фарму ресурсів.",
                DescriptionENG = "A chic set for light farming wood and ores.",
                Details = "Три відбійних молотки ( Бур )\r\nТри бензопили\r\n5 саморобних льодорубів\r\n5 саморобних сокир\r\n500 низькоякісного палива\r\n3 рудних чаї найвищої якості\r\n3 чаї для фарма дерева найкращої якості",
                DetailsENG = "Three jackhammers (drills)\r\nThree chainsaws\r\n5 homemade ice axes\r\n5 homemade axes\r\n500 liters of low-quality fuel\r\n3 ore teas of the highest quality\r\n3 pharma tree teas of the highest quality",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635615/case3_jr4qxp.png" },
                Price = 139m,
                SalePrice = 69m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "jackhammer", 3));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "chainsaw", 3));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "icepick.salvaged", 5));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "axe.salvaged", 5));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "lowgradefuel", 500));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "oretea.pure", 3));
            naborFarmera.Items.Add(CreatePackItem(naborFarmera.Id, "woodtea.pure", 3));
            packs.Add(naborFarmera);

            // -----------------------------
            // 9) Набір "Власна територія"
            // -----------------------------
            var naborVlasnaTerytoriya = new Pack
            {
                Name = "Набір 'Власна територія'",
                NameENG = "Pack 'OWN TERRITORY'",
                Description = "Набір для облаштування свого житлового куточка.",
                DescriptionENG = "A set for arranging your living space.",
                Details = "20 високих зовнішніх кам'яних стін\r\n1 високі зовнішні кам'яні ворота\r\n2 великі печі\r\n1 нафтопереробна станція\r\n1 сторожова вежа",
                DetailsENG = "20 high outer stone walls\r\n1 high outer stone gate\r\n2 large furnaces\r\n1 oil refinery\r\n1 watchtower.",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635618/case4_nq06cr.png" },
                Price = 159m,
                SalePrice = 79m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborVlasnaTerytoriya.Items.Add(CreatePackItem(naborVlasnaTerytoriya.Id, "wall.external.high.stone", 20));
            naborVlasnaTerytoriya.Items.Add(CreatePackItem(naborVlasnaTerytoriya.Id, "gates.external.high.stone", 1));
            naborVlasnaTerytoriya.Items.Add(CreatePackItem(naborVlasnaTerytoriya.Id, "furnace.large", 2));
            naborVlasnaTerytoriya.Items.Add(CreatePackItem(naborVlasnaTerytoriya.Id, "small.oil.refinery", 1));
            naborVlasnaTerytoriya.Items.Add(CreatePackItem(naborVlasnaTerytoriya.Id, "watchtower.wood", 1));
            packs.Add(naborVlasnaTerytoriya);

            // -----------------------------
            // 10) Набір турелі + ППО
            // -----------------------------
            var naborTureli = new Pack
            {
                Name = "Набір турелі + ППО",
                NameENG = "TURRET PACK + Anti Air Defense",
                Description = "Ідеально нейтралізує будь-які рухомі об'єкти в зоні досяжності...",
                DescriptionENG = "Perfectly neutralizes any moving objects within range...",
                Details = "П'ять турелей.\r\nОдне ППО.\r\nСотня ракет для ППО.\r\nВітряк для генерування електроенергії.",
                DetailsENG = "Five turrets.\r\nOne air defense system.\r\nOne hundred missiles for air defense.\r\nA windmill to generate electricity.",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635618/case5_qnpcaj.png" },
                Price = 159m,
                SalePrice = 79m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborTureli.Items.Add(CreatePackItem(naborTureli.Id, "autoturret", 5));
            naborTureli.Items.Add(CreatePackItem(naborTureli.Id, "samsite", 1));
            naborTureli.Items.Add(CreatePackItem(naborTureli.Id, "ammo.rocket.sam", 100));
            naborTureli.Items.Add(CreatePackItem(naborTureli.Id, "generator.wind.scrap", 1));
            packs.Add(naborTureli);

            // -----------------------------
            // 11) Набір з верстаками
            // -----------------------------
            var naborZVerstakamy = new Pack
            {
                Name = "Набір з верстаками",
                NameENG = "KIT WITH WORKBENCHES",
                Description = "Чудовий набір який надає можливість вивчити та скрафтити всі предмети.",
                DescriptionENG = "A great set that gives you the opportunity to explore and craft all the items.",
                Details = "Придбавши набір ви отримаєте по одному верстату кожного рівню.",
                DetailsENG = "By purchasing the set, you will receive one machine of each level.",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635619/case6_r4qqxv.png" },
                Price = 119m,
                SalePrice = 59m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            naborZVerstakamy.Items.Add(CreatePackItem(naborZVerstakamy.Id, "workbench1", 1));
            naborZVerstakamy.Items.Add(CreatePackItem(naborZVerstakamy.Id, "workbench2", 1));
            naborZVerstakamy.Items.Add(CreatePackItem(naborZVerstakamy.Id, "workbench3", 1));
            packs.Add(naborZVerstakamy);

            // -----------------------------
            // 12) Набір "Паливний магнат"
            // -----------------------------
            var naborPalyvnyiMagnat = new Pack
            {
                Name = "Набір 'Паливний магнат'",
                NameENG = "KIT 'FUEL TYCOON'",
                Description = "Даний набір дозволить обслуговувати всі ваші транспортні засоби та не тільки.",
                DescriptionENG = "This set will allow you to service all your vehicles and more.",
                Details = "Нафтопереробний завод.\r\n500 сирої нафти.\r\n1000 палива низької якості.\r\n20 бочок з дизельним паливом.",
                DetailsENG = "Oil refinery.\r\n500 crude oil.\r\n1000 low quality fuel.\r\n20 barrels of diesel fuel.",
                Images = new List<string> { "https://res.cloudinary.com/dai2q2olh/image/upload/v1741635622/case7_owtg41.png" },
                Price = 139m,
                SalePrice = 69m,
                Type = PackType.Resource,
                Items = new List<PackItem>()
            };
            // "Нафтопереробний завод" — чаще всего это small.oil.refinery
            naborPalyvnyiMagnat.Items.Add(CreatePackItem(naborPalyvnyiMagnat.Id, "small.oil.refinery", 1));
            naborPalyvnyiMagnat.Items.Add(CreatePackItem(naborPalyvnyiMagnat.Id, "crude.oil", 500));
            naborPalyvnyiMagnat.Items.Add(CreatePackItem(naborPalyvnyiMagnat.Id, "lowgradefuel", 1000));
            naborPalyvnyiMagnat.Items.Add(CreatePackItem(naborPalyvnyiMagnat.Id, "diesel_barrel", 20));
            packs.Add(naborPalyvnyiMagnat);

            context.Packs.AddRange(packs);
            context.SaveChanges();
        }

        private static PackItem CreatePackItem(int packId, string name, int quantity)
        {
            var itemType = DetermineItemType(name);
            return new PackItem(packId, name, quantity)
            {
                ItemType = itemType
            };
        }

        private static ItemType DetermineItemType(string name)
        {
            if (name.Equals("vip", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("elite", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("sponsor", StringComparison.OrdinalIgnoreCase))
            {
                return ItemType.Privilege;
            }

            if (name.Equals("SkinsALL", StringComparison.OrdinalIgnoreCase))
            {
                return ItemType.Skins;
            }
            if (name.Equals("bpulockall", StringComparison.OrdinalIgnoreCase))
            {
                return ItemType.Blueprints;
            }           

            return ItemType.Resource;
        }
    }
}
