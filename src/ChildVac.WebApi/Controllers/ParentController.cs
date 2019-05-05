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
using Microsoft.Extensions.Logging;
using NLog;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ParentController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ParentController> _logger;

        public ParentController(ApplicationContext context ,ILogger<ParentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<IEnumerable<Parent>>>> Get()
        {
            try
            {
                return Ok(new ResponseBaseModel<IEnumerable<Parent>>
                {
                    Result = await _context.Parents.ToListAsync()
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBaseModel<Parent>>> GetById(int id)
        {
            try
            {
                return Ok(new ResponseBaseModel<Parent>
                {
                    Result = await _context.Parents.FirstOrDefaultAsync(x => x.Id == id)
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }
        }

        [HttpGet("iin/{iin}")]
        public ActionResult<ResponseBaseModel<IEnumerable<Parent>>> FindByIin(string iin)
        {
            if (string.IsNullOrWhiteSpace(iin) || iin.Length < 4)
            {
                return NotFound(new MessageResponseModel(false,
                    new MessageModel("Недостаточно символов для поиска.",
                        "Для поиска по ИИН введите минимум 4 символа.")));
            }

            try
            {
                return Ok(new ResponseBaseModel<IEnumerable<Parent>>
                {
                    Result = _context.Parents.Where(x => x.Iin.Contains(iin)).Take(10).ToList()
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody]Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Iin.Equals(parent.Iin));
            if (user != null)
            {
                return BadRequest(new MessageResponseModel(false,
                    new MessageModel("Пользователь с данным ИИН уже зарегистрирован.",
                        "Проверьте правильность данных и попробуйте снова.")));
            }

            parent.Password = AccountHelper.GenerateRandomPassword();
            parent.Role = _context.Roles.FirstOrDefault(
                x => x.Name.Equals("Parent",
                StringComparison.InvariantCultureIgnoreCase));

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            try
            {
                var emailSubject = "Регистрация в системе ChildVac";
                var emailBody = $"Здравствуйте, {parent.FirstName} {parent.Patronim}!";
                emailBody += "\n\nВы были зарегистрированы в системе ChildVac.";
                emailBody += "\nИспользуйте указанные ниже данные для входа:";
                emailBody += $"\nЛогин: {parent.Iin}";
                emailBody += $"\nПароль: {parent.Password}";
                emailBody += "\n\n С уваженим, администрация ChildVac";
                SmtpServiceHelper.SendMail(parent.Email, emailSubject, emailBody);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to send email");
            }

            return CreatedAtAction(nameof(GetById), new { id = parent.Id },
                new MessageResponseModel(true,
                    new MessageModel("Регистрация прошла успешно!",
                        "На Email родителя было отправлено письмо с данными для авторизации.")));
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Put(int id, [FromBody]Parent newParent)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == id);

            if (parent == null)
            {
                return NotFound(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь с данным ID не найден.",
                            "Проверьте правильность данных и попробуйте снова.")));
            }

            _context.Parents.Update(newParent);
            await _context.SaveChangesAsync();

            return Ok(
                new MessageResponseModel(true,
                    new MessageModel("Данные были успешно обновлены.")));
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Delete(int id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == id);

            if (parent == null)
            {
                return NotFound(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь с данным ID не найден.",
                            "Проверьте правильность данных и попробуйте снова.")));
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return Ok(
                new MessageResponseModel(true,
                    new MessageModel("Пользователь был успешно удален.")));
        }
    }
}
