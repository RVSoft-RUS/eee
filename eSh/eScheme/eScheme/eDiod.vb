Imports eScheme

Public Class eDiod
    Implements IConnectable
	'Implements ISetValue
	Implements IMovable
	Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public R As Single
    Public work As Boolean
    Public Ia As Single
    Public loc As Integer = 1
    Private c1 As Integer = 0
    Private c2 As Integer = 0

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Integer, work_ As Boolean, ia_ As Single, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        R = r_
        R = 0
        work = work_
        Ia = ia_
        loc = locat

        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
            Cursor = Form1.d1_cur
        ElseIf loc = 3 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
            Cursor = Form1.d3_cur
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
            Cursor = Form1.d2_cur
        ElseIf loc = 4 Then
            Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
            Cursor = Form1.d4_cur
        End If

        PaintMe()

        If Form1.Mode = "eDiod1" Then 'Только при создании, при открытии файла не делать
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
        If Form1.Mode = "eDiod3" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eDiod2" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y + 40, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eDiod4" Then 'Только при создании, при открытии файла не делать
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
            p = New EPoint(X, Y + 40, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
    End Sub

    Sub PaintMe()
        pb1.Visible = False
        pb2.Visible = False
        pb3.Visible = False
        pb4.Visible = False
        If work Then
            If loc = 1 Then
                pb1.Visible = True
            ElseIf loc = 3 Then
                pb3.Visible = True
            ElseIf loc = 2 Then
                pb2.Visible = True
            ElseIf loc = 4 Then
                pb4.Visible = True
            End If
        End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        'Throw New NotImplementedException()
        Dim sig1, sig2 As Integer
        Dim p1, p2 As EPoint
        Dim eComp As EComponent

        If from = num + 1 Then
            eComp = Form1.Elements(num + 2)
            p2 = eComp.component
            Form1.pointsInProcessSig.Clear()
            sig2 = p2.CheckSig(num)
            If condition >= 15 Then 'прилетел +
                If sig2 = 0 Then 'Все норм пускаем + дальше
                    Form1.pointsInProcessSig.Clear()
                    p2.Change(num, condition)
                    work = True
                    PaintMe()
                ElseIf sig2 < 0 Then 'Напоролись на массу
                    work = False
                    PaintMe()
                ElseIf sig2 <> condition Then 'Тоже + но другой и не равный пришедшему
                    MsgBox("Замыкание цепей " + sig2.ToString + " и " + condition.ToString)
                End If
            Else 'прилетел 0 или -
                If sig2 = 0 Then 'Все норм пускаем + дальше
                    Form1.pointsInProcessSig.Clear()
                    p2.Change(num, 0)
                    work = True
                    PaintMe()
                End If
            End If

        End If
        If from = num + 2 Then
            eComp = Form1.Elements(num + 1)
            p1 = eComp.component
            Form1.pointsInProcessSig.Clear()
            sig1 = p1.CheckSig(num)
            If condition < 0 Then
                If sig1 = 0 Then 'Все норм пускаем + дальше
                    Form1.pointsInProcessSig.Clear()
                    p1.Change(num, condition)
                    work = True
                    PaintMe()
                ElseIf sig1 > 0 Then 'Напоролись на +
                    work = False
                    PaintMe()
                End If
            Else
                If sig1 = 0 Then 'Все норм пускаем + дальше
                    Form1.pointsInProcessSig.Clear()
                    p1.Change(num, 0)
                    work = True
                    PaintMe()
                End If
            End If

        End If
    End Sub

    Public Sub MoveOK() Implements IMovable.MoveOK
		X = m_X
		Y = m_Y

		If loc = 1 Then
			Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
		ElseIf loc = 3 Then
			Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
		ElseIf loc = 2 Then
			Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
		ElseIf loc = 4 Then
			Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
		End If
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eDiod",
			num,
			X,
			Y,
			R,
			work,
			Ia,
			loc
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        'Throw New NotImplementedException()
        Dim sig1, sig2 As Integer
        Dim p1, p2 As EPoint
        Dim eComp As EComponent

        If from = num + 1 Then
            eComp = Form1.Elements(num + 2)
            p2 = eComp.component
            Form1.pointsInProcessSig.Clear()
            sig2 = p2.CheckSig(num)
            If sig2 < 0 Then ' -|>| если тут -, то вертаем -
                Return sig2
            Else 'иначе 0
                Return 0
            End If
        Else 'If from = num + 2 Then
            eComp = Form1.Elements(num + 1)
            p1 = eComp.component
            Form1.pointsInProcessSig.Clear()
            sig1 = p1.CheckSig(num)
            If sig1 > 0 Then ' -|<| если тут +, то вертаем +
                Return sig1
            Else 'иначе 0
                Return 0
            End If
        End If

    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        If Form1.isCycle Then
            Return 0
        End If
        If from = num + 1 Then
            Dim eComp As EComponent = Form1.Elements(num + 2)
            If eComp Is Nothing Then Return 0 'Эта строчка нужна при случае, если удалена одна из точек у лампы (при Ctrl-Z)
            Dim p2 As EPoint = eComp.component
            'Dim asd As ArrayList = Form1.pointsInProcessSig
            Form1.pointsInProcessSig.Clear()
            If p2.CheckSig(num) = -1 Then
                Ia = U / (R + r_)
                'Form1.pointsInProcessUI.Clear()
                'p2.CheckUI(num, 0, 0)
                Return Ia
            Else
                Form1.pointsInProcessUI.Clear()
                Ia = p2.CheckUI(num, U, R + r_)
                Return Ia
            End If
        Else
            Return 0
        End If
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

	Private Sub pb4_MouseDown(sender As Object, e As MouseEventArgs) Handles pb1.MouseDown, pb2.MouseDown, pb3.MouseDown, pb4.MouseDown
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
		End If
	End Sub

    Private Sub eDiod_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, pb1.MouseEnter, pb2.MouseEnter, pb3.MouseEnter, pb4.MouseEnter
        ToolTip1.SetToolTip(sender, "Ток через диод " + CStr(Math.Round(Ia, 3)) + " A")
    End Sub
End Class
