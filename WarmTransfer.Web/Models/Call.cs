namespace WarmTransfer.Web.Models
{
    public class Call
    {
        private Call() { } //Required by Entity Framework

        public Call(string agentId, string conferenceId)
        {
            AgentId = agentId;
            ConferenceId = conferenceId;
        }

        public int Id { get; set; }
        public string AgentId { get; set; }
        public string ConferenceId { get; set; }
    }
}