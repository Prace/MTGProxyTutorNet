using MTGProxyTutorNet.Contracts.Models.App;

namespace MTGProxyTutorNet.Contracts.Models.Pokemon
{
    public class PokemonCardPrint : CardPrint
    {
        public string SpecificCardName { get; set; }

        public override string CompleteInfo
        {
            get
            {
                return $"{SpecificCardName} ({SetName}) ";
            }
        }
    }
}
