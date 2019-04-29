using System;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Represents the Prescription for consultation
    /// </summary>
    public class Prescription
    {
        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Creation DateTime
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     Diagnosis defined by Doctor
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        ///     Additional notes
        /// </summary>
        public string Medication { get; set; }

        /// <summary>
        ///     Additional notes
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Prescription type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Id of binded Ticket
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        ///     Binded Ticket
        /// </summary>
        public Ticket Ticket { get; set; }
    }
}
