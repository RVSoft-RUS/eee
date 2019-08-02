Imports System.Drawing.Drawing2D
Imports eScheme

Public Class eMotor
    Implements IConnectable
    Dim img As Bitmap, imgOut As Bitmap, g As Graphics
    Dim angle As Integer = 30

    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public R As Single
    Public work As Integer
    Public Ia As Single
    Public loc As Integer = 1
    Private c1 As Integer = 0
    Private c2 As Integer = 0

    Dim m_Y As Integer
    Dim m_X As Integer
    Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Single, work_ As Integer, ia_ As Single, locat As Integer)
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
        Dim gpath As New GraphicsPath
        Dim re As Rectangle = cPic.ClientRectangle
        're.Inflate(-1, -1)  ' уменьшаем, чтобы края не обрезались,можно наоборот                          
        gpath.AddEllipse(re)
        cPic.Region = New Region(gpath)
        img = New Bitmap(cPic.Image)
        imgOut = New Bitmap(Me.BackgroundImage)
        cPic.Image = Nothing
        'g = cPic.CreateGraphics
        g = Me.CreateGraphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        X = rx
        Y = ry
        num = n
        R = r_
        work = work_
        Ia = ia_
        loc = locat

        If loc = 1 Then
            Me.Location = New Point(X - 15, Y - 15) 'Для настройки положения
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        angle += 12
        g.Clear(Color.White)
        'g.DrawImage(imgOut, 0, 0)
        'g.TranslateTransform(12, 12)
        'g.RotateTransform(angle)
        'g.DrawImage(img, -12, -12)
        Dim tm As New System.Drawing.Drawing2D.Matrix 'задаем координатную сетку
        'tm.RotateAt(-angle, New PointF(15, 15)) 'с поворотом 
        'g.Transform = tm
        g.DrawImage(imgOut, 0, 0)
        tm.RotateAt(angle, New PointF(15, 15)) 'с поворотом 
        g.Transform = tm
        g.DrawImage(img, 5, 5)

    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Throw New NotImplementedException()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Return Nothing
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Throw New NotImplementedException()
    End Function

    Private Sub cPic_Click(sender As Object, e As EventArgs) Handles cPic.Click, Me.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Throw New NotImplementedException()
    End Function
End Class
