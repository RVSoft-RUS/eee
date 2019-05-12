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
			s += "Sig=" + Str(Condition)
			MsgBox(s)
		End If
		If e.KeyCode = Keys.Delete Then
			If Form1.Mode = "" Then
				DeleteMe()
			End If
		End If
		If e.KeyCode = Keys.C Then
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
			Form1.createConnect = New EAddLinesAndPoints(X, Y, num, clr)
		End If
		If Form1.Mode = "Delete" Then
			If links.Count = 2 Then
				Dim eComp As EComponent = Form1.Elements(links(0))
				Dim line1 As eLine = eComp.component
				Dim p2 As EPoint
				eComp = Form1.Elements(links(1))
				Dim line2 As eLine = eComp.component

				If line1.Loc = "H" And line2.Loc = "H" Then
					'Обе горизонтально
					If line1.X1 > line2.X1 Then
						Dim line As eLine = line1
						line1 = line2
						line2 = line
					End If
					line1.links.Remove(num)
					line2.links.Remove(num)
					eComp = Form1.Elements(line2.links(0))
					p2 = eComp.component
					p2.links.Remove(line2.num)
					p2.links.Add(line1.num)
					line1.links.Add(line2.links(0))
					line1.X2 = line2.X2
					line1.Width = line1.X2 - line1.X1 - 10
					Form1.Delete(line2.num)
					Form1.Delete(num)
				End If
				If line1.Loc = "V" And line2.Loc = "V" Then
					'Обе вертикально
					If line1.Y1 > line2.Y1 Then
						Dim line As eLine = line1
						line1 = line2
						line2 = line
					End If
					line1.links.Remove(num)
					line2.links.Remove(num)
					eComp = Form1.Elements(line2.links(0))
					p2 = eComp.component
					p2.links.Remove(line2.num)
					p2.links.Add(line1.num)
					line1.links.Add(line2.links(0))
					line1.Y2 = line2.Y2
					line1.Height = line1.Y2 - line1.Y1 - 10
					Form1.Delete(line2.num)
					Form1.Delete(num)
				End If
			Else
				DeleteMe()
			End If
		End If
		If Form1.Mode = "eGND" Then
			If Condition_ <= 0 Then
				Form1.Mode = ""
				Dim eComp As New EComponent With {
								.aType = "eGND",
								.numInArray = Form1.Elements.Count
							}
				Form1.Elements.Add(eComp)
				Dim gnd As New EGND(X, Y, eComp.numInArray) With {
					.link = num
				}
				Form1.Controls.Add(gnd)
				eComp.component = gnd
				links.Add(gnd.num)
				Form1.pointsInProcess.Clear()
				Change(gnd.num, -1)

				Form1.GroupBox1.Visible = True
				Form1.CheckBox2.Visible = True
				Form1.Cursor = Cursors.Default
				Form1.NeedSave = True
			Else
				MsgBox("Массу нельзя подключить к плюсовому проводу.", vbInformation, "Подключение массы")
			End If

		End If
	End Sub

	Sub DeleteMe()
		If links.Count = 0 Then
			'Удаляем при условии, что нет ссылок
			Form1.Delete(num)
		Else
			MsgBox("Можно удалить только пустой узел или находящийся на одной линии.")
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
		Dim asd As ArrayList = Form1.pointsInProcess
		If Form1.pointsInProcess.Contains(num) Then
			MsgBox("Не допускаестся создание замкнутых контуров." +
				   vbCrLf + "Удалите лишние связи.", vbCritical, "Ошибка в схеме #Sig")
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

	Public Function CheckUI(from As Integer, U As Single, Optional t As Integer = 0) As Single Implements IConnectable.CheckUI
		Dim asd As ArrayList = Form1.pointsInProcess
		If Form1.pointsInProcess.Contains(num) Then
			MsgBox("Не допускаестся создание замкнутых контуров." +
				   vbCrLf + "Удалите лишние связи.", vbCritical, "Ошибка в схеме #UI")
			Return num + 1000000
		Else
			Form1.pointsInProcess.Add(num)
		End If
		'в точку from вернуть сумму токов из остальных точек
		Dim iis As New ArrayList
		For j = 0 To links.Count - 1
			If links(j) <> from Then
				Dim eComp As EComponent = Form1.Elements(links(j))
				Dim iConn As IConnectable = eComp.component
				iis.Add(iConn.CheckUI(num, U, t))
			End If
		Next
		Dim I As Single = 0
		For j = 0 To iis.Count - 1
			I += iis(j)
		Next
		Return I
	End Function
End Class
