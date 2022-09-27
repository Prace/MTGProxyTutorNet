using System.Collections.Generic;

namespace MTGProxyTutorNet.DataGathering.Scryfall.Models
{
    public class ImageUris
    {
        public string Small { get; set; }
        public string Normal { get; set; }
        public string Large { get; set; }
        public string Png { get; set; }
        public string Art_crop { get; set; }
        public string Border_crop { get; set; }
    }

    public class Legalities
    {
        public string Standard { get; set; }
        public string Future { get; set; }
        public string Historic { get; set; }
        public string Pioneer { get; set; }
        public string Modern { get; set; }
        public string Legacy { get; set; }
        public string Pauper { get; set; }
        public string Vintage { get; set; }
        public string Penny { get; set; }
        public string Commander { get; set; }
        public string Brawl { get; set; }
        public string Duel { get; set; }
        public string Oldschool { get; set; }
    }

    public class Preview
    {
        public string Source { get; set; }
        public string Source_uri { get; set; }
        public string Previewed_at { get; set; }
    }

    public class Prices
    {
        public string Usd { get; set; }
        public string Usd_foil { get; set; }
        public string Eur { get; set; }
        public string Eur_foil { get; set; }
        public string Tix { get; set; }
    }

    public abstract class BaseCard
    {
        public string Name { get; set; }
        public string Mana_cost { get; set; }
        public string Type_line { get; set; }
        public string Oracle_text { get; set; }
        public List<string> Colors { get; set; }
        public string Artist { get; set; }
        public string Illustration_id { get; set; }
        public ImageUris Image_uris { get; set; }
    }

    public class CardFace : BaseCard
    {
        public string Object { get; set; }
        public string Artist_id { get; set; }
    }

    public class ScryfallCard : BaseCard
    {
        public string Id { get; set; }
        public string Oracle_id { get; set; }
        public List<string> Multiverse_ids { get; set; }
        public int Tcgplayer_id { get; set; }
        public int Cardmarket_id { get; set; }
        public string Lang { get; set; }
        public string Released_at { get; set; }
        public string Uri { get; set; }
        public string Scryfall_uri { get; set; }
        public string Layout { get; set; }
        public bool Highres_image { get; set; }
        public List<CardFace> Card_faces { get; set; }
        public double Cmc { get; set; }
        public List<string> Color_identity { get; set; }
        public List<string> Keywords { get; set; }
        public Legalities Legalities { get; set; }
        public List<string> Games { get; set; }
        public bool Reserved { get; set; }
        public bool Foil { get; set; }
        public bool Nonfoil { get; set; }
        public bool Oversized { get; set; }
        public bool Promo { get; set; }
        public bool Reprint { get; set; }
        public bool Variation { get; set; }
        public string Set { get; set; }
        public string Set_name { get; set; }
        public string Set_type { get; set; }
        public string Set_search_uri { get; set; }
        public string Scryfall_set_uri { get; set; }
        public string Rulings_uri { get; set; }
        public string Prints_search_uri { get; set; }
        public string Collector_number { get; set; }
        public bool Digital { get; set; }
        public string Rarity { get; set; }
        public string Card_back_id { get; set; }
        public List<string> Artist_ids { get; set; }
        public string Border_color { get; set; }
        public string Frame { get; set; }
        public bool Full_art { get; set; }
        public bool Textless { get; set; }
        public bool Booster { get; set; }
        public bool Story_spotlight { get; set; }
        public int Edhrec_rank { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public Prices Prices { get; set; }
    }

    public class ScryfallCardPrintings
    {
        public string Object { get; set; }
        public int Total_cards { get; set; }
        public bool Has_more { get; set; }
        public List<ScryfallCard> Data { get; set; } = new List<ScryfallCard>();
    }
}
