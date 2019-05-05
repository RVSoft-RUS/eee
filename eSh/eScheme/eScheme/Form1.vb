﻿Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.Serialization

Public Class Form1
	Public color0 As Color = Color.Gray
	Public colorM As Color = Color.Black
	Public color15 As Color = Color.Red
	Public color30 As Color = Color.Purple
	Public rx As Integer, ry As Integer 'округленные до 20 координаты точки
	Dim zx, zy As Integer 'Для предотвращения мерцания линий при MouseMove
	Public Mode As String = "" 'Текущий режим - показывает состояние, что делаем. Пусто - ничего не делаем
	Public Elements As New ArrayList
	Public FileName As String
	Public NeedSave As Boolean = False 'Были изменения, нужно сохранять
	Dim a As New ArrayList ' Все линии для форматки
	Public f As New EFormat
	Public createConnect As EAddLinesAndPoints
	Public line_cur As Cursor
	Public point_cur As Cursor
	Public addPoint_cur As Cursor
	Public addLine_cur As Cursor
	Public del_cur As Cursor
	Public element_cur As Cursor
	Public gnd_cur As Cursor
	Dim OpenAtStart As Boolean = False
	'во время переключений pointsInProcess обнулять и добавлять номера точек, через который прошел сигнал
	'если точка при добавлении уже существует, значит зациклено
	Public pointsInProcess As New ArrayList
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
			Elements.Add(Nothing)
			fnt.AddFontFile(Application.StartupPath + "\resourses\gost.ttf")
			lblKF_A4.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
			lblKF_A3.Font = New Font(fnt.Families(0), 9, FontStyle.Italic)
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

		Try
			line_cur = New Cursor(Application.StartupPath + "\resourses\line.cur")
			point_cur = New Cursor(Application.StartupPath + "\resourses\point.cur")
			addLine_cur = New Cursor(Application.StartupPath + "\resourses\addLine.cur")
			del_cur = New Cursor(Application.StartupPath + "\resourses\del.cur")
			addPoint_cur = New Cursor(Application.StartupPath + "\resourses\addPoint.cur")
			element_cur = New Cursor(Application.StartupPath + "\resourses\element.cur")
			gnd_cur = New Cursor(Application.StartupPath + "\resourses\gnd.cur")
		Catch ex As Exception
			line_cur = Cursors.Default
			point_cur = Cursors.Cross
			addLine_cur = Cursors.Help
			del_cur = Cursors.No
			addPoint_cur = Cursors.NoMove2D
			element_cur = Cursors.Default
			gnd_cur = Cursors.Hand
		End Try

		Dim Proc() As Process
		'Определение полного имени текущего процесса.
		Dim ModuleName, ProcName As String
		ModuleName = Process.GetCurrentProcess.MainModule.ModuleName
		ProcName = System.IO.Path.GetFileNameWithoutExtension(ModuleName)
		'Находим все процессы с данным именем
		Proc = Process.GetProcessesByName(ProcName)
		'Если процесса такого нет то запускаем программу
		'Если процесс есть уже с таким именем то закрываем программу
		'Если вы хотите разрешить запуск 2 экзэмпляра приложения то измените Proc.Length > 1 на Proc.Length > 2
		If Proc.Length > 1 Then
			'Если лицензия не разрешает, то выход

		End If

		Try
			FileName = My.Application.CommandLineArgs.Item(0)
			If FileName <> "" Then
				OpenAtStart = True
				Me.Visible = True
				ОткрытьToolStripMenuItem1_Click(Me, e)
			End If
		Catch exс As Exception

		End Try

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
		pbNumber.Visible = False
		lblIzm.Visible = False
		lbliList.Visible = False
		lblNdoc.Visible = False
		lblPodp.Visible = False
		lblDate.Visible = False
		lblKF_A4.Visible = False
		lblKF_A3.Visible = False
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
		txtList.Location = New Point(485, 839)
		txtListov.Visible = True
		txtListov.Location = New Point(565, 839)
		txtMashtab.Visible = True
		txtMashtab.Location = New Point(561, 805)
		txtMassa.Visible = True
		txtMassa.Location = New Point(495, 805)
		txtName.Visible = True
		txtName.Location = New Point(263, 779)
		txtNcontr.Visible = True
		txtNcontr.Location = New Point(95, 869)
		txtNumber.Visible = True
		txtNumber.Location = New Point(348, 750)
		txtOrg1.Visible = True
		txtOrg1.Location = New Point(453, 855)
		txtOrg2.Visible = True
		txtOrg2.Location = New Point(453, 874)
		txtProv.Visible = True
		txtProv.Location = New Point(95, 824)
		txtRazrab.Visible = True
		txtRazrab.Location = New Point(95, 809)
		txtSogl.Visible = True
		txtSogl.Location = New Point(95, 839)
		txtTcontr.Visible = True
		txtTcontr.Location = New Point(95, 854)

		txtType.Visible = True
		txtType.Location = New Point(263, 832)
		txtUtv.Visible = True
		txtUtv.Location = New Point(95, 884)
		pb1.Visible = True
		pb2.Visible = True
		pb3.Visible = True
		pb4.Visible = True
		pb5.Visible = True
		pb6.Visible = True
		pb7.Visible = True
		pbNumber.Visible = True
		lblIzm.Visible = True
		lblIzm.Location = New Point(43, 794)
		lbliList.Visible = True
		lbliList.Location = New Point(64, 794)
		lblNdoc.Visible = True
		lblNdoc.Location = New Point(96, 794)
		lblPodp.Visible = True
		lblPodp.Location = New Point(166, 794)
		lblDate.Visible = True
		lblDate.Location = New Point(209, 794)
		lblKF_A4.Visible = True
		lblList.Visible = True
		lblList.Location = New Point(450, 839)
		lblListov.Visible = True
		lblListov.Location = New Point(520, 839)
		lblLit.Visible = True
		lblLit.Location = New Point(456, 779)
		lblMashtab.Visible = True
		lblMashtab.Location = New Point(546, 779)
		lblMassa.Visible = True
		lblMassa.Location = New Point(502, 779)
		lblNcontr.Visible = True
		lblNcontr.Location = New Point(45, 869)
		lblProv.Visible = True
		lblProv.Location = New Point(45, 824)
		lblRazrab.Visible = True
		lblRazrab.Location = New Point(45, 809)
		lblSogl.Visible = True
		lblSogl.Location = New Point(45, 839)
		lblTcontr.Visible = True
		lblTcontr.Location = New Point(45, 854)
		lblUtv.Visible = True
		lblUtv.Location = New Point(45, 884)
	End Sub

	Private Sub ShowFormatA4_2()
		txtList.Visible = True
		txtList.Location = New Point(576, 880)
		lblList.Visible = True
		lblList.Location = New Point(568, 860)
		txtNumber.Visible = True
		txtNumber.Location = New Point(320, 868)
		pb3.Visible = True
		pb4.Visible = True
		pb5.Visible = True
		pb6.Visible = True
		pb7.Visible = True
		pbNumber.Visible = True

		lblIzm.Location = New Point(43, 884)
		lbliList.Location = New Point(64, 884)
		lblNdoc.Location = New Point(96, 884)
		lblPodp.Location = New Point(166, 884)
		lblDate.Location = New Point(209, 884)
		lblIzm.Visible = True
		lbliList.Visible = True
		lblNdoc.Visible = True
		lblPodp.Visible = True
		lblDate.Visible = True

		lblKF_A4.Visible = True
	End Sub

	Private Sub ShowFormatA3_1()
		txtList.Visible = True
		txtList.Location = New Point(485 + 630, 839) 'x485 A4 =+630
		txtListov.Visible = True
		txtListov.Location = New Point(565 + 630, 839)
		txtMashtab.Visible = True
		txtMashtab.Location = New Point(561 + 630, 805)
		txtMassa.Visible = True
		txtMassa.Location = New Point(495 + 630, 805)
		txtName.Visible = True
		txtName.Location = New Point(263 + 630, 779)
		txtNcontr.Visible = True
		txtNcontr.Location = New Point(95 + 630, 869)
		txtNumber.Visible = True
		txtNumber.Location = New Point(348 + 630, 750)
		txtOrg1.Visible = True
		txtOrg1.Location = New Point(453 + 630, 855)
		txtOrg2.Visible = True
		txtOrg2.Location = New Point(453 + 630, 874)

		txtProv.Visible = True
		txtProv.Location = New Point(95 + 630, 824)
		txtRazrab.Visible = True
		txtRazrab.Location = New Point(95 + 630, 809)
		txtSogl.Visible = True
		txtSogl.Location = New Point(95 + 630, 839)
		txtTcontr.Visible = True
		txtTcontr.Location = New Point(95 + 630, 854)
		txtType.Visible = True
		txtType.Location = New Point(263 + 630, 832)
		txtUtv.Visible = True
		txtUtv.Location = New Point(95 + 630, 884)
		pb1.Visible = True
		pb2.Visible = True
		pb3.Visible = True
		pb4.Visible = True
		pb5.Visible = True
		pb6.Visible = True
		pb7.Visible = True
		pbNumber.Visible = True
		lblIzm.Visible = True
		lblIzm.Location = New Point(43 + 630, 794)
		lbliList.Visible = True
		lbliList.Location = New Point(64 + 630, 794)
		lblNdoc.Visible = True
		lblNdoc.Location = New Point(96 + 630, 794)
		lblPodp.Visible = True
		lblPodp.Location = New Point(166 + 630, 794)
		lblDate.Visible = True
		lblDate.Location = New Point(209 + 630, 794)
		lblKF_A3.Visible = True
		lblList.Visible = True
		lblList.Location = New Point(450 + 630, 839)
		lblListov.Visible = True
		lblListov.Location = New Point(520 + 630, 839)
		lblLit.Visible = True
		lblMashtab.Visible = True
		lblMassa.Visible = True
		lblLit.Location = New Point(456 + 630, 779)
		lblMashtab.Location = New Point(546 + 630, 779)
		lblMassa.Location = New Point(502 + 630, 779)
		lblNcontr.Visible = True
		lblNcontr.Location = New Point(45 + 630, 869)
		lblProv.Visible = True
		lblProv.Location = New Point(45 + 630, 824)
		lblRazrab.Visible = True
		lblRazrab.Location = New Point(45 + 630, 809)
		lblSogl.Visible = True
		lblSogl.Location = New Point(45 + 630, 839)
		lblTcontr.Visible = True
		lblTcontr.Location = New Point(45 + 630, 854)
		lblUtv.Visible = True
		lblUtv.Location = New Point(45 + 630, 884)
	End Sub

	Private Sub ShowFormatA3_2()
		txtList.Visible = True
		txtList.Location = New Point(576 + 630, 880)
		lblList.Visible = True
		lblList.Location = New Point(568 + 630, 860)
		txtNumber.Visible = True
		txtNumber.Location = New Point(320 + 630, 868)
		pb3.Visible = True
		pb4.Visible = True
		pb5.Visible = True
		pb6.Visible = True
		pb7.Visible = True
		pbNumber.Visible = True

		lblIzm.Location = New Point(43 + 630, 884)
		lbliList.Location = New Point(64 + 630, 884)
		lblNdoc.Location = New Point(96 + 630, 884)
		lblPodp.Location = New Point(166 + 630, 884)
		lblDate.Location = New Point(209 + 630, 884)
		lblIzm.Visible = True
		lbliList.Visible = True
		lblNdoc.Visible = True
		lblPodp.Visible = True
		lblDate.Visible = True

		lblKF_A3.Visible = True
	End Sub


	Public Sub CreateFormat()
		If a.Count > 0 And f.format <> "" Then
			'форматка уже есть, спросить надо ли переделать?
			Dim a As Microsoft.VisualBasic.MsgBoxResult
			a = MsgBox("Изменить формат?", MsgBoxStyle.YesNo, "Предупреждение")
			If a <> vbYes Then
				Exit Sub
			End If
		End If
		Dim theLine As IRemovable
		For i = 0 To a.Count - 1
			theLine = a(i)
			theLine.Dispose()
		Next
		a.Clear()
		HideFormatText()
		'f всегда присутствует и хранит последние данные
		Dim X_ As Integer = 14, Y_ As Integer = 12 'Смещение рамки от краев
		Dim aLine As hLine, bLine As vLine
		txtNumber.Text = f.number
		txtList.Text = f.list
		txtListov.Text = f.listov
		txtMashtab.Text = f.mashtab
		txtMassa.Text = f.massa
		txtName.Text = f.name
		txtNcontr.Text = f.nkontr
		txtOrg1.Text = f.org1
		txtOrg2.Text = f.org2
		txtProv.Text = f.prov
		txtRazrab.Text = f.razrab
		txtSogl.Text = f.sogl
		txtTcontr.Text = f.tkontr
		txtType.Text = f.type
		txtUtv.Text = f.utv

		If f.format = "A41" Then
			'***********************************************
			aLine = New hLine(X_ - 12, X_ + 185, Y_ + 0, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0, X_ + 70, Y_ + 14, 2)
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
			aLine = New hLine(X_ - 12, X_ + 185, Y_ + 287, 2)
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
			bLine = New vLine(X_ + 70, Y_ + 0, Y_ + 14, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 185, Y_ + 0, Y_ + 287, 2)
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
			aLine = New hLine(X_ + 65, X_ + 185, Y_ + 287 - 15, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 65, X_ + 185, Y_ + 287 - 40, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 0, X_ + 185, Y_ + 287 - 55, 2)
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
			aLine = New hLine(X_ + 135, X_ + 185, Y_ + 287 - 35, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 135, X_ + 185, Y_ + 287 - 20, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			ShowFormatA4_1()
		End If
		If f.format = "A42" Then
			'***********************************************
			aLine = New hLine(X_ - 0, X_ + 185, Y_ + 0, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0, X_ + 70, Y_ + 14, 2)
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
			aLine = New hLine(X_ - 12, X_ + 185, Y_ + 287, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ - 12, Y_ + 142, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ - 7, Y_ + 142, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ - 0, Y_ + 0, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 70, Y_ + 0, Y_ + 14, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 185, Y_ + 0, Y_ + 287, 2)
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
			aLine = New hLine(X_ - 0, X_ + 185, Y_ + 287 - 15, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			'bLine = New vLine(X_ + 65, Y_ + 287 - 15, Y_ + 287, 2)
			'Me.Controls.Add(bLine)
			'a.Add(bLine)
			bLine = New vLine(X_ + 7, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 17, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 40, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 55, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 65, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 175, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			aLine = New hLine(X_ + 175, X_ + 185, Y_ + 287 - 8, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)

			ShowFormatA4_2()
		End If
		If f.format = "A31" Then
			'***********************************************
			aLine = New hLine(X_ - 12, X_ + 395, Y_ + 0, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0, X_ + 70, Y_ + 14, 2)
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
			aLine = New hLine(X_ - 12, X_ + 395, Y_ + 287, 2)
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
			bLine = New vLine(X_ + 70, Y_ + 0, Y_ + 14, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 395, Y_ + 0, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			'Основная надпись А42
			Dim t As Integer = 1
			For i = 5 To 50 Step 5
				If i = 30 Or i = 35 Then
					t = 2
				Else
					t = 1
				End If
				aLine = New hLine(X_ + 210, X_ + 210 + 65, Y_ + 287 - i, t)
				Me.Controls.Add(aLine)
				a.Add(aLine)
			Next
			aLine = New hLine(X_ + 210 + 65, X_ + 395, Y_ + 287 - 15, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 210 + 65, X_ + 395, Y_ + 287 - 40, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 210, X_ + 395, Y_ + 287 - 55, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ + 210 + 7, Y_ + 287 - 55, Y_ + 287 - 30, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 17, Y_ + 287 - 55, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 40, Y_ + 287 - 55, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 55, Y_ + 287 - 55, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 65, Y_ + 287 - 55, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 135, Y_ + 287 - 40, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 140, Y_ + 287 - 35, Y_ + 287 - 20, 1)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 145, Y_ + 287 - 35, Y_ + 287 - 20, 1)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 150, Y_ + 287 - 40, Y_ + 287 - 20, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 167, Y_ + 287 - 40, Y_ + 287 - 20, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 210 + 155, Y_ + 287 - 20, Y_ + 287 - 15, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			aLine = New hLine(X_ + 210 + 135, X_ + 210 + 185, Y_ + 287 - 35, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ + 210 + 135, X_ + 210 + 185, Y_ + 287 - 20, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ + 210, Y_ + 287 - 55, Y_ + 287 - 0, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			ShowFormatA3_1()
		End If
		If f.format = "A32" Then
			'***********************************************
			aLine = New hLine(X_ - 0, X_ + 395, Y_ + 0, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0, X_ + 70, Y_ + 14, 2)
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
			aLine = New hLine(X_ - 12, X_ + 395, Y_ + 287, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ - 12, Y_ + 142, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ - 7, Y_ + 142, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ - 0, Y_ + 0, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 70, Y_ + 0, Y_ + 14, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 395, Y_ + 0, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			'Код общей рамки А3
			'Основная надпись А32
			aLine = New hLine(X_ - 0 + 210, X_ + 65 + 210, Y_ + 287 - 5, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0 + 210, X_ + 65 + 210, Y_ + 287 - 10, 1)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			aLine = New hLine(X_ - 0 + 210, X_ + 185 + 210, Y_ + 287 - 15, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ + 7 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 17 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 40 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 55 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 65 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			bLine = New vLine(X_ + 175 + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			aLine = New hLine(X_ + 175 + 210, X_ + 185 + 210, Y_ + 287 - 8, 2)
			Me.Controls.Add(aLine)
			a.Add(aLine)
			bLine = New vLine(X_ + 210, Y_ + 287 - 15, Y_ + 287, 2)
			Me.Controls.Add(bLine)
			a.Add(bLine)
			ShowFormatA3_2()
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
			If Mode = "eBat" Then
				Pn = New Pen(Color.Black, 1)
				G.DrawLine(Pn, rx + 5, ry - 2, rx + 55, ry - 2)
				G.DrawLine(Pn, rx + 55, ry - 2, rx + 55, ry + 30)
				G.DrawLine(Pn, rx + 5, ry - 2, rx + 5, ry + 30)
				G.DrawLine(Pn, rx + 5, ry + 30, rx + 55, ry + 30)
			End If
			G.Dispose()
			'ToolTip1.SetToolTip(Me, CStr(rx) + ", " + CStr(ry))
		End If
		If Mode = "createConnect1" Then
			createConnect.MouseMove(rx, ry)
		End If
		If Mode = "" Then
			createConnect = Nothing
		End If

	End Sub

	Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
		If Mode = "newPoint" Then
			'Создание пустой точки потом запретить!!!!!!!!!!!!!!!!!!!!!!
			Dim eComp As New EComponent With {
				.aType = "ePoint",
				.numInArray = Elements.Count
			}
			Elements.Add(eComp)
			Dim p As New EPoint(rx, ry, eComp.numInArray)
			Me.Controls.Add(p)
			eComp.component = p

			Mode = ""
			GroupBox1.Visible = True
			CheckBox2.Visible = True
			Me.Cursor = Cursors.Default
			NeedSave = True
			'!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		End If
		If Mode = "createConnect1" Then
			createConnect.MouseClick(rx, ry)
		End If
		If Mode = "eBat" Then
			Dim eComp As New EComponent With {
							.aType = "eBat",
							.numInArray = Elements.Count
						}
			Elements.Add(eComp)
			Dim bat As New eBat(rx, ry, eComp.numInArray)
			Me.Controls.Add(bat)
			eComp.component = bat

			Mode = ""
			GroupBox1.Visible = True
			CheckBox2.Visible = True
			Me.Cursor = Cursors.Default
			NeedSave = True
		End If
		If Mode = "eGND" Then
			Dim eComp As New EComponent With {
							.aType = "eGND",
							.numInArray = Elements.Count
						}
			Elements.Add(eComp)
			Dim gnd As New EGND(rx, ry, eComp.numInArray)
			Me.Controls.Add(gnd)
			eComp.component = gnd

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
		'Файл/Новый
		Dim q As Microsoft.VisualBasic.MsgBoxResult
		If NeedSave Then
			q = MsgBox("Сохранить изменения в схеме?", MsgBoxStyle.YesNo, "Предупреждение")
			If q = vbYes Then
				'сохранение
				FileSave()
			End If
		End If
		Dim eComp As EComponent
		For i = 1 To Elements.Count - 1
			eComp = Elements(i)
			If eComp Is Nothing Then

			Else
				eComp.component.Dispose()
			End If
		Next
		Elements.Clear()
		Elements.Add(Nothing)
		Dim theLine As IRemovable
		For i = 0 To a.Count - 1
			theLine = a(i)
			theLine.Dispose()
		Next
		a.Clear()
		f.format = ""
		HideFormatText()
		FileName = ""
		Me.Text = "Безымянный - eScheme"
		NeedSave = False
	End Sub

	Private Sub FileSave()
		If Elements.Count = 0 Then Exit Sub
		If FileName = "" Then
			Dim a As Integer
			SaveFileDialog1.FileName = FileName
			a = SaveFileDialog1.ShowDialog()
			If a = 2 Then Exit Sub
			FileName = SaveFileDialog1.FileName
			Me.Text = FileName + " - eScheme"
		End If
		Try
			'не сериализуется
			Dim saveArray As New ArrayList
			Dim eComp As EComponent
			Dim e As IConnectable
			saveArray.Add(f)

			ProgressBar.Maximum = 50 + Elements.Count - 1
			Dim count As Integer = Elements.Count - 1
			Dim sleepTime As Integer = 1000 / count + 5
			ProgressBar.Visible = True
			ProgressBar.Value = 50
			Application.DoEvents()

			For i = 1 To Elements.Count - 1
				eComp = Elements(i)
				If eComp Is Nothing Then
					saveArray.Add(Nothing)
				Else
					e = eComp.component
					saveArray.Add(e.ForSave)
				End If
				System.Threading.Thread.Sleep(sleepTime)
				ProgressBar.Value = 50 + i
				Application.DoEvents()
			Next

			Dim fStream As New FileStream(FileName, FileMode.Create, FileAccess.Write)
			Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
			myBinaryFormatter.Serialize(fStream, saveArray)
			fStream.Close()
			NeedSave = False
			ProgressBar.Visible = False
		Catch ex As Exception
			MsgBox("Не удалось сохранить файл " + CStr(FileName) + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Предупреждение")
		End Try

	End Sub

	Private Sub PrintReversNumber()
		Try
			Dim fs As New FontStyle
			fs = FontStyle.Italic Or FontStyle.Bold
			Dim font As New Font(fnt.Families(0), 14, fs)
			Dim g As Graphics = pbNumber.CreateGraphics
			g.Clear(Me.BackColor)
			g.TranslateTransform(200, 22)
			g.RotateTransform(180)
			g.DrawString(f.number, font, New SolidBrush(Color.Black), New PointF(0, 0))
			g.Dispose()
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Txt_TextChanged(sender As Object, e As EventArgs) Handles txtNumber.TextChanged, txtList.TextChanged, txtListov.TextChanged,
			txtMashtab.TextChanged, txtMassa.TextChanged, txtName.TextChanged, txtNcontr.TextChanged, txtOrg1.TextChanged,
			txtOrg2.TextChanged, txtProv.TextChanged, txtRazrab.TextChanged, txtSogl.TextChanged, txtTcontr.TextChanged,
			txtType.TextChanged, txtUtv.TextChanged
		If sender.Equals(txtNumber) Then
			f.number = txtNumber.Text
			PrintReversNumber()
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

	Private Sub ВыходToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem1.Click
		Me.Close()
	End Sub

	Private Sub ОткрытьToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ОткрытьToolStripMenuItem1.Click
		Dim q As Microsoft.VisualBasic.MsgBoxResult
		'If AddFrm Then GoTo StartFile
		If OpenAtStart Then
			OpenAtStart = False
			GoTo StartFile
		End If
		If NeedSave Then
			q = MsgBox("Сохранить изменения в схеме?", MsgBoxStyle.YesNo, "Предупреждение")
			If q = vbYes Then
				'сохранение
				FileSave()
			End If
		End If
		OpenFileDialog1.FileName = FileName
		Dim b As System.Windows.Forms.DialogResult
		b = OpenFileDialog1.ShowDialog()
		If b = vbCancel Then Exit Sub
		FileName = OpenFileDialog1.FileName
		If FileName = "" Then
			Exit Sub
		End If
StartFile:
		Me.Text = FileName + " - eScheme"
		Dim theLine As IRemovable
		For i = 0 To a.Count - 1
			theLine = a(i)
			theLine.Dispose()
		Next
		a.Clear()
		Dim eComp As EComponent
		For i = 1 To Elements.Count - 1
			eComp = Elements(i)
			If eComp Is Nothing Then

			Else
				eComp.component.Dispose()
			End If

		Next
		Elements.Clear()
		Elements.Add(Nothing)

		Dim saveArray As ArrayList
		Try
			Dim fStream As New FileStream(FileName, FileMode.Open, FileAccess.Read)
			Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
			saveArray = CType(myBinaryFormatter.Deserialize(fStream), ArrayList)
			fStream.Close()
		Catch ex As Exception
			MsgBox("Ошибка при чтении файла " + CStr(FileName) + vbCrLf + "Файл возможно поврежден." + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Предупреждение")
			Exit Sub
		End Try
		'Отображение прочитанного массива
		'Dim eComp As New eComponent
		f = saveArray(0)
		ProgressBar.Maximum = 50 + saveArray.Count - 1
		Dim count As Integer = saveArray.Count - 1
		Dim sleepTime As Integer = 1000 / count + 5
		ProgressBar.Visible = True
		ProgressBar.Value = 50
		Application.DoEvents()
		CreateFormat()

		Dim aComp As ArrayList
		For i = 1 To saveArray.Count - 1
			aComp = saveArray(i)
			If aComp Is Nothing Then
				'Nothing
				Elements.Add(Nothing)
			Else
				'ePoint
				If aComp(0) = "ePoint" Then
					Dim p As New EPoint(aComp(2), aComp(3), aComp(1)) With {
						.links = aComp(4),
						.Condition = aComp(5)
					}
					eComp = New EComponent With {
						.aType = "ePoint",
						.numInArray = p.num,
						.component = p
					}
					Elements.Add(eComp)
					Me.Controls.Add(p)
				End If
				'eLine
				If aComp(0) = "eLine" Then
					Dim line As New eLine(aComp(2), aComp(3), aComp(4), aComp(5), aComp(1)) With {
						.links = aComp(6),
						.Condition = aComp(7)
					}
					eComp = New EComponent With {
						.aType = "eLine",
						.numInArray = line.num,
						.component = line
					}
					Elements.Add(eComp)
					Me.Controls.Add(line)
				End If
				'eBat
				If aComp(0) = "eBat" Then
					Dim bat As New eBat(aComp(2), aComp(3), aComp(1))
					eComp = New EComponent With {
						.aType = "eBat",
						.numInArray = bat.num,
						.component = bat
					}
					Elements.Add(eComp)
					Me.Controls.Add(bat)
				End If
				'eGND
				If aComp(0) = "eGND" Then
					Dim gnd As New EGND(aComp(2), aComp(3), aComp(1)) With {
						.link = aComp(4)
					}
					eComp = New EComponent With {
						.aType = "eGND",
						.numInArray = gnd.num,
						.component = gnd
					}
					Elements.Add(eComp)
					Me.Controls.Add(gnd)
				End If
			End If
			System.Threading.Thread.Sleep(sleepTime)
			ProgressBar.Value = 50 + i
			Application.DoEvents()
		Next
		ProgressBar.Visible = False
	End Sub

	Private Sub PictureBox_eBat_Click(sender As Object, e As EventArgs) Handles PictureBox_eBat.Click
		Mode = "eBat"
		GroupBox1.Visible = False
		CheckBox2.Visible = False
		Me.Cursor = element_cur
	End Sub

	Private Sub PictureBox_Point_Click(sender As Object, e As EventArgs) Handles PictureBox_Point.Click
		Mode = "newPoint"
		GroupBox1.Visible = False
		CheckBox2.Visible = False
		Me.Cursor = Cursors.Hand
	End Sub

	Private Sub PictureBox_Provod_Click(sender As Object, e As EventArgs) Handles PictureBox_Provod.Click
		Mode = "createConnect"
		GroupBox1.Visible = False
		CheckBox2.Visible = False
		Me.Cursor = addLine_cur
	End Sub

	Private Sub PictureBox_Massa_Click(sender As Object, e As EventArgs) Handles PictureBox_Massa.Click
		Mode = "eGND"
		GroupBox1.Visible = False
		CheckBox2.Visible = False
		Me.Cursor = gnd_cur
	End Sub

	Private Sub PictureBox_Delete_Click(sender As Object, e As EventArgs) Handles PictureBox_Delete.Click
		Mode = "Delete"
		GroupBox1.Visible = False
		CheckBox2.Visible = False
		Me.Cursor = del_cur
	End Sub

	Public Sub Delete(num As Integer)
		Dim eComp As EComponent
		eComp = Elements(num)
		eComp.component.Dispose()
		Elements(num) = Nothing
		NeedSave = True
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		pointsInProcess.Clear()
		Dim eComp As EComponent
		Dim econ As IConnectable
		eComp = Elements(1)
		econ = eComp.component
		econ.Change(0, 15)
	End Sub

	Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
		If e.KeyCode = Keys.F12 And e.Shift Then
			If f.format.StartsWith("A4") Then
				Dim bm As New Bitmap(613, 944, Drawing.Imaging.PixelFormat.Format32bppArgb)
				Dim Gf As Graphics = Me.CreateGraphics
				Gf.Clear(Me.BackColor)
				Gf.Dispose()
				Me.DrawToBitmap(bm, New Rectangle(New Point(0, 0), New Point(613, 944)))

				Dim img As New Bitmap(603, 888, Drawing.Imaging.PixelFormat.Format32bppArgb)
				Using g As Graphics = Graphics.FromImage(img)
					g.DrawImage(bm, New Rectangle(New Point(), img.Size), New Rectangle(New Point(10, 56), New Point(603, 888)), GraphicsUnit.Pixel)
				End Using

				img.Save(f.number & "_лист" & f.list & ".png", Drawing.Imaging.ImageFormat.Png)
				zx = 0
				zy = 0
			End If
			If f.format.StartsWith("A3") Then
				Dim bm As New Bitmap(613 + 630, 944, Drawing.Imaging.PixelFormat.Format32bppArgb)
				Dim Gf As Graphics = Me.CreateGraphics
				Gf.Clear(Me.BackColor)
				Gf.Dispose()
				Me.DrawToBitmap(bm, New Rectangle(New Point(0, 0), New Point(613 + 630, 944)))

				Dim img As New Bitmap(603 + 630, 888, Drawing.Imaging.PixelFormat.Format32bppArgb)
				Using g As Graphics = Graphics.FromImage(img)
					g.DrawImage(bm, New Rectangle(New Point(), img.Size), New Rectangle(New Point(10, 56), New Point(603 + 630, 888)), GraphicsUnit.Pixel)
				End Using

				img.Save(f.number & "_лист" & f.list & ".png", Drawing.Imaging.ImageFormat.Png)
				zx = 0
				zy = 0
			End If
			ProgressBar.Visible = True
			For i = 1 To 100
				System.Threading.Thread.Sleep(5)
				ProgressBar.Value = i
				Application.DoEvents()
			Next
			System.Threading.Thread.Sleep(50)
			ProgressBar.Visible = False
		End If
		If e.KeyCode = Keys.Escape Then
			Mode = ""
			GroupBox1.Visible = True
			CheckBox2.Visible = True
			Me.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub PbNumber_Paint(sender As Object, e As PaintEventArgs) Handles pbNumber.Paint
		Try
			Dim fs As New FontStyle
			fs = FontStyle.Italic Or FontStyle.Bold
			Dim font As New Font(fnt.Families(0), 14, fs)
			'Dim g As Graphics = pbNumber.CreateGraphics
			e.Graphics.Clear(Me.BackColor)
			e.Graphics.TranslateTransform(200, 22)
			e.Graphics.RotateTransform(180)
			e.Graphics.DrawString(f.number, font, New SolidBrush(Color.Black), New PointF(0, 0))
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		Dim q As Microsoft.VisualBasic.MsgBoxResult
		If NeedSave Then
			q = MsgBox("Сохранить изменения в схеме?", MsgBoxStyle.YesNo, "Предупреждение")
			If q = vbYes Then
				'сохранение
				FileSave()
			End If
		End If
	End Sub

	Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		Dim a, b As Integer
		a = Me.Height
		a = CInt((a - 20) / 2)
		b = Me.Width
		b = CInt((b - 250) / 2)
		ProgressBar.Location = New System.Drawing.Point(b, a)
	End Sub

	Public Sub OnConnect(n1 As Integer, n2 As Integer)
		Dim eComp As EComponent
		Dim p01, p02 As EPoint
		eComp = Elements(n1)
		p01 = eComp.component
		eComp = Elements(n2)
		p02 = eComp.component
		Dim p01C As Integer = p01.Condition 'Текущие значения Condition на точках
		Dim p02C As Integer = p02.Condition
		pointsInProcess.Clear()
		Dim p01Ck As Integer = p01.CheckSig(0) 'Текущие значения CheckSig на точках
		pointsInProcess.Clear()
		Dim p02Ck As Integer = p02.CheckSig(0)

		'НИЖЕ ВСЯ ЛОГИКА СОЕДИНЕНИЯ
		If p01C <> 0 Then 'Тут пока временное говно - Сейчас создание замкнутого контура не выдает ошибку
			If p02C = 0 Then
				pointsInProcess.Clear()
				p02.Change(0, p01C)
			End If

		Else
			If p02C <> 0 Then
				pointsInProcess.Clear()
				p01.Change(0, p02C)
			End If
		End If
		'В конце соединить точки (links.add)
	End Sub

	Public Sub DisConnect(n1 As Integer, n2 As Integer)
		Dim eComp As EComponent
		Dim p01, p02 As EPoint
		eComp = Elements(n1)
		p01 = eComp.component
		eComp = Elements(n2)
		p02 = eComp.component
		Dim p01C As Integer = p01.Condition 'Текущие значения Condition на точках
		Dim p02C As Integer = p02.Condition
		p01.links.Remove(p02.num) 'разъединяем точки
		p02.links.Remove(p01.num)
		pointsInProcess.Clear()
		Dim p01Ck As Integer = p01.CheckSig(0) 'Текущие значения CheckSig на точках
		pointsInProcess.Clear()
		Dim p02Ck As Integer = p02.CheckSig(0)

		If p01Ck = 0 And p01C <> 0 Then
			pointsInProcess.Clear()
			p01.Change(0, 0)
		End If
		If p02Ck = 0 And p02C <> 0 Then
			pointsInProcess.Clear()
			p02.Change(0, 0)
		End If
	End Sub
End Class
