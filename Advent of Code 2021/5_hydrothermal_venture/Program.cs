using _5_hydrothermal_venture;

var input = File.ReadAllLines("input.txt");

List<(Point StartPoint, Point EndPoint)> points = new List<(Point, Point)>();

foreach (var line in input)
{
    var (startPoint, endPoint) = GetTwoElementsSplittedByString(line, "->");

    var (startPointX, startPointY) = GetTwoElementsSplittedByString(startPoint, ",");

    var (endPointX, endPointY) = GetTwoElementsSplittedByString(endPoint, ",");

    points.Add(new(new Point(startPointX, startPointY), new Point(endPointX, endPointY)));
}

var pointsOnMap = new List<Point>();

foreach (var (StartPoint, EndPoint) in points)
{
    bool isHorizontalLine = StartPoint.Y == EndPoint.Y;

    bool isVerticalLine = StartPoint.X == EndPoint.X;

    if (isHorizontalLine)
    {
        int startX, endX;

        if (StartPoint.X > EndPoint.X)
        {
            startX = EndPoint.X;
            endX = StartPoint.X;
        }
        else
        {
            startX = StartPoint.X;
            endX = EndPoint.X;
        }

        for (int i = startX; i <= endX; i++)
        {
            pointsOnMap.Add(new Point(i, StartPoint.Y));
        }
    }
    else if (isVerticalLine)
    {
        int startY, endY;

        if (StartPoint.Y > EndPoint.Y)
        {
            startY = EndPoint.Y;
            endY = StartPoint.Y;
        }
        else
        {
            startY = StartPoint.Y;
            endY= EndPoint.Y;
        }

        for (int i = startY; i <= endY; i++)
        {
            pointsOnMap.Add(new Point(StartPoint.X, i));
        }
    }
    else // else statement is for part two
    {
        bool incrementX = StartPoint.X < EndPoint.X;

        bool incrementY = StartPoint.Y < EndPoint.Y;

        var (currentValueX, currentValueY) = (StartPoint.X, StartPoint.Y);

        for (int i = 0; i <= Math.Abs(StartPoint.X - EndPoint.X); i++)
        {
            pointsOnMap.Add(new Point(currentValueX, currentValueY));

            if (incrementX) currentValueX++;
            else currentValueX--;

            if (incrementY) currentValueY++;
            else currentValueY--;
        }
    }
}

var groupedPoints = pointsOnMap.GroupBy(p => new { p.X, p.Y })
    .Select(g => new { Value = g.Key, Count = g.Count() })
    .OrderByDescending(x => x.Count);

Console.WriteLine("Part two answer: " + groupedPoints.Where(g => g.Count > 1).Count());

static (string, string) GetTwoElementsSplittedByString(string input, string splitter)
{
    var splittedInput = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

    return (splittedInput[0], splittedInput[1]);
}