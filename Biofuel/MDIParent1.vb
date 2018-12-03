Imports System.Windows.Forms

Public Class MDIParent1
    Private Sub FarmerDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FarmerDetailsToolStripMenuItem.Click
        Farmer_frm.Show()
    End Sub

    Private Sub SupplierDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierDetailsToolStripMenuItem.Click
        Supplier_frm.Show()
    End Sub

    Private Sub CurrentRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrentRateToolStripMenuItem.Click
        Rate_frm.Show()
    End Sub

    Private Sub StockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockToolStripMenuItem.Click
        Storage_frm.Show()
    End Sub

    Private Sub ProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcessToolStripMenuItem.Click
        Process_frm.Show()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        Customer_frm.Show()
    End Sub

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Dim ans
        ans = MsgBox("Do you really want to terminate the system", vbQuestion + vbYesNo)
        If ans = vbYes Then
            End
        Else
            Exit Sub
        End If

    End Sub

    Private Sub PurchaseReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseReportToolStripMenuItem.Click
        Purchase_rept.Show()
    End Sub

    Private Sub SalesReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesReportToolStripMenuItem.Click
        Sales_rept.Show()
    End Sub

    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        Worker_frm.Show()
    End Sub

    Private Sub ProcessReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcessReportToolStripMenuItem.Click
        Process_rep1.Show()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Purchase_rept.Show()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Sales_rept.Show()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Process_rep1.Show()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Rate_frm.Show()
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        Process_frm.Show()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Storage_frm.Show()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Worker_frm.Show()
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        Supplier_frm.Show()
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Farmer_frm.Show()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Customer_frm.Show()
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Process_rep1.Show()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Rate_frm.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        'GroupBox1.Visible = False
        GroupBox2.Visible = True
        GroupBox3.Visible = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        GroupBox3.Visible = True
        GroupBox2.Visible = False
        GroupBox3.Visible = True
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        'GroupBox1.Visible = False
        'GroupBox2.Visible = False
        GroupBox3.Visible = True
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        GroupBox1.Visible = True
        GroupBox2.Visible = False
        GroupBox3.Visible = False
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        GroupBox1.Visible = True
        GroupBox2.Visible = False
        GroupBox3.Visible = False
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        'GroupBox1.Visible = False
        GroupBox2.Visible = True
        GroupBox3.Visible = False
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Farmer_frm.Show()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Customer_frm.Show()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Process_frm.Show()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Storage_frm.Show()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Supplier_frm.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Worker_frm.Show()
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        Sales_rept.Show()
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        Purchase_rept.Show()
    End Sub

    Private Sub PackageReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackageReportToolStripMenuItem.Click
        Package.Show()
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        Package.Show()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Package.Show()
    End Sub
End Class
