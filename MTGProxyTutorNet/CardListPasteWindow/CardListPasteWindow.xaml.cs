using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.DependencyInjection;
using MTGProxyTutorNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MTGProxyTutorNet
{
    /// <summary>
    /// Interaction logic for CardListPasteWindow.xaml
    /// </summary>
    public partial class CardListPasteWindow : Window
    {
        public event EventHandler<List<ParsedCard>> CardsParsedEvent;
        public readonly CardListPasteWindowViewModel VM;

        public CardListPasteWindow()
        {
            VM = ViewModelLocator.GetViewModel<CardListPasteWindowViewModel>();
            DataContext = VM;
            InitializeComponent();
            CardsTextBox.Focus();
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

        private void ParseCards(object sender, RoutedEventArgs e)
        {
            CardsParsedEvent?.Invoke(this, GetParsedCards().ToList());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
