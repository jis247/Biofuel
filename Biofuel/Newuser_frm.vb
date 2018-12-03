Imports System.Data.SqlClient
Public Class Newuser_frm
    Private Sub Newuser_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()
    End Sub

    Private Sub Save_btn_Click(sender As Object, e As EventArgs) Handles Save_btn.Click
        If cpass_txt.Text = pass_txt.Text Then

            Dim insertquery As String = "insert into Login_tbl(username,password) values(@username,@password)"
            Execuryourquery(insertquery)
            MessageBox.Show("User and Password saved")
            Me.Close()
        Else
            MessageBox.Show("Password did not match")
        End If
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub
    Public Sub Execuryourquery(ByVal query As String)
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@username", user_txt.Text)
        cmd.Parameters.AddWithValue("@password", cpass_txt.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
End Class