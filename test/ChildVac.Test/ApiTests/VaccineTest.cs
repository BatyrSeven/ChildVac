using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChildVac.WebApi;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public class VaccineTest : IClassFixture<ApiWebApplicationFactory<Startup>>
    {
        public VaccineTest(ApiWebApplicationFactory<Startup> factory)
        {
            _factory = factory;

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _client.BaseAddress = new Uri("https://localhost:44319");
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header
        }

        private readonly HttpClient _client;
        private readonly ApiWebApplicationFactory<Startup> _factory;
        private IServiceScopeFactory ScopeFactory => _factory.Server.Host.Services.GetService<IServiceScopeFactory>();
        private string Resource => "/api/Vaccine";

        [Fact]
        public async Task ShouldAddWhenPost()
        {
            // Arrange
            var name = "test vaccine";
            var recieveTime = TimeSpan.FromDays(1);
            var vaccine = new JObject
            {
                ["name"] = name,
                ["RecieveTime"] = recieveTime
            };

            var content = new StringContent(vaccine.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            // Act
            var response = await _client.PostAsync(Resource, content);
            //string resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                Assert.Equal(1, context.Vaccines.Count());

                var actualVaccine = context.Vaccines.First();
                Assert.Equal(name, actualVaccine.Name);
                Assert.Equal(recieveTime, actualVaccine.RecieveTime);

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
                RecieveTime = TimeSpan.FromDays(1)
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveTime = TimeSpan.FromDays(2)
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync(Resource);
            var vaccineList = await response.Content.ReadAsAsync<List<Vaccine>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, vaccineList.Count);

            Assert.Equal(first.RecieveTime, vaccineList.First().RecieveTime);
            Assert.Equal(first.Name, vaccineList.First().Name);

            Assert.Equal(second.RecieveTime, vaccineList.Last().RecieveTime);
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
                RecieveTime = TimeSpan.FromDays(1)
            };
            var second = new Vaccine
            {
                Name = "test name",
                RecieveTime = TimeSpan.FromDays(2)
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync($"{Resource}/{first.Id}");
            var vaccine = await response.Content.ReadAsAsync<Vaccine>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(first.RecieveTime, vaccine.RecieveTime);
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
                RecieveTime = TimeSpan.FromDays(1)
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveTime = TimeSpan.FromDays(2)
            };
            var updatedName = "updated name";
            var updatedRecieveTime = TimeSpan.FromDays(3);

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

            // Act
            var content = new StringContent(updatedVaccine.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            var response = await _client.PutAsync($"{Resource}/{first.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();

                var actual = context.Vaccines.First(x => x.Id == first.Id);
                var last = context.Vaccines.Last();

                Assert.Equal(updatedRecieveTime, actual.RecieveTime);
                Assert.Equal(updatedName, actual.Name);

                Assert.NotEqual(updatedRecieveTime, last.RecieveTime);
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
                RecieveTime = TimeSpan.FromDays(1)
            };
            var second = new Vaccine
            {
                Name = "test name 2",
                RecieveTime = TimeSpan.FromDays(2)
            };

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                context.Vaccines.Add(first);
                context.Vaccines.Add(second);
                context.SaveChanges();
            }

            // Act
            var response = await _client.DeleteAsync($"{Resource}/{first.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var scope = ScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                Assert.Single(context.Vaccines);

                var last = context.Vaccines.Last();

                Assert.Equal(second.RecieveTime, last.RecieveTime);
                Assert.Equal(second.Name, last.Name);

                context.Vaccines.RemoveRange(context.Vaccines);
                context.SaveChanges();
            }
        }
    }
}