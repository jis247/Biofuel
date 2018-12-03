Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Supplier_bill
    Private Sub Supplier_bill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cryrpt As New ReportDocument
        Dim s As String

        's = InputBox("Enter Supplier id")
        s = Supplier_frm.sid_txt.Text
        cryrpt.Load("D:\Project\BioFuel\Biofuel\Supplier_bill_rep.rpt")

        Dim pfield1 As New ParameterField
        Dim pdiscrete1 As New ParameterDiscreteValue
        Dim pfields As New ParameterFields

        pfield1.Name = "id"
        pdiscrete1.Value = s
        pfield1.CurrentValues.Add(pdiscrete1)
        pfields.Add(pfield1)

        CrystalReportViewer1.ReportSource = cryrpt
        CrystalReportViewer1.ParameterFieldInfo = pfields
        CrystalReportViewer1.Refresh()

    End Sub
End Class