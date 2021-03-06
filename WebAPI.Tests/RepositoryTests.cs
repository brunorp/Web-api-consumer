using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests
{
    public class RepositoryTests
    {       
        
        private static Mock<HttpMessageHandler> mock = new Mock<HttpMessageHandler>();
        private RepositoryService repo = new RepositoryService();

        [Fact]
        public async void ResponseTests()
        {
            RepetitiveMethod("../../../Data/MockData.json", HttpStatusCode.OK);

            var response = await repo.RequestApi(new HttpClient(mock.Object), "ibm");
            foreach (var item in response)
            {
                Assert.NotEqual(item.Name, string.Empty);
                Assert.NotNull(item.Url);
                Assert.NotEqual(item.Language, string.Empty);
                Assert.NotNull(item.IssueUrl);
            }
        }
        
        [Fact]
        public async void testRequestError()
        {
            RepetitiveMethod("../../../Data/MockData.json", HttpStatusCode.ServiceUnavailable);

            await Assert.ThrowsAsync<Exception>(
                async () => await repo.RequestApi(new HttpClient(mock.Object), "ibm")
            );
        }

        [Fact]
        public async void WrongResponseTests()
        {
            RepetitiveMethod("../../../Data/MockWrongData.json", HttpStatusCode.OK);
        
            await Assert.ThrowsAsync<Exception>(
               async () => await repo.RequestApi(new HttpClient(mock.Object), "ibm")
            );
        }

        private void RepetitiveMethod(string dir, HttpStatusCode statusCode)
        {   
            var mockData = new StringContent(File.ReadAllText(dir));
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = mockData
            });
        }
    }
}