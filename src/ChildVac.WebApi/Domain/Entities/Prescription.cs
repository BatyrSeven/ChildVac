using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Diagnosis { get; set; }
        public string Description { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
