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
    }
}
