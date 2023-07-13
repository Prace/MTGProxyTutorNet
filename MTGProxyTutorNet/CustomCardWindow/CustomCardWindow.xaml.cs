using Microsoft.Win32;
using MTGProxyTutorNet.Contracts.Models.App;
using MTGProxyTutorNet.Contracts.Models.Custom;
using MTGProxyTutorNet.ServiceLocators;
using MTGProxyTutorNet.ViewModels;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MTGProxyTutorNet
{
    /// <summary>
    /// Interaction logic for CustomCardWindow.xaml
    /// </summary>
    public partial class CustomCardWindow : Window
    {
        private CustomCard _card;
        public CustomCardWindowViewModel VM { get; }

        public event EventHandler<CustomCard> CustomCardLoaded;

        public CustomCardWindow()
        {
            VM = ViewModelLocator.GetViewModel<CustomCardWindowViewModel>();
            DataContext = VM;
            InitializeComponent();
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg,*jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                VM.FilePath = openFileDialog.FileName;
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
            _card.Quantity = VM.Quantity;
            CustomCardLoaded?.Invoke(this, _card);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
