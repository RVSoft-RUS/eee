Imports eScheme

Public Class eBat
	Implements IConnectable
	Implements ISetValue
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	Public U As Integer
	Public I As Single

	Public Sub New(rx As Integer, ry As Integer, n As Integer, u_ As Integer, i_ As Single)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		U = u_
		I = i_
		Cursor = Form1.element_cur
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
		If from = num + 1 And condition <> -1 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If
		If from = num + 2 And condition <> 15 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If
		If from = num + 3 And condition <> 30 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If

	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eBat",
			num,
			X,
			Y,
			U,
			I
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

	Private Sub EBat_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
		If e.Button = MouseButtons.Right Then
			ContextMenu1.Show(Me, e.X, e.Y)
		End If
	End Sub

	Private Sub EBat_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
		ToolTip1.SetToolTip(Me, "Напряжение " + CStr(U) + "В" + vbCrLf + "Ток потребления " + CStr(Math.Round(I, 3)) + " A")
	End Sub

	Private Sub ЗадатьНапряжениеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьНапряжениеToolStripMenuItem.Click
		Form1.Enabled = False
		DialogForm.Show(Me)
		DialogForm.Left = X + Form1.Left
		DialogForm.Top = Y + Form1.Top
		DialogForm.OnView("Напряжение источника питания, В", Me)
	End Sub

	Public Sub SetValue(value As Integer) Implements ISetValue.SetValue
		U = value
		Form1.NeedSave = True
		Form1.pointsInProcess.Clear()
		CheckUI(0, 0, 0)
	End Sub

	Public Function CheckUI(from As Integer, U_ As Single, Optional t As Integer = 0) As Single Implements IConnectable.CheckUI
		Dim q As ArrayList = Form1.Elements
		Dim eComp As EComponent = Form1.Elements(num + 2)
		Dim p1 As EPoint = eComp.component
		eComp = Form1.Elements(num + 3)
		Dim p2 As EPoint = eComp.component
		I = p1.CheckUI(num, CSng(U)) + p2.CheckUI(num, CSng(U))
		Return 0
	End Function
End Class
