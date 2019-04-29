using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Application.Models;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ChildVac.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Some useless stuff
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ApplicationContext _context;

        /// <summary>
        ///     Constructor of FeedbackController
        /// </summary>
        /// <param name="context"></param>
        public FeedbackController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Returns all Feedbacks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Feedback> Get()
        {
            return _context.Feedbacks.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponseModel>> Post([FromBody] Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, 
                    new MessageResponseModel(false,
                        new MessageModel("Извините, произошла ошибка", "Попробуйте снова")));
            }

            return Ok(
                new MessageResponseModel(true,
                    new MessageModel("Спасибо за отзыв!", "Нам важно наше мнение.")));
        }
    }
}