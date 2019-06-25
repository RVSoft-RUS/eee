Imports eScheme

Public Class eDiod
    Implements IConnectable
    'Implements ISetValue
    'Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public R As Single
    Public work As Boolean
    Public Ia As Single
    Public loc As Integer = 1
    Private c1 As Integer = 0
    Private c2 As Integer = 0

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Integer, work_ As Boolean, ia_ As Single, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        R = r_
        work = work_
        Ia = ia_
        loc = locat

        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
            Cursor = Form1.d1_cur
        ElseIf loc = 3 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
            Cursor = Form1.d3_cur
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
            Cursor = Form1.d2_cur
        ElseIf loc = 4 Then
            Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
            Cursor = Form1.d4_cur
        End If

        PaintMe()

        If Form1.Mode = "eDiod1" Then 'Только при создании, при открытии файла не делать
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
            p = New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eDiod3" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X + 40, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eDiod2" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y + 40, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eDiod4" Then 'Только при создании, при открытии файла не делать
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
            p = New EPoint(X, Y + 40, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
    End Sub

    Sub PaintMe()
        pb1.Visible = False
        pb2.Visible = False
        pb3.Visible = False
        pb4.Visible = False
        If work Then
            If loc = 1 Then
                pb1.Visible = True
            ElseIf loc = 3 Then
                pb3.Visible = True
            ElseIf loc = 2 Then
                pb2.Visible = True
            ElseIf loc = 4 Then
                pb4.Visible = True
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
            "eDiod",
            num,
            X,
            Y,
            R,
            work,
            Ia,
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
