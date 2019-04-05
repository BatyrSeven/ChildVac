using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChildVac.Test.Helpers;
using ChildVac.WebApi;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public class HospitalTest : ApiTestBase
    {
        public HospitalTest(ApiWebApplicationFactory<Startup> factory) : base(factory)
        {
            
        }

        protected override string Resource => "/api/Hospital";

        [Theory]
        [InlineData("test name", "test address")]
        public async Task ShouldAddWhenPost(string name, string address)
        {
            // Arrange
            var hospital = new JObject
            {
                ["name"] = name,
                ["address"] = address
            };

            var content = new StringContent(hospital.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            var token = await AuthenticationHelper.GetAdminToken(Client);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await Client.PostAsync(Resource, content);
            //string resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                Assert.Equal(1, context.Hospitals.Count());

                var actualHospital = context.Hospitals.First();
                Assert.Equal(name, actualHospital.Name);
                Assert.Equal(address, actualHospital.Address);

                context.Hospitals.Remove(actualHospital);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldReturnAllWhenGetAll()
        {
            // Arrange
            var first = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };
            var second = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Hospitals.Add(first);
                context.Hospitals.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await Client.GetAsync(Resource);
            var hostpitalList = await response.Content.ReadAsAsync<List<Hospital>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(hostpitalList.Count >= 2);

            
            var firstHospital = hostpitalList.OrderByDescending(x => x.Id).Skip(1).FirstOrDefault();
            Assert.Equal(first.Address, firstHospital?.Address);
            Assert.Equal(first.Name, firstHospital?.Name);

            Assert.Equal(second.Address, hostpitalList.Last().Address);
            Assert.Equal(second.Name, hostpitalList.Last().Name);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Hospitals.RemoveRange(hostpitalList);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldReturnWhenGet()
        {
            // Arrange
            var first = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };
            var second = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Hospitals.Add(first);
                context.Hospitals.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await Client.GetAsync($"{Resource}/{first.Id}");
            var hospital = await response.Content.ReadAsAsync<Hospital>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(first.Address, hospital.Address);
            Assert.Equal(first.Name, hospital.Name);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Hospitals.RemoveRange(context.Hospitals);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldUpdateWhenPut()
        {
            // Arrange
            var first = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };
            var second = new Hospital
            {
                Name = "test name 2",
                Address = "test address 2"
            };
            var updatedName = "updated name";
            var updatedAddress = "updated address";

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Database.EnsureCreated();
                context.Hospitals.Add(first);
                context.Hospitals.Add(second);
                context.SaveChanges();
            }

            var updatedHospital = new JObject
            {
                ["id"] = first.Id,
                ["name"] = updatedName,
                ["address"] = updatedAddress
            };

            var token = await AuthenticationHelper.GetAdminToken(Client);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var content = new StringContent(updatedHospital.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            var response = await Client.PutAsync($"{Resource}/{first.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();

                var actual = context.Hospitals.First(x => x.Id == first.Id);
                var last = context.Hospitals.Last();

                Assert.Equal(updatedAddress, actual.Address);
                Assert.Equal(updatedName, actual.Name);

                Assert.NotEqual(updatedAddress, last.Address);
                Assert.NotEqual(updatedName, last.Name);

                context.Hospitals.RemoveRange(context.Hospitals);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldRemoveWhenDelete()
        {
            // Arrange
            var first = new Hospital
            {
                Name = "test name",
                Address = "test address"
            };
            var second = new Hospital
            {
                Name = "test name 2",
                Address = "test address 2"
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Hospitals.Add(first);
                context.Hospitals.Add(second);
                context.SaveChanges();
            }

            var token = await AuthenticationHelper.GetAdminToken(Client);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await Client.DeleteAsync($"{Resource}/{first.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                Assert.Single(context.Hospitals);

                var last = context.Hospitals.Last();

                Assert.Equal(second.Address, last.Address);
                Assert.Equal(second.Name, last.Name);

                context.Hospitals.RemoveRange(context.Hospitals);
                context.SaveChanges();
            }
        }
    }
}