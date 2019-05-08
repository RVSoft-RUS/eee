﻿Imports eScheme

Public Class EResist
	Implements IConnectable
	Implements ISetValue
	Public X As Integer
	Public Y As Integer
	Public num As Integer
	Public R As Integer
	Public work As Boolean
	Private c1 As Integer = 0
	Private c2 As Integer = 0

	Public Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Integer, work_ As Boolean)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		R = r_
		work = work_
		PaintMe()
		Cursor = Form1.element_cur
		Dim eComp As EComponent
		Me.Location = New Point(X - 15, Y - 10) 'Для настройки положения

		If Form1.Mode = "eResist" Then 'Только при создании, при открытии файла не делать
			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X + 20, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 20, Y + 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			Form1.TextBox1.Text &= "eResist #" + CStr(num) + " changed (2) points" + vbCrLf
		End If

	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		If from = num + 1 Then
			c1 = condition
			Dim eComp As EComponent = Form1.Elements(num + 2)
			Dim p As EPoint = eComp.component
			c2 = p.Condition
		End If
		If from = num + 2 Then
			c2 = condition
			Dim eComp As EComponent = Form1.Elements(num + 1)
			Dim p As EPoint = eComp.component
			c1 = p.Condition
		End If
		If c1 * c2 = -15 Or c1 * c2 = -30 Then
			work = True
		Else
			work = False
		End If
		PaintMe()
	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eResist",
			num,
			X,
			Y,
			R,
			work
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Return 0
	End Function

	Sub PaintMe()
		If work Then
			PictureBox1.BackColor = Color.Orange
		Else
			PictureBox1.BackColor = Color.White
		End If
	End Sub

	Private Sub EResist_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
		ToolTip1.SetToolTip(Me, "Сопротивление " + CStr(R) + " Ом")
		ToolTip1.SetToolTip(PictureBox1, "Сопротивление " + CStr(R) + " Ом")
	End Sub

	Private Sub ЗадатьНапряжениеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьToolStripMenuItem.Click
		Form1.Enabled = False
		DialogForm.Show(Me)
		DialogForm.Left = X + Form1.Left
		DialogForm.Top = Y + Form1.Top
		DialogForm.OnView("Сопротивление потребителя, Ом", Me)
	End Sub

	Public Sub SetValue(value As Integer) Implements ISetValue.SetValue
		R = value
		Form1.NeedSave = True
	End Sub

	Private Sub EResist_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, PictureBox1.Click
		If e.Button = MouseButtons.Right Then
			ContextMenu1.Show(Me, e.X, e.Y)
		End If
	End Sub

End Class
