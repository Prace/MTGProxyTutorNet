using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.Contracts.Models.Magic
{
    public class MagicCard : Card
    {
        public string ManaCost { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
    }
}
