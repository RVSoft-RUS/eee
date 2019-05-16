Imports System.ComponentModel

Public Class DialogForm
	Public cnt As ISetValue
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then TextBox1.Text = "0"
        Try
            Dim value As Single = CSng(TextBox1.Text)
            cnt.SetValue(value)
            Me.Close()
        Catch ex As Exception
            MsgBox("Некорректное значение")
        End Try
    End Sub

    Public Sub OnView(txt As String, cnt_ As ISetValue, lastText As String)
        Label1.Text = txt
        TextBox1.Text = lastText
        TextBox1.SelectAll()
        TextBox1.Focus()
        cnt = cnt_
    End Sub

    Private Sub DialogForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		Form1.Enabled = True
	End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        'If Not Char.IsDigit(e.KeyChar) Then
        '    e.Handled = True
        'End If
        Dim temp As Single
        e.Handled = Not Single.TryParse(TextBox1.Text & e.KeyChar, temp)
    End Sub
End Class