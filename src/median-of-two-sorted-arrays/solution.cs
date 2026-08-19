double MedianOfTwoSortedArrays(int[] nums1, int[] nums2)
{
    int m = nums1.Length;
    int n = nums2.Length;

    if (m + n == 1)
    {
        if (m > 0)
        {
            return nums1[0];
        }

        if (n > 0)
        {
            return nums2[0];
        }
    }

    int i = 0;
    int j = 0;

    int mid1 = 0;
    int mid2 = 0;

    int average = (m + n) / 2;

    for (int count = 0; count <= average; count++)
    {
        // mid1 is now the next biggest element, mid2 holds it
        mid2 = mid1;

        // both arrays have more elements
        if (i != m && j != n)
        {
            mid1 = (nums2[j] < nums1[i]) ? nums2[j++] : nums1[i++];
        }

        // Only num2 has remaining elements
        else if (j != n)
        {
            mid1 = nums2[j++];
        }

        else
        {
            mid1 = nums1[i++];
        }
    }

    if ((m + n) % 2 != 0)
    {
        // Odd so we only need the middle element
        return mid1;
    }
    else
    {
        return (mid1 + mid2) / 2.0;
    }
}

void Check(int[] nums1, int[] nums2, double expected)
{
    var result = MedianOfTwoSortedArrays(nums1, nums2);
    if (Math.Abs(result - expected) > 0.000000001)
    {
        throw new Exception($"Calculated result {result} is not expected {expected}");
    }
}

Check([], [1], 1.0);
Check([2], [], 2.0);
Check([1, 2], [3, 4], 2.5);
Check([1, 3], [2], 2.0);
Check([73], [20, 89, 100], 81.0);
Check([1,2,3,4,5], [6,7,8,9,10,11,12,13,14,15,16,17], 9);
Console.WriteLine("All checks passed.");
