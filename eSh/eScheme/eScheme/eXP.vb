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

    Dim dt As Date
    Private IsItMySignal() As Boolean 'Если сигнал от меня, то True

    Dim mFile As MemoryMappedFile
    Dim sFile As MemoryMappedFile
    Dim UIFile As MemoryMappedFile
    Dim eventX As EventWaitHandle
    Dim eventThread As Thread
    Dim wasSendEvent As Integer = 0
    Dim wasSendUI As Integer = 0
    Dim Signal As String = ""
    Dim lastSignal As String = " "
    Dim Servers As New ArrayList
    Dim MyServerNum As Integer
    Delegate Sub DoWork_()
    Private myDelegateDoWork As DoWork_ = New DoWork_(AddressOf DoWork)

    Dim m_Y As Integer
    Dim m_X As Integer

    Public Sub New(rx As Integer, ry As Integer, n As Integer, aName As String, pinov As Integer, locat As Integer, Optional arr As ArrayList = Nothing)
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

        ReDim IsItMySignal(pins + 1)
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

                IsItMySignal(j) = False
            Next
        Else
            If arr IsNot Nothing Then
                For j = 1 To pins
                    IsItMySignal(j) = arr(j)
                Next
            End If

        End If

        OnCreate()
    End Sub

    Sub OnCreate()
        mFile = MemoryMappedFile.CreateOrOpen(myName, 32)
        sFile = MemoryMappedFile.CreateOrOpen("s" & myName, 512)
        UIFile = MemoryMappedFile.CreateOrOpen("UI" & myName, 2048)
        eventX = New EventWaitHandle(False, EventResetMode.ManualReset, "000" & myName)
        eventThread = New Thread(AddressOf WaitSignal) With {
            .IsBackground = True
        }

        DoWork()
        MyServerNum = GetServerLastNum() 'присвоить свободный
        Servers.Add(MyServerNum.ToString)
        ToolTip1.SetToolTip(Label_Name, "MyServerNum = " & MyServerNum.ToString & " |  Всего обслуживается объектов: " & Servers.Count.ToString)
        Write_sFile()
        eventThread.Name = "MyServerNum = " & MyServerNum.ToString
        eventThread.Start()
    End Sub

    Sub WaitSignal()
        Try
            Do
                eventX.WaitOne()
                'Отправили изменение, ждем когда все получат и сбрасываем сигнальное состояние
                If wasSendEvent = 2 Then
                    wasSendEvent = 0
                    eventX.Reset()
                    'MsgBox(Now.Ticks - dt.Ticks)
                    Continue Do
                End If
                If wasSendEvent = 1 Then
                    dt = Now
                    Thread.Sleep(10)
                    wasSendEvent = 2

                    Continue Do
                End If
                'Отправили запрос UI, ждем ответа от всех
                If wasSendUI = 1 Then
                    Continue Do
                End If
                Me.Invoke(myDelegateDoWork)
            Loop
        Catch ex As ThreadInterruptedException

        End Try
    End Sub

    Function Read_mFile() '-1 - ошибка, 0 - пустой файл, 1- успешно
        'Чтение из файла, сигнал добавляем в список, если он новый или список пустой
        Dim size1 As Integer
        Dim message As Char(), msg As String = ""
        Try
            SyncLock (mFile)
                Using reader As MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read)
                    size1 = reader.ReadInt32(0)
                End Using
                Using reader = mFile.CreateViewAccessor(4, size1 * 2, MemoryMappedFileAccess.Read)
                    message = New Char(size1 - 1) {}
                    reader.ReadArray(Of Char)(0, message, 0, size1)
                End Using
            End SyncLock

            msg = New String(message)

            If msg = "" Then Return 0 'Файл только что создан (пустой)

            Signal = msg
            Return 1
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Sub Read_sFile()
        'добавляем в список сервера
        Dim size1 As Integer
        Dim message As Char(), msg As String = ""
        Try
            SyncLock (sFile)
                Using reader As MemoryMappedViewAccessor = sFile.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read)
                    size1 = reader.ReadInt32(0)
                End Using
                Using reader = sFile.CreateViewAccessor(4, size1 * 2, MemoryMappedFileAccess.Read)
                    message = New Char(size1 - 1) {}
                    reader.ReadArray(Of Char)(0, message, 0, size1)
                End Using
            End SyncLock

            msg = New String(message)

            Dim arr() = msg.Split(";")
            Servers.Clear()
            Dim str As String = ""
            For j = 0 To arr.Length - 1
                str = arr(j)
                If str <> "" Then Servers.Add(str)
            Next

        Catch ex As Exception

        End Try
        ToolTip1.SetToolTip(Label_Name, "MyServerNum = " & MyServerNum.ToString & " |  Всего обслуживается объектов: " & Servers.Count.ToString)
    End Sub

    Sub Write_mFile(msg As String)
        Dim message As Char() = msg
        Dim size As Integer = message.Length
        SyncLock (mFile)
            Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 32, MemoryMappedFileAccess.Write)
                writer.Write(0, size)
                writer.WriteArray(Of Char)(4, message, 0, size)
            End Using
        End SyncLock
    End Sub

    Sub Write_sFile()
        Dim msg As String = ""
        Dim str As String = ""
        For j = 0 To Servers.Count - 1
            str = Servers(j) & ";"
            msg &= str
        Next
        If msg.Contains(";") Then msg = msg.Remove(msg.Length - 1)

        Dim message As Char() = msg
        Dim size As Integer = message.Length
        SyncLock (sFile)
            Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = sFile.CreateViewAccessor(0, 512, MemoryMappedFileAccess.Write)
                writer.Write(0, size)
                writer.WriteArray(Of Char)(4, message, 0, size)
            End Using
        End SyncLock

    End Sub

    Sub DoWork()
        Read_mFile()
        Read_sFile()
        If (Signal <> lastSignal) Then
            DoNext()
        End If

    End Sub


    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change

        If condition <> 0 Then
            IsItMySignal(from - num) = True
            Dim msg As String = "A;" & (from - num).ToString & ";" & (condition + 50).ToString
            Write_mFile(msg)
            wasSendEvent = 1
            eventX.Set()
        End If
        If condition = 0 And IsItMySignal(from - num) Then
            IsItMySignal(from - num) = False
            Dim msg As String = "A;" & (from - num).ToString & ";" & (condition + 50).ToString
            Write_mFile(msg)
            wasSendEvent = 1
            eventX.Set()
        End If
        Read_sFile()

    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
        Dim str As String = MyServerNum.ToString
        Read_sFile()
        Servers.Remove(str)
        Write_sFile()

        eventThread.Interrupt()
        eventThread = Nothing
        eventX = Nothing
        mFile = Nothing
        sFile = Nothing
        UIFile = Nothing
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
            loc,
            IsItMySignal.ToList
        }
        Return save
    End Function

    Public Function CheckSig(from As Integer) As Integer Implements IConnectable.CheckSig
        If IsItMySignal(from - num) Then
            Return 0
        Else
            Dim eComp As EComponent = Form1.Elements(from)
            Dim p As EPoint = eComp.component
            Return p.Condition
        End If
    End Function

    Public Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single Implements IConnectable.CheckUI
        Dim msg As String = "U;" & (from - num).ToString & ";" & U.ToString & ";" & r_.ToString
        Write_mFile(msg)
        wasSendUI = 1
        Read_sFile()
        Clear_UIFile()
        lastSignal = " "
        eventX.Set()

        Dim s As Single
        Do While True
            Application.DoEvents()
            Thread.Sleep(10)
            s = ReadUIFile()
            If s >= 0 Then
                wasSendUI = 0
                eventX.Reset()
                Return s
            End If
        Loop
        Return 0 'Сделать при превышении времени

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
            ElseIf msg.StartsWith("U") Then 'CheckUI
                Form1.LabelSig.BackColor = Color.Violet
                Form1.isCheckUI = True
                Application.DoEvents()
                numPoint = Integer.Parse(arr(1)) + num
                Dim U As Single = Single.Parse(arr(2))
                Dim R As Single = Single.Parse(arr(3))
                eComp = Form1.Elements(numPoint)
                Dim p1 As EPoint = eComp.component
                Dim I As Single
                I = p1.CheckUI(num, U, R)
                WriteI_toUIFile(I)
                Form1.LabelSig.BackColor = Color.White
                Form1.isCheckUI = False
            End If
            lastSignal = Signal
        Catch ex As Exception

        End Try
    End Sub

    Function ReadUIFile() As Single
        Dim s As Single, sum As Single
        Using reader As MemoryMappedViewAccessor = UIFile.CreateViewAccessor(0, 1024, MemoryMappedFileAccess.Read)
            For j = 0 To Servers.Count - 1
                s = reader.ReadSingle(j * 4)
                If s < 0 Then
                    Return -1
                Else
                    sum += s
                End If
            Next
        End Using
        Return sum
    End Function

    Sub WriteI_toUIFile(s As Single)
        Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = UIFile.CreateViewAccessor(0, 1024, MemoryMappedFileAccess.Write)
            For j = 0 To Servers.Count - 1
                If Servers(j) = MyServerNum.ToString Then
                    writer.Write(0 + j * 4, s)
                    Exit For
                End If
            Next
        End Using
    End Sub

    Sub Clear_UIFile()
        Dim iSng As Single = -1.0
        Dim mySng As Single = 0.0
        Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = UIFile.CreateViewAccessor(0, 1024, MemoryMappedFileAccess.Write)
            Dim s As Single
            For j = 0 To Servers.Count - 1
                If Servers(j) = MyServerNum.ToString Then
                    s = mySng
                Else
                    s = iSng
                End If
                writer.Write(0 + j * 4, s)
            Next
        End Using
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
            k = Integer.Parse(str)
            Return k + 1
        Catch ex As Exception
            Return Integer.MinValue
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
            Dim eComp As EComponent
            Dim p As EPoint

            For j = 1 To pins
                eComp = Form1.Elements(num + j)
                p = eComp.component
                p.links.Remove(num)
                p.DeleteMe()
            Next

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
