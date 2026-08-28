using System;

int FirstMissingPositive(int[] nums)
{
    for (var i = 0; i < nums.Length; )
    {
        var value = nums[i];
        if (value < 1)
        {
            i++;
            continue;
        }

        if (value > nums.Length)
        {
            i++;
            continue;
        }

        if (nums[value - 1] == nums[i])
        {
            i++;
            continue;
        }

        nums[i] = nums[value - 1];
        nums[value - 1] = value;
    }

    var smallestInt = 1;
    for (var i = 0; i < nums.Length; i++)
    {
        if (nums[i] != smallestInt)
        {
            break;
        }

        smallestInt++;
    }

    return smallestInt;
}

void Check(int[] nums, int expected)
{
    var result = FirstMissingPositive(nums);

    Console.WriteLine(result);
}

Check([1, 2, 0], 3);
Check([3, 4, -1, 1], 2);
Check([7, 8, 9, 11, 12], 1);
