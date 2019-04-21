namespace ChildVac.WebApi.Application.Models
{
    public class SuccessResponseModel : ResponseBaseModel<bool>
    {
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public override bool Result
        {
            get { return true; }
            set { }
        }
    }
}
