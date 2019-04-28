using System;

namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Type of ticket: Consultation or Vaccination
    /// </summary>
    public enum TicketType
    {
        /// <summary>
        ///     Ticket type is not defined 
        /// </summary>
        Undefined,

        /// <summary>
        ///     Consultation with the doctor
        /// </summary>
        Consultation, 

        /// <summary>
        ///     Child's Vaccination
        /// </summary>
        Vaccination
    }
}
