using System;

int Trap(int[] height)
{
    if (height.Length < 3)
    {
        return 0;
    }

    var result = 0;
    var previousIndex = 0;

    for (var i = 0; i < height.Length; i++)
    {
        var value = height[i];

        if (value >= height[previousIndex])
        {
            for (var j = previousIndex + 1; j < i; j++)
            {
                result += height[previousIndex] - height[j];
            }

            previousIndex = i;
        }
    }

    // So far we have accounted for water in between columns, but the end of the array is not bounded by a barrier
    // So we need to do a backwards pass to previousIndex to chck there's no traps left
    var futureIndex = height.Length - 1;
    for (var i = height.Length - 1; i >= previousIndex; i--)
    {
        var value = height[i];

        if (value >= height[futureIndex])
        {
            for (var j = i + 1; j < futureIndex; j++)
            {
                result += height[futureIndex] - height[j];
            }

            futureIndex = i;
        }
    }

    return result;
}

void Check(int[] height, int expected)
{
    var result = Trap(height);
    if (result != expected)
    {
        throw new Exception($"Expected {expected} ~ Actual {result}");
    }
}

Check([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1], 6);
Check([4, 2, 0, 3, 2, 5], 9);
Check([4, 2, 3], 1);
Console.WriteLine("All tests passed.");
