Imports eScheme

Public Class eFuse
    Implements IConnectable
    Implements ISetValue
    Implements ILinked
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public Imax As Single
    Public pos As String
    Public Ia As Single
    Public work As Boolean

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

    Public Sub SetValue(value As Integer) Implements ISetValue.SetValue
        Throw New NotImplementedException()
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
            End If
            If pos = "V" Then
                PictureBoxVC.Visible = True
            End If
            'Form1.DisConnect(num, num + 1) 'ТУТ ПИСДЕЦ С МНОГОПОТОЧНОСТЬЮ 
            Return 0
        Else
            PictureBoxHC.Visible = False
            PictureBoxVC.Visible = False
        End If
        Return Ia
    End Function

    Public Function chk_Sig() As Integer Implements ILinked.chk_Sig
        Return CheckSig(0) ' Тут что то сиранноу!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    End Function
End Class
