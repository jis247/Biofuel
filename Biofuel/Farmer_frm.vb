Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Farmer_frm
    Private Sub Farmer_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()

        'set datetimepicker to current date
        DateTimePicker1.Value = DateTime.Today

        'textbox and combobox enabled false
        fid_txt.Enabled = False
        fname_txt.Enabled = False
        fadd_txt.Enabled = False
        mobile_txt.Enabled = False
        quantity_txt.Enabled = False

        bank_txt.Enabled = False
        Acc_txt.Enabled = False
        rate_txt.Enabled = False
        gst_txt.Enabled = False
        ifsc_txt.Enabled = False
        amt_txt.Enabled = False
        gst1_txt.Enabled = False

        Del_btn.Enabled = False
        Bill_btn.Enabled = False


        'items added to comobox
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

            TextBox1.Text = table.Rows(0)(3).ToString()
            TextBox2.Text = table.Rows(1)(3).ToString()
        End If

        'record displayed in datagridview
        Dim com As String = "select Fdate,Fid,Fname,Product,Quantity,Amount from farmer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "farmer_tbl")
        DataGridView1.DataSource = ds.Tables(0)
        fid_txt.Select()
    End Sub

    Public Sub Execuryourquery(ByVal query As String)
        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Fdate", DateTimePicker1.Value)
        cmd.Parameters.AddWithValue("@Fid", fid_txt.Text)
        cmd.Parameters.AddWithValue("@Fname", fname_txt.Text)
        cmd.Parameters.AddWithValue("@faddress", fadd_txt.Text)
        cmd.Parameters.AddWithValue("@mobile", mobile_txt.Text)

        If RadioButton1.Checked Then
            cmd.Parameters.AddWithValue("@Product", RadioButton1.Text)
        Else
            cmd.Parameters.AddWithValue("@Product", RadioButton2.Text)
        End If
        cmd.Parameters.AddWithValue("@Quantity", quantity_txt.Text)
        cmd.Parameters.AddWithValue("@rate", rate_txt.Text)
        cmd.Parameters.AddWithValue("@gst", gst_txt.Text)
        cmd.Parameters.AddWithValue("@bank", bank_txt.Text)
        cmd.Parameters.AddWithValue("@acc_no", Acc_txt.Text)
        cmd.Parameters.AddWithValue("@ifsc", ifsc_txt.Text)
        cmd.Parameters.AddWithValue("@Amount", amt_txt.Text)
        cmd.Parameters.AddWithValue("@gst_per", gst1_txt.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub quantity_txt_LostFocus(sender As Object, e As EventArgs) Handles quantity_txt.LostFocus
        'textbox cannot be empty
        If quantity_txt.Text = "" Then
            MsgBox("Quantity can not be empty")
            Exit Sub
        End If

        'display rate gst from rate_tbl
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
                rate_txt.Text = table.Rows(0)(2).ToString()
                gst1_txt.Text = table.Rows(0)(9).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(2).ToString() * table.Rows(0)(9).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(2).ToString() + gst_txt.Text
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
                rate_txt.Text = table.Rows(0)(1).ToString()
                gst1_txt.Text = table.Rows(0)(8).ToString()
                gst_txt.Text = quantity_txt.Text * table.Rows(0)(1).ToString() * table.Rows(0)(8).ToString() / 100
                amt_txt.Text = quantity_txt.Text * table.Rows(0)(1).ToString() + gst_txt.Text
            Else
                MessageBox.Show("Current rate not found.Plz enter Current rate")
                Rate_frm.Show()
            End If
        End If
    End Sub

    Private Sub Add_btn_Click(sender As Object, e As EventArgs) Handles Add_btn.Click
        'auto generate farmer id
        Dim GetCode As String = "0"
        Dim cmd As SqlCommand
        cmd = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP(1) * FROM farmer_tbl ORDER BY fid DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("fid")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        If (GetCode = "0") Then
            fid_txt.Text = "FARM00001"
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)
            fid_txt.Text = "FARM" + Format(OldNum + 1, "00000").ToString
        End If

        'textbox enabled true
        fname_txt.Enabled = True
        fadd_txt.Enabled = True
        mobile_txt.Enabled = True
        quantity_txt.Enabled = True

        bank_txt.Enabled = True
        Acc_txt.Enabled = True
        ifsc_txt.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = False

        Del_btn.Enabled = False
        Bill_btn.Enabled = False


        'clear textbox........
        bank_txt.Text = ""
        fname_txt.Clear()
        fadd_txt.Clear()
        rate_txt.Clear()
        gst_txt.Clear()
        mobile_txt.Clear()
        quantity_txt.Clear()

        Acc_txt.Clear()
        ifsc_txt.Clear()
        amt_txt.Clear()
        gst1_txt.Clear()

        'record displayed in datagridview
        Dim com As String = "select Fdate,Fid,Fname,Product,Quantity,Amount from farmer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "farmer_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        conn.Close()
        fname_txt.Select()
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub

    Private Sub Save_btn_Click(sender As Object, e As EventArgs) Handles Save_btn.Click
        'textbox cannot be empty
        If fname_txt.Text = "" Then
            MsgBox("Cannot save if name is empty")
            fname_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty
        If fadd_txt.Text = "" Then
            MsgBox("Cannot save if address is empty")
            fadd_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and less than 10 digit
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
        If quantity_txt.Text = "" Then
            MsgBox("Cannot save if quantity is empty")
            quantity_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and less than 16 digit
        If Acc_txt.Text = "" Then
            MsgBox("Cannot save if account number is empty")
            Acc_txt.Select()
            Exit Sub
        ElseIf Len(Acc_txt.Text) < 16 Then
            MsgBox("Account no. cannot be less than 16 digit")
            Acc_txt.Select()
            Exit Sub
        End If

        'textbox cannot be empty and less than 11 character
        If ifsc_txt.Text = "" Then
            MsgBox("Cannot save if IFSC code is empty")
            ifsc_txt.Select()
            Exit Sub
        ElseIf Len(ifsc_txt.Text) < 11 Then
            MsgBox("IFSC cannot be less than 11 characters")
            ifsc_txt.Select()
            Exit Sub
        End If


        'record saved 
        Dim r As String
        Dim j As Double
        Dim n As Double
        Dim insertquery As String = "insert into farmer_tbl(Fid,Fname,faddress,mobile,Product,Quantity,bank,acc_no,ifsc,Amount,Fdate,rate,gst,gst_per) values(@Fid,@Fname,@faddress,@mobile,@Product,@Quantity,@bank,@acc_no,@ifsc,@Amount,@Fdate,@gst,@rate,@gst_per)"
        Execuryourquery(insertquery)

        'record updated in stock
        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in,Stock from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox1.Text
                Exec(query1, j, n)
            End If
        Else
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
        End If
        MessageBox.Show("Record inserted successfully")

        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd1 As New SqlCommand(searchquery, conn)
        Dim da1 As New SqlDataAdapter(cmd1)
        Dim table1 As New DataTable
        da1.Fill(table1)
        If table1.Rows.Count > 0 Then

            TextBox1.Text = table1.Rows(0)(3).ToString()
            TextBox2.Text = table1.Rows(1)(3).ToString()
        End If

        'record displayed in datagridview
        Dim com As String = "select Fdate,Fid,Fname,Product,Quantity,Amount from farmer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "farmer_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Del_btn_Click(sender As Object, e As EventArgs) Handles Del_btn.Click

        Dim r As String
        Dim j As Double
        Dim n As Double
        Dim s1 As String

        s1 = fid_txt.Text
        Dim delquery As String = "delete from farmer_tbl where Fid='" & s1 & "' "
        Execuryourquery(delquery)
        MessageBox.Show("Record deleted seccessfully")

        'record updated in stock
        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in,Stock from storage_tbl where Item='" & r & "' "
            Dim cmd As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                n = TextBox1.Text
                Exec1(query1, j, n)
            End If
        Else
            r = RadioButton2.Text
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock,Stock=@Stock_in where Item='" & r & "' "
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
        End If

        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd1 As New SqlCommand(searchquery, conn)
        Dim da1 As New SqlDataAdapter(cmd1)
        Dim table1 As New DataTable
        da1.Fill(table1)
        If table1.Rows.Count > 0 Then

            TextBox1.Text = table1.Rows(0)(3).ToString()
            TextBox2.Text = table1.Rows(1)(3).ToString()
        End If

        Dim com As String = "select Fdate,Fid,Fname,Product,Quantity,Amount from farmer_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "farmer_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        Dim s As String
        s = InputBox("Enter farmer id")
        Dim searchquery As String = "Select * from farmer_tbl where Fid='" & s & "' "
        Dim adpt As New SqlDataAdapter(searchquery, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "farmer_tbl")
        DataGridView1.DataSource = ds.Tables(0)
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            fid_txt.Text = table.Rows(0)(1).ToString()
            fname_txt.Text = table.Rows(0)(2).ToString()
            fadd_txt.Text = table.Rows(0)(3).ToString()
            mobile_txt.Text = table.Rows(0)(4).ToString()

            If table.Rows(0)(6).ToString() = "Corn" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
            quantity_txt.Text = table.Rows(0)(7).ToString()
            rate_txt.Text = table.Rows(0)(13).ToString()
            gst_txt.Text = table.Rows(0)(14).ToString()
            gst1_txt.Text = table.Rows(0)(15).ToString()
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
        Farmer_bill.Show()
    End Sub
    Private Sub fname_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles fname_txt.KeyPress
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
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub



    Private Sub Acc_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Acc_txt.KeyPress
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