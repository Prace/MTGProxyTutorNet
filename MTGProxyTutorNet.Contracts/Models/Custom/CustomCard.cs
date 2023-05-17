using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.Contracts.Models.Custom
{
    public class CustomCard : Card
    {
        public CustomCard(string fileName, CardImage cardImage)
        {
            CardName = fileName;
            CardImage = cardImage;
            Printings = new List<CardPrint>
            {
                new CustomCardPrint
                {
                    ImageUrls = new List<string>(),
                    SetName = "Custom",
                    Rarity = "Custom"
                }
            };
        }

        public CardImage CardImage { get; }
        public int Quantity { get; set; } = 1;
    }
}
