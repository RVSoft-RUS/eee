Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text
Imports System.Net.Sockets
Imports System.Net

<Serializable> Public Class License
    Private ReadOnly id As String = "0"         '1011
    Private ReadOnly name As String = " "       '3
    Private ReadOnly company As String = " "    '7
    Private ReadOnly contacts As String = " "   '9
    Private ReadOnly about As String = " "      '121
	Private ReadOnly endDate As String = "0"    '167
	Private ReadOnly stream As String = "0"     '255 ' = level

	Public Sub New(fileName As String)
        Dim arr As Hashtable
		Try

			Dim fStream As New FileStream(fileName, FileMode.Open, FileAccess.Read)
			Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
			arr = CType(myBinaryFormatter.Deserialize(fStream), Hashtable)
			fStream.Close()

			id = Encode(arr(1011))
			name = arr.Item(3)
			company = arr.Item(7)
			contacts = arr.Item(9)
			about = arr.Item(121)
			endDate = arr.Item(167)
			stream = arr.Item(255)
		Catch ex As Exception

			stream = "0"
		End Try
		Dim dNow As Date
		Try
			dNow = GetNetworkTime()
			If dNow.Year = 2019 And dNow.Month < 12 Then
				stream = "2"
			End If
			Dim TheEndDate As Long
			Try
				TheEndDate = Long.Parse(endDate)
				If dNow.Ticks > TheEndDate And TheEndDate > 0 Then
					stream = "0"
				End If
			Catch ex As Exception

			End Try

		Catch ex As Exception
			MsgBox("Для работы с временной лицензией необходим доступ к ntpServer As String = time.windows.com", MsgBoxStyle.Information, "Нет доступа к интернет...")
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
End Class
