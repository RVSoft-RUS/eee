Public Class FormLicense
	Dim R, G, B As Integer
	Dim Ri, Gi, Bi As Integer

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Me.Close()
	End Sub

	Dim rnd As New Random

	Private Sub FormLicense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Ri = 1
		Gi = 1
		Bi = -1
		R = 190
		G = 210
		B = 225
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Dim k As Integer

		k = rnd.Next(1, 3)
		R += k * Ri
		If R > 220 Then
			R = 220
			Ri = -1
		End If
		If R < 190 Then
			R = 190
			Ri = 1
		End If

		k = rnd.Next(1, 3)
		G += k * Gi
		If G > 240 Then
			G = 240
			Gi = -1
		End If
		If G < 210 Then
			G = 210
			Gi = 1
		End If

		k = rnd.Next(1, 4)
		B += k * Bi
		If B > 255 Then
			B = 255
			Bi = -1
		End If
		If B < 180 Then
			B = 180
			Bi = 1
		End If

		Me.BackColor = Color.FromArgb(R, G, B)
	End Sub
End Class