using System;

namespace TF47_Database.Model
{
    public class TicketLog
    {
        public uint Id { get; set; }
        public Session Session { get; set; }
        public uint SessionId { get; set; }
        public uint TicketOld { get; set; }
        public uint TicketNew { get; set; }
        public int TicketChange { get; set; }
        public string Reason { get; set; }
        public uint MissionTime { get; set; }
        public DateTime TimeChanged { get; set; }
        public bool SessionFinished { get; set; }
        public Player Player { get; set; }
        public uint PlayerId { get; set; }
    }
}