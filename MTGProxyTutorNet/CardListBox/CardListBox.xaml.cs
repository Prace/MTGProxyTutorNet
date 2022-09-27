using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MTGProxyTutorNet.ViewModels;

namespace MTGProxyTutorNet
{
    public partial class CardListBox : UserControl
    {
        public readonly CardListBoxViewModel VM;

        public CardListBox()
        {
            VM = ViewModelLocator.GetViewModel<CardListBoxViewModel>();
            DataContext = VM;
            InitializeComponent();
        }

        public IEnumerable<ParsedCard> GetParsedCards()
        {
            var parsedCards = VM.ParseCards(out List<string> failed);
            if (failed.Any())
            {
                var failedParseMessage = $"Could not parse the following card(s):\n\n{string.Join("\n", failed)}";
                MessageBox.Show(failedParseMessage, "Failed Cards", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return parsedCards;
        }
    }
}
