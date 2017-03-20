using System.Threading.Tasks;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using WarmTransfer.Web.Domain;
using WarmTransfer.Web.Models;
using WarmTransfer.Web.Models.Repository;

namespace WarmTransfer.Web.Controllers
{
    public class ConferenceController : TwilioController
    {
        private readonly ICallCreator _callCreator;
        private readonly ICallsRepository _callsRepository;
        private const string OriginHeader = "Origin";
        public static string WaitUrl = "http://twimlets.com/holdmusic?Bucket=com.twilio.music.classical";

        public ConferenceController() : this(
            new CallCreator(),
            new CallsRepository(new WarmTransferContext())) { }

        public ConferenceController(
            ICallCreator callCreator, ICallsRepository callsRepository)
        {
            _callCreator = callCreator;
            _callsRepository = callsRepository;
        }

        [HttpPost]
        public async Task<TwiMLResult> ConnectClient(string callSid)
        {
            const string agentOne = "agent1";
            var conferenceId = callSid;
            var callBackUrl = GetConnectConfereceUrlForAgent(agentOne, conferenceId);
            await _callCreator.CallAgentAsync(agentOne, callBackUrl);
            await _callsRepository.CreateOrUpdateAsync(agentOne, conferenceId);
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, false, true);

            return TwiML(response);
        }

        [HttpPost]
        public TwiMLResult Wait()
        {
            return TwiML(TwiMLGenerator.GenerateWait());
        }

        [HttpPost]
        public TwiMLResult ConnectAgent1(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, true, false);

            return TwiML(response);
        }

        [HttpPost]
        public TwiMLResult ConnectAgent2(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, true, true);

            return TwiML(response);
        }

        [HttpPost]
        public async Task<ActionResult> CallAgent2(string agentId)
        {
            var call = await _callsRepository.FindByAgentIdAsync(agentId);
            var callBackUrl = GetConnectConfereceUrlForAgent(agentId, call.ConferenceId);
            await _callCreator.CallAgentAsync("agent2", callBackUrl);

            return new EmptyResult();
        }

        private string GetConnectConfereceUrlForAgent(string agentId, string conferenceId) {
            var action = agentId == "agent1" ? "ConnectAgent1" : "ConnectAgent2";
            var origin = Request.Headers[OriginHeader];
            var url = $"https://{origin}{Url.Action(action, new {conferenceId})}";

            return url;
        }
    }
}