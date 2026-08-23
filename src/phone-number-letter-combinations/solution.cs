IList<string> LetterCombinations(string digits)
{
    var combinations = new Dictionary<int, char[]>()
    {
        { 2, ['a', 'b', 'c'] },
        { 3, ['d', 'e', 'f'] },
        { 4, ['g', 'h', 'i'] },
        { 5, ['j', 'k', 'l'] },
        { 6, ['m', 'n', 'o'] },
        { 7, ['p', 'q', 'r', 's'] },
        { 8, ['t', 'u', 'v'] },
        { 9, ['w', 'x', 'y', 'z'] }
    };

    string[] output = combinations[(int)char.GetNumericValue(digits[0])].Select(c => c.ToString()).ToArray();
    for (var i = 1; i < digits.Length; i++)
    {
        List<string> next = [];

        int digit = (int)char.GetNumericValue(digits[i]);

        var characters = combinations[digit];

        for (var j = 0; j < output.Length; j++)
        {
            var current = output[j];
            for (var k = 0; k < characters.Length; k++)
            {
                next.Add(current + characters[k]);
            }
        }

        output = next.ToArray();
    }

    return output;
}

void Check(string digits, string[] expected)
{
    var actual = LetterCombinations(digits);
    if (!actual.SequenceEqual(expected))
    {
        throw new Exception($"Expected {string.Join(", ", expected)} ~ Actual {string.Join(", ", actual)}");
    }
}

Check("23", ["ad","ae","af","bd","be","bf","cd","ce","cf"]);
Check("9", ["w", "x", "y", "z"]);
Console.WriteLine("All tests passed.");
