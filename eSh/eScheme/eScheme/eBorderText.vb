Imports eScheme

Public Class eBorderText
    Implements IConnectable, IMovable
    Public p1 As Point
    Public p2 As Point
    Public txt As String
    Public num As Integer
    Dim h11, h12, h2 As hLine
    Dim v1, v2 As vLine
    Public Ctrl As Boolean = False

    Sub New(p01 As Point, p02 As Point, t As String, n As Integer)
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
        Label1.Font = New Font(Form1.fnt.Families(0), 9, FontStyle.Italic)
        TextBox1.Font = New Font(Form1.fnt.Families(0), 9, FontStyle.Italic)
        num = n
        txt = t
        Label1.Text = txt
        Label1.BackColor = Form1.bordColor
        p1 = p01
        p2 = p02
        Dim X1 As Integer = Math.Min(p1.X, p2.X)
        Dim Y1 As Integer = Math.Min(p1.Y, p2.Y)
        Dim X2 As Integer = Math.Max(p1.X, p2.X)
        Dim Y2 As Integer = Math.Max(p1.Y, p2.Y)
        Me.Location = New Point(X1 + 10, Y1 - 6)
        h11 = New hLine(X1 / 3, X1 / 3 + 4, Y1 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h11)
        h12 = New hLine((X1 + Me.Width) / 3, X2 / 3, Y1 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h12)
        h2 = New hLine(X1 / 3, X2 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h2)
        v1 = New vLine(X1 / 3, Y1 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(v1)
        v2 = New vLine(X2 / 3, Y1 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(v2)

    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        h11.Dispose()
        h12.Dispose()
        h2.Dispose()
        v1.Dispose()
        v2.Dispose()
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eBText",
            p1,
            p2,
            txt,
            num
        }
        Return save
    End Function

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Throw New NotImplementedException()
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Throw New NotImplementedException()
    End Function

    Private Sub Label1_MouseClick(sender As Object, e As MouseEventArgs) Handles Label1.MouseClick
        If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
            Form1.Mode = ""
            Form1.GroupBox1.Visible = True
            Form1.CheckBox2.Visible = True
            Form1.CheckBox2.Checked = True
            Form1.Cursor = Cursors.Default
            Exit Sub
        End If
        If Form1.Mode = "Move" Then
            Form1.moveObject = Me
            Form1.Cursor = Cursors.SizeAll
            Form1.moveXstart = Form1.rx
            Form1.moveYstart = Form1.ry
            Form1.Mode = "MoveMe"
        End If
        If Form1.Mode = "" Then
            TextBox1.Width = Label1.Width
            TextBox1.Text = Label1.Text
            TextBox1.Visible = True
            TextBox1.SelectAll()
            TextBox1.Focus()
        End If

        If Form1.Mode = "Delete" Then
            Form1.Delete(num)
        End If
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.Visible = False
        Form1.Mode = ""
        Form1.Cursor = Cursors.Default
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp

        If e.KeyCode = Keys.Escape Then
            TextBox1.Visible = False
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox1.Visible = False
            Label1.Text = TextBox1.Text
            txt = Label1.Text
            Form1.Mode = ""
            Form1.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Label1_Resize(sender As Object, e As EventArgs) Handles Label1.Resize
        Me.Width = Label1.Width
        Me.Height = Label1.Height

        Dim X1 As Integer = Math.Min(p1.X, p2.X)
        Dim Y1 As Integer = Math.Min(p1.Y, p2.Y)
        Dim X2 As Integer = Math.Max(p1.X, p2.X)
        If Not IsNothing(h11) Then
            h12.Dispose()
            h12 = New hLine((X1 + Me.Width) / 3, X2 / 3, Y1 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
            Form1.Controls.Add(h12)
        End If
    End Sub

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        If Form1.Mode = "Delete" Then
            Label1.Cursor = Form1.del_cur
        Else
            Label1.Cursor = Cursors.Hand
        End If
    End Sub

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        p1.X += dX
        p1.Y += dY
        If Not Ctrl Then
            p2.X += dX
            p2.Y += dY
        End If
        h11.Dispose()
        h12.Dispose()
        h2.Dispose()
        v1.Dispose()
        v2.Dispose()
        Dim X1 As Integer = Math.Min(p1.X, p2.X)
        Dim Y1 As Integer = Math.Min(p1.Y, p2.Y)
        Dim X2 As Integer = Math.Max(p1.X, p2.X)
        Dim Y2 As Integer = Math.Max(p1.Y, p2.Y)
        Me.Location = New Point(X1 + 10, Y1 - 6)
        h11 = New hLine(X1 / 3, X1 / 3 + 4, Y1 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h11)
        h12 = New hLine((X1 + Me.Width) / 3, X2 / 3, Y1 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h12)
        h2 = New hLine(X1 / 3, X2 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(h2)
        v1 = New vLine(X1 / 3, Y1 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(v1)
        v2 = New vLine(X2 / 3, Y1 / 3, Y2 / 3, 1) With {
            .BackColor = Form1.bordColor
        }
        Form1.Controls.Add(v2)
        Form1.NeedSave = True
        Return True
    End Function

    Public Function GetX() As Integer Implements IMovable.GetX
        Throw New NotImplementedException()
    End Function

    Public Function GetY() As Integer Implements IMovable.GetY
        Throw New NotImplementedException()
    End Function

    Public Sub MoveOK() Implements IMovable.MoveOK
        Throw New NotImplementedException()
    End Sub

    Private Sub Label1_KeyDown(sender As Object, e As KeyEventArgs) Handles Label1.KeyDown
        Ctrl = e.Control
    End Sub

    Private Sub Label1_KeyUp(sender As Object, e As KeyEventArgs) Handles Label1.KeyUp
        Ctrl = e.Control
    End Sub
End Class
