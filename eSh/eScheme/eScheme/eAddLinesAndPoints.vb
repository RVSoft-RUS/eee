Imports System.Math
Public Class EAddLinesAndPoints

	'Вспомогательный класс для соединения точки с точкой
	Public px As Point 'координаты промежуточной точки
	Private pts As New ArrayList
	Private ReadOnly num1 As Integer
	Private num2 As Integer
	Private ReadOnly clr As Color


	Public Sub New(X As Integer, Y As Integer, n As Integer, clr_ As Color)
		num1 = n
		pts.Add(New Point(X, Y))
		Form1.Mode = "createConnect1"
		clr = clr_
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

		'Form1.TextBox1.Text = ""
		'For i = 0 To pts.Count - 1
		'	Dim xp As Point
		'	xp = pts(i)
		'	Form1.TextBox1.Text &= Str(xp.X) + "  " + CStr(xp.Y) + vbCrLf
		'Next
	End Sub

	Public Sub MouseClick(X As Integer, Y As Integer)
		pts.Add(px)
		'Form1.TextBox1.Text = ""
		'For i = 0 To pts.Count - 1
		'	Dim xp As Point
		'	xp = pts(i)
		'	Form1.TextBox1.Text &= Str(xp.X) + "  " + CStr(xp.Y) + vbCrLf
		'Next
	End Sub

    Public Sub EndCreate(X As Integer, Y As Integer, n As Integer)
        If n = num1 Then
            MsgBox("Замкнутый контур.", vbCritical)
            Exit Sub
        End If
        num2 = n
        Dim p As Point
        p = pts(pts.Count - 1)
        Dim dx As Integer = X - p.X
        Dim dy As Integer = Y - p.Y
        If dx + dy <> 0 Then
            px = New Point(p.X + dx, p.Y + dy)
            pts.Add(px)
        Else

        End If

        pts.Add(New Point(X, Y))
        Dim pts2 As New ArrayList
        For i = 0 To pts.Count - 2
            Dim pt As Point
            pt = pts(i)
            If Not pts2.Contains(pt) Then
                pts2.Add(pt)
            End If
        Next
        pts2.Add(pts(pts.Count - 1))
        'Form1.TextBox1.Text = ""
        'For i = 0 To pts2.Count - 1
        '	Dim xp As Point
        '	xp = pts2(i)
        '	Form1.TextBox1.Text &= Str(xp.X) + "  " + CStr(xp.Y) + vbCrLf
        'Next

        Dim eComp As EComponent
        eComp = Form1.Elements(num1)
        Dim p1 As EPoint = eComp.component 'Первая точка из которой строили соединение
        Dim p01 As EPoint = p1 'Первая точка из которой строили соединение для последующих процедур Change
        Dim p02 As EPoint = p1 'Вторая точка из которой строили соединение для последующих процедур Change - Здесь присвоено для искючения предупреждения, так должна быть пустая
        Dim lastLine As eLine = Nothing

        Dim p2 As EPoint = p1 'Здесь присвоено для искючения предупреждения, так должна быть пустая
        For i = 1 To pts2.Count - 2
            Dim pt1, pt2, pt3 As Point
            If i > 1 Then p1 = p2
            pt1 = pts2(i - 1)
            pt2 = pts2(i)
            pt3 = pts2(i + 1)

            If i = pts2.Count - 2 Then
                eComp = Form1.Elements(num2)
                p2 = eComp.component 'Вторая точка до которой строили соединение
                p02 = p2
            Else
                eComp = New EComponent With {
                                .aType = "ePoint",
                                .numInArray = Form1.Elements.Count
                                }
                Form1.Elements.Add(eComp)
                p2 = New EPoint(pt2.X, pt2.Y, eComp.numInArray)
                Form1.Controls.Add(p2)
                eComp.component = p2 'Точка
            End If

            '=====================
            eComp = New EComponent With {
                .aType = "eLine",
                .numInArray = Form1.Elements.Count
            }
            Form1.Elements.Add(eComp)
            Dim line As New eLine(pt1.X, pt1.Y, pt2.X, pt2.Y, eComp.numInArray, 0, 0)
            Form1.Controls.Add(line)
            eComp.component = line 'Линия
            '=====================
            'Ссылки
            p1.links.Add(line.num)
            p2.links.Add(line.num)
            line.links.Add(p1.num)
            line.links.Add(p2.num)
            If i < pts2.Count - 2 Then
                line.Condition = p1.Condition
                p2.Condition = line.Condition
            End If

            lastLine = line
        Next
        'TODO Проброс сигнала TODO продумать как при обоих не =0 перебрасывать
        p1.links.Remove(lastLine.num)
        p2.links.Remove(lastLine.num)
        If Form1.OnConnect(lastLine.links(0), lastLine.links(1)) Then
            lastLine.Condition = p1.Condition
            p1.links.Add(lastLine.num)
            p2.links.Add(lastLine.num)
            p1.links.Remove(p2.num)
            p2.links.Remove(p1.num)
        Else
            p1.links.Remove(lastLine.num)
            p2.links.Remove(lastLine.num)
            eComp = Form1.Elements(lastLine.num)
            eComp = Nothing
            lastLine.DeleteMe()
        End If

    End Sub

    Sub DrawPath()
		If pts.Count < 2 Then Exit Sub
		Dim g As Graphics = Form1.CreateGraphics
		Dim p As New Pen(clr) With {
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
