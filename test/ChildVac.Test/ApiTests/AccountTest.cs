using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChildVac.WebApi;
using ChildVac.WebApi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public class AccountTest : ApiTestBase
    {
        public AccountTest(ApiWebApplicationFactory<Startup> factory) : base(factory)
        {
           
        }

        protected override string Resource => "/api/Account";

        [Fact]
        public async Task ShouldReturnTokenWhenPostValid()
        {
            // Arrange
            var data = new JObject
            {
                ["login"] = "qwerty",
                ["password"] = "55555"
            };

            var content = new StringContent(data.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            // Act
            var response = await Client.PostAsync(Resource, content);
            string resultContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = JObject.Parse(resultContent);
            Assert.True(result.ContainsKey("token"));
            Assert.True(result.ContainsKey("login"));
        }

        [Fact]
        public async Task ShouldReturnTokenWhenPostInvalid()
        {
            // Arrange
            var data = new JObject
            {
                ["login"] = "qwerty",
                ["password"] = "12345"
            };

            var content = new StringContent(data.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            // Act
            var response = await Client.PostAsync(Resource, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}