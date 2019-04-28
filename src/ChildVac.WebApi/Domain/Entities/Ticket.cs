using System;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Represents the ticket for consultation
    /// </summary>
    public class Ticket
    {
        /// <summary>
        ///     ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Ticket title for some important information
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Ticket text for additional infomation
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     The number of room
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        ///     DateTime of Ticket
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///     Type of ticket: Consultation or Vaccination
        /// </summary>
        public TicketType TicketType {get;set;}

        /// <summary>
        ///     Child Id
        /// </summary>
        public int ChildId { get; set; }

        /// <summary>
        ///     Child
        /// </summary>
        public Child Child { get; set; }

        /// <summary>
        ///     Doctor Id
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        ///     Doctor
        /// </summary>
        public Doctor Doctor { get; set; }
    }
}
