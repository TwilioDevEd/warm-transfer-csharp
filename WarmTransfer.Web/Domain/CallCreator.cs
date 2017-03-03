using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WarmTransfer.Web.Domain
{
    public interface ICallCreator
    {
        Task CallAgentAsync(string agentId, string callbackUrl);
    }

    public class CallCreator : ICallCreator
    {
        
        public CallCreator()
        {
            TwilioClient.Init(Config.AccountSid, Config.AuthToken);
        }

        public CallCreator(ITwilioRestClient restClient) : this()
        {
            TwilioClient.SetRestClient(restClient);
        }

        public async Task CallAgentAsync(string agentId, string callbackUrl)
        {
            var to = new PhoneNumber($"client:{agentId}");
            var from = new PhoneNumber(Config.TwilioPhoneNumber);
            await CallResource.CreateAsync(to, from, url: new Uri(callbackUrl));
        }
    }
}