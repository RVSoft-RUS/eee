Imports System.ComponentModel

Public Class DialogForm
	Public cnt As ISetValue
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If TextBox1.Text = "" Then TextBox1.Text = "0"
		Dim value As Integer = CInt(TextBox1.Text)
		cnt.SetValue(value)
		Me.Close()
	End Sub

	Public Sub OnView(txt As String, cnt_ As ISetValue)
		Label1.Text = txt
		cnt = cnt_
	End Sub

	Private Sub DialogForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		Form1.Enabled = True
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

	End Sub

	Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
		If Not Char.IsDigit(e.KeyChar) Then
			e.Handled = True
		End If
	End Sub
End Class