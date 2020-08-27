namespace TF47_Database.Model
{
    public class ChatLog
    {
        public uint Id { get; set; }
        public Player Player { get; set; }
        public uint PlayerId { get; set; }
        public string Message { get; set; }
        public Channel Channel { get; set; }
        public string TimeSend { get; set; }
        public Session Session { get; set; }
        public uint SessionId { get; set; }
    }
}