using MTGProxyTutorNet.Contracts.Models.Magic;
using MTGProxyTutorNet.Contracts.Models.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MTGProxyTutorNet
{
    public class CardNameGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MagicCard)
                return (value as MagicCard).CardName;
            else if (value is PokemonCard)
            {
                var printing = (value as PokemonCard).SelectedPrint as PokemonCardPrint;
                return printing.SpecificCardName;
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
