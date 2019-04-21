﻿using System;
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
    public class ParentController : Controller
    {
        private readonly ApplicationContext _context;

        public ParentController(ApplicationContext context)
        {
            _context = context;
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
                return StatusCode(500, new ErrorResponseModel());
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBaseModel<Parent>>> Get(int id)
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
                return StatusCode(500, new ErrorResponseModel());
            }
        }

        [HttpGet("iin/{iin}")]
        public ActionResult<ResponseBaseModel<IEnumerable<Parent>>> FindByIin(string iin)
        {
            if (string.IsNullOrWhiteSpace(iin) || iin.Length < 4)
            {
                return StatusCode(500, new ErrorResponseModel()
                {
                    MessageTitle = "Недостаточно символов для поиска.",
                    MessageText = "Для поиска по ИИН введите минимум 4 символа."
                });
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
                return StatusCode(500, new ErrorResponseModel());
            }
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task<ActionResult<SuccessResponseModel>> Post([FromBody]Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, new ErrorResponseModel());
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Iin.Equals(parent.Iin));
            if (user != null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageText = "Пользователь с данным ИИН уже зарегистрирован.",
                    MessageTitle = "Проверьте правильность данных и попробуйте снова."
                });
            }

            parent.Password = "123456";

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return Ok(new SuccessResponseModel
            {
                MessageTitle = "Регистрация прошла успешно!",
                MessageText = "Временный пароль для входа в систему: " + parent.Password
            });
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SuccessResponseModel>> Put(int id, [FromBody]Parent newParent)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == id);

            if (parent == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageText = "Пользователь с данным ID не найден.",
                    MessageTitle = "Проверьте правильность данных и попробуйте снова."
                });
            }

            _context.Parents.Update(newParent);
            await _context.SaveChangesAsync();

            return Ok(new SuccessResponseModel
            {
                MessageTitle = "Данные были успешно обновлены."
            });
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuccessResponseModel>> Delete(int id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == id);

            if (parent == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageText = "Пользователь с данным ID не найден.",
                    MessageTitle = "Проверьте правильность данных и попробуйте снова."
                });
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return Ok(new SuccessResponseModel
            {
                MessageTitle = "Пользователь был успешно удален."
            });
        }
    }
}
