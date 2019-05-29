Public Class eLamp
    'Implements IConnectable
    'Implements ISetValue
    'Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public R As Single
    Public work As Boolean
    Public Ia As Single
    Private c1 As Integer = 0
    Private c2 As Integer = 0

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Integer, work_ As Boolean, ia_ As Single)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        R = r_
        work = work_
        Ia = ia_
        'PaintMe()

        Cursor = Form1.element_cur                                 'Поменять
        Dim eComp As EComponent
        Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения

        If Form1.Mode = "eLamp1" Then 'Только при создании, при открытии файла не делать

            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eLamp1" Then 'Только при создании, при открытии файла не делать

            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y - 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If


    End Sub

    Sub PaintMe()
        If work Then
            pb2.Visible = True
            pb1.Visible = False
        Else
            pb1.Visible = True
            pb2.Visible = False
        End If
    End Sub
End Class
