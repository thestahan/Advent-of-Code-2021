namespace _4_giant_squid
{
    internal class Board
    {
        public List<List<int>> RowsData { get; set; } = new List<List<int>>();

        public void MarkBingoNumber(int number)
        {
            for (int i = 0; i < RowsData.Count; i++)
            {
                for (int j = 0; j < RowsData[i].Count; j++)
                {
                    if (RowsData[i][j] == number) RowsData[i][j] = -1;
                }
            }
        }

        public bool DidWin()
        {
            for (int i = 0; i < RowsData.Count; i++)
            {
                bool rowWon = RowsData[i].All(x => x == -1);

                List<int> columnData = new();

                for (int j = 0; j < RowsData[i].Count; j++) columnData.Add(RowsData[j][i]);

                bool columnWon = columnData.All(x => x == -1);

                if (columnWon || rowWon) return true;
            }

            return false;
        }

        public int GetWinningScore(int lastNumber)
        {
            int remainingNumbersSum = RowsData.Select(r => r.Where(c => c != -1).Sum(c => c)).Sum();

            return remainingNumbersSum * lastNumber;
        }
    }
}
