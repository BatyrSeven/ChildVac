using System;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;

namespace ChildVac.Test
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationContext context)
        {
            context.Admins.Add(GetAdminUser());
            context.Children.Add(GetChildUser());
            context.Doctors.Add(GetDoctorUser());
            context.Parents.Add(GetParentUser());
            context.SaveChanges();
        }

        public static Parent GetParentUser()
        {
            return new Parent
            {
                Login = "parent_login",
                Password = "12345",
                FirstName = "Parent Name",
                LastName = "Parent Surname",
                Address = "test address"
            };
        }

        public static Doctor GetDoctorUser()
        {
            return new Doctor
            {
                Login = "doctor_login",
                Password = "12345",
                FirstName = "Doctor Name",
                LastName = "Doctor Surname"
            };
        }

        public static Child GetChildUser()
        {
            return new Child
            {
                Login = "child_login",
                Password = "12345",
                FirstName = "Child Name",
                LastName = "Child Surname",
                Iin = "990724300739",
                DateOfBirth = Convert.ToDateTime("1999, 07, 24")
            };
        }

        public static Admin GetAdminUser()
        {
            return new Admin
            {
                Login = "admin_login",
                Password = "12345",
                FirstName = "Admin Name",
                LastName = "Admin Surname"
            };
        }
    }
}