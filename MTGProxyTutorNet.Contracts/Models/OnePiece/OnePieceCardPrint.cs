using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.Contracts.Models.OnePiece
{
    public class OnePieceCardPrint : CardPrint
    {
        public string CardName { get; set; }
        public bool IsAlternateArt { get; set; }

        public override string CompleteInfo
        {
            get
            {
                var info = $"{CardName} ({SetName}) ";
                if (IsAlternateArt)
                    return info + "Alt.";
                return info;
            }
        }
    }
}
