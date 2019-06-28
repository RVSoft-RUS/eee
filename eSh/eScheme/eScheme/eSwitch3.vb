Imports eScheme

Public Class eSwitch3
    Implements IConnectable
    'Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public work As Integer '(0, 1 or 2)
    Public loc As Integer = 1

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, work_ As Integer, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        work = work_
        loc = locat
        PaintMe()
        ' 2 cur для кнопки
        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 30) 'Для настройки положения
            Me.Height = 52
            Me.Width = 30
            pb1.Cursor = Form1.BuH_cur
            pb2.Cursor = Form1.BuH_cur
            pb3.Cursor = Form1.BuH_cur
        ElseIf loc = 2 Then
            Me.Height = 30
            Me.Width = 52
            Me.Location = New Point(X - 30, Y - 35) 'Для настройки положения
            pb4.Cursor = Form1.BuV_cur
            pb5.Cursor = Form1.BuV_cur
            pb6.Cursor = Form1.BuV_cur
        End If

        If Form1.Mode = "eSwitch3-1" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 1
            }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num + 3)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y - 20, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 4
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y + 20, eComp.numInArray)
            p.links.Add(-1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eSwitch3-2" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 1
            }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num + 3)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y - 40, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 20, Y - 40, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 4
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y - 40, eComp.numInArray)
            p.links.Add(-1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
    End Sub

    Sub PaintMe()
        If loc = 1 Then
            If work = 0 Then
                pb1.Visible = True
                pb2.Visible = False
                pb3.Visible = False
            End If
            If work = 1 Then
                pb1.Visible = False
                pb2.Visible = True
                pb3.Visible = False
            End If
            If work = 2 Then
                pb1.Visible = False
                pb2.Visible = False
                pb3.Visible = True
            End If
        Else 'loc = 2
            If work = 0 Then
                pb4.Visible = True
                pb5.Visible = False
                pb6.Visible = False
            End If
            If work = 1 Then
                pb4.Visible = False
                pb5.Visible = True
                pb6.Visible = False
            End If
            If work = 2 Then
                pb4.Visible = False
                pb5.Visible = False
                pb6.Visible = True
            End If
        End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eSwitch",
            num,
            X,
            Y,
            work,
            loc
        }
        Return save
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Throw New NotImplementedException()
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Throw New NotImplementedException()
    End Function
End Class
