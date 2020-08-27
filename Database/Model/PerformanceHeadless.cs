namespace TF47_Database.Model
{
    public class PerformanceHeadless
    {
        public uint Id { get; set; }
        public string HeadlessName { get; set; }
        public Session Session { get; set; }
        public uint SessionId { get; set; }
        public uint ServerTime { get; set; }
        public uint Fps { get; set; }
        public uint SqfSpawned { get; set; }
        public uint SqfExecVm { get; set; }
        public uint SqfFsm { get; set; }
        public uint SqfCalled { get; set; }
        public uint ObjectCount { get; set; }
        public uint UnitCountLocal { get; set; }
    }
}