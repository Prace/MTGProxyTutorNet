using System;
using System.Collections.Generic;
using System.Linq;

namespace MTGProxyTutorNet.Contracts.Models.App
{
	public abstract class Card
	{
		public event Action OnSelectedPrintChanged;

		public string CardName { get; set; }
		public virtual List<CardPrint> Printings { get; set; }

		private CardPrint selectedPrint;
		public CardPrint SelectedPrint
		{
			get
			{
				return selectedPrint ?? Printings.FirstOrDefault();
			}
			set
			{
				selectedPrint = value;
				OnSelectedPrintChanged?.Invoke();
			}
		}
	}
}
