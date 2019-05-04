using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Application.Models
{
    public class VaccinationСardModel
    {
        public string ChildName { get; set; }
        public List<VaccineCardItem> Items { get; set; }

        public class VaccineCardItem
        {
            public string VaccineName { get; set; }
            public string DoctorName { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}
