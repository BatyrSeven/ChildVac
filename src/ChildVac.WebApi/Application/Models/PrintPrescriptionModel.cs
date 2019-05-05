using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Application.Models
{
    public class PrintPrescriptionModel
    {
        public DateTime DateTime { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DoctorName { get; set; }
    }
}
