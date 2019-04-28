using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Application.Models;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TicketController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            return Ok(_context.Tickets
                .OrderBy(x => x.Id));
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetById(int id)
        {
            return _context.Tickets
                .Include(x => x.Child)
                .FirstOrDefault(x => x.Id == id);
        }

        // GET: api/Ticket/doctor/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("doctor/{doctorId}")]
        public ActionResult<ResponseBaseModel<IEnumerable<TicketResponseModel>>> GetByDoctorId(int doctorId)
        {
            try
            {
                var tickets = _context.Tickets
                    .Where(x => x.DoctorId == doctorId)
                    .Include(x => x.Child);

                var result = tickets.Select(x => new TicketResponseModel(x))
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Time);

                return Ok(new ResponseBaseModel<IEnumerable<TicketResponseModel>>
                {
                    Result = result
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }
        }

        // POST: api/Ticket
        [Authorize(Roles = "Doctor, Admin")]
        [HttpPost]
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody] Ticket ticket)
        {
            try
            {
                var doctorIin = User?.Identity?.Name;

                if (!string.IsNullOrWhiteSpace(doctorIin))
                {
                    var user = _context.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Iin == doctorIin);

                    if (user != null)
                    {
                        ticket.DoctorId = user.Id;
                        _context.Tickets.Add(ticket);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }

            return CreatedAtAction(nameof(GetById), new { id = ticket.Id },
                new MessageResponseModel(true,
                    new MessageModel("Запись на прием была успешно сохранена.")));
        }

        // PUT: api/Ticket/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Ticket hospital)
        {
            if (hospital == null)
                return NotFound();

            _context.Tickets.Update(hospital);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Ticket/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
                return NotFound();

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
