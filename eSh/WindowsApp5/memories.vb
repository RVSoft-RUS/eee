Imports System.ComponentModel

Public Class memories

    Private Sub memories_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form1.ini.h_2 = Me.Height
        Form1.ini.w_2 = Me.Width
        Dim p As New Point
        p = Me.Location
        Form1.ini.left2 = p.X
        Form1.ini.top2 = p.Y
    End Sub

    Private Sub memories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = Form1.ini.h_2
        Me.Width = Form1.ini.w_2
        Dim p As New Point
        p.X = Form1.ini.left2
        p.Y = Form1.ini.top2
        Me.Location = p
    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.SelectAll()
        If TextBox1.Text.Contains(".") Then
            Exit Sub
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            Dim i As Integer = ListBox1.SelectedIndex
            'If i < 0 Then i = 0
            For j = i + 1 To ListBox1.Items.Count - 1
                Dim str As String = ListBox1.Items.Item(j)
                If str.Contains(TextBox1.Text) Then
                    ListBox1.SelectedIndex = j
                    Exit Sub
                End If
            Next
        End If
    End Sub
End Class