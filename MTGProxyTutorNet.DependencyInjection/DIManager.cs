﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MTGProxyTutorNet.BusinessLogic.Http;
using MTGProxyTutorNet.BusinessLogic.Loggers;
using MTGProxyTutorNet.BusinessLogic.Parsers;
using MTGProxyTutorNet.BusinessLogic.PDF;
using MTGProxyTutorNet.Contracts.Interfaces;
using MTGProxyTutorNet.DataGathering.OnePIeceTCG.Logic;
using MTGProxyTutorNet.DataGathering.PokemonTCG.Logic;
using MTGProxyTutorNet.DataGathering.Scryfall.Logic;
using MTGProxyTutorNet.DependencyInjection.MapperProfiles;
using MTGProxyTutorNet.ViewModels;
using System;
using System.Net.Http;

namespace MTGProxyTutorNet.DependencyInjection
{
    public class DIManager
    {
        public readonly IServiceProvider ServiceProvider;

        public DIManager()
        {
            var serviceCollection = InitializeContainer();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private IServiceCollection InitializeContainer()
        {
            var serviceCollection = new ServiceCollection();

            #region Common

            serviceCollection.AddSingleton<HttpClient>();
            serviceCollection.AddSingleton<ILogger, SimpleLogger>();
            serviceCollection.AddSingleton<IPDFManager, PDFManager>();
            serviceCollection.AddScoped<IMultiLineStringParser, MultiLineStringParser>();
            serviceCollection.AddScoped<IWebApiConsumer, WebApiConsumer>();
            serviceCollection.AddScoped<OnePieceDataConsumer>();
            serviceCollection.AddSingleton<ICardDataFetcher, ScryfallFetcher>();
            serviceCollection.AddSingleton<ICardDataFetcher, PokemonTCGFetcher>();
            serviceCollection.AddSingleton<ICardDataFetcher, OnePieceTCGFetcher>();


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ScryfallMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);

            #endregion

            #region ViewModels

            serviceCollection.AddScoped<MainWindowViewModel>();
            serviceCollection.AddScoped<CardListPasteWindowViewModel>();
            serviceCollection.AddScoped<CardSelectionGridViewModel>();
            serviceCollection.AddScoped<CustomCardWindowViewModel>();

            #endregion

            return serviceCollection;

        }
    }
}
