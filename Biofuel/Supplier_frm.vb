Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class Supplier_frm
    Private Sub Supplier_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()

        'following textbox enabled false
        sid_txt.Enabled = False
        sname_txt.Enabled = False
        sadd_txt.Enabled = False
        mobile_txt.Enabled = False
        quantity_txt.Enabled = False

        bank_txt.Enabled = False
        Acc_txt.Enabled = False
        rate_txt.Enabled = False
        gst_txt.Enabled = False
        ifsc_txt.Enabled = False
        amt_txt.Enabled = False
        gst_1.Enabled = False
        sname_txt.Select()

        'set current date in date time picker
        DateTimePicker1.Value = DateTime.Today

        'display data in combobox
        bank_txt.Items.Add("ICICI Bank")
        bank_txt.Items.Add("Axis Bank")
        bank_txt.Items.Add("Bank of Baroda")
        bank_txt.Items.Add("Dena Bank")
        bank_txt.Items.Add("State Bank of India")
        bank_txt.Items.Add("Yes Bank")
        bank_txt.Items.Add("others")

        'record displayed from storage_tbl
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then

            TextBox1.Text = table.Rows(4)(3).ToString()
            TextBox2.Text = table.Rows(6)(3).ToString()
            TextBox3.Text = table.Rows(5)(3).ToString()
        End If

        Del_btn.Enabled = False
        Bill_btn.Enabled = False

        'display data in datagridview
        Dim com As String = "select Sdate,SId,SName,Product,Quantity,Amount from supplier_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "supplier_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Public Sub Execuryourquery(ByVal query As String)
        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Sdate", DateTimePicker1.Value)
        cmd.Parameters.AddWithValue("@SId", sid_txt.Text)
        cmd.Parameters.AddWithValue("@Sname", sname_txt.Text)
        cmd.Parameters.AddWithValue("@saddress", sadd_txt.Text)
        cmd.Parameters.AddWithValue("@mobile", mobile_txt.Text)

        If RadioButton1.Checked Then
            cmd.Parameters.AddWithValue("@Product", RadioButton1.Text)
        ElseIf RadioButton2.Checked Then
            cmd.Parameters.AddWithValue("@Product", RadioButton2.Text)
        Else
            cmd.Parameters.AddWithValue("@Product", RadioButton3.Text)
        End If
        cmd.Parameters.AddWithValue("@Quantity", quantity_txt.Text)
        cmd.Parameters.AddWithValue("@rate", rate_txt.Text)
        cmd.Parameters.AddWithValue("@gst", gst_txt.Text)
        cmd.Parameters.AddWithValue("@bank", bank_txt.Text)
        cmd.Parameters.AddWithValue("@acc_no", Acc_txt.Text)
        cmd.Parameters.AddWithValue("@ifsc", ifsc_txt.Text)
        cmd.Parameters.AddWithValue("@gst_per", gst_1.Text)
        cmd.Parameters.AddWithValue("@Amount", amt_txt.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub quantity_txt_LostFocus(sender As Object, e As EventArgs) Handles quantity_txt.LostFocus
        'lostfocus event used to display data from another table to this form
        If quantity_txt.Text = "" Then
            MsgBox("quantity can not be empty")
            Exit Sub
        End If
        Dim searchquery As String
        Dim s As String
        s = DateTimePicker1.Value
        If RadioButton1.Checked Then
            searchquery = "Select * from rate_tbl where mdate='" & s & "' "
            Dim cmd As New SqlCommand(searchquery, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                rate_txt.Text = table.Rows(0)(3).ToString()
                gst_1.Text = table.Rows(0)(10).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(3).ToString() * table.Rows(0)(10).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(3).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Please enter Current rate")
                Rate_frm.Show()
            End If
        ElseIf RadioButton2.Checked Then
            searchquery = "Select * from rate_tbl where mdate='" & s & "' "
            Dim cmd As New SqlCommand(searchquery, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                rate_txt.Text = table.Rows(0)(5).ToString()
                gst_1.Text = table.Rows(0)(12).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(5).ToString() * table.Rows(0)(12).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(5).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Please enter Current rate")
                Rate_frm.Show()
            End If
        Else
            searchquery = "Select * from rate_tbl where mdate='" & s & "' "
            Dim cmd As New SqlCommand(searchquery, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                rate_txt.Text = table.Rows(0)(4).ToString()
                gst_1.Text = table.Rows(0)(11).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(4).ToString() * table.Rows(0)(11).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(4).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Please enter Current rate")
                Rate_frm.Show()
            End If
        End If
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub

    Private Sub Add_btn_Click(sender As Object, e As EventArgs) Handles Add_btn.Click

        'auto generate id
        Dim GetCode As String = "0"
        Dim cmd As SqlCommand
        cmd = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP(1) * FROM supplier_tbl ORDER BY SId DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("SId")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        If (GetCode = "0") Then
            sid_txt.Text = "SUPP00001"
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)

            sid_txt.Text = "SUPP" + Format(OldNum + 1, "00000").ToString

        End If

        'enabled true
        sname_txt.Enabled = True
        sadd_txt.Enabled = True
        mobile_txt.Enabled = True
        quantity_txt.Enabled = True

        bank_txt.Enabled = True
        Acc_txt.Enabled = True
        ifsc_txt.Enabled = True

        Del_btn.Enabled = False
        Bill_btn.Enabled = False

        'clear textbox and combobox and radio button
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        bank_txt.Text = ""
        sname_txt.Clear()
        sadd_txt.Clear()
        rate_txt.Clear()
        gst_txt.Clear()
        mobile_txt.Clear()
        quantity_txt.Clear()

        Acc_txt.Clear()
        ifsc_txt.Clear()
        amt_txt.Clear()
        conn.Close()
        gst_1.Clear()
        sname_txt.Select()

        'display data in datagridview
        Dim com As String = "select Sdate,SId,SName,Product,Quantity,Amount from supplier_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "supplier_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Save_btn_Click(sender As Object, e As EventArgs) Handles Save_btn.Click

        'Cannot save if name is empty
        If sname_txt.Text = "" Then
            MsgBox("Cannot save if name is empty")
            sname_txt.Select()
            Exit Sub
        End If

        'Cannot save if address is empty
        If sadd_txt.Text = "" Then
            MsgBox("Cannot save if address is empty")
            sadd_txt.Select()
            Exit Sub
        End If

        'Cannot save if Mobile number is empty and less than 10 digit
        If mobile_txt.Text = "" Then
            MsgBox("Cannot save if mobile number is empty")
            mobile_txt.Select()
            Exit Sub
        ElseIf Len(mobile_txt.Text) < 10 Then
            MsgBox("Mobile cannot be less than 10 digit")
            mobile_txt.Select()
            Exit Sub
        End If


        'Cannot save if quantity is empty
        If quantity_txt.Text = "" Then
            MsgBox("Cannot save if quantity is empty")
            quantity_txt.Select()
            Exit Sub
        End If

        'Cannot save if account is empty
        If Acc_txt.Text = "" Then
            MsgBox("Cannot save if account number is empty")
            Acc_txt.Select()
            Exit Sub
        ElseIf Len(Acc_txt.Text) < 16 Then
            MsgBox("Account no. cannot be less than 18 digit")
            Acc_txt.Select()
            Exit Sub
        End If

        'Cannot save if ifsc is empty and less than 11
        If ifsc_txt.Text = "" Then
            MsgBox("Cannot save if IFSC code is empty")
            ifsc_txt.Select()
            Exit Sub
        ElseIf Len(ifsc_txt.Text) < 11 Then
            MsgBox("IFSC cannot be less than 11 digit")
            ifsc_txt.Select()
            Exit Sub
        End If


        'save
        Dim r As String
        Dim j As Double
        Dim n As Double
        Dim insertquery As String = "insert into supplier_tbl(SId,SName,saddress,mobile,Product,Quantity,bank,acc_no,ifsc,Amount,Sdate,rate,gst,gst_per) values(@SId,@SName,@saddress,@mobile,@Product,@Quantity,@bank,@acc_no,@ifsc,@Amount,@Sdate,@gst,@rate,@gst_per)"
        Execuryourquery(insertquery)
        MessageBox.Show("Record inserted successfully")

        'update in stock
        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox1.Text
                Exec(query1, j, n)
            End If
        ElseIf RadioButton2.Checked = True Then
            r = RadioButton2.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox2.Text
                Exec(query1, j, n)
            End If
        Else
            r = RadioButton3.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox3.Text
                Exec(query1, j, n)
            End If
        End If

        'record displayed from storage_tbl
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd1 As New SqlCommand(searchquery, conn)
        Dim da1 As New SqlDataAdapter(cmd1)
        Dim table1 As New DataTable
        da1.Fill(table1)
        If table1.Rows.Count > 0 Then

            TextBox1.Text = table1.Rows(4)(3).ToString()
            TextBox2.Text = table1.Rows(6)(3).ToString()
            TextBox3.Text = table1.Rows(5)(3).ToString()
        End If


        'display data in datagridview
        Dim com As String = "select Sdate,SId,SName,Product,Quantity,Amount from supplier_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "supplier_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Del_btn_Click(sender As Object, e As EventArgs) Handles Del_btn.Click
        Dim r As String
        Dim j As Double
        Dim n As Double

        'delete data from database
        Dim s1 As String
        s1 = sid_txt.Text
        Dim delquery As String = "delete from supplier_tbl where SId='" & s1 & "' "
        Execuryourquery(delquery)
        MessageBox.Show("Record deleted seccessfully")

        'delete in stock
        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox1.Text
                Exec1(query1, j, n)
            End If
        ElseIf RadioButton2.Checked = True Then
            r = RadioButton2.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox2.Text
                Exec1(query1, j, n)
            End If
        Else
            r = RadioButton3.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox3.Text
                Exec1(query1, j, n)
            End If
        End If

        'record displayed from storage_tbl
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd1 As New SqlCommand(searchquery, conn)
        Dim da1 As New SqlDataAdapter(cmd1)
        Dim table1 As New DataTable
        da1.Fill(table1)
        If table1.Rows.Count > 0 Then

            TextBox1.Text = table1.Rows(4)(3).ToString()
            TextBox2.Text = table1.Rows(6)(3).ToString()
            TextBox3.Text = table1.Rows(5)(3).ToString()
        End If

        'update data in datagridview
        Dim com As String = "select Sdate,SId,SName,Product,Quantity,Amount from supplier_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "supplier_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click

        'to display data in datagridview
        Dim s As String
        s = InputBox("Enter Supplier id")
        Dim searchquery As String = "Select Sdate,SId,SName,Product,Quantity,Amount from supplier_tbl where SId='" & s & "' "
        Dim adpt As New SqlDataAdapter(searchquery, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "supplier_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        'to display data in textboxes
        Dim searchquery1 As String = "Select * from supplier_tbl where SId='" & s & "' "
        Dim cmd As New SqlCommand(searchquery1, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            sid_txt.Text = table.Rows(0)(1).ToString()
            sname_txt.Text = table.Rows(0)(2).ToString()
            sadd_txt.Text = table.Rows(0)(3).ToString()
            mobile_txt.Text = table.Rows(0)(4).ToString()

            If table.Rows(0)(6).ToString() = "Yest" Then
                RadioButton1.Checked = True
            ElseIf table.Rows(0)(6).ToString() = "Water" Then
                RadioButton2.Checked = True
            Else
                RadioButton3.Checked = True
            End If
            quantity_txt.Text = table.Rows(0)(7).ToString()
            rate_txt.Text = table.Rows(0)(13).ToString()
            gst_txt.Text = table.Rows(0)(14).ToString()
            gst_1.Text = table.Rows(0)(15).ToString()
            bank_txt.Text = table.Rows(0)(8).ToString()
            Acc_txt.Text = table.Rows(0)(9).ToString()
            ifsc_txt.Text = table.Rows(0)(10).ToString()
            amt_txt.Text = table.Rows(0)(11).ToString()
            MessageBox.Show("Record found!")
            Del_btn.Enabled = True
            Bill_btn.Enabled = True
        Else
            MessageBox.Show("Sorry record not found")
        End If
    End Sub

    Public Sub Exec(ByVal query As String, ByVal j As Double, ByVal n As Double)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, z As Double
        k = quantity_txt.Text
        s = j + k
        z = n + k
        cmd.Parameters.AddWithValue("@Stock_in", s)
        cmd.Parameters.AddWithValue("@Stock", z)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Sub Exec1(ByVal query As String, ByVal j As Double, ByVal n As Double)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, z As Double
        k = quantity_txt.Text
        s = j - k
        z = n - k
        cmd.Parameters.AddWithValue("@Stock_in", s)
        cmd.Parameters.AddWithValue("@Stock", z)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub Bill_btn_Click(sender As Object, e As EventArgs) Handles Bill_btn.Click
        'bill using crystal report
        Supplier_bill.Show()
    End Sub


    Private Sub sname_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles sname_txt.KeyPress
        'name cannot be number on keypress event
        Select Case Asc(e.KeyChar)
            Case 8
            Case 65 To 90
            Case 97 To 122
            Case 32
            Case Else
                MsgBox("Should be a character")
        End Select
    End Sub

    Private Sub mobile_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mobile_txt.KeyPress
        'mobile cannot be character on keypress event
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be a number")
        End Select
    End Sub

    Private Sub Acc_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Acc_txt.KeyPress
        'account cannot be character on keypress event
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be a number")
        End Select
    End Sub

    Private Sub ifsc_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ifsc_txt.KeyPress

    End Sub

    Private Sub ifsc_txt_LostFocus(sender As Object, e As EventArgs) Handles ifsc_txt.LostFocus
        Dim ifsc As Boolean
        ifsc = Regex.IsMatch(ifsc_txt.Text, "[A-Z|a-z]{4}[0-9]{7}$")
        If Not ifsc Then
            MsgBox("Not a valid IFSC code")
        End If
    End Sub
End Class