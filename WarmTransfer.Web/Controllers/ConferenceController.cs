using System.Web.Mvc;
using Twilio;
using Twilio.TwiML.Mvc;
using WarmTransfer.Web.Domain;
using WarmTransfer.Web.Models.Repository;

namespace WarmTransfer.Web.Controllers
{
    public class ConferenceController : TwilioController
    {
        private readonly ICallCreator _callCreator;
        private readonly ICallsRepository _callsRepository;

        public ConferenceController() : this(
            new CallCreator(new TwilioRestClient(Config.AccountSID, Config.AuthToken)),
            new CallsRepository(new Models.WarmTransferContext())) { }

        public ConferenceController(
            ICallCreator callCreator, ICallsRepository callsRepository)
        {
            _callCreator = callCreator;
            _callsRepository = callsRepository;
        }

        public ActionResult ConnectClient(string conferenceId)
        {
            const string agentOne = "agent1";
            const string callBackUrl = "callback-url"; //TODO extract it to a config file
            _callCreator.CallAgent(agentOne, callBackUrl);
            _callsRepository.CreateIfNotExists(agentOne, conferenceId);
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", false, true);
            return TwiML(response);
        }

        public ActionResult Wait()
        {
            return TwiML(TwiMLGenerator.GenerateWait());
        }

        public ActionResult ConnectAgent1(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", false, true);
            return TwiML(response);
        }

        public ActionResult ConnectAgent2(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", true, true);
            return TwiML(response);
        }

        public ActionResult CallAgent2(string agentId)
        {
            _callsRepository.FindByAgentId(agentId);
            const string callBackUrl = "agent2-callback-url"; //TODO extract it to a config file
            _callCreator.CallAgent("agent2", callBackUrl);
            return null;
        }

    }
}