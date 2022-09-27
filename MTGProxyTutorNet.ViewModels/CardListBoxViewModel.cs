using MTGProxyTutorNet.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutorNet.ViewModels
{
    public class CardListBoxViewModel : BaseViewModel
    {
        private IMultiLineStringParser _multiLineStringParser { get; }

        public CardListBoxViewModel(IMultiLineStringParser multilineStringParser)
        {
            _multiLineStringParser = multilineStringParser;
        }

        public string pastedCardList = "";
        public string PastedCardList
        {
            get { return pastedCardList; }
            set
            {
                pastedCardList = value;
                this.OnPropertyChanged(nameof(pastedCardList));
                this.OnPropertyChanged(nameof(PastedCardListEmpty));
            }
        }

        public bool PastedCardListEmpty
        {
            get { return string.IsNullOrWhiteSpace(pastedCardList); }
        }



        public IEnumerable<ParsedCard> ParseCards(out List<string> failed)
        {
            return _multiLineStringParser.Parse(PastedCardList, out failed);
        }
    }
}
