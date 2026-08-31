using System;
using System.Text;

string Multiply(string num1, string num2)
{
    if (num1.Length == 0 || num2.Length == 0)
    {
        return "";
    }

    // Longest result possible is n + m
    var result = new int[num1.Length + num2.Length];

    for (var i = num1.Length - 1; i >= 0; i--)
    {
        for (var j = num2.Length - 1; j >= 0; j--)
        {
            var x = (int)char.GetNumericValue(num1[i]);
            var y = (int)char.GetNumericValue(num2[j]);

            var pos = i + j + 1;

            var mult = x * y;
            var total = mult + result[pos];

            result[pos] = total % 10;
            result[pos - 1] += total / 10;
        }
    }

    var sb = new StringBuilder();

    var start = 0;
    while (start < result.Length - 1 && result[start] == 0)
    {
        start++;
    }

    for (var i = start; i < result.Length; i++)
    {
        sb.Append(result[i]);
    }

    return sb.ToString();
}

void Check(string num1, string num2, string expected)
{
    var result = Multiply(num1, num2);
    if (result != expected)
    {
        throw new Exception($"Expected {expected} ~ Actual {result}");
    }
}

Check("2", "3", "6");
Check("38", "24", "912");
Check("99", "99", "9801");
Check("123", "456", "56088");
Console.WriteLine("All tests passed.");
