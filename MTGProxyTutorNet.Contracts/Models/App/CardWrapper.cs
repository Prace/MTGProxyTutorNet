using System;
using System.Collections.Generic;

namespace MTGProxyTutorNet.Contracts.Models.App
{
	public class CardWrapper
	{
        public CardWrapper()
        {
			this.Id = Guid.NewGuid().ToString();
		}

		public readonly string Id;
		public bool IsSelected { get; set; } = true;
		public Card Card { get; set; }
		public int Quantity { get; set; }
		public List<CardImage> Images { get; set; } = new List<CardImage>();
	}
}
