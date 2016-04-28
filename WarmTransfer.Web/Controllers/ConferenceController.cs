using System;
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

        [HttpPost]
        public ActionResult ConnectClient(string conferenceId)
        {
            const string agentOne = "agent1";
            var callBackUrl = GetConnectConfereceUrlFor(agentOne, conferenceId);
            _callCreator.CallAgent(agentOne, callBackUrl);
            _callsRepository.CreateIfNotExists(agentOne, conferenceId);
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, conferenceId, false, true);
            return TwiML(response);
        }

        [HttpPost]
        public ActionResult Wait()
        {
            return TwiML(TwiMLGenerator.GenerateWait());
        }

        [HttpPost]
        public ActionResult ConnectAgent1(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", false, true);
            return TwiML(response);
        }

        [HttpPost]
        public ActionResult ConnectAgent2(string conferenceId)
        {
            var response = TwiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", true, true);
            return TwiML(response);
        }

        [HttpPost]
        public ActionResult CallAgent2(string agentId)
        {
            _callsRepository.FindByAgentId(agentId);
            const string callBackUrl = "agent2-callback-url"; //TODO extract it to a config file
            _callCreator.CallAgent("agent2", callBackUrl);
            return null;
        }

        //conference_connect_agent1_url
        private string GetConnectConfereceUrlFor(string agentId, string conferenceId) {
            var action = agentId == "agent1" ? "ConnectAgent1" : "ConnectAgent2";
            var ac = Url.Action(action);
            return string.Format(
                "{0}://{1}{2}",
                Request.Url.Scheme,
                Config.Domain,
                Url.Action(action, new {conferenceId}));
        }
    }
}