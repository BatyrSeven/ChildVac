using System;
using System.Collections.Generic;

namespace ChildVac.WebApi.Application.Models
{
    public class VaccinationСardModel
    {
        public string ChildName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<VaccineCardItem> Items { get; set; }

        public class VaccineCardItem
        {
            public string VaccineName { get; set; }
            public DateTime DateTime { get; set; }
            public int AgeInMonth { get; set; }
            public string DoctorName { get; set; }
        }
    }
}
