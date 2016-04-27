using System.Xml.XPath;
using NUnit.Framework;
using WarmTransfer.Web.Domain;

namespace WarmTransfer.Web.Tests.Domain
{

    public class TwiMLGeneratorTest
    {
        [Test]
        public void WhenGenerateConnectConference_ThenGeneratesTwiMLWithDialAndConference()
        {
            var response = TwiMLGenerator.GenerateConnectConference("conference-id", "wait-url", true, false);
            var conference =
                response.ToXDocument().XPathSelectElement("Response/Dial/Conference");


            Assert.That(conference.Value, Is.EqualTo("conference-id"));
            Assert.That(conference.Attribute("waitUrl").Value, Is.EqualTo("wait-url"));
            Assert.That(conference.Attribute("startConferenceOnEnter").Value, Is.EqualTo("true"));
            Assert.That(conference.Attribute("endConferenceOnExit").Value, Is.EqualTo("false"));
        }

        [Test]
        public void WhenGenerateWait_ThenGeneratesTwiMLWithSayAndPlay()
        {
            var response = TwiMLGenerator.GenerateWait();
            var document = response.ToXDocument();
            Assert.That(
                document.XPathSelectElement("Response/Say").Value,
                Is.EqualTo("Thank you for calling. Please wait in line for a few seconds. An agent will be with you shortly."));
            Assert.That(
                document.XPathSelectElement("Response/Play").Value,
                Is.EqualTo("http://com.twilio.music.classical.s3.amazonaws.com/BusyStrings.mp3"));
        }
    }
}
