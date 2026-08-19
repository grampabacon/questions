string Convert(string s, int numRows)
{
    if (numRows <= 1 || s.Length < numRows)
    {
        return s;
    }

    char[,] grid = new char[s.Length, numRows];

    var character = 0;
    for (int i = 0; character < s.Length; i++)
    {
        var x = i / numRows;
        var y = (i % (numRows));
        if (x % (numRows - 1) == 0)
        {
            grid[x, y] = s[character++];
        }
        else if (x % (numRows - 1) + y == (numRows - 1))
        {
            grid[x, y] = s[character++];
        }
    }

    var output = new System.Text.StringBuilder();
    for (int row = 0; row < grid.GetLength(1); row++)
    {
        for (int col = 0; col < grid.GetLength(0); col++)
        {
            if (grid[col, row] != '\0')
            {
                output.Append(grid[col, row]);
            }
        }
    }
    return output.ToString();
}

void Check(string s, int numRows, string expected)
{
    var actual = Convert(s, numRows);
    if (actual != expected)
    {
        throw new Exception($"Expected {expected}, got {actual}. Failed.");
    }
}


Check("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR");
Check("PAYPALISHIRING", 4, "PINALSIGYAHRPI");
Check("A", 1, "A");
Console.WriteLine("All tests pass.");
