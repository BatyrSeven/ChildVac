using System.Collections.Generic;
using System.Linq;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Hospital
        [HttpGet]
        public IEnumerable<Hospital> Get()
        {
            return _context.Hospitals
                .OrderBy(x => x.Name);
        }

        // GET: api/Hospital/5
        [HttpGet("{id}", Name = "Get")]
        public Hospital Get(int id)
        {
            return _context.Hospitals
                .FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Hospital
        [HttpPost]
        public void Post([FromBody] Hospital hospital)
        {
            if (hospital == null) return;

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
        }

        // PUT: api/Hospital/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hospital hospital)
        {
            if (hospital == null) return;
            
            _context.Hospitals.Update(hospital);
            _context.SaveChanges();
        }

        // DELETE: api/Hospital/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var hospital = Get(id);

            if (hospital == null) return;

            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();
        }
    }
}
