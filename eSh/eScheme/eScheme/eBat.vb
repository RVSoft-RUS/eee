Imports eScheme

Public Class eBat
	Implements IConnectable
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	'Private pMassa As EPoint
	'Private p15 As EPoint
	'Private p30 As EPoint

	Public Sub New(rx As Integer, ry As Integer, n As Integer)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		Dim eComp As EComponent
		Dim econ As IConnectable
		Me.Location = New Point(X + 5, Y - 2) 'Для настройки положения

		If Form1.Mode = "eBat" Then 'Только при создании, при открытии файла не делать
			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y + 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 1)
			econ = eComp.component
			econ.Change(num, -1)

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 60, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 2)
			econ = eComp.component
			econ.Change(num, 15)

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 3
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 60, Y + 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 3)
			econ = eComp.component
			econ.Change(num, 30)

			Form1.TextBox1.Text &= "eBat #" + CStr(num) + " changed (3) points" + vbCrLf
		End If

	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eBat",
			num,
			X,
			Y
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Select Case from
			Case num + 1
				Return -1
			Case num + 2
				Return 15
			Case Else ' num + 3
				Return 30
		End Select
	End Function
End Class
