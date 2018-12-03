Imports System.Data.SqlClient
Public Class Storage_frm
    Private Sub Display_btn_Click(sender As Object, e As EventArgs) Handles Display_btn.Click
        'only displays data in datagridview
        Dim com As String = "select * from storage_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "storage_tbl")
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.Columns(0).Width = 200
        DataGridView1.Columns(1).Width = 125
        DataGridView1.Columns(2).Width = 125
        DataGridView1.Columns(3).Width = 125

    End Sub

    Private Sub Storage_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()
        Dim regdate As Date = Date.Now()
        Sdate.Text = regdate.ToString("dd-MM-yyyy")
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub
End Class