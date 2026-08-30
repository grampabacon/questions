package `trapping-rain-water`

fun trap(height: IntArray): Int {
    var left = 0
    var right = height.size - 1

    var leftMax = 0
    var rightMax = 0

    var result = 0

    while (left <= right) {
        if (height[left] <= height[right]) {
            if (height[left] >= leftMax) {
                leftMax = height[left]
            } else {
                result += leftMax - height[left]
            }

            left++
        } else {
            if (height[right] >= rightMax) {
                rightMax = height[right]
            } else {
                result += rightMax - height[right]
            }

            right--
        }
    }

    return result
}

fun check(height: IntArray, expected: Int) {
    val result = trap(height)
    if (result != expected) {
        throw RuntimeException("Expected $expected ~ Actual $result")
    }
}

fun main() {
    check(intArrayOf(0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1), 6)
    check(intArrayOf(4, 2, 0, 3, 2, 5), 9)
    check(intArrayOf(4, 2, 3), 1)
    println("All tests passed.")
}
