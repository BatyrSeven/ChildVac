namespace ChildVac.WebApi.Application.Models
{
    public class ResponseBaseModel<T>
    {
        public virtual T Result { get; set; }
    }
}
