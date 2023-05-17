using Microsoft.Win32;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.Contracts.Models.Custom;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CustomCardWindow.xaml
    /// </summary>
    public partial class CustomCardWindow : Window
    {
        private CustomCard _card;

        public event EventHandler<CustomCard> CustomCardLoaded;

        public CustomCardWindow()
        {
            InitializeComponent();
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FileNameTextBox.Text = openFileDialog.FileName;
                var cardImage = new CardImage(File.ReadAllBytes(openFileDialog.FileName));
                _card = new CustomCard(System.IO.Path.GetFileName(openFileDialog.FileName), cardImage);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            CustomCardLoaded?.Invoke(this, _card);
        }
    }
}
