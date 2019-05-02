using System.Linq;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
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
    }
}
