Imports System.Drawing.Drawing2D
Imports eScheme

<Serializable> Public Class EPoint
	Implements IConnectable
	Public X As Integer
	Public Y As Integer
	Dim clr As New Color()
	Dim br As Brush
	Public links As New ArrayList
	Public Condition_ As Integer
	Public num As Integer
	'Dim Left As
	Public Sub New(rx As Integer, ry As Integer, n As Integer)
		' Этот вызов является обязательным для конструктора.
		InitializeComponent()
		' Добавить код инициализации после вызова InitializeComponent().
		num = n
		X = rx
		Y = ry
		Me.Location = New Point(X - 5, Y - 5)
		Condition = 0
		Cursor = Form1.point_cur
	End Sub

	Public Property Condition
		Set(value)
			Condition_ = value
			Select Case Condition_
				Case -1, -2
					clr = Form1.colorM
				Case 0
					clr = Form1.color0
				Case 15, 16
					clr = Form1.color15
				Case 30, 31
					clr = Form1.color30
			End Select
			'Dim pen As New Pen(clr) With {
			'			.Width = 4
			'		}
			Dim g As Graphics = Me.CreateGraphics
			g.Clear(Color.White)
			Dim pen As New Pen(clr) With {
				.Width = 3
			}
			g.DrawEllipse(pen, 3, 3, 4, 4)
			g.DrawEllipse(pen, 4, 4, 2, 2)
			g.Dispose()
		End Set
		Get
			Return Condition_
		End Get
	End Property

	Public Sub Change(from As Integer, condition_ As Integer) Implements IConnectable.Change
		Select Case condition_
			Case -1, -2
				clr = Form1.colorM
			Case 0
				clr = Form1.color0
			Case 15, 16
				clr = Form1.color15
			Case 30, 31
				clr = Form1.color30
		End Select
		Me.Condition_ = condition_
		Dim g As Graphics = Me.CreateGraphics
		g.Clear(Color.White)
		Dim pen As New Pen(clr) With {
			.Width = 3
		}
		g.DrawEllipse(pen, 3, 3, 4, 4)
		g.DrawEllipse(pen, 4, 4, 2, 2)
		g.Dispose()
		If Form1.pointsInProcess.Contains(num) Then
			MsgBox("Не допускаестся создание замкнутых контуров." +
				   vbCrLf + "Удалите лишние связи.", vbCritical, "Ошибка в схеме")
			Exit Sub
		Else
			Form1.pointsInProcess.Add(num)
		End If
		'Отправление дальше
		For i = 0 To links.Count - 1
			If links(i) <> from Then
				If links(i) <> 0 Then
					Dim eComp As EComponent
					Dim econ As IConnectable
					eComp = Form1.Elements(links(i))
					econ = eComp.component
					econ.Change(num, Me.Condition_)
					Form1.TextBox1.Text &= "point " + CStr(num) + " to " + CStr(links(i)) + vbCrLf
				End If
			End If
		Next
	End Sub

	Private Sub EPoint_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
		If e.KeyCode = Keys.N Then
			MsgBox("Номер узла: " + CStr(num))
		End If
		If e.KeyCode = Keys.I Then
			Dim s As String
			s = "All links for p" + CStr(num) + vbCrLf
			For i = 0 To links.Count - 1
				s &= "link" + CStr(i) + ": " + CStr(links(i)) + vbCrLf
			Next
			MsgBox(s)
		End If
		If e.KeyCode = Keys.Delete Then
			If Form1.Mode = "" Then
				DeleteMe()
			End If
		End If
		If e.KeyCode = Keys.S Then
			Form1.pointsInProcess.Clear()
			MsgBox(CStr(CheckSig(0)), vbInformation, "Check")
		End If
	End Sub

	Private Sub Connectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Private Sub EPoint_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
		e.Graphics.Clear(Color.White)
		Dim pen As New Pen(clr) With {
			.Width = 3
		}
		e.Graphics.DrawEllipse(pen, 3, 3, 4, 4)
		e.Graphics.DrawEllipse(pen, 4, 4, 2, 2)
	End Sub

	Private Sub EPoint_Click(sender As Object, e As EventArgs) Handles Me.Click
		If Form1.Mode = "createConnect1" Then
			Form1.createConnect.MouseClick(X, Y)
			Form1.createConnect.EndCreate(X, Y, num)
			Form1.Mode = ""
			Form1.GroupBox1.Visible = True
			Form1.CheckBox2.Visible = True
			Form1.Cursor = Cursors.Default
			Form1.NeedSave = True
		End If
		If Form1.Mode = "createConnect" Then
			Form1.createConnect = New EAddLinesAndPoints(X, Y, num)
		End If
		If Form1.Mode = "Delete" Then
			DeleteMe()
		End If
	End Sub

	Sub DeleteMe()
		If links.Count = 0 Then
			'Удаляем при условии, что нет ссылок
			Form1.Delete(num)
		Else
			MsgBox("Можно удалить только пустой узел.")
		End If
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"ePoint",
			num,
			X,
			Y,
			links,
			Condition_
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		If Form1.pointsInProcess.Contains(num) Then
			MsgBox("Не допускаестся создание замкнутых контуров." +
				   vbCrLf + "Удалите лишние связи.", vbCritical, "Ошибка в схеме")
			Return num + 1000000
		Else
			Form1.pointsInProcess.Add(num)
		End If
		Dim linkForCheck As New ArrayList
		'Отправление дальше
		For i = 0 To links.Count - 1
			If links(i) <> from Then
				If links(i) <> 0 Then
					linkForCheck.Add(links(i))
				End If
			End If
		Next
		Dim sig As Integer
		Dim eComp As EComponent
		Dim econ As IConnectable

		For i = 0 To linkForCheck.Count - 1
			eComp = Form1.Elements(linkForCheck(i))
			econ = eComp.component
			sig = econ.CheckSig(num)
			If sig <> 0 Then Return sig
		Next
		Return 0
	End Function

End Class
