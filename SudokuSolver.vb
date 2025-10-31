Public Class SudokuSolver
    ' Backtracking solver for a 9x9 Sudoku grid
    ' The grid is modified in place
    Public Function Solve(grid As Integer(,)) As Boolean
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                If grid(r, c) = 0 Then
                    For num As Integer = 1 To 9
                        If IsValid(grid, r, c, num) Then
                            grid(r, c) = num
                            If Solve(grid) Then Return True
                            grid(r, c) = 0 ' Backtrack
                        End If
                    Next
                    Return False
                End If
            Next
        Next
        Return True ' Solved
    End Function

    ' Check if placing 'num' at (row, col) is valid
    Public Function IsValid(grid As Integer(,), row As Integer, col As Integer, num As Integer) As Boolean
        ' Check row and column
        For i As Integer = 0 To 8
            If grid(row, i) = num OrElse grid(i, col) = num Then Return False
        Next

        ' Check 3x3 block
        Dim startRow = (row \ 3) * 3
        Dim startCol = (col \ 3) * 3
        For r As Integer = startRow To startRow + 2
            For c As Integer = startCol To startCol + 2
                If grid(r, c) = num Then Return False
            Next
        Next

        Return True
    End Function
End Class