using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Application.Models;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Controllers
{
    [Route("print")]
    public class PrintController : Controller
    {
        private readonly ApplicationContext _context;

        public PrintController(ApplicationContext context)
        {
            _context = context;
        }

        [Route("vaccination-card/{childId}")]
        public IActionResult VaccinationСard(int childId)
        {
            var child = _context.Children.FirstOrDefault(x => x.Id == childId);

            if (ReferenceEquals(child, null))
            {
                return NotFound($"Child with id '{childId}' was not found");
            }

            var tickets = _context.Tickets
                .Include(x => x.Child)
                .Include(x => x.Doctor)
                .Include(x => x.Vaccine)
                .Where(x => x.ChildId == childId && 
                            x.TicketType == Domain.Entities.TicketType.Vaccination);

            var model = new VaccinationСardModel
            {
                ChildName = $"{child.LastName} {child.FirstName} {child.Patronim}",
                Items = tickets.Select(x => new VaccinationСardModel.VaccineCardItem
                {
                    VaccineName = x.Vaccine.Name,
                    DoctorName = $"{x.Doctor.LastName} {x.Doctor.FirstName} {x.Doctor.Patronim}",
                    DateTime = x.StartDateTime
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Prescription(int prescriptionId)
        {
            var prescription = _context.Prescriptions
                .FirstOrDefault(x => x.Id == prescriptionId);

            if (ReferenceEquals(prescription, null))
            {
                return NotFound();
            }

            return View();
        }
    }
}
