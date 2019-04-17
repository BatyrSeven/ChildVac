using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Child : User
    {
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
