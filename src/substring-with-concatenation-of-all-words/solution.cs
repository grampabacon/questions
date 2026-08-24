using System;
using System.Collections.Generic;
using System.Linq;

IList<int> FindSubstring(string s, string[] words)
{
    List<int> result = [];
    if (words.Length == 0 || s.Length == 0) return result;

    int wordSize = words[0].Length;

    Dictionary<string, int> wordCounts = new Dictionary<string, int>();
    foreach (var word in words)
    {
        if (!wordCounts.TryAdd(word, 1))
            wordCounts[word]++;
    }

    for (int offset = 0; offset < wordSize; offset++)
    {
        Dictionary<string, int> currentCount = new Dictionary<string, int>();
        int start = offset;
        int count = 0;
        for (int end = offset; end + wordSize <= s.Length; end += wordSize)
        {
            var current = s.Substring(end, wordSize);
            if (wordCounts.TryGetValue(current, out var wordCount))
            {
                if (!currentCount.TryAdd(current, 1))
                {
                    currentCount[current]++;
                }

                count++;

                while (currentCount[current] > wordCount)
                {
                    string startWord = s.Substring(start, wordSize);
                    currentCount[startWord]--;
                    start += wordSize;
                    count--;
                }

                if (count == words.Length)
                {
                    result.Add(start);
                }
            }
            else
            {
                count = 0;
                start = end + wordSize;
                currentCount.Clear();
            }
        }
    }

    return result;
}

void Check(string s, string[] words, int[] expected)
{
    var actual = FindSubstring(s, words);
    if (!expected.SequenceEqual(actual))
    {
        throw new Exception($"Expected {string.Join(", ", expected)} ~ Actual {string.Join(", ", actual)}");
    }
}

Check("barfoothefoobarman", ["foo", "bar"], [0, 9]);
Check("wordgoodgoodgoodbestword", ["word", "good", "best", "word"], []);
Check("barfoofoobarthefoobarman", ["bar", "foo", "the"], [6, 9, 12]);
Console.WriteLine("All tests passed.");
