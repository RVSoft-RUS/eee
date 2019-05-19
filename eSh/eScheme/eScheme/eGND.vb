Imports eScheme

Public Class EGND
	Implements IConnectable
	Implements ILinked
	Implements IMovable
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	Public link As Integer
	Dim m_Y As Integer
	Dim m_X As Integer


	Public Sub New(rx As Integer, ry As Integer, n As Integer)
		InitializeComponent()
		X = rx
		Y = ry
		m_X = X
		m_Y = Y
		num = n
		Cursor = Form1.gnd_cur
		Dim eComp As EComponent
		Dim econ As IConnectable
		Me.Location = New Point(X - 8, Y + 5) 'Для настройки положения

		If Form1.Mode = "eGND" Then 'Только при создании, при открытии файла не делать
			link = num + 1
			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 1)
			econ = eComp.component
			econ.Change(num, -1)
			'Form1.TextBox1.Text &= "eGND #" + CStr(num) + " changed (1) point" + vbCrLf
		End If
	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		If condition <> -1 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "GND")
		End If
	End Sub

	Public Sub addLink(n As Integer) Implements ILinked.addLink
		'
	End Sub

	Public Sub remLink(n As Integer) Implements ILinked.remLink
		'
	End Sub

	Public Sub Changee(from As Integer, condition_ As Integer) Implements ILinked.Changee
		If condition_ <> -1 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "GND")
		End If
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Private Sub EGND_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
		If Form1.Mode = "Delete" Then
			Form1.DisConnect(num, num + 1)
			Dim eComp As EComponent = Form1.Elements(num + 1)
			Dim p As EPoint = eComp.component 'Первая точка 
			p.links.Remove(num)
			If p.links.Count = 0 Then
				p.DeleteMe()
			End If

			'eComp = Form1.Elements(num + 2)
			'p = eComp.component 'Первая точка 
			'p.links.Remove(num)
			'If p.links.Count = 0 Then
			'    p.DeleteMe()
			'End If
			If Form1.f.Batt > 0 Then
				eComp = Form1.Elements(Form1.f.Batt)
				Dim bat As eBat = eComp.component
				Form1.pointsInProcessUI.Clear()
				bat.CheckUI(0, 0)
			End If
			Form1.Delete(num)
		End If
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eGND",
			num,
			X,
			Y,
			link
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Return -1
	End Function

	Public Function CheckUI(from As Integer, U As Single, Optional t As Integer = 0) As Single Implements IConnectable.CheckUI
		Return 0
	End Function

	Public Function chk_Sig() As Integer Implements ILinked.chk_Sig
		Return -1
	End Function

	Public Function GetX() As Integer Implements IMovable.GetX
		Throw New NotImplementedException()
	End Function

	Public Function GetY() As Integer Implements IMovable.GetY
		Throw New NotImplementedException()
	End Function

	Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
		Form1.moveArray.Add(Me)
		Dim mayMove As Boolean = True
		If from Is Me Then
			Dim m As IMovable
			Dim eComp As EComponent = Form1.Elements(link)
			m = eComp.component
			mayMove = m.Move(Me, dX, dY)
			If mayMove Then
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

	Private Sub EGND_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
		If Form1.Mode = "Move" Then
			Form1.moveObject = Me
			Form1.Cursor = Cursors.SizeAll
			Form1.moveXstart = Form1.rx
			Form1.moveYstart = Form1.ry
			Form1.Mode = "MoveMe"
		End If
	End Sub

	Public Sub MoveOK() Implements IMovable.MoveOK
		X = m_X
		Y = m_Y
		Me.Location = New Point(X - 8, Y + 5)
	End Sub
End Class
