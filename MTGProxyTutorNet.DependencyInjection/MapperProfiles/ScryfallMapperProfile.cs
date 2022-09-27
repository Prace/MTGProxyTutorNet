using AutoMapper;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.Contracts.Models.Magic;
using MTGProxyTutorNet.DataGathering.Scryfall.Models;
using MTGProxyTutorNet.ViewModels;

namespace MTGProxyTutorNet.DependencyInjection.MapperProfiles
{
    public class ScryfallMapperProfile : Profile
    {
        public ScryfallMapperProfile()
        {
            CreateMap<ScryfallCard, MagicCard>()
                .ForMember(dest => dest.CardName, src => src.MapFrom(s => s.Name))
                .ForMember(dest => dest.ManaCost, src => src.MapFrom(s => s.Mana_cost))
                .ForMember(dest => dest.Type, src => src.MapFrom(s => s.Type_line))
                .ForMember(dest => dest.Text, src => src.MapFrom(s => s.Oracle_text));

            CreateMap<ScryfallCard, MagicCardPrint>()
                .ForMember(dest => dest.SetName, src => src.MapFrom(s => s.Set_name))
                .ForMember(dest => dest.FullArt, src => src.MapFrom(s => s.Full_art))
                .ForMember(dest => dest.ImageUrls, src => src.MapFrom(s => convertScryfallImages(s)));


            CreateMap<CardWrapper, CardWrapperViewModel>();
            CreateMap<CardWrapperViewModel, CardWrapper>();
        }

        private List<string> convertScryfallImages(ScryfallCard card)
        {
            if (card.Image_uris == null)
            {
                return card.Card_faces == null ? new List<string> { }
                    : card.Card_faces.Select(cf => cf.Image_uris.Normal).ToList();
            }
            return new List<string> { card.Image_uris.Normal };
        }
    }
}
