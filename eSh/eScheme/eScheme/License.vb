Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text

<Serializable> Public Class License
    Private ReadOnly id As String = "0"         '1011
    Private ReadOnly name As String = " "       '3
    Private ReadOnly company As String = " "    '7
    Private ReadOnly contacts As String = " "   '9
    Private ReadOnly about As String = " "      '121
    Private ReadOnly endDate As String = 0      '167
    Private ReadOnly stream As String = 0       '255 ' = level

    Public Sub New(fileName As String)
        Dim arr As Hashtable
        Try

            Dim fStream As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim myBinaryFormatter As New Formatters.Binary.BinaryFormatter
            arr = CType(myBinaryFormatter.Deserialize(fStream), Hashtable)
            fStream.Close()

            id = encode(arr(1011))
            name = arr.Item(3)
            company = arr.Item(7)
            contacts = arr.Item(9)
            about = arr.Item(121)
            endDate = arr.Item(167)
            stream = arr.Item(255)
        Catch ex As Exception

            stream = 0
        End Try

    End Sub

    Function encode(s As String) As String
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
End Class
