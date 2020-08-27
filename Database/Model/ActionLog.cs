using System;

namespace TF47_Database.Model
{
    public class ActionLog
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint ActionId { get; set; }
        public uint PlayerId { get; set; }
        public uint TargetId { get; set; }
        public string TargetVehicle { get; set; }
        public uint MissionTime { get; set; }
        public DateTime TimeTracked { get; set; }

    }
}