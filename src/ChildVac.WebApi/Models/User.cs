using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Iin { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronim { get;set; }
        public Gender Gender { get; set; }
        
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
