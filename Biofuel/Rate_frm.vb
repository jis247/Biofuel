Imports System.Data.SqlClient
Public Class Rate_frm
    Private Sub Rate_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()
        Dim regdate As Date = Date.Now()
        mdate.Text = regdate.ToString("dd-MM-yyyy")
        srate_txt.Select()
        DateTimePicker1.Refresh()
        DateTimePicker1.Value = DateTime.Today
        update_btn.Enabled = False
    End Sub

    Private Sub close_btn_Click(sender As Object, e As EventArgs) Handles close_btn.Click
        Me.Close()
    End Sub

    Private Sub clear_btn_Click(sender As Object, e As EventArgs) Handles clear_btn.Click
        srate_txt.Clear()
        crate_txt.Clear()
        yrate_txt.Clear()
        enzrate_txt.Clear()
        wrate_txt.Clear()
        ethanol1_txt.Clear()
        ethanol2_txt.Clear()
        sgst.Clear()
        cgst.Clear()
        yestgst_txt.Clear()
        enzygst_txt.Clear()
        watergst_txt.Clear()
        Ethanol1gst_txt.Clear()
        Ethanol2gst_txt.Clear()

    End Sub

    Private Sub save_btn_Click(sender As Object, e As EventArgs) Handles save_btn.Click
        'validation textbox cannot be empty
        If srate_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            srate_txt.Select()
            Exit Sub
        End If
        If crate_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            crate_txt.Select()
            Exit Sub
        End If
        If yrate_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            yrate_txt.Select()
            Exit Sub
        End If
        If enzrate_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            enzrate_txt.Select()
            Exit Sub
        End If
        If wrate_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            wrate_txt.Select()
            Exit Sub
        End If
        If ethanol1_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            ethanol1_txt.Select()
            Exit Sub
        End If
        If ethanol2_txt.Text = "" Then
            MsgBox("Cannot save if rate is empty")
            ethanol2_txt.Select()
            Exit Sub
        End If
        If sgst.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            sgst.Select()
            Exit Sub
        End If
        If cgst.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            cgst.Select()
            Exit Sub
        End If
        If yestgst_txt.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            yestgst_txt.Select()
            Exit Sub
        End If
        If enzygst_txt.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            enzygst_txt.Select()
            Exit Sub
        End If
        If watergst_txt.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            watergst_txt.Select()
            Exit Sub
        End If
        If Ethanol1gst_txt.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            Ethanol1gst_txt.Select()
            Exit Sub
        End If
        If Ethanol2gst_txt.Text = "" Then
            MsgBox("Cannot save if GST is empty")
            Ethanol2gst_txt.Select()
            Exit Sub
        End If

        'record saved in rate_tbl
        Dim insertquery As String = "insert into rate_tbl(sugarcane,corn,yest,enzyme,water,ethanol_corn,ethanol_sugarcane,sgst,corn_gst,yest_gst,enzyme_gst,water_gst,ethanol1_gst,ethanol2_gst,mdate) values(@sugarcane,@corn,@yest,@enzyme,@water,@ethanol_corn,@ethanol_sugarcane,@sgst,@corn_gst,@yest_gst,@enzyme_gst,@water_gst,@ethanol1_gst,@ethanol2_gst,@mdate)"
        execuryourquery(insertquery)
        MessageBox.Show("Record inserted successfully")
    End Sub
    Public Sub execuryourquery(ByVal query As String)
        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@sugarcane", srate_txt.Text)
        cmd.Parameters.AddWithValue("@corn", crate_txt.Text)
        cmd.Parameters.AddWithValue("@yest", yrate_txt.Text)
        cmd.Parameters.AddWithValue("@enzyme", enzrate_txt.Text)
        cmd.Parameters.AddWithValue("@water", wrate_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol_corn", ethanol1_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol_sugarcane", ethanol2_txt.Text)
        cmd.Parameters.AddWithValue("@sgst", sgst.Text)
        cmd.Parameters.AddWithValue("@corn_gst", cgst.Text)
        cmd.Parameters.AddWithValue("@yest_gst", yestgst_txt.Text)
        cmd.Parameters.AddWithValue("@enzyme_gst", enzygst_txt.Text)
        cmd.Parameters.AddWithValue("@water_gst", watergst_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol1_gst", Ethanol1gst_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol2_gst", Ethanol2gst_txt.Text)

        cmd.Parameters.AddWithValue("@mdate", mdate.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub search_btn_Click(sender As Object, e As EventArgs) Handles search_btn.Click
        'search and display on form
        Dim s As Date
        s = DateTimePicker1.Value.Date
        Dim searchquery As String = "Select * from rate_tbl where mdate='" & s & "' "
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            srate_txt.Text = table.Rows(0)(1).ToString()
            crate_txt.Text = table.Rows(0)(2).ToString()
            yrate_txt.Text = table.Rows(0)(3).ToString()
            enzrate_txt.Text = table.Rows(0)(4).ToString()
            wrate_txt.Text = table.Rows(0)(5).ToString()
            ethanol1_txt.Text = table.Rows(0)(6).ToString()
            ethanol2_txt.Text = table.Rows(0)(7).ToString()
            sgst.Text = table.Rows(0)(8).ToString()
            cgst.Text = table.Rows(0)(9).ToString()
            yestgst_txt.Text = table.Rows(0)(10).ToString()
            enzygst_txt.Text = table.Rows(0)(11).ToString()
            watergst_txt.Text = table.Rows(0)(12).ToString()
            Ethanol1gst_txt.Text = table.Rows(0)(13).ToString()
            Ethanol2gst_txt.Text = table.Rows(0)(14).ToString()

            MessageBox.Show("Record found!")
            update_btn.Enabled = True
        Else
            MessageBox.Show("Sorry record not found")
        End If
    End Sub

    Private Sub delete_btn_Click(sender As Object, e As EventArgs) Handles delete_btn.Click
        'delete record from database on date bases
        Dim s1 As String
        s1 = InputBox("enter date to delete(dd-MM-yyyy")
        Dim delquery As String = "delete from rate_tbl where mdate='" & s1 & "' "
        execuryourquery(delquery)
        MessageBox.Show("Record deleted seccessfully")
    End Sub


    Private Sub srate_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles srate_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub crate_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles crate_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub



    Private Sub yrate_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles yrate_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub enzrate_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles enzrate_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub wrate_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles wrate_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub ethanol1_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ethanol1_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub ethanol2_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ethanol2_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub



    Private Sub sgst_KeyPress(sender As Object, e As KeyPressEventArgs) Handles sgst.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub cgst_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cgst.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub yestgst_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles yestgst_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub enzygst_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles enzygst_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub watergst_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles watergst_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub Ethanol1gst_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Ethanol1gst_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub


    Private Sub Ethanol2gst_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Ethanol2gst_txt.KeyPress
        'validation Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub update_btn_Click(sender As Object, e As EventArgs) Handles update_btn.Click
        Dim s As Date
        s = DateTimePicker1.Value.Date
        Dim insertquery As String = "update rate_tbl set sugarcane=@sugarcane,corn=@corn,yest=@yest,enzyme=@enzyme,water=@water,ethanol_corn=@ethanol_corn,ethanol_sugarcane=@ethanol_sugarcane,sgst=@sgst,corn_gst=@corn_gst,yest_gst=@yest_gst,enzyme_gst=@enzyme_gst,water_gst=@water_gst,ethanol1_gst=@ethanol1_gst,ethanol2_gst=@ethanol2_gst where mdate='" & s & "'"
        Dim cmd As New SqlCommand(insertquery, conn)
        cmd.Parameters.AddWithValue("@sugarcane", srate_txt.Text)
        cmd.Parameters.AddWithValue("@corn", crate_txt.Text)
        cmd.Parameters.AddWithValue("@yest", yrate_txt.Text)
        cmd.Parameters.AddWithValue("@enzyme", enzrate_txt.Text)
        cmd.Parameters.AddWithValue("@water", wrate_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol_corn", ethanol1_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol_sugarcane", ethanol2_txt.Text)
        cmd.Parameters.AddWithValue("@sgst", sgst.Text)
        cmd.Parameters.AddWithValue("@corn_gst", cgst.Text)
        cmd.Parameters.AddWithValue("@yest_gst", yestgst_txt.Text)
        cmd.Parameters.AddWithValue("@enzyme_gst", enzygst_txt.Text)
        cmd.Parameters.AddWithValue("@water_gst", watergst_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol1_gst", Ethanol1gst_txt.Text)
        cmd.Parameters.AddWithValue("@ethanol2_gst", Ethanol2gst_txt.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
        MessageBox.Show("Record Updated successfully")
    End Sub
End Class