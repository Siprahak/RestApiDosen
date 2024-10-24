Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form2

    Dim strArr() As String
    Dim strArr1() As String
    Dim count, c1 As Integer
    Dim str, str2 As String

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnBackDosen_Click(sender As Object, e As EventArgs) Handles BtnBackDosen.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub BtnShwDosen_Click(sender As Object, e As EventArgs) Handles BtnShwDosen.Click
        Dim uri As New Uri("http://localhost/restapidosen/dosen")
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


    Private Sub IptNamaDosen_TextChanged(sender As Object, e As EventArgs) Handles IptNamaDosen.TextChanged

    End Sub

    Private Sub IptNIDN_TextChanged(sender As Object, e As EventArgs) Handles IptNIDN.TextChanged

    End Sub

    Private Sub IptEmailDosen_TextChanged(sender As Object, e As EventArgs) Handles IptEmailDosen.TextChanged

    End Sub

    Private Sub BtnUpdDosen_Click(sender As Object, e As EventArgs) Handles BtnUpdDosen.Click
        ' Ambil data dari inputan TextBox
        Dim idDosen As String = IptIdDosen.Text
        Dim namaDosen As String = IptNamaDosen.Text
        Dim nidn As String = IptNIDN.Text
        Dim email As String = IptEmailDosen.Text

        ' Cek apakah semua field sudah diisi
        If String.IsNullOrEmpty(idDosen) Or String.IsNullOrEmpty(namaDosen) Or String.IsNullOrEmpty(nidn) Or String.IsNullOrEmpty(email) Then
            MessageBox.Show("Mohon lengkapi semua field.")
            Return
        End If

        ' Buat request PUT untuk update data
        Try
            Dim uri As New Uri("http://localhost/restapidosen/dosen?id_dosen=" & idDosen)
            Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
            request.Method = "PUT"
            request.ContentType = "application/x-www-form-urlencoded"

            ' Data yang akan dikirim
            Dim putData As String = $"nama_dosen={namaDosen}&nidn={nidn}&email={email}"
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

        BtnShwDosen.PerformClick()
    End Sub

    Private Sub DataGridViewDosen_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewDosen.CellContentClick
        ' Cek jika ada baris yang dipilih
        If e.RowIndex >= 0 Then
            ' Ambil data dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewDosen.Rows(e.RowIndex)

            ' Menampilkan data di TextBox
            IptNamaDosen.Text = selectedRow.Cells("NamaDosen").Value.ToString()
            IptEmailDosen.Text = selectedRow.Cells("Email").Value.ToString()
            IptNIDN.Text = selectedRow.Cells("NIDN").Value.ToString()
            IptIdDosen.Text = selectedRow.Cells("IdDosen").Value.ToString()
        End If
    End Sub

    Private Sub BtnDelDosen_Click(sender As Object, e As EventArgs) Handles BtnDelDosen.Click
        ' Memastikan bahwa ada baris yang dipilih di DataGridView
        If DataGridViewDosen.SelectedRows.Count > 0 Then
            ' Ambil id_dosen dari baris yang dipilih
            Dim selectedRow As DataGridViewRow = DataGridViewDosen.SelectedRows(0)
            Dim idDosen As String = selectedRow.Cells("IdDosen").Value.ToString()

            ' Konfirmasi sebelum menghapus
            Dim confirmResult As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus dosen ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo)
            If confirmResult = DialogResult.Yes Then
                ' Jika Yes, panggil fungsi untuk menghapus data
                DeleteDosen(idDosen)
            End If
        Else
            MessageBox.Show("Pilih data dosen yang ingin dihapus!")
        End If
        BtnShwDosen.PerformClick()
    End Sub

    Private Sub IptIdDosen_TextChanged(sender As Object, e As EventArgs) Handles IptIdDosen.TextChanged

    End Sub

    Private Sub BtnCrtDosen_Click(sender As Object, e As EventArgs) Handles BtnCrtDosen.Click
        ' Prepare the data to be sent in the request
        Dim namaDosen As String = IptNamaDosen.Text
        Dim nidn As String = IptNIDN.Text
        Dim email As String = IptEmailDosen.Text

        ' Check if fields are not empty
        If String.IsNullOrEmpty(namaDosen) Or String.IsNullOrEmpty(nidn) Or String.IsNullOrEmpty(email) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        ' Create the POST request
        Dim uri As New Uri("http://localhost/restapidosen/dosen")
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Prepare the POST data
        Dim postData As String = $"nama_dosen={namaDosen}&nidn={nidn}&email={email}"
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

        BtnShwDosen.PerformClick()
    End Sub

    Private Sub DeleteDosen(idDosen As String)
        Try
            ' URL API untuk menghapus dosen berdasarkan id
            Dim uri As New Uri("http://localhost/restapidosen/dosen?id_dosen=" & idDosen)
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
