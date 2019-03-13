using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan RecieveTime { get; set; }
    }
}
