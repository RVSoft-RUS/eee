Public Class EGND
	Implements IConnectable
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	Public link As Integer

	Public Sub New(rx As Integer, ry As Integer, n As Integer)
		InitializeComponent()
		X = rx
		Y = ry
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
			Form1.TextBox1.Text &= "eGND #" + CStr(num) + " changed (1) point" + vbCrLf
		End If
	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		If condition <> -1 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "GND")
		End If
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
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
End Class
