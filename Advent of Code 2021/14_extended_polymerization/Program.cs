var input = File.ReadAllLines("input.txt");

var rules = input.Skip(2).Select(line => (pair: line.Split("->")[0].Trim(), elementToInsert: line.Split("->")[1].Trim())).ToList();

int steps = 40;

string startingText = input.First();

var pairs = new Dictionary<string, long>();

var lettersCounts = startingText.Distinct().ToDictionary(c => c.ToString(), c => (long)startingText.Count(x => x == c));

for (int i = 0; i < startingText.Length - 1; i++)
{
    string pairToAdd = startingText.Substring(i, 2);

    pairs[pairToAdd] = pairs.GetValueOrDefault(pairToAdd, 0) + 1;
}

for (int i = 0; i < steps; i++)
{
    var newPairs = new Dictionary<string, long>();

    foreach (var pair in pairs)
    {
        var rule = rules.FirstOrDefault(r => r.pair == pair.Key);

        var pairsToInsert = new List<string> { rule.pair[0] + rule.elementToInsert, rule.elementToInsert + rule.pair[1] };

        foreach (var pairToInsert in pairsToInsert)
        {
            newPairs[pairToInsert] = newPairs.GetValueOrDefault(pairToInsert, 0) + pair.Value;
        }

        lettersCounts[rule.elementToInsert] = lettersCounts.GetValueOrDefault(rule.elementToInsert, 0) + pair.Value;
    }

    pairs = newPairs.ToDictionary(p => p.Key, p => p.Value);
}

long leastCommonQuantity = lettersCounts.Min(l => l.Value);

long mostCommonQuantity = lettersCounts.Max(l => l.Value);

Console.WriteLine("Part two answer: " + (mostCommonQuantity - leastCommonQuantity));