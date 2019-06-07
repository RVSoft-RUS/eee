Public Class FormOptions
    Private Sub FormOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelNeu.BackColor = Form1.color0
        LabelMas.BackColor = Form1.colorM
        Label15.BackColor = Form1.color15
        Label30.BackColor = Form1.color30
        LabelTXT.BackColor = Form1.txtColor
        LabelComment.BackColor = Form1.commentColor
        LabelRam.BackColor = Form1.bordColor

        TextBoxUdef.Text = Form1.Udefault.ToString
        TextBoxResist.Text = Form1.Rdefault.ToString
        TextBoxLamp.Text = Form1.RLampdefault.ToString
        TextBoxNomPred.Text = Form1.FUSEdefault.ToString
        TextBoxRele.Text = Form1.RELEdefault.ToString

        CheckBox1.Checked = Form1.stopAtRMClick
        CheckBox3.Checked = Form1.openWithSize

    End Sub

    Private Sub TextBoxUdef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxUdef.KeyPress, TextBoxResist.KeyPress, TextBoxLamp.KeyPress, TextBoxNomPred.KeyPress, TextBoxRele.KeyPress
        Dim temp As Single
        e.Handled = Not Single.TryParse(sender.Text & e.KeyChar, temp)
    End Sub

    Private Sub LabelNeu_Click(sender As Object, e As EventArgs) Handles LabelNeu.Click, LabelMas.Click, Label15.Click, Label30.Click, LabelTXT.Click, LabelComment.Click, LabelRam.Click
        ColorDialog1.Color = sender.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then sender.BackColor = ColorDialog1.Color
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Form1.color0 = LabelNeu.BackColor
            Form1.colorM = LabelMas.BackColor
            Form1.color15 = Label15.BackColor
            Form1.color30 = Label30.BackColor
            Form1.txtColor = LabelTXT.BackColor
            Form1.commentColor = LabelComment.BackColor
            Form1.bordColor = LabelRam.BackColor

            Form1.stopAtRMClick = CheckBox1.Checked
            Form1.openWithSize = CheckBox3.Checked

            Form1.Udefault = CSng(TextBoxUdef.Text)
            Form1.Rdefault = CSng(TextBoxResist.Text)
            Form1.RLampdefault = CSng(TextBoxLamp.Text)
            Form1.FUSEdefault = CSng(TextBoxNomPred.Text)
            Form1.RELEdefault = CSng(TextBoxRele.Text)

            Me.Close()
        Catch ex As Exception
            MsgBox("Некорректные значения.")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("В разработке.")
    End Sub
End Class