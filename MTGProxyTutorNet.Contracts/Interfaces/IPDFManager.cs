using MTGProxyTutorNet.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutorNet.Contracts.Interfaces
{
    public interface IPDFManager
    {
        void CreatePDF(IEnumerable<CardWrapper> cardWrappers, string filename);
    }
}