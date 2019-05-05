using System.Linq;
using ChildVac.Test.Extensions;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;

namespace ChildVac.Test
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationContext context)
        {
            context.ResetValueGenerators();
            context.Database.EnsureDeleted();

            AddRoles(context);

            AddAdminUser(context);
            AddParentUser(context);
            AddChildUser(context);
            AddDoctorUser(context);
        }

        public static void AddRoles(ApplicationContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "Admin" });
            context.Roles.Add(new Role { Id = 2, Name = "Child" });
            context.Roles.Add(new Role { Id = 3, Name = "Doctor" });
            context.Roles.Add(new Role { Id = 4, Name = "Parent" });
            context.SaveChanges();
        }

        public static void AddParentUser(ApplicationContext context)
        {
            context.Parents.Add(new Parent
            {
                Iin = "123456789004",
                Password = "123456",
                FirstName = "Parent Name",
                LastName = "Parent Surname",
                Address = "test address",
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Parent"))
            });
            context.SaveChanges();
        }

        public static void AddDoctorUser(ApplicationContext context)
        {
            context.Doctors.Add(new Doctor
            {
                Iin = "123456789003",
                Password = "123456",
                FirstName = "Doctor Name",
                LastName = "Doctor Surname",
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Doctor"))
            });
            context.SaveChanges();
        }

        public static void AddChildUser(ApplicationContext context)
        {
            context.Children.Add(new Child
            {
                Iin = "123456789002",
                Password = "123456",
                FirstName = "Child Name",
                LastName = "Child Surname",
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Child"))
            });
            context.SaveChanges();
        }

        public static void AddAdminUser(ApplicationContext context)
        {
            context.Admins.Add(new Admin
            {
                Iin = "123456789001",
                Password = "123456",
                FirstName = "Admin Name",
                LastName = "Admin Surname",
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Admin"))
            });
            context.SaveChanges();
        }
    }
}