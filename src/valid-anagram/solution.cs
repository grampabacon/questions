bool ValidAnagram(string s, string t)
{
	Dictionary<char, int> characters = new();

	if (s.Length != t.Length)
	{
		return false;
	}

	foreach(char c in s)
	{
		if (characters.TryGetValue(c, out var value))
		{
			characters[c]++;
		}
		else
		{
			characters.Add(c, 1);
		}
	}

	foreach(char c in t)
	{
		if (characters.TryGetValue(c, out var value))
		{
			if (--characters[c] == 0)
			{
				characters.Remove(c);
			}
		}
		else
		{
			return false;
		}
	}

	if (characters.Count == 0)
	{
		return true;
	}

	return false;
}

void Check(string s, string t, bool expected)
{
	bool result = ValidAnagram(s, t);
	if (result != expected) {
		throw new Exception($"Test failed: got {result}, expected {expected}");
	}
}

Check("racecar", "eracrac", true);
Check("eiufhdoufh", "siucgsidgco", false)
Console.WriteLine("All tests passed.");
