using NUnit.Framework;
using WarmTransfer.Web.Domain;

namespace WarmTransfer.Web.Tests.Domain
{
    public class CabapilityGeneratorTest
    {
        [Test]
        public void WhenGenerate_ThenItShouldGenerateATwilioCapability()
        {
            CapabilityGenerator capabilityGenerator = new CapabilityGenerator();

            var token = capabilityGenerator.Generate("agentId1");

            Assert.IsNotNull(token);
        }
    }
}
