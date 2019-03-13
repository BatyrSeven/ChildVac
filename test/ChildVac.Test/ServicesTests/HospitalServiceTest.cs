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
        private DbContextOptions _options;

        [SetUp]
        public void Setup()
        {
            // In-memory database only exists while the connection is open
            _connection = new SqliteConnection("DataSource=:memory:");

            _connection.Open();

            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(_connection)
                .Options;
        }

        [TearDown]
        public void TearDown()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        [Test]
        public void ShouldWriteToDatabaseWhenAdd()
        {
            // Create the schema in the database
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            // Run the test against one instance of the context
            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                service.Add("Hospital #12", "Zhandosov st.");
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationContext(_options))
            {
                Assert.AreEqual(1, context.Hospitals.Count());
            }
        }

        [Test]
        public void ShouldFindOccuranceWhenSearch()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationContext(_options))
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

            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                var result = service.Find("12");
                Assert.AreEqual(2, result.Count());
            }
        }

        [Test]
        public void ShouldGetWhenExists()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationContext(_options))
            {
                context.Hospitals.Add(new Hospital
                {
                    Name = "Hospital #12",
                    Address = "Zhandosov st."
                });

                context.SaveChanges();
            }

            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                var result = service.Get(1);
                Assert.NotNull(result);
            }
        }

        [Test]
        public void ShouldNotGetWhenNotExists()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationContext(_options))
            {
                context.Hospitals.Add(new Hospital
                {
                    Name = "Hospital #12",
                    Address = "Zhandosov st."
                });

                context.SaveChanges();
            }

            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                var result = service.Get(2);
                Assert.Null(result);
            }
        }

        [Test]
        public void ShouldReturnAllWhenGetAll()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationContext(_options))
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

            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                var result = service.GetAll();
                Assert.AreEqual(3, result.Count());
            }
        }

        [Test]
        public void ShouldDeleteWhenExists()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationContext(_options))
            {
                context.Hospitals.Add(new Hospital
                {
                    Name = "Hospital #12",
                    Address = "Zhandosov st."
                });

                context.SaveChanges();
            }

            using (var context = new ApplicationContext(_options))
            {
                var service = new HospitalService(context);
                service.Delete(1);
            }

            using (var context = new ApplicationContext(_options))
            {
                Assert.AreEqual(0, context.Hospitals.Count());
            }
        }
    }
}