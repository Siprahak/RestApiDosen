Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form4

    Dim strArr() As String
    Dim strArr1() As String
    Dim count, c1 As Integer
    Dim str, str2 As String

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnCrtMjr_Click(sender As Object, e As EventArgs) Handles BtnCrtMjr.Click
        ' Prepare the data to be sent in the request
        Dim iddosen As String = IptIdDosen.Text
        Dim idmk As String = IptIdMk.Text
        Dim smtr As String = IptSmtr.Text
        Dim ta As String = IptTa.Text

        ' Check if fields are not empty
        If String.IsNullOrEmpty(iddosen) Or String.IsNullOrEmpty(idmk) Or String.IsNullOrEmpty(smtr) Or String.IsNullOrEmpty(ta) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        ' Create the POST request
        Dim uri As New Uri("http://localhost/restapidosen/mengajar")
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Prepare the POST data
        Dim postData As String = $"id_dosen={iddosen}&id_matakuliah={idmk}&semester={smtr}&tahun_ajaran={ta}"
        Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
        request.ContentLength = byteArray.Length

        ' Write the POST data to the request stream
        Using dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
        End Using

        ' Get the response
        Try
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim responseFromServer As String = reader.ReadToEnd()

            ' Parse the JSON response
            Dim jsonResponse As JObject = JObject.Parse(responseFromServer)

            ' Display message based on the response
            If jsonResponse("status").ToString() = "1" Then
                MessageBox.Show("Data successfully added.")
            Else
                MessageBox.Show("Failed to add data.")
            End If

            ' Clean up
            reader.Close()
            response.Close()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try

        BtnShwMjr.PerformClick()
    End Sub

    Private Sub BtnDelMjr_Click(sender As Object, e As EventArgs) Handles BtnDelMjr.Click
        ' Memastikan bahwa ada baris yang dipilih di DataGridView
        If DataGridViewMjr.SelectedRows.Count > 0 Then
            ' Ambil id_dosen dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewMjr.SelectedRows(0)
            Dim idmjr As String = selectedRow.Cells("ID Mengajar").Value.ToString()

            ' Konfirmasi sebelum menghapus
            Dim confirmResult As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus dosen ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo)
            If confirmResult = DialogResult.Yes Then
                ' Jika Yes, panggil fungsi untuk menghapus data
                DeleteMjr(idmjr)
            End If
        Else
            MessageBox.Show("Pilih data dosen yang ingin dihapus!")
        End If
        BtnShwMjr.PerformClick()
    End Sub

    Private Sub BtnBackMjr_Click(sender As Object, e As EventArgs) Handles BtnBackMjr.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub DataGridViewMjr_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewMjr.CellContentClick
        ' Cek jika ada baris yang dipilih
        If e.RowIndex >= 0 Then
            ' Ambil data dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewMjr.Rows(e.RowIndex)

            ' Menampilkan data di TextBox
            IptIdMjr.Text = selectedRow.Cells("ID Mengajar").Value.ToString()
            IptIdDosen.Text = selectedRow.Cells("ID Dosen").Value.ToString()
            IptIdMk.Text = selectedRow.Cells("ID Matakuliah").Value.ToString()
            IptSmtr.Text = selectedRow.Cells("Semester").Value.ToString()
            IptTa.Text = selectedRow.Cells("Tahun Ajaran").Value.ToString()
        End If
    End Sub

    Private Sub BtnUpdMjr_Click(sender As Object, e As EventArgs) Handles BtnUpdMjr.Click
        ' Ambil data dari inputan TextBox
        Dim idmjr As String = IptIdMjr.Text
        Dim iddosen As String = IptIdDosen.Text
        Dim idmk As String = IptIdMk.Text
        Dim smtr As String = IptSmtr.Text
        Dim ta As String = IptTa.Text

        ' Cek apakah semua field sudah diisi
        If String.IsNullOrEmpty(idmjr) Or String.IsNullOrEmpty(iddosen) Or String.IsNullOrEmpty(idmk) Or String.IsNullOrEmpty(smtr) Or String.IsNullOrEmpty(ta) Then
            MessageBox.Show("Mohon lengkapi semua field.")
            Return
        End If

        ' Buat request PUT untuk update data
        Try
            Dim uri As New Uri("http://localhost/restapidosen/mengajar?id_mengajar=" & idmjr)
            Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
            request.Method = "PUT"
            request.ContentType = "application/x-www-form-urlencoded"

            ' Data yang akan dikirim
            Dim putData As String = $"id_dosen={iddosen}&id_matakuliah={idmk}&semester={smtr}&tahun_ajaran={ta}"
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(putData)
            request.ContentLength = byteArray.Length

            ' Tulis data ke request stream
            Using dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using

            ' Dapatkan respon dari server
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim responseFromServer As String = reader.ReadToEnd()

            ' Parse respon JSON
            Dim jsonResponse As JObject = JObject.Parse(responseFromServer)

            ' Tampilkan pesan berdasarkan hasil respon
            If jsonResponse("status").ToString() = "1" Then
                MessageBox.Show("Data berhasil diperbarui.")
            Else
                MessageBox.Show("Gagal memperbarui data.")
            End If

            ' Tutup reader dan respon
            reader.Close()
            response.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        BtnShwMjr.PerformClick()
    End Sub

    Private Sub BtnShwDosen_Click(sender As Object, e As EventArgs) Handles BtnShwDosen.Click
        Form5.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form6.Show()
    End Sub

    Private Sub BtnShwMjr_Click(sender As Object, e As EventArgs) Handles BtnShwMjr.Click
        Dim uri As New Uri("http://localhost/restapidosen/mengajar")
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = WebRequestMethods.Http.Get
        Dim response As HttpWebResponse = request.GetResponse()
        Dim reader As New StreamReader(response.GetResponseStream)
        Dim pagehtml As String = reader.ReadToEnd()
        response.Close()
        str = pagehtml

        ' Parse the JSON as an array
        Dim dataList As JArray = JArray.Parse(str)

        ' Initialize the DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID Mengajar", GetType(String))
        dt.Columns.Add("ID Dosen", GetType(String))
        dt.Columns.Add("ID Matakuliah", GetType(String))
        dt.Columns.Add("Semester", GetType(String))
        dt.Columns.Add("Tahun Ajaran", GetType(String))

        ' Iterate through each item in the array and populate the DataTable
        For Each item In dataList
            Dim row As DataRow = dt.NewRow()
            row("ID Mengajar") = item("id_mengajar").ToString()
            row("ID Dosen") = item("id_dosen").ToString()
            row("ID Matakuliah") = item("id_matakuliah").ToString()
            row("Semester") = item("semester").ToString()
            row("Tahun Ajaran") = item("tahun_ajaran").ToString()
            dt.Rows.Add(row)
        Next

        ' Display the DataTable in the DataGridView
        DataGridViewMjr.DataSource = dt
    End Sub

    Private Sub DeleteMjr(idmjr As String)
        Try
            ' URL API untuk menghapus dosen berdasarkan id
            Dim uri As New Uri("http://localhost/restapidosen/mengajar?id_mengajar=" & idmjr)
            Dim request As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)
            request.Method = "DELETE"

            ' Mendapatkan respons dari server
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim result As String = reader.ReadToEnd()

            ' Menampilkan pesan hasil
            MessageBox.Show("Data dosen berhasil dihapus!")

            ' Memperbarui tampilan DataGridView setelah data dihapus

        Catch ex As Exception
            MessageBox.Show("Gagal menghapus data dosen: " & ex.Message)
        End Try
    End Sub
End Class
