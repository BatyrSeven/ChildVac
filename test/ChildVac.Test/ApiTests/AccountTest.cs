using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChildVac.WebApi;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
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

        [Theory]
        [InlineData("admin_login", "12345", "Admin")]
        [InlineData("child_login", "12345", "Child")]
        [InlineData("doctor_login", "12345", "Doctor")]
        [InlineData("parent_login", "12345", "Parent")]
        public async Task ShouldReturnTokenWhenPostValid(string login, string password, string expectedRole)
        {
            // Arrange
            var data = new JObject
            {
                ["login"] = login,
                ["password"] = password
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
            Assert.True(result.ContainsKey("role"));

            Assert.Equal(login, result["login"].ToString());
            Assert.Equal(expectedRole, result["role"].ToString());
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