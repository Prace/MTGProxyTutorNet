namespace MTGProxyTutorNet.Contracts.Models.App
{
    public class ParsedCard
	{
		public ParsedCard(int quantity, string cardName)
		{
			Quantity = quantity;
			CardName = cardName;
		}

		public int Quantity { get; set; }
		public string CardName { get; set; }
	}
}
