using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Child Child { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
