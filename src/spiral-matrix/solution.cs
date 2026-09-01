IList<int> SpiralOrder(int[][] matrix)
{
    if (matrix.Length == 0)
        return [];

    var m = matrix.Length;
    var n = matrix[0].Length;

    var top = 0;
    var bottom = m - 1;

    var left = 0;
    var right = n - 1;

    var x = 0;
    var y = 0;

    var result = new List<int>();
    while (result.Count < m * n)
    {
        if (y == top && x != right)
        {
            // Move to the right
            // Console.WriteLine("right");
            result.Add(matrix[y][x]);
            x++;

            if (x == right && top != bottom - 1)
            {
                top++;
            }
        }
        else if (x == right && y != bottom)
        {
            // Move down
            // Console.WriteLine("down");
            result.Add(matrix[y][x]);

            y++;

            if (y == bottom && right != left + 1)
            {
                right--;
            }
        }
        else if (y == bottom && x != left)
        {
            // Move left
            // Console.WriteLine("left");
            result.Add(matrix[y][x]);

            x--;
            if (x == left && bottom != top + 1)
            {
                bottom--;
            }
        }
        else
        {
            // Moving up
            // Console.WriteLine("up");
            result.Add(matrix[y][x]);

            y--;

            if (y == top && left != right - 1)
            {
                left++;
            }
        }
    }

    return result;
}

void Check(int[][] matrix, IList<int> expected)
{
    var actual = SpiralOrder(matrix);
    if (!actual.SequenceEqual(expected))
    {
        throw new Exception(
            $"Expected {string.Join(", ", expected)} ~ Actual {string.Join(", ", actual)}"
        );
    }
}

Check(
    [
        [1, 2],
        [3, 4],
    ],
    [1, 2, 4, 3]
);

Check(
    [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9],
    ],
    [1, 2, 3, 6, 9, 8, 7, 4, 5]
);
Check(
    [
        [1, 2, 3, 4],
        [5, 6, 7, 8],
        [9, 10, 11, 12],
    ],
    [1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]
);
Console.WriteLine("All tests passed.");
