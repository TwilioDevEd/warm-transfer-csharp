using NUnit.Framework;
using TestStack.FluentMVCTesting;
using WarmTransfer.Web.Controllers;

namespace WarmTransfer.Web.Tests.Controllers
{
    public class TokenControllerTest
    {
        [Test]
        public void WhenGenerate_ThenItShouldGenerateAToken()
        {
            var tokenController = new TokenController();
            tokenController.WithCallTo(c => c.Generate("agentid"))
                .ShouldReturnJson();
        }
    }
}
