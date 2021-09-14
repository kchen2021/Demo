namespace ConfluentKafa.Interfaces.Errors
{
    public class MessagingGeneralError : MessagingError
    {
        public MessagingGeneralError(string message) : base(message)
        {
        }
    }
}