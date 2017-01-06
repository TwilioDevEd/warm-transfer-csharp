using Twilio.JWT;

namespace WarmTransfer.Web.Domain
{
    public class CapabilityGenerator
    {
        public static string Generate(string agentId)
        {
            var twilioCapability = new TwilioCapability(Config.AccountSid, Config.AuthToken);

            twilioCapability.AllowClientIncoming(agentId);
            return twilioCapability.GenerateToken();
        }
    }
}