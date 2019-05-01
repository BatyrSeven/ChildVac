using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Application.Models;
using ChildVac.WebApi.Application.Utils;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Prescription operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Prescription Controller contructor
        /// </summary>
        /// <param name="context">Application Database Context</param>
        public PrescriptionController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Returns all prescriptions of Child
        /// </summary>
        /// <returns>all prescriptions</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Prescription>> Get()
        {
            var iin = User.Identity.Name;
            var parent = AccountHelper.GetParentByIin(_context, iin);
            var childrenIdList = parent.Children.Select(x => x.Id);
            var prescriptions = _context.Prescriptions
                .Include(x => x.Ticket)
                    .ThenInclude(x => x.Doctor)
                .Where(x => childrenIdList.Contains(x.Ticket.ChildId))
                .OrderBy(x => x.Id);

            return Ok(prescriptions);
        }

        /// <summary>
        ///     Finds Prescription by ID
        /// </summary>
        /// <param name="id">Prescription ID</param>
        /// <returns>Prescription found by ID</returns>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Prescription> GetById(int id)
        {
            var iin = User.Identity.Name;
            var parent = AccountHelper.GetParentByIin(_context, iin);
            var childrenIdList = parent.Children.Select(x => x.Id);
            var prescription = _context.Prescriptions
                    .Include(x => x.Ticket)
                .FirstOrDefault(x => x.Id == id);

            if (prescription == null)
            {
                return NoContent();
            }

            if (!childrenIdList.Contains(prescription.Ticket.ChildId))
            {
                return Unauthorized();
            }

            return Ok(prescription);
        }

        /// <summary>
        ///     Adds new prescription
        /// </summary>
        /// <param name="prescription">new prescription</param>
        /// <returns>Message Model with success or fail message</returns>
        [Authorize(Roles = "Doctor, Admin")]
        [HttpPost]
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody] Prescription prescription)
        {
            prescription.DateTime = DateTime.Now;
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = prescription.Id },
                new MessageResponseModel(true,
                    new MessageModel("Запись на прием была успешно сохранена.")));
        }

        /// <summary>
        ///     Updates prescription by id
        /// </summary>
        /// <param name="id">prescription id</param>
        /// <param name="prescription">new prescription</param>
        /// <returns>Message Model with success or fail message</returns>
        [Authorize(Roles = "Doctor, Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Prescription prescription)
        {
            if (prescription == null)
                return NotFound();

            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        ///     Deletes prescription by id
        /// </summary>
        /// <param name="id">prescription id</param>
        /// <returns>Message Model with success or fail message</returns>
        [Authorize(Roles = "Doctor, Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (prescription == null)
                return NotFound();

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
