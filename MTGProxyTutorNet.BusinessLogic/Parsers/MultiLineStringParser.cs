using MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.BusinessLogic.Parsers
{
    public class MultiLineStringParser : BaseParser, IMultiLineStringParser
    {
        List<string> _failedParse;

        public MultiLineStringParser()
            : base()
        { }

        public IEnumerable<ParsedCard> Parse(string input, out List<string> failedParse)
        {
            _failedParse = new List<string>();
            var result = splitLinesAndParse(input);
            failedParse = _failedParse;
            return result;
        }

        private IEnumerable<ParsedCard> splitLinesAndParse(string input)
        {
            using (StringReader sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parsed = ParseSingleLine(line);
                    if (parsed is null)
                        _failedParse.Add(line);
                    else
                        yield return parsed;
                }
            }
        }
    }
}
