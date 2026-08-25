function twoSum(nums: number[], target: number): number[] {
  const seen = new Map<number, number>();

  for (let i = 0; i < nums.length; i++) {
    const complement = target - nums[i];
    const foundAt = seen.get(complement);
    if (foundAt !== undefined) {
      return [foundAt, i];
    }
    seen.set(nums[i], i);
  }

  return [];
}

function check(actual: number[], expected: number[]): void {
  if (JSON.stringify(actual) !== JSON.stringify(expected)) {
    throw new Error(
      `Test failed: got ${JSON.stringify(actual)}, expected ${JSON.stringify(expected)}`,
    );
  }
}

check(twoSum([2, 7, 11, 15], 9), [0, 1]);
check(twoSum([3, 2, 4], 6), [1, 2]);
check(twoSum([3, 3], 6), [0, 1]);
console.log("All tests passed.");
