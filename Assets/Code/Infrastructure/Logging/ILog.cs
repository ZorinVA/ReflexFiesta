namespace Code.Infrastructure.Logging
{
    public interface ILog
    {
        void Write(string message);
        void WriteWarning(string message);
        void WriteError(string message);
    }
}