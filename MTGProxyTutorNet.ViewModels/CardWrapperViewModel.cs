using MTGProxyTutorNet.Contracts.Models.App;
using System;
using System.Collections.Generic;

namespace MTGProxyTutorNet.ViewModels
{
	public class CardWrapperViewModel : BaseViewModel
    {
        public CardWrapperViewModel(Card card, int quantity)
        {
            Card = card;
            Quantity = quantity;
            this.Card.OnSelectedPrintChanged += selectedPrintChangedHandler;
        }

        private bool _isSelected = true;
		public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                this.OnPropertyChanged(nameof(IsSelected));
            }
        }

        private Card _card;
        public Card Card
        {
            get
            {
                return _card;
            }
            set
            {
                _card = value;
                this.OnPropertyChanged(nameof(Card));
            }
        }

        private int _quantity;
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

        private List<CardImage> _images;
        public List<CardImage> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                this.OnPropertyChanged(nameof(Images));
            }
        }

        private bool _isCustom = false;
        public bool IsCustom
        {
            get
            {
                return _isCustom;
            }
            set
            {
                _isCustom = value;
                this.OnPropertyChanged(nameof(IsCustom));
            }
        }

        public int NumCardImages
        {
            get 
            {
                return IsCustom ? 1 : this.Card.SelectedPrint.ImageUrls.Count;
            }
        }

        private void selectedPrintChangedHandler()
        {
            this.OnPropertyChanged(nameof(Card));
            this.OnPropertyChanged(nameof(NumCardImages));
        }
    }
}
