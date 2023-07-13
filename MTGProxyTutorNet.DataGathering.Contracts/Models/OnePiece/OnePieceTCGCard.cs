namespace MTGProxyTutorNet.DataGathering.Contracts.Models.OnePiece
{
    public class OnePieceTCGCard
    {
        public string CardId { get; set; }
        public string CardName { get; set; }
        public bool IsAltArt { get; set; }
        public string CardImageRelativeUrl { get; set; }
    }
}
