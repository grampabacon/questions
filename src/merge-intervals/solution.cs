int[][] Merge(int[][] intervals)
{
    // Sorting ensures we only have to compare to the previous result
    intervals.Sort((a, b) => a[0] - b[0]);

    List<int[]> result = [];
    foreach (var interval in intervals)
    {
        if (result.Count == 0)
        {
            result.Add(interval.ToArray());
            continue;
        }

        var previous = result[^1];
        if (interval[0] <= previous[1])
        {
            previous[1] = Math.Max(previous[1], interval[1]);
        }
        else
        {
            result.Add(interval.ToArray());
        }
    }

    return result.ToArray();
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

void Check(int[][] intervals, int[][] expected)
{
    var result = Merge(intervals);
    if (!AreArraysEqual(result, expected))
    {
        throw new Exception(
            $"Lists don't match. Actual: [{string.Join(" | ", result.Select(x => $"[{string.Join(", ", x)}]"))}]; Expected: [{string.Join(" | ", expected.Select(x => $"[{string.Join(", ", x)}]"))}]"
        );
    }
}

Check(
    [
        [1, 3],
        [2, 6],
        [8, 10],
        [15, 18],
    ],
    [
        [1, 6],
        [8, 10],
        [15, 18],
    ]
);
Check(
    [
        [1, 4],
        [4, 5],
    ],
    [
        [1, 5],
    ]
);
Check(
    [
        [4, 7],
        [1, 4],
    ],
    [
        [1, 7],
    ]
);

Console.WriteLine("All tests passed.");
