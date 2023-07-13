namespace MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces
{
    public interface ILogger
	{
        void Debug(string text);
        void Error(string text);
        void Info(string text);
        void Warning(string text);
    }
}
