using NUnit.Framework;
using TestStack.FluentMVCTesting;
using WarmTransfer.Web.Controllers;

namespace WarmTransfer.Web.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Test]
        public void GivenACreateAction_ThenRendersTheDefaultView()
        {
            var controller = new HomeController();
            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
