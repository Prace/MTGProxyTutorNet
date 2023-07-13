namespace MTGProxyTutorNet.ViewModels
{
    public class CustomCardWindowViewModel : BaseViewModel
    {
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                this.OnPropertyChanged(nameof(FilePath));
                this.OnPropertyChanged(nameof(IsAddCardButtonEnabled));
            }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                this.OnPropertyChanged(nameof(Quantity));
            }
        }

        public bool IsAddCardButtonEnabled
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FilePath);
            }
        }
    }
}
