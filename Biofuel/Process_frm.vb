Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Process_frm
    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub
    Private Sub Process_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = getconnect()

        'set datetimepicker to current date
        DateTimePicker1.Value = DateTime.Today

        'display data from storage table to form
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd As New SqlCommand(searchquery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim table As New DataTable
        da.Fill(table)
        If table.Rows.Count > 0 Then
            rcorn_txt.Text = table.Rows(0)(3).ToString()
            rmaze_txt.Text = table.Rows(1)(3).ToString()
            fethanol_txt.Text = table.Rows(2)(3).ToString()
            fEthanol2_txt.Text = table.Rows(3)(3).ToString()
            ye_txt.Text = table.Rows(4)(3).ToString()
            en_txt.Text = table.Rows(5)(3).ToString()
            sim_txt.Text = table.Rows(6)(3).ToString()
            Total_water.Text = table.Rows(6)(2).ToString()
        End If

        'auto generate process id
        Dim GetCode As String = "0"
        Dim cmd29 As SqlCommand
        cmd29 = New SqlCommand()
        cmd29.Connection = conn
        cmd29.CommandText = "SELECT TOP(1) * FROM Process_tbl ORDER BY PId DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd29.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("PId")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        If (GetCode = "0") Then
            Process_id_txt.Text = "PROC00001"
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)

            Process_id_txt.Text = "PROC" + Format(OldNum + 1, "00000").ToString

        End If

        'textbox enabled false
        smash_txt.Enabled = False
        wwaste_txt.Enabled = False
        water_txt.Enabled = False
        time1_txt.Enabled = False
        heat_txt.Enabled = False
        liqemash_txt.Enabled = False
        liqmash_txt.Enabled = False
        heat2_txt.Enabled = False
        yest_txt.Enabled = False
        co2_txt.Enabled = False
        water2_txt.Enabled = False
        time2_txt.Enabled = False
        fermash_txt.Enabled = False
        rcorn_txt.Enabled = False
        rmaze_txt.Enabled = False
        fethanol_txt.Enabled = False
        fEthanol2_txt.Enabled = False
        femash_txt.Enabled = False
        enzyme_txt.Enabled = False
        heat3_txt.Enabled = False
        wewater_txt.Enabled = False
        swaste_txt.Enabled = False
        time3_txt.Enabled = False
        ethanol_txt.Enabled = False
        quantity_txt.Select()

        'display data in datagridview
        Dim com As String = "select Process_date,Raw_Material,Quantity_Used,Total_Time,Ethanol from Process_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "Process_tbl")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Begin_btn_Click(sender As Object, e As EventArgs) Handles Begin_btn.Click
        'textbox cannot be empty
        If quantity_txt.Text = "" Then
            MsgBox("Quantity can not be empty")
            Exit Sub
        End If

        'validation
        If RadioButton1.Checked = True Then
            Dim o, p As Integer
            o = quantity_txt.Text
            p = rcorn_txt.Text

            If o > p Then
                MsgBox(" Corn out of stock")
                Exit Sub
            End If
        ElseIf RadioButton2.Checked = True Then
            Dim o, p As Integer
            o = quantity_txt.Text
            p = rcorn_txt.Text
            If o > p Then
                MsgBox(" Sugarcane out of stock")
                Exit Sub
            End If
        End If


        Dim x As Double
        Dim y As Integer
        Dim i As Integer
        Dim r As String
        Dim j As Integer
        Dim n As Integer
        Dim ye As String
        Dim en As String
        Dim wa As String
        Dim z As Double
        Dim z1 As Double
        Dim z2 As Double
        Dim z3 As Double
        Dim z4 As Double
        Dim z5 As Double
        Dim w As Integer

        ye = "Yest"
        en = "Enzyme"
        wa = "Water"

        'calculation 
        smash_txt.Text = quantity_txt.Text
        wwaste_txt.Text = (quantity_txt.Text * 0.23)
        time1_txt.Text = 2
        heat_txt.Text = (quantity_txt.Text * 0.3)
        liqmash_txt.Text = (quantity_txt.Text * 2.6)
        water_txt.Text = (quantity_txt.Text * 0.3)
        liqemash_txt.Text = (quantity_txt.Text * 2.6)
        heat2_txt.Text = 72
        co2_txt.Text = (quantity_txt.Text * 31)
        water2_txt.Text = (quantity_txt.Text * 0.61)
        time2_txt.Text = 11
        fermash_txt.Text = (quantity_txt.Text * 2.97)
        yest_txt.Text = (quantity_txt.Text * 0.03)
        femash_txt.Text = (quantity_txt.Text * 2.97)
        heat3_txt.Text = 63
        wewater_txt.Text = quantity_txt.Text * 0.21
        swaste_txt.Text = quantity_txt.Text * 2.44
        time3_txt.Text = 6
        ethanol_txt.Text = quantity_txt.Text * 415.54
        enzyme_txt.Text = quantity_txt.Text * 0.03
        x = (ethanol_txt.Text / 160)

        Dim l, k, v, h, g, f As Double
        l = enzyme_txt.Text
        k = yest_txt.Text
        v = ye_txt.Text
        h = en_txt.Text
        g = Total_water.Text
        f = sim_txt.Text

        If l > h Then
            MsgBox(" Enzyme out of stock")
            Exit Sub
        End If

        If k > v Then
            MsgBox(" Yest out of stock")
            Exit Sub
        End If

        If g > f Then
            MsgBox(" Water out of stock")
            Exit Sub
        End If

        'round of value 
        y = Convert.ToInt32(x.ToString().Substring(0, x.ToString().IndexOf(".")))
        No_barrel_txt.Text = y
        leftover_txt.Text = Math.Round((ethanol_txt.Text Mod 160), 2, MidpointRounding.AwayFromZero)

        TextBox5.Text = (quantity_txt.Text * 0.3) + (quantity_txt.Text * 0.61)

        'auto generate barrel id
        Dim GetCode As String = "0"
        Dim cmd As SqlCommand
        cmd = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP(1) * FROM Barrel_tbl ORDER BY Bid DESC"
        Try
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If (reader.HasRows = True) Then
                While reader.Read()
                    GetCode = (reader.GetString(reader.GetOrdinal("Bid")))
                End While
            End If
            reader.Close()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        Finally
            'conn.Close()
        End Try
        If (GetCode = "0") Then
            For i = 1 To y
                ListBox1.Items.Add("BARL00001")
            Next
        ElseIf (GetCode <> "0") Then
            Dim TotalCodeWithoutLable As String = GetCode.Count - 4
            Dim OldNum As String = GetCode.Substring(GetCode.Length - TotalCodeWithoutLable)
            For i = 1 To y
                ListBox1.Items.Add("BARL" + Format(OldNum + i, "00000").ToString)
            Next
        End If

        'record saved
        Dim insertquery As String = "insert into Process_tbl(Pid,Process_date,Raw_Material,Quantity_Used,Solid_mash,Water1,WasteWater,Time1,Steam,Liquefied_Mash,Heat2,Yest,Co2,Water2,Time2,Fermented_Mash,WasteWater2,Enzyme,Solid_Waste,Heat3,Time3,Ethanol,Total_Time,No_of_Barrel,Leftovers,TWater) values(@Pid,@Process_date,@Raw_Material,@Quantity_Used,@Solid_mash,@Water1,@WasteWater,@Time1,@Steam,@Liquefied_Mash,@Heat2,@Yest,@Co2,@Water2,@Time2,@Fermented_Mash,@WasteWater2,@Enzyme,@Solid_Waste,@Heat3,@Time3,@Ethanol,@Total_Time,@No_of_Barrel,@Leftovers,@TWater)"
        Execuryourquery(insertquery)
        MessageBox.Show("Record inserted successfully")

        'record displayed in datagridview
        Dim com As String = "select Process_date,Raw_Material,Quantity_Used,Total_Time,Ethanol from Process_tbl"
        Dim adpt As New SqlDataAdapter(com, conn)
        Dim ds As New DataSet()
        adpt.Fill(ds, "Process_tbl")
        DataGridView1.DataSource = ds.Tables(0)

        'barrel id and status updated in Barrel_tbl
        Dim sql9 = "insert into Barrel_tbl(Bid,status,product)Values(@Bid,@status,@product)"
        Dim cmd9 As New SqlCommand(sql9, conn)
        Dim name = New SqlParameter("@Bid", SqlDbType.NVarChar)
        Dim name1 = New SqlParameter("@status", SqlDbType.NVarChar)
        Dim name3 = New SqlParameter("@product", SqlDbType.NVarChar)
        cmd9.Parameters.Add(name)
        cmd9.Parameters.Add(name1)
        cmd9.Parameters.Add(name3)
        If RadioButton1.Checked = True Then
            For Each nam As String In ListBox1.Items
                cmd9.Parameters("@Bid").Value = nam
                cmd9.Parameters("@status").Value = "not sold"
                cmd9.Parameters("@product").Value = "Ethanol(Corn)"
                cmd9.ExecuteNonQuery()
            Next
        Else
            For Each nam As String In ListBox1.Items
                cmd9.Parameters("@Bid").Value = nam
                cmd9.Parameters("@status").Value = "not sold"
                cmd9.Parameters("@product").Value = "Ethanol(sugarcane)"
                cmd9.ExecuteNonQuery()
            Next
        End If

        'update in storage_tbl
        If RadioButton1.Checked = True Then

            r = "Ethanol(corn)"
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd3 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd3)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                w = fethanol_txt.Text
                Exec(query1, j, w)
            End If
        Else
            r = "Ethanol(sugarcane)"
            Dim query1 As String = "update storage_tbl set Stock_in=@Stock_in,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_in from storage_tbl where Item='" & r & "' "
            Dim cmd3 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd3)
            Dim table As New DataTable
            da.Fill(table)
            If table.Rows.Count > 0 Then
                j = table.Rows(0)(0).ToString()
                w = fEthanol2_txt.Text
                Exec(query1, j, w)
            End If
        End If

        If RadioButton1.Checked = True Then
            r = RadioButton1.Text
            Dim query1 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_out from storage_tbl where Item='" & r & "' "
            Dim cmd5 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd5)
            Dim table3 As New DataTable
            da.Fill(table3)
            If table3.Rows.Count > 0 Then
                j = table3.Rows(0)(0).ToString()
                n = rcorn_txt.Text
                Exec1(query1, j, n)
            End If
        Else
            r = RadioButton2.Text
            Dim query1 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & r & "' "
            Dim query2 As String = "Select Stock_out from storage_tbl where Item='" & r & "' "
            Dim cmd5 As New SqlCommand(query2, conn)
            Dim da As New SqlDataAdapter(cmd5)
            Dim table3 As New DataTable
            da.Fill(table3)
            If table3.Rows.Count > 0 Then
                j = table3.Rows(0)(0).ToString()
                n = rmaze_txt.Text
                Exec1(query1, j, n)
            End If
        End If

        Dim query10 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & ye & "' "
        Dim query12 As String = "Select Stock_out from storage_tbl where Item='" & ye & "' "
        Dim cmd15 As New SqlCommand(query12, conn)
        Dim da1 As New SqlDataAdapter(cmd15)
        Dim table13 As New DataTable
        da1.Fill(table13)

        If table13.Rows.Count > 0 Then
            z = table13.Rows(0)(0).ToString()
            z1 = ye_txt.Text
            Exec11(query10, z, z1)
        End If

        Dim query11 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & en & "' "
        Dim query13 As String = "Select Stock_out from storage_tbl where Item='" & en & "' "
        Dim cmd14 As New SqlCommand(query13, conn)
        Dim da3 As New SqlDataAdapter(cmd14)
        Dim table14 As New DataTable
        da3.Fill(table14)

        If table14.Rows.Count > 0 Then
            z2 = table14.Rows(0)(0).ToString()
            z3 = en_txt.Text
            Exec2(query11, z2, z3)
        End If

        Dim query19 As String = "update storage_tbl set Stock_out=@Stock_out,Stock=@Stock where Item='" & wa & "' "
        Dim query20 As String = "Select Stock_out from storage_tbl where Item='" & wa & "' "
        Dim cmd22 As New SqlCommand(query20, conn)
        Dim da9 As New SqlDataAdapter(cmd22)
        Dim table9 As New DataTable
        da3.Fill(table9)

        If table9.Rows.Count > 0 Then
            z4 = Total_water.Text
            z5 = sim_txt.Text
            Exec22(query19, z4, z5)
        End If


        'display updated data on Process_form
        Dim searchquery As String = "Select * from storage_tbl "
        Dim cmd4 As New SqlCommand(searchquery, conn)
        Dim da2 As New SqlDataAdapter(cmd4)
        Dim table2 As New DataTable
        da2.Fill(table2)
        If table2.Rows.Count > 0 Then
            rcorn_txt.Text = table2.Rows(0)(3).ToString()
            rmaze_txt.Text = table2.Rows(1)(3).ToString()
            fethanol_txt.Text = table2.Rows(2)(3).ToString()
            fEthanol2_txt.Text = table2.Rows(3)(3).ToString()
            ye_txt.Text = table2.Rows(4)(3).ToString()
            en_txt.Text = table2.Rows(5)(3).ToString()
            sim_txt.Text = table2.Rows(6)(3).ToString()
            Total_water.Text = table2.Rows(6)(2).ToString()
        End If
        conn.Close()
    End Sub

    Private Sub Clear_btn_Click(sender As Object, e As EventArgs) Handles Clear_btn.Click
        'clear all data from textbox and uncheck radiobutton
        quantity_txt.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        smash_txt.Text = ""
        wwaste_txt.Text = ""
        time1_txt.Text = ""
        heat_txt.Text = ""
        liqmash_txt.Text = ""
        water_txt.Text = ""
        liqemash_txt.Text = ""
        heat2_txt.Text = ""
        co2_txt.Text = ""
        water2_txt.Text = ""
        time2_txt.Text = ""
        fermash_txt.Text = ""
        yest_txt.Text = ""
        femash_txt.Text = ""
        heat3_txt.Text = ""
        wewater_txt.Text = ""
        swaste_txt.Text = ""
        time3_txt.Text = ""
        ethanol_txt.Text = ""
        enzyme_txt.Text = ""
        No_barrel_txt.Text = ""
        leftover_txt.Text = ""
        quantity_txt.Select()
        ListBox1.Items.Clear()

    End Sub

    Public Sub Execuryourquery(ByVal query As String)
        'save function
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Process_date", DateTimePicker1.Value)
        If RadioButton1.Checked Then
            cmd.Parameters.AddWithValue("@Raw_Material", RadioButton1.Text)
        Else
            cmd.Parameters.AddWithValue("@Raw_Material", RadioButton2.Text)
        End If
        cmd.Parameters.AddWithValue("@Pid", Process_id_txt.Text)
        cmd.Parameters.AddWithValue("@Quantity_Used", quantity_txt.Text)
        cmd.Parameters.AddWithValue("@Solid_mash", smash_txt.Text)
        cmd.Parameters.AddWithValue("@Water1", water_txt.Text)
        cmd.Parameters.AddWithValue("@WasteWater", wwaste_txt.Text)
        cmd.Parameters.AddWithValue("@Time1", time1_txt.Text)
        cmd.Parameters.AddWithValue("@Steam", heat_txt.Text)
        cmd.Parameters.AddWithValue("@Liquefied_Mash", liqmash_txt.Text)
        cmd.Parameters.AddWithValue("@Heat2", heat2_txt.Text)
        cmd.Parameters.AddWithValue("@Yest", yest_txt.Text)
        cmd.Parameters.AddWithValue("@Co2", co2_txt.Text)
        cmd.Parameters.AddWithValue("@Water2", water2_txt.Text)
        cmd.Parameters.AddWithValue("@Time2", time2_txt.Text)
        cmd.Parameters.AddWithValue("@Fermented_Mash", fermash_txt.Text)
        cmd.Parameters.AddWithValue("@WasteWater2", wewater_txt.Text)
        cmd.Parameters.AddWithValue("@Enzyme", enzyme_txt.Text)
        cmd.Parameters.AddWithValue("@Solid_Waste", swaste_txt.Text)
        cmd.Parameters.AddWithValue("@Heat3", heat3_txt.Text)
        cmd.Parameters.AddWithValue("@Time3", time3_txt.Text)
        cmd.Parameters.AddWithValue("@Ethanol", ethanol_txt.Text)
        cmd.Parameters.AddWithValue("@Total_Time", TextBox4.Text)
        cmd.Parameters.AddWithValue("@No_of_Barrel", No_barrel_txt.Text)
        cmd.Parameters.AddWithValue("@Leftovers", leftover_txt.Text)
        cmd.Parameters.AddWithValue("@TWater", TextBox5.Text)

        cmd.ExecuteNonQuery()
    End Sub

    Public Sub Exec(ByVal query As String, ByVal j As Integer, ByVal w As Integer)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, m As Integer
        k = ethanol_txt.Text
        s = j + k
        m = w + k
        cmd.Parameters.AddWithValue("@Stock_in", s)
        cmd.Parameters.AddWithValue("@Stock", m)
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

    Public Sub Exec11(ByVal query As String, ByVal z As Double, ByVal z1 As Double)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, n As Double
        k = yest_txt.Text
        n = Math.Round((k + z), 2, MidpointRounding.AwayFromZero)
        s = Math.Round((z1 - k), 2, MidpointRounding.AwayFromZero)
        cmd.Parameters.AddWithValue("@Stock_out", n)
        cmd.Parameters.AddWithValue("@Stock", s)
        cmd.ExecuteNonQuery()
    End Sub
    Public Sub Exec2(ByVal query As String, ByVal z2 As Double, ByVal z3 As Double)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, k, n As Double
        k = enzyme_txt.Text
        n = Math.Round((k + z2), 2, MidpointRounding.AwayFromZero)
        s = Math.Round((z3 - k), 2, MidpointRounding.AwayFromZero)
        cmd.Parameters.AddWithValue("@Stock_out", n)
        cmd.Parameters.AddWithValue("@Stock", s)
        cmd.ExecuteNonQuery()
    End Sub

    Public Sub Exec22(ByVal query As String, ByVal z4 As Double, ByVal z5 As Double)
        Dim cmd As New SqlCommand(query, conn)
        Dim s, n, k As Double
        k = "0.91"
        n = Math.Round((k + z4), 2, MidpointRounding.AwayFromZero)
        s = Math.Round((z5 - k), 2, MidpointRounding.AwayFromZero)
        cmd.Parameters.AddWithValue("@Stock_out", n)
        cmd.Parameters.AddWithValue("@Stock", s)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        Proc_rep6.Show()
    End Sub
End Class