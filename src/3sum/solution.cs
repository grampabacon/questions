int[][] ThreeSum(int[] nums)
{
    nums.Sort();
    List<int[]> result = [];

    for (var i = 0; i < nums.Length; i++)
    {
        // Ignores duplicate triplets
        if (i > 0 && nums[i] == nums[i - 1])
        {
            continue;
        }

        // If we are already in the positive half then there can be no valid triplets formed
        if (nums[i] > 0)
        {
            break;
        }

        var target = -nums[i];
        var start = i + 1;
        var end = nums.Length - 1;
        while (start < end)
        {
            var current = nums[start] + nums[end];
            if (current == target)
            {
                result.Add([nums[i], nums[start], nums[end]]);
                start++;
                end--;

                // Skip duplicates
                while (start < end && nums[start] == nums[start - 1])
                {
                    start++;
                }
            }
            else if (current < target)
            {
                start++;
            }
            else
            {
                end--;
            }
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

void Check(int[] nums, int[][] expected)
{
    var result = ThreeSum(nums);
    if (!AreArraysEqual(result, expected))
    {
        Console.WriteLine("Expected: ");
        foreach (var e in expected)
        {
            Console.WriteLine($"    {string.Join(", ", e)}");
        }
        Console.WriteLine("Actual: ");
        foreach (var a in result)
        {
            Console.WriteLine($"    {string.Join(", ", a)}");
        }
        throw new Exception("Actual and expected do not match.");
    }
}

Check(
    [-1, 0, 1, 2, -1, -4],
    [
        [-1, -1, 2],
        [-1, 0, 1],
    ]
);
Check(
    [0, 0, 0],
    [
        [0, 0, 0],
    ]
);
Check(
    [-2, 0, 1, 1, 2],
    [
        [-2, 0, 2],
        [-2, 1, 1],
    ]
);
Check(
    [-100, -70, -60, 110, 120, 130, 160],
    [
        [-100, -60, 160],
        [-70, -60, 130],
    ]
);
Console.WriteLine("All tests passed.");
