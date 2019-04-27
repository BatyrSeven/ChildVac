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
    public class ChildController : Controller
    {
        private readonly ApplicationContext _context;

        public ChildController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<IEnumerable<Child>>>> Get()
        {
            try
            {
                return Ok(new ResponseBaseModel<IEnumerable<Child>>
                {
                    Result = await _context.Children.ToListAsync()
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
        public async Task<ActionResult<ResponseBaseModel<Child>>> GetById(int id)
        {
            try
            {
                return Ok(new ResponseBaseModel<Child>
                {
                    Result = await _context.Children.FirstOrDefaultAsync(x => x.Id == id)
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
        public ActionResult<ResponseBaseModel<IEnumerable<Child>>> FindByIin(string iin)
        {
            if (string.IsNullOrWhiteSpace(iin) || iin.Length < 4)
            {
                return NotFound(new MessageResponseModel(false,
                    new MessageModel("Недостаточно символов для поиска.",
                        "Для поиска по ИИН введите минимум 4 символа.")));
            }

            try
            {
                return Ok(new ResponseBaseModel<IEnumerable<Child>>
                {
                    Result = _context.Children.Where(x => x.Iin.Contains(iin)).Take(10).ToList()
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
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody]Child child)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, new MessageResponseModel(false,
                    new MessageModel("Извините, произошла ошибка.",
                        "Попробуйте снова чуть позже.")));
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Iin.Equals(child.Iin));
            if (user != null)
            {
                return BadRequest(new MessageResponseModel(false,
                    new MessageModel("Пользователь с данным ИИН уже зарегистрирован.",
                        "Проверьте правильность данных и попробуйте снова.")));
            }

            child.Password = "123456";
            child.Role = _context.Roles.FirstOrDefault(
                x => x.Name.Equals("Child",
                StringComparison.InvariantCultureIgnoreCase));

            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = child.Id },
                new MessageResponseModel(true,
                    new MessageModel("Регистрация прошла успешно!")));
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MessageResponseModel>> Put(int id, [FromBody]Child newChild)
        {
            var child = await _context.Children.FirstOrDefaultAsync(x => x.Id == id);

            if (child == null)
            {
                return NotFound(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь с данным ID не найден.",
                            "Проверьте правильность данных и попробуйте снова.")));
            }

            _context.Children.Update(newChild);
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
            var child = await _context.Children.FirstOrDefaultAsync(x => x.Id == id);

            if (child == null)
            {
                return NotFound(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь с данным ID не найден.",
                            "Проверьте правильность данных и попробуйте снова.")));
            }

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();

            return Ok(
                new MessageResponseModel(true,
                    new MessageModel("Пользователь был успешно удален.")));
        }
    }
}
