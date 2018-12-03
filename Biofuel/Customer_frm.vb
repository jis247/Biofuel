Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class Customer_frm
    Private Sub Customer_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()
        DateTimePicker1.Enabled = False
        'set datetimepicker to current date
        DateTimePicker1.Value = DateTime.Today

        'display data from storage_tbl to Customer_frm
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then

            fethanol_txt.Text = table.Rows(2)(3).ToString()
            fEthanol2_txt.Text = table.Rows(3)(3).ToString()
        End If

        'display data in datagridview
        Dim com As String = "select Cdate,CId,CName,Product,Quantity,Amount from customer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "customer_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        'textbox and combobox enabled fales
        cid_txt.Enabled = False
        cname_txt.Enabled = False
        cadd_txt.Enabled = False
        mobile_txt.Enabled = False


        barrel_txt.Enabled = False
        quantity_txt.Enabled = False
        rate_txt.Enabled = False
        gst_txt.Enabled = False
        gst_1.Enabled = False
        bank_txt.Enabled = False
        Acc_txt.Enabled = False
        ifsc_txt.Enabled = False
        amt_txt.Enabled = False
        cname_txt.Select()

        Del_btn.Enabled = False
        Bill_btn.Enabled = False

        'add items to combobox
        bank_txt.Items.Add("ICICI Bank")
        bank_txt.Items.Add("Axis Bank")
        bank_txt.Items.Add("Bank of Baroda")
        bank_txt.Items.Add("Dena Bank")
        bank_txt.Items.Add("State Bank of India")
        bank_txt.Items.Add("Yes Bank")
        bank_txt.Items.Add("others")


    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub

    Private Sub Add_btn_Click(sender As Object, e As EventArgs) Handles Add_btn.Click
        'auto generate customer id
        Dim GetCode As String = "0"
        Dim cmd As SqlCommand
        cmd = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP(1) * FROM customer_tbl ORDER BY CId DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("CId")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            'conn.Close()
        End Try

        If (GetCode = "0") Then
            cid_txt.Text = "CUST00001"
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)

            cid_txt.Text = "CUST" + Format(OldNum + 1, "00000").ToString

        End If

        'clear textbox......
        cname_txt.Clear()
        cadd_txt.Clear()
        mobile_txt.Clear()

        barrel_txt.Clear()
        quantity_txt.Clear()
        rate_txt.Clear()
        gst_txt.Clear()
        gst_1.Clear()
        bank_txt.Text = ""
        Acc_txt.Clear()
        ifsc_txt.Clear()
        amt_txt.Clear()
        ListBox1.Items.Clear()

        'textbox enabled false
        cname_txt.Enabled = True
        cadd_txt.Enabled = True
        mobile_txt.Enabled = True


        barrel_txt.Enabled = True
        bank_txt.Enabled = True
        Acc_txt.Enabled = True
        ifsc_txt.Enabled = True

        Del_btn.Enabled = False
        Bill_btn.Enabled = False

        RadioButton1.Checked = False
        RadioButton2.Checked = False

        'display data in datagridview
        Dim com As String = "select Cdate,CId,CName,Product,Quantity,Amount from customer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "customer_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        conn.Close()
    End Sub

    Private Sub barrel_txt_LostFocus(sender As Object, e As EventArgs) Handles barrel_txt.LostFocus
        'textbox cannot be empty
        If barrel_txt.Text = "" Then
            MsgBox("Barrel can not be empty")
            Exit Sub
        End If
        quantity_txt.Text = barrel_txt.Text * 160

        'valdiation
        If RadioButton1.Checked = True Then
            Dim o, p As Integer
            o = quantity_txt.Text
            p = fethanol_txt.Text
            If o > p Then
                MsgBox(" Not enough stock")
                Exit Sub
            End If
        ElseIf RadioButton2.Checked = True Then
            Dim o, p As Integer
            o = quantity_txt.Text
            p = fEthanol2_txt.Text
            If o > p Then
                MsgBox(" Not enough stock")
                Exit Sub
            End If
        End If

        'to display data from rate_tbl at lostfocus event
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
                rate_txt.Text = table.Rows(0)(6).ToString()
                gst_1.Text = table.Rows(0)(13).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(6).ToString() * table.Rows(0)(13).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(6).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Plz enter Current rate")
                Rate_frm.Show()
            End If
        Else
            searchquery = "Select * from rate_tbl where mdate='" & s & "' "
            Dim cmd As New SqlCommand(searchquery, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                rate_txt.Text = table.Rows(0)(7).ToString()
                gst_1.Text = table.Rows(0)(14).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(7).ToString() * table.Rows(0)(14).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(7).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Plz enter Current rate")
                Rate_frm.Show()
            End If

        End If
    End Sub

    Private Sub Save_btn_Click(sender As Object, e As EventArgs) Handles Save_btn.Click
        'textbox cannot be empty
        If cname_txt.Text = "" Then
            MsgBox("Cannot save if name is empty")
            cname_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty
        If cadd_txt.Text = "" Then
            MsgBox("Cannot save if address is empty")
            cadd_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and cannot be less than 10 digit
        If mobile_txt.Text = "" Then
            MsgBox("Cannot save if mobile number is empty")
            mobile_txt.Select()
            Exit Sub
        ElseIf Len(mobile_txt.Text) < 10 Then
            MsgBox("Mobile cannot be less than 10 digit")
            mobile_txt.Select()
            Exit Sub
        End If



        'textbox cannot be empty
        If barrel_txt.Text = "" Then
            MsgBox("Cannot save if barrel is empty")
            barrel_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and cannot be less than 18 digit
        If Acc_txt.Text = "" Then
            MsgBox("Cannot save if account number is empty")
            Acc_txt.Select()
            Exit Sub
        ElseIf Len(Acc_txt.Text) < 16 Then
            MsgBox("Account cannot be less than 18 digit")
            Acc_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and cannot be less than 11 digit
        If ifsc_txt.Text = "" Then
            MsgBox("Cannot save if ifsc code is empty")
            ifsc_txt.Select()
            Exit Sub
        ElseIf Len(ifsc_txt.Text) < 11 Then
            MsgBox("IFSC cannot be less than 11 digit")
            ifsc_txt.Select()
            Exit Sub
        End If


        Dim r As String
        Dim j As Integer
        Dim n As Integer
        conn.Open()

        'record saved
        Dim insertquery As String = "insert into customer_tbl(CId,CName,caddress,mobile,Product,Quantity,bank,acc_no,ifsc,Amount,Cdate,rate,gst,gst_per,no_of_barrel) values(@CId,@CName,@caddress,@mobile,@Product,@Quantity,@bank,@acc_no,@ifsc,@Amount,@Cdate,@gst,@rate,@gst_per,@no_of_barrel)"
        Execuryourquery(insertquery)
        MessageBox.Show("Record inserted successfully")

        'record updated in storage_tbl
        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_out from storage_tbl where Item='" & r & "' "
            Dim cmd4 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd4)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = fethanol_txt.Text
                Exec1(query1, j, n)
            End If
        Else
            r = RadioButton2.Text
            Dim query1 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_out from storage_tbl where Item='" & r & "' "
            Dim cmd4 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd4)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = fEthanol2_txt.Text
                Exec1(query1, j, n)
            End If

        End If

        'record displayed in datagridview
        Dim com As String = "select Cdate,CId,CName,Product,Quantity,Amount from customer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "customer_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        'record displayed from storage_tbl
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd1 As New SqlCommand(searchquery, conn)
        Dim da1 As New SqlDataAdapter(cmd1)
        Dim table1 As New DataTable
        da1.Fill(table1)
        If table1.Rows.Count > 0 Then

            fethanol_txt.Text = table1.Rows(2)(3).ToString()
            fEthanol2_txt.Text = table1.Rows(3)(3).ToString()
        End If


        'barrel_tbl updated
        Dim s1 As String
        Dim s2 As String
        Dim s3 As String
        Dim kl As Integer
        Dim z As Integer
        z = barrel_txt.Text
        s3 = cid_txt.Text
        s1 = "not sold"
        If RadioButton1.Checked = True Then
            Dim s4 As String
            s4 = "Ethanol(Corn)"
            Dim cmd3 As New SqlCommand("select * from Barrel_tbl where status='" & s1 & "' and product='" & s4 & "'", conn)
            Dim adapter As New SqlDataAdapter(cmd3)
            Dim table3 As New DataTable()
            adapter.Fill(table3)
            If table3.Rows.Count > 0 Then
                For kl = 0 To z - 1
                    ListBox1.Items.Add(table3.Rows(kl)(1).ToString())
                    s2 = table3.Rows(kl)(1).ToString()
                    Dim query1 As String = "update Barrel_tbl set status=@status where Bid='" & s2 & "' "
                    Dim cmd4 As New SqlCommand(query1, conn)
                    cmd4.Parameters.AddWithValue("@status", s3)
                    cmd4.ExecuteNonQuery()
                Next
            End If
        Else
            Dim s4 As String
            s4 = "Ethanol(sugarcane)"
            Dim cmd3 As New SqlCommand("select * from Barrel_tbl where status='" & s1 & "' and product='" & s4 & "'", conn)
            Dim adapter As New SqlDataAdapter(cmd3)
            Dim table3 As New DataTable()
            adapter.Fill(table3)
            If table3.Rows.Count > 0 Then
                For kl = 0 To z - 1
                    ListBox1.Items.Add(table3.Rows(kl)(1).ToString())
                    s2 = table3.Rows(kl)(1).ToString()
                    Dim query1 As String = "update Barrel_tbl set status=@status where Bid='" & s2 & "' "
                    Dim cmd4 As New SqlCommand(query1, conn)
                    cmd4.Parameters.AddWithValue("@status", s3)
                    cmd4.ExecuteNonQuery()
                Next
            End If
        End If
        conn.Close()
    End Sub

    Private Sub Exec1(query1 As String, j As Integer)
        Throw New NotImplementedException()
    End Sub

    Public Sub Execuryourquery(ByVal query As String)

        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Cdate", DateTimePicker1.Value)
        cmd.Parameters.AddWithValue("@CId", cid_txt.Text)
        cmd.Parameters.AddWithValue("@CName", cname_txt.Text)
        cmd.Parameters.AddWithValue("@caddress", cadd_txt.Text)
        cmd.Parameters.AddWithValue("@mobile", mobile_txt.Text)

        If RadioButton1.Checked Then
            cmd.Parameters.AddWithValue("@Product", RadioButton1.Text)
        Else
            cmd.Parameters.AddWithValue("@Product", RadioButton2.Text)

        End If
        cmd.Parameters.AddWithValue("@no_of_barrel", barrel_txt.Text)
        cmd.Parameters.AddWithValue("@Quantity", quantity_txt.Text)
        cmd.Parameters.AddWithValue("@rate", rate_txt.Text)
        cmd.Parameters.AddWithValue("@gst", gst_txt.Text)
        cmd.Parameters.AddWithValue("@bank", bank_txt.Text)
        cmd.Parameters.AddWithValue("@acc_no", Acc_txt.Text)
        cmd.Parameters.AddWithValue("@ifsc", ifsc_txt.Text)
        cmd.Parameters.AddWithValue("@gst_per", gst_1.Text)
        cmd.Parameters.AddWithValue("@Amount", amt_txt.Text)
        cmd.ExecuteNonQuery()

    End Sub
    Public Sub Exec1(ByVal query As String, ByVal j As Integer, ByVal n As Integer)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, z As Integer
        k = quantity_txt.Text
        z = k + j
        s = n - k
        cmd.Parameters.AddWithValue("@Stock_out", z)
        cmd.Parameters.AddWithValue("@Stock", s)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub Del_btn_Click(sender As Object, e As EventArgs) Handles Del_btn.Click
        'record deleted
        conn.Open()
        Dim s1 As String
        s1 = InputBox("Enter Customer id")
        Dim delquery As String = "delete from customer_tbl where CId='" & s1 & "' "
        Execuryourquery(delquery)
        MessageBox.Show("Record deleted seccessfully")

        'updated record displayed in datagridview
        Dim com As String = "select Cdate,CId,CName,Product,Quantity,Amount from customer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "customer_tbl")
        DataGridView1.DataSource = ds.Tables(0)
        conn.Close()
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        conn.Open()
        Dim s As String
        Dim r As Integer
        Dim z As Integer
        s = InputBox("Enter Customer id")
        Dim searchquery As String = "Select * from customer_tbl where CId='" & s & "' "

        'display data in datagridview
        Dim adpt As New SqlDataAdapter(searchquery, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "customer_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            cid_txt.Text = table.Rows(0)(1).ToString()
            cname_txt.Text = table.Rows(0)(2).ToString()
            cadd_txt.Text = table.Rows(0)(3).ToString()
            mobile_txt.Text = table.Rows(0)(4).ToString()

            If table.Rows(0)(6).ToString() = "Ethanol(Corn)" Then
                RadioButton1.Checked = True
            ElseIf table.Rows(0)(6).ToString() = "Ethanol(sugarcane)" Then
                RadioButton2.Checked = True

            End If
            quantity_txt.Text = table.Rows(0)(7).ToString()
            rate_txt.Text = table.Rows(0)(13).ToString()
            gst_txt.Text = table.Rows(0)(14).ToString()
            gst_1.Text = table.Rows(0)(15).ToString()
            bank_txt.Text = table.Rows(0)(8).ToString()
            Acc_txt.Text = table.Rows(0)(9).ToString()
            ifsc_txt.Text = table.Rows(0)(10).ToString()
            amt_txt.Text = table.Rows(0)(11).ToString()
            barrel_txt.Text = table.Rows(0)(16).ToString()
            z = table.Rows(0)(16).ToString()
            MessageBox.Show("Record found!")
            Del_btn.Enabled = True
            Bill_btn.Enabled = True
        Else
            MessageBox.Show("Sorry record not found")
        End If
        Dim cmd3 As New SqlCommand("select * from Barrel_tbl where status='" & s & "'")
        cmd3.Connection = conn
        Dim adapter As New SqlDataAdapter(cmd3)
        Dim table3 As New DataTable()
        adapter.Fill(table3)
        If table3.Rows.Count > 0 Then
            For r = 0 To z - 1
                ListBox1.Items.Add(table3.Rows(r)(1).ToString())
            Next
        End If
        conn.Close()
    End Sub

    Private Sub Bill_btn_Click(sender As Object, e As EventArgs) Handles Bill_btn.Click
        Customer_bill2.Show()
    End Sub

    Private Sub cname_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cname_txt.KeyPress
        'Should be character
        Select Case Asc(e.KeyChar)
            Case 8
            Case 65 To 90
            Case 97 To 122
            Case 32
            Case Else
                MsgBox("Should be character")
        End Select
    End Sub

    Private Sub mobile_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mobile_txt.KeyPress
        'Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub



    Private Sub barrel_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles barrel_txt.KeyPress
        'Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub Acc_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Acc_txt.KeyPress
        'Should be number
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub ifsc_txt_LostFocus(sender As Object, e As EventArgs) Handles ifsc_txt.LostFocus
        Dim ifsc As Boolean
        ifsc = Regex.IsMatch(ifsc_txt.Text, "[A-Z|a-z]{4}[0-9]{7}$")
        If Not ifsc Then
            MsgBox("Not valid IFSC code")
        End If
    End Sub
End Class