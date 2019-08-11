Imports eScheme

Public Class eRepo
    Implements IConnectable
	Implements ISetValue
	Implements IMovable
	Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public time As Integer
    Public work As Integer
    Public Ia As Single
    Public loc As Integer = 1
    Private plus As Integer = 0
    Private minus As Integer = 0
    Private rr As Integer = 0
    Private ar As Integer = 0
    Private sigL As Boolean = False
	Private sigR As Boolean = False
	Private Timer1Enabled = False

	Dim r21 As Boolean = False
	Dim r22 As Boolean = False


	Dim m_Y As Integer
    Dim m_X As Integer

	Public Sub New(rx As Integer, ry As Integer, n As Integer, time_ As Single, work_ As Integer, ia_ As Single, locat As Integer, pls As Integer, mns As Integer, rr_ As Integer, ar_ As Integer, sL As Boolean, sR As Boolean)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		If time_ < 200 Then
			time = 200
		ElseIf time_ > 2000 Then
			time = 2000
		Else
			time = CInt(time_)
		End If
		'time = 1000
		work = work_
		Ia = ia_
		loc = locat
		plus = pls
		minus = mns
		rr = rr_
		ar = ar_
		sigL = sL
		sigR = sR

		Cursor = Form1.element_cur
		Dim eComp As EComponent

		If loc = 1 Then
			Me.Location = New Point(X - 10, Y + 5) 'Для настройки положения
			Me.Height = 30
			Me.Width = 180
			pb1.Visible = True
			pb1.Location = New Point(0, 0)
			pb0.Size = New Size(170, 8)
			pb0.Location = New Point(5, 18)
		ElseIf loc = 3 Then
			Me.Height = 30
			Me.Width = 180
			Me.Location = New Point(X - 10, Y - 35) 'Для настройки положения
			pb1.Visible = True
			pb1.Location = New Point(0, 15)
			pb0.Size = New Size(170, 8)
			pb0.Location = New Point(5, 4)
		ElseIf loc = 2 Then
			Me.Height = 180
			Me.Width = 30
			Me.Location = New Point(X + 5, Y - 170) 'Для настройки положения
			pb2.Visible = True
			pb2.Location = New Point(0, 0)
			pb0.Size = New Size(8, 170)
			pb0.Location = New Point(18, 5)
		ElseIf loc = 4 Then
			Me.Height = 180
			Me.Width = 30
			Me.Location = New Point(X - 35, Y - 170) 'Для настройки положения
			pb0.Location = New Point(5, 4)
			pb0.Width = 30
			pb0.Height = 15
			pb2.Visible = True
			pb2.Location = New Point(15, 0)
			pb0.Size = New Size(8, 170)
			pb0.Location = New Point(4, 5)
		End If

		If Form1.Mode = "eRepo1" Or Form1.Mode = "eRepo3" Then 'Только при создании, при открытии файла не делать

			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 20, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 3
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 40, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 4
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 60, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 5
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 80, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 6
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 100, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 7
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 120, Y, eComp.numInArray)
			p.links.Add(-1)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 8
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 140, Y, eComp.numInArray)
			p.links.Add(-1)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 9
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 160, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p
		End If
		If Form1.Mode = "eRepo2" Or Form1.Mode = "eRepo4" Then 'Только при создании, при открытии файла не делать

			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 3
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 40, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 4
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 60, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 5
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 80, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 6
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 100, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 7
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 120, eComp.numInArray)
			p.links.Add(-1)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 8
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 140, eComp.numInArray)
			p.links.Add(-1)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 9
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 160, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p
		End If
		PaintMe()
	End Sub

	Sub PaintMe()
        Timer1.Interval = time
		If work = 0 Then
			pb0.BackColor = Color.Silver
			Timer1Enabled = False
			ToolTip1.SetToolTip(pb0, "Питание не подключено!")
			ToolTip1.SetToolTip(Me, "Питание не подключено!")
		ElseIf work = 1 Then
			pb0.BackColor = Color.LightGreen
			Timer1Enabled = False
			ToolTip1.SetToolTip(pb0, "Подключено питание.")
			ToolTip1.SetToolTip(Me, "Подключено питание.")
		ElseIf work = 2 Then
			pb0.BackColor = Color.Green
			Timer1Enabled = False
			ToolTip1.SetToolTip(pb0, "Рабочий режим.")
			ToolTip1.SetToolTip(Me, "Рабочий режим.")
		ElseIf work > 20 Then
			pb0.BackColor = Color.Green
			Timer1.Enabled = True
			Timer1Enabled = True
			ToolTip1.SetToolTip(pb0, "Рабочий режим. Активен.")
			ToolTip1.SetToolTip(Me, "Рабочий режим. Активен.")
		ElseIf work = 3 Then
            pb0.BackColor = Color.Red
			Timer1.Enabled = True
			Timer1Enabled = True
			ToolTip1.SetToolTip(pb0, "Аварийный режим.")
			ToolTip1.SetToolTip(Me, "Аварийный режим.")
		ElseIf work = -1 Then
            pb0.BackColor = Color.Black
			Timer1Enabled = False
			ToolTip1.SetToolTip(pb0, "Неверное подключение устройства!")
			ToolTip1.SetToolTip(Me, "Неверное подключение устройства!")
		End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		Dim plusConn As Boolean = False
		Dim eComp As EComponent
        Dim p01 As EPoint
        eComp = Form1.Elements(num + 4)
		p01 = eComp.component
		Form1.TextBox1.Text = CStr(from - num) + ": " + CStr(condition) + vbCrLf + Form1.TextBox1.Text
		If from = num + 1 Then '         +Un
            If condition >= 15 Then
                plus = 1
                plusConn = True
            ElseIf condition = 0 Then
                plus = 0
            ElseIf condition < 0 Then
                work = -1
                plus = -1
            End If
        End If
        If from = num + 2 Then '         PP
            If condition >= 15 Then
                rr = 1
            ElseIf condition = 0 Then
                rr = 0
            ElseIf condition < 0 Then
                work = -1
                rr = -1
            End If
        End If
        If from = num + 3 Then '         -
            If condition < 0 Then
                minus = 1
            ElseIf condition = 0 Then
                minus = 0
            ElseIf condition > 0 Then
                work = -1
                minus = -1
            End If
        End If
        If from = num + 4 Then '         П
            If condition > 0 Then
                work = -1
            End If
        End If
        If from = num + 5 Then '         ЛБ
            If condition < 0 Then
                r21 = True
            ElseIf condition = 0 Then
                r21 = False
            ElseIf condition > 0 Then
                work = -1
                r21 = False
            End If
        End If
        If from = num + 6 Then '         ПБ
            If condition < 0 Then
                r22 = True
            ElseIf condition = 0 Then
                r22 = False
            ElseIf condition > 0 Then
                work = -1
                r22 = False
            End If
        End If
        If from = num + 9 Then '         AP
            If condition >= 15 And minus = 1 And plus = 1 Then
                ar = 1
            ElseIf condition = 0 Then
                ar = 0
            ElseIf condition < 0 Then
                work = -1
                ar = -1
            End If
        End If

        If work <> -1 Then
            If plus = 1 And minus = 1 Then
                work = 1
                If rr = 1 Then work = 2
                If r21 Then work = 21
                If r22 Then work = 22
                If ar = 1 Then work = 3
            Else
                work = 0
            End If
        End If

        If from < num + 4 Then
            Form1.pointsInProcessSig.Clear()
            If minus = 1 And rr = 1 And plus = 1 Then
                p01.Change(num, -1)
            Else
                p01.Change(num, 0)
				'Timer1.Enabled = False
				'If plusConn Then Form1.DisConnect(num + 1, num + 7)
				'If plusConn Then Form1.DisConnect(num + 1, num + 8)
				'sigL = False
				'sigR = False
				'plusConn = False
			End If
        End If


		'     If work > 20 Or work = 3 Then
		'         Timer1.Enabled = True
		'     Else
		'         Timer1.Enabled = False
		'         If plusConn Then Form1.DisConnect(num + 1, num + 7)
		'If plusConn Then Form1.DisConnect(num + 1, num + 8)
		''
		'Timer1Enabled = False
		''
		'plusConn = False
		'         sigL = False
		'         sigR = False
		'     End If
		PaintMe()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eRepo",
			num,
			X,
			Y,
			CSng(time),
			work,
			Ia,
			loc,
			plus,
			minus,
			rr,
			ar,
			sigL,
			sigR
		}
		Return save
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        If from = num + 4 Then '         
            If minus = 1 And rr = 1 And plus = 1 Then
                Return -1
            End If
        End If
        Return 0
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Return 0
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		If Form1.ProgressBar.Visible Then Exit Sub
		Select Case work
            Case 21
                If sigL Then
					Form1.DisConnect(num + 1, num + 7)
					If Not Timer1Enabled Then Timer1.Enabled = False
				Else
                    Form1.OnConnect(num + 1, num + 7)
                End If
                sigL = Not sigL
            Case 22
                If sigR Then
					Form1.DisConnect(num + 1, num + 8)
					If Not Timer1Enabled Then Timer1.Enabled = False
				Else
                    Form1.OnConnect(num + 1, num + 8)
                End If
                sigR = Not sigR
            Case 3
				If sigL <> sigR Then
					sigL = True
					sigR = True
				End If
				If sigR Then
					Form1.DisConnect(num + 1, num + 8)
					If Not Timer1Enabled Then Timer1.Enabled = False
				Else
					Form1.OnConnect(num + 1, num + 8)
                End If
                sigR = Not sigR
                If sigL Then
					Form1.DisConnect(num + 1, num + 7)
					If Not Timer1Enabled Then Timer1.Enabled = False
				Else
                    Form1.OnConnect(num + 1, num + 7)
                End If
                sigL = Not sigL
			Case Else
				If Not Timer1Enabled Then Timer1.Enabled = False
				Form1.DisConnect(num + 1, num + 7)
				Form1.DisConnect(num + 1, num + 8)

				sigL = False
				sigR = False
		End Select
    End Sub

	Private Sub ERepo_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, pb0.MouseClick, pb1.MouseClick, pb2.MouseClick
		If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
			Form1.Mode = ""
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.CheckBox2.Checked = True
			Form1.Cursor = Cursors.Default
			Exit Sub
		End If
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
			Exit Sub
		End If
		If e.Button = MouseButtons.Right Then
			ContextMenuStrip1.Show(Me, e.X, e.Y)
			Exit Sub
		End If
		If Form1.Mode = "Delete" Then
			Dim wasWork As Integer = work
			work = 0
			PaintMe()
			If wasWork > 2 Then
				Do While sigL Or sigR
					Application.DoEvents()
				Loop
			End If
			Dim eComp As EComponent
			Dim p As EPoint

			For j = 1 To 9
				eComp = Form1.Elements(num + j)
				p = eComp.component 'Вторая точка 
				p.links.Remove(num)
				p.links.Remove(-1)
				p.DeleteMe()
			Next

			If Form1.f.Batt > 0 And wasWork > 2 Then
				eComp = Form1.Elements(Form1.f.Batt)
				Dim bat As eBat = eComp.component
				Form1.pointsInProcessUI.Clear()
				bat.CheckUI(0, 0)
			End If
			Form1.Delete(num)
		End If
	End Sub

	Private Sub ЗадатьСопротивлениеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьСопротивлениеToolStripMenuItem.Click
		Form1.Enabled = False
		DialogForm.Show(Me)
		DialogForm.Left = X + Form1.Left
		DialogForm.Top = Y + Form1.Top
		DialogForm.OnView("Время срабатывания, мс", Me, time.ToString)
	End Sub

	Public Sub SetValue(value As Single) Implements ISetValue.SetValue
		If value >= 500 And value <= 2000 Then
			time = value
		Else
			If value < 500 Then time = 500
			If value > 2000 Then time = 2000
			MsgBox("Допустимое значение: от 500 до 2 000 мс." + vbCrLf + "Указанное значение " + CStr(value) + " установлено в " + CStr(time) + ".")
		End If
		Timer1.Interval = time
		Form1.DoNeedSave()
	End Sub

	Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
		Form1.moveArray.Add(Me)
		Dim mayMove1, mayMove2, mayMove3, mayMove4, mayMove5, mayMove6, mayMove7, mayMove8, mayMove9 As Boolean
		If from Is Me Then
			Dim m As IMovable
			Dim eComp As EComponent = Form1.Elements(num + 1)
			m = eComp.component
			mayMove1 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 2)
			m = eComp.component
			mayMove2 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 3)
			m = eComp.component
			mayMove3 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 4)
			m = eComp.component
			mayMove4 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 5)
			m = eComp.component
			mayMove5 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 6)
			m = eComp.component
			mayMove6 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 7)
			m = eComp.component
			mayMove7 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 8)
			m = eComp.component
			mayMove8 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 9)
			m = eComp.component
			mayMove9 = m.Move(Me, dX, dY)

			If mayMove1 And mayMove2 And mayMove3 And mayMove4 And mayMove5 And mayMove6 And mayMove7 And mayMove8 And mayMove9 Then
				m_X = X + dX
				m_Y = Y + dY

				Return True
			Else
				Return False
			End If
		Else
			Return False
		End If
	End Function

	Public Function GetXY() As Integer Implements IMovable.GetX, IMovable.GetY
		Throw New NotImplementedException()
	End Function

	Public Sub MoveOK() Implements IMovable.MoveOK
		X = m_X
		Y = m_Y

		If loc = 1 Then
			Me.Location = New Point(X - 10, Y + 5) 'Для настройки положения
		ElseIf loc = 3 Then
			Me.Location = New Point(X - 10, Y - 35) 'Для настройки положения
		ElseIf loc = 2 Then
			Me.Location = New Point(X + 5, Y - 170) 'Для настройки положения
		ElseIf loc = 4 Then
			Me.Location = New Point(X - 35, Y - 170) 'Для настройки положения
		End If
	End Sub
End Class
