Imports System.IO.MemoryMappedFiles
Imports System.Runtime.Serialization
Imports System.Threading
Imports eScheme

Public Class eXP
    Implements IConnectable
    'Implements IMovable
    Public X As Integer
    Public Y As Integer
    Public num As Integer
    Public myName As String
    Public pins As Integer
    Public loc As Integer = 1

    'Dim points() As Apoint
    Dim mFile As MemoryMappedFile
    Dim evFile As MemoryMappedFile
    Dim eventX As EventWaitHandle
    Dim eventThread As Thread
    Dim wasSendEvent As Boolean = False
    Dim Signals As New ArrayList
    Delegate Sub DoNext_()
    Private myDelegate As DoNext_ = New DoNext_(AddressOf DoNext)
    Private listOfeventX As New ArrayList

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
        mFile = MemoryMappedFile.CreateOrOpen(myName, 64)
        evFile = MemoryMappedFile.CreateOrOpen(myName & "ev", 2048)
        eventX = New EventWaitHandle(False, EventResetMode.ManualReset, "000" & myName)
        eventThread = New Thread(AddressOf WaitSignal) With {
            .IsBackground = True
        }
        ReadAllEventX()
        listOfeventX.Add(eventX)
        WriteAllEventX()
        eventThread.Start()
    End Sub

    Sub WaitSignal()
        Try
            Do
                eventX.WaitOne()
                If wasSendEvent Then
                    wasSendEvent = False
                    eventX.Reset()
                    Continue Do
                End If
                'Выполняем необходимые действия
                DoWork()
            Loop
        Catch ex As ThreadInterruptedException

        End Try


    End Sub

    Sub ReadAllEventX()
        Try
            Using reader As MemoryMappedViewStream = evFile.CreateViewStream()
                Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
                listOfeventX = CType(myBinaryFormatter.Deserialize(reader), ArrayList)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub WriteAllEventX()
        Try
            Using reader As MemoryMappedViewStream = evFile.CreateViewStream()
                Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
                myBinaryFormatter.Serialize(reader, listOfeventX)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub DoWork()
        Dim size1 As Integer
        Dim message As Char(), msg As String
        Try
            Using reader As MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read)
                size1 = reader.ReadInt32(0)
            End Using
            Using reader = mFile.CreateViewAccessor(4, size1 * 2, MemoryMappedFileAccess.Read)
                message = New Char(size1 - 1) {}
                reader.ReadArray(Of Char)(0, message, 0, size1)
            End Using
            msg = New String(message)
            Signals.Add(msg)
            Me.Invoke(myDelegate)
        Catch ex As Exception

        End Try
    End Sub


    Public Sub Change(from As Integer, condition As Integer) Implements IConnectable.Change

        Dim msg As String = "A;" & (from - num).ToString & ";" & (condition + 50).ToString
        Dim message As Char() = msg
        Dim size As Integer = message.Length

        Using writer As System.IO.MemoryMappedFiles.MemoryMappedViewAccessor = mFile.CreateViewAccessor(0, 256, MemoryMappedFileAccess.Write)
            writer.Write(0, size)
            writer.WriteArray(Of Char)(4, message, 0, message.Length)
        End Using
        eventX.Set()
        wasSendEvent = True
    End Sub

    Private Sub IConnectable_Dispose() Implements IConnectable.Dispose
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
            msg = Signals(0)
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
            Signals.RemoveAt(0)
            If Signals.Count = 0 Then
                'Timer1.Stop()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
