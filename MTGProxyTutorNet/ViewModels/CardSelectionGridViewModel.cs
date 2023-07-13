using AutoMapper;
using MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Extensions;
using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;
using System.Linq;

namespace MTGProxyTutorNet.ViewModels
{
    public class CardSelectionGridViewModel : BaseViewModel
    {
        private readonly IPDFManager _pdfManager;
        private readonly IMapper _mapper;

        public CardSelectionGridViewModel(IPDFManager pdfManager, IMapper mapper)
        {
            _pdfManager = pdfManager;
            _mapper = mapper;
        }

        public void FlushCards()
        {
            this.Cards.Clear();
        }

        private RealObservableCollection<CardWrapperViewModel> cards = new RealObservableCollection<CardWrapperViewModel>();
        public RealObservableCollection<CardWrapperViewModel> Cards
        {
            get
            {
                return this.cards;
            }
            set
            {
                this.cards = value;
                this.OnPropertyChanged(nameof(Cards));
            }
        }

        public void CreatePDF(IEnumerable<CardWrapperViewModel> cards, string filePath)
        {
            List<CardWrapper> selectedCards = cards.Select(c => _mapper.Map<CardWrapper>(c)).ToList();
            _pdfManager.CreatePDF(selectedCards, filePath);
        }

        public void RemoveCard(string cardName)
        {
            var toRemove = this.Cards.Where(c => c.Card.CardName == cardName);
            foreach (var c in toRemove)
            {
                this.Cards.Remove(c);
            }
        }

        public void RemoveCard(CardWrapperViewModel card)
        {
            this.Cards.Remove(card);
        }
    }
}
