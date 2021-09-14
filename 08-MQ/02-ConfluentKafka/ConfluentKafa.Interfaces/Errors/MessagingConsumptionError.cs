namespace ConfluentKafa.Interfaces.Errors
{
    public class MessagingConsumptionError : MessagingError
    {
        public MessagingConsumptionError(string message) : base(message)
        {
        }
    }
}
