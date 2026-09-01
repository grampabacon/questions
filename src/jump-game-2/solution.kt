package `jump-game-2`

fun jump(nums: IntArray): Int {
    if (nums.size <= 1) return 0

    var jumps = 0
    var currentEnd = 0
    var furthest = 0

    for (i in 0 until nums.size - 1) {
        furthest = maxOf(furthest, i + nums[i])

        if (i == currentEnd) {
            jumps++
            currentEnd = furthest
        }
    }

    return jumps
}

fun check(nums: IntArray, expected: Int) {
    val result = jump(nums)
    if (result != expected) {
        throw RuntimeException("Expected $expected ~ Actual $result")
    }
}

fun main() {
    check(intArrayOf(1, 1, 1, 1), 3)
    check(intArrayOf(2, 3, 1, 1, 4), 2)
    check(intArrayOf(2, 3, 0, 1, 4), 2)
    check(intArrayOf(1, 3, 2), 2)
    check(intArrayOf(1, 2, 0, 1), 2)
}
