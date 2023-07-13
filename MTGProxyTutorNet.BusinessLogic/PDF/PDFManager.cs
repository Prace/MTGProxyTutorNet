using MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Models.App;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace MTGProxyTutorNet.BusinessLogic.PDF
{
    public class PDFManager : IPDFManager
    {
        const double resizer = 0.8;
        const int PDFCardWidth = (int)(223 * resizer);
        const int PDFCardHeight = (int)(310 * resizer);
        const int marginTop = 60;
        const int marginBottom = 10;
        const int marginLeft = 60;
        const int marginRight = 10;

        public void CreatePDF(IEnumerable<CardWrapper> cardWrappers, string filename)
        {
            var doc = new PdfDocument();
            addPageToPDF(doc);
            var currCoordinate = new PDFCoordinate();

            foreach (var cardWrapper in cardWrappers)
            {
                for (int i = 0; i < cardWrapper.Quantity; i++)
                {
                    foreach (var image in cardWrapper.Images)
                    {
                        using (var xgr = XGraphics.FromPdfPage(doc.Pages[currCoordinate.PageNumber]))
                        {
                            var rect = new XRect(currCoordinate.ColNumber * PDFCardWidth, currCoordinate.RowNumber * PDFCardHeight, PDFCardWidth, PDFCardHeight);
                            var imageToPDF = XImage.FromStream(image.GetStream);
                            xgr.DrawImage(imageToPDF, rect);

                            var nextCoordinate = currCoordinate.NextCoordinate();

                            if (currCoordinate.PageNumber != nextCoordinate.PageNumber)
                            {
                                // skip if last card to print
                                bool isLastCard = cardWrapper.Id == cardWrappers.Last().Id;
                                bool isLastImageForCard = image == cardWrapper.Images.Last();
                                bool isLastRepetitionForCard = i == cardWrapper.Quantity - 1;

                                if (isLastCard && isLastImageForCard && isLastRepetitionForCard)
                                {
                                    break;
                                }

                                addPageToPDF(doc);
                            }

                            currCoordinate = nextCoordinate;
                        }
                    }
                }
            }

            doc.Save(filename);
            doc.Close();
        }

        private void addPageToPDF(PdfDocument doc)
        {
            PdfPage page = doc.AddPage();
            var size = PageSizeConverter.ToSize(PageSize.A4);
            page.Width = size.Width;
            page.Height = size.Height;
            page.Orientation = PageOrientation.Portrait;
            page.TrimMargins.Top = marginTop;
            page.TrimMargins.Right = marginRight;
            page.TrimMargins.Bottom = marginBottom;
            page.TrimMargins.Left = marginLeft;
        }
    }
}
