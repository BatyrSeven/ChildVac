using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        public async Task<IEnumerable<Parent>> Get()
        {
            return await _context.Parents.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Parent> Get(int id)
        {
            return await _context.Parents.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("iin/{iin}")]
        public IEnumerable<Parent> FindByIin(string iin)
        {
            if (string.IsNullOrWhiteSpace(iin) || iin.Length < 4)
            {
                return null;
            }

            return _context.Parents.Where(x => x.Iin.Contains(iin)).Take(10);
        }

        // POST api/<controller>
        //[Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task Post([FromBody]Parent parent)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 500;
                await Response.WriteAsync("Passed model is invalid");
            }

            parent.Password = "123456";

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Parent newParent)
        {
            var parent = await Get(id);

            if (parent == null)
            {
                // TODO: return business error
                Response.StatusCode = 204;
                await Response.WriteAsync("ChiParentld not found");
                return;
            }

            _context.Parents.Update(parent);
            await _context.SaveChangesAsync();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var parent = await Get(id);

            if (parent == null)
            {
                // TODO: return business error
                Response.StatusCode = 204;
                await Response.WriteAsync("Parent not found");
                return;
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
        }
    }
}
