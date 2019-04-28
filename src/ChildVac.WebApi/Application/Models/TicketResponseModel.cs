using ChildVac.WebApi.Domain.Entities;

namespace ChildVac.WebApi.Application.Models
{
    /// <summary>
    ///     Represents the response for tiket on the doctors calendar
    /// </summary>
    public class TicketResponseModel
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public TicketResponseModel()
        {
        }

        /// <summary>
        ///     Paramterized constructor
        /// </summary>
        /// <param name="ticket">Ticket entity</param>
        public TicketResponseModel(Ticket ticket)
        {
            Id = ticket.Id;
            Date = ticket.StartDateTime.ToString("yyyy/MM/dd").Replace(".", "/");
            Time = ticket.StartDateTime.ToString("HH:mm");
            ChildId = ticket.ChildId;
            ChildName = $"{ticket.Child?.LastName} {ticket.Child?.FirstName} {ticket.Child?.Patronim}";
            ChildIin = ticket.Child?.Iin;
        }

        /// <summary>
        ///     ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///     Time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        ///     Child Id
        /// </summary>
        public int ChildId { get; set; }

        /// <summary>
        ///     Child Name
        /// </summary>
        public string ChildName { get; set; }

        /// <summary>
        ///     Child IIN
        /// </summary>
        public string ChildIin { get; set; }

        /// <summary>
        ///     Calendar event title
        /// </summary>
        public string Title => ChildName;
    }
}