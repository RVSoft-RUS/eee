Imports eScheme

Public Class eLine
	Implements IConnectable
	Public X1 As Integer
	Public Y1 As Integer
	Public X2 As Integer
	Public Y2 As Integer
	Dim clr As New Color()
	Public links As New ArrayList
	Public Condition As Integer
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

		Change(0, 0)

	End Sub

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
		Condition = condition_
		Me.BackColor = clr
		'Отправление дальше
		For i = 0 To links.Count - 1
			If links(i) <> from Then
				If links(i) <> 0 Then
					'Отправление дальше
				End If
			End If
		Next
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Private Sub ELine_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
		If e.KeyChar = "n" Or e.KeyChar = "т" Or e.KeyChar = "N" Or e.KeyChar = "Т" Then
			MsgBox("Номер провода: " + CStr(num))
		End If
		If e.KeyChar = "ш" Or e.KeyChar = "Ш" Or e.KeyChar = "i" Or e.KeyChar = "I" Then
			MsgBox("linkA: " + CStr(links(0)) + ";  linkB: " + CStr(links(1)))
		End If
	End Sub
End Class
