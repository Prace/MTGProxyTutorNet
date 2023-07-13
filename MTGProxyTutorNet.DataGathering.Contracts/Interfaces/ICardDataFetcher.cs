using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.DataGathering.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		Task<Card> GetCardByNameAsync(string cardName);
		Task<CardImage> GetCardImageByUrlAsync(string url);
	}
}
