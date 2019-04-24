using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public int ChildId { get; set; }
        public Child Child { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
