string LongestPalindromicSubstring(string s)
{
    int start = 0;
    int end = 0;
    for (int i = 0; i < s.Length; i++)
    {
        int oddLen = ExpandAboutCenter(s, i, i);
        int evenLen = ExpandAboutCenter(s, i, i + 1);
        int maxLen = Math.Max(oddLen, evenLen);

        if (maxLen > end - start)
        {
            start = i - ((maxLen - 1) / 2);
            end = i + (maxLen / 2);
        }
    }

    return s.Substring(start, end - start + 1);
}

int ExpandAboutCenter(string s, int left, int right)
{
    while (left >= 0 && right < s.Length && s[left] == s[right])
    {
        left--;
        right++;
    }

    return right - left - 1; // Length of substring
}

void Check(string s, string[] validSolutions)
{
    var failed = true;
    var substring = LongestPalindromicSubstring(s);
    foreach (var sol in validSolutions)
    {
        if (substring == sol)
        {
            failed = false;
        }
    }

    if (failed)
    {
        throw new Exception(
            $"Failure, expected {string.Join(", ", validSolutions)}; found {substring}"
        );
    }
}

Check("babad", ["bab", "aba"]);
Check("cbbd", ["bb"]);
Check(
    "fidfhouehfoheofhoehfoiehfioneiofndoibiaygd9qhdobwidetartratedduhaoufhdboehdoehodihpiadpiwhd083whd3d",
    ["detartrated"]
);
Console.WriteLine("All tests passed.");
