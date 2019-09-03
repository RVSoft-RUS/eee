Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.Serialization

Public Class Form1
    Public result As String = Nothing
    Dim iniFile As String
    Dim busFile As String
    Public ini As New Ini_
    Dim buses As New ArrayList
    Dim currentBus As New Bus

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newBus As String
        newBus = InputString(Me.Location.X + GroupBox1.Location.X + ComboBox1.Location.X, Me.Location.Y + GroupBox1.Location.Y + ComboBox1.Location.Y + ComboBox1.Height + 20)
        If newBus <> "" Then
            'If ComboBox1.SelectedIndex > 0 Then
            '    ComboBox1.SelectedIndex = 0
            'Else
            '    ComboBox1.SelectedIndex = -1
            'End If
            currentBus = New Bus
            currentBus.bus = newBus
            buses.Add(currentBus)
            ComboBox1.Items.Add(newBus)
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
            Data1.Rows.Clear()
        End If
    End Sub

    Function InputString(X As Integer, Y As Integer) As String
        Me.Enabled = False
        Dim p As New Point
        p.X = X
        p.Y = Y
        FormIn.Visible = True
        FormIn.Opacity = 0
        FormIn.Location = p
        Do While result = Nothing
            Application.DoEvents()
        Loop
        Me.Enabled = True
        Dim S As String = result.Trim
        result = Nothing
        Me.Activate()
        Return S
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim index As Integer
        index = ComboBox1.SelectedIndex
        If index < 0 Then Exit Sub
        Dim aBus As New Bus
        aBus = buses(index)
        If aBus.jobs.Count > 0 Then
            Dim answ As MsgBoxResult
            answ = MsgBox("Вы действительно хотите удалить выбранный раздел - " + aBus.bus + " из списка?" +
                          vbCrLf + "Вернуть записи для удаленного раздела будет невозможно.", vbYesNo, "Предупреждение!")
            If answ <> vbYes Then
                Exit Sub
            End If
        End If
        ComboBox1.Items.RemoveAt(index)
        ComboBox1.Text = ""
        GroupBox2.Text = ""
        Data1.Rows.Clear()
        buses.RemoveAt(index)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        '*****************************************************
        currentBus.jobs = New ArrayList
        For i = 0 To Data1.Rows.Count - 2
            Dim ajob As New job
            ajob.job_ = Data1.Item(0, i).Value
            ajob.who = CStr(Data1.Item(1, i).Value)
            ajob.what = Data1.Item(2, i).Value
            ajob.comment = Data1.Item(3, i).Value
            ajob.type = Data1.Item(4, i).Value
            ajob.Ndoc = Data1.Item(5, i).Value
            ajob.color = Data1.Item(3, i).Style.BackColor
            currentBus.jobs.Add(ajob)
        Next
        '****************************************************** Сохранение списка
        GroupBox2.Text = ComboBox1.Text
        currentBus = buses(ComboBox1.SelectedIndex)
        Data1.Rows.Clear()

        For i = 0 To currentBus.jobs.Count - 1
            Data1.Rows.Add()
            Dim ajob As New job
            ajob = currentBus.jobs(i)
            Data1.Item(0, i).Value = ajob.job_
            Data1.Item(1, i).Value = ajob.who
            Data1.Item(2, i).Value = ajob.what
            Data1.Item(3, i).Value = ajob.comment
            Data1.Item(4, i).Value = ajob.type
            Data1.Item(5, i).Value = ajob.Ndoc
            If ajob.color <> Nothing Then
                For j = 0 To 5
                    Data1.Item(j, i).Style.BackColor = ajob.color
                Next
            End If
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniFile = Application.UserAppDataPath
        If Not iniFile.EndsWith("\") Then iniFile += "\"
        iniFile += "settings.dat"
        busFile = Application.StartupPath
        If Not busFile.EndsWith("\") Then busFile += "\"
        busFile += "bus.dat"

        Dim fStream As FileStream
        Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter()
        Try
            fStream = New FileStream(iniFile, FileMode.Open, FileAccess.Read)
            ini = CType(myBinaryFormatter.Deserialize(fStream), Ini_)
        Catch ex As Exception

        Finally
            If Not (fStream Is Nothing) Then fStream.Close()
        End Try

        Me.Height = ini.h
        Me.Width = ini.w
        Dim p As New Point
        p.X = ini.left
        p.Y = ini.top
        Me.Location = p
        Data1.Columns(0).Width = ini.w1
        Data1.Columns(1).Width = ini.w2
        Data1.Columns(2).Width = ini.w3
        Data1.Columns(3).Width = ini.w4


        Try
            fStream = New FileStream(busFile, FileMode.Open, FileAccess.Read)
            buses = CType(myBinaryFormatter.Deserialize(fStream), ArrayList)
            For i = 0 To buses.Count - 1
                Dim aBus As New Bus
                aBus = buses.Item(i)
                ComboBox1.Items.Add(aBus.bus)
            Next
            If ini.selItem >= 0 Then
                ComboBox1.SelectedIndex = ini.selItem
            End If
        Catch ex As Exception
            If Not (fStream Is Nothing) Then fStream.Close()
            Exit Sub
        Finally
            If Not (fStream Is Nothing) Then fStream.Close()
        End Try


    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '*****************************************************
        currentBus.jobs = New ArrayList
        For i = 0 To Data1.Rows.Count - 2
            Dim ajob As New job
            ajob.job_ = Data1.Item(0, i).Value
            ajob.who = CStr(Data1.Item(1, i).Value)
            ajob.what = Data1.Item(2, i).Value
            ajob.comment = Data1.Item(3, i).Value
            ajob.type = Data1.Item(4, i).Value
            ajob.Ndoc = Data1.Item(5, i).Value
            ajob.color = Data1.Item(3, i).Style.BackColor
            currentBus.jobs.Add(ajob)
        Next
        '****************************************************** Сохранение списка
        ini.h = Me.Height
        ini.w = Me.Width
        Dim p As New Point
        p = Me.Location
        ini.left = p.X
        ini.top = p.Y
        ini.selItem = ComboBox1.SelectedIndex
        ini.w1 = Data1.Columns(0).Width
        ini.w2 = Data1.Columns(1).Width
        ini.w3 = Data1.Columns(2).Width
        ini.w4 = Data1.Columns(3).Width

        Dim fStream As FileStream
        Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter()
        Try
            fStream = New FileStream(iniFile, FileMode.Create, FileAccess.Write)
            myBinaryFormatter.Serialize(fStream, ini)
        Catch ex As Exception

        Finally
            If Not (fStream Is Nothing) Then fStream.Close()
        End Try

        Try
            Try
                File.Copy(busFile, busFile + ".last", True)
            Catch ex As Exception
                'первый запуск программы
            End Try

            fStream = New FileStream(busFile, FileMode.Create, FileAccess.Write)
            myBinaryFormatter.Serialize(fStream, buses)
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error was occured at null")
        Finally
            If Not (fStream Is Nothing) Then fStream.Close()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'currentBus.jobs = New ArrayList
        'For i = 0 To Data1.Rows.Count - 1
        '    Dim ajob As New job
        '    ajob.job_ = Data1.Item(0, i).Value
        '    ajob.who = CStr(Data1.Item(1, i).Value)
        '    ajob.what = Data1.Item(2, i).Value
        '    ajob.comment = Data1.Item(3, i).Value
        '    ajob.color = Data1.Item(3, i).Style.BackColor
        '    currentBus.jobs.Add(ajob)
        'Next
    End Sub

    Private Sub Data1_MouseClick(sender As Object, e As MouseEventArgs) Handles Data1.MouseClick
        If e.Button = MouseButtons.Right Then
            Try
                Dim clr As Color
                Dim result As DialogResult
                Dim i As Integer = Data1.CurrentCell.RowIndex
                clr = Data1.Item(0, i).Style.BackColor
                ColorDialog1.Color = clr
                result = ColorDialog1.ShowDialog()
                If result = vbCancel Then
                    Exit Sub
                End If
                clr = ColorDialog1.Color

                For j = 0 To 5
                    Data1.Item(j, i).Style.BackColor = clr
                Next
                Dim ajob As New job
                ajob = currentBus.jobs(i)
                ajob.color = clr
                currentBus.jobs(i) = ajob
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Data1_KeyUp(sender As Object, e As KeyEventArgs) Handles Data1.KeyUp
        If e.KeyCode = Keys.F4 Then
            Try
                Dim r As Integer = Data1.CurrentCell.RowIndex
                Dim c As Integer = Data1.CurrentCell.ColumnIndex
                If Data1.Item(c, r + 1).Value = Nothing Then
                    Data1.Item(c, r + 1).Value = Data1.Item(c, r).Value
                    Data1.CurrentCell = Data1.Item(c, r + 1)
                End If
                ComboBox1_SelectedIndexChanged(sender, e)
            Catch ex As Exception

            End Try
        End If
        If e.KeyCode = Keys.F5 Then
            Try
                Dim r As Integer = Data1.CurrentCell.RowIndex
                Dim c As Integer = Data1.CurrentCell.ColumnIndex
                Data1.Item(c, r + 1).Value = Data1.Item(c, r).Value
                Data1.CurrentCell = Data1.Item(c, r + 1)
                ComboBox1_SelectedIndexChanged(sender, e)
            Catch ex As Exception

            End Try
        End If

        If e.KeyCode = Keys.Insert Then
            Try
                Dim k As Integer = Data1.CurrentCell.RowIndex
                Data1.Rows.Add()

                For i = Data1.Rows.Count - 1 To k + 1 Step -1
                    For j = 0 To 5
                        Data1.Item(j, i).Value = Data1.Item(j, i - 1).Value
                        Data1.Item(j, i).Style.BackColor = Data1.Item(j, i - 1).Style.BackColor
                    Next
                Next
                For j = 0 To 5
                    Data1.Item(j, k).Value = Nothing
                    Data1.Item(j, k).Style.BackColor = Color.White
                Next
                ComboBox1_SelectedIndexChanged(sender, e)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        memories.Visible = True
        memories.Activate()
        memories.ListBox1.Items.Clear()
        memories.Text = "Напоминания (Все)"
        For i = 0 To buses.Count - 1
            Dim aBus As New Bus
            aBus = buses(i)
            For j = 0 To aBus.jobs.Count - 1
                Dim aJob As New job
                aJob = aBus.jobs(j)
                If aJob.type = "Напоминание" Then
                    memories.ListBox1.Items.Add(aBus.bus + " \ " + aJob.job_)
                End If
            Next
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        memories.Visible = True
        memories.Activate()
        memories.ListBox1.Items.Clear()
        memories.Text = "КД согласовано / на проработку"
        For i = 0 To buses.Count - 1
            Dim aBus As New Bus
            aBus = buses(i)
            For j = 0 To aBus.jobs.Count - 1
                Dim aJob As New job
                aJob = aBus.jobs(j)
                If aJob.type = "Согл. КД" Or aJob.type = "На проработку" Then
                    memories.ListBox1.Items.Add(aJob.job_ + vbTab + aJob.type + vbTab + vbTab + aJob.comment + vbTab + aJob.Ndoc)
                End If
            Next
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        memories.Visible = True
        memories.Activate()
        memories.ListBox1.Items.Clear()
        memories.Text = "Незавершенные задания"
        For i = 0 To buses.Count - 1
            Dim aBus As New Bus
            aBus = buses(i)
            For j = 0 To aBus.jobs.Count - 1
                Dim aJob As New job
                aJob = aBus.jobs(j)
                If aJob.type <> Nothing And aJob.what <> Nothing Then
                    If aJob.type = "Выполнить" And aJob.what.EndsWith("%") Then
                        memories.ListBox1.Items.Add(aBus.bus + " \ " + aJob.job_ + vbTab + vbTab + aJob.who + vbTab + aJob.what + vbTab + aJob.comment)
                    End If
                End If
            Next
        Next

    End Sub

End Class
