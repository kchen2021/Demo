namespace ConfluentKafa.Interfaces.Errors
{
    public abstract class MessagingError
    {
        protected MessagingError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}

