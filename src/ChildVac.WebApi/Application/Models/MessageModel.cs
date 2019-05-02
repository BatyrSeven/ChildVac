namespace ChildVac.WebApi.Application.Models
{
    /// <summary>
    ///     Represents the success or fail message
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        ///     Alert title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Alert text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Paramterized constructor
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="text">Text</param>
        public MessageModel(string title = "", string text = "")
        {
            Title = title;
            Text = text;
        }
    }
}