using System;
using System.Collections.Generic;
using System.Linq;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Application.Utils
{
    /// <summary>
    ///     Util for users entities
    /// </summary>
    public static class AccountHelper
    {
        /// <summary>
        ///     Finds the user by IIN
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="iin">User IIN</param>
        /// <returns>User entity</returns>
        public static User GetUserByIin(ApplicationContext context, string iin)
        {
            if (context == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(iin))
            {
                return null;
            }

            var user = context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Iin == iin);

            return user;
        }

        public static Parent GetParentByIin(ApplicationContext context, string iin)
        {
            if (context == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(iin))
            {
                return null;
            }

            var parent = context.Parents
                .Include(p => p.Role)
                .Include(p => p.Children)
                .FirstOrDefault(p => p.Iin == iin);

            return parent;
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
