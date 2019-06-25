Public Class FormLicense
	Dim R, G, B As Integer
    Dim Ri, Gi, Bi As Integer
    Dim minR, maxR, minG, maxG, minB, maxB As Integer

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim a As System.Windows.Forms.DialogResult
        a = OpenFileDialog1.ShowDialog()
        If a = 2 Then Exit Sub
        Form1.liFile = OpenFileDialog1.FileName
        Form1.lic.NewLicense(Form1.liFile)
        ShowLicense()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Dim rnd As New Random

    Sub ShowLicense()
        TextBox1.Text = Form1.lic.GetID
        Label_Name.Text = Form1.lic.GetName
        Label_Company.Text = Form1.lic.GetCompany
        TextBox4.Text = Form1.lic.GetContacts
        TextBox5.Text = Form1.lic.GetAbout
        Label_endDate.Text = Form1.lic.GetEndDate
        Dim lev As Integer = CInt(Form1.lic.GetLevel)
        Ri = 1
        Gi = 1
        Bi = -1
        R = 190
        G = 210
        B = 225
        minR = 180
        maxR = 200
        minG = 180
        maxG = 200
        minB = 180
        maxB = 200
        Select Case lev
            Case 0
                maxR = 250
                Me.Text = "Лицензия - отсутствует"
            Case 2
                maxG = 250
                Me.Text = "Лицензия - коммерческая " + Form1.lic.GetLicNumber.ToUpper
            Case 1
                maxB = 250
                Me.Text = "Лицензия - базовая " + Form1.lic.GetLicNumber.ToUpper
            Case 3
                maxB = 250
                Me.Text = "Лицензия - временная " + Form1.lic.GetLicNumber.ToUpper
        End Select
    End Sub

    Private Sub FormLicense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowLicense()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Dim k As Integer

		k = rnd.Next(1, 3)
		R += k * Ri
        If R > maxR Then
            R = maxR
            Ri = -1
        End If
        If R < 180 Then
            R = 180
            Ri = 1
        End If

        k = rnd.Next(1, 3)
		G += k * Gi
        If G > maxG Then
            G = maxG
            Gi = -1
        End If
        If G < 180 Then
            G = 180
            Gi = 1
        End If

        k = rnd.Next(1, 3)
        B += k * Bi
        If B > maxB Then
            B = maxB
            Bi = -1
        End If
        If B < 180 Then
			B = 180
			Bi = 1
		End If

		Me.BackColor = Color.FromArgb(R, G, B)
	End Sub
End Class