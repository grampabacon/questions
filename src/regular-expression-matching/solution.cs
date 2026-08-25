bool IsMatch(string s, string p)
{
    bool Match(int i, int j)
    {
        if (j == p.Length)
        {
            return i == s.Length;
        }

        var firstMatch = i < s.Length && (s[i] == p[j] || p[j] == '.');

        if (j + 1 < p.Length && p[j + 1] == '*')
        {
            if (Match(i, j + 2))
                return true;

            if (firstMatch)
                return Match(i + 1, j);

            return false;
        }

        return firstMatch && Match(i + 1, j + 1);
    }

    return Match(0, 0);
}

void Check(string s, string p, bool expected)
{
    bool res = IsMatch(s, p);
    if (res != expected)
    {
        throw new Exception($"Fail on {s} {p} | Expected {expected}, got {res}");
    }
}

Check("mississippi", "mis*is*ip*.", true);

Check("aa", "a", false);
Check("aa", "a*", true);
Check("ab", ".*", true);

Check("aab", "c*a*b", true);

Console.WriteLine("All tests passed.");
