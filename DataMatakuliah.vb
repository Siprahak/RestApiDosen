Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form6

    Dim strArr() As String
    Dim strArr1() As String
    Dim count, c1 As Integer
    Dim str, str2 As String

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim uri As New Uri("http://localhost/restapidosen/matakuliah")
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = WebRequestMethods.Http.Get
        Dim response As HttpWebResponse = request.GetResponse()
        Dim reader As New StreamReader(response.GetResponseStream)
        Dim pagehtml As String = reader.ReadToEnd()
        response.Close()
        Str = pagehtml

        ' Parse the JSON as an array
        Dim dataList As JArray = JArray.Parse(Str)

        ' Initialize the DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID Matakuliah", GetType(String))
        dt.Columns.Add("Nama MataKuliah", GetType(String))
        dt.Columns.Add("Kode Matakuliah", GetType(String))
        dt.Columns.Add("SKS", GetType(String))

        ' Iterate through each item in the array and populate the DataTable
        For Each item In dataList
            Dim row As DataRow = dt.NewRow()
            row("ID Matakuliah") = item("id_matakuliah").ToString()
            row("Nama Matakuliah") = item("nama_matakuliah").ToString()
            row("Kode Matakuliah") = item("kode_matakuliah").ToString()
            row("SKS") = item("sks").ToString()
            dt.Rows.Add(row)
        Next

        ' Display the DataTable in the DataGridView
        DataGridViewMK.DataSource = dt
    End Sub
End Class
