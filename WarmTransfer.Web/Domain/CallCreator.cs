using Twilio;

namespace WarmTransfer.Web.Domain
{
    public interface ICallCreator
    {
        void CallAgent(string agentId, string callbackUrl);
    }

    public class CallCreator : ICallCreator
    {
        private readonly TwilioRestClient _client;
        public CallCreator(TwilioRestClient client)
        {
            _client = client;
        }

        public void CallAgent(string agentId, string callbackUrl)
        {
            // TODO: Pull out the from number from the configuration file.
            const string from = "";
            _client.InitiateOutboundCall(from, string.Format("client:{0}", agentId), callbackUrl);
        }
    }
}