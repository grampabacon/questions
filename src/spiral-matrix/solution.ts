function spiralOrder(matrix: number[][]): number[] {
  if (matrix.length === 0) {
    return [];
  }

  let top = 0;
  let bottom = matrix.length - 1;
  let left = 0;
  let right = matrix[0].length - 1;

  const result: number[] = [];

  while (top <= bottom && left <= right) {
    // Top: left -> right
    for (let x = left; x <= right; x++) {
      result.push(matrix[top][x]);
    }

    top++;

    // Right: top -> bottom
    for (let y = top; y <= bottom; y++) {
      result.push(matrix[y][right]);
    }

    right--;

    // Bottom: right -> left
    if (top <= bottom) {
      for (let x = right; x >= left; x--) {
        result.push(matrix[bottom][x]);
      }

      bottom--;
    }

    // Left: bottom -> top
    if (left <= right) {
      for (let y = bottom; y >= top; y--) {
        result.push(matrix[y][left]);
      }

      left++;
    }
  }

  return result;
}

function check(matrix: number[][], expected: number[]) {
  let actual = spiralOrder(matrix);
  if (JSON.stringify(actual) != JSON.stringify(expected)) {
    throw new Error(
      `Expected: ${JSON.stringify(expected)} ~ Actual ${JSON.stringify(actual)}`,
    );
  }
}

check(
  [
    [1, 2],
    [3, 4],
  ],
  [1, 2, 4, 3],
);
check(
  [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9],
  ],
  [1, 2, 3, 6, 9, 8, 7, 4, 5],
);
check(
  [
    [1, 2, 3, 4],
    [5, 6, 7, 8],
    [9, 10, 11, 12],
  ],
  [1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7],
);
console.log("All tests passed.");
