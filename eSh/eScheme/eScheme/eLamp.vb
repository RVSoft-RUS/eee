Imports eScheme

Public Class eLamp
    Implements IConnectable
    Implements ISetValue
    Implements IMovable
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

    Public Sub New(rx As Integer, ry As Integer, n As Integer, r_ As Single, work_ As Boolean, ia_ As Single)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        R = r_
        work = work_
        Ia = ia_
		PaintMe()
		If Form1.Mode = "" Then Me.Location = New Point(X, Y)
        Cursor = Form1.element_cur                                 'Поменять
        Dim eComp As EComponent


        If Form1.Mode = "eLamp1" Then 'Только при создании, при открытии файла не делать
            X = X + 5
            Y = Y - 15
            Me.Location = New Point(X, Y) 'Для настройки положения
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X - 5, Y + 15, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 40 - 5, Y + 15, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p
        End If
        If Form1.Mode = "eLamp2" Then 'Только при создании, при открытии файла не делать
            X = X - 15
            Y = Y - 35
            Me.Location = New Point(X, Y) 'Для настройки положения
            eComp = New EComponent With {
                            .aType = "ePoint",
                            .numInArray = num + 1
                        }
            Form1.Elements.Add(eComp)
            Dim p As New EPoint(X + 15, Y + 35, eComp.numInArray)
            p.links.Add(num)
            Form1.Controls.Add(p)
            eComp.component = p

            eComp = New EComponent With {
                    .aType = "ePoint",
                    .numInArray = num + 2
                }
            Form1.Elements.Add(eComp)
            p = New EPoint(X + 15, Y - 40 + 35, eComp.numInArray)
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

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        If from = num + 1 Then
            c1 = condition
            Dim eComp As EComponent = Form1.Elements(num + 2)
            Dim p As EPoint = eComp.component
            c2 = p.Condition
        End If
        If from = num + 2 Then
            c2 = condition
            Dim eComp As EComponent = Form1.Elements(num + 1)
            Dim p As EPoint = eComp.component
            c1 = p.Condition
        End If
        If c1 * c2 = -15 Or c1 * c2 = -30 Then
            work = True
        Else
            work = False
        End If
        PaintMe()
    End Sub

    Public Sub SetValue(value As Single) Implements ISetValue.SetValue
        R = value
        Form1.NeedSave = True
        If Form1.f.Batt > 0 Then
            Dim eComp As EComponent = Form1.Elements(Form1.f.Batt)
            Dim bat As eBat = eComp.component
            Form1.pointsInProcessUI.Clear()
            bat.CheckUI(0, 0)
        End If
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eLamp",
            num,
            X,
            Y,
            R,
            work,
            Ia
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
        Dim from_ As Integer = num + 1
        Dim to_ As Integer = num + 2
        If from = num + 2 Then
            from_ = num + 2
            to_ = num + 1
        End If
        Dim eComp As EComponent = Form1.Elements(to_)
        Dim p1 As EPoint = eComp.component
        Dim asd As ArrayList = Form1.pointsInProcessSig
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

    Private Sub ЗадатьСопротивлениеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьСопротивлениеToolStripMenuItem.Click
        Form1.Enabled = False
        DialogForm.Show(Me)
        DialogForm.Left = X + Form1.Left
        DialogForm.Top = Y + Form1.Top
        DialogForm.OnView("Сопротивление лампы, Ом", Me, R.ToString)
    End Sub

    Private Sub eLamp_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, pb1.MouseEnter, pb2.MouseEnter
        ToolTip1.SetToolTip(pb1, "Сопротивление " + CStr(R) + " Ом")
        ToolTip1.SetToolTip(pb2, "Сопротивление " + CStr(R) + " Ом")
    End Sub

    Private Sub pb2_MouseClick(sender As Object, e As MouseEventArgs) Handles pb2.MouseClick, pb1.MouseClick
        If Form1.Mode = "MoveMe" And e.Button = MouseButtons.Right Then
            Form1.Mode = ""
            Form1.GroupBox1.Visible = True
            Form1.CheckBox2.Visible = True
            Form1.CheckBox2.Checked = True
            Form1.Cursor = Cursors.Default
            Exit Sub
        End If
        If Form1.Mode = "Move" Then
            Form1.moveObject = Me
            Form1.Cursor = Cursors.SizeAll
            Form1.moveXstart = Form1.rx
            Form1.moveYstart = Form1.ry
            Form1.Mode = "MoveMe"
            Exit Sub
        End If
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show(Me, e.X, e.Y)
            Exit Sub
        End If
        If Form1.Mode = "Delete" Then
            Dim eComp As EComponent = Form1.Elements(num + 1)
            Dim p As EPoint = eComp.component 'Первая точка 
            p.links.Remove(num)
            If p.links.Count = 0 Then
                p.DeleteMe()
            End If

            eComp = Form1.Elements(num + 2)
            p = eComp.component 'Вторая точка 
            p.links.Remove(num)
            If p.links.Count = 0 Then
                p.DeleteMe()
            End If
            If Form1.f.Batt > 0 Then
                eComp = Form1.Elements(Form1.f.Batt)
                Dim bat As eBat = eComp.component
                Form1.pointsInProcessUI.Clear()
                bat.CheckUI(0, 0)
            End If
            Form1.Delete(num)
        End If
    End Sub

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Form1.moveArray.Add(Me)
        Dim mayMove1, mayMove2 As Boolean
        If from Is Me Then
            Dim m As IMovable
            Dim eComp As EComponent = Form1.Elements(num + 1)
            m = eComp.component
            mayMove1 = m.Move(Me, dX, dY)
            eComp = Form1.Elements(num + 2)
            m = eComp.component
            mayMove2 = m.Move(Me, dX, dY)
            'Form1.TextBox1.Text = "mayMove1=" + CStr(mayMove1) + "  mayMove2=" + CStr(mayMove2) + vbCrLf + Form1.TextBox1.Text
            If mayMove1 And mayMove2 Then
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

    Public Function GetX() As Integer Implements IMovable.GetX
        Throw New NotImplementedException()
    End Function

    Public Function GetY() As Integer Implements IMovable.GetY
        Throw New NotImplementedException()
    End Function

    Public Sub MoveOK() Implements IMovable.MoveOK
        X = m_X
        Y = m_Y
        Me.Location = New Point(X, Y) 'Для настройки положения
        Form1.NeedSave = True
    End Sub
End Class
