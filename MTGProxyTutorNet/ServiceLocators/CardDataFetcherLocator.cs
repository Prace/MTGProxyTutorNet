using MTGProxyTutorNet.Contracts.Models.App;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MTGProxyTutorNet.DataGathering.Scryfall;
using MTGProxyTutorNet.DataGathering.PokemonTCG;
using MTGProxyTutorNet.DataGathering.Contracts.Interfaces;
using MTGProxyTutorNet.DataGathering.OnePieceTCG;

namespace MTGProxyTutorNet.ServiceLocators
{
    internal static class CardDataFetcherLocator
    {
        private static readonly DependencyInjectionManager _DIManager = new DependencyInjectionManager();
        public static TCGType CurrentGame = TCGType.MAGIC;

        public static ICardDataFetcher Instance
        {
            get
            {
                try
                {
                    ICardDataFetcher currentFetcher;

                    var fetchers = _DIManager.ServiceProvider.GetServices<ICardDataFetcher>();


                    switch (CurrentGame)
                    {
                        case TCGType.POKEMON:
                            currentFetcher = fetchers.Single(f => f.GetType() == typeof(PokemonTCGFetcher));
                            break;

                        case TCGType.ONEPIECE:
                            currentFetcher = fetchers.Single(f => f.GetType() == typeof(OnePieceTCGFetcher));
                            break;

                        case TCGType.MAGIC:
                        default:
                            currentFetcher = fetchers.Single(f => f.GetType() == typeof(ScryfallFetcher));
                            break;
                    }

                    return currentFetcher;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
