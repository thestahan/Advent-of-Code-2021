var input = File.ReadAllLines("input.txt");

var crabPositions = input.First().Split(",").Select(Int32.Parse).ToList();

var allPossibleCrabPositions = Enumerable.Range(0, crabPositions.Max());

var partOneAnswer = allPossibleCrabPositions
    .Select(c => crabPositions
        .Sum(c2 => Math.Abs(c - c2)))
    .Min();

var partTwoAnswer = allPossibleCrabPositions
    .Select(c => crabPositions
        .Sum(c2 =>
        {
            int n = Math.Abs(c - c2);

            return n * (n + 1) / 2;
        }))
    .Min();

Console.WriteLine($"Part one answer: {partOneAnswer}");

Console.WriteLine($"Part two answer: {partTwoAnswer}");