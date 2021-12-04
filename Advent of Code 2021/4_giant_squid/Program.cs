using _4_giant_squid;

var inputData = File.ReadAllLines("input.txt");

var moves = inputData.First().Split(",").Select(x => Int32.Parse(x));

var boards = new List<Board>();

for (int i = 2; i < inputData.Length; i += 6)
{
    var rowsData = new List<List<int>>
    {
        inputData[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList(),
        inputData[i + 1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList(),
        inputData[i + 2].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList(),
        inputData[i + 3].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList(),
        inputData[i + 4].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList()
    };

    boards.Add(new Board { RowsData = rowsData });
}

int winningScore = SimulateGamePartOne(moves, boards);

int lastWinningScore = SimulateGamePartTwo(moves, boards);

Console.WriteLine("First part: " + winningScore);

Console.WriteLine("Second part: " + lastWinningScore);

static int SimulateGamePartOne(IEnumerable<int> moves, List<Board> boards)
{
    foreach (var move in moves)
    {
        foreach (var board in boards)
        {
            board.MarkBingoNumber(move);

            if (board.DidWin()) return board.GetWinningScore(move);
        }
    }

    return 0;
}

static int SimulateGamePartTwo(IEnumerable<int> moves, List<Board> boards)
{
    var indexesToConsider = Enumerable.Range(0, boards.Count).ToList();

    foreach (var move in moves)
    {
        for (int i = 0; i < boards.Count; i++)
        {
            if (!indexesToConsider.Contains(i)) continue;

            var board = boards[i];

            board.MarkBingoNumber(move);

            if (board.DidWin())
            {
                if (indexesToConsider.Count == 1)
                {
                    return board.GetWinningScore(move);
                }

                indexesToConsider.Remove(i);
            }
        }
    }

    return 0;
}