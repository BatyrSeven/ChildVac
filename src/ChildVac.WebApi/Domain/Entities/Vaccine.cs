using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan RecieveTime { get; set; }
    }
}
