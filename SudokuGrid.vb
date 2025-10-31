Public Class SudokuGrid
    Public Cells(8, 8) As TextBox
    Private parentForm As Form

    Private startX As Integer = 20
    Private startY As Integer = 20
    Private size As Integer = 50
    Private spacing As Integer = 5
    Private blockSpacing As Integer = 10
    Private fontSize As Integer

    Public ReadOnly Property GridWidth As Integer
        Get
            ' Left margin + width of 9 boxes + spacing between boxes + block spacing + right margin
            Return startX * 2 + size * 9 + spacing * 8 + blockSpacing * 2
        End Get
    End Property

    Public ReadOnly Property GridHeight As Integer
        Get
            ' Top margin + height of 9 boxes + spacing between boxes + block spacing + bottom margin
            Return startY * 2 + size * 9 + spacing * 8 + blockSpacing * 2
        End Get
    End Property


    Public Sub New(parent As Form)
        parentForm = parent
        fontSize = CInt(size * 0.4)
        InitializeGrid()
        SetupNavigation()
    End Sub

    Private Sub InitializeGrid()
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                Dim extraX As Integer = (c \ 3) * blockSpacing
                Dim extraY As Integer = (r \ 3) * blockSpacing

                Cells(r, c) = New TextBox With {
                    .Width = size,
                    .Height = size,
                    .MaxLength = 1,
                    .TextAlign = HorizontalAlignment.Center,
                    .Font = New Font("Arial", fontSize, FontStyle.Bold),
                    .Left = startX + c * (size + spacing) + extraX,
                    .Top = startY + r * (size + spacing) + extraY,
                    .Tag = Tuple.Create(r, c)
                }

                parentForm.Controls.Add(Cells(r, c))
            Next
        Next
    End Sub

    Private Sub SetupNavigation()
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                AddHandler Cells(r, c).KeyPress, AddressOf TextBox_KeyPress
                AddHandler Cells(r, c).KeyDown, AddressOf TextBox_KeyDown
            Next
        Next
    End Sub

    ' Handle digit input
    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim tb As TextBox = DirectCast(sender, TextBox)

        ' Allow only digits 1-9
        If Not Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "0"c Then
            e.Handled = True
            Return
        End If

        ' Prevent default input
        e.Handled = True
        tb.Text = e.KeyChar.ToString()

        ' Get position
        Dim pos = DirectCast(tb.Tag, Tuple(Of Integer, Integer))
        Dim r As Integer = pos.Item1
        Dim c As Integer = pos.Item2

        ' Move focus like Right Arrow / Space
        If c < 8 Then
            MoveFocus(r, c, 1, 0) ' normale spostamento a destra
        ElseIf r < 8 Then
            ' ultima colonna → prima cella della riga successiva
            Cells(r + 1, 0).Focus()
            Cells(r + 1, 0).SelectAll()
        End If
    End Sub

    ' Handle navigation keys
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb As TextBox = DirectCast(sender, TextBox)
        Dim pos = DirectCast(tb.Tag, Tuple(Of Integer, Integer))
        Dim r As Integer = pos.Item1
        Dim c As Integer = pos.Item2

        Select Case e.KeyCode
            Case Keys.Right, Keys.Space
                If c < 8 Then
                    MoveFocus(r, c, 1, 0)
                ElseIf r < 8 Then
                    Cells(r + 1, 0).Focus()
                    Cells(r + 1, 0).SelectAll()
                End If
                e.Handled = True

            Case Keys.Left
                If c > 0 Then
                    MoveFocus(r, c, -1, 0)
                ElseIf r > 0 Then
                    Cells(r - 1, 8).Focus()
                    Cells(r - 1, 8).SelectAll()
                End If
                e.Handled = True

            Case Keys.Up
                If r > 0 Then MoveFocus(r, c, 0, -1)
                e.Handled = True

            Case Keys.Down
                If r < 8 Then MoveFocus(r, c, 0, 1)
                e.Handled = True

            Case Keys.Enter
                If r < 8 Then
                    Cells(r + 1, 0).Focus()
                    Cells(r + 1, 0).SelectAll()
                End If
                e.Handled = True

            Case Keys.Back
                tb.Text = ""
                If c > 0 Then
                    MoveFocus(r, c, -1, 0)
                ElseIf r > 0 Then
                    Cells(r - 1, 8).Focus()
                    Cells(r - 1, 8).SelectAll()
                End If
                e.Handled = True
        End Select
    End Sub

    ' Move focus inside grid with clamping
    Private Sub MoveFocus(r As Integer, c As Integer, deltaC As Integer, deltaR As Integer)
        Dim newR As Integer = r + deltaR
        Dim newC As Integer = c + deltaC

        If newR < 0 Then newR = 0
        If newR > 8 Then newR = 8
        If newC < 0 Then newC = 0
        If newC > 8 Then newC = 8

        Cells(newR, newC).Focus()
        Cells(newR, newC).SelectAll()
    End Sub

    ' Clear all cells
    Public Sub ClearGrid()
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                Cells(r, c).Text = ""
            Next
        Next
        Cells(0, 0).Focus()
    End Sub

    ' Get grid values as Integer array for solver
    Public Function GetGrid() As Integer(,)
        Dim grid(8, 8) As Integer
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                If Integer.TryParse(Cells(r, c).Text, grid(r, c)) = False Then
                    grid(r, c) = 0
                End If
            Next
        Next
        Return grid
    End Function

    ' Update the grid with array values
    Public Sub SetGrid(grid As Integer(,))
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                Cells(r, c).Text = If(grid(r, c) = 0, "", grid(r, c).ToString())
            Next
        Next
    End Sub

    ' Set every cell to readonly / enabled
    Public Sub SetReadOnly(_ReadOnly As Boolean)
        For r As Integer = 0 To 8
            For c As Integer = 0 To 8
                Cells(r, c).ReadOnly = _ReadOnly
            Next
        Next
    End Sub
End Class