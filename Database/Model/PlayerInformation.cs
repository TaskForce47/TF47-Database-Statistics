using System;
using TF47_Database.Model;

namespace TF47_Database.Model
{
    public class PlayerInformation 
    {
        public Player Player { get; set; }
        public uint PlayerId { get; set; }
        public string NameFirstTimeSeen { get; set; }
        public string Name { get; set; }
        public DateTime FirstTimeSeen { get; set; }
        public DateTime LastTimeSeen { get; set; }
        public uint Connections { get; set; }
    }
}