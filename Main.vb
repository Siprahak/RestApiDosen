Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnDosen_Click(sender As Object, e As EventArgs) Handles BtnDosen.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub BtnMataKuliah_Click(sender As Object, e As EventArgs) Handles BtnMataKuliah.Click
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub BtnMengajar_Click(sender As Object, e As EventArgs) Handles BtnMengajar.Click
        Me.Hide()
        Form4.Show()
    End Sub
End Class
