using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChildVac.Test.Helpers;
using ChildVac.WebApi;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public class VaccineTest : ApiTestBase
    {
        public VaccineTest(ApiWebApplicationFactory<Startup> factory) : base(factory)
        {
            
        }

        protected override string Resource => "/api/Vaccine";

        [Fact]
        public async Task ShouldAddWhenPost()
        {
            // Arrange
            var name = "test vaccine";
            var recieveTime = 1;
            var vaccine = new JObject
            {
                ["name"] = name,
                ["RecieveTime"] = recieveTime
            };

            var content = new StringContent(vaccine.ToString(),
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
                Assert.Equal(1, context.Vaccines.Count());

                var actualVaccine = context.Vaccines.First();
                Assert.Equal(name, actualVaccine.Name);
                Assert.Equal(recieveTime, actualVaccine.RecieveMonth);

                context.Vaccines.Remove(actualVaccine);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldReturnAllWhenGetAll()
        {
            // Arrange
            var first = new Vaccine
            {
                Name = "test name",
                RecieveMonth = 1
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveMonth = 2
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await Client.GetAsync(Resource);
            var vaccineList = await response.Content.ReadAsAsync<List<Vaccine>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(vaccineList.Count >= 2);

            var firstVaccine = vaccineList.OrderByDescending(x => x.Id).Skip(1).FirstOrDefault();
            Assert.Equal(first.RecieveMonth, firstVaccine?.RecieveMonth);
            Assert.Equal(first.Name, firstVaccine?.Name);

            Assert.Equal(second.RecieveMonth, vaccineList.Last().RecieveMonth);
            Assert.Equal(second.Name, vaccineList.Last().Name);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.RemoveRange(vaccineList);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldReturnWhenGet()
        {
            // Arrange
            var first = new Vaccine
            {
                Name = "test name",
                RecieveMonth = 1
            };
            var second = new Vaccine
            {
                Name = "test name",
                RecieveMonth = 2
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await Client.GetAsync($"{Resource}/{first.Id}");
            var vaccine = await response.Content.ReadAsAsync<Vaccine>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(first.RecieveMonth, vaccine.RecieveMonth);
            Assert.Equal(first.Name, vaccine.Name);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.RemoveRange(context.Vaccines);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldUpdateWhenPut()
        {
            // Arrange
            var first = new Vaccine
            {
                Name = "test name",
                RecieveMonth = 1
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveMonth = 2
            };
            var updatedName = "updated name";
            var updatedRecieveTime = 3;

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            var updatedVaccine = new JObject
            {
                ["id"] = first.Id,
                ["name"] = updatedName,
                ["recieveTime"] = updatedRecieveTime
            };

            var token = await AuthenticationHelper.GetAdminToken(Client);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var content = new StringContent(updatedVaccine.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            var response = await Client.PutAsync($"{Resource}/{first.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();

                var actual = context.Vaccines.First(x => x.Id == first.Id);
                var last = context.Vaccines.Last();

                Assert.Equal(updatedRecieveTime, actual.RecieveMonth);
                Assert.Equal(updatedName, actual.Name);

                Assert.NotEqual(updatedRecieveTime, last.RecieveMonth);
                Assert.NotEqual(updatedName, last.Name);

                context.Vaccines.RemoveRange(context.Vaccines);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task ShouldRemoveWhenDelete()
        {
            // Arrange
            var first = new Vaccine
            {
                Name = "test name",
                RecieveMonth = 1
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveMonth = 2
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
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
                Assert.Single(context.Vaccines);

                var last = context.Vaccines.Last();

                Assert.Equal(second.RecieveMonth, last.RecieveMonth);
                Assert.Equal(second.Name, last.Name);

                context.Vaccines.RemoveRange(context.Vaccines);
                context.SaveChanges();
            }
        }
    }
}