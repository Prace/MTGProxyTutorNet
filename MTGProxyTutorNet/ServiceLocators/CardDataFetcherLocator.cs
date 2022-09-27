using MTGProxyTutorNet.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.DependencyInjection;
using MTGProxyTutorNet.DataGathering.PokemonTCG.Logic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MTGProxyTutorNet.DataGathering.Scryfall.Logic;

namespace MTGProxyTutorNet
{
    internal static class CardDataFetcherLocator
    {
        private static readonly DIManager _DIManager = new DIManager();
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
