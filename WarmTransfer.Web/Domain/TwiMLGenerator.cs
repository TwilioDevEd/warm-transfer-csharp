using Twilio.TwiML;

namespace WarmTransfer.Web.Domain
{

    public static class TwiMLGenerator
    {
        public static VoiceResponse GenerateConnectConference(string conferenceId, 
                                                       string waitUrl, 
                                                       bool startConferenceOnEnter, 
                                                       bool endConferenceOnExit)
        {
            var response = new VoiceResponse();
            var conference = new Dial().Conference(
                conferenceId, 
                waitUrl: waitUrl, 
                startConferenceOnEnter: startConferenceOnEnter, 
                endConferenceOnExit: endConferenceOnExit);

            return response.Dial(conference);
        }

        public static VoiceResponse GenerateWait()
        {
            return new VoiceResponse()
                .Say("Thank you for calling. Please wait in line for a few seconds. " +
                     "An agent will be with you shortly.")
                .Play("http://com.twilio.music.classical.s3.amazonaws.com/BusyStrings.mp3");
        }
    }
}