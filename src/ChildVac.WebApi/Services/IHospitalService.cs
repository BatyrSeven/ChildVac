using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Services
{
    public interface IHospitalService
    {
        void Add(string name, string address);
        Hospital Get(int id);
        IEnumerable<Hospital> GetAll();
        void Delete(int id);
        IEnumerable<Hospital> Find(string name);
    }
}
