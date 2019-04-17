using System.Collections.Generic;
using ChildVac.WebApi.Models;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
