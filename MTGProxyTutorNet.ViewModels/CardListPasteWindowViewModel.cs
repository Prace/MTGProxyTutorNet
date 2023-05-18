using MTGProxyTutorNet.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutorNet.ViewModels
{
    public class CardListPasteWindowViewModel : BaseViewModel
    {
        private IMultiLineStringParser _multiLineStringParser { get; }

        public CardListPasteWindowViewModel(IMultiLineStringParser multilineStringParser)
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
                this.OnPropertyChanged(nameof(IsParseButtonEnabled));
            }
        }

        public bool IsParseButtonEnabled
        {
            get
            {
                return !string.IsNullOrWhiteSpace(pastedCardList);
            } 
        }

        public IEnumerable<ParsedCard> ParseCards(out List<string> failed)
        {
            return _multiLineStringParser.Parse(PastedCardList, out failed);
        }
    }
}
