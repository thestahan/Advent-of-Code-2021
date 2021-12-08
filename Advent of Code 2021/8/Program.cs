var input = File.ReadAllLines("input.txt");

var numberMappings = new List<string>
{
    "abcefg", //0
    "cf", //1
    "acdeg", //2
    "acdfg", //3
    "bcdf", //4
    "abdfg", //5
    "abdefg", //6
    "acf", //7
    "abcdefg", //8
    "abcdfg"//9
};

var result = input.Select(x => x.Split("|").Select(y => y.Trim())).Sum(x =>
{
    var input = x.ElementAt(0).Split(" ");

    var output = x.ElementAt(1).Split(" ");

    var mappings = GetMappings(input);

    string outputSum = string.Empty;

    foreach (var outputNumber in output)
    {
        var matchingMapping = mappings.Single(x => x.Length == outputNumber.Length && x.All(outputNumber.Contains));

        outputSum += mappings.IndexOf(matchingMapping);
    }

    return Int32.Parse(outputSum);
});

Console.WriteLine($"Part two answer: {result}");

static List<string> GetMappings(IEnumerable<string> entryValues)
{
    var one = entryValues.Single(x => x.Length == 2);
    var seven = entryValues.Single(x => x.Length == 3);
    var four = entryValues.Single(x => x.Length == 4);
    var eight = entryValues.Single(x => x.Length == 7);

    var two = entryValues.Single(x => x.Length == 5 && !one.All(x.Contains) && !four.Except(one).All(x.Contains));
    var three = entryValues.Single(x => x.Length == 5 && one.All(x.Contains));
    var five = entryValues.Single(x => x.Length == 5 && four.Except(one).All(x.Contains));

    var six = entryValues.Single(x => x.Length == 6 && !one.All(x.Contains));
    var nine = entryValues.Single(x => x.Length == 6 && four.All(x.Contains));
    var zero = entryValues.Single(x => x.Length == 6 && one.All(x.Contains) && !four.All(x.Contains));

    return new List<string> { zero, one, two, three, four, five, six, seven, eight, nine };
}