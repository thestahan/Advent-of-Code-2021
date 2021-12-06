var input = File.ReadAllLines("input.txt");

var lanternfish = input.First().Split(",").Select(Int32.Parse).ToList();

int daysCount = 256;

var fishDays = new long[9];

foreach (var fish in lanternfish)
{
    fishDays[fish]++;
}

for (int i = 0; i < daysCount; i++)
{
    var newFishDays = new long[9];

    newFishDays[6] = fishDays[0];
    newFishDays[8] = fishDays[0];
    newFishDays[0] = 0;

    for (int j = 1; j < fishDays.Length; j++)
    {
        newFishDays[j - 1] += fishDays[j];
    }

    fishDays = newFishDays;
}

Console.WriteLine($"Fish count after {daysCount} days: {fishDays.Sum()}");