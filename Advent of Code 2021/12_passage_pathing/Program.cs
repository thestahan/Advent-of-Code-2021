var input = File.ReadAllLines("input.txt");

var connections = input.Select(line => (FirstCave: line.Split("-")[0], SecondCave: line.Split("-")[1])).ToList();

var caves = input.SelectMany(line => line.Split("-")).Distinct().ToList();

var paths = new List<string>();

var starts = connections.Where(c => c.FirstCave == "start" || c.SecondCave == "start").ToList();

foreach (var start in starts)
{
    if (start.FirstCave == "start")
    {
        var pathsToAdd = GetPaths(connections, start.SecondCave, new List<string> { start.FirstCave, start.SecondCave });

        paths.AddRange(pathsToAdd);
    }
    else
    {
        var pathsToAdd = GetPaths(connections, start.FirstCave, new List<string> { start.SecondCave, start.FirstCave });

        paths.AddRange(pathsToAdd);
    }
}

Console.WriteLine($"Second part answer: {paths.Count}");

List<string> GetPaths(List<(string FirstCave, string SecondCave)> connections, string currentCave, List<string> currentPath)
{
    var newPaths = new List<string>();

    var possibleConnections = connections.Where(c => 
            (c.FirstCave == currentCave || c.SecondCave == currentCave) && 
            (c.FirstCave != "start" && c.SecondCave != "start"))
        .ToList();

    foreach (var possibleConnection in possibleConnections)
    {
        string nextCave = currentCave == possibleConnection.FirstCave ? possibleConnection.SecondCave : possibleConnection.FirstCave;

        var smallCaves = currentPath.Where(c => char.IsLower(c[0]))
            .GroupBy(c => c);

        var smallCaveOccuredTwice = smallCaves.Any(c => c.Count() > 1);

        if (char.IsLower(nextCave[0]) && (smallCaveOccuredTwice && currentPath.Contains(nextCave))) continue;

        if (nextCave == "end")
        {
            newPaths.Add(string.Join(",", currentPath) + ",meta");

            continue;
        };

        newPaths.AddRange(GetPaths(connections, nextCave, currentPath.Concat(new List<string> { nextCave }).ToList()));
    }

    return newPaths;
}