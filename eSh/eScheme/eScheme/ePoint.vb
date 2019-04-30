Imports System.Drawing.Drawing2D
Imports eScheme

<Serializable> Public Class EPoint
	Implements IConnectable
    Public X As Integer
    Public Y As Integer
    Dim clr As New Color()
    Dim br As Brush
    Public links() As Integer
    Public Condition As Integer
    Public num As Integer
    'Dim Left As
    Public Sub New(rx As Integer, ry As Integer)
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
        X = rx
        Y = ry
        Me.Location = New Point(X - 5, Y - 5)
        Change(0, 0)
        ReDim links(3)
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Select Case condition
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
        'g.DrawEllipse(pen, 4, 4, 2, 2)
        g.Dispose()
    End Sub

    Public Function freePins() As ArrayList
        Dim arr As New ArrayList
        For i = 0 To 3
            If links(i) = 0 Then 'Если =0 значит не ссылается на элемент и номер пина не занят
                arr.Add(i)
            End If
        Next
        Return arr 'Вернуть список свободных пинов (направлений)
    End Function

    Private Sub EPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
		If e.KeyChar = "n" Or e.KeyChar = "т" Then
			MsgBox("Номер узла: " + CStr(num))
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

    'Private Sub EPoint_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Dim pen As New Pen(clr) With {
    '                .Width = 3
    '            }
    '    Dim g As Graphics = Me.CreateGraphics
    '    g.DrawEllipse(pen, 3, 3, 4, 4)
    '    g.DrawEllipse(pen, 4, 4, 2, 2)
    '    g.Dispose()
    'End Sub
End Class
