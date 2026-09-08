def solveNQueens(n):
    """
    :type n: int
    :rtype: List[List[str]]
    """
    occupies_cols = set()
    occupied_diags1 = set()
    occupied_diags2 = set()

    solutions = []

    board = [["."] * n for _ in range(n)]

    def place_queen(row):
        if row == n:
            solutions.append(["".join(r) for r in board])
            return

        for col in range(0, len(board)):
            diag1 = row - col
            diag2 = row + col

            if col in occupies_cols:
                continue

            if diag1 in occupied_diags1 or diag2 in occupied_diags2:
                continue

            board[row][col] = "Q"
            occupied_diags1.add(diag1)
            occupied_diags2.add(diag2)
            occupies_cols.add(col)

            place_queen(row + 1)

            board[row][col] = "."
            occupied_diags1.discard(diag1)
            occupied_diags2.discard(diag2)
            occupies_cols.discard(col)

    place_queen(0)

    return solutions


if __name__ == "__main__":
    assert solveNQueens(4) == [
        [".Q..", "...Q", "Q...", "..Q."],
        ["..Q.", "Q...", "...Q", ".Q.."],
    ]
    assert solveNQueens(1) == [["Q"]]
    print("All tests passed.")
