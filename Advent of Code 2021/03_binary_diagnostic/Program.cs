var input = File.ReadAllLines("input.txt");

int columnsCount = input.First().Length;

var indexesToConsider = new List<int>();

for (int i = 0; i < columnsCount; i++)
{
    var indexesOfPositiveNumbers = new List<int>();
    var indexesOfNegativeNumbers = new List<int>();

    int positiveBitsCount = 0;
    int negativeBitsCount = 0;

    for (int j = 0; j < input.Length; j++)
    {
        if (i >= 1 && !indexesToConsider.Contains(j))
            continue;

        if (input[j][i] == '1')
        {
            positiveBitsCount++;
            indexesOfPositiveNumbers.Add(j);
        }
        else
        {
            negativeBitsCount++;
            indexesOfNegativeNumbers.Add(j);
        }
    }

    if (positiveBitsCount >= negativeBitsCount) indexesToConsider = indexesOfPositiveNumbers;
    else indexesToConsider = indexesOfNegativeNumbers;

    if (indexesToConsider.Count == 1) break;
}

var scubberIndexesToConsider = new List<int>();

for (int i = 0; i < columnsCount; i++)
{
    var indexesOfPositiveNumbers = new List<int>();
    var indexesOfNegativeNumbers = new List<int>();

    int positiveBitsCount = 0;
    int negativeBitsCount = 0;

    for (int j = 0; j < input.Length; j++)
    {
        if (i >= 1 && !scubberIndexesToConsider.Contains(j))
            continue;

        if (input[j][i] == '1')
        {
            positiveBitsCount++;
            indexesOfPositiveNumbers.Add(j);
        }
        else
        {
            negativeBitsCount++;
            indexesOfNegativeNumbers.Add(j);
        }
    }

    if (positiveBitsCount >= negativeBitsCount) scubberIndexesToConsider = indexesOfNegativeNumbers;
    else scubberIndexesToConsider = indexesOfPositiveNumbers;

    if (scubberIndexesToConsider.Count == 1) break;
}

int firstNumber = Convert.ToInt32(input[indexesToConsider.First()], 2);
int secondNumber = Convert.ToInt32(input[scubberIndexesToConsider.First()], 2);

Console.WriteLine("Answer: " + firstNumber * secondNumber);