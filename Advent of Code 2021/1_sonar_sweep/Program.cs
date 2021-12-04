string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

int counter = 0;

int prev = Convert.ToInt32(inputData[0]);

foreach (var data in inputData.Skip(1))
{
    int value = Convert.ToInt32(data);

    if (value > prev) counter++;

    prev = value;
}

Console.WriteLine("First part answer: " + counter);

counter = 0;

prev = Convert.ToInt32(inputData[0]) + Convert.ToInt32(inputData[1]) + Convert.ToInt32(inputData[2]);

for (int i = 1; i < inputData.Length - 2; i++)
{
    int currentSum = Convert.ToInt32(inputData[i]) + Convert.ToInt32(inputData[i + 1]) + Convert.ToInt32(inputData[i + 2]);

    if (currentSum > prev) counter++;

    prev = currentSum;
}

Console.WriteLine("Second part answer: " + counter);