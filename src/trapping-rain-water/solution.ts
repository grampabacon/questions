// Two pointer solution
function trap(height: number[]): number {
  let left = 0;
  let right = height.length - 1;

  let leftMax = 0;
  let rightMax = 0;

  let result = 0;

  while (left <= right) {
    if (height[left] <= height[right]) {
      if (height[left] >= leftMax) {
        leftMax = height[left];
      } else {
        result += leftMax - height[left];
      }

      left++;
    } else {
      if (height[right] >= rightMax) {
        rightMax = height[right];
      } else {
        result += rightMax - height[right];
      }

      right--;
    }
  }

  return result;
}

function check(height: number[], expected: number) {
  let result = trap(height);
  if (result != expected) {
    throw new Error(`Expected ${expected} ~ Actual ${result}`);
  }
}

check([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1], 6);
check([4, 2, 0, 3, 2, 5], 9);
check([4, 2, 3], 1);
console.log("All tests passed.");
