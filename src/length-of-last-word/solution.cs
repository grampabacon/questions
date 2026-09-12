int LengthOfLastWord(string s)
{
    s = s.Trim();
    var split = s.Split(" ");

    return split[^1].Length;
}

void Check(string s, int expected)
{
    var result = LengthOfLastWord(s);
    if (result != expected)
    {
        throw new Exception($"Expected {expected} ~ Actual {result}");
    }
}

Check("Hello World", 5);
Check("   fly me   to   the moon  ", 4);
Check("luffy is still joyboy", 6);
Console.WriteLine("All tests passed.");
