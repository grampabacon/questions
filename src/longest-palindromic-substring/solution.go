package main

import (
	"fmt"
	"os"
)

func longestPalindromicSubstring(s string) string {
	if s == reverse(s) {
		return s
	}

	for currentLength := len(s) - 1; currentLength > 0; currentLength-- {
		for remainder := len(s) - currentLength; remainder >= 0; remainder-- {
			substr := s[remainder:currentLength]
			if reverse(substr) == substr {
				return substr
			}
		}
	}

	return ""
}

func reverse(s string) string {
	r := []rune(s)

	for i, j := 0, len(r)-1; i < j; i, j = i+1, j-1 {
		r[i], r[j] = r[j], r[i]
	}

	return string(r)
}

func check(s string, expected []string) {
	actual := longestPalindromicSubstring(s)
	failed := true
	for _, b := range expected {
		if b == actual {
			failed = false
		}
	}

	if failed {
		fmt.Printf("Test failed: got %v, expected %v\n", actual, expected)
		os.Exit(1)
	}
}

func main() {
	check("babad", []string{"bab", "aba"})
	check("cbbd", []string{"bb"})
	fmt.Println("All tests passed.")
}
