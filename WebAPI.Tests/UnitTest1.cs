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
    public class UnitTest1
    {       
        [Fact]
        public async void Test1()
        {   
            var mockData = new StringContent(System.IO.File.ReadAllText("c:/Users/BrunoRossettoPereira/Desktop/WebAPIClient/WebAPI.Tests/MockData.json"));
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = mockData
            };
            
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var repo = new RepositoryService(new HttpClient(mock.Object));


            Assert.NotNull(await repo.RequestApi("ibm"));
            mock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}