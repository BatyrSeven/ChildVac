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
            var child = _context.Children
                .Include(c => c.Parent)
                .FirstOrDefault(x => x.Id == childId);

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
                Address = child.Parent.Address,
                DateOfBirth = child.DateOfBirth.ToString("dd.MM.yyyy"),
                Items = tickets.Select(x => new VaccinationСardModel.VaccineCardItem
                {
                    VaccineName = x.Vaccine.Name,
                    DoctorName = $"{x.Doctor.LastName} {x.Doctor.FirstName} {x.Doctor.Patronim}",
                    DateTime = x.StartDateTime,
                    AgeInMonth = x.Vaccine.RecieveMonth
                }).ToList()
            };

            return View(model);
        }

        [Route("prescription/{prescriptionId}")]
        public IActionResult Prescription(int prescriptionId)
        {
            var prescription = _context.Prescriptions
                .Include(p => p.Ticket)
                    .ThenInclude(t => t.Doctor)
                .FirstOrDefault(x => x.Id == prescriptionId);

            if (ReferenceEquals(prescription, null))
            {
                return NotFound($"Prescription with id '{prescriptionId}' was not found");
            }

            var doctor = prescription.Ticket.Doctor;
            var doctorFullName = $"{doctor.LastName} {doctor.FirstName} {doctor.Patronim}";

            var model = new PrintPrescriptionModel
            {
                DateTime = prescription.DateTime,
                Description = prescription.Description,
                Diagnosis = prescription.Diagnosis,
                Medication = prescription.Medication,
                Type = prescription.Type,
                DoctorName = doctorFullName
            };

            return View(model);
        }
    }
}
