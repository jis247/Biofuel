Imports System.Data.SqlClient
Public Class Login_frm
    Dim count As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Login_btn.Click
        conn = getconnect()
        conn.Open()
        Dim cmd As New SqlCommand("select * from Login_tbl where username=@username and password=@password", conn)
        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = TextBox2.Text
        Dim adp As New SqlDataAdapter(cmd)
        Dim tbl As New DataTable()
        adp.Fill(tbl)
        If tbl.Rows.Count() <= 0 Then
            MessageBox.Show("Username or Password invalid")
            TextBox1.Text = ""
            TextBox2.Text = ""
            count = count + 1
            If count > 2 Then
                MessageBox.Show("You have used 3 attempts.")
                Me.Close()
            End If
        Else
            'MessageBox.Show("Login success")
            MDIParent1.Show()
        End If
        conn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Cancel_btn.Click
        Me.Close()
    End Sub

    Private Sub Login_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Newuser_frm.Show()
    End Sub
End Class
