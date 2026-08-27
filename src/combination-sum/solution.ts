function combinationSum(candidates: number[], target: number): number[][] {
  let output: number[][] = [];

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
      let c = candidates[i];
      result.push(c);

      findCandidate(i, remaining - c, result);

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

check([2, 3, 6, 7], 7, [[2, 2, 3], [7]]);
check([2, 3, 5], 8, [
  [2, 2, 2, 2],
  [2, 3, 3],
  [3, 5],
]);
check([2], 1, []);
