namespace ChildVac.WebApi.Application.Models
{
    public class TokenRequestModel
    {
        public string Iin { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
