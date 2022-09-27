using MTGProxyTutorNet.Contracts.Interfaces;
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

        private int calcSheetsToPrint(int numberOfCardFaces)
        {
            return (int)Math.Ceiling(numberOfCardFaces / 9.0);
        }
    }
}
