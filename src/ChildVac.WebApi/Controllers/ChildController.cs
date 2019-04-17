using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IEnumerable<Child>> Get()
        {
            return await _context.Children.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Child> Get(int id)
        {
            return await _context.Children.FirstOrDefaultAsync(x => x.Id == id);
        }

        // POST api/<controller>
        //[Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task Post([FromBody]Child child)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 500;
                await Response.WriteAsync("Passed model is invalid");
            }

            child.Password = "123456";

            _context.Children.Add(child);
            await _context.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Child newChild)
        {
            var child = await Get(id);

            if (child == null)
            {
                // TODO: return business error
                Response.StatusCode = 404;
                await Response.WriteAsync("Child not found");
                return;
            }

            _context.Children.Update(child);
            await _context.SaveChangesAsync();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var child = await Get(id);

            if (child == null)
            {
                // TODO: return business error
                Response.StatusCode = 404;
                await Response.WriteAsync("Child not found");
                return;
            }

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
        }
    }
}
