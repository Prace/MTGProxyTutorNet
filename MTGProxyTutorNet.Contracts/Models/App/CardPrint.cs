using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutorNet.Contracts.Models.App
{
    public abstract class CardPrint
    {
        public string SetName {  get; set; }
        public virtual string Rarity { get; set; }
        public List<string> ImageUrls { get; set; }
        public abstract string CompleteInfo { get; }
    }
}
