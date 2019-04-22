using System;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Child : User
    {
        public DateTime DateOfBirth { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
