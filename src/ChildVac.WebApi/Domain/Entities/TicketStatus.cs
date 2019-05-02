namespace ChildVac.WebApi.Domain.Entities
{
    /// <summary>
    ///     Current status of the Ticket
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        ///     Something went wrong
        /// </summary>
        Undefined,

        /// <summary>
        ///     Ticket was created and wating for accpetance
        /// </summary>
        Waiting,

        /// <summary>
        ///     Ticket was successfully closed
        /// </summary>
        Closed,

        /// <summary>
        ///     Ticket was canceled
        /// </summary>
        Canceled
    }
}