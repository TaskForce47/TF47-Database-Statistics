namespace TF47_Database.Model
{
    public class PlayerWhitelisting
    {
        public uint Id { get; set; }
        public uint WhitelistId { get; set; }

        public Whitelist Whitelist { get; set; }
    }
}