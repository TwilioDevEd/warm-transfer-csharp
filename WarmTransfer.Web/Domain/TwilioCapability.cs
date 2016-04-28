using Twilio;

namespace WarmTransfer.Web.Domain
{
    public class CapabilityGenerator
    {
        private readonly TwilioCapability _twilioCapability;

        public CapabilityGenerator()
        {
            _twilioCapability = new TwilioCapability(Config.AccountSID, Config.AuthToken);
        }

        public string Generate(string agentId)
        {
            _twilioCapability.AllowClientIncoming(agentId);
            return _twilioCapability.GenerateToken();
        }
    }
}