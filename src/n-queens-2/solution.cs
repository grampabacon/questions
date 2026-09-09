int TotalNQueens(int n)
{
    var count = 0;
    var mask = (1 << n) - 1;

    void Search(int columns, int diagonalsLeft, int diagonalsRight)
    {
        if (columns == mask)
        {
            count++;
            return;
        }

        int available = mask & ~(columns | diagonalsLeft | diagonalsRight);

        while (available != 0)
        {
            int position = available & -available;
            available -= position;

            Search(
                columns | position,
                (diagonalsLeft | position) << 1,
                (diagonalsRight | position) >> 1
            );
        }
    }

    Search(0, 0, 0);

    return count;
}

// int TotalNQueens(int n) {
//     var cols = new HashSet<int>();
//     var lowerDiags = new HashSet<int>();
//     var upperDiags = new HashSet<int>();
//
//     var count = 0;
//
//     void PlaceQueen(int row)
//     {
//         if (row == n)
//         {
//             count++;
//             return;
//         }
//
//         for (var col = 0; col < n; col++)
//         {
//             var lowerDiag = row - col;
//             var upperDiag = row + col;
//
//             if (cols.Contains(col))
//                 continue;
//             if (upperDiags.Contains(upperDiag) || lowerDiags.Contains(lowerDiag))
//                 continue;
//
//             upperDiags.Add(upperDiag);
//             lowerDiags.Add(lowerDiag);
//             cols.Add(col);
//
//             PlaceQueen(row + 1);
//
//             upperDiags.Remove(upperDiag);
//             lowerDiags.Remove(lowerDiag);
//             cols.Remove(col);
//         }
//     }
//
//     PlaceQueen(0);
//
//     return count;
// }

void Check(int n, int expected)
{
    var actual = TotalNQueens(n);
    if (actual != expected)
    {
        throw new Exception($"Expected {expected} ~ Actual {actual}");
    }
}

Check(4, 2);
Check(1, 1);
Console.WriteLine("All tests passed.");
