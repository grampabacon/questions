class TrappingRainWaterSolution {
  static int trap(int[] height) {
    int left = 0;
    int right = height.length - 1;

    int leftMax = 0;
    int rightMax = 0;

    int result = 0;

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

  static void check(int[] height, int expected) {
    int result = trap(height);
    if (result != expected) {
      throw new RuntimeException(String.format("Expected %s ~ Actual %s", expected, result));
    }
  }

  static void main(String[] args) {
    check(new int[] {0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1}, 6);
    check(new int[] {4, 2, 0, 3, 2, 5}, 9);
    check(new int[] {4, 2, 3}, 1);
    System.out.println("All tests passed.");
  }
}
