function threeSum(nums: number[]): number[][] {
    nums.sort((a, b) => a - b);
    const result: number[][] = [];

    for (let i = 0; i < nums.length; i++) {
        if (i > 0 && nums[i] == nums[i - 1]) {
            continue;
        }

        if (nums[i] > 0) {
            break;
        }

        const target = -nums[i];
        let start = i + 1;
        let end = nums.length - 1;
        while (start < end) {

            let current = nums[start] + nums[end]
            if (current == target) {
                result.push([nums[i], nums[start], nums[end]]);
                start++;
                end--;

                while (start < end && nums[start] == nums[start - 1]) {
                    start++;
                }
            } else if (current < target) {
                start++;
            } else {
                end--;
            }
        }
    }

    return result;
}

function check(nums: number[], expected: number[][]) {
    const result = threeSum(nums);
    if (JSON.stringify(result) != JSON.stringify(expected)) {
        throw new Error(`Expected ${JSON.stringify(expected)}, got ${JSON.stringify(result)}`)
    }
}


check([-1, 0, 1, 2, -1, -4], [[-1, -1, 2], [-1, 0, 1]]);
check([0, 0, 0], [[0, 0, 0]]);
check([-2, 0, 1, 1, 2], [[-2, 0, 2], [-2, 1, 1]]);
check([-100,-70,-60,110,120,130,160], [[-100,-60,160],[-70,-60,130]]);
console.log("All tests passed.")
