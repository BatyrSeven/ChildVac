using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Parent : User
    {
        public List<Child> Children { get; set; }
    }
}
