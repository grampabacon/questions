using System;
using System.Text;

string CountAndSay(int n)
{
    if (n < 1)
    {
        throw new ArgumentOutOfRangeException();
    }
    if (n == 1)
    {
        return "1";
    }

    var previousRecur = CountAndSay(n - 1);
    var output = new StringBuilder();

    char prevChar = previousRecur[0];
    var prevCount = 0;
    for (var i = 0; i < previousRecur.Length; i++)
    {
        var c = previousRecur[i];

        if (prevChar == c)
        {
            prevCount++;
            continue;
        }

        output.Append(prevCount);
        output.Append(prevChar);
        prevChar = c;
        prevCount = 1;
    }

    output.Append(prevCount);
    output.Append(prevChar);

    return output.ToString();
}

void Check(int n, string expected)
{
    string res = CountAndSay(n);
    if (res != expected)
    {
        throw new Exception($"Expected {expected} got {res}");
    }
}

Check(1, "1");
Check(4, "1211");
Check(7, "13112221");
Console.WriteLine("All tests passed.");
