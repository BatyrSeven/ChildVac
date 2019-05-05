using System.Collections.Generic;
using System.Linq;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public VaccineController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Vaccine
        [HttpGet]
        public IEnumerable<Vaccine> Get()
        {
            return _context.Vaccines
                .OrderBy(x => x.RecieveMonth);
        }

        // GET: api/Vaccine/5
        [HttpGet("{id}", Name = "GetVaccine")]
        public Vaccine Get(int id)
        {
            return _context.Vaccines
                .FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Vaccine
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post([FromBody] Vaccine vaccine)
        {
            if (vaccine == null) return;

            _context.Vaccines.Add(vaccine);
            _context.SaveChanges();
        }

        // PUT: api/Vaccine/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vaccine vaccine)
        {
            if (vaccine == null) return;
            
            _context.Vaccines.Update(vaccine);
            _context.SaveChanges();
        }

        // DELETE: api/Vaccine/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var vaccine = Get(id);

            if (vaccine == null) return;

            _context.Vaccines.Remove(vaccine);
            _context.SaveChanges();
        }
    }
}
