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
    ///     For recieving random advices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdviceController : ControllerBase
    {
        private readonly ApplicationContext _context;

        /// <summary>
        ///     Constructor of AdviceController
        /// </summary>
        /// <param name="context"></param>
        public AdviceController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Returns all Advices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Advice> Get()
        {
            return _context.Advices;
        }

        /// <summary>
        ///     Returns random Advice
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        public Advice GetRandom()
        {
            var advices = _context.Advices.ToList();
            var length = advices.Count();
            if (length > 0)
            {
                Random random = new Random();
                var randomIndex = random.Next(length);
                return advices[randomIndex];
            }

            return null;
        }
    }
}