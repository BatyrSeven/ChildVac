using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Used by Parents for leaving feedback
    /// </summary>
    public class Feedback
    {
        /// <summary>
        ///     Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Name of Doctor that Parent complains about
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        ///     Feedback text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Creation DateTime
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     Rate of application by parent 
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        ///     User Id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        ///     User
        /// </summary>
        public User User { get; set; }
    }
}
