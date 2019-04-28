namespace ChildVac.WebApi.Application.Models
{
    /// <summary>
    ///     Represents base response model with generic result
    /// </summary>
    public class ResponseBaseModel<T>
    {
        /// <summary>
        ///     Reponse result
        /// </summary>
        public T Result { get; set; }
    }
}