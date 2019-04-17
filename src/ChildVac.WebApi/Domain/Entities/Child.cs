using ChildVac.WebApi.Models;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Child : User
    {
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
