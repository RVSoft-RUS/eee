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
			Dim pen As New Pen(clr) With {
						.Width = 4
					}
			Dim g As Graphics = Me.CreateGraphics
			g.FillEllipse(New SolidBrush(clr), 2, 2, 6, 6)
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
		Dim pen As New Pen(clr) With {
					.Width = 4
				}
		Dim g As Graphics = Me.CreateGraphics
		g.FillEllipse(New SolidBrush(clr), 2, 2, 6, 6)
		g.Dispose()
		'Отправление дальше
		For i = 0 To links.Count - 1
			If links(i) <> from Then
				If links(i) <> 0 Then
					Dim eComp As EComponent
					Dim econ As IConnectable
					eComp = Form1.Elements(links(i))
					econ = eComp.component
					econ.Change(num, Me.Condition_)
					Form1.TextBox1.Text &= "from " + CStr(num) + " to " + CStr(links(i)) + vbCrLf
				End If
			End If
		Next
	End Sub

	'Public Function FreePins() As ArrayList
	'	Dim arr As New ArrayList
	'	For i = 0 To 3
	'		If links(i) = 0 Then 'Если =0 значит не ссылается на элемент и номер пина не занят
	'			arr.Add(i)
	'		End If
	'	Next
	'	Return arr 'Вернуть список свободных пинов (направлений)
	'End Function

	Private Sub EPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
		If e.KeyChar = "n" Or e.KeyChar = "т" Or e.KeyChar = "N" Or e.KeyChar = "Т" Then
			MsgBox("Номер узла: " + CStr(num))
		End If
		If e.KeyChar = "ш" Or e.KeyChar = "Ш" Or e.KeyChar = "i" Or e.KeyChar = "I" Then
			Dim s As String
			s = "All links for p" + CStr(num) + vbCrLf
			For i = 0 To links.Count - 1
				s &= "link" + CStr(i) + ": " + CStr(links(i)) + vbCrLf
			Next
			MsgBox(s)
		End If
	End Sub

	Private Sub Connectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Private Sub EPoint_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
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

End Class
