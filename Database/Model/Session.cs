using System;
using System.Collections.Generic;
using TF47_Database.Model;

namespace TF47_Database.Model
{
    public class Session
    {
        public Session()
        {
            PerformanceServer = new HashSet<PerformanceServer>();
            PerformanceHeadless = new HashSet<PerformanceHeadless>();
            PerformancePlayers = new HashSet<PerformancePlayer>();
            Chats = new HashSet<ChatLog>();
            TicketLogEntries = new HashSet<TicketLog>();
            PositionTrackingPoints = new HashSet<PositionTracking>();
        }
        public uint Id { get; set; }
        public Mission Mission { get; set; }
        public uint MissionId { get; set; }
        public Campaign Campaign { get; set; }
        public uint? CampaignId { get; set; }
        public DateTime TimeStarted { get; set; }
        public IEnumerable<ChatLog> Chats { get; set; }
        public IEnumerable<PerformancePlayer> PerformancePlayers { get; set; }
        public IEnumerable<PerformanceHeadless> PerformanceHeadless { get; set; }
        public IEnumerable<PerformanceServer> PerformanceServer { get; set; }
        public IEnumerable<TicketLog> TicketLogEntries { get; set; }
        public IEnumerable<PositionTracking> PositionTrackingPoints { get; set; }
    }
}