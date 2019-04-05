using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ChildVac.WebApi;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public abstract class ApiTestBase : IClassFixture<ApiWebApplicationFactory<Startup>>
    {
        protected ApiTestBase(ApiWebApplicationFactory<Startup> factory)
        {
            Factory = factory;

            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            Client.BaseAddress = new Uri("https://localhost:44319");
            Client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header
        }

        protected readonly HttpClient Client;
        protected readonly ApiWebApplicationFactory<Startup> Factory;
        protected IServiceScopeFactory ScopeFactory => Factory.Server.Host.Services.GetService<IServiceScopeFactory>();
        protected abstract string Resource { get; }
    }
}
