using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Services
{
    public class HospitalService
    {
        private ApplicationContext _context;

        public HospitalService(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(string name, string address)
        {
            var hospital = new Hospital
            {
                Name = name,
                Address = address
            };

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
        }

        public IEnumerable<Hospital> Find(string name)
        {
            return _context.Hospitals
                .Where(x => x.Name.Contains(name))
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
