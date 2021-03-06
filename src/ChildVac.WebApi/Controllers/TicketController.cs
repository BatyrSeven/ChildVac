﻿using System;
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
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<TicketController> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context">Application Database Context</param>
        public TicketController(ApplicationContext context, ILogger<TicketController> logger)
        {
            _context = context;
            _logger = logger;
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
                .Include(x => x.Doctor)
                .Include(x => x.Vaccine)
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
                .Include(x => x.Doctor)
                .Include(x => x.Vaccine)
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
                if (ticket.StartDateTime < DateTime.Now)
                {
                    return BadRequest(new MessageResponseModel(false,
                        new MessageModel("Время приема должно быть позже настоящего времени.",
                            "Укажите корректное значение")));
                }

                var iin = User?.Identity?.Name;
                var user = AccountHelper.GetUserByIin(_context, iin);
                ticket.DoctorId = user.Id;
                ticket.Status = TicketStatus.Waiting;

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                try
                {
                    var childId = ticket.ChildId;
                    var child = _context.Children
                        .Include(x => x.Parent)
                        .FirstOrDefault(x => x.Id == childId);

                    var parent = child?.Parent;

                    if (parent != null)
                    {

                        var emailSubject = "Назначен прием к врачу";
                        var emailBody = $"Здравствуйте, {parent.FirstName} {parent.Patronim}!";
                        emailBody += $"\n\nВашему ребенку была назначена "
                                     + (ticket.TicketType == TicketType.Consultation ? "консультация" : "вакцинация")
                                     + " в системе ChildVac.";
                        emailBody += $"\nВремя: {ticket.StartDateTime.ToString("dd.MM.yyyy HH:mm")}";
                        emailBody += $"\nКабинет: {ticket.Room}";
                        emailBody += "\n\n С уваженим, администрация ChildVac";
                        SmtpServiceHelper.SendMail(parent.Email, emailSubject, emailBody);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to send email");
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

        /// <summary>
        ///     Updates Ticket by Id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="ticket">New Ticket model</param>
        /// <returns>Response with message of request status</returns>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Put(int id, [FromBody] Ticket ticket)
        {
            try
            {
                if (ticket == null)
                    return NotFound();

                ticket.Status = TicketStatus.Waiting;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();

                try
                {
                    var childId = ticket.ChildId;
                    var child = _context.Children
                        .Include(x => x.Parent)
                        .FirstOrDefault(x => x.Id == childId);

                    var parent = child?.Parent;

                    if (parent != null)
                    {
                        var emailSubject = "Изменения в записи к врачу";
                        var emailBody = $"Здравствуйте, {parent.FirstName} {parent.Patronim}!";
                        emailBody += "\n\n"
                                     + (ticket.TicketType == TicketType.Consultation ? "Консультация" : "Вакцинация")
                                     + ", назначенная вам раннее в системе ChildVac была изменена врачем.";
                        emailBody += $"\nВремя: {ticket.StartDateTime:dd.MM.yyyy HH:mm}";
                        emailBody += $"\nКабинет: {ticket.Room}";
                        emailBody += "\n\n С уваженим, администрация ChildVac";
                        SmtpServiceHelper.SendMail(parent.Email, emailSubject, emailBody);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to send email");
                }
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
        ///     Updates the status of Ticket by Id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="ticketWithNewStatus">Ticket with new status</param>
        /// <returns>Response with message of request status</returns>
        [Authorize]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<MessageResponseModel>> PatchStatus(int id, [FromBody] Ticket ticketWithNewStatus)
        {
            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
                if (ticket == null)
                    return NotFound();

                ticket.Status = ticketWithNewStatus.Status;
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
                new MessageModel("Статус был успешно обновлен.")));
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
