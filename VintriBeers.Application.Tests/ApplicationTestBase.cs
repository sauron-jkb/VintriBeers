
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;

using VintriBeers.Domain.Data;
using VintriBeers.Externalities.ExternalServices.PunkApi;

namespace VintriBeers.Application.Tests
{
    public class ApplicationTestBase : IDisposable
    {
        public IConfigurationRoot ConfigurationRoot { get; private set; }
        public IPunkApiService PunkApiService { get; private set; }
        public IUserRatingsContext UserRatingsContext { get; private set; }

        public ApplicationTestBase()
        {
            ApplicationTestSetUp();
        }

        public void ApplicationTestSetUp()
        {
            InitializeConfiguration();
            InitializeServices();
        }

        public void InitializeConfiguration()
        {
            var configuration = new Dictionary<string, string>
            {
                { "UserRatingsContextFilePath", "C:\\Sandbox\\VintriBeers\\VintriBeers.Application.Tests\\database_testing.json" },
                { "LogFilePath", "C:\\Sandbox\\VintriBeers\\VintriBeers.Application.Tests\\" }
            };

            ConfigurationRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(configuration)
                .Build();
        }

        public void InitializeServices()
        {
            PunkApiService = new PunkApiService();
            UserRatingsContext = new UserRatingsContext(ConfigurationRoot);
        }

        public void Dispose()
        {
            try
            {
                PunkApiService = null;
                UserRatingsContext = null;
            }
            catch { }
        }
    }
}
