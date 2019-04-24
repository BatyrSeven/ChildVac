using System.Collections.Generic;

namespace ChildVac.WebApi.Application.Models
{
    public class MessageResponseModel : ResponseBaseModel<bool>
    {
        public List<MessageModel> Messages { get; set; }

        public MessageResponseModel(bool result, params MessageModel[] messages)
        {
            Result = result;
            Messages = new List<MessageModel>();
            Messages.AddRange(messages);
        }
    }
}
