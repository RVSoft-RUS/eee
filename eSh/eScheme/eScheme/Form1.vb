Imports System.IO
Imports System.Runtime.Serialization

Public Class Form1
    Public rx As Integer, ry As Integer 'округленные до 20 координаты точки
    Dim zx, zy As Integer 'Для предотвращения мерцания линий при MouseMove
    Public Mode As String = "" 'Текущий режим - показывает состояние, что делаем. Пусто - ничего не делаем
    Public Elements As New ArrayList
    Public FileName As String
    Public NeedSave As Boolean = False 'Были изменения, нужно сохранять
    Dim a As New ArrayList ' Все линии для форматки
    Public f As New eFormat
    Dim fnt As System.Drawing.Text.PrivateFontCollection = New System.Drawing.Text.PrivateFontCollection()

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        GroupBox1.Visible = CheckBox2.Checked
        If CheckBox2.Checked Then
            ToolTip1.SetToolTip(CheckBox2, "Скрыть панель")
        Else
            ToolTip1.SetToolTip(CheckBox2, "Показать панель")
        End If
    End Sub

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try

            fnt.AddFontFile("resourses\gost.ttf")
			lblKF_A4.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblIzm.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lbliList.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblNdoc.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblPodp.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblDate.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblList.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
            lblListov.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblLit.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblMashtab.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblMassa.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblNcontr.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblProv.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblRazrab.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblSogl.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblTcontr.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblUtv.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			txtList.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.list = txtList.Text
			txtListov.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.listov = txtListov.Text
			txtMashtab.Font = New Font(fnt.Families(0), 14, FontStyle.Italic)
			f.mashtab = txtMashtab.Text
			txtMassa.Font = New Font(fnt.Families(0), 14, FontStyle.Italic)
			f.massa = txtMassa.Text
			Dim fs As New FontStyle
			fs = FontStyle.Italic Or FontStyle.Bold
			txtName.Font = New Font(fnt.Families(0), 14, fs)
			f.name = txtName.Text
			txtNcontr.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.nkontr = txtNcontr.Text
			txtNumber.Font = New Font(fnt.Families(0), 14, fs)
			f.number = txtNumber.Text
			txtOrg1.Font = New Font(fnt.Families(0), 14, fs)
			f.org1 = txtOrg1.Text
			txtOrg2.Font = New Font(fnt.Families(0), 14, fs)
			f.org2 = txtOrg2.Text
			txtProv.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.prov = txtProv.Text
			txtRazrab.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.razrab = txtRazrab.Text
			txtSogl.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.sogl = txtSogl.Text
			txtTcontr.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.tkontr = txtTcontr.Text
			txtType.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.type = txtType.Text
			txtUtv.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			f.utv = txtUtv.Text
		Catch ex As Exception
			MsgBox("Не найден шрифт GOST Type A в дипектории \resourses." + vbCrLf + "Для нормальной работы программы скопируйте файл gost.ttf в указанный каталог", vbCritical, "Fatal")
		End Try

		HideFormatText()
	End Sub

	Private Sub Label_A4_Click(sender As Object, e As EventArgs) Handles Label_A4.Click
        Me.Enabled = False
        Form2.Visible = True
    End Sub

    Private Sub HideFormatText()
        txtList.Visible = False
        txtListov.Visible = False
        txtMashtab.Visible = False
        txtMassa.Visible = False
        txtName.Visible = False
        txtNcontr.Visible = False
        txtNumber.Visible = False
        txtOrg1.Visible = False
        txtOrg2.Visible = False
        txtProv.Visible = False
        txtRazrab.Visible = False
        txtSogl.Visible = False
        txtTcontr.Visible = False
        txtType.Visible = False
        txtUtv.Visible = False
        pb1.Visible = False
        pb2.Visible = False
        pb3.Visible = False
        pb4.Visible = False
        pb5.Visible = False
        pb6.Visible = False
		pb7.Visible = False
        lblIzm.Visible = False
        lbliList.Visible = False
        lblNdoc.Visible = False
        lblPodp.Visible = False
        lblDate.Visible = False
        lblKF_A4.Visible = False
        lblList.Visible = False
        lblListov.Visible = False
        lblLit.Visible = False
        lblMashtab.Visible = False
        lblMassa.Visible = False
        lblNcontr.Visible = False
        lblProv.Visible = False
        lblRazrab.Visible = False
        lblSogl.Visible = False
        lblTcontr.Visible = False
        lblUtv.Visible = False
    End Sub

    Private Sub ShowFormatA4_1()
        txtList.Visible = True
        txtListov.Visible = True
        txtMashtab.Visible = True
        txtMassa.Visible = True
        txtName.Visible = True
        txtNcontr.Visible = True
        txtNumber.Visible = True
        txtOrg1.Visible = True
        txtOrg2.Visible = True
        txtProv.Visible = True
        txtRazrab.Visible = True
        txtSogl.Visible = True
        txtTcontr.Visible = True
        txtType.Visible = True
        txtUtv.Visible = True
        pb1.Visible = True
        pb2.Visible = True
        pb3.Visible = True
        pb4.Visible = True
        pb5.Visible = True
        pb6.Visible = True
		pb7.Visible = True
        lblIzm.Visible = True
        lbliList.Visible = True
        lblNdoc.Visible = True
        lblPodp.Visible = True
        lblDate.Visible = True
        lblKF_A4.Visible = True
        lblList.Visible = True
        lblListov.Visible = True
        lblLit.Visible = True
        lblMashtab.Visible = True
        lblMassa.Visible = True
        lblNcontr.Visible = True
        lblProv.Visible = True
        lblRazrab.Visible = True
        lblSogl.Visible = True
        lblTcontr.Visible = True
        lblUtv.Visible = True
    End Sub

    Public Sub CreateFormat()
        If a.Count > 0 Then
            'форматка уже есть, спросить надо ли переделать?
            Dim a As Microsoft.VisualBasic.MsgBoxResult
            a = MsgBox("Изменить формат?", MsgBoxStyle.YesNo, "Предупреждение")
            If a <> vbYes Then
                Exit Sub
            End If
        End If
        Dim theLine As Removable
        For i = 0 To a.Count - 1
            theLine = a(i)
            theLine.Dispose()
        Next
        a.Clear()
        HideFormatText()
        'f всегда присутствует и хранит последние данные
        Dim X_ As Integer = 14, Y_ As Integer = 12 'Смещение рамки от краев
        Dim aLine As hLine, bLine As vLine
        If f.format = "A41" Then
            '***********************************************
            aLine = New hLine(X_ - 12, X_ + 197, Y_ + 0, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 0, X_ + 82, Y_ + 14, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 60, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 120, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25 + 25, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25 + 25 + 35, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 197, Y_ + 287, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            bLine = New vLine(X_ - 12, Y_ + 0, Y_ + 120, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 12, Y_ + 142, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 7, Y_ + 142, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 7, Y_ + 0, Y_ + 120, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 0, Y_ + 0, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 82, Y_ + 0, Y_ + 14, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 197, Y_ + 0, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            'Код общей рамки А4
            'Основная надпись А41
            Dim t As Integer = 1
            For i = 5 To 50 Step 5
                If i = 30 Or i = 35 Then
                    t = 2
                Else
                    t = 1
                End If
                aLine = New hLine(X_ - 0, X_ + 65, Y_ + 287 - i, t)
                Me.Controls.Add(aLine)
                a.Add(aLine)
            Next
            aLine = New hLine(X_ + 65, X_ + 197, Y_ + 287 - 15, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ + 65, X_ + 197, Y_ + 287 - 40, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ + 0, X_ + 197, Y_ + 287 - 55, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            bLine = New vLine(X_ + 7, Y_ + 287 - 55, Y_ + 287 - 30, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 17, Y_ + 287 - 55, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 40, Y_ + 287 - 55, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 55, Y_ + 287 - 55, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 65, Y_ + 287 - 55, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 135, Y_ + 287 - 40, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 140, Y_ + 287 - 35, Y_ + 287 - 20, 1)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 145, Y_ + 287 - 35, Y_ + 287 - 20, 1)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 150, Y_ + 287 - 40, Y_ + 287 - 20, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 167, Y_ + 287 - 40, Y_ + 287 - 20, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 155, Y_ + 287 - 20, Y_ + 287 - 15, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            aLine = New hLine(X_ + 135, X_ + 197, Y_ + 287 - 35, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ + 135, X_ + 197, Y_ + 287 - 20, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            ShowFormatA4_1()
        End If
        If f.format = "A42" Then
            '***********************************************
            aLine = New hLine(X_ - 12, X_ + 197, Y_ + 0, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 0, X_ + 82, Y_ + 14, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 60, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 120, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25 + 25, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 0, Y_ + 142 + 35 + 25 + 25 + 35, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 12, X_ + 197, Y_ + 287, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            bLine = New vLine(X_ - 12, Y_ + 0, Y_ + 120, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 12, Y_ + 142, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 7, Y_ + 142, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 7, Y_ + 0, Y_ + 120, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ - 0, Y_ + 0, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 82, Y_ + 0, Y_ + 14, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            bLine = New vLine(X_ + 197, Y_ + 0, Y_ + 287, 2)
            Me.Controls.Add(bLine)
            a.Add(bLine)
            'Код общей рамки А4
            'Основная надпись А41
            aLine = New hLine(X_ - 0, X_ + 65, Y_ + 287 - 5, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 0, X_ + 65, Y_ + 287 - 10, 1)
            Me.Controls.Add(aLine)
            a.Add(aLine)
            aLine = New hLine(X_ - 0, X_ + 197, Y_ + 287 - 15, 2)
            Me.Controls.Add(aLine)
            a.Add(aLine)

            'aLine = New hLine(X_ + 65, X_ + 197, Y_ + 287 - 15, 2)
            'Me.Controls.Add(aLine)
            'a.Add(aLine)
            'aLine = New hLine(X_ + 65, X_ + 197, Y_ + 287 - 40, 2)
            'Me.Controls.Add(aLine)
            'a.Add(aLine)
            'aLine = New hLine(X_ + 0, X_ + 197, Y_ + 287 - 55, 2)
            'Me.Controls.Add(aLine)
            'a.Add(aLine)
            'bLine = New vLine(X_ + 7, Y_ + 287 - 55, Y_ + 287 - 30, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 17, Y_ + 287 - 55, Y_ + 287, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 40, Y_ + 287 - 55, Y_ + 287, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 55, Y_ + 287 - 55, Y_ + 287, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 65, Y_ + 287 - 55, Y_ + 287, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 135, Y_ + 287 - 40, Y_ + 287, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 140, Y_ + 287 - 35, Y_ + 287 - 20, 1)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 145, Y_ + 287 - 35, Y_ + 287 - 20, 1)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 150, Y_ + 287 - 40, Y_ + 287 - 20, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 167, Y_ + 287 - 40, Y_ + 287 - 20, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'bLine = New vLine(X_ + 155, Y_ + 287 - 20, Y_ + 287 - 15, 2)
            'Me.Controls.Add(bLine)
            'a.Add(bLine)
            'aLine = New hLine(X_ + 135, X_ + 197, Y_ + 287 - 35, 2)
            'Me.Controls.Add(aLine)
            'a.Add(aLine)
            'aLine = New hLine(X_ + 135, X_ + 197, Y_ + 287 - 20, 2)
            'Me.Controls.Add(aLine)
            'a.Add(aLine)
            'ShowFormatA4_1()
        End If

    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Math.Abs(zx - e.X) + Math.Abs(zy - e.Y) < 4 Then
            Exit Sub 'Если почти не сдвинулось - то выход
        End If
        rx = CInt(Math.Round(e.X / 20))
        rx = rx * 20
        ry = CInt(Math.Round(e.Y / 20))
        ry = ry * 20
        zx = e.X
        zy = e.Y
        Dim G As Graphics = Me.CreateGraphics
        If e.Button = MouseButtons.Left Then

        Else
            G.Clear(Me.BackColor)
        End If
        If e.Button = MouseButtons.Right Then

        Else
            G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim Pn As New Pen(Color.LightGreen, 1) With {
                .DashStyle = Drawing2D.DashStyle.Dash
            }
            G.DrawLine(Pn, 0, ry, 3000, ry)
            G.DrawLine(Pn, rx, 0, rx, 3000)
            G.Dispose()
            ToolTip1.SetToolTip(Me, CStr(rx) + ", " + CStr(ry))
        End If

    End Sub

    Private Sub PictureBox_Point_Click(sender As Object, e As EventArgs) Handles PictureBox_Point.Click
        Mode = "newPoint"
        GroupBox1.Visible = False
        CheckBox2.Visible = False
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If Mode = "newPoint" Then
            Dim eComp As New EComponent With {
                .X = rx,
                .Y = ry,
                .aType = "ePoint",
                .numInArray = Elements.Count
            }
            Elements.Add(eComp)
            Dim p As New EPoint(rx, ry) With {
                .num = eComp.numInArray
            }
            Me.Controls.Add(p)
            eComp.component = p

            Mode = ""
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            NeedSave = True
        End If

    End Sub

    Private Sub СохранитьToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles СохранитьToolStripMenuItem1.Click
        FileSave()
    End Sub
    Private Sub СохранитьКакToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles СохранитькакToolStripMenuItem.Click
        FileName = ""
        FileSave()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim eComp As EComponent
        For i = 0 To Elements.Count - 1
            eComp = Elements(i)
            eComp.component.Dispose()
        Next
        Elements.Clear()
        Dim theLine As Removable
        For i = 0 To a.Count - 1
            theLine = a(i)
            theLine.Dispose()
        Next
        a.Clear()
        HideFormatText()
    End Sub

    Private Sub FileSave()
        If Elements.Count = 0 Then Exit Sub
        If FileName = "" Then
            Dim a As Integer
            SaveFileDialog1.FileName = FileName
            a = SaveFileDialog1.ShowDialog()
            If a = 2 Then Exit Sub
            FileName = SaveFileDialog1.FileName
        End If
        Try
            'не сериализуется
            Dim fStream As New FileStream(FileName, FileMode.Create, FileAccess.Write)
            Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
            myBinaryFormatter.Serialize(fStream, Elements)
            fStream.Close()
            NeedSave = False
        Catch ex As Exception
            MsgBox("Не удалось сохранить файл " + CStr(FileName) + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Предупреждение")
        End Try
    End Sub

    Private Sub printReversNumber()
        Try
            Dim fs As New FontStyle
            fs = FontStyle.Italic Or FontStyle.Bold
            Dim font As New Font(fnt.Families(0), 14, fs)
            Dim g As Graphics = pbNumber.CreateGraphics
            g.Clear(Me.BackColor)
            g.TranslateTransform(240, 22)
            g.RotateTransform(180)
            g.DrawString(f.number, font, New SolidBrush(Color.Black), New PointF(0, 0))
            g.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txt_TextChanged(sender As Object, e As EventArgs) Handles txtNumber.TextChanged, txtList.TextChanged, txtListov.TextChanged,
			txtMashtab.TextChanged, txtMassa.TextChanged, txtName.TextChanged, txtNcontr.TextChanged, txtOrg1.TextChanged,
			txtOrg2.TextChanged, txtProv.TextChanged, txtRazrab.TextChanged, txtSogl.TextChanged, txtTcontr.TextChanged,
			txtType.TextChanged, txtUtv.TextChanged
		If sender.Equals(txtNumber) Then
            f.number = txtNumber.Text
            printReversNumber()
        End If
		If sender.Equals(txtList) Then
			f.list = txtList.Text
		End If
		If sender.Equals(txtListov) Then
			f.listov = txtListov.Text
		End If
		If sender.Equals(txtMashtab) Then
			f.mashtab = txtMashtab.Text
		End If
		If sender.Equals(txtMassa) Then
			f.massa = txtMassa.Text
		End If
		If sender.Equals(txtName) Then
			f.name = txtName.Text
		End If
		If sender.Equals(txtNcontr) Then
			f.nkontr = txtNcontr.Text
		End If
		If sender.Equals(txtOrg1) Then
			f.org1 = txtOrg1.Text
		End If
		If sender.Equals(txtOrg2) Then
			f.org2 = txtOrg2.Text
		End If
		If sender.Equals(txtProv) Then
			f.prov = txtProv.Text
		End If
		If sender.Equals(txtRazrab) Then
			f.razrab = txtRazrab.Text
		End If
		If sender.Equals(txtSogl) Then
			f.sogl = txtSogl.Text
		End If
		If sender.Equals(txtTcontr) Then
			f.tkontr = txtTcontr.Text
		End If
		If sender.Equals(txtType) Then
			f.type = txtType.Text
		End If
		If sender.Equals(txtUtv) Then
			f.utv = txtUtv.Text
		End If
	End Sub

    Private Sub pbNumber_Click(sender As Object, e As EventArgs) Handles pbNumber.Click

    End Sub

    Private Sub ОткрытьToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ОткрытьToolStripMenuItem1.Click
        Dim a As Microsoft.VisualBasic.MsgBoxResult
        'If AddFrm Then GoTo StartFile
        'If OpenAtStart Then
        '	OpenAtStart = False
        '	GoTo StartFile
        'End If
        If NeedSave Then
            a = MsgBox("Сохранить изменения в схеме?", MsgBoxStyle.YesNo, "Предупреждение")
            If a = vbYes Then
                'сохранение
                FileSave()
            End If
        End If
        OpenFileDialog1.FileName = FileName
        Dim b As System.Windows.Forms.DialogResult
        b = OpenFileDialog1.ShowDialog()
        If b = vbCancel Then Exit Sub
        FileName = OpenFileDialog1.FileName
        If FileName = "" Then Exit Sub

        Dim eComp As EComponent
        For i = 0 To Elements.Count - 1
            eComp = Elements(i)
            eComp.component.Dispose()
        Next
        Elements.Clear()

        Try
            Dim fStream As New FileStream(FileName, FileMode.Open, FileAccess.Read)
            Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
            Elements = CType(myBinaryFormatter.Deserialize(fStream), ArrayList)
            fStream.Close()
        Catch ex As Exception
            MsgBox("Ошибка при чтении файла " + CStr(FileName) + vbCrLf + "Файл возможно поврежден." + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Предупреждение")
            Exit Sub
        End Try
        'Отображение прочитанного массива
        'Dim eComp As New eComponent
        For i = 0 To Elements.Count - 1
            eComp = Elements(i)
            'ePoint
            If eComp.aType = "ePoint" Then
                Dim p As New EPoint(eComp.X, eComp.Y) With {
                    .num = i
                }
                Me.Controls.Add(p)
                eComp.component = p
            End If
        Next
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F12 And e.Shift Then
            Dim bm As New Bitmap(653, 944, Drawing.Imaging.PixelFormat.Format32bppArgb)
            Dim Gf As Graphics = Me.CreateGraphics
            Gf.Clear(Me.BackColor)
            Gf.Dispose()
            Me.DrawToBitmap(bm, New Rectangle(New Point(0, 0), New Point(653, 944)))

            Dim img As New Bitmap(643, 888, Drawing.Imaging.PixelFormat.Format32bppArgb)
            Using g As Graphics = Graphics.FromImage(img)
				g.DrawImage(bm, New Rectangle(New Point(), img.Size), New Rectangle(New Point(10, 56), New Point(643, 888)), GraphicsUnit.Pixel)
			End Using

			img.Save(f.number & "_лист" & f.list & ".png", Drawing.Imaging.ImageFormat.Png)
			zx = 0
			zy = 0
		End If
    End Sub

    Private Sub pbNumber_Paint(sender As Object, e As PaintEventArgs) Handles pbNumber.Paint
        Try
            Dim fs As New FontStyle
            fs = FontStyle.Italic Or FontStyle.Bold
            Dim font As New Font(fnt.Families(0), 14, fs)
			'Dim g As Graphics = pbNumber.CreateGraphics
			e.Graphics.Clear(Me.BackColor)
			e.Graphics.TranslateTransform(240, 22)
			e.Graphics.RotateTransform(180)
			e.Graphics.DrawString(f.number, font, New SolidBrush(Color.Black), New PointF(0, 0))
			'g.Dispose()




		Catch ex As Exception

        End Try
    End Sub
End Class
