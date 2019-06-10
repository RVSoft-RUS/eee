Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.Serialization

Public Class Form1
    Public color0 As Color = Color.Gray
    Public colorM As Color = Color.Black
    Public color15 As Color = Color.Red
    Public color30 As Color = Color.Purple
    Public bordColor As Color = Color.LightBlue
    Public txtColor As Color = Color.Black
    Public commentColor As Color = Color.SandyBrown
    Public stopAtRMClick As Boolean = True
    Public openWithSize As Boolean = False

    Public rx As Integer, ry As Integer 'округленные до 20 координаты точки
    Dim zx, zy As Integer 'Для предотвращения мерцания линий при MouseMove
    Public Mode As String = "" 'Текущий режим - показывает состояние, что делаем. Пусто - ничего не делаем
    Dim lastMode As String = ""
    Public Elements As New ArrayList
    Public FileName As String = ""
    Dim fileParam As String = ""
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
    Public R1_cur As Cursor
    Public R2_cur As Cursor
    Public R3_cur As Cursor
    Public R4_cur As Cursor
    Public LampH_cur As Cursor
    Public LampV_cur As Cursor
    Public FuseH_cur As Cursor
    Public FuseV_cur As Cursor
    Public BuH_cur As Cursor
    Public BuV_cur As Cursor

    Dim OpenAtStart As Boolean = False
    Public isCheckUI As Boolean = False
    Public isChanging As Boolean = False
    Public isCycle As Boolean = False
    'во время переключений pointsInProcess обнулять и добавлять номера точек, через который прошел сигнал
    'если точка при добавлении уже существует, значит зациклено
    Public pointsInProcessSig As New ArrayList
    Public pointsInProcessUI As New ArrayList
    Public Udefault As Single = 12
    Public Rdefault As Single = 50
    Public RLampdefault As Single = 29
    Public FUSEdefault As Single = 10
    Public RELEdefault As Single = 59
    Public fnt As System.Drawing.Text.PrivateFontCollection = New System.Drawing.Text.PrivateFontCollection()
    Public gost_font As Font
    Dim firstPoint As Point

    Public moveObject As IMovable = Nothing
    Public moveXstart As Integer
    Public moveYstart As Integer
    Public moveArray As New ArrayList

	Public unDoArray As New Stack()
	Dim isUndo As Boolean = False

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
			'fnt.AddFontFile(Application.StartupPath + "\resourses\gost.ttf")
			Dim buffer() As Byte
			buffer = My.Resources.gost
			Dim ip As IntPtr = Runtime.InteropServices.Marshal.AllocHGlobal(Runtime.InteropServices.Marshal.SizeOf(GetType(Byte)) * buffer.Length)
			Runtime.InteropServices.Marshal.Copy(buffer, 0, ip, buffer.Length)
			fnt.AddMemoryFont(ip, buffer.Length)
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
			MsgBox("Не внедрить шрифт gost.ttf." + vbCrLf + "Для нормальной работы программы скопируйте файл gost.ttf в указанный каталог", vbCritical, "Fatal")
        End Try
        HideFormatText()
        Try
            'line_cur = New Cursor(Application.StartupPath + "\resourses\line.cur")
            line_cur = New Cursor(New System.IO.MemoryStream(My.Resources.line))
            'point_cur = New Cursor(Application.StartupPath + "\resourses\point.cur")
            point_cur = New Cursor(New System.IO.MemoryStream(My.Resources.point))
            'addLine_cur = New Cursor(Application.StartupPath + "\resourses\addLine.cur")
            addLine_cur = New Cursor(New System.IO.MemoryStream(My.Resources.addLine))
            'del_cur = New Cursor(Application.StartupPath + "\resourses\del.cur")
            del_cur = New Cursor(New System.IO.MemoryStream(My.Resources.del))
            'addPoint_cur = New Cursor(Application.StartupPath + "\resourses\addPoint.cur")
            addPoint_cur = New Cursor(New System.IO.MemoryStream(My.Resources.addPoint))
            'element_cur = New Cursor(Application.StartupPath + "\resourses\element.cur")
            element_cur = New Cursor(New System.IO.MemoryStream(My.Resources.element))
            'gnd_cur = New Cursor(Application.StartupPath + "\resourses\gnd.cur")
            gnd_cur = New Cursor(New System.IO.MemoryStream(My.Resources.gnd))
            'R1_cur = New Cursor(Application.StartupPath + "\resourses\R1.cur")
            R1_cur = New Cursor(New System.IO.MemoryStream(My.Resources.R1))
            'R2_cur = New Cursor(Application.StartupPath + "\resourses\R2.cur")
            R2_cur = New Cursor(New System.IO.MemoryStream(My.Resources.R2))
            'R3_cur = New Cursor(Application.StartupPath + "\resourses\R3.cur")
            R3_cur = New Cursor(New System.IO.MemoryStream(My.Resources.R3))
            'R4_cur = New Cursor(Application.StartupPath + "\resourses\R4.cur")
            R4_cur = New Cursor(New System.IO.MemoryStream(My.Resources.R4))
            'FuseH_cur = New Cursor(Application.StartupPath + "\resourses\fuseH.cur")
            FuseH_cur = New Cursor(New System.IO.MemoryStream(My.Resources.fuseH))
            'FuseV_cur = New Cursor(Application.StartupPath + "\resourses\fuseV.cur")
            FuseV_cur = New Cursor(New System.IO.MemoryStream(My.Resources.fuseV))
            'BuH_cur = New Cursor(Application.StartupPath + "\resourses\BuH.cur")
            BuH_cur = New Cursor(New System.IO.MemoryStream(My.Resources.BuH))
            ' BuV_cur = New Cursor(Application.StartupPath + "\resourses\BuV.cur")
            BuV_cur = New Cursor(New System.IO.MemoryStream(My.Resources.BuV))
            LampH_cur = New Cursor(New System.IO.MemoryStream(My.Resources.LampH))
            LampV_cur = New Cursor(New System.IO.MemoryStream(My.Resources.LampV))
        Catch ex As Exception
            line_cur = Cursors.Default
            point_cur = Cursors.Cross
            addLine_cur = Cursors.Help
            del_cur = Cursors.No
            addPoint_cur = Cursors.NoMove2D
            element_cur = Cursors.Default
            gnd_cur = Cursors.Hand
            R1_cur = Cursors.Hand
            R2_cur = Cursors.Hand
            R3_cur = Cursors.Hand
            R4_cur = Cursors.Hand
            FuseH_cur = Cursors.NoMoveHoriz
            FuseV_cur = Cursors.NoMoveVert
            BuH_cur = Cursors.NoMoveHoriz
            BuV_cur = Cursors.NoMoveVert
        End Try

        Try
            fileParam = Application.StartupPath
            If fileParam.EndsWith("\") Then
                fileParam += "default_values.inf"
            Else
                fileParam += "\default_values.inf"
            End If
            Dim dict As ArrayList
            Dim fStream As New FileStream(fileParam, FileMode.Open, FileAccess.Read)
            Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
            dict = CType(myBinaryFormatter.Deserialize(fStream), ArrayList)
            fStream.Close()

            color0 = dict(0)
            colorM = dict(1)
            color15 = dict(2)
            color30 = dict(3)
            bordColor = dict(4)
            txtColor = dict(5)
            commentColor = dict(6)
            stopAtRMClick = dict(7)
            openWithSize = dict(8)
            Udefault = dict(9)
            Rdefault = dict(10)
            RLampdefault = dict(11)
            FUSEdefault = dict(12)
            RELEdefault = dict(13)
        Catch ex As Exception

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
        LabelSig.Focus()
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
        If a.Count > 0 And f.format <> "" And Not isUndo Then
            'форматка уже есть, спросить надо ли переделать?
            Dim a As Microsoft.VisualBasic.MsgBoxResult
            a = MsgBox("Изменить формат?", MsgBoxStyle.YesNo, "Предупреждение")
            If a <> vbYes Then
                Exit Sub
            End If
        End If
        'NeedSave = True 'Тут получается если в сохр файле есть формат, то сохранять его нужно, даже если не изменяли ничего
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
        If Math.Abs(zx - e.X) + Math.Abs(zy - e.Y) < 5 Then
            Exit Sub 'Если почти не сдвинулось - то выход

        End If
        rx = CInt(Math.Round(e.X / 20))
        rx = rx * 20
        ry = CInt(Math.Round(e.Y / 20))
        ry = ry * 20
        zx = e.X
        zy = e.Y

        ToolTip1.SetToolTip(Me, Mode)
        If Mode = "MoveMe" Then
            TextBox1.Text = "rx -Xstart=" + CStr(rx - moveXstart) + "  ry - Ystart=" + CStr(ry - moveYstart) + vbCrLf + TextBox1.Text
            If moveObject Is Nothing Then Exit Sub

            Dim mayMove As Boolean = False

            If rx - moveXstart <> 0 Then
                moveArray.Clear()
                mayMove = moveObject.Move(moveObject, rx - moveXstart, 0)
                If mayMove Then
                    Cursor = Cursors.SizeAll
                    Dim m As IMovable
                    For j = 0 To moveArray.Count - 1
                        m = moveArray(j)
                        m.MoveOK()
                    Next
                    DoNeedSave()
                Else
                    Cursor = Cursors.No
                End If
                moveXstart = rx
                GoTo nextAfterMove
            End If
            If ry - moveYstart <> 0 Then
                moveArray.Clear()
                mayMove = False
                mayMove = moveObject.Move(moveObject, 0, ry - moveYstart)
                If mayMove Then
                    Cursor = Cursors.SizeAll
                    Dim m As IMovable
                    For j = 0 To moveArray.Count - 1
                        m = moveArray(j)
                        m.MoveOK()
                    Next
                    DoNeedSave()
                Else
                    Cursor = Cursors.No
                End If
                moveYstart = ry
                TextBox1.Text = "ry:" + CStr(moveArray.Count) + vbCrLf + TextBox1.Text
            End If

        End If

nextAfterMove:
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
            If Mode = "bt2" Then
                Pn = New Pen(bordColor, 1)
                Dim secondPoint = New Point(e.X, e.Y)
                G.DrawLine(Pn, firstPoint.X, firstPoint.Y, secondPoint.X, firstPoint.Y)
                G.DrawLine(Pn, secondPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y)
                G.DrawLine(Pn, secondPoint.X, secondPoint.Y, firstPoint.X, secondPoint.Y)
                G.DrawLine(Pn, firstPoint.X, secondPoint.Y, firstPoint.X, firstPoint.Y)
            End If
            If Mode = "eResist1" Or Mode = "eResist3" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx - 15, ry - 10, rx + 15, ry - 10)
                G.DrawLine(Pn, rx - 15, ry + 30, rx + 15, ry + 30)
                G.DrawLine(Pn, rx - 15, ry - 10, rx - 15, ry + 30)
                G.DrawLine(Pn, rx + 15, ry - 10, rx + 15, ry + 30)
            End If
            If Mode = "eResist2" Or Mode = "eResist4" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx - 10, ry - 15, rx + 30, ry - 15)
                G.DrawLine(Pn, rx + 30, ry - 15, rx + 30, ry + 15)
                G.DrawLine(Pn, rx + 30, ry + 15, rx - 10, ry + 15)
                G.DrawLine(Pn, rx - 10, ry + 15, rx - 10, ry - 15)
            End If
            If Mode = "eLamp1" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx, ry, rx + 5, ry)
                G.DrawLine(Pn, rx + 35, ry, rx + 40, ry)
                G.DrawEllipse(Pn, rx + 5, ry - 15, 30, 30)
            End If
            If Mode = "eLamp2" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx, ry, rx, ry - 5)
                G.DrawLine(Pn, rx, ry - 35, rx, ry - 40)
                G.DrawEllipse(Pn, rx - 15, ry - 35, 30, 30)
            End If


            If Mode = "newFuseV" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx + 5, ry + 5, rx - 5, ry + 5)
                G.DrawLine(Pn, rx + 5, ry + 35, rx - 5, ry + 35)
                G.DrawLine(Pn, rx + 5, ry + 5, rx + 5, ry + 35)
                G.DrawLine(Pn, rx - 5, ry + 5, rx - 5, ry + 35)
            End If
            If Mode = "newFuseH" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx + 5, ry + 5, rx + 5, ry - 5)
                G.DrawLine(Pn, rx + 5, ry - 5, rx + 35, ry - 5)
                G.DrawLine(Pn, rx + 35, ry - 5, rx + 35, ry + 5)
                G.DrawLine(Pn, rx + 35, ry + 5, rx + 5, ry + 5)
            End If
            If Mode = "eButton1" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx + 5, ry - 15, rx + 35, ry - 15)
                G.DrawLine(Pn, rx + 5, ry + 5, rx + 35, ry + 5)
                G.DrawLine(Pn, rx + 5, ry - 15, rx + 5, ry + 5)
                G.DrawLine(Pn, rx + 35, ry + 5, rx + 35, ry - 15)
            End If
            If Mode = "eButton2" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx - 15, ry - 5, rx - 15, ry - 35)
                G.DrawLine(Pn, rx + 5, ry - 5, rx + 5, ry - 35)
                G.DrawLine(Pn, rx + 5, ry - 5, rx - 15, ry - 5)
                G.DrawLine(Pn, rx + 5, ry - 35, rx - 15, ry - 35)
            End If
            If Mode = "eSwitch1" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx + 5, ry + 2, rx + 35, ry + 2)
                G.DrawLine(Pn, rx + 35, ry + 2, rx + 35, ry - 30)
                G.DrawLine(Pn, rx + 35, ry - 30, rx + 5, ry - 30)
                G.DrawLine(Pn, rx + 5, ry - 30, rx + 5, ry + 2)
            End If
            If Mode = "eSwitch2" Then
                Pn = New Pen(Color.Black, 1)
                G.DrawLine(Pn, rx + 2, ry - 5, rx + 2, ry - 35)
                G.DrawLine(Pn, rx + 2, ry - 35, rx - 30, ry - 35)
                G.DrawLine(Pn, rx - 30, ry - 35, rx - 30, ry - 5)
                G.DrawLine(Pn, rx - 30, ry - 5, rx + 2, ry - 5)
            End If
            G.Dispose()
            ToolTip1.SetToolTip(Me, Mode)
        End If
        If Mode = "createConnect1" Then
            createConnect.MouseMove(rx, ry)
        End If
        If Mode = "" Then
            createConnect = Nothing
        End If

    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = MouseButtons.Right And stopAtRMClick Then
            Mode = ""
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            CheckBox2.Checked = True
            Me.Cursor = Cursors.Default
        End If
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
            CheckBox2.Checked = True
            CheckBox2.Visible = True
            GroupBox1.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        End If
        If Mode = "createConnect1" Then
            createConnect.MouseClick(rx, ry)
        End If
        If Mode = "bt2" Then
            Dim secondPoint = New Point(e.X, e.Y)
            Dim eComp As New EComponent With {
                                        .aType = "eBText",
                                        .numInArray = Elements.Count
                                                                }
            Elements.Add(eComp)
            Dim bt As New eBorderText(firstPoint, secondPoint, "Введите текст", eComp.numInArray)
            Me.Controls.Add(bt)
            eComp.component = bt

            Mode = ""
            CheckBox2.Checked = True
            CheckBox2.Visible = True
            GroupBox1.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode = "eText" Then
            Dim eComp As New EComponent With {
                                        .aType = "eText",
                                        .numInArray = Elements.Count
                                                                }
            Elements.Add(eComp)
            Dim t As New eText(e.X, e.Y, "Введите текст", eComp.numInArray)
            Me.Controls.Add(t)
            eComp.component = t

            Mode = ""
            CheckBox2.Checked = True
            CheckBox2.Visible = True
            GroupBox1.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode = "eTextC" Then
            Dim eComp As New EComponent With {
                                        .aType = "eTextC",
                                        .numInArray = Elements.Count
                                                                }
            Elements.Add(eComp)
            Dim t As New eTextC(e.X, e.Y, "Комментарий", eComp.numInArray)
            Me.Controls.Add(t)
            eComp.component = t

            Mode = ""
            CheckBox2.Checked = True
            CheckBox2.Visible = True
            GroupBox1.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If

        If Mode = "bt1" Then
            firstPoint = New Point(e.X, e.Y)
            Mode = "bt2"
        End If
        If Mode = "eBat" Then
            If f.Batt > 0 Then
                MsgBox("Допускается только один источник питания в схеме.")
                Exit Sub
            End If
            Dim eComp As New EComponent With {
                            .aType = "eBat",
                            .numInArray = Elements.Count
                                                    }
            f.Batt = eComp.numInArray
            Elements.Add(eComp)
            Dim bat As New eBat(rx, ry, eComp.numInArray, Udefault, 0)
            Me.Controls.Add(bat)
            eComp.component = bat

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode.StartsWith("eResist") Then
            Dim eComp As New EComponent With {
                            .aType = "eResist",
                            .numInArray = Elements.Count
                        }
            Elements.Add(eComp)
            Dim loc As Integer = 4
            If Mode = "eResist1" Then loc = 1
            If Mode = "eResist2" Then loc = 2
            If Mode = "eResist3" Then loc = 3
            Dim eResist As New EResist(rx, ry, eComp.numInArray, Rdefault, False, 0, loc)
            Me.Controls.Add(eResist)
            eComp.component = eResist

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode.StartsWith("eButton") Then
            Dim eComp As New EComponent With {
                            .aType = "eButton",
                            .numInArray = Elements.Count
                        }
            Elements.Add(eComp)
            Dim loc As Integer
            If Mode = "eButton1" Then loc = 1
            If Mode = "eButton2" Then loc = 2
            Dim eButt As New eButton(rx, ry, eComp.numInArray, False, 1000, loc)
            Me.Controls.Add(eButt)
            eComp.component = eButt

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode.StartsWith("eLamp") Then
            Dim eComp As New EComponent With {
                            .aType = "eLamp",
                            .numInArray = Elements.Count
                        }
            Elements.Add(eComp)
            Dim eLa As New eLamp(rx, ry, eComp.numInArray, RLampdefault, False, 0)
            Me.Controls.Add(eLa)
            eComp.component = eLa

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode.StartsWith("eSwitch") Then
            Dim eComp As New EComponent With {
                            .aType = "eSwitch",
                            .numInArray = Elements.Count
                        }
            Elements.Add(eComp)
            Dim loc As Integer
            If Mode = "eSwitch1" Then loc = 1
            If Mode = "eSwitch2" Then loc = 2
            Dim eSw As New eSwitch(rx, ry, eComp.numInArray, False, loc)
            Me.Controls.Add(eSw)
            eComp.component = eSw

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
        End If
        If Mode.StartsWith("newFuse") Then
            Dim eComp As New EComponent With {
                            .aType = "eFuse",
                            .numInArray = Elements.Count
                        }
            Elements.Add(eComp)
            Dim pos As String
            If Mode.EndsWith("H") Then
                pos = "H"
            Else
                pos = "V"
            End If
            Dim eFH As New eFuse(rx, ry, eComp.numInArray, FUSEdefault, pos, 0, True)
            Me.Controls.Add(eFH)
            eComp.component = eFH

            Mode = ""
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
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
            CheckBox2.Checked = True
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            DoNeedSave()
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
        f.Batt = 0
        pointsInProcessSig.Clear()
        pointsInProcessUI.Clear()

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
		unDoArray.Clear()
		DoNeedSave()
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
            f.pos = Me.Location
            f.size = Me.Size
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
        f.Batt = 0
        pointsInProcessSig.Clear()
        pointsInProcessUI.Clear()

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
        If openWithSize Then
            Me.Location = f.pos
            Me.Size = f.size
        End If
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
                    Dim line As New eLine(aComp(2), aComp(3), aComp(4), aComp(5), aComp(1), aComp(8), aComp(9)) With {
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
                    Dim bat As New eBat(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5))
                    eComp = New EComponent With {
                        .aType = "eBat",
                        .numInArray = bat.num,
                        .component = bat
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(bat)
                End If
                'eR
                If aComp(0) = "eResist" Then
                    Dim res As New EResist(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6), aComp(7))
                    eComp = New EComponent With {
                        .aType = "eResist",
                        .numInArray = res.num,
                        .component = res
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(res)
                End If
                'eLamp
                If aComp(0) = "eLamp" Then
                    Dim la As New eLamp(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6))
                    eComp = New EComponent With {
                        .aType = "eLamp",
                        .numInArray = la.num,
                        .component = la
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(la)
                End If
                'eButton
                If aComp(0) = "eButton" Then
                    Dim but As New eButton(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6))
                    eComp = New EComponent With {
                        .aType = "eButton",
                        .numInArray = but.num,
                        .component = but
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(but)
                End If
                'eSwitch
                If aComp(0) = "eSwitch" Then
                    Dim but As New eSwitch(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5))
                    eComp = New EComponent With {
                        .aType = "eSwitch",
                        .numInArray = but.num,
                        .component = but
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(but)
                End If
                'eFuse
                If aComp(0) = "eFuse" Then
                    Dim fu As New eFuse(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6), aComp(7))
                    eComp = New EComponent With {
                        .aType = "eFuse",
                        .numInArray = fu.num,
                        .component = fu
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(fu)
                End If
                'eBText
                If aComp(0) = "eBText" Then
                    Dim bt As New eBorderText(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eBText",
                        .numInArray = bt.num,
                        .component = bt
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(bt)
                End If
                'eText
                If aComp(0) = "eText" Then
                    Dim t As New eText(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eText",
                        .numInArray = t.num,
                        .component = t
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(t)
                End If
                'eTextC
                If aComp(0) = "eTextC" Then
                    Dim t As New eTextC(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eTextC",
                        .numInArray = t.num,
                        .component = t
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(t)
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
            End If 'Этот кусок при изменении перекопировать в Undo
            System.Threading.Thread.Sleep(sleepTime)
            ProgressBar.Value = 50 + i
            Application.DoEvents()
        Next
        ProgressBar.Visible = False
		ShowComments(f.showComments)
		unDoArray.Clear()
		DoNeedSave()
		NeedSave = False
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
        'NeedSave = True
    End Sub

    Sub HidePanel()
        If Mode <> "Delete" And Mode <> "Move" And Mode <> "createConnect" And Mode <> "" And Mode <> "newPoint" Then
            lastMode = Mode
        End If
        If Mode <> "" Then
            GroupBox1.Visible = False
            CheckBox2.Visible = False
            Select Case Mode
                Case "eLamp1"
                    Me.Cursor = LampH_cur'************************
                Case "eLamp2"
                    Me.Cursor = LampV_cur'************************
                Case "eResist1"
                    Me.Cursor = R1_cur
                Case "eResist2"
                    Me.Cursor = R2_cur
                Case "eResist3"
                    Me.Cursor = R3_cur
                Case "eResist4"
                    Me.Cursor = R4_cur
                Case "eButton1", "eSwitch1"
                    Me.Cursor = BuH_cur
                Case "eButton2", "eSwitch2"
                    Me.Cursor = BuV_cur
                Case "eBat"
                    Me.Cursor = element_cur
                Case "newPoint"
                    Me.Cursor = Cursors.Hand
                Case "createConnect"
                    Me.Cursor = addLine_cur
                Case "eGND"
                    Me.Cursor = gnd_cur
                Case "newFuseH"
                    Me.Cursor = FuseH_cur
                Case "Delete"
                    Me.Cursor = del_cur
                Case "Move"
                    Me.Cursor = Cursors.NoMove2D
            End Select
        End If
    End Sub

    Private Sub PictureBox_Resist_Click(sender As Object, e As EventArgs) Handles PictureBox_Resist.Click
        Mode = "eResist1"
        HidePanel()
    End Sub

    Private Sub PictureBox_Lamp_Click(sender As Object, e As EventArgs) Handles PictureBox_Lamp.Click
        Mode = "eLamp1"
        HidePanel()
    End Sub

    Private Sub PictureBox_eBat_Click(sender As Object, e As EventArgs) Handles PictureBox_eBat.Click
        Mode = "eBat"
        HidePanel()
    End Sub

    Private Sub PictureBox_Point_Click(sender As Object, e As EventArgs) Handles PictureBox_Point.Click
        Mode = "newPoint"
        HidePanel()
    End Sub

    Private Sub PictureBox_Provod_Click(sender As Object, e As EventArgs) Handles PictureBox_Provod.Click
        Mode = "createConnect"
        HidePanel()
    End Sub

    Private Sub PictureBox_Massa_Click(sender As Object, e As EventArgs) Handles PictureBox_Massa.Click
        Mode = "eGND"
        HidePanel()
    End Sub

    Private Sub PictureBox_Fuse_Click(sender As Object, e As EventArgs) Handles PictureBox_Fuse.Click
        Mode = "newFuseH"
        HidePanel()
    End Sub


    Private Sub PictureBox_Delete_Click(sender As Object, e As EventArgs) Handles PictureBox_Delete.Click
        Mode = "Delete"
        HidePanel()
    End Sub

    Private Sub PictureBox_BorderText_Click(sender As Object, e As EventArgs) Handles PictureBox_BorderText.Click
        Mode = "bt1"
        HidePanel()
    End Sub

    Private Sub PictureBox_eText_Click(sender As Object, e As EventArgs) Handles PictureBox_eText.Click
        Mode = "eText"
        HidePanel()
    End Sub

    Private Sub PictureBox_eComment_Click(sender As Object, e As EventArgs) Handles PictureBox_eComment.Click
        Mode = "eTextC"
        HidePanel()
    End Sub

    Private Sub PictureBox_Move_Click(sender As Object, e As EventArgs) Handles PictureBox_Move.Click
        Mode = "Move"
        HidePanel()
    End Sub

    Private Sub PictureBox_Button_Click(sender As Object, e As EventArgs) Handles PictureBox_Button.Click
        Mode = "eButton1"
        HidePanel()
    End Sub

    Private Sub PictureBox_Switch_Click(sender As Object, e As EventArgs) Handles PictureBox_Switch.Click
        Mode = "eSwitch1"
        HidePanel()
    End Sub

    Public Sub Delete(num As Integer)
        Dim eComp As EComponent
        eComp = Elements(num)
        eComp.component.Dispose()
        Elements(num) = Nothing
        DoNeedSave()
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If Mode = "MoveMe" Then
            If Not e.Control Then
                If moveObject.GetType.ToString = "eScheme.eBorderText" Then
                    Dim bt As eBorderText = moveObject
                    bt.Ctrl = False
                End If
            End If
        End If
        If e.KeyCode = Keys.F12 And e.Shift Then
            Dim aPath As String
            aPath = FileName
            If aPath <> "" Then
                Dim i As Integer
                i = aPath.LastIndexOf("\")
                If i <> 0 Then
                    aPath = aPath.Substring(0, i + 1)
                End If
            End If
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

                img.Save(aPath + f.number & "_лист" & f.list & ".png", Drawing.Imaging.ImageFormat.Png)
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

                img.Save(aPath + f.number & "_лист" & f.list & ".png", Drawing.Imaging.ImageFormat.Png)
                zx = 0
                zy = 0
            End If
            ProgressBar.Visible = True
            ProgressBar.Maximum = 100
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
            CheckBox2.Checked = True
            Me.Cursor = Cursors.Default
        End If
        If e.KeyCode = Keys.F5 Then
            Mode = ""
            GroupBox1.Visible = True
            CheckBox2.Visible = True
            Me.Cursor = Cursors.Default
            If f.Batt > 0 Then
                Dim eComp As EComponent = Elements(f.Batt)
                Dim bat As eBat = eComp.component
                pointsInProcessUI.Clear()
                bat.CheckUI(0, 0)
            End If
        End If
        If e.KeyCode = Keys.F4 Then
            Mode = lastMode
            HidePanel()
        End If
        If e.KeyCode = Keys.R Then
            If Mode = "newFuseH" Then
                Mode = "newFuseV"
                Me.Cursor = FuseV_cur
            ElseIf Mode = "newFuseV" Then
                Mode = "newFuseH"
                Me.Cursor = FuseH_cur
            End If
            If Mode = "eResist4" Then
                Mode = "eResist1"
                Me.Cursor = R1_cur
            ElseIf Mode = "eResist3" Then
                Mode = "eResist4"
                Me.Cursor = R4_cur
            ElseIf Mode = "eResist2" Then
                Mode = "eResist3"
                Me.Cursor = R3_cur
            ElseIf Mode = "eResist1" Then
                Mode = "eResist2"
                Me.Cursor = R2_cur
            End If
            If Mode = "eButton2" Then
                Mode = "eButton1"
                Me.Cursor = BuH_cur
            ElseIf Mode = "eButton1" Then
                Mode = "eButton2"
                Me.Cursor = BuV_cur
            End If
            If Mode = "eLamp2" Then
                Mode = "eLamp1"
                Me.Cursor = LampH_cur '***********
            ElseIf Mode = "eLamp1" Then
                Mode = "eLamp2"
                Me.Cursor = LampV_cur '***********
            End If
            If Mode = "eSwitch2" Then
                Mode = "eSwitch1"
                Me.Cursor = BuH_cur
            ElseIf Mode = "eSwitch1" Then
                Mode = "eSwitch2"
                Me.Cursor = BuV_cur
            End If
            If Mode <> "Delete" And Mode <> "Move" And Mode <> "createConnect" And Mode <> "" And Mode <> "newPoint" Then
                lastMode = Mode
            End If
        End If
        If e.KeyCode = Keys.M Then
            Mode = "Move"
            HidePanel()
        End If
        If e.KeyCode = Keys.D Then
            Mode = "Delete"
            HidePanel()
        End If
        If e.KeyCode = Keys.C Then
            Mode = "createConnect"
            HidePanel()
        End If
        If e.KeyCode = Keys.P Then
            Mode = "newPoint"
            HidePanel()
        End If
        If e.KeyCode = Keys.Z And e.Control Then
            Undo()
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
            q = MsgBox("Сохранить изменения в схеме?", MsgBoxStyle.YesNoCancel, "Предупреждение")
            If q = vbYes Then
                'сохранение
                FileSave()
            End If
            If q = vbCancel Then
                e.Cancel = True
                Exit Sub
            End If
        End If
        Dim dict As New ArrayList From {
                color0,
                colorM,
                color15,
                color30,
                bordColor,
                txtColor,
                commentColor,
                stopAtRMClick,
                openWithSize,
                Udefault,
                Rdefault,
                RLampdefault,
                FUSEdefault,
                RELEdefault
            }
        Dim fStream As New FileStream(fileParam, FileMode.Create, FileAccess.Write)
        Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
        myBinaryFormatter.Serialize(fStream, dict)
        fStream.Close()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim a, b As Integer
        a = Me.Height
        a = CInt((a - 20) / 2)
        b = Me.Width
        b = CInt((b - 250) / 2)
        ProgressBar.Location = New System.Drawing.Point(b, a)
    End Sub

    Private Sub СкрытьКомментарииToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СкрытьКомментарииToolStripMenuItem.Click
        ShowComments(False)
    End Sub

    Private Sub ShowComments(show As Boolean)
        f.showComments = show
        Dim eComp As EComponent
        For i = 1 To Elements.Count - 1
            eComp = Elements(i)
            If Not (eComp Is Nothing) Then
                If eComp.aType = "eTextC" Then
                    Dim t As eTextC = eComp.component
                    t.Visible = show
                End If
            End If


        Next
    End Sub

    Private Sub ПоказатьКомментарииToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ПоказатьКомментарииToolStripMenuItem.Click
        ShowComments(True)
    End Sub

    Public Function OnConnect(n1 As Integer, n2 As Integer) As Boolean
        isCycle = False
        LabelSig.BackColor = Color.Red
        Application.DoEvents()
        isChanging = True
        Dim eComp As EComponent
        Dim p01, p02 As ILinked
        eComp = Elements(n1)
        p01 = eComp.component
        eComp = Elements(n2)
        p02 = eComp.component
        'Dim p01C As Integer = p01.Condition 'Текущие значения Condition на точках
        'Dim p02C As Integer = p02.Condition
        pointsInProcessSig.Clear()
        Dim p01Ck As Integer = p01.chk_Sig 'Текущие значения CheckSig на точках
        pointsInProcessSig.Clear()
        Dim p02Ck As Integer = p02.chk_Sig

        'НИЖЕ ВСЯ ЛОГИКА СОЕДИНЕНИЯ
        If p01Ck = p02Ck Then
            p01.addLink(n2)
            p02.addLink(n1)
            'Для проверки на замкнутость контура
            pointsInProcessSig.Clear()
            p01Ck = p01.chk_Sig
        Else
            If p01Ck = 0 Then
                pointsInProcessSig.Clear()
                p01.Changee(n2, p02Ck)
            ElseIf p02Ck = 0 Then
                pointsInProcessSig.Clear()
                p02.Changee(n1, p01Ck)
            Else
                MsgBox("Замыкание цепь " + CStr(p01Ck) + " и цепь " + CStr(p02Ck) + vbCrLf + "", vbCritical, "Ошибка в схеме")
                Return False
            End If
            p01.addLink(n2)
            p02.addLink(n1)
        End If
        LabelSig.BackColor = Color.White
        isChanging = False
        If f.Batt > 0 Then
            eComp = Elements(f.Batt)
            Dim bat As eBat = eComp.component
            pointsInProcessUI.Clear()
            bat.CheckUI(0, 0)
        End If
        Return True
    End Function

    Public Sub DisConnect(n1 As Integer, n2 As Integer)
        isCycle = False
        LabelSig.BackColor = Color.Red
        Application.DoEvents()
        If isChanging Then
            MsgBox("Одновременно более двух процессов пытаются получить доступ к функции Public Sub DisConnect(n1 As Integer, n2 As Integer)")
        End If
        If isCheckUI Then
            Do While isCheckUI
                Application.DoEvents()
            Loop
        End If
        isChanging = True
        Dim eComp As EComponent
        Dim p01, p02 As ILinked
        eComp = Elements(n1)
        p01 = eComp.component
        eComp = Elements(n2)
        p02 = eComp.component
        'Dim p01C As Integer = p01.Condition 'Текущие значения Condition на точках мб в iLinked добавить getCondition============================
        'Dim p02C As Integer = p02.Condition
        p01.remLink(n2) 'разъединяем точки
        p02.remLink(n1)
        pointsInProcessSig.Clear()
        Dim p01Ck As Integer = p01.chk_Sig 'Текущие значения CheckSig на точках
        pointsInProcessSig.Clear()
        Dim p02Ck As Integer = p02.chk_Sig

        If p01Ck = 0 Then 'And p01C <> 0
            pointsInProcessSig.Clear()
            p01.Changee(n2, 0)
            If f.Batt > 0 Then
                pointsInProcessUI.Clear()
                'p01.CheckUI(n2, 0, 0)
            End If
        End If
        If p02Ck = 0 Then ' And p02C <> 0 Then
            pointsInProcessSig.Clear()
            p02.Changee(0, 0)
            If f.Batt > 0 Then
                pointsInProcessUI.Clear()
                'p02.CheckUI(0, 0, 0)
            End If
        End If
        LabelSig.BackColor = Color.White
        isChanging = False
        If f.Batt > 0 Then
            eComp = Elements(f.Batt)
            pointsInProcessUI.Clear()
            Dim bat As eBat = eComp.component
            bat.CheckUI(0, 0)
        End If
    End Sub

    Private Sub PictureBox_Lmp_Click(sender As Object, e As EventArgs) Handles PictureBox_Lmp.Click

    End Sub

    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim sd As New SortedDictionary(Of Integer, String)
    '    For i = 0 To TextBox1.Lines.Count - 1
    '        Dim n As Integer = NumWords(TextBox1.Lines(i))
    '        Dim s As String = TextBox1.Lines(i)
    '        If sd.ContainsKey(n) Then
    '            s += vbCrLf + sd(n)
    '            sd(n) = s
    '        Else
    '            sd.Add(n, s)
    '        End If

    '    Next
    '    TextBox1.Clear()
    '    For Each s As String In sd.Values
    '        TextBox1.Text += s + vbCrLf
    '    Next
    'End Sub

    'Public Function NumWords(s As String) As Integer
    '    Dim k As Integer
    '    For i = 0 To s.Length - 1
    '        If s(i) = " " Then
    '            k += 1
    '        End If
    '    Next
    '    Return k + 1
    'End Function

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Mode = "MoveMe" Then
            If e.Control Then
                If moveObject.GetType.ToString = "eScheme.eBorderText" Then
                    Dim bt As eBorderText = moveObject
                    bt.Ctrl = True
                End If
            End If
        End If
    End Sub

    Private Sub ОпрограммеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОпрограммеToolStripMenuItem.Click
        MsgBox("Ознакомительная версия." + vbCrLf + "Для Алексея Владимировича :)")
    End Sub

    Private Sub PictureBox_Lmp_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_Lmp.MouseEnter
        PictureBox_Lmp.Visible = False
    End Sub

    Private Sub PictureBox_Lamp_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_Lamp.MouseLeave
        PictureBox_Lmp.Visible = True
    End Sub

    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    '    search(New DirectoryInfo(Application.StartupPath), "*")
    'End Sub

    'Sub search(dir As DirectoryInfo, find As String)
    '    For Each fileItem In dir.GetFiles(find)
    '        ListBox1.Items.Add(fileItem)
    '    Next
    '    Dim dirItem As DirectoryInfo
    '    For Each dirItem In dir.GetDirectories
    '        search(dirItem, find)
    '    Next
    'End Sub

    Public Sub DoNeedSave()
        NeedSave = True
        If isUndo Then
            Exit Sub
        End If


        Dim saveArray As New ArrayList
        Dim eComp As EComponent
        Dim e As IConnectable
        saveArray.Add(f)

        Dim count As Integer = Elements.Count - 1

        For i = 1 To Elements.Count - 1
            eComp = Elements(i)
            If eComp Is Nothing Then
                saveArray.Add(Nothing)
            Else
                e = eComp.component
                Dim arr As New ArrayList
                arr = e.ForSave
                saveArray.Add(arr)
            End If
        Next

		unDoArray.Push(saveArray)
		'If unDoArray.Count > 30 Then
		'          unDoArray.RemoveAt(0)
		'      End If
		'ShowUnDoArray()
	End Sub

    Private Sub НастройкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НастройкиToolStripMenuItem.Click
        FormOptions.Visible = True
    End Sub

    Sub Undo()
        Dim saveArray As ArrayList

		Dim index As Integer = unDoArray.Count - 1 'Продумать
		'If index > 0 Then
		'    unDoArray.RemoveAt(index)
		'End If

		'index = unDoArray.Count - 1

		If index < 0 Then Exit Sub
        isUndo = True
		saveArray = unDoArray.Pop

		TextBox1.Text = unDoArray.Count.ToString + vbCrLf + TextBox1.Text
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
        pointsInProcessSig.Clear()
        pointsInProcessUI.Clear()



        f = saveArray(0)
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
                    Dim line As New eLine(aComp(2), aComp(3), aComp(4), aComp(5), aComp(1), aComp(8), aComp(9)) With {
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
                    Dim bat As New eBat(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5))
                    eComp = New EComponent With {
                        .aType = "eBat",
                        .numInArray = bat.num,
                        .component = bat
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(bat)
                End If
                'eR
                If aComp(0) = "eResist" Then
                    Dim res As New EResist(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6), aComp(7))
                    eComp = New EComponent With {
                        .aType = "eResist",
                        .numInArray = res.num,
                        .component = res
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(res)
                End If
                'eLamp
                If aComp(0) = "eLamp" Then
                    Dim la As New eLamp(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6))
                    eComp = New EComponent With {
                        .aType = "eLamp",
                        .numInArray = la.num,
                        .component = la
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(la)
                End If
                'eButton
                If aComp(0) = "eButton" Then
                    Dim but As New eButton(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6))
                    eComp = New EComponent With {
                        .aType = "eButton",
                        .numInArray = but.num,
                        .component = but
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(but)
                End If
                'eSwitch
                If aComp(0) = "eSwitch" Then
                    Dim but As New eSwitch(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5))
                    eComp = New EComponent With {
                        .aType = "eSwitch",
                        .numInArray = but.num,
                        .component = but
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(but)
                End If
                'eFuse
                If aComp(0) = "eFuse" Then
                    Dim fu As New eFuse(aComp(2), aComp(3), aComp(1), aComp(4), aComp(5), aComp(6), aComp(7))
                    eComp = New EComponent With {
                        .aType = "eFuse",
                        .numInArray = fu.num,
                        .component = fu
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(fu)
                End If
                'eBText
                If aComp(0) = "eBText" Then
                    Dim bt As New eBorderText(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eBText",
                        .numInArray = bt.num,
                        .component = bt
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(bt)
                End If
                'eText
                If aComp(0) = "eText" Then
                    Dim t As New eText(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eText",
                        .numInArray = t.num,
                        .component = t
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(t)
                End If
                'eTextC
                If aComp(0) = "eTextC" Then
                    Dim t As New eTextC(aComp(1), aComp(2), aComp(3), aComp(4))
                    eComp = New EComponent With {
                        .aType = "eTextC",
                        .numInArray = t.num,
                        .component = t
                    }
                    Elements.Add(eComp)
                    Me.Controls.Add(t)
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
            End If 'Этот кусок при изменении перекопировать в Undo
        Next
        ShowComments(f.showComments)
        TextBox1.Text = unDoArray.Count.ToString + vbCrLf + TextBox1.Text
        'unDoArray.RemoveAt(index)
        isUndo = False
        TextBox1.Text = unDoArray.Count.ToString + vbCrLf + TextBox1.Text
    End Sub

    Sub ShowUnDoArray()
        TextBox1.Text = ""
        For i = 0 To unDoArray.Count - 1
            Dim anArray As New ArrayList
            anArray = unDoArray(i)
            Dim el As New ArrayList
            For j = 1 To anArray.Count - 1
                el = anArray(j)
                If el Is Nothing Then
                    GoTo nth
                End If
                If el(0) = "ePoint" Then
                    Dim n As Integer = el(1)
                    Dim lnk As New ArrayList
                    lnk = el(4)
                    Dim s As String = ""
                    s += "p" + CStr(n) + ": "
                    For k = 0 To lnk.Count - 1
                        s += CStr(lnk(k)) + ", "
                    Next
                    'TextBox1.Text = s + vbCrLf + TextBox1.Text
                End If
                If el(0) = "eLine" Then
                    Dim n As Integer = el(1)
                    Dim lnk As New ArrayList
                    lnk = el(6)
                    Dim s As String = ""
                    s += "L" + CStr(n) + ": "
                    For k = 0 To lnk.Count - 1
                        s += CStr(lnk(k)) + ", "
                    Next
                    'TextBox1.Text = s + vbCrLf + TextBox1.Text
                End If
nth:
            Next
            'TextBox1.Text = "--------------" + vbCrLf + TextBox1.Text
        Next
    End Sub
End Class
