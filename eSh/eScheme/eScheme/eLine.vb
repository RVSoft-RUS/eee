Imports eScheme

Public Class eLine
	Implements IConnectable
	Public X1 As Integer
	Public Y1 As Integer
	Public X2 As Integer
	Public Y2 As Integer
	Dim clr As New Color()
	Public links As New ArrayList
	Public Condition_ As Integer
	Public num As Integer
	'Dim Left As
	Public Sub New(rx1 As Integer, ry1 As Integer, rx2 As Integer, ry2 As Integer, n As Integer)
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
		Cursor = Form1.line_cur
	End Sub

	Public Property Condition
		Set(value)
			Condition_ = value
			Dim s As String = ""
			Select Case Condition_
				Case -1, -2
					clr = Form1.colorM
					s = "Масса"
				Case 0
					clr = Form1.color0
					s = "Нет сигнала"
				Case 15, 16
					clr = Form1.color15
					s = "Сигнал +15"
				Case 30, 31
					clr = Form1.color30
					s = "Сигнал +30"
			End Select
			Me.BackColor = clr
			ToolTip1.SetToolTip(Me, s)
		End Set
		Get
			Return Condition_
		End Get
	End Property

	Public Sub Change(from As Integer, condition_ As Integer) Implements IConnectable.Change
		Dim s As String = ""
		Select Case condition_
			Case -1, -2
				clr = Form1.colorM
				s = "Масса"
			Case 0
				clr = Form1.color0
				s = "Нет сигнала"
			Case 15, 16
				clr = Form1.color15
				s = "Сигнал +15"
			Case 30, 31
				clr = Form1.color30
				s = "Сигнал +30"
		End Select
		Me.Condition_ = condition_
		Me.BackColor = clr
		ToolTip1.SetToolTip(Me, s)
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
					Form1.TextBox1.Text &= "line " + CStr(num) + " to " + CStr(links(i)) + vbCrLf
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
			Condition_
		}
		Return save
	End Function

	Private Sub ELine_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
		If Form1.Mode = "Delete" Then
			DeleteMe()
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

		Form1.Delete(num)
		'Изменения Change добавить ++++++++++++++++++++++++++++++++++++++++++++++
		Form1.DisConnect(n1, n2)

	End Sub

	Private Sub ELine_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
		Me.BackColor = Color.Aqua
	End Sub

	Private Sub ELine_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
		Me.BackColor = clr
	End Sub

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Dim linkForCheck As Integer
		For i = 0 To links.Count - 1
			If links(i) <> from Then
				If links(i) <> 0 Then
					linkForCheck = links(i)
				End If
			End If
		Next
		'Отправление дальше
		Form1.TextBox1.Text &= "check from line " + CStr(num) + " to " + CStr(linkForCheck) + vbCrLf
		Dim eComp As EComponent
		Dim econ As IConnectable
		eComp = Form1.Elements(linkForCheck)
		econ = eComp.component
		Return econ.CheckSig(num)
	End Function
End Class
