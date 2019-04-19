namespace ChildVac.WebApi.Application.Models
{
    public class ErrorResponseModel : ResponseBaseModel<bool>
    {
        public string MessageTitle { get; set; } = "Извините, произошла ошибка.";
        public string MessageText { get; set; } = "Попробуйте снова.";
        public override bool Result
        {
            get { return false; }
            set { }
        }
    }
}
