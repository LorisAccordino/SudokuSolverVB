Imports System.Threading

Public Class MainForm
    Private sudoku As SudokuGrid
    Private solver As SudokuSolver = New SudokuSolver()
    Private solveTimer As Stopwatch = New Stopwatch()

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Create the grid
        sudoku = New SudokuGrid(Me)

        ' Adjust form size
        ClientSize = New Size(sudoku.GridWidth, sudoku.GridHeight + clearBtn.Height + 20)

        ' Prevent resizing
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
    End Sub

    Private Sub ClearBtn_Click(sender As Object, e As EventArgs) Handles clearBtn.Click
        sudoku.SetReadOnly(False)
        sudoku.ClearGrid()
    End Sub

    Private Sub uiTimer_Tick(sender As Object, e As EventArgs) Handles uiTimer.Tick
        timeLbl.Text = $"{solveTimer.Elapsed.TotalSeconds:F1}s"
    End Sub

    Private Sub SolveBtn_Click(sender As Object, e As EventArgs) Handles solveBtn.Click
        ' Disable input while solving
        sudoku.SetReadOnly(True)
        solveBtn.Enabled = False
        clearBtn.Enabled = False

        ' Get current grid
        Dim grid = sudoku.GetGrid()

        ' Start timers
        solveTimer.Restart()
        uiTimer.Start()

        ' Start solver in background thread
        Dim t As New Thread(Sub() SolveInThread(grid)) With {.IsBackground = True}
        t.Start()
    End Sub

    ' Method executed in the background thread for solving
    Private Sub SolveInThread(grid As Integer(,))
        ' Run backtracking solver and update cells in UI
        Dim solved = SolveSudokuWithUI(grid)

        ' Stop the timer
        solveTimer.Stop()
        uiTimer.Stop()

        ' Update the UI in the main thread once done
        sudoku.Cells(0, 0).Invoke(Sub() SolveFinished(grid, solved))
        sudoku.Cells(0, 0).Invoke(Sub() sudoku.SetReadOnly(False))
    End Sub

    ' Solve with UI updates (slowly to show progress)
    Private Function SolveSudokuWithUI(grid As Integer(,)) As Boolean
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                If grid(r, c) = 0 Then
                    For num As Integer = 1 To 9
                        If solver.Solve(New Integer(8, 8) {}) Then ' Optional: just to use solver logic
                            ' Not necessary, we handle backtracking here
                        End If
                        If solver.IsValid(grid, r, c, num) Then
                            grid(r, c) = num
                            UpdateCellUI(r, c, num.ToString())
                            Thread.Sleep(5)
                            If SolveSudokuWithUI(grid) Then Return True
                            grid(r, c) = 0
                            UpdateCellUI(r, c, "")
                        End If
                    Next
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    ' Helper method to update a cell safely from any thread
    Private Sub UpdateCellUI(r As Integer, c As Integer, value As String)
        sudoku.Cells(r, c).Invoke(Sub()
                                      sudoku.Cells(r, c).Text = value
                                  End Sub)
    End Sub

    ' Method to update the UI after solving is finished
    Private Sub SolveFinished(grid As Integer(,), solved As Boolean)
        ' Update the TextBoxes with the solved grid
        sudoku.SetGrid(grid)

        ' Show message box with result and elapsed time
        MsgBox(If(solved,
              $"Sudoku solved in {solveTimer.Elapsed.TotalSeconds:F2} seconds!",
              "Sudoku cannot be solved."),
           MsgBoxStyle.Information)

        ' Re-enable the buttons after solving
        solveBtn.Enabled = True
        clearBtn.Enabled = True
    End Sub
End Class