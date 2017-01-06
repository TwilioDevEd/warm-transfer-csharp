using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Twilio.Clients;
using Twilio.Http;
using WarmTransfer.Web.Domain;

namespace WarmTransfer.Web.Tests.Domain
{
    public class CallCreatorTest
    {
        [Test]
        public async Task WhenCallAgent_ThenCreateIsInvoked()
        {
            // Given
            var requestParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("To", "client:agent-id"),
                new KeyValuePair<string, string>("From", "twilio-phone-number"),
                new KeyValuePair<string, string>("Url", "http://callback-url.com/"),
            };
            var expectedRequest = new Request(
                HttpMethod.Post,
                Twilio.Rest.Domain.Api,
                "/2010-04-01/Accounts/accountSid/Calls.json",
                null,
                postParams: requestParams
            );
            var mockClient = SetupTwilioRestClientMock(expectedRequest);
            var callCreator = new CallCreator(mockClient.Object);

            // When
            await callCreator.CallAgentAsync("agent-id", "http://callback-url.com");

            // Then
            mockClient.Verify(c => c.RequestAsync(expectedRequest), Times.Once());
        }

        private static Mock<ITwilioRestClient> SetupTwilioRestClientMock(Request request)
        {
            var mockClient = new Mock<ITwilioRestClient>();
            
            mockClient
                .Setup(c => c.RequestAsync(request))
                .Returns(Task.FromResult(new Response(System.Net.HttpStatusCode.OK, "")));
            mockClient
                .Setup(r => r.AccountSid)
                .Returns("accountSid");

            return mockClient;
        }
    }
}

