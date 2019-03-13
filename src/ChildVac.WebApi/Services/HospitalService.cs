using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Services
{
    public class HospitalService : IHospitalService
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

        public Hospital Get(int id)
        {
            return _context.Hospitals
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Hospital> GetAll()
        {
            return _context.Hospitals
                .OrderBy(x => x.Name);
        }

        public void Delete(int id)
        {
            var hospital = Get(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                _context.SaveChanges();
            }
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
