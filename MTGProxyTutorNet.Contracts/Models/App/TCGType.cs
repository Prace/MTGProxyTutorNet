using System.ComponentModel;

namespace MTGProxyTutorNet.Contracts.Models.App
{
    public enum TCGType
    {
        [Description("Magic the Gathering")]
        MAGIC,
        [Description("Pokémon TCG")]
        POKEMON,
        [Description("One Piece TCG")]
        ONEPIECE
    }
}
