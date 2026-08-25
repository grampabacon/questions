using System;
using System.Collections.Generic;

int[] TwoSum(int[] nums, int target)
{
    Dictionary<int, int> complements = new();

    for (int i = 0; i < nums.Length; i++)
    {
        int complement = target - nums[i];
        if (complements.ContainsKey(complement))
        {
            return [complements[complement], i];
        }
        complements[nums[i]] = i;
    }

    return [];
}

void Check(int[] actual, int[] expected)
{
    if (actual.Length != expected.Length || actual[0] != expected[0] || actual[1] != expected[1])
    {
        throw new Exception(
            $"Test failed: got [{string.Join(",", actual)}], expected [{string.Join(",", expected)}]"
        );
    }
}

Check(TwoSum(new int[] { 2, 7, 11, 15 }, 9), new int[] { 0, 1 });
Check(TwoSum(new int[] { 3, 2, 4 }, 6), new int[] { 1, 2 });
Check(TwoSum(new int[] { 3, 3 }, 6), new int[] { 0, 1 });
Console.WriteLine("All tests passed.");
