using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Vaccination
    {
        public int Id { get; set; }
        public Vaccine Vaccine { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime DateTime { get; set; }
    }
}
