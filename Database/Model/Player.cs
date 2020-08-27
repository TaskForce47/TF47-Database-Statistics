using System.Collections.Generic;

namespace TF47_Database.Model
{
    public class Player
    {
        public Player()
        {
            Chats = new HashSet<ChatLog>();
            PlayerPerformance = new HashSet<PerformancePlayer>();
            TicketLog = new HashSet<TicketLog>();
        }

        public uint Id { get; set; }
        public string PlayerUid { get; set; }

        public IEnumerable<ChatLog> Chats { get; set; }
        public IEnumerable<PerformancePlayer> PlayerPerformance { get; set; }
        public IEnumerable<TicketLog> TicketLog { get; set; }
        public PlayerInformation PlayerInformation { get; set; }
    }
}