Imports eScheme

Public Class eBat
	Implements IConnectable
    Implements ISetValue, IMovable
    Public X As Integer
	Public Y As Integer
	Public num As Integer
    Public U As Single
    Public I As Single

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, u_ As Integer, i_ As Single)
		InitializeComponent()
		X = rx
		Y = ry
		num = n
		U = u_
		I = i_
		Cursor = Form1.element_cur
		Dim eComp As EComponent
		Dim econ As IConnectable
		Me.Location = New Point(X + 5, Y - 2) 'Для настройки положения

		If Form1.Mode = "eBat" Then 'Только при создании, при открытии файла не делать
			eComp = New EComponent With {
							.aType = "ePoint",
							.numInArray = num + 1
						}
			Form1.Elements.Add(eComp)
			Dim p As New EPoint(X, Y + 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 1)
			econ = eComp.component
			econ.Change(num, -1)

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 2
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 60, Y, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 2)
			econ = eComp.component
			econ.Change(num, 15)

			eComp = New EComponent With {
					.aType = "ePoint",
					.numInArray = num + 3
				}
			Form1.Elements.Add(eComp)
			p = New EPoint(X + 60, Y + 20, eComp.numInArray)
			p.links.Add(num)
			Form1.Controls.Add(p)
			eComp.component = p

			eComp = Form1.Elements(num + 3)
			econ = eComp.component
			econ.Change(num, 30)

            'Form1.TextBox1.Text &= "eBat #" + CStr(num) + " changed (3) points" + vbCrLf
        End If

	End Sub

	Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
		If from = num + 1 And condition <> -1 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If
		If from = num + 2 And condition <> 15 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If
		If from = num + 3 And condition <> 30 Then
			MsgBox("Какого хуя сюда прилетает посторонний сигнал?", vbCritical, "Источник питания")
		End If

	End Sub

	Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
		Me.Dispose()
	End Sub

	Public Function ForSave() As ArrayList Implements IConnectable.ForSave
		Dim save As New ArrayList From {
			"eBat",
			num,
			X,
			Y,
			U,
			I
		}
		Return save
	End Function

	Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
		Select Case from
			Case num + 1
				Return -1
			Case num + 2
				Return 15
			Case Else ' num + 3
				Return 30
		End Select
	End Function

	Private Sub EBat_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
		If e.Button = MouseButtons.Right Then
			ContextMenu1.Show(Me, e.X, e.Y)
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

			eComp = Form1.Elements(num + 3)
			p = eComp.component 'Вторая точка 
			p.links.Remove(num)
			If p.links.Count = 0 Then
				p.DeleteMe()
			End If

            Form1.Delete(num)
            Form1.f.Batt = 0
        End If
	End Sub

	Private Sub EBat_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
		ToolTip1.SetToolTip(Me, "Напряжение " + CStr(U) + "В" + vbCrLf + "Ток потребления " + CStr(Math.Round(I, 3)) + " A")
	End Sub

	Private Sub ЗадатьНапряжениеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗадатьНапряжениеToolStripMenuItem.Click
		Form1.Enabled = False
		DialogForm.Show(Me)
		DialogForm.Left = X + Form1.Left
		DialogForm.Top = Y + Form1.Top
        DialogForm.OnView("Напряжение источника питания, В", Me, U.ToString)
    End Sub

    Public Sub SetValue(value As Single) Implements ISetValue.SetValue
        U = value
        Form1.NeedSave = True
        Form1.pointsInProcessUI.Clear()
        CheckUI(0, 0, 0)
    End Sub

    Public Function CheckUI(from As Integer, U_ As Single, Optional t As Integer = 0) As Single Implements IConnectable.CheckUI
        Dim q As ArrayList = Form1.Elements
        Form1.LabelSig.BackColor = Color.Violet
        Form1.isCheckUI = True
        Application.DoEvents()
        Dim eComp As EComponent = Form1.Elements(num + 2)
        Dim p1 As EPoint = eComp.component
        eComp = Form1.Elements(num + 3)
        Dim p2 As EPoint = eComp.component
        I = p1.CheckUI(num, CSng(U)) + p2.CheckUI(num, CSng(U))
        Form1.LabelSig.BackColor = Color.White
        Form1.isCheckUI = False
        Return 0
    End Function

    Private Sub EGND_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
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
            If Not mayMove Then
                Return False
            End If
            eComp = Form1.Elements(num + 3)
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
        Me.Location = New Point(X + 5, Y - 2)
    End Sub
End Class
