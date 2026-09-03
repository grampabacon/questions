function permute(nums: number[]): number[][] {
  if (nums.length == 0) return [];

  let result: number[][] = [[]];

  for (let num of nums) {
    const current: number[][] = [];
    for (let arr of result) {
      for (let i = 0; i <= arr.length; i++) {
        const temp = [...arr];

        temp.splice(i, 0, num);
        current.push(temp);
      }
    }
    result = current;
  }

  return result;
}

function arraysEqual(a: number[][], b: number[][]): boolean {
  if (a.length !== b.length) return false;

  const sortRows = (arr: number[][]) =>
    [...arr].sort((rowA, rowB) => {
      for (let i = 0; i < Math.max(rowA.length, rowB.length); i++) {
        const diff = (rowA[i] ?? -Infinity) - (rowB[i] ?? -Infinity);
        if (diff !== 0) return diff;
      }
      return 0;
    });

  const sortedA = sortRows(a);
  const sortedB = sortRows(b);

  return sortedA.every(
    (row, i) =>
      row.length === sortedB[i].length &&
      row.every((value, j) => value === sortedB[i][j]),
  );
}

function check(nums: number[], expected: number[][]) {
  let result = permute(nums);

  if (!arraysEqual(expected, result)) {
    throw new Error(
      `Expected ${JSON.stringify(expected)} ~ Actual ${JSON.stringify(result)}`,
    );
  }
}

check(
  [1, 2, 3],
  [
    [1, 2, 3],
    [1, 3, 2],
    [2, 1, 3],
    [2, 3, 1],
    [3, 1, 2],
    [3, 2, 1],
  ],
);
check(
  [0, 1],
  [
    [0, 1],
    [1, 0],
  ],
);
check([1], [[1]]);
check([], []);
console.log("All tests passed.");
