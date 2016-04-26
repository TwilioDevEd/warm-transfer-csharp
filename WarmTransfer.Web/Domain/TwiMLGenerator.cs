using Twilio.TwiML;

namespace WarmTransfer.Web.Domain
{
    public interface ITwiMLGenerator
    {
        TwilioResponse GenerateConnectConference(
            string conferenceId, string waitUrl, bool startConferenceOnEnter, bool endConferenceOnExit);
    }

    public class TwiMLGenerator : ITwiMLGenerator
    {
        public TwilioResponse GenerateConnectConference(
            string conferenceId, string waitUrl, bool startConferenceOnEnter, bool endConferenceOnExit)
        {
            var response = new TwilioResponse();
            var conference = new Conference(conferenceId, new {waitUrl, startConferenceOnEnter, endConferenceOnExit});

            return response.Dial(conference);
        }
    }
}