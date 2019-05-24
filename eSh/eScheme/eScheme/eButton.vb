Imports eScheme

Public Class eButton
    Implements IConnectable
    Implements ISetValue
    Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public work As Boolean
    Public wTime As Single = 1000
    Public loc As Integer = 1

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, work_ As Boolean, wTime_ As Single, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        work = work_
        wTime = wTime_
        loc = locat
        PaintMe()
        ' 2 cur для кнопки
        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 15) 'Для настройки положения
            Me.Height = 20
            Me.Width = 30
        ElseIf loc = 2 Then
            Me.Height = 30
            Me.Width = 20
            Me.Location = New Point(X - 15, Y + 5) 'Для настройки положения
        End If

        If Form1.Mode = "eButton1" Then 'Только при создании, при открытии файла не делать

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
        End If
        If Form1.Mode = "eButton2" Then 'Только при создании, при открытии файла не делать
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X, Y - 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y - 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Throw New NotImplementedException()
    End Function

    Sub PaintMe()
        If work Then
            If loc = 1 Then
                PictureBox1.Visible = False
                PictureBox2.Visible = True
            End If
            If loc = 2 Then
                PictureBox3.Visible = False
                PictureBox4.Visible = True
            End If
        Else
            If loc = 1 Then
                PictureBox1.Visible = True
                PictureBox2.Visible = False
            End If
            If loc = 2 Then
                PictureBox3.Visible = True
                PictureBox4.Visible = False
            End If
        End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Throw New NotImplementedException()
    End Sub

    Public Sub SetValue(value As Single) Implements ISetValue.SetValue
        Throw New NotImplementedException()
    End Sub

    Public Sub MoveOK() Implements IMovable.MoveOK
        Throw New NotImplementedException()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Throw New NotImplementedException()
    End Sub

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Throw New NotImplementedException()
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Throw New NotImplementedException()
    End Function

    Public Function GetX() As Integer Implements IMovable.GetX
        Throw New NotImplementedException()
    End Function

    Public Function GetY() As Integer Implements IMovable.GetY
        Throw New NotImplementedException()
    End Function

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Throw New NotImplementedException()
    End Function
End Class
