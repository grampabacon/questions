IList<IList<string>> GroupAnagrams(string[] strs)
{
    if (strs.Length == 0)
        return [];

    List<IList<string>> result = [];
    for (var i = 0; i < strs.Length; )
    {
        List<string> temp = [];
        for (var j = i; j < strs.Length; j++)
        {
            var found = strs[j];
            if (ValidAnagram(strs[i], found))
            {
                strs[j] = strs[i + temp.Count]; // Swap matched to start of array
                strs[i + temp.Count] = found;
                temp.Add(found);
            }
        }

        result.Add(temp);
        i += temp.Count;
    }

    return result;
}

bool ValidAnagram(string s, string t)
{
    if (s.Length != t.Length)
        return false;

    int[] counts = new int[26];

    foreach (var c in s)
        counts[c - 'a']++;

    foreach (var c in t)
        counts[c - 'a']--;

    return counts.All(x => x == 0);
}

bool DeepEqualsUnordered(IList<IList<string>> a, IList<IList<string>> b)
{
    if (a.Count != b.Count)
        return false;

    var normalise = (IList<IList<string>> x) =>
        x.Select(inner => inner.OrderBy(s => s))
            .OrderBy(inner => string.Join("\0", inner))
            .ToList();

    return normalise(a).Zip(normalise(b)).All(pair => pair.First.SequenceEqual(pair.Second));
}

void Check(string[] strs, IList<IList<string>> expected)
{
    var result = GroupAnagrams(strs);
    if (!DeepEqualsUnordered(result, expected))
    {
        throw new Exception(
            $"Lists don't match. Actual: [{string.Join(" | ", result.Select(x => $"[{string.Join(", ", x)}]"))}]; Expected: [{string.Join(" | ", expected.Select(x => $"[{string.Join(", ", x)}]"))}]"
        );
    }
}

Check(
    [""],
    [
        [""],
    ]
);
Check(
    ["a"],
    [
        ["a"],
    ]
);
Check(
    ["eat", "tea", "tan", "ate", "nat", "bat"],
    [
        ["bat"],
        ["nat", "tan"],
        ["ate", "eat", "tea"],
    ]
);
Check(
    ["ad", "bc", "cb"],
    [
        ["ad"],
        ["bc", "cb"],
    ]
);
Console.WriteLine("All tests passed.");
