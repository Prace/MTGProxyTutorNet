using MTGProxyTutorNet.Contracts.Models.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq;
using MTGProxyTutorNet.ViewModels;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;

namespace MTGProxyTutorNet
{
    public partial class CardSelectionGrid : UserControl
    {
        public readonly CardSelectionGridViewModel VM;
        public event Action SelectedCardsChanged;
        private const int _apiCallWaitingTimeMs = 100;

        public CardSelectionGrid()
        {
            VM = ViewModelLocator.GetViewModel<CardSelectionGridViewModel>();
            DataContext = VM;
            InitializeComponent();
        }

        public int TotalCardsToPrint
        {
            get
            {
                var cards = CardSelectionDataGrid.ItemsSource as IEnumerable<CardWrapperViewModel>;
                return cards.Sum(c => c.CardsToPrint);
            }
        }

        public async Task ExportToPDF(string filePath)
        {
            try
            {
                if (CardSelectionDataGrid.ItemsSource is IEnumerable<CardWrapperViewModel> cards)
                {
                    IEnumerable<CardWrapperViewModel> selectedCards = cards.Where(x => x.IsSelected);

                    if (selectedCards.Any())
                    {
                        foreach (CardWrapperViewModel c in selectedCards)
                        {
                            if (c.IsCustom)
                                continue;

                            if (c.Images != null)
                                c.Images.Clear();
                            else
                                c.Images = new List<CardImage>();

                            foreach (string ci in c.Card.SelectedPrint.ImageUrls)
                            {
                                await Task.Delay(_apiCallWaitingTimeMs);
                                CardImage image = await CardDataFetcherLocator.Instance.GetCardImageByUrlAsync(ci);
                                c.Images.Add(image);
                            }
                        }

                        if (selectedCards.Any())
                        {
                            VM.CreatePDF(selectedCards, filePath);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void CardSelectionCheckChanged(object sender, RoutedEventArgs e)
        {
            SelectedCardsChanged?.Invoke();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            var card = (sender as FrameworkElement).DataContext as CardWrapperViewModel;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                VM.RemoveCard(card);
            }
        }
    }
}
