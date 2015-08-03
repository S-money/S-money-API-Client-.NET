namespace Smoney.API.Client
{
    public interface ILogAdapter
    {
        void Warn(string message);
        void Debug(string message);
        void Trace(string message);
    }
}