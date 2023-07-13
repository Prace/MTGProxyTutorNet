using AutoMapper;
using MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.Contracts.Models.OnePiece;
using MTGProxyTutorNet.DataGathering.Contracts.Interfaces;
using MTGProxyTutorNet.DataGathering.Contracts.Models.OnePiece;

namespace MTGProxyTutorNet.DataGathering.OnePieceTCG
{
    public class OnePieceTCGFetcher : ICardDataFetcher
    {
        private const string BASE_URL = "https://www.onepiece-cardgame.it";
        private const string SEARCH_BY_NAME_URL = BASE_URL + "/dbcarte?nomecarta={0}&cerca=true&ordina=cardid&page=";
        private const int CALL_WAIT_TIME_MS = 200;
        private readonly IOnePieceDataConsumer _onePieceDataConsumer;
        private readonly IWebApiConsumer _webApiConsumer;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OnePieceTCGFetcher(IOnePieceDataConsumer onePieceDataConsumer, IWebApiConsumer webApiConsumer, ILogger logger, IMapper mapper)
        {
            _onePieceDataConsumer = onePieceDataConsumer;
            _webApiConsumer = webApiConsumer;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Card> GetCardByNameAsync(string cardName)
        {
            OnePieceTCGSearchResult cardsDetails = await getOnePieceTCGCardByNameAsync(cardName);

            if (cardsDetails != null)
            {
                return mapResultDataToCard(cardsDetails);
            }

            return null;
        }

        private Card mapResultDataToCard(OnePieceTCGSearchResult cardsDetails)
        {
            var card = new OnePieceCard();
            card.CardName = cardsDetails.Data.First().CardName;
            card.Printings = cardsDetails.Data.Select(c =>
            {
                var printing = new OnePieceCardPrint();
                printing.SetName = c.CardId;
                printing.CardName = c.CardName;
                printing.IsAlternateArt = c.IsAltArt;
                printing.Rarity = "?";
                printing.ImageUrls = new List<string> { c.CardImageRelativeUrl };
                return printing as CardPrint;
            }).ToList();

            return card;
        }

        private async Task<OnePieceTCGSearchResult> getOnePieceTCGCardByNameAsync(string cardName)
        {
            var completeUrl = string.Format(SEARCH_BY_NAME_URL, cardName);
            var result = await _onePieceDataConsumer.GetCardAsync(completeUrl);
            return result;
        }

        public async Task<CardImage> GetCardImageByUrlAsync(string url)
        {
            if (url == null)
                return null;

            var binary = await _webApiConsumer.GetBinaryAsync(BASE_URL + url, CALL_WAIT_TIME_MS);
            if (binary != null)
                return new CardImage(binary);
            return null;
        }
    }
}
