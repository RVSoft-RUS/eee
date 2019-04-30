Imports System.Math
Public Class EAddLinesAndPoints

	'Вспомогательный класс для соединения точки с точкой
	Public px As Point 'координаты промежуточной точки
	Private pts As New ArrayList
	Private ReadOnly num1 As Integer
	Private num2 As Integer

	Public Sub New(X As Integer, Y As Integer, n As Integer)
		num1 = n
		pts.Add(New Point(X, Y))
		Form1.Mode = "createConnect1"
	End Sub

	Public Sub MouseMove(X As Integer, Y As Integer)
		Dim p As Point
		Dim PointWasAdded As Boolean = False
		p = pts(pts.Count - 1)
		Dim dx As Integer = X - p.X
		Dim dy As Integer = Y - p.Y
		If Abs(dx) >= Abs(dy) And Abs(dx) > 0 And Abs(dy) > 0 Then
			px = New Point(p.X + dx, p.Y)
			pts.Add(px)
			PointWasAdded = True
		Else
			px = New Point(p.X, p.Y + dy)
			pts.Add(px)
			PointWasAdded = True
		End If

		pts.Add(New Point(X, Y))
		DrawPath()
		pts.RemoveAt(pts.Count - 1)
		If PointWasAdded Then pts.RemoveAt(pts.Count - 1)
	End Sub

	Public Sub MouseClick(X As Integer, Y As Integer)
		Dim z As Integer = X
		pts.Add(px)
	End Sub

	Public Sub EndCreate(X As Integer, Y As Integer, n As Integer)
		num2 = n
		pts.Add(New Point(X, Y))
		Dim eComp As EComponent
		eComp = Form1.Elements(num1)
		Dim p1 As EPoint = eComp.component 'Первая точка из которой строили соединение
		p1.links.Add(Form1.Elements.Count + 1)
		Dim p2 As EPoint
		For i = 1 To pts.Count - 2
			Dim pt1, pt2, pt3 As Point
			pt1 = pts(i - 1)
			pt2 = pts(i)
			pt3 = pts(i + 1)
			eComp = New EComponent With {
				.aType = "ePoint",
				.numInArray = Form1.Elements.Count
			}
			Form1.Elements.Add(eComp)
			p2 = New EPoint(pt2.X, pt2.Y, eComp.numInArray)
			Form1.Controls.Add(p2)
			eComp.component = p2 'Точка
			'=====================
			eComp = New EComponent With {
				.aType = "eLine",
				.numInArray = Form1.Elements.Count
			}
			Form1.Elements.Add(eComp)
			Dim line As New eLine(pt1.X, pt1.Y, pt2.X, pt2.Y, eComp.numInArray)
			Form1.Controls.Add(line)
			eComp.component = line 'Линия
			'=====================
			'Ссылки
			p2.links.Add(line.num)
		Next
	End Sub

	Sub DrawPath()
		If pts.Count < 2 Then Exit Sub
		Dim g As Graphics = Form1.CreateGraphics
		Dim p As New Pen(Color.Plum) With {
			.Width = 1
		}
		For i = 0 To pts.Count - 2
			Dim p01 As Point = pts(i)
			Dim p02 As Point = pts(i + 1)
			g.DrawLine(p, p01, p02)
		Next
		g.Dispose()
	End Sub
End Class
