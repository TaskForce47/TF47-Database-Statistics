using System.Collections.Generic;

namespace TF47_Database.Model
{
    public class Whitelist
    {
        public Whitelist()
        {
            Whitelisting = new HashSet<PlayerWhitelisting>();
        }

        public uint Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<PlayerWhitelisting> Whitelisting { get; set; }
    }
}