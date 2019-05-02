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
    ///     Everything about Child
    /// </summary>
    [Route("api/[controller]")]
    public class ChildController : Controller
    {
        private readonly ApplicationContext _context;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context">Application DB context</param>
        public ChildController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Return the list of Parent's children
        /// </summary>
        /// <returns>List of Parent's children</returns>
        [Authorize(Roles="Parent")]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<IEnumerable<Child>>>> Get()
        {
            try
            {
                var iin = User.Identity.Name;
                var parent = AccountHelper.GetParentByIin(_context, iin);
                var children = _context.Children
                    .Where(x => x.ParentId == parent.Id)
                    .Include(x => x.Parent);

                return Ok(new ResponseBaseModel<IEnumerable<Child>>
                {
                    Result = children
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
        ///     Returns Child by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Searches children by IIN (min length = 4)
        /// </summary>
        /// <param name="iin">Child IIN</param>
        /// <returns>List of children that IIN match</returns>
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

        /// <summary>
        ///     Adds new Child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Updates Child by ID
        /// </summary>
        /// <param name="id">Child Id</param>
        /// <param name="newChild">New Child entity</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Deletes Child by Id
        /// </summary>
        /// <param name="id">Child Id</param>
        /// <returns></returns>
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
