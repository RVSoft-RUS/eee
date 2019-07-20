Imports eScheme

Public Class eSwitch3
    Implements IConnectable
	Implements IMovable
	Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public work As Integer '(0, 1 or 2)
    Public loc As Integer = 1

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, work_ As Integer, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        work = work_
        loc = locat
        PaintMe()
        ' 2 cur для кнопки
        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 30) 'Для настройки положения
            Me.Height = 52
            Me.Width = 30
            pb1.Cursor = Form1.BuH_cur
            pb2.Cursor = Form1.BuH_cur
            pb3.Cursor = Form1.BuH_cur
        ElseIf loc = 2 Then
            Me.Height = 30
            Me.Width = 52
            Me.Location = New Point(X - 30, Y - 35) 'Для настройки положения
            pb4.Cursor = Form1.BuV_cur
            pb5.Cursor = Form1.BuV_cur
            pb6.Cursor = Form1.BuV_cur
        End If

        If Form1.Mode = "eSwitch3-1" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 1
            }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num + 3)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y - 20, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 4
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y + 20, eComp.numInArray)
            p.links.Add(-1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eSwitch3-2" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 1
            }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num + 3)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y - 40, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 20, Y - 40, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 4
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y - 40, eComp.numInArray)
            p.links.Add(-1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
    End Sub

    Sub PaintMe()
        If loc = 1 Then
            If work = 0 Then
                pb1.Visible = True
                pb2.Visible = False
                pb3.Visible = False
            End If
            If work = 1 Then
                pb1.Visible = False
                pb2.Visible = True
                pb3.Visible = False
            End If
            If work = 2 Then
                pb1.Visible = False
                pb2.Visible = False
                pb3.Visible = True
            End If
        Else 'loc = 2
            If work = 0 Then
                pb4.Visible = True
                pb5.Visible = False
                pb6.Visible = False
            End If
            If work = 1 Then
                pb4.Visible = False
                pb5.Visible = True
                pb6.Visible = False
            End If
            If work = 2 Then
                pb4.Visible = False
                pb5.Visible = False
                pb6.Visible = True
            End If
        End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub

	Public Sub MoveOK() Implements IMovable.MoveOK
		X = m_X
		Y = m_Y

		If loc = 1 Then
			Me.Location = New Point(X + 5, Y - 30) 'Для настройки положения
		ElseIf loc = 2 Then
			Me.Location = New Point(X - 30, Y - 35) 'Для настройки положения
		End If
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Private Sub ESwitch3_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, pb1.MouseClick, pb2.MouseClick, pb3.MouseClick, pb4.MouseClick, pb5.MouseClick, pb6.MouseClick
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
		End If
		If Form1.Mode = "Delete" Then
			Dim eComp As EComponent = Form1.Elements(num + 1)
			Dim p As EPoint = eComp.component 'Первая точка 
			p.links.Remove(num + 2)
			p.links.Remove(num + 3)
			p.links.Remove(num + 4)
			p.DeleteMe()

			eComp = Form1.Elements(num + 2)
			p = eComp.component 'Вторая точка 
			p.links.Remove(num + 1)
			p.links.Remove(-1)
			p.DeleteMe()

			eComp = Form1.Elements(num + 3)
			p = eComp.component '3 точка 
			p.links.Remove(num + 1)
			p.links.Remove(-1)
			p.DeleteMe()

			eComp = Form1.Elements(num + 4)
			p = eComp.component '4 точка 
			p.links.Remove(num + 1)
			p.links.Remove(-1)
			p.DeleteMe()

			Form1.Delete(num)
			Exit Sub
		End If
		If work = 0 Then
			work = 1
			PaintMe()

			Form1.DisConnect(num + 1, num + 3)
			Form1.OnConnect(num + 1, num + 2)
			Dim eComp As EComponent = Form1.Elements(num + 3)
			Dim p As EPoint = eComp.component
			p.addLink(-1)
			eComp = Form1.Elements(num + 2)
			p = eComp.component
			p.remLink(-1)
		ElseIf work = 1 Then
			If loc = 1 Then
				If e.Y < 30 Then
					work = 0
				Else
					work = 2
				End If
			End If
			If loc = 2 Then
				If e.X < 30 Then
					work = 0
				Else
					work = 2
				End If
			End If
			PaintMe()

			If work = 0 Then
				Form1.DisConnect(num + 1, num + 2)
				Form1.OnConnect(num + 1, num + 3)
				Dim eComp As EComponent = Form1.Elements(num + 2)
				Dim p As EPoint = eComp.component
				p.addLink(-1)
				eComp = Form1.Elements(num + 3)
				p = eComp.component
				p.remLink(-1)
			End If
			If work = 2 Then
				Form1.DisConnect(num + 1, num + 2)
				Form1.OnConnect(num + 1, num + 4)
				Dim eComp As EComponent = Form1.Elements(num + 2)
				Dim p As EPoint = eComp.component
				p.addLink(-1)
				eComp = Form1.Elements(num + 4)
				p = eComp.component
				p.remLink(-1)
			End If
		ElseIf work = 2 Then
			work = 1
			PaintMe()

			Form1.DisConnect(num + 1, num + 4)
			Form1.OnConnect(num + 1, num + 2)
			Dim eComp As EComponent = Form1.Elements(num + 4)
			Dim p As EPoint = eComp.component
			p.addLink(-1)
			eComp = Form1.Elements(num + 2)
			p = eComp.component
			p.remLink(-1)
		End If
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eSwitch3",
			num,
			X,
			Y,
			work,
			loc
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Throw New NotImplementedException()
	End Function

	Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
		Throw New NotImplementedException()
	End Function

	Public Function GetXY() As Integer Implements IMovable.GetX, IMovable.GetY
		Throw New NotImplementedException()
	End Function

	Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
		Form1.moveArray.Add(Me)
		Dim mayMove1, mayMove2, mayMove3, mayMove4 As Boolean
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
			'Form1.TextBox1.Text = "mayMove1=" + CStr(mayMove1) + "  mayMove2=" + CStr(mayMove2) + vbCrLf + Form1.TextBox1.Text
			If mayMove1 And mayMove2 And mayMove3 And mayMove4 Then
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
End Class
