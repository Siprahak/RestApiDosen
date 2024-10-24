Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form5
    Dim strArr() As String
    Dim strArr1() As String
    Dim count, c1 As Integer
    Dim str, str2 As String

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim uri As New Uri("http://localhost/restapidosen/dosen")
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
        dt.Columns.Add("IdDosen", GetType(String))
        dt.Columns.Add("NamaDosen", GetType(String))
        dt.Columns.Add("NIDN", GetType(String))
        dt.Columns.Add("Email", GetType(String))

        ' Iterate through each item in the array and populate the DataTable
        For Each item In dataList
            Dim row As DataRow = dt.NewRow()
            row("IdDosen") = item("id_dosen").ToString()
            row("NamaDosen") = item("nama_dosen").ToString()
            row("NIDN") = item("nidn").ToString()
            row("Email") = item("email").ToString()
            dt.Rows.Add(row)
        Next

        ' Display the DataTable in the DataGridView
        DataGridViewDosen.DataSource = dt
    End Sub
End Class
