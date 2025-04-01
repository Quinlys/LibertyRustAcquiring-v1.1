using LibertyRustAcquiring.Models.Enums;

namespace LibertyRustAcquiring.Models.Entities
{
    public class PackItem
    {
        public int Id { get; set; }
        public string Name { get; private set;  }
        public int Quantity { get; private set; }
        public ItemType ItemType { get; set; }

        public int PackId { get; set; }

        public PackItem(int packId, string name, int quantity)
        {
            PackId = packId;
            Name = name;
            Quantity = quantity;
        }

        public static List<string> ItemNames = new List<string>
        {
            "sewingkit",
            "gears",
            "metalblade",
            "roadsigns",
            "metalspring",
            "metalpipe",
            "rope",
            "semibody",
            "smgbody",
            "riflebody",
            "techparts",

            "furbace.large",
            "wall.external.high.stone",
            "small.oil.refinery",
            "gates.external.high.stone",
            "watchtower.wood",

            "crude.oil",
            "lowgradefuel",
            "diesel_barrel",

            "autoturret",
            "samsite",
            "ammo.rocket.sam",
            "generator.wind.scrap",

            "jackhammer",
            "chainsaw",
            "icepick.salvaged",
            "axe.salvaged",
            "oretea.pure",
            "woodtea.pure",

            "stones",
            "wood",
            "metal.fragments",
            "metal.refined",
            "cloth",
            
            "workbench1",
            "workbench2",
            "workbench3",
        };
        public static List<string> PrivelegeNames = new List<string>
        {
            "vip",
            "elite",
            "sponsor",
            "SkinsALL",
        };
    }

}