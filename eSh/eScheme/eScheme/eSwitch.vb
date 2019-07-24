Imports eScheme

Public Class eSwitch
    Implements IConnectable
    Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public work As Boolean
    Public loc As Integer = 1

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, work_ As Boolean, locat As Integer)
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
            Me.Height = 32
            Me.Width = 30
            pb1.Cursor = Form1.BuH_cur
            pb2.Cursor = Form1.BuH_cur
        ElseIf loc = 2 Then
            Me.Height = 30
            Me.Width = 32
            Me.Location = New Point(X - 30, Y - 35) 'Для настройки положения
            pb3.Cursor = Form1.BuV_cur
            pb4.Cursor = Form1.BuV_cur
        End If

        If Form1.Mode = "eSwitch1" Then 'Только при создании, при открытии файла не делать

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


        End If
        If Form1.Mode = "eSwitch2" Then 'Только при создании, при открытии файла не делать
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

            Form1.Controls.Add(p)
            eComp.component = p
            p.links.Add(-1)
            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 20, Y - 40, eComp.numInArray)
            p.links.Add(num + 1)
            Form1.Controls.Add(p)
            eComp.component = p

        End If
    End Sub

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

	Private Sub ESwitch_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, pb1.MouseDown, pb2.MouseDown, pb3.MouseDown, pb4.MouseDown
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
		End If
	End Sub

	Private Sub ESwitch_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, pb1.MouseClick, pb2.MouseClick, pb3.MouseClick, pb4.MouseClick
		If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
			Form1.Mode = ""
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.CheckBox2.Checked = True
			Form1.Cursor = Cursors.Default
			Exit Sub
		End If
		'If e.Button = MouseButtons.Right Then
		'    ContextMenu1.Show(Me, e.X, e.Y)
		'    Exit Sub
		'End If
		If Form1.Mode = "Delete" Then
			Dim eComp As EComponent = Form1.Elements(num + 1)
			Dim p As EPoint = eComp.component 'Первая точка 
			p.links.Remove(num + 2)
			p.links.Remove(num + 3)
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

            Form1.Delete(num)
			Exit Sub
		End If
		If Not work Then
			work = True
			Form1.DisConnect(num + 1, num + 3)
            Form1.OnConnect(num + 1, num + 2)
            Dim eComp As EComponent = Form1.Elements(num + 3)
            Dim p As EPoint = eComp.component
            p.addLink(-1)
            eComp = Form1.Elements(num + 2)
            p = eComp.component
            p.remLink(-1)
            PaintMe()
			'Form1.DoNeedSave()
		Else
			work = False
			Form1.DisConnect(num + 1, num + 2)
            Form1.OnConnect(num + 1, num + 3)
            Dim eComp As EComponent = Form1.Elements(num + 2)
            Dim p As EPoint = eComp.component
            p.addLink(-1)
            eComp = Form1.Elements(num + 3)
            p = eComp.component
            p.remLink(-1)
            PaintMe()
			'Form1.DoNeedSave()
		End If
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eSwitch",
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

    Public Function GetX() As Integer Implements IMovable.GetX, IMovable.GetY
        Throw New NotImplementedException()
    End Function

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Form1.moveArray.Add(Me)
        Dim mayMove1, mayMove2, mayMove3 As Boolean
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
            'Form1.TextBox1.Text = "mayMove1=" + CStr(mayMove1) + "  mayMove2=" + CStr(mayMove2) + vbCrLf + Form1.TextBox1.Text
            If mayMove1 And mayMove2 And mayMove3 Then
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
