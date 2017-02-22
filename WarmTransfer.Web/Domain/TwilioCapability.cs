using System.Collections.Generic;
using Twilio.Jwt;
using Twilio.Jwt.Client;

namespace WarmTransfer.Web.Domain
{
    public class CapabilityGenerator
    {
        public static string Generate(string agentId)
        {
            var scopes = new HashSet<IScope>
            {
                new IncomingClientScope(agentId)
            };

            var capability = new ClientCapability(Config.AccountSid, Config.AuthToken, scopes: scopes);

            return capability.ToJwt();
        }
    }
}