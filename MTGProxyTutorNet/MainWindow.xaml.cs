using Microsoft.Win32;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.Contracts.Models.Custom;
using MTGProxyTutorNet.DependencyInjection;
using MTGProxyTutorNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTGProxyTutorNet
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;
        private List<ParsedCard> _parsedCards;
        private CardListPasteWindow _cardListPasteWindow;
        private CustomCardWindow _customCardWindow;

        public MainWindow()
        {
            _vm = ViewModelLocator.GetViewModel<MainWindowViewModel>();
            DataContext = _vm;
            InitializeComponent();
            SubscribeToChildrenEvents();
        }

        public void ParseCards_Click(object sender, RoutedEventArgs e)
        {
            _cardListPasteWindow = new CardListPasteWindow();
            _cardListPasteWindow.CardsParsedEvent += CardsParsedEventHandler;
            _cardListPasteWindow.ShowDialog();
        }

        private void CardsParsedEventHandler(object sender, List<ParsedCard> e)
        {
            _parsedCards = e;
            var _ = FillCardGrid();
            _cardListPasteWindow.Close();
            _cardListPasteWindow.CardsParsedEvent -= CardsParsedEventHandler;
        }

        private async Task FillCardGrid()
        {
            _vm.ParseCardsBtnEnabled = false;
            _vm.AddSingleCardBtnEnabled = false;

            // Clear card list 
            CardSelection.VM.FlushCards();

            var failedFetch = new List<ParsedCard>();

            foreach (var pc in _parsedCards)
            {
                try
                {
                    var cardWrapper = await GetCard(pc);
                    AddOrUpdateCard(cardWrapper);
                }
                catch
                {
                    failedFetch.Add(pc);
                }
            }

            NotifyFailedFetchedCards(failedFetch);

            _vm.ParseCardsBtnEnabled = true;
            _vm.AddSingleCardBtnEnabled = true;
        }

        private async void ExportToPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                _vm.ExportBtnEnabled = false;

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (.pdf)|*.pdf"
                };
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                    await CardSelection.ExportToPDF(saveFileDialog.FileName);

            }
            catch (Exception ex)
            {
                var message = $"An error has occurred exporting PDF file: {ex.Message}";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _vm.ExportBtnEnabled = true;
            }
        }

        private void NotifyFailedFetchedCards(List<ParsedCard> failedFetch)
        {
            if (failedFetch.Any())
            {
                var failedParseMessage = $"Could not fetch the following card(s):\n\n{string.Join("\n", failedFetch.Select(f => f.CardName))}";
                MessageBox.Show(failedParseMessage, "Failed Cards", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SubscribeToChildrenEvents()
        {
            CardSelection.SelectedCardsChanged += ToggleExportBtn;
            CardSelection.VM.Cards.CollectionChanged += UpdateCardListInfo;
            TCGSelection.SelectionChanged += UpdateCardFecthingStrategy;

        }

        private void UpdateCardListInfo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CardSelection.VM.Cards != null)
            {
                ToggleClearListBtn();
                ToggleExportBtn();
                _vm.TotalCardsToPrint = CardSelection.VM.Cards.Where(c => c.IsSelected).Sum(c => c.CardsToPrint);
            }
        }

        private void UpdateCardFecthingStrategy(object sender, SelectionChangedEventArgs e)
        {
            CardDataFetcherLocator.CurrentGame = _vm.SelectedTCGType;
            ClearData();
        }

        private void AddOrUpdateCard(CardWrapperViewModel cardWrapper)
        {
            //var match = CardSelection.VM.Cards.FirstOrDefault(c => cardWrapper.Card.CardName == c.Card.CardName);

            //if (match != null)
            //{
            //    match.Quantity = cardWrapper.Quantity;
            //}
            //else
            //{
                CardSelection.VM.Cards.Add(cardWrapper);
            //}
        }

        private async Task<Card> GetCardByNameAsync(string cardName)
        {
            return await CardDataFetcherLocator.Instance.GetCardByNameAsync(cardName);
        }

        private async Task<CardWrapperViewModel> GetCard(ParsedCard parsedCard)
        {
            var cardData = await GetCardByNameAsync(parsedCard.CardName);
            var cardWrapper = new CardWrapperViewModel(cardData, parsedCard.Quantity);
            return cardWrapper;
        }

        private void ToggleExportBtn()
        {
            _vm.ExportBtnEnabled = CardSelection.VM.Cards.Any(c => c.IsSelected);
        }

        private void ToggleClearListBtn()
        {
            _vm.ClearListBtnEnabled = CardSelection.VM.Cards.Any();
        }

        private void AddCustomCard_Click(object sender, RoutedEventArgs e)
        {
            _customCardWindow = new CustomCardWindow();
            _customCardWindow.CustomCardLoaded += CustomCardLoadedEventHandler;
            _customCardWindow.ShowDialog();
        }

        private void CustomCardLoadedEventHandler(object sender, CustomCard e)
        {
            var cardWrapper = new CardWrapperViewModel(e, e.Quantity);
            cardWrapper.Images = new List<CardImage> { e.CardImage };
            cardWrapper.IsCustom = true;
            AddOrUpdateCard(cardWrapper);
            _customCardWindow.Close();
            _customCardWindow.CustomCardLoaded -= CustomCardLoadedEventHandler;
        }

        private void AddSingleCard_Click(object sender, RoutedEventArgs e)
        {
            var _ = AddSingleCard();
        }

        private async Task AddSingleCard()
        {
            var pc = new ParsedCard(1, _vm.SingleCardToAdd);
            try
            {
                var cardWrapper = await GetCard(pc);
                AddOrUpdateCard(cardWrapper);
            }
            catch
            {
                NotifyFailedFetchedCards(new List<ParsedCard> { pc });
            }
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Clear Cards Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            CardSelection.VM.FlushCards();
            ToggleExportBtn();
            ToggleClearListBtn();
        }

        private void SingleCard_OnEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var _ = AddSingleCard();
            }
        }
    }
}
