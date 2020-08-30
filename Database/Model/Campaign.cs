using System;
using System.Collections.Generic;

namespace TF47_Database.Model
{
    public class Campaign
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStarted { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}