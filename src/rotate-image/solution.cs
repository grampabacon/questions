void Rotate(int[][] matrix)
{
    int n = matrix.Length,
        k = n - 1;

    // Process each layer from the outside towards the centre.
    for (int i = 0; i < n >> 1; i++)
    {
        // Move across the current layer, excluding the final position
        // because it will be handled by the 4-way swap.
        for (int j = i; j < k - i; j++)
        {
            int value = matrix[i][j];

            // Left -> Top
            matrix[i][j] = matrix[k - j][i];

            // Bottom -> Left
            matrix[k - j][i] = matrix[k - i][k - j];

            // Right -> Bottom
            matrix[k - i][k - j] = matrix[j][k - i];

            // Top -> Right
            matrix[j][k - i] = value;
        }
    }
}

bool AreArraysEqual(int[][] array1, int[][] array2)
{
    if (ReferenceEquals(array1, array2))
        return true;
    if (array1 == null || array2 == null)
        return false;

    if (array1.Length != array2.Length)
        return false;

    for (int i = 0; i < array1.Length; i++)
    {
        if (array1[i] == null && array2[i] == null)
            continue;
        if (array1[i] == null || array2[i] == null)
            return false;

        if (!array1[i].SequenceEqual(array2[i]))
        {
            return false;
        }
    }

    return true;
}

void Check(int[][] matrix, int[][] expected)
{
    Rotate(matrix);
    if (!AreArraysEqual(matrix, expected))
    {
        throw new Exception(
            $"Expected {string.Join(", ", expected)} ~ Actual {string.Join(", ", matrix)}"
        );
    }
}

Check(
    [
        [1],
    ],
    [
        [1],
    ]
);
Check(
    [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9],
    ],
    [
        [7, 4, 1],
        [8, 5, 2],
        [9, 6, 3],
    ]
);
Check(
    [
        [5, 1, 9, 11],
        [2, 4, 8, 10],
        [13, 3, 6, 7],
        [15, 14, 12, 16],
    ],
    [
        [15, 13, 2, 5],
        [14, 3, 4, 1],
        [12, 6, 8, 9],
        [16, 7, 10, 11],
    ]
);
Console.WriteLine("All tests passed.");
