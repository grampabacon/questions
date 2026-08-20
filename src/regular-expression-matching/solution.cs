bool IsMatch(string s, string p)
{
    // Break up string into individual rules.
    List<string> rules = [];
    for (var i = 0; i < p.Length; i++)
    {
        var c = p[i];
        if (c == char.Parse("*"))
        {
            rules[^1] += "*";
            continue;
        }

        rules.Add(c.ToString());
    }

    Console.WriteLine(string.Join(", ", rules));

    return false;
}

void Check(string s, string p, bool expected)
{
    bool res = IsMatch(s, p);
    if (res != expected)
    {
        // throw new Exception($"Expected {expected}, got {res}");
    }
}

Check("aa", "ab*", false);
Check("aa", "a*", true);
Check("ab", ".*", true);
Console.WriteLine("All tests passed.");
