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

fun main() {
//    println(jump(intArrayOf(2, 3, 1, 1, 4)))
//    println(jump(intArrayOf(2, 3, 0, 1, 4)))
//    println(jump(intArrayOf(1, 3, 2)))
//    println(jump(intArrayOf(1, 2, 0, 1)))
    println(jump(intArrayOf(1, 1, 1, 1)))
}
