using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChildVac.WebApi;
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
        [InlineData("123456789001", "123456", "Admin")]
        [InlineData("123456789002", "123456", "Child")]
        [InlineData("123456789003", "123456", "Doctor")]
        [InlineData("123456789004", "123456", "Parent")]
        public async Task ShouldReturnTokenWhenPostValid(string iin, string password, string role)
        {
            // Arrange
            var data = new JObject
            {
                ["iin"] = iin,
                ["password"] = password,
                ["role"] = role
            };

            var content = new StringContent(data.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            // Act
            var response = await Client.PostAsync(Resource, content);
            string resultContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var parsedContent = JObject.Parse(resultContent);
            Assert.NotNull(parsedContent);

            var result = parsedContent["result"] as JObject;
            Assert.NotNull(result);

            Assert.True(result.ContainsKey("token"));
            Assert.True(result["user"]?["iin"] != null);
            Assert.True(result["user"]?["role"] != null);

            Assert.Equal(iin, result["user"]?["iin"].ToString());
            Assert.Equal(role, result["user"]?["role"].ToString());
        }

        [Fact]
        public async Task ShouldReturnTokenWhenPostInvalid()
        {
            // Arrange
            var data = new JObject
            {
                ["iin"] = "123456789000",
                ["password"] = "12345",
                ["role"] = ""
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