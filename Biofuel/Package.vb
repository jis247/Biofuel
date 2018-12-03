Imports System.Data.SqlClient
Public Class Package
    Private Sub Package_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()
        ComboBox2.Items.Add("Sold")
        ComboBox2.Items.Add("not sold")

        ComboBox1.Items.Add("Ethanol(Corn)")
        ComboBox1.Items.Add("Ethanol(sugarcane)")
        ComboBox1.Items.Add("Both")
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub

    Private Sub Display_btn_Click(sender As Object, e As EventArgs) Handles Display_btn.Click
        'validation
        If ComboBox1.SelectedItem = "" Then
            MsgBox("Product can not be empty")
            Exit Sub
        End If
        'validation
        If ComboBox2.SelectedItem = "" Then
            MsgBox("Status can not be empty")
            Exit Sub
        End If

        'display
        Dim i, z As Integer
        If ComboBox2.SelectedItem = "not sold" Then
            If ComboBox1.SelectedItem = "Both" Then
                Dim s1 As String

                s1 = "not sold"
                Dim com As String = "select * from Barrel_tbl where status='" & s1 & "'"
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
            If ComboBox1.SelectedItem = "Ethanol(Corn)" Then
                Dim s1 As String
                Dim s2 As String
                s1 = "not sold"
                s2 = "Ethanol(Corn)"
                Dim com As String = "select * from Barrel_tbl where status='" & s1 & "' and product='" & s2 & "' "
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
            If ComboBox1.SelectedItem = "Ethanol(sugarcane)" Then
                Dim s1 As String
                Dim s2 As String
                s1 = "not sold"
                s2 = "Ethanol(sugarcane)"
                Dim com As String = "select * from Barrel_tbl where status='" & s1 & "' and product='" & s2 & "' "
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
        ElseIf ComboBox2.SelectedItem = "Sold" Then
            If ComboBox1.SelectedItem = "Both" Then
                Dim s1 As String
                s1 = "not sold"
                Dim com As String = "select * from Barrel_tbl where status<>'" & s1 & "'"
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
            If ComboBox1.SelectedItem = "Ethanol(Corn)" Then
                Dim s1 As String
                Dim s2 As String
                s1 = "not sold"
                s2 = "Ethanol(Corn)"
                Dim com As String = "select * from Barrel_tbl where status<>'" & s1 & "' and product='" & s2 & "' "
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
            If ComboBox1.SelectedItem = "Ethanol(sugarcane)" Then
                Dim s1 As String
                Dim s2 As String
                s1 = "not sold"
                s2 = "Ethanol(sugarcane)"
                Dim com As String = "select * from Barrel_tbl where status<>'" & s1 & "' and product='" & s2 & "' "
                Dim adpt As New SqlDataAdapter(com, conn)
                Dim ds As New DataSet()
                adpt.Fill(ds, "Barrel_tbl")
                DataGridView1.DataSource = ds.Tables(0)
                i = DataGridView1.Rows.Count.ToString - 1
                TextBox1.Text = i
                z = i * 160
                TextBox2.Text = z
            End If
        End If
    End Sub

    Private Sub Report_btn_Click(sender As Object, e As EventArgs) Handles Report_btn.Click
        Paackage_frm.Show()
    End Sub
End Class