// Modify in place
function solveSudoku(board: string[][]): void {
    solve(board);
}

function solve(board: string[][]): boolean {
    let bestCandidateX = -1;
    let bestCandidateY = -1;
    let bestCandidateCount = 10;

    for (let i = 0; i < 9; i++) {
        if (bestCandidateCount === 1) {
            break;
        }

        for (let j = 0; j < 9; j++) {
            if (board[i][j] !== ".") {
                continue;
            }

            const candidates = getCandidates(board, i, j);
            if (candidates.size === 0) {
                return false;
            }

            if (candidates.size < bestCandidateCount) {
                bestCandidateCount = candidates.size;
                bestCandidateX = i;
                bestCandidateY = j;

                if (bestCandidateCount === 1) {
                    break;
                }
            }
        }
    }

    // No empty cells = solved
    if (bestCandidateCount === 10) {
        return true;
    }

    const candidates = getCandidates(board, bestCandidateX, bestCandidateY);

    for (const entry of candidates) {
        board[bestCandidateX][bestCandidateY] = `${entry}`;

        if (solve(board)) {
            return true;
        }

        board[bestCandidateX][bestCandidateY] = ".";
    }

    return false;
}

// x = row, y = column
function getCandidates(board: string[][], x: number, y: number): Set<number> {
    if (!Number.isNaN(parseInt(board[x][y]))) {
        return new Set<number>([parseInt(board[x][y])]);
    }

    let possibleValues = new Set<number>([1, 2, 3, 4, 5, 6, 7, 8, 9]);
    let inverseCandidates = new Set<number>(
        board[x]
            .map(s => parseInt(s, 10))
            .filter(n => !Number.isNaN(n))
    );
    for (let i = 0; i < 9; i++) {
        let value = parseInt(board[i][y]);
        if (!Number.isNaN(value)) {
            inverseCandidates.add(value);
        }
    }

    let startXGridPos = Math.floor(x / 3) * 3;
    let startYGridPos = Math.floor(y / 3) * 3;

    for (let i = startXGridPos; i < startXGridPos + 3; i++) {
        for (let j = startYGridPos; j < startYGridPos + 3; j++) {
            let value = parseInt(board[i][j]);
            if (!Number.isNaN(value)) {
                inverseCandidates.add(value);
            }
        }
    }

    return new Set([...possibleValues].filter(n => !inverseCandidates.has(n)));
}

function check(board: string[][], expected: string[][]) {
    solveSudoku(board);
    if (JSON.stringify(board) !== JSON.stringify(expected)) {
        throw new Error(`Expected ${JSON.stringify(expected)} ~ Actual ${JSON.stringify(board)}`);
    }
}




check([["5", "3", ".", ".", "7", ".", ".", ".", "."], ["6", ".", ".", "1", "9", "5", ".", ".", "."], [".", "9", "8", ".", ".", ".", ".", "6", "."], ["8", ".", ".", ".", "6", ".", ".", ".", "3"], ["4", ".", ".", "8", ".", "3", ".", ".", "1"], ["7", ".", ".", ".", "2", ".", ".", ".", "6"], [".", "6", ".", ".", ".", ".", "2", "8", "."], [".", ".", ".", "4", "1", "9", ".", ".", "5"], [".", ".", ".", ".", "8", ".", ".", "7", "9"]], [["5","3","4","6","7","8","9","1","2"],["6","7","2","1","9","5","3","4","8"],["1","9","8","3","4","2","5","6","7"],["8","5","9","7","6","1","4","2","3"],["4","2","6","8","5","3","7","9","1"],["7","1","3","9","2","4","8","5","6"],["9","6","1","5","3","7","2","8","4"],["2","8","7","4","1","9","6","3","5"],["3","4","5","2","8","6","1","7","9"]])
check([[".", ".", "9", "7", "4", "8", ".", ".", "."], ["7", ".", ".", ".", ".", ".", ".", ".", "."], [".", "2", ".", "1", ".", "9", ".", ".", "."], [".", ".", "7", ".", ".", ".", "2", "4", "."], [".", "6", "4", ".", "1", ".", "5", "9", "."], [".", "9", "8", ".", ".", ".", "3", ".", "."], [".", ".", ".", "8", ".", "3", ".", "2", "."], [".", ".", ".", ".", ".", ".", ".", ".", "6"], [".", ".", ".", "2", "7", "5", "9", ".", "."]],[
    [
        '5', '1', '9',
        '7', '4', '8',
        '6', '3', '2'
    ],
    [
        '7', '8', '3',
        '6', '5', '2',
        '4', '1', '9'
    ],
    [
        '4', '2', '6',
        '1', '3', '9',
        '8', '7', '5'
    ],
    [
        '3', '5', '7',
        '9', '8', '6',
        '2', '4', '1'
    ],
    [
        '2', '6', '4',
        '3', '1', '7',
        '5', '9', '8'
    ],
    [
        '1', '9', '8',
        '5', '2', '4',
        '3', '6', '7'
    ],
    [
        '9', '7', '5',
        '8', '6', '3',
        '1', '2', '4'
    ],
    [
        '8', '3', '2',
        '4', '9', '1',
        '7', '5', '6'
    ],
    [
        '6', '4', '1',
        '2', '7', '5',
        '9', '8', '3'
    ]
])
console.log("All tests passed.")
