using MTGProxyTutorNet.Contracts.Models.App;
using System;
using System.Text.RegularExpressions;

namespace MTGProxyTutorNet.BusinessLogic.Parsers
{
    public abstract class BaseParser
	{
		protected Regex lineWithQtyParseRegex = new Regex(@"\s*(\d+)\s*[xX]?\s*(.+)");

		protected ParsedCard ParseSingleLine(string line)
		{
			var lineWithQtyMatch = lineWithQtyParseRegex.Match(line);

			if (lineWithQtyMatch.Success)
				return new ParsedCard(Int32.Parse(lineWithQtyMatch.Groups[1].Value), lineWithQtyMatch.Groups[2].Value);
			else if (!string.IsNullOrWhiteSpace(line))
				return new ParsedCard(1, line.Trim());
			else
				return null;
		}
	}
}
