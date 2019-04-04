using System;
using System.Linq;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;

namespace ChildVac.Test
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationContext context)
        {
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
                Login = "parent_login",
                Password = "12345",
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
                Login = "doctor_login",
                Password = "12345",
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
                Login = "child_login",
                Password = "12345",
                FirstName = "Child Name",
                LastName = "Child Surname",
                Iin = "990724300739",
                DateOfBirth = Convert.ToDateTime("1999, 07, 24"),
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Child"))
            });
            context.SaveChanges();
        }

        public static void AddAdminUser(ApplicationContext context)
        {
            context.Admins.Add(new Admin
            {
                Login = "admin_login",
                Password = "12345",
                FirstName = "Admin Name",
                LastName = "Admin Surname",
                Role = context.Roles.FirstOrDefault(x => x.Name.Equals("Admin"))
            });
            context.SaveChanges();
        }
    }
}