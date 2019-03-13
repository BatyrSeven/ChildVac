using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Child : User
    {
        public string Iin { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
