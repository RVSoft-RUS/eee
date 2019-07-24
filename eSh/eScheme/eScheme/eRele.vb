Imports eScheme

Public Class eRele
    Implements IConnectable
    Implements ISetValue
    Implements IMovable
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
        PaintMe()

        Cursor = Form1.element_cur
        Dim eComp As EComponent
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 20) 'Для настройки положения
            Me.Height = 51
            Me.Width = 30
        ElseIf loc = 3 Then
            Me.Height = 51
            Me.Width = 30
            Me.Location = New Point(X - 35, Y - 20) 'Для настройки положения
        ElseIf loc = 2 Then
            Me.Height = 30
            Me.Width = 51
            Me.Location = New Point(X - 20, Y - 35) 'Для настройки положения
        ElseIf loc = 4 Then
            Me.Height = 30
            Me.Width = 51
            Me.Location = New Point(X - 20, Y + 5) 'Для настройки положения
        End If

        If Form1.Mode = "eRele1" Then 'Только при создании, при открытии файла не делать
            'ТОЧКИ ПЕРЕКЛЮЧАТЕЛЯ, нумерация аналогично eSwitch
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

            'ТОЧКИ ОБМОТКИ РЕЛЕ, поведение как у eResist
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 4
                        }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y + 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 5
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40, Y + 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If

        If Form1.Mode = "eRele3" Then 'Только при создании, при открытии файла не делать
            'ТОЧКИ ПЕРЕКЛЮЧАТЕЛЯ, нумерация аналогично eSwitch
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
            p = New EPoint(X - 40, Y, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 40, Y - 20, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            'ТОЧКИ ОБМОТКИ РЕЛЕ, поведение как у eResist
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 4
                        }
            Form1.Elements.Add(eComp)
            p = New EPoint(X, Y + 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 5
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 40, Y + 20, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If

        If Form1.Mode = "eRele2" Then 'Только при создании, при открытии файла не делать
            'ТОЧКИ ПЕРЕКЛЮЧАТЕЛЯ, нумерация аналогично eSwitch
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

            'ТОЧКИ ОБМОТКИ РЕЛЕ, поведение как у eResist
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 4
                        }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 5
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y - 40, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If

        If Form1.Mode = "eRele4" Then 'Только при создании, при открытии файла не делать
            'ТОЧКИ ПЕРЕКЛЮЧАТЕЛЯ, нумерация аналогично eSwitch
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
            p = New EPoint(X, Y + 40, eComp.numInArray)
            p.links.Add(-1) 'Добавлять в пустую точку, чтоб нре удалялась отдельно
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 3
            }
            Form1.Elements.Add(eComp)
            p = New EPoint(X - 20, Y + 40, eComp.numInArray)
            p.links.Add(num + 1)
            'p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            'ТОЧКИ ОБМОТКИ РЕЛЕ, поведение как у eResist
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 4
                        }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 5
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 20, Y + 40, eComp.numInArray)
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
        pb5.Visible = False
        pb6.Visible = False
        pb7.Visible = False
        pb8.Visible = False
        If work Then
            If loc = 1 Then
                pb2.Visible = True
            ElseIf loc = 2 Then
                pb4.Visible = True
            ElseIf loc = 3 Then
                pb6.Visible = True
            ElseIf loc = 4 Then
                pb8.Visible = True
            End If
        Else
            If loc = 1 Then
                pb1.Visible = True
            ElseIf loc = 2 Then
                pb3.Visible = True
            ElseIf loc = 3 Then
                pb5.Visible = True
            ElseIf loc = 4 Then
                pb7.Visible = True
            End If
        End If
    End Sub

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        If from = num + 4 Then
            c1 = condition
            Dim eComp As EComponent = Form1.Elements(num + 5)
            Dim p As EPoint = eComp.component
            c2 = p.Condition
        End If
        If from = num + 5 Then
            c2 = condition
            Dim eComp As EComponent = Form1.Elements(num + 4)
            Dim p As EPoint = eComp.component
            c1 = p.Condition
        End If
        If c1 * c2 = -15 Or c1 * c2 = -30 Then
            work = True
            Form1.DisConnect(num + 1, num + 3)
            Form1.OnConnect(num + 1, num + 2)
            Dim eComp As EComponent = Form1.Elements(num + 3)
            Dim p As EPoint = eComp.component
            p.addLink(-1)
            eComp = Form1.Elements(num + 2)
            p = eComp.component
            p.remLink(-1)
        Else
            If work Then
                work = False
                Ia = 0
                Form1.DisConnect(num + 1, num + 2)
                Form1.OnConnect(num + 1, num + 3)
                Dim eComp As EComponent = Form1.Elements(num + 2)
                Dim p As EPoint = eComp.component
                p.addLink(-1)
                eComp = Form1.Elements(num + 3)
                p = eComp.component
                p.remLink(-1)
            End If

        End If
        PaintMe()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Private Sub eRele_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, pb1.MouseEnter, pb2.MouseEnter, pb3.MouseEnter, pb4.MouseEnter, pb5.MouseEnter, pb6.MouseEnter, pb7.MouseEnter, pb8.MouseEnter
        ToolTip1.SetToolTip(pb1, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb2, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb3, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb4, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb5, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb6, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb7, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")
        ToolTip1.SetToolTip(pb8, "Сопротивление " + CStr(R) + " Ом," + vbCrLf + "Ток в катушке " + CStr((Math.Round(Ia, 3))) + " A")

    End Sub

    Private Sub eRele_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, pb1.MouseClick, pb2.MouseClick, pb3.MouseClick, pb4.MouseClick, pb5.MouseClick, pb6.MouseClick, pb7.MouseClick, pb8.MouseClick
        If Form1.Mode = "Move" Then
            Form1.moveObject = Me
            Form1.Cursor = Cursors.SizeAll
            Form1.moveXstart = Form1.rx
            Form1.moveYstart = Form1.ry
            Form1.Mode = "MoveMe"
            Exit Sub
        End If
        If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
            Form1.Mode = ""
            Form1.GroupBox1.Visible = True
            Form1.CheckBox2.Visible = True
            Form1.CheckBox2.Checked = True
            Form1.Cursor = Cursors.Default
            Exit Sub
        End If
        If e.Button = MouseButtons.Right Then
            ContextMenu1.Show(Me, e.X, e.Y)
            Exit Sub
        End If
        If Form1.Mode = "Delete" Then
            Dim eComp As EComponent = Form1.Elements(num + 1)
            Dim p As EPoint = eComp.component 'Первая точка 
            p.links.Remove(num + 2)
            p.links.Remove(num + 3)
            p.DeleteMe()

            eComp = Form1.Elements(num + 2)
            p = eComp.component 'Вторая точка 
            p.links.Remove(num + 1)
            p.links.Remove(-1)
            p.DeleteMe()

            eComp = Form1.Elements(num + 3)
            p = eComp.component '3 точка 
            p.links.Remove(num + 1)
            p.links.Remove(-1)
            p.DeleteMe()

            eComp = Form1.Elements(num + 4)
            p = eComp.component '4 точка 
            p.links.Remove(num)
            p.DeleteMe()

            eComp = Form1.Elements(num + 5)
            p = eComp.component '5 точка 
            p.links.Remove(num)
            p.DeleteMe()

            If Form1.f.Batt > 0 Then
                eComp = Form1.Elements(Form1.f.Batt)
                Dim bat As eBat = eComp.component
                Form1.pointsInProcessUI.Clear()
                bat.CheckUI(0, 0)
            End If
            Form1.Delete(num)
        End If
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eRele",
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
        Return 0
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        If Form1.isCycle Then
            Return 0
        End If
        Dim from_ As Integer = num + 4
        Dim to_ As Integer = num + 5
        If from = num + 5 Then
            from_ = num + 5
            to_ = num + 4
        End If
        Dim eComp As EComponent = Form1.Elements(to_)
        Dim p1 As EPoint = eComp.component
        'Dim asd As ArrayList = Form1.pointsInProcessSig
        Form1.pointsInProcessSig.Clear()
        If p1.CheckSig(num) = -1 Then
            Ia = U / (R + r_)
            Form1.pointsInProcessUI.Clear()
            p1.CheckUI(num, 0, 0)
            Return Ia
        Else
            Form1.pointsInProcessUI.Clear()
            Ia = p1.CheckUI(num, U, R + r_)
            Return Ia
        End If
    End Function

    Private Sub ЗадатьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьToolStripMenuItem.Click
        Form1.Enabled = False
        DialogForm.Show(Me)
        DialogForm.Left = X + Form1.Left
        DialogForm.Top = Y + Form1.Top
        DialogForm.OnView("Сопротивление обмотки, Ом", Me, R.ToString)
    End Sub

    Public Sub SetValue(value As Single) Implements ISetValue.SetValue
        R = value
        Form1.DoNeedSave()
        If Form1.f.Batt > 0 Then
            Dim eComp As EComponent = Form1.Elements(Form1.f.Batt)
            Dim bat As eBat = eComp.component
            Form1.pointsInProcessUI.Clear()
            bat.CheckUI(0, 0)
        End If
    End Sub

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Form1.moveArray.Add(Me)
        Dim mayMove1, mayMove2, mayMove3, mayMove4, mayMove5 As Boolean
        If from Is Me Then
            Dim m As IMovable
            Dim eComp As EComponent = Form1.Elements(num + 1)
            m = eComp.component
            mayMove1 = m.Move(Me, dX, dY)
            eComp = Form1.Elements(num + 2)
            m = eComp.component
            mayMove2 = m.Move(Me, dX, dY)
            eComp = Form1.Elements(num + 3)
            m = eComp.component
            mayMove3 = m.Move(Me, dX, dY)
            eComp = Form1.Elements(num + 4)
            m = eComp.component
            mayMove4 = m.Move(Me, dX, dY)
            eComp = Form1.Elements(num + 5)
            m = eComp.component
            mayMove5 = m.Move(Me, dX, dY)
            'Form1.TextBox1.Text = "mayMove1=" + CStr(mayMove1) + "  mayMove2=" + CStr(mayMove2) + vbCrLf + Form1.TextBox1.Text
            If mayMove1 And mayMove2 And mayMove3 And mayMove4 And mayMove5 Then
                m_X = X + dX
                m_Y = Y + dY

                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function GetXY() As Integer Implements IMovable.GetX, IMovable.GetY
        Throw New NotImplementedException()
    End Function

    Public Sub MoveOK() Implements IMovable.MoveOK
        X = m_X
        Y = m_Y

        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 20) 'Для настройки положения
        ElseIf loc = 3 Then
            Me.Location = New Point(X - 35, Y - 20) 'Для настройки положения
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 20, Y - 35) 'Для настройки положения
        ElseIf loc = 4 Then
            Me.Location = New Point(X - 20, Y + 5) 'Для настройки положения
        End If
    End Sub
End Class
