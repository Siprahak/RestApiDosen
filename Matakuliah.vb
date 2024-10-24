Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form3

    Dim strArr() As String
    Dim strArr1() As String
    Dim count, c1 As Integer
    Dim str, str2 As String

    Private Sub DataGridViewMK_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewMK.CellContentClick
        ' Cek jika ada baris yang dipilih
        If e.RowIndex >= 0 Then
            ' Ambil data dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewMK.Rows(e.RowIndex)

            ' Menampilkan data di TextBox
            IptNamaMK.Text = selectedRow.Cells("Nama Matakuliah").Value.ToString()
            IptSksMK.Text = selectedRow.Cells("SKS").Value.ToString()
            IptKodeMK.Text = selectedRow.Cells("Kode Matakuliah").Value.ToString()
            IptIdMK.Text = selectedRow.Cells("ID Matakuliah").Value.ToString()
        End If
    End Sub

    Private Sub BtnUpdMK_Click(sender As Object, e As EventArgs) Handles BtnUpdMK.Click
        ' Ambil data dari inputan TextBox
        Dim idMk As String = IptIdMK.Text
        Dim namaMk As String = IptNamaMK.Text
        Dim KodeMk As String = IptKodeMK.Text
        Dim Sks As String = IptSksMK.Text

        ' Cek apakah semua field sudah diisi
        If String.IsNullOrEmpty(idMk) Or String.IsNullOrEmpty(namaMk) Or String.IsNullOrEmpty(KodeMk) Or String.IsNullOrEmpty(Sks) Then
            MessageBox.Show("Mohon lengkapi semua field.")
            Return
        End If

        ' Buat request PUT untuk update data
        Try
            Dim uri As New Uri("http://localhost/restapidosen/matakuliah?id_matakuliah=" & idMk)
            Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
            request.Method = "PUT"
            request.ContentType = "application/x-www-form-urlencoded"

            ' Data yang akan dikirim
            Dim putData As String = $"nama_matakuliah={namaMk}&kode_matakuliah={KodeMk}&sks={Sks}"
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
        BtnShwMK.PerformClick()
    End Sub

    Private Sub BtnDelMK_Click(sender As Object, e As EventArgs) Handles BtnDelMK.Click
        ' Memastikan bahwa ada baris yang dipilih di DataGridView
        If DataGridViewMK.SelectedRows.Count > 0 Then
            ' Ambil id_dosen dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewMK.SelectedRows(0)
            Dim idMk As String = selectedRow.Cells("ID Matakuliah").Value.ToString()

            ' Konfirmasi sebelum menghapus
            Dim confirmResult As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus dosen ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo)
            If confirmResult = DialogResult.Yes Then
                ' Jika Yes, panggil fungsi untuk menghapus data
                DeleteMatakuliah(idMk)
            End If
        Else
            MessageBox.Show("Pilih data dosen yang ingin dihapus!")
        End If
        BtnShwMK.PerformClick()
    End Sub

    Private Sub BtnBackMK_Click(sender As Object, e As EventArgs) Handles BtnBackMK.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub BtnCrtMK_Click(sender As Object, e As EventArgs) Handles BtnCrtMK.Click
        ' Prepare the data to be sent in the request
        Dim namaMk As String = IptNamaMK.Text
        Dim kodeMk As String = IptKodeMK.Text
        Dim Sks As String = IptSksMK.Text

        ' Check if fields are not empty
        If String.IsNullOrEmpty(namaMk) Or String.IsNullOrEmpty(kodeMk) Or String.IsNullOrEmpty(Sks) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        ' Create the POST request
        Dim uri As New Uri("http://localhost/restapidosen/matakuliah")
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Prepare the POST data
        Dim postData As String = $"nama_matakuliah={namaMk}&kode_matakuliah={kodeMk}&sks={Sks}"
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

        BtnShwMK.PerformClick()
    End Sub

    Private Sub BtnShwMK_Click(sender As Object, e As EventArgs) Handles BtnShwMK.Click
        Dim uri As New Uri("http://localhost/restapidosen/matakuliah")
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

    Private Sub DeleteMatakuliah(idMk As String)
        Try
            ' URL API untuk menghapus dosen berdasarkan id
            Dim uri As New Uri("http://localhost/restapidosen/matakuliah?id_matakuliah=" & idMk)
            Dim request As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)
            request.Method = "DELETE"

            ' Mendapatkan respons dari server
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim result As String = reader.ReadToEnd()

            ' Menampilkan pesan hasil
            MessageBox.Show("Data dosen berhasil dihapus!")

            ' Memperbarui tampilan DataGridView setelah data dihapus
            BtnShwMK.PerformClick()

        Catch ex As Exception
            MessageBox.Show("Gagal menghapus data dosen: " & ex.Message)
        End Try
    End Sub
End Class
