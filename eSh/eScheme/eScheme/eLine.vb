Imports eScheme

Public Class eLine
	Implements IConnectable, ILinked, IMovable
	Public X1 As Integer
    Public Y1 As Integer
    Public X2 As Integer
    Public Y2 As Integer
    Dim clr As New Color()
    Public links As New ArrayList
    Public Condition_ As Integer
    Public num As Integer
    Public Ia As Single
	Public U As Single

	Dim mX1, mY1, mX2, mY2 As Integer

	'Dim Left As
	Public Sub New(rx1 As Integer, ry1 As Integer, rx2 As Integer, ry2 As Integer, n As Integer, I_ As Single, u_ As Single)
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
        num = n
        If rx1 > rx2 Then
            X1 = rx2
            X2 = rx1
        Else
            X1 = rx1
            X2 = rx2
        End If
        If ry1 > ry2 Then
            Y1 = ry2
            Y2 = ry1
        Else
            Y1 = ry1
            Y2 = ry2
        End If
        Dim dx As Integer = X2 - X1
        Dim dy As Integer = Y2 - Y1
        If dx > 0 Then 'Горизонтально
            Me.Height = 3
            Me.Left = X1 + 5
            Me.Top = Y1 - 1
            Me.Width = X2 - X1 - 10
        End If
        If dy > 0 Then 'Вертикально
            Me.Width = 3
            Me.Top = Y1 + 5
            Me.Left = X1 - 1
            Me.Height = Y2 - Y1 - 10
        End If
        Condition = 0
        Ia = I_
        U = u_
		Cursor = Form1.line_cur
		mX1 = X1
		mX2 = X2
		mY1 = Y1
		mY2 = Y2
	End Sub

    Public Property Condition
        Set(value)
            Condition_ = value
            'Dim s As String = ""
            Select Case Condition_
                Case -1, -2
                    clr = Form1.colorM
                    's = "Масса"
                Case 0
                    clr = Form1.color0
                    's = "Нет сигнала"
                Case 15, 16
                    clr = Form1.color15
                    's = "Сигнал +15"
                    's = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
                Case 30, 31
                    clr = Form1.color30
                    's = "Сигнал +30"
                    's = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
            End Select
			Me.BackColor = clr

			'ToolTip1.SetToolTip(Me, s)
		End Set
        Get
            Return Condition_
        End Get
    End Property

    Public Sub Change(from As Integer, condition_ As Integer) Implements IConnectable.Change
        'Dim s As String = ""
        Select Case condition_
            Case -1, -2
                clr = Form1.colorM
                's = "Масса"
            Case 0
                clr = Form1.color0
                's = "Нет сигнала"
            Case 15, 16
                clr = Form1.color15
                's = "Сигнал +15"
                's = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
            Case 30, 31
                clr = Form1.color30
                's = "Сигнал +30"
                's = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
        End Select

        Me.Condition_ = condition_
		Me.BackColor = clr
		Form1.DoLight()
		'ToolTip1.SetToolTip(Me, s)
		'Отправление дальше
		For i = 0 To links.Count - 1
            If links(i) <> from Then
                If links(i) <> 0 Then
                    'Отправление дальше
                    Dim eComp As EComponent
                    Dim econ As IConnectable
                    eComp = Form1.Elements(links(i))
                    econ = eComp.component
                    econ.Change(num, Me.Condition_)
                    'Form1.TextBox1.Text &= "line " + CStr(num) + " to " + CStr(links(i)) + vbCrLf
                End If
            End If
        Next
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Private Sub ELine_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.N Then
            MsgBox("Номер провода: " + CStr(num))
        End If
        If e.KeyCode = Keys.I Then
            MsgBox("linkA: " + CStr(links(0)) + ";  linkB: " + CStr(links(1)))
        End If
        If e.KeyCode = Keys.Delete Then
            If Form1.Mode = "" Then
                DeleteMe()
            End If
        End If
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eLine",
            num,
            X1,
            Y1,
            X2,
            Y2,
            links,
            Condition_,
            Ia,
            U
        }
        Return save
    End Function

    Private Sub ELine_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If Form1.Mode = "Delete" Then
            DeleteMe()
        End If
        If Form1.Mode = "newPoint" Then
            If X1 = X2 Then
                'Return "V"
                If Form1.ry = Y1 Or Form1.ry = Y2 Then
                    MsgBox("Слишком близко.")
                    Exit Sub
                End If
                Dim eComp As New EComponent With {
                    .aType = "ePoint",
                    .numInArray = Form1.Elements.Count
                }
                Form1.Elements.Add(eComp)
                Dim p As New EPoint(Form1.rx, Form1.ry, eComp.numInArray)
                Form1.Controls.Add(p)
                eComp.component = p
                p.Condition = Condition_

                eComp = New EComponent With {
                    .aType = "eLine",
                    .numInArray = Form1.Elements.Count
                }
                Form1.Elements.Add(eComp)

                Dim line As New eLine(Form1.rx, Form1.ry, X2, Y2, eComp.numInArray, 0, 0)
                Form1.Controls.Add(line)
                eComp.component = line
                line.Condition = Condition_

                Y2 = Form1.ry
                Me.Height = Y2 - Y1 - 10

                eComp = Form1.Elements(links(0))
                Dim p1 As EPoint = eComp.component
                eComp = Form1.Elements(links(1))
                Dim p2 As EPoint = eComp.component
                If p1.Y > p2.Y Then
                    Dim p0 As EPoint = p1
                    p1 = p2
                    p2 = p0
                    'Теперь р2 ссылается на самую нижнюю точку
                End If

                links.Remove(p2.num)
                links.Add(p.num)
                line.links.Add(p2.num)
                p2.links.Remove(num)
                p2.links.Add(line.num)
                p.links.Add(line.num)
                p.links.Add(num)
                line.links.Add(p.num)
            Else
                'Return "H"
                If Form1.rx = X1 Or Form1.rx = X2 Then
                    MsgBox("Слишком близко.")
                    Exit Sub
                End If
                Dim eComp As New EComponent With {
                    .aType = "ePoint",
                    .numInArray = Form1.Elements.Count
                }
                Form1.Elements.Add(eComp)
                Dim p As New EPoint(Form1.rx, Form1.ry, eComp.numInArray)
                Form1.Controls.Add(p)
                eComp.component = p
                p.Condition = Condition_

                eComp = New EComponent With {
                    .aType = "eLine",
                    .numInArray = Form1.Elements.Count
                }
                Form1.Elements.Add(eComp)

                Dim line As New eLine(Form1.rx, Form1.ry, X2, Y2, eComp.numInArray, 0, 0)
                Form1.Controls.Add(line)
                eComp.component = line
                line.Condition = Condition_

                X2 = Form1.rx
                Me.Width = X2 - X1 - 10

                eComp = Form1.Elements(links(0))
                Dim p1 As EPoint = eComp.component
                eComp = Form1.Elements(links(1))
                Dim p2 As EPoint = eComp.component
                If p1.X > p2.X Then
                    Dim p0 As EPoint = p1
                    p1 = p2
                    p2 = p0
                    'Теперь р2 ссылается на самую правую точку
                End If

                links.Remove(p2.num)
                links.Add(p.num)
                line.links.Add(p2.num)
                p2.links.Remove(num)
                p2.links.Add(line.num)
                p.links.Add(line.num)
                p.links.Add(num)
                line.links.Add(p.num)
            End If
            Form1.Mode = ""
            Form1.GroupBox1.Visible = True
            Form1.CheckBox2.Visible = True
            Form1.Cursor = Cursors.Default
            'Form1.DoNeedSave()
        End If

    End Sub

    Sub DeleteMe()
        'Линию любую можно удалить, предваритьльно обнулив ссылки на нее из точек
        Dim n1, n2 As Integer
        Dim eComp As EComponent = Form1.Elements(links(0))
        Dim p As EPoint = eComp.component 'Первая точка 
        p.links.Remove(num)
        n1 = p.num

        eComp = Form1.Elements(links(1))
        p = eComp.component 'Вторая точка
        p.links.Remove(num)
        n2 = p.num


		'Изменения Change добавить ++++++++++++++++++++++++++++++++++++++++++++++
		Form1.DisConnect(n1, n2)
		Form1.Delete(num)
	End Sub

    Private Sub ELine_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Me.BackColor = Color.Aqua
    End Sub

    Private Sub ELine_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.BackColor = clr
    End Sub

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Try
            Dim linkForCheck As Integer
            For i = 0 To links.Count - 1
                If links(i) <> from Then
                    If links(i) <> 0 Then
                        linkForCheck = links(i)
                    End If
                End If
            Next
            'Отправление дальше
            'Form1.TextBox1.Text &= "check from line " + CStr(num) + " to " + CStr(linkForCheck) + vbCrLf
            Dim eComp As EComponent
            Dim econ As IConnectable
            eComp = Form1.Elements(linkForCheck)
            econ = eComp.component
            Return econ.CheckSig(num)
        Catch ex As Exception
            MsgBox("Не допускаестся создание замкнутых контуров." +
                   vbCrLf + "Удалите лишние связи.", vbCritical, "Ошибка в схеме #Sig_Line")
            Return 0
        End Try

    End Function

    Public Function Loc() As String
        If X1 = X2 Then
            Return "V"
        Else
            Return "H"
        End If
    End Function

    Private Sub ELine_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If Form1.Mode = "newPoint" Then
            Cursor = Form1.point_cur
            ToolTip1.SetToolTip(Me, "Добавить узел")

        Else
            Cursor = Form1.line_cur
            Dim s As String = ""
            Select Case Condition_
                Case -1, -2
                    clr = Form1.colorM
                    s = "Масса"
                Case 0
                    clr = Form1.color0
                    s = "Нет сигнала"
                    s = "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
                Case 15, 16
                    clr = Form1.color15
                    s = "Сигнал +15"
                    s = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
                Case 30, 31
                    clr = Form1.color30
                    s = "Сигнал +30"
                    s = s + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A" + vbCrLf + "Напряжение " + CStr(Math.Round(U, 3)) + " В"
            End Select

            Me.Condition_ = Condition_
            Me.BackColor = clr
            ToolTip1.SetToolTip(Me, s)
        End If
    End Sub

    Public Function CheckUI(from As Integer, U_ As Single, Optional t As Integer = 0) As Single Implements IConnectable.CheckUI
        For j = 0 To links.Count - 1
            If links(j) <> from Then
                Dim eComp As EComponent = Form1.Elements(links(j))
                Dim p1 As EPoint = eComp.component
                Ia = p1.CheckUI(num, U_, t)
                U = U_ - Ia * t
                Return Ia
            End If
        Next
        Return Ia
    End Function

    Public Sub addLink(n As Integer) Implements ILinked.addLink
        links.Add(n)
    End Sub

    Public Sub remLink(n As Integer) Implements ILinked.remLink
        links.Remove(n)
    End Sub

    Public Function chk_Sig() As Integer Implements ILinked.chk_Sig
        Return CheckSig(0)
    End Function

    Public Sub Changee(from As Integer, condition_ As Integer) Implements ILinked.Changee
        Change(from, condition_)
    End Sub

	Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
		Form1.moveArray.Add(Me)
		Dim mayMove As Boolean = True
		Dim nx As Integer = X2 - X1
        Dim ny As Integer = Y2 - Y1
        mX1 = X1
        mX2 = X2
        mY1 = Y1
        mY2 = Y2
        If nx > 0 Then 'Горизонтально
			If dX <> 0 And dY = 0 Then
                If from Is Me Then
                    'Dim m As IMovable
                    'For j = 0 To links.Count - 1
                    '	Dim eComp As EComponent = Form1.Elements(links(j))
                    '	m = eComp.component
                    '	mayMove = m.Move(Me, 0, dY)
                    'Next
                    Return True
                End If
                If from.GetX = X1 Then 'Тянем за левый конец
					If X1 + dX >= X2 Then Return False
					mX1 = X1 + dX
					Return True
				End If
				If from.GetX = X2 Then 'Тянем за praвый конец
					If X2 + dX <= X1 Then Return False
					mX2 = X2 + dX
					Return True
				End If
			End If
			If dY <> 0 And dX = 0 Then
				Dim m As IMovable
				For j = 0 To links.Count - 1
					Dim eComp As EComponent = Form1.Elements(links(j))
					m = eComp.component
					If Not (m Is from) Then
						mayMove = m.Move(Me, dX, dY)
						If Not mayMove Then
							Return False
						End If
					End If
				Next
				If mayMove Then
					mY1 = Y1 + dY
					mY2 = Y2 + dY
					Return True
				Else
					Return False
				End If
				Return False
			End If
		End If
		If ny > 0 Then 'Вертикально
			If dY <> 0 And dX = 0 Then
				'Form1.TextBox2.Text = "dx=" + CStr(dX) + " dy=" + CStr(dY) + vbCrLf + Form1.TextBox2.Text
				If from Is Me Then
					Return True
				End If
				If from.GetY = Y1 Then 'Тянем за верхний конец
					If Y1 + dY >= Y2 Then Return False
					mY1 = Y1 + dY
					Return True
				End If
				If from.GetY = Y2 Then 'Тянем за нижний конец
					If Y2 + dY <= Y1 Then Return False
					mY2 = Y2 + dY
					Return True
				End If
			End If
			If dY = 0 And dX <> 0 Then
				Dim m As IMovable
				For j = 0 To links.Count - 1
					Dim eComp As EComponent = Form1.Elements(links(j))
					m = eComp.component
					If Not (m Is from) Then
						mayMove = m.Move(Me, dX, dY)
						If Not mayMove Then
							Return False
						End If
					End If
				Next
				If mayMove Then
					mX1 = X1 + dX
					mX2 = X2 + dX
					Return True
				Else
					Return False
				End If
				Return False
			End If
		End If
		Return False
	End Function

	Public Function GetX() As Integer Implements IMovable.GetX
		Throw New NotImplementedException()
	End Function

	Public Function GetY() As Integer Implements IMovable.GetY
		Throw New NotImplementedException()
	End Function

	Private Sub ELine_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
		End If
	End Sub

	Public Sub MoveOK() Implements IMovable.MoveOK
		Dim d_x As Integer = X2 - X1
        Dim d_y As Integer = Y2 - Y1
        X1 = mX1
        X2 = mX2
        Y2 = mY2
        Y1 = mY1
        If d_x > 0 Then 'Горизонтально
            Me.Height = 3
            Me.Left = X1 + 5
            Me.Top = Y1 - 1
            Me.Width = X2 - X1 - 10
        End If
        If d_y > 0 Then 'Вертикально
            Me.Width = 3
            Me.Top = Y1 + 5
            Me.Left = X1 - 1
            Me.Height = Y2 - Y1 - 10
        End If
    End Sub
End Class
