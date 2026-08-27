function combinationSum(candidates: number[], target: number): number[][] {
  let output: number[][] = [];

  candidates.sort((a, b) => a - b);

  function findCandidate(
    start: number,
    remaining: number,
    result: number[],
  ): void {
    if (remaining == 0) {
      output.push([...result]);
      return;
    }
    if (remaining < 0) {
      return;
    }

    for (let i = start; i < candidates.length; i++) {
      if (i > start && candidates[i] === candidates[i - 1]) {
        continue;
      }

      let c = candidates[i];

      result.push(c);

      findCandidate(i + 1, remaining - c, result);

      result.pop();
    }
  }

  findCandidate(0, target, []);

  return output;
}

function check(
  candidates: number[],
  target: number,
  expected: number[][],
): void {
  var result = combinationSum(candidates, target);
  if (JSON.stringify(result) != JSON.stringify(expected)) {
    throw new Error(
      `Expected ${JSON.stringify(expected)} ~ Actual ${JSON.stringify(result)}`,
    );
  }
}

check([10, 1, 2, 7, 6, 1, 5], 8, [
  [1, 1, 6],
  [1, 2, 5],
  [1, 7],
  [2, 6],
]);
check([2, 5, 2, 1, 2], 5, [[1, 2, 2], [5]]);
console.log("All tests passed.");
