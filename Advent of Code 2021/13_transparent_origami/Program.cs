var input = File.ReadAllLines("input.txt");

var indexOfEmptyLine = Array.IndexOf(input, string.Empty);

var points = input.Take(indexOfEmptyLine)
    .Select(line => 
        (x: Int32.Parse(line.Split(",")[0]), y: Int32.Parse(line.Split(",")[1])))
    .ToList();

var folds = input.Skip(indexOfEmptyLine + 1)
    .Select(line => line.Replace("fold along ", ""))
    .Select(line => (axis: line.Split("=")[0], value: Int32.Parse(line.Split("=")[1])))
    .ToList();

foreach (var (axis, value) in folds)
{
    for (int i = 0; i < points.Count; i++)
    {
        if (axis == "x" && points[i].x >= value)
        {
            points[i] = (x: value - (Math.Abs(value - points[i].x)), y: points[i].y);
        }
        else if (axis == "y" && points[i].y >= value)
        {
            points[i] = (x: points[i].x, y: value - (Math.Abs(value - points[i].y)));
        }
    }

    Console.WriteLine($"Points after fold: {points.Distinct().Count()}");
}

points = points.Distinct().ToList();

for (int i = 0; i <= points.Max(p => p.y); i++)
{
    for (int j = 0; j <= points.Max(p => p.x); j++)
    {
        if (points.Any(p => p.y == i && p.x == j))
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
            Console.ResetColor();
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
}