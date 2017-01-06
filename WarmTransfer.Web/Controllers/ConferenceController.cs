using System.Threading.Tasks;
using System.Web.Mvc;
using WarmTransfer.Web.Domain;
using WarmTransfer.Web.Models;
using WarmTransfer.Web.Models.Repository;

namespace WarmTransfer.Web.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ICallCreator _callCreator;
        private readonly ICallsRepository _callsRepository;

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
        public async Task<ActionResult> ConnectClient(string callSid)
        {
            const string agentOne = "agent1";
            var conferenceId = callSid;
            var callBackUrl = GetConnectConfereceUrlForAgent(agentOne, conferenceId);
            await _callCreator.CallAgentAsync(agentOne, callBackUrl);
            await _callsRepository.CreateOrUpdateAsync(agentOne, conferenceId);
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, false, true);

            return Content(response, "text/xml");
        }

        [HttpPost]
        public ActionResult Wait()
        {
            return Content(TwiMLGenerator.GenerateWait(), "text/xml");
        }

        [HttpPost]
        public ActionResult ConnectAgent1(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, true, false);

            return Content(response, "text/xml");
        }

        [HttpPost]
        public ActionResult ConnectAgent2(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, WaitUrl, true, true);

            return Content(response, "text/xml");
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
            var url = string.Format(
                "https://{0}{1}",
                Config.Domain,
                Url.Action(action, new {conferenceId}));

            return url;
        }
    }
}