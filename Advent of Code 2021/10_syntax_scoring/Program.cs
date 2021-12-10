var input = File.ReadAllLines("input.txt");

var matchingTags = new List<(char OpeningTag, char ClosingTag)>
{
    ('[', ']'),
    ('<', '>'),
    ('{', '}'),
    ('(', ')'),
};

int invalidClosingTagsScore = 0;

var missingTagsScore = new List<long>();

foreach (var line in input)
{
    var stack = new Stack<char>();

    bool foundBrokenLine = false;

    long lineMissingTagsScore = 0;

    foreach (var character in line)
    {
        stack.Push(character);

        if (matchingTags.Any(x => x.ClosingTag == character))
        {
            stack.Pop();

            if (stack.Peek() == matchingTags.Single(x => x.ClosingTag == character).OpeningTag)
            {
                stack.Pop();
            }
            else
            {
                invalidClosingTagsScore += GetClosingTagScore(character);
                foundBrokenLine = true;
                break;
            }
        }
    }

    while (stack.Count > 0 && !foundBrokenLine)
    {
        var openingTag = stack.Pop();

        var closingTag = matchingTags.Single(x => x.OpeningTag == openingTag).ClosingTag;

        lineMissingTagsScore = lineMissingTagsScore * 5 + GetMissingTagScore(closingTag);
    }

    if (lineMissingTagsScore > 0) missingTagsScore.Add(lineMissingTagsScore);
}

Console.WriteLine($"Part one answer: {invalidClosingTagsScore}");

Console.WriteLine($"Part two answer: {missingTagsScore.OrderBy(x => x).Skip(missingTagsScore.Count / 2).Take(1).First()}");

int GetClosingTagScore(char character) =>
    character switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => 0,
    };

long GetMissingTagScore(char character) =>
    character switch
    {
        ')' => 1,
        ']' => 2,
        '}' => 3,
        '>' => 4,
        _ => 0,
    };