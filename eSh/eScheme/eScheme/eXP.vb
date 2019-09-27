Imports System.IO.MemoryMappedFiles
Imports System.Runtime.Serialization
Imports System.Threading
Imports eScheme

Public Class eXP
    Implements IConnectable
    Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public myName As String
    Public pins As Integer
    Public loc As Integer = 1

    'Dim points() As Apoint
    Dim mFile As MemoryMappedFile
    Dim eventX As EventWaitHandle
    Dim eventThread As Thread
    Dim wasSendEvent As Boolean = False '?
    Dim Signal As String = ""
    Dim lastSignal As String = " "
    Dim Servers As New ArrayList
    Dim MyServerNum As Integer
    Delegate Sub DoWork_()
    Private myDelegate As DoWork_ = New DoWork_(AddressOf DoWork)

    Dim m_Y As Integer
    Dim m_X As Integer

    'Structure Apoint
    '	Dim myCondition As Integer
    '	Dim Condition As Integer
    '	Dim pin As Integer
    'End Structure

    Public Sub New(rx As Integer, ry As Integer, n As Integer, aName As String, pinov As Integer, locat As Integer)
        InitializeComponent()
        X = rx
        Y = ry
        num = n
        myName = aName
        Label_Name.Text = aName
        pins = pinov
        loc = locat
        ' 2 cur для кнопки.
        Cursor = Form1.element_cur

        Me.Width = 30
        Me.Height = 15 + pins * 20
        If loc = 1 Then
            Me.Location = New Point(X + 5, Y - 25) 'Для настройки положения
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 35, Y - 25) 'Для настройки положения
        End If
        For j = 1 To pins
            Dim lb As New Label With {
                .Name = "lb" & j.ToString,
                .AutoSize = False,
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle,
                .Size = New Size(30, 20),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(0, j * 20 - 5),
                .Text = "/" & j.ToString
            }
            Me.Controls.Add(lb)
        Next

        Dim eComp As EComponent
        If Form1.Mode.StartsWith("eXP") Then 'Только при создании, при открытии файла не делать
            For j = 1 To pins
                eComp = New EComponent With {
                                    .aType = "ePoint",
                                    .numInArray = num + j
                            }
                Form1.Elements.Add(eComp)
                Dim p As New EPoint(X, Y + (j - 1) * 20, eComp.numInArray)
                p.links.Add(num)
                Form1.Controls.Add(p)
                eComp.component = p
            Next
        End If

        OnCreate()
    End Sub

    Sub OnCreate()
        mFile = MemoryMappedFile.CreateOrOpen(myName, 512)
        eventX = New EventWaitHandle(False, EventResetMode.ManualReset, "000" & myName)
        eventThread = New Thread(AddressOf WaitSignal) With {
            .IsBackground = True
        }

        DoWork()
        MyServerNum = GetServerLastNum() 'присвоить свободный
        Servers.Add(MyServerNum.ToString & "0")
        ToolTip1.SetToolTip(Label_Name, "MyServerNum = " & MyServerNum.ToString & " |  Всего обслуживается объектов объектов: " & Servers.Count.ToString)
        Write_mFile("")
        eventThread.Start()
    End Sub

    Sub WaitSignal()
        Try
            Do
                eventX.WaitOne()
                If wasSendEvent Then
                    If Read_mFile() = 1 Then
                        wasSendEvent = False
                        eventX.Reset()
                    End If
                    Continue Do
                End If
                'Выполняем необходимые действия
                Me.Invoke(myDelegate)
            Loop
        Catch ex As ThreadInterruptedException

        End Try


    End Sub

    Function Read_mFile() '-1 - ошибка, 0 - пустой файл, 1- все прочитали
        'Чтение из файла, сигнал добавляем в список, если он новый или список пустой
        'добавляем в список сервера и их состояние, последняя цифра N0 или N1
        Dim size1 As Integer
        Dim message As Char(), msg As String = ""
        Try
            Using reader As MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read)
                size1 = reader.ReadInt32(0)
            End Using
            Using reader = mFile.CreateViewAccessor(4, size1 * 2, MemoryMappedFileAccess.Read)
                message = New Char(size1 - 1) {}
                reader.ReadArray(Of Char)(0, message, 0, size1)
            End Using
            msg = New String(message)

            If msg = "" Then Return 0 'Файл только что создан (пустой)

            Dim arr() = msg.Split("%")

            Signal = arr(0)

            arr = arr(1).Split(";")
            Servers.Clear()
            Dim res As Integer = 1, str As String = "", k As Integer = 0
            For j = 0 To arr.Length - 1
                str = arr(j)
                Servers.Add(str)
                str = str.Remove(0, str.Length - 1)
                k = Integer.Parse(str)
                res *= k
            Next
            'ToolTip1.SetToolTip(Label_Name, "MyServerNum = " & MyServerNum.ToString & " |  Всего обслуживается объектов объектов: " & Servers.Count.ToString)
            Return res
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Sub Write_mFile(msg As String, Optional reset As Boolean = False)
        msg &= "%"

        Dim str As String = "", value As String = ""
        For j = 0 To Servers.Count - 1
            str = Servers(j)
            If reset Then
                str = str.Remove(str.Length - 1)
                str &= "0"
            End If

            If j = MyServerNum Then
                str = str.Remove(str.Length - 1)
                value = "1;"
            Else
                value = ";"
            End If
            str &= value
            msg &= str
        Next
        msg = msg.Remove(msg.Length - 1)

        Dim message As Char() = msg
        Dim size As Integer = message.Length

        Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 256, MemoryMappedFileAccess.Write)
            writer.Write(0, size)
            writer.WriteArray(Of Char)(4, message, 0, size)
        End Using
    End Sub

    Sub DoWork()
        Read_mFile()
        If Signal <> lastSignal Then

            Write_mFile(Signal)
            DoNext()
            Write_mFile(Signal)
        End If

    End Sub


    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change
        Dim q As Integer = Servers.Count - 1

        Dim msg As String = "A;" & (from - num).ToString & ";" & (condition + 50).ToString
        Write_mFile(msg, True)

        wasSendEvent = True
        eventX.Set()
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Dim str As String = MyServerNum.ToString & "0"
        Servers.Remove(str)
        str = MyServerNum.ToString & "1"
        Servers.Remove(str)
        Write_mFile("")

        eventThread.Interrupt()
        eventThread = Nothing
        eventX = Nothing
        mFile = Nothing
        Me.Dispose()
    End Sub

    Public Function ForSave() As ArrayList Implements IConnectable.ForSave
        Dim save As New ArrayList From {
            "eXP",
            num,
            X,
            Y,
            myName,
            pins,
            loc
        }
        Return save
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        Return 0 'Временно
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Return 0 'Временно
    End Function

    Private Sub DoNext()
        Dim numPoint As Integer, theSig As Integer
        Dim msg As String
        Dim aPoint As IConnectable
        Dim eComp As EComponent
        Try
            msg = Signal
            Dim arr() As String
            arr = msg.Split(";")
            If msg.StartsWith("A") Then 'Change

                numPoint = Integer.Parse(arr(1)) + num
                theSig = Integer.Parse(arr(2)) - 50
                eComp = Form1.Elements(numPoint)
                aPoint = eComp.component
                Form1.pointsInProcessSig.Clear()
                aPoint.Change(num, theSig)
            ElseIf msg.StartsWith("E") Then 'Check

            ElseIf msg.StartsWith("U") Then 'CheckUI

            End If
            lastSignal = Signal
        Catch ex As Exception

        End Try
    End Sub

    'Sub TestExcel()
    '    Dim xlApp As Microsoft.Office.Interop.Excel.Application
    '    Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
    '    Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet

    '    xlApp = CType(CreateObject("Excel.Application"),
    '            Microsoft.Office.Interop.Excel.Application)
    '    xlBook = CType(xlApp.Workbooks.Add,
    '            Microsoft.Office.Interop.Excel.Workbook)
    '    xlSheet = CType(xlBook.Worksheets(1),
    '            Microsoft.Office.Interop.Excel.Worksheet)

    '    ' The following statement puts text in the second row of the sheet.
    '    xlSheet.Cells(2, 2) = "This is column B row 2"
    '    ' The following statement shows the sheet.
    '    xlSheet.Application.Visible = True
    '    ' The following statement saves the sheet to the C:\Test.xls directory.
    '    xlSheet.SaveAs("C:\Test.xls")
    '    ' Optionally, you can call xlApp.Quit to close the workbook.
    'End Sub
    Function GetServerLastNum()
        Dim str As String = "", k As Integer
        k = Servers.Count

        If k = 0 Then Return 0

        Try
            str = Servers(Servers.Count - 1)
            str = str.Remove(str.Length - 1)
            k = Integer.Parse(str)
            Return k + 1
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Private Function IMovable_Move(from As IMovable, dX As Integer, dY As Integer) As Boolean Implements IMovable.Move
        Form1.moveArray.Add(Me)
        Dim mayMove(pins) As Boolean
        If from Is Me Then
            Dim m As IMovable
            Dim eComp As EComponent

            For j = 1 To pins
                eComp = Form1.Elements(num + j)
                m = eComp.component
                If m.Move(Me, dX, dY) = False Then Return False
            Next
            m_X = X + dX
            m_Y = Y + dY
            Return True
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
            Me.Location = New Point(X + 5, Y - 25) 'Для настройки положения
        ElseIf loc = 2 Then
            Me.Location = New Point(X - 35, Y - 25) 'Для настройки положения
        End If
    End Sub

    Private Sub Label_Name_MouseClick(sender As Object, e As MouseEventArgs) Handles Label_Name.MouseClick, Me.MouseClick
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
        'If e.Button = MouseButtons.Right Then
        '    ContextMenu1.Show(Me, e.X, e.Y)
        '    Exit Sub
        'End If
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
End Class
