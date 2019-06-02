Imports eScheme

Public Class eFuse
    Implements IConnectable
    Implements ISetValue
    Implements ILinked, IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public Imax As Single
    Public pos As String
    Public Ia As Single
    Public work As Boolean
    Dim command As Integer = 0

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, imax_ As Single, pos_ As String, ia_ As Single, work_ As Boolean)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        Imax = imax_
        pos = pos_
        Ia = ia_
        work = work_
        'PaintMe()
        If pos = "H" Then
            Cursor = Form1.FuseH_cur
            Me.Location = New Point(X + 5, Y - 6) 'Для настройки положения
            Me.Size = New Point(30, 12) 'Для настройки положения
            PictureBoxV.Visible = False
            If Not work Then
                PictureBoxHC.Visible = True
            End If
        End If
        If pos = "V" Then
            Cursor = Form1.FuseV_cur
            Me.Location = New Point(X - 6, Y + 5) 'Для настройки положения
            Me.Size = New Point(12, 30) 'Для настройки положения
            PictureBoxH.Visible = False
            If Not work Then
                PictureBoxVC.Visible = True
            End If
        End If

        Dim eComp As EComponent


        If Form1.Mode = "newFuseH" Then 'Только при создании, при открытии файла не делать
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
        If Form1.Mode = "newFuseV" Then 'Только при создании, при открытии файла не делать
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

    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        'Отправление дальше
        Dim linkForCheck As Integer = num + 1
        If linkForCheck = from Then
            linkForCheck = num + 2
        End If
        'Отправление дальше
        Dim eComp As EComponent
        Dim econ As IConnectable
        eComp = Form1.Elements(linkForCheck)
        econ = eComp.component
        econ.Change(num, condition)
    End Sub

    Public Sub SetValue(value As Single) Implements ISetValue.SetValue
        Imax = value
		Form1.DoNeedSave()
		If Form1.f.Batt > 0 Then
            Dim eComp As EComponent = Form1.Elements(Form1.f.Batt)
            Dim bat As eBat = eComp.component
            Form1.pointsInProcessUI.Clear()
            bat.CheckUI(0, 0)
        End If
    End Sub

    Public Sub addLink(n As Integer) Implements ILinked.addLink
        'типа добавилась связь с точкой
    End Sub

    Public Sub remLink(n As Integer) Implements ILinked.remLink
        'удалилась связь с точкой типа
    End Sub

    Public Sub Changee(from As Integer, condition_ As Integer) Implements ILinked.Changee
        Change(from, condition_)
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Me.Dispose()
    End Sub

    Private Sub eFuse_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, PictureBoxH.MouseEnter, PictureBoxV.MouseEnter
        ToolTip1.SetToolTip(PictureBoxH, "Номинал " + CStr(Imax) + " А" + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A")
        ToolTip1.SetToolTip(PictureBoxV, "Номинал " + CStr(Imax) + " А" + vbCrLf + "Ток " + CStr(Math.Round(Ia, 3)) + " A")
        'ToolTip1.SetToolTip(PictureBox1, "Сопротивление " + CStr(R) + " Ом")
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eFuse",
            num,
            X,
            Y,
            Imax,
            pos,
            Ia,
            work
        }
        Return save
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Dim linkForCheck As Integer = num + 1
        If linkForCheck = from Then
            linkForCheck = num + 2
        End If
        If linkForCheck = num + 1 And Not work Then
            Return 0
        End If
        'Отправление дальше
        'Form1.TextBox1.Text &= "check from line " + CStr(num) + " to " + CStr(linkForCheck) + vbCrLf
        Dim eComp As EComponent
        Dim econ As IConnectable
        eComp = Form1.Elements(linkForCheck)
        econ = eComp.component
        Return econ.CheckSig(num)
    End Function

    Public Function CheckUI(from As Integer, U_ As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Dim linkForCheck As Integer = num + 1
        If linkForCheck = from Then
            linkForCheck = num + 2
        End If

        Dim eComp As EComponent = Form1.Elements(linkForCheck)
        Dim p1 As EPoint = eComp.component
        Ia = p1.CheckUI(num, U_, r_)
        If Ia > Imax Then
            work = False
            If pos = "H" Then
                PictureBoxHC.Visible = True
                ToolTip1.SetToolTip(PictureBoxHC, "Номинал " + CStr(Imax) + " А" + vbCrLf + "Последний ток " + CStr(Math.Round(Ia, 3)) + " A")
            End If
            If pos = "V" Then
                PictureBoxVC.Visible = True
                ToolTip1.SetToolTip(PictureBoxVC, "Номинал " + CStr(Imax) + " А" + vbCrLf + "Последний ток " + CStr(Math.Round(Ia, 3)) + " A")
            End If
            command = -1 'Разорвать цепь
            Timer1.Enabled = True
            Return 0
        Else
            PictureBoxHC.Visible = False
            PictureBoxVC.Visible = False
            work = True
        End If
        Return Ia
    End Function

    Public Function chk_Sig() As Integer Implements ILinked.chk_Sig
        Return CheckSig(num + 2) ' Тут что то сиранноу!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If command = -1 Then
            If Form1.isCheckUI Then
                Timer1.Interval = 50
            Else
                Timer1.Interval = 500
                Timer1.Enabled = False
                command = 0
                Form1.DisConnect(num, num + 1) 'ТУТ ПИСДЕЦ С МНОГОПОТОЧНОСТЬЮ 

            End If
        End If
    End Sub

    Private Sub ВставитьПредохранительToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВставитьПредохранительToolStripMenuItem.Click
        work = False ' ? Точно,  Может True
        PictureBoxHC.Visible = False
        PictureBoxVC.Visible = False
        Form1.OnConnect(num, num + 1)
    End Sub

    Private Sub eFuse_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, PictureBoxH.MouseClick, PictureBoxHC.MouseClick, PictureBoxV.MouseClick, PictureBoxVC.MouseClick
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
            work = False
            Form1.DisConnect(num, num + 1)
            Dim eComp As EComponent = Form1.Elements(num + 1)
            Dim p As EPoint = eComp.component 'Первая точка 
            p.links.Remove(num)
            If p.links.Count = 0 Then
                p.DeleteMe()
            End If

            eComp = Form1.Elements(num + 2)
            p = eComp.component 'Первая точка 
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

    Private Sub УдалитьПредохранительToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles УдалитьПредохранительToolStripMenuItem.Click
        work = False
        If pos = "H" Then
            PictureBoxHC.Visible = True
            ToolTip1.SetToolTip(PictureBoxHC, "Номинал - НЕТ" + vbCrLf + "Последний ток " + CStr(Math.Round(Ia, 3)) + " A")
        End If
        If pos = "V" Then
            PictureBoxVC.Visible = True
            ToolTip1.SetToolTip(PictureBoxVC, "Номинал - НЕТ" + vbCrLf + "Последний ток " + CStr(Math.Round(Ia, 3)) + " A")
        End If
        Form1.DisConnect(num, num + 1)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Form1.Enabled = False
        DialogForm.Show(Me)
        DialogForm.Left = X + Form1.Left
        DialogForm.Top = Y + Form1.Top
        DialogForm.OnView("Номинал предохранителя, А", Me, Imax.ToString)
    End Sub

    Private Sub EGND_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, PictureBoxH.MouseDown, PictureBoxHC.MouseDown, PictureBoxV.MouseDown, PictureBoxVC.MouseDown
        If Form1.Mode = "Move" Then
            Form1.moveObject = Me
            Form1.Cursor = Cursors.SizeAll
            Form1.moveXstart = Form1.rx
            Form1.moveYstart = Form1.ry
            Form1.Mode = "MoveMe"
        End If
    End Sub

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Form1.moveArray.Add(Me)
        Dim mayMove As Boolean = True
        If from Is Me Then
            Dim m As IMovable
            Dim eComp As EComponent = Form1.Elements(num + 1)
            m = eComp.component
            mayMove = m.Move(Me, dX, dY)
            If Not mayMove Then
                Return False
            End If
            eComp = Form1.Elements(num + 2)
            m = eComp.component
            mayMove = m.Move(Me, dX, dY)
            If mayMove Then
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

        If pos = "H" Then
            Me.Location = New Point(X + 5, Y - 6) 'Для настройки положения
        End If
        If pos = "V" Then
            Me.Location = New Point(X - 6, Y + 5) 'Для настройки положения
        End If
		Form1.DoNeedSave()
	End Sub
End Class
