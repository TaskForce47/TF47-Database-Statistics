namespace TF47_Database.Model
{
    public class PositionTracking
    {
        public uint Id { get; set; }
        public Session Session { get; set; }
        public uint SessionId { get; set; }
        public uint PlayerId { get; set; }
        public float[] Position { get; set; }
        public int Dir { get; set; }
        //public int Velocity { get; set; }
        public VehicleType VehicleType { get; set; }
        public string VehicleName { get; set; }
        public string Group { get; set; }
        public bool IsGroupLeader { get; set; }
        public uint MissionTime { get; set; }
    }
}