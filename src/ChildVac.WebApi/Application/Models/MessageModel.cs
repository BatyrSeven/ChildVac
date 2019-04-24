namespace ChildVac.WebApi.Application.Models
{
    public class MessageModel
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public MessageModel(string title = "", string text = "")
        {
            Title = title;
            Text = text;
        }
    }
}