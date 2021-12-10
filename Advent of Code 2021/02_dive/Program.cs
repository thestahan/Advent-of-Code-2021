var input = File.ReadAllLines("input.txt");

int depth = 0;

int horizontal = 0;

int aim = 0;

foreach (var line in input)
{
    var splittedLine = line.Split(" ");

    string actionName = splittedLine[0];

    int value = int.Parse(splittedLine[1]);

    if (actionName == "forward")
    {
        horizontal += value;
        depth += value * aim;
    }
    else if (actionName == "up")
    {
        //depth -= value;
        aim -= value;
    }
    else
    {
        //depth += value;
        aim += value;
    }
}

Console.WriteLine(depth + " * " + horizontal + " = " + depth * horizontal);