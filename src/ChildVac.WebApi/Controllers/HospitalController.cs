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
                .OrderBy(x => x.Id);
        }

        // GET: api/Hospital/5
        [HttpGet("{id}", Name = "GetHospital")]
        public Hospital Get(int id)
        {
            return _context.Hospitals
                .FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Hospital
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post([FromBody] Hospital hospital)
        {
            if(ModelState.IsValid)
            {
                _context.Hospitals.Add(hospital);
                _context.SaveChanges();
            }
        }

        // PUT: api/Hospital/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hospital hospital)
        {
            if (hospital == null) return;
            
            _context.Hospitals.Update(hospital);
            _context.SaveChanges();
        }

        // DELETE: api/Hospital/5
        [Authorize(Roles = "Admin")]
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
