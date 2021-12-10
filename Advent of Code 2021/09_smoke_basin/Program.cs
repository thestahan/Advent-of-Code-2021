var input = File.ReadAllLines("input.txt");

var rows = input.Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToList()).ToList();

var lowestPointsInAreas = new List<int>();

var basins = new List<int>();

for (int i = 0; i < rows.Count; i++)
{
    for (int j = 0; j < rows[i].Count; j++)
    {
        int top = i - 1 < 0 ? -1 : rows[i - 1][j];
        int bottom = i + 1 >= rows.Count ? -1 : rows[i + 1][j];
        int left = j - 1 < 0 ? -1 : rows[i][j - 1];
        int right = j + 1 >= rows[i].Count ? -1 : rows[i][j + 1];

        var area = new List<int> { top, bottom, left, right };

        if (IsLowestInArea(rows[i][j], area.Where(x => x != -1).ToList()))
        {
            lowestPointsInAreas.Add(rows[i][j]); // part one

            var lowestPoint = new Point { X = j, Y = i };

            var neighbours = new List<Point> { lowestPoint };

            var allNeighbours = FindPointNeighbours(lowestPoint, neighbours, rows);

            basins.Add(allNeighbours.Count);
        }
    }
}

Console.WriteLine("Part one answer: " + (lowestPointsInAreas.Sum() + lowestPointsInAreas.Count));

var topBasins = basins.OrderByDescending(x => x).Take(3).ToList();

Console.WriteLine("Part two answer: " + topBasins[0] * topBasins[1] * topBasins[2]);

bool IsLowestInArea(int number, IEnumerable<int> area)
{
    return area.All(x => x > number);
}

List<Point> FindPointNeighbours(Point point, List<Point> currentNeighbours, List<List<int>> map)
{
    var neighbours = new List<Point>();

    int top = point.Y - 1 < 0 ? -1 : point.Y - 1;
    int bottom = point.Y + 1 >= map.Count ? -1 : point.Y + 1;
    int left = point.X - 1 < 0 ? -1 : point.X - 1;
    int right = point.X + 1 >= map[point.Y].Count ? -1 : point.X + 1;

    if (top != -1) neighbours.Add(new Point { X = point.X, Y = top });
    if (bottom != -1) neighbours.Add(new Point { X = point.X, Y = bottom });
    if (left != -1) neighbours.Add(new Point { X = left, Y = point.Y });
    if (right != -1) neighbours.Add(new Point { X = right, Y = point.Y });

    foreach (var neighbour in neighbours)
    {
        if (map[neighbour.Y][neighbour.X] > map[point.Y][point.X] && 
            map[neighbour.Y][neighbour.X] < 9 &&
            currentNeighbours.All(p =>
            {
                return !(p.X == neighbour.X && p.Y == neighbour.Y);
            }))
        {
            currentNeighbours.Add(neighbour);
            FindPointNeighbours(neighbour, currentNeighbours, map);
        }
    }

    return currentNeighbours;
}

class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}