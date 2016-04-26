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
            var response = new TwiMLGenerator().GenerateConnectConference("conference-id", "wait-url", true, false);
            var conference =
                response.ToXDocument().XPathSelectElement("Response/Dial/Conference");


            Assert.That(conference.Value, Is.EqualTo("conference-id"));
            Assert.That(conference.Attribute("waitUrl").Value, Is.EqualTo("wait-url"));
            Assert.That(conference.Attribute("startConferenceOnEnter").Value, Is.EqualTo("true"));
            Assert.That(conference.Attribute("endConferenceOnExit").Value, Is.EqualTo("false"));
        }
    }
}
