using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests
{
    public class MockTests
    {       
        
        private static Mock<HttpMessageHandler> mock = new Mock<HttpMessageHandler>();
        private RepositoryService repo = new RepositoryService(new HttpClient(mock.Object));

        [Fact]
        public async void ResponseTests()
        {
            HttpResponse();

            var response = await repo.RequestApi("ibm");
            Assert.NotEmpty(response);
            foreach (var item in response)
            {
                Assert.NotEqual(item.Name, string.Empty);
                Assert.NotNull(item.Url);
            }
        }

        [Fact]
        public async void HttpResponseTests()
        {

            HttpResponse();

            Assert.NotNull(await repo.RequestApi("ibm"));
            mock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }
    
        private void HttpResponse()
        {   
            var mockData = new StringContent(File.ReadAllText("../../../MockData.json"));
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = mockData
            };
            
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }
    }
}