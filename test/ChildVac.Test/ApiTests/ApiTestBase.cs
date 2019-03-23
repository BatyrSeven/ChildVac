using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ChildVac.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ChildVac.Test.ApiTests
{
    public abstract class ApiTestBase : IClassFixture<ApiWebApplicationFactory<Startup>>
    {
        protected ApiTestBase(ApiWebApplicationFactory<Startup> factory)
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

        protected readonly HttpClient _client;
        protected readonly ApiWebApplicationFactory<Startup> _factory;
        protected IServiceScopeFactory ScopeFactory => _factory.Server.Host.Services.GetService<IServiceScopeFactory>();
        protected abstract string Resource { get; }
    }
}
