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

            Assert.IsNotNull(capabilityGenerator.Generate("agentId1"));
        }
    }
}
