namespace SyncPointBack.Helper.ErrorHandler.CustomException
{
    public class RegistrationException : Exception
    {
        public List<string> Errors { get; }

        public RegistrationException(List<string> errors)
        {
            Errors = errors ?? new List<string>();
        }

        public override string Message => string.Join(Environment.NewLine, Errors);
    }

    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}