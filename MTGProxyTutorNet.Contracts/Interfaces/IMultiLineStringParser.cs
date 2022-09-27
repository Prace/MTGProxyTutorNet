using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutorNet.Contracts.Interfaces
{
    public interface IMultiLineStringParser
    {
        IEnumerable<ParsedCard> Parse(string input, out List<string> failedParse);
    }
}