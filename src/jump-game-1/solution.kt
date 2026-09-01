package `jump-game-1`

fun canJump(nums: IntArray): Boolean {
    if (nums.size <= 1) return true

    var jumps = 0
    var currentEnd = 0
    var furthest = 0

    for (i in 0 until nums.size - 1) {
        furthest = maxOf(furthest, i + nums[i])

        if (i == currentEnd) {
            if (currentEnd == furthest) return false

            jumps++
            currentEnd = furthest
        }
    }

    return true
}

fun check(nums: IntArray, expected: Boolean) {
    val result = canJump(nums)
    if (result != expected) {
        throw RuntimeException("Expected $expected ~ Actual $result")
    }
}

fun main() {
    check(intArrayOf(1, 1, 1, 1), true)
    check(intArrayOf(2, 3, 1, 1, 4), true)
    check(intArrayOf(3,2,1,0,4), false)
    println("All tests passed.")
}
