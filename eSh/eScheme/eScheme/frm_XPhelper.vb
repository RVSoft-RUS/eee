Public Class frm_XPhelper
	Public XPname As String
	Public pins As Integer

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		XPname = TextBox1.Text
	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		pins = ComboBox1.SelectedIndex + 1
	End Sub

	Private Sub Frm_XPhelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ComboBox1.SelectedIndex = 0
	End Sub

	Private Sub Frm_XPhelper_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, Label1.MouseClick
		If Form1.stopAtRMClick And e.Button = MouseButtons.Right Then
			Form1.Mode = ""
			Me.Visible = False
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.CheckBox2.Checked = True
			Form1.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Frm_XPhelper_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp, ComboBox1.KeyUp, TextBox1.KeyUp
		If e.KeyCode = Keys.Escape Then
			Form1.Mode = ""
			Me.Visible = False
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.CheckBox2.Checked = True
			Form1.Cursor = Cursors.Default
		End If
		If e.KeyCode = Keys.R Then
			If Form1.Mode = "eXP1" Then
				Form1.Mode = "eXP2"
			ElseIf Form1.Mode = "eXP2" Then
				Form1.Mode = "eXP1"
			End If
		End If
		e.Handled = True
	End Sub
End Class