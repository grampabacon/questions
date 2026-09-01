# Questions

Just some fun coding questions from different sources to keep me sharp on a variety of languages and skills.

Goals of this repo:
- Stay sharp on core algorithm/data-structure patterns
- Compare idioms across languages

## Progress

<!-- progress:start -->
Legend: 🐍 Python · 🟨 JavaScript · 🔷 TypeScript · 🐹 Go · 🟪 C# · ☕ Java · 🟠 Kotlin · 💎 Ruby

| Problem | Topic | Difficulty | Solutions |
| --- | --- | --- | --- |
| [3Sum](src/3sum) | — | — | 🔷 🟪 |
| [Champernowne's Constant](src/champernownes-constant) | — | — | 🟪 |
| [Combination Sum](src/combination-sum) | — | — | 🔷 |
| [Combination Sum II](src/combination-sum-2) | — | — | 🔷 |
| [Count and Say](src/count-and-say) | — | — | 🟪 |
| [First Missing Positive](src/first-missing-positive) | — | — | 🟪 |
| [Jump Game](src/jump-game-1) | — | — | 🟠 |
| [Jump Game II](src/jump-game-2) | — | — | 🟠 |
| [Letter Combinations of a Phone Number](src/phone-number-letter-combinations) | — | — | 🟪 |
| [Longest Palindromic Substring](src/longest-palindromic-substring) | — | — | 🐹 🟪 |
| [Median Of Two Sorted Arrays](src/median-of-two-sorted-arrays) | — | — | 🟪 |
| [Merge k Sorted Lists](src/merge-k-sorted-lists) | — | — | 🟪 |
| [Multiply Strings](src/multiply-strings) | — | — | 🟪 |
| [Regular Expression Matching](src/regular-expression-matching) | — | — | 🟪 |
| [Reverse Integer](src/reverse-integer) | — | — | 🟪 |
| [Spiral Matrix](src/spiral-matrix) | — | — | 🔷 🟪 |
| [String to Integer (atoi)](src/string-to-integer) | — | — | 🟪 |
| [Substring with Concatenation of All Words](src/substring-with-concatenation-of-all-words) | — | — | 🟪 |
| [Sudoku Solver](src/sudoku-solver) | — | — | 🔷 |
| [Trapping Rain Water](src/trapping-rain-water) | — | — | 🔷 🟪 ☕ 🟠 💎 |
| [Two Sum](src/two-sum) | — | — | 🐍 🟨 🔷 🐹 🟪 |
| [Valid Anagram](src/valid-anagram) | — | — | 🐍 🟪 ☕ |
| [ZigZag Conversion](src/zigzag-conversion) | — | — | 🟪 |
<!-- progress:end -->

## Testing locally

Three scripts, all skip a language automatically if you don't have that toolchain installed:

```bash
# Run every solution for one problem
./scripts/run.sh two-sum

# Run every solution in the whole repo
./scripts/run-all.sh

# Run one specific solution file
./scripts/run-file.sh two-sum/solution.py
```

## How each problem is organized

```
/problem-name
  README.md
  solution.py
  solution.js
  solution.go
```
