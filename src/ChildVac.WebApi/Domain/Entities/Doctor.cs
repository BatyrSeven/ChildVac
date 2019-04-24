namespace ChildVac.WebApi.Domain.Entities
{
    public class Doctor : User
    {
        public string PhoneNumber { get;set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
