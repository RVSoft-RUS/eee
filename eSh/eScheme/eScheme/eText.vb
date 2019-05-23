Imports eScheme

Public Class eText
    Implements IConnectable, IMovable
    Public X As Integer
    Public Y As Integer
    Public txt As String
    Public num As Integer

    Sub New(x_ As Integer, y_ As Integer, t As String, n As Integer)
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
        Label1.Font = New Font(Form1.fnt.Families(0), 10, FontStyle.Italic)
        TextBox1.Font = New Font(Form1.fnt.Families(0), 10, FontStyle.Italic)
        Me.Height = Label1.Height
        num = n
        txt = t
        Label1.Text = txt
        Label1.ForeColor = Form1.txtColor
        X = x_
        Y = y_
        Me.Location = New Point(X, Y)
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub
    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Throw New NotImplementedException()
    End Function
    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Throw New NotImplementedException()
    End Function

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eText",
            X,
            Y,
            txt,
            num
        }
        Return save
    End Function

    Private Sub Label1_MouseClick(sender As Object, e As MouseEventArgs) Handles Label1.MouseClick
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
        If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
            Form1.Mode = ""
            Form1.GroupBox1.Visible = True
            Form1.CheckBox2.Visible = True
            Form1.CheckBox2.Checked = True
            Form1.Cursor = Cursors.Default
        End If
        If Form1.Mode = "Move" Then
            Form1.moveObject = Me
            Form1.Cursor = Cursors.SizeAll
            Form1.moveXstart = Form1.rx
            Form1.moveYstart = Form1.ry
            Form1.Mode = "MoveMe"
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
    End Sub

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        If Form1.Mode = "Delete" Then
            Label1.Cursor = Form1.del_cur
        Else
            Label1.Cursor = Cursors.Hand
        End If
    End Sub

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        X += dX
        Y += dY
        Me.Location = New Point(X, Y)
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
End Class
