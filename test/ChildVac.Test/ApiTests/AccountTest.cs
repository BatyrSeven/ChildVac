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
        public async Task ShouldReturnTokenWhenPost()
        {
            Assert.True(false);
        }
    }
}