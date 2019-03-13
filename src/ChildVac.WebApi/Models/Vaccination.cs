using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Vaccination
    {
        public int Id { get; set; }
        public Vaccine Vaccine { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime DateTime { get; set; }
    }
}
