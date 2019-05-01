using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Advice for users
    /// </summary>
    public class Advice
    {
        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Text
        /// </summary>
        public string Text { get; set; }
    }
}
