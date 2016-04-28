using Moq;
using NUnit.Framework;
using System.Xml.XPath;
using TestStack.FluentMVCTesting;
using WarmTransfer.Web.Controllers;
using WarmTransfer.Web.Domain;
using WarmTransfer.Web.Models.Repository;
using WarmTransfer.Web.Tests.Extensions;

namespace WarmTransfer.Web.Tests.Controllers
{
    public class ConferenceControllerTest
    {
        private Mock<ICallCreator> _mockCallCreator;
        private Mock<ICallsRepository> _mockCallsRepository;
        private ConferenceController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCallCreator = new Mock<ICallCreator>();
            _mockCallsRepository = new Mock<ICallsRepository>();

            _controller = new ConferenceController(
                _mockCallCreator.Object, _mockCallsRepository.Object);
        }

        [Test]
        public void WhenConnectClient_ThenShouldCallAgent()
        {
            _controller.ConnectClient("conference-id");

            _mockCallCreator.Verify(c => c.CallAgent("agent1", "callback-url"), 
                Times.Once());
        }

        [Test]
        public void WhenConnectClient_ThenItShouldCreateACall()
        {
            _controller.ConnectClient("conference-id");

            _mockCallsRepository.Verify(c => c.CreateIfNotExists("agent1", "conference-id"),
                Times.Once());
        }

        [Test]
        public void WhenConnectClient_ThenItShouldGenerateConnectConference()
        {
            _controller.WithCallTo(c => c.ConnectClient("conference-id"))
            .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Dial/Conference").Value, Is.EqualTo("conference-id"));
                });

        }

        [Test]
        public void WhenWait_ThenItShouldGenerateWait()
        {
            _controller.WithCallTo(c => c.Wait())
            .ShouldReturnTwiMLResult(data =>
            {
                StringAssert.Contains("Please wait", data.XPathSelectElement("Response/Say").Value);
                StringAssert.Contains("twilio.music", data.XPathSelectElement("Response/Play").Value);
            });

        }

        [Test]
        public void WhenConnectAgent1_ThenItShouldGenerateConnectConference()
        {
            _controller.WithCallTo(c => c.ConnectAgent1("conference-id"))
            .ShouldReturnTwiMLResult(data =>
            {
                var xElement = data.XPathSelectElement("Response/Dial/Conference");
                Assert.That(xElement.Value, Is.EqualTo("conference-id"));
                Assert.That(xElement.Attribute("waitUrl").Value, Is.EqualTo("wait-url"));
                Assert.That(xElement.Attribute("startConferenceOnEnter").Value, Is.EqualTo("false"));
                Assert.That(xElement.Attribute("endConferenceOnExit").Value, Is.EqualTo("true"));
            });

        }
    }
}
