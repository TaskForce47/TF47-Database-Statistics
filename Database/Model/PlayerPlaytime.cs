namespace TF47_Database.Model
{
    public class PlayerPlaytime
    {
        public uint PlayerId { get; set; }
        public uint TimeAsInfantry { get; set; }
        public uint TimeInVehicleSmall { get; set; }
        public uint TimeInVehicleTracked { get; set; }
        public uint TimeInVehicleHelicopter { get; set; }
        public uint TimeInVehiclePlane { get; set; }
        public uint TimeInObjective { get; set; }
        public uint TimeInBase { get; set; }

        public uint TotalPlayTime => TimeAsInfantry + TimeInVehicleSmall + TimeInVehicleTracked +
                                     TimeInVehicleHelicopter + TimeInVehiclePlane + TimeInObjective + TimeInBase;

        public uint TotalPlayTimeWithoutBase => TotalPlayTime - TimeInBase;
    }
}