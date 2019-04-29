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
    /// <summary>
    ///     Everything with Tickets
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context">Application Database Context</param>
        public TicketController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Returns all Tickets of parent's child
        /// </summary>
        /// <returns>List of tickets</returns>
        [Authorize(Roles = "Parent")]
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            var iin = User.Identity.Name;
            var parent = AccountHelper.GetParentByIin(_context, iin);
            var childrenIdList = parent.Children.Select(x => x.Id);
            var tickets = _context.Tickets
                .Where(x => childrenIdList.Contains(x.ChildId))
                .OrderBy(x => x.Id);

            return Ok(tickets);
        }

        /// <summary>
        ///     Finds Ticket by Id
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Found ticket</returns>
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetById(int id)
        {
            return _context.Tickets
                .Include(x => x.Child)
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///     Return the tickets created by the doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet("doctor/{doctorId}")]
        public ActionResult<ResponseBaseModel<IEnumerable<Ticket>>> GetByDoctorId(int doctorId)
        {
            try
            {
                var tickets = _context.Tickets
                    .Where(x => x.DoctorId == doctorId)
                    .Include(x => x.Child)
                    .Include(x => x.Prescriptions)
                    .OrderBy(x => x.StartDateTime);

                return Ok(new ResponseBaseModel<IEnumerable<Ticket>>
                {
                    Result = tickets
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }
        }

        /// <summary>
        ///     Adds new Ticket
        /// </summary>
        /// <param name="ticket">New Ticket</param>
        /// <returns>Response with message of request status</returns>
        [Authorize(Roles = "Doctor, Admin")]
        [HttpPost]
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody] Ticket ticket)
        {
            try
            {
                var iin = User?.Identity?.Name;
                var user = AccountHelper.GetUserByIin(_context, iin);
                ticket.DoctorId = user.Id;
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
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

        /// <summary>
        ///     Updates Ticket by Id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="ticket">new Ticket model</param>
        /// <returns>Response with message of request status</returns>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Put(int id, [FromBody] Ticket ticket)
        {
            try
            {
                if (ticket == null)
                    return NotFound();

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }

            return Ok(new MessageResponseModel(true,
                new MessageModel("Данные успешно обновлены")));
        }

        /// <summary>
        ///     Deletes the Ticket
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Response with message of request status</returns>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Delete(int id)
        {
            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

                if (ticket == null)
                    return NotFound();

                var prescriptions = _context.Prescriptions
                    .Where(x => x.TicketId == id);

                foreach (var prescription in prescriptions)
                {
                    _context.Prescriptions.Remove(prescription);
                }

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }

            return Ok(new MessageResponseModel(true,
                new MessageModel("Запись на прием была успешно удалена")));
        }
    }
}
