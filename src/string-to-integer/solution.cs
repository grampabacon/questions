int MyAtoi(string s)
{
    long result = 0;
    var isPositive = true;
    var resultUpdated = false;

    for (var i = 0; i < s.Length; i++)
    {
        var c = s[i];

        if (c == char.Parse(" ") && !resultUpdated)
        {
            continue;
        }

        // Break if c is not a digit
        if (!char.IsNumber(c) && !((c == char.Parse("-") || c == char.Parse("+")) && !resultUpdated))
        {
            break;
        }

        if (result == 0 && (c == char.Parse("0")))
        {
            resultUpdated = true;
            continue;
        }

        if (c == char.Parse("+") && !resultUpdated)
        {
            resultUpdated = true;
            continue;
        }

        if (c == char.Parse("-") && !resultUpdated)
        {
            isPositive = !isPositive;
            resultUpdated = true;
            continue;
        }

        result *= 10;
        int digit = (int)char.GetNumericValue(c);

        resultUpdated = true;
        result += digit;

        if (isPositive && result > int.MaxValue)
        {
            return int.MaxValue;
        }

        if (!isPositive && -result < int.MinValue)
        {
            return int.MinValue;
        }
    }

    // We can cast as we know that it is within int limits
    return isPositive ? (int)result : -1 * (int)result;
}

void Check(string s, int expected)
{
    var result = MyAtoi(s);
    if (result != expected)
    {
        throw new Exception($"Expect {expected}, got {result}.");
    }
}

Check("+1", 1);
Check("+-4", 0);
Check(" -042", -42);
Check("0-1", 0);
Check("words and 987", 0);
Check("4193 with words", 4193);
Console.WriteLine("All tests passed.");
