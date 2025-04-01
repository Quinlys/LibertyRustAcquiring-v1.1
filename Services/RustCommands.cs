namespace LibertyRustAcquiring.Utils
{
    public static class RustCommands
    {
        public static string AddItemCommand(string steamId, string item, int quantity)
        {
            return $"inventory.giveto {steamId} {item} {quantity}";
        }
        public static string AddPrivelegeCommand(string steamId, string item, int duration)
        {
            return $"addgroup {steamId} {item} {duration}d";
        }

        public static string UnclockSkinsCommand(string steamId, int duration)
        {
            return $"addgroup {steamId} SkinsALL {duration}d";
        }

        public static string UnlockBlueprints(string steamId) 
        {
            return $"bpunlockall {steamId}";
        }
        public static string CheckPlayerOnLine(string steamId)
        {
            return $"checkonline {steamId}";
        }
        public static string CheckSlots(string steamId)
        {
            return $"checkslots {steamId}";
        }
    }
}
