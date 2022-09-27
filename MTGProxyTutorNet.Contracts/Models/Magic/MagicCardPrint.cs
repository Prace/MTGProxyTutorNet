using MTGProxyTutorNet.Contracts.Extensions;
using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;
using System.Linq;

namespace MTGProxyTutorNet.Contracts.Models.Magic
{
    public class MagicCardPrint : CardPrint
    {
        public bool FullArt { get; set; }
        public bool Variation { get; set; }
        public bool Promo { get; set; }
        public bool Oversized { get; set; }
        public bool Textless { get; set; }

        private string rarity;
        public override string Rarity
        {
            get { return rarity; }
            set { rarity = value?.CapitalizeFirstLetter(); }
        }

        public override string CompleteInfo
        {
            get
            {
                var conditions = new List<CardAdditionalInfo> {
                    new CardAdditionalInfo(Variation, nameof(Variation)),
                    new CardAdditionalInfo(FullArt, nameof(FullArt)),
                    new CardAdditionalInfo(Promo, nameof(Promo)),
                    new CardAdditionalInfo(Oversized, nameof(Oversized)),
                    new CardAdditionalInfo(Textless, nameof(Textless))
                };
                var trueConditions = conditions.Where(c => c.Value).Select(c => c.Description);
                if (trueConditions.Any())
                    return $"{SetName} ({string.Join(",", trueConditions)})";
                return SetName;
            }
        }
    }

    public class CardAdditionalInfo
    {
        public CardAdditionalInfo(bool value, string description)
        {
            this.Value = value;
            this.Description = description;
        }

        public bool Value { get; private set; }
        public string Description { get; private set; }
    }
}
