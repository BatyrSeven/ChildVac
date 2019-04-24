using System.Globalization;
using ChildVac.WebApi.Domain.Entities;

namespace ChildVac.WebApi.Application.Models
{
    public class TicketResponseModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int ChildId { get; set; }

        public string ChildName { get; set; }

        public string ChildIin { get; set; }

        public string Title => ChildName;

        public TicketResponseModel()
        {
            
        }

        public TicketResponseModel(Ticket ticket)
        {
            Id = ticket.Id;
            Date = ticket.StartDateTime.ToString("yyyy/MM/dd").Replace(".", "/");
            Time = ticket.StartDateTime.ToString("HH:mm");
            ChildId = ticket.ChildId;
            ChildName = $"{ticket.Child?.LastName} {ticket.Child?.FirstName} {ticket.Child?.Patronim}";
            ChildIin = ticket.Child?.Iin;
        }
    }
}
