using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime DateTime { get; set; }
        public string Diagnosis { get; set; }
        public string Description { get; set; }
    }
}
