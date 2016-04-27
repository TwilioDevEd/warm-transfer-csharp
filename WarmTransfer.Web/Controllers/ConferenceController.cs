using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.TwiML.Mvc;
using WarmTransfer.Web.Domain;
using WarmTransfer.Web.Models.Repository;

namespace WarmTransfer.Web.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ICallCreator _callCreator;
        private readonly ICallsRepository _callsRepository;
        private readonly ITwiMLGenerator _twiMLGenerator;

        public ConferenceController(
            ICallCreator callCreator, ICallsRepository callsRepository)
        {
            _callCreator = callCreator;
            _callsRepository = callsRepository;
            _twiMLGenerator = new TwiMLGenerator();
        }

        public ActionResult ConnectClient(string conferenceId)
        {
            const string agentOne = "agent1";
            const string callBackUrl = "callback-url"; //TODO extract it to a config file
            _callCreator.CallAgent(agentOne, callBackUrl);
            _callsRepository.CreateIfNotExists(agentOne, conferenceId);
            var response = _twiMLGenerator.GenerateConnectConference(conferenceId, "wait-url", false, true);
            return new TwiMLResult(response);
        }
    }
}