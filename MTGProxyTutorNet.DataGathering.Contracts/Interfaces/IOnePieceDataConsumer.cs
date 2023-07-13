using MTGProxyTutorNet.DataGathering.Contracts.Models.OnePiece;

namespace MTGProxyTutorNet.DataGathering.Contracts.Interfaces
{
    public interface IOnePieceDataConsumer
    {
        Task<OnePieceTCGSearchResult> GetCardAsync(string url, int msDelay = 0, bool useCache = true);
    }
}