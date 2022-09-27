namespace MTGProxyTutorNet.BusinessLogic.PDF
{
    internal class PDFCoordinate
    {
        private const int MAX_COL_NUMBER = 3;
        private const int MAX_ROW_NUMBER = 3;

        public PDFCoordinate()
        { }

        public PDFCoordinate(int pageNumber, int rowNumber, int colNumber)
        {
            PageNumber = pageNumber;
            RowNumber = rowNumber;
            ColNumber = colNumber;
        }

        public int PageNumber { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        public PDFCoordinate Clone()
        {
            return new PDFCoordinate(PageNumber, RowNumber, ColNumber);
        }

        public PDFCoordinate NextCoordinate()
        {
            var nextCoord = this.Clone();
            nextCoord.ColNumber++;

            if (nextCoord.ColNumber == MAX_COL_NUMBER)
            {
                nextCoord.ColNumber = 0;
                nextCoord.RowNumber++;
            }

            if (nextCoord.RowNumber == MAX_ROW_NUMBER)
            {
                return new PDFCoordinate(this.PageNumber + 1, 0, 0);
            }

            return nextCoord;
        }
    }
}