from typing import List


def two_sum(nums: List[int], target: int) -> List[int]:
    complements = {}

    for i, num in enumerate(nums):
        complement = target - num
        if complement in complements:
            return [complements[complement], i]
        complements[num] = i

    return []


if __name__ == "__main__":
    assert two_sum([2, 7, 11, 15], 9) == [0, 1]
    assert two_sum([3, 2, 4], 6) == [1, 2]
    assert two_sum([3, 3], 6) == [0, 1]
    print("All tests passed.")
