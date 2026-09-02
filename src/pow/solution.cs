double MyPow(double x, int n)
{
    if (n == 0)
        return 1;

    var result = 1.0;
    var currentPower = x;
    var exponent = Math.Abs((long)n);

    while (exponent > 0)
    {
        if (exponent % 2 != 0)
        {
            result *= currentPower;
        }

        currentPower *= currentPower;

        exponent /= 2;
    }

    if (n < 0)
    {
        result = 1 / result;
    }

    return result;
}

void Check(double x, int n, double expected)
{
    var actual = MyPow(x, n);
    if (Math.Abs(actual - expected) > 1e-9)
    {
        throw new Exception($"Expected {expected} ~ Actual {actual}");
    }
}

Check(4, 0, 1);
Check(16, 1, 16);
Check(2.00000, 10, 1024.00000);
Check(2.10000, 3, 9.26100);
Check(2.00000, -2, 0.25000);
Check(1, 2147483647, 1);
Check(2.00000, -2147483648, 0);

Console.WriteLine("All tests passed.");
