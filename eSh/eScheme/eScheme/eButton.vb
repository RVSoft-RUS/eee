Imports eScheme

Public Class eButton
	Implements IConnectable
	Implements ISetValue
	Implements IMovable
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	Public work As Boolean
	Public wTime As Single = 1000
	Public loc As Integer = 1

	Dim m_Y As Integer
	Dim m_X As Integer

	Public Sub New(rx As Integer, ry As Integer, n As Integer, work_ As Boolean, wTime_ As Single, locat As Integer)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		work = work_
		wTime = wTime_
		loc = locat
		PaintMe()
		' 2 cur для кнопки
		Dim eComp As EComponent
		If loc = 1 Then
			Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
			Me.Height = 20
            Me.Width = 30
            pb1.Cursor = Form1.BuH_cur
            pb2.Cursor = Form1.BuH_cur
        ElseIf loc = 2 Then
			Me.Height = 30
			Me.Width = 20
            Me.Location = New Point(X - 15, Y - 35) 'Для настройки положения
            pb3.Cursor = Form1.BuV_cur
            pb4.Cursor = Form1.BuV_cur
        End If

		If Form1.Mode = "eButton1" Then 'Только при создании, при открытии файла не делать

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
            p = New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
			eComp.component = p
		End If
		If Form1.Mode = "eButton2" Then 'Только при создании, при открытии файла не делать
			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y, eComp.numInArray)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X, Y - 40, eComp.numInArray)
			Form1.Controls.Add(p)
			eComp.component = p
		End If

		If work Then
			Timer1.Interval = wTime
			Timer1.Enabled = True
		End If
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eButton",
			num,
			X,
			Y,
			work,
			wTime,
			loc
		}
		Return save
	End Function

	Sub PaintMe()
		If work Then
			If loc = 1 Then
				pb1.Visible = False
				pb2.Visible = True
			End If
			If loc = 2 Then
				pb3.Visible = False
				pb4.Visible = True
			End If
		Else
			If loc = 1 Then
				pb1.Visible = True
				pb2.Visible = False
			End If
			If loc = 2 Then
				pb3.Visible = True
				pb4.Visible = False
			End If
		End If
	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        'Throw New NotImplementedException()
    End Sub

	Public Sub SetValue(value As Single) Implements ISetValue.SetValue
		If value > 10 And value < 30000 Then
			wTime = value
		Else
			If value < 10 Then wTime = 10
			If value > 30000 Then wTime = 30000
            MsgBox("Допустимое значение: от 10 до 30 000 мс." + vbCrLf + "Указанное значение " + CStr(value) + " установлено в " + CStr(wTime) + ".")
        End If

	End Sub

	Public Sub MoveOK() Implements IMovable.MoveOK
		X = m_X
		Y = m_Y

        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 15, Y - 35) 'Для настройки положения
        End If
        Form1.NeedSave = True
    End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Return 0
    End Function

	Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Return 0
    End Function

	Public Function GetX() As Integer Implements IMovable.GetX
		Throw New NotImplementedException()
	End Function

	Public Function GetY() As Integer Implements IMovable.GetY
		Throw New NotImplementedException()
	End Function

	Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
		Form1.moveArray.Add(Me)
		Dim mayMove1, mayMove2 As Boolean
		If from Is Me Then
			Dim m As IMovable
			Dim eComp As EComponent = Form1.Elements(num + 1)
			m = eComp.component
			mayMove1 = m.Move(Me, dX, dY)
			eComp = Form1.Elements(num + 2)
			m = eComp.component
			mayMove2 = m.Move(Me, dX, dY)
			'Form1.TextBox1.Text = "mayMove1=" + CStr(mayMove1) + "  mayMove2=" + CStr(mayMove2) + vbCrLf + Form1.TextBox1.Text
			If mayMove1 And mayMove2 Then
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

	Private Sub EButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, pb1.MouseDown, pb2.MouseDown, pb3.MouseDown, pb4.MouseDown
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
		End If
	End Sub

	Private Sub EButton_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, pb1.MouseClick, pb2.MouseClick, pb3.MouseClick, pb4.MouseClick
		If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
			Form1.Mode = ""
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.CheckBox2.Checked = True
			Form1.Cursor = Cursors.Default
			Exit Sub
		End If
		If e.Button = MouseButtons.Right Then
			ContextMenu1.Show(Me, e.X, e.Y)
			Exit Sub
		End If
		If Form1.Mode = "Delete" Then
			If work Then
				MsgBox("Отключите кнопку, прежде чем ее удалять.")
				Exit Sub
			End If
			Dim eComp As EComponent = Form1.Elements(num + 1)
			Dim p As EPoint = eComp.component 'Первая точка 
			p.links.Remove(num)
			If p.links.Count = 0 Then
				p.DeleteMe()
			End If

			eComp = Form1.Elements(num + 2)
			p = eComp.component 'Вторая точка 
			p.links.Remove(num)
			If p.links.Count = 0 Then
				p.DeleteMe()
			End If
            Form1.Delete(num)
            Exit Sub
        End If
		If Not work Then
			work = True

            Form1.OnConnect(num + 1, num + 2)

            Timer1.Interval = wTime
			Timer1.Enabled = True
			If loc = 1 Then
				pb1.Visible = False
				pb2.Visible = True
			ElseIf loc = 2 Then
				pb3.Visible = False
				pb4.Visible = True
			End If
		End If
	End Sub

	Private Sub EButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, pb1.MouseEnter, pb2.MouseEnter, pb3.MouseEnter, pb4.MouseEnter
		ToolTip1.SetToolTip(pb1, "Время отключения " + CStr(wTime) + " мс")
		ToolTip1.SetToolTip(pb2, "Время отключения " + CStr(wTime) + " мс")
		ToolTip1.SetToolTip(pb3, "Время отключения " + CStr(wTime) + " мс")
		ToolTip1.SetToolTip(pb4, "Время отключения " + CStr(wTime) + " мс")
	End Sub

	Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
		Form1.Enabled = False
		DialogForm.Show(Me)
		DialogForm.Left = X + Form1.Left
		DialogForm.Top = Y + Form1.Top
		DialogForm.OnView("Время отключения, мс", Me, wTime.ToString)
	End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Timer1.Enabled = False
		work = False
		Form1.DisConnect(num + 1, num + 2)
		If loc = 1 Then
			pb2.Visible = False
			pb1.Visible = True
		ElseIf loc = 2 Then
			pb4.Visible = False
			pb3.Visible = True
		End If
	End Sub
End Class
