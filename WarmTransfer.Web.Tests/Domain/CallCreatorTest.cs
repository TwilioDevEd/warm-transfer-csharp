using Moq;
using NUnit.Framework;
using Twilio;
using WarmTransfer.Web.Domain;

namespace WarmTransfer.Web.Tests.Domain
{
    public class CallCreatorTest
    {
        [Test]
        public void WhenCallAgent_ThenCreateIsInvoked()
        {
            var mockClient = new Mock<TwilioRestClient>(string.Empty, string.Empty);

            var callCreator = new CallCreator(mockClient.Object);
            callCreator.CallAgent("agent-id", "callback-url");

            mockClient.Verify(
                c => c.InitiateOutboundCall("twilio-phone-number", "client:agent-id", "callback-url"), Times.Once());
        }
    }
}
