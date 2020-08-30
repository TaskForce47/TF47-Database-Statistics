using System;

namespace TF47_Database.Model
{
    public class KillLog
    {
        public uint Id { get; set; }
        public uint KillerId { get; set; }
        public uint VictimId { get; set; }
        public string KillerVehicle { get; set; }
        public string KillerSide { get; set; }
        public string Weapon { get; set; }
        public string VictimSide { get; set; }
        public int Distance { get; set; }
        public DateTime TimeTracked { get; set; }
        public uint MissionTime { get; set; }

    }
}