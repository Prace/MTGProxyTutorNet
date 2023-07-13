using MTGProxyTutorNet.Contracts.Models.App;
using System;
using System.Threading.Tasks;

namespace MTGProxyTutorNet.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
        }

        private TCGType selectedTCGType = TCGType.MAGIC;
        public TCGType SelectedTCGType
        {
            get {  return selectedTCGType; }
            set 
            {  
                selectedTCGType = value;
                OnPropertyChanged(nameof(SelectedTCGType));
            }
        }

        private int totalCardsToPrint;
        public int TotalCardsToPrint
        {
            get {  return totalCardsToPrint; }
            set
            {
                totalCardsToPrint = value;
                TotalSheetsToPrint = calcSheetsToPrint(value);
                OnPropertyChanged(nameof(TotalCardsToPrint));
            }
        }

        private int totalSheetsToPrint;
        public int TotalSheetsToPrint
        {
            get { return totalSheetsToPrint; }
            set
            {
                totalSheetsToPrint = value;
                OnPropertyChanged(nameof(TotalSheetsToPrint));
            }
        }

        private bool parseCardsBtnEnabled = true;
        public bool ParseCardsBtnEnabled
        {
            get {  return parseCardsBtnEnabled; }
            set
            {
                parseCardsBtnEnabled = value;
                OnPropertyChanged(nameof(ParseCardsBtnEnabled));
            }
        }

        private bool exportBtnEnabled = false;
        public bool ExportBtnEnabled
        {
            get { return exportBtnEnabled; }
            set
            {
                exportBtnEnabled = value;
                OnPropertyChanged(nameof(ExportBtnEnabled));
            }
        }

        private bool clearListBtnEnabled = false;
        public bool ClearListBtnEnabled
        {
            get { return clearListBtnEnabled; }
            set
            {
                clearListBtnEnabled = value;
                OnPropertyChanged(nameof(ClearListBtnEnabled));
            }
        }

        private bool addSingleCardBtnEnabled = true;
        public bool AddSingleCardBtnEnabled
        {
            get { return addSingleCardBtnEnabled && !string.IsNullOrWhiteSpace(SingleCardToAdd); }
            set
            {
                addSingleCardBtnEnabled = value;
                OnPropertyChanged(nameof(AddSingleCardBtnEnabled));
            }
        }

        private string singleCardToAdd;
        public string SingleCardToAdd
        {
            get { return singleCardToAdd; }
            set
            {
                singleCardToAdd = value;
                OnPropertyChanged(nameof(SingleCardToAdd));
                OnPropertyChanged(nameof(AddSingleCardBtnEnabled));
            }
        }


        private int calcSheetsToPrint(int numberOfCardFaces)
        {
            return (int)Math.Ceiling(numberOfCardFaces / 9.0);
        }
    }
}
