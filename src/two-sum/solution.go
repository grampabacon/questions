package main

import (
	"fmt"
	"os"
	"reflect"
)

func twoSum(nums []int, target int) []int {
	seen := make(map[int]int)

	for i, num := range nums {
		complement := target - num
		if idx, ok := seen[complement]; ok {
			return []int{idx, i}
		}
		seen[num] = i
	}

	return []int{}
}

func check(actual, expected []int) {
	if !reflect.DeepEqual(actual, expected) {
		fmt.Printf("Test failed: got %v, expected %v\n", actual, expected)
		os.Exit(1)
	}
}

func main() {
	check(twoSum([]int{2, 7, 11, 15}, 9), []int{0, 1})
	check(twoSum([]int{3, 2, 4}, 6), []int{1, 2})
	check(twoSum([]int{3, 3}, 6), []int{0, 1})
	fmt.Println("All tests passed.")
}
