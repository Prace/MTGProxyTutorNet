using MTGProxyTutorNet.Contracts.Models.App;
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
    /// Interaction logic for CardSelectionWindow.xaml
    /// </summary>
    public partial class CardSelectionWindow : Window
    {
        public event EventHandler<List<ParsedCard>> CardsParsedEvent;

        public CardSelectionWindow()
        {
            InitializeComponent();
        }

        private void ParseCards(object sender, RoutedEventArgs e)
        {
            CardsParsedEvent?.Invoke(this, this.CardList.GetParsedCards().ToList());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
