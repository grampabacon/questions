int ReverseInteger(int input)
{
    if (input == int.MinValue)
    {
        return 0;
    }

    var isNegative = input < 0;
    if (isNegative)
    {
        input = Math.Abs(input);
    }

    string chars = $"{input}";

    int output = 0;
    for (int i = 0; i < chars.Length; i++)
    {
        var multiplier = (int)Math.Pow(10, i);
        var nextDigit = (int)char.GetNumericValue(chars[i]);
        var toAdd = multiplier * nextDigit;

        if (int.MaxValue / multiplier < nextDigit)
        {
            return 0;
        }
        if (int.MaxValue - output < toAdd)
        {
            return 0;
        }

        output += toAdd;
    }

    if (isNegative)
        output *= -1;

    return output;
}

void Check(int input, int expected)
{
    var actual = ReverseInteger(input);
    if (actual != expected)
    {
        throw new Exception($"Actual value {actual} does not meet expected {expected}");
    }
}

Check(120, 21);
Check(1, 1);
Check(-837492374, -473294738);
Check(1534236469, 0);
Check(-2147483648, 0);
Console.WriteLine("All tests passed.");
