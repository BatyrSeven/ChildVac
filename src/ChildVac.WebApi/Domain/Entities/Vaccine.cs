using System;
using System.Collections.Generic;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RecieveMonth { get; set; }
        public string Description { get; set; }
    }
}
