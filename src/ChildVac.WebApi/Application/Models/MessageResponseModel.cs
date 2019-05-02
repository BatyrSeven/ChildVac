using System.Collections.Generic;

namespace ChildVac.WebApi.Application.Models
{
    /// <summary>
    ///     Represents response model with the success or fail messages
    /// </summary>
    public class MessageResponseModel : ResponseBaseModel<bool>
    {
        /// <summary>
        ///     Paramterized constructor
        /// </summary>
        /// <param name="result">true - success, false - fail</param>
        /// <param name="messages">List of messages</param>
        public MessageResponseModel(bool result, params MessageModel[] messages)
        {
            Result = result;
            Messages = new List<MessageModel>();
            Messages.AddRange(messages);
        }

        /// <summary>
        ///     List of messages
        /// </summary>
        public List<MessageModel> Messages { get; set; }
    }
}