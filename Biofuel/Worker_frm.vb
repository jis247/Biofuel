Imports System.Data.SqlClient
Public Class Worker_frm
    Private Sub Worker_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()

        'following textbox enabled false
        eid_txt.Enabled = False
        name_txt.Enabled = False
        address_txt.Enabled = False
        mobile_txt.Enabled = False
        DateTimePicker1.Enabled = False
        DateTimePicker2.Enabled = False
        desi_cmb.Enabled = False
        bsal_txt.Enabled = False
        bonus_txt.Enabled = False
        da_txt.Enabled = False
        hra_txt.Enabled = False
        ta_txt.Enabled = False
        proftax_txt.Enabled = False
        pf_txt.Enabled = False
        netsal_txt.Enabled = False
        name_txt.Select()

        'display data in combobox
        desi_cmb.Items.Add("Manager")
        desi_cmb.Items.Add("Engineer")
        desi_cmb.Items.Add("Worker")
        desi_cmb.Items.Add("Accountant")

        'display data in datagridview
        Dim com As String = "select eid,Name,Designation,DOB,Mobile_no,Bonus,Net_Salary from emp_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "emp_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub exit_btn_Click(sender As Object, e As EventArgs) Handles exit_btn.Click
        Me.Close()
    End Sub

    Private Sub add_btn_Click(sender As Object, e As EventArgs) Handles add_btn.Click
        Dim GetCode As String = "0"
        Dim cmd As SqlCommand
        cmd = New SqlCommand()
        cmd.Connection = conn

        'auto generate emp id
        cmd.CommandText = "SELECT TOP(1) * FROM emp_tbl ORDER BY eid DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("eid")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        If (GetCode = "0") Then
            eid_txt.Text = "EMPP00001"
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)
            eid_txt.Text = "EMPP" + Format(OldNum + 1, "00000").ToString
        End If

        'enabled true
        name_txt.Enabled = True
        address_txt.Enabled = True
        mobile_txt.Enabled = True
        DateTimePicker1.Enabled = True
        DateTimePicker2.Enabled = True
        desi_cmb.Enabled = True
        bsal_txt.Enabled = True
        bonus_txt.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = False

        'clear textbox and combobox
        desi_cmb.Text = ""
        name_txt.Clear()
        address_txt.Clear()
        mobile_txt.Clear()
        bsal_txt.Clear()
        bonus_txt.Clear()
        da_txt.Clear()
        hra_txt.Clear()
        ta_txt.Clear()
        proftax_txt.Clear()
        pf_txt.Clear()
        netsal_txt.Clear()
        name_txt.Select()
    End Sub

    Private Sub save_btn_Click(sender As Object, e As EventArgs) Handles save_btn.Click

        'Cannot save if name is empty
        If name_txt.Text = "" Then
            MsgBox("Cannot save if name is empty")
            name_txt.Select()
            Exit Sub
        End If

        'Cannot save if address is empty
        If address_txt.Text = "" Then
            MsgBox("Cannot save if address is empty")
            address_txt.Select()
            Exit Sub
        End If

        'Cannot save if Mobile number is empty
        If mobile_txt.Text = "" Then
            MsgBox("Cannot save if Mobile number is empty")
            mobile_txt.Select()
            Exit Sub
        End If

        'Cannot save if Basic Salary is empty
        If bsal_txt.Text = "" Then
            MsgBox("Cannot save if Basic Salary is empty")
            bsal_txt.Select()
            Exit Sub
        End If

        'Cannot save if Bonus is empty
        If bonus_txt.Text = "" Then
            MsgBox("Cannot save if Bonus is empty")
            bonus_txt.Select()
            Exit Sub
        End If

        'save 
        Dim insertquery As String = "insert into emp_tbl(eid,Name,Address,Mobile_no,DOB,Gender,Date_Hired,Designation,Basic_Salary,Bonus,DA,HRA,TA,Prof_tax,PF,Net_Salary) values(@eid,@Name,@Address,@Mobile_no,@DOB,@Gender,@Date_Hired,@Designation,@Basic_Salary,@Bonus,@DA,@HRA,@TA,@Prof_tax,@PF,@Net_Salary)"
        execuryourquery(insertquery)
        MessageBox.Show("Record inserted successfully")

        'display in datagridview
        Dim com As String = "select eid,Name,Designation,DOB,Mobile_no,Bonus,Net_Salary from emp_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "emp_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Public Sub execuryourquery(ByVal query As String)

        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@eid", eid_txt.Text)
        cmd.Parameters.AddWithValue("@Name", name_txt.Text)
        cmd.Parameters.AddWithValue("@Address", address_txt.Text)
        cmd.Parameters.AddWithValue("@Mobile_no", mobile_txt.Text)
        cmd.Parameters.AddWithValue("@DOB", DateTimePicker1.Value.Date.ToShortDateString)
        If RadioButton1.Checked Then
            cmd.Parameters.AddWithValue("@Gender", RadioButton1.Text)
        Else
            cmd.Parameters.AddWithValue("@Gender", RadioButton2.Text)
        End If
        cmd.Parameters.AddWithValue("@Date_Hired", DateTimePicker2.Value.Date.ToShortDateString)
        cmd.Parameters.AddWithValue("@Designation", desi_cmb.Text)
        cmd.Parameters.AddWithValue("@Basic_Salary", bsal_txt.Text)
        cmd.Parameters.AddWithValue("@Bonus", bonus_txt.Text)
        cmd.Parameters.AddWithValue("@DA", da_txt.Text)
        cmd.Parameters.AddWithValue("@HRA", hra_txt.Text)
        cmd.Parameters.AddWithValue("@TA", ta_txt.Text)
        cmd.Parameters.AddWithValue("@Prof_tax", proftax_txt.Text)
        cmd.Parameters.AddWithValue("@PF", pf_txt.Text)
        cmd.Parameters.AddWithValue("@Net_Salary", netsal_txt.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub delete_btn_Click(sender As Object, e As EventArgs) Handles delete_btn.Click

        'delete query
        Dim s1 As String
        s1 = InputBox("enter Employee id")
        Dim delquery As String = "delete from emp_tbl where eid='" & s1 & "' "
        execuryourquery(delquery)
        MessageBox.Show("Record deleted seccessfully")

        'display data in datagridview
        Dim com As String = "select eid,Name,Designation,DOB,Mobile_no,Bonus,Net_Salary from emp_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "emp_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub search_btn_Click(sender As Object, e As EventArgs) Handles search_btn.Click

        'search
        Dim s As String
        s = InputBox("enter Employee id")
        Dim searchquery As String = "Select * from emp_tbl where eid='" & s & "' "
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            eid_txt.Text = table.Rows(0)(0).ToString()
            name_txt.Text = table.Rows(0)(1).ToString()
            address_txt.Text = table.Rows(0)(2).ToString()
            mobile_txt.Text = table.Rows(0)(3).ToString()
            DateTimePicker1.Text = table.Rows(0)(4).ToString
            If table.Rows(0)(5).ToString() = "Male" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
            DateTimePicker2.Text = table.Rows(0)(6).ToString()
            desi_cmb.Text = table.Rows(0)(7).ToString()
            bsal_txt.Text = table.Rows(0)(8).ToString()
            bonus_txt.Text = table.Rows(0)(9).ToString()
            da_txt.Text = table.Rows(0)(10).ToString()
            hra_txt.Text = table.Rows(0)(11).ToString()
            ta_txt.Text = table.Rows(0)(12).ToString()
            proftax_txt.Text = table.Rows(0)(13).ToString()
            pf_txt.Text = table.Rows(0)(14).ToString()
            netsal_txt.Text = table.Rows(0)(15).ToString()
            MessageBox.Show("Record found!")
        Else
            MessageBox.Show("Sorry record not found")
        End If
    End Sub

    Private Sub bsal_txt_LostFocus(sender As Object, e As EventArgs) Handles bsal_txt.LostFocus

        'calculate......da,hra.....
        Dim bs, da, hra, pf, ta, pt, gs, ns As Double
        bs = bsal_txt.Text
        da = bs * 41 / 100
        da_txt.Text = da
        hra = bs * 30 / 100
        hra_txt.Text = hra
        pf = bs * 12 / 100
        pf_txt.Text = pf
        ta = bs * 20 / 100
        ta_txt.Text = ta
        gs = bs + da + hra + ta
        pt = gs * 14 / 100
        proftax_txt.Text = pt
        ns = gs - pf - pt
        netsal_txt.Text = ns
    End Sub

    Private Sub bonus_txt_LostFocus(sender As Object, e As EventArgs) Handles bonus_txt.LostFocus

        'bonus
        Dim bs, da, hra, pf, ta, pt, gs, ns, b As Double
        bs = bsal_txt.Text
        da = bs * 41 / 100
        hra = bs * 30 / 100
        pf = bs * 12 / 100
        ta = bs * 20 / 100
        gs = bs + da + hra + ta
        pt = gs * 14 / 100
        ns = gs - pf - pt
        b = bonus_txt.Text
        netsal_txt.Text = ns + b
    End Sub

    Private Sub name_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles name_txt.KeyPress
        'name cannot be number on keypress event
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
        'mobile cannot be character on keypress event
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub bsal_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles bsal_txt.KeyPress
        'salary cannot be character on keypress event
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

    Private Sub bonus_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles bonus_txt.KeyPress

        'bonus cannot be character on keypress event
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 8
            Case Else
                MsgBox("Should be number")
        End Select
    End Sub

End Class