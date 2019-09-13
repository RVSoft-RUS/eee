Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text
Imports System.Net.Sockets
Imports System.Net
Imports System.Management
<Serializable> Public Class License


    Private my_id As String
    Private id As String = "0"         '1011
    Private name As String = " "       '3
    Private company As String = " "    '7
    Private contacts As String = " "   '9
    Private about As String = " "      '121
    Private endDate As String = "0"    '167
    Private stream As String = "0"     '255 ' = level

    Private lic_num As String = ""

    Public Sub New(fileName As String)

        Dim pc, un As String
        Dim num As Long = 10_000_000
        pc = My.Computer.Name + "юя"
        un = My.User.Name + "юя"

        For i = 0 To pc.Length - 1
            num += Asc(pc(i)) * 131
        Next

        For i = 0 To un.Length - 1
            num += Asc(un(i)) * 157
        Next

        pc = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData
        For i = 0 To pc.Length - 1
            num += Asc(pc(i)) * 119
        Next

        Try
            num *= 81
        Catch ex As Exception

        End Try

        my_id = Convert.ToString(num, 16).ToUpper
        NewLicense(fileName)
    End Sub

    Public Sub NewLicense(fileName As String)
        Dim arr As Hashtable
        Try

            Dim fStream As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
            arr = CType(myBinaryFormatter.Deserialize(fStream), Hashtable)
            fStream.Close()

            id = Encode(arr(1011))

            name = Encode(arr.Item(3))
            company = Encode(arr.Item(7))
            contacts = Encode(arr.Item(9))
            about = Encode(arr.Item(121))
            endDate = arr.Item(167)
            stream = arr.Item(255)
            Dim ff As New FileInfo(fileName)
            lic_num = ff.Name
            lic_num = lic_num.Replace(".elc", "")
			If id <> my_id.ToString Then
				stream = "0"
				MsgBox("Указанная лицензия №" + lic_num + " не подходит к Вашей версии ПО.")
				name = ""
				company = ""
				contacts = ""
				about = ""
				endDate = ""
				lic_num = ""
			End If
			If stream = "2" Then
				Form1.Level = Integer.Parse(stream)
                Exit Sub
            End If
		Catch ex As Exception
            stream = "0"
        End Try

        Dim dNow As Date
        Dim str As String = stream
        Dim lTime As Long
        lTime = (New Date(2010, 3, 1)).Ticks 'До 1 марта 2020 года бесплатно

        Try
            dNow = GetNetworkTime()
            If dNow.Ticks < lTime Then
                If stream <> "2" And stream <> "3" Then
                    stream = "3"

                    endDate = lTime.ToString
                End If

            End If
            Dim TheEndDate As Long
            Try
                TheEndDate = Long.Parse(endDate)
                If dNow.Ticks > TheEndDate And TheEndDate > 0 Then
                    stream = str
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
            MsgBox("Для работы с временной лицензией необходим доступ к ntpServer time.windows.com", MsgBoxStyle.Information, "Нет доступа к интернет...")
        End Try
        Form1.Level = Integer.Parse(stream)
    End Sub

    Function Encode(s As String) As String
        Dim sb As New StringBuilder
        Dim c() As Char
        Dim cur As Char
        c = s.ToCharArray
        Dim j As Integer = 1
        Dim k As Integer
        For i = 0 To c.Count - 1
            k = AscW(c(i))
            k = k + j
            cur = ChrW(k)
            sb.Append(cur)
            j += 1
            If j = 10 Then j = 1
        Next

        'Dim qs As String = ""
        'qs += ChrW(116)
        'qs += ChrW(105)
        'qs += ChrW(116)
        'qs += ChrW(116)
        'qs += ChrW(105)
        'qs += ChrW(101)
        'qs += ChrW(115)
        'MsgBox(qs)

        Return sb.ToString
    End Function

    Public Shared Function GetNetworkTime() As Date
        Const ntpServer As String = "time.windows.com"
        Dim ntpData = New Byte(47) {}

        ntpData(0) = &H1B

        Dim addresses = Dns.GetHostEntry(ntpServer).AddressList

        Dim ipEndPoint = New IPEndPoint(addresses(0), 123)

        Using socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            socket.Connect(ipEndPoint)

            socket.ReceiveTimeout = 3000

            socket.Send(ntpData)
            socket.Receive(ntpData)
            socket.Close()
        End Using

        Const serverReplyTime As Byte = 40

        Dim intPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime)

        Dim fractPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime + 4)

        intPart = SwapEndianness(intPart)
        fractPart = SwapEndianness(fractPart)

        Dim milliseconds = (intPart * 1000) + ((fractPart * 1000) \ &H100000000L)

        Dim networkDateTime = (New Date(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(CLng(milliseconds))

        Return networkDateTime.ToLocalTime()
    End Function

    Shared Function SwapEndianness(ByVal x As ULong) As UInteger
        Return CUInt(((x And &HFF) << 24) + ((x And &HFF00) << 8) + ((x And &HFF0000) >> 8) + ((x And &HFF000000UI) >> 24))
    End Function

    Public ReadOnly Property GetID As String
        Get
            Return my_id
        End Get
    End Property

    Public ReadOnly Property GetName As String
        Get
            Return name
        End Get
    End Property

    Public ReadOnly Property GetCompany As String
        Get
            Return company
        End Get
    End Property

    Public ReadOnly Property GetContacts As String
        Get
            Return contacts
        End Get
    End Property

    Public ReadOnly Property GetAbout As String
        Get
            Return about
        End Get
    End Property

    Public ReadOnly Property GetEndDate As String
        Get
            If endDate = "0" Then
                Return "неограничено"
            Else
                If endDate = "" Then endDate = "0"
                Return New DateTime(Long.Parse(endDate)).ToLongDateString
            End If
        End Get
    End Property

    Public ReadOnly Property GetLicNumber As String
        Get
            Return lic_num
        End Get
    End Property

    Public ReadOnly Property GetLevel As String
        Get
            Return Integer.Parse(stream)
        End Get
    End Property
End Class
