namespace WarmTransfer.Web.Models
{
    public class Call
    {
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