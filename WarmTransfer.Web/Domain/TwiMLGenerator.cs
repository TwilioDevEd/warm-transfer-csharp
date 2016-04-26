using Twilio.TwiML;

namespace WarmTransfer.Web.Domain
{
    public interface ITwiMLGenerator
    {
        TwilioResponse GenerateConnectConference(
            string conferenceId, string waitUrl, bool startConferenceOnEnter, bool endConferenceOnExit);

        TwilioResponse GenerateWait();
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

        public TwilioResponse GenerateWait()
        {
            return new TwilioResponse()
                .Say("Thank you for calling. Please wait in line for a few seconds. An agent will be with you shortly.")
                .Play("http://com.twilio.music.classical.s3.amazonaws.com/BusyStrings.mp3");
        }
    }
}