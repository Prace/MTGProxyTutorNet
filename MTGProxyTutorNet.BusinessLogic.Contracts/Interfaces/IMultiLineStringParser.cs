using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces
{
    public interface IMultiLineStringParser
    {
        IEnumerable<ParsedCard> Parse(string input, out List<string> failedParse);
    }
}