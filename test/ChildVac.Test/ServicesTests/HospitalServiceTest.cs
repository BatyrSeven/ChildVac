using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using ChildVac.WebApi.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace ChildVac.Test.ServicesTests
{
    public class HospitalServiceTest
    {
        private SqliteConnection _connection;
        [SetUp]
        public void Setup()
        {
            // In-memory database only exists while the connection is open
            _connection = new SqliteConnection("DataSource=:memory:");
        }

        [Test]
        public void ShouldWriteToDatabaseWhenAdd()
        {
            _connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseSqlite(_connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ApplicationContext(options))
                {
                    var service = new HospitalService(context);
                    service.Add("Hospital #12", "Zhandosov st.");
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationContext(options))
                {
                    Assert.AreEqual(1, context.Hospitals.Count());
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Test]
        public void ShouldFindOccuranceWhenSearch()
        {
            _connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseSqlite(_connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ApplicationContext(options))
                {
                    context.Hospitals.Add(new Hospital
                    {
                        Name = "Hospital #12",
                        Address = "Zhandosov st."
                    });

                    context.Hospitals.Add(new Hospital
                    {
                        Name = "Hospital #123",
                        Address = "Not Zhandosov st."
                    });

                    context.Hospitals.Add(new Hospital
                    {
                        Name = "Something different",
                        Address = "Some st."
                    });

                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationContext(options))
                {
                    var service = new HospitalService(context);
                    var result = service.Find("12");
                    Assert.AreEqual(2, result.Count());
                }
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}