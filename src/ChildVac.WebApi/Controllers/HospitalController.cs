using System;
using System.Collections.Generic;
using ChildVac.WebApi.Models;
using ChildVac.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private IHospitalService _service;

        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        // GET: api/Hospital
        [HttpGet]
        public IEnumerable<Hospital> Get()
        {
            return _service.GetAll();
        }

        // GET: api/Hospital/5
        [HttpGet("{id}", Name = "Get")]
        public Hospital Get(int id)
        {
            return _service.Get(id);
        }

        // POST: api/Hospital
        [HttpPost]
        public void Post([FromBody] string name, [FromBody] string address)
        {
            _service.Add(name, address);
        }

        // PUT: api/Hospital/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
