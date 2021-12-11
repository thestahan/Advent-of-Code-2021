var input = File.ReadAllLines("input.txt");

var octopuses = input.Select(line => line.Select(o => (int)char.GetNumericValue(o)).ToList()).ToList();

int steps = Int32.MaxValue; // for part one -> 100

int flashesCount = 0;

int allFlashedStep = 0;

for (int i = 0; i < steps; i++)
{
    if (octopuses.All(line => line.All(o => o == 0)))
    {
        allFlashedStep = i;
        break;
    }

    var flashedInStep = new List<(int x, int y)>();

    octopuses = octopuses.Select(line => line.Select(o => o + 1).ToList()).ToList();

    for (int j = 0; j < octopuses.Count; j++)
    {
        for (int k = 0; k < octopuses.First().Count; k++)
        {
            if (octopuses[j][k] > 9)
            {
                Flash(octopuses, k, j, flashedInStep, ref flashesCount);
            }
        }
    }
}

Console.WriteLine($"Flashes afer {steps} steps: {flashesCount}"); // for part one

Console.WriteLine($"All flashed on step: {allFlashedStep}");

static void Flash(List<List<int>> octopuses, int octopusX, int octopusY, List<(int x, int y)> flashedInStep,  ref int flashesCount)
{
    flashedInStep.Add((octopusX, octopusY));

    flashesCount++;

    var directions = new List<(int x, int y)>
    {
        (octopusX - 1, octopusY - 1),   // top left
        (octopusX, octopusY - 1),       // top
        (octopusX + 1, octopusY - 1),   // top right
        (octopusX + 1, octopusY),       // right
        (octopusX + 1, octopusY + 1),   // bottom right
        (octopusX, octopusY + 1),       // bottom
        (octopusX - 1, octopusY + 1),   // bottom left
        (octopusX - 1, octopusY),       // left
    };

    directions = directions.Where(dir => 
            (dir.x < octopuses.First().Count && dir.x >= 0) && 
            (dir.y < octopuses.Count && dir.y >= 0))
        .ToList();

    foreach (var (x, y) in directions)
    {
        if (flashedInStep.Any(f => f.x == x && f.y == y)) continue;

        octopuses[y][x]++;

        if (octopuses[y][x] > 9)
        {
            Flash(octopuses, x, y, flashedInStep, ref flashesCount);
        }
    }

    octopuses[octopusY][octopusX] = 0;
}