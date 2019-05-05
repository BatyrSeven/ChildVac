using System.Collections.Generic;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Parent entity
    /// </summary>
    public class Parent : User
    {
        /// <summary>
        ///     List of binded childern
        /// </summary>
        public List<Child> Children { get; set; }

        /// <summary>
        ///     Current address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Phone to contact with Parent
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Email of Parent
        /// </summary>
        public string Email { get; set; }
    }
}
