using System.Collections.Generic;

namespace TF47_Database.Model
{
    public class Mission
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public MissionType Type { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}