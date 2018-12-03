Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Farmer_bill
    Private Sub Farmer_bill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cryrpt As New ReportDocument
        Dim s As String
        's = InputBox("enter farmer id")
        s = Farmer_frm.fid_txt.Text
        cryrpt.Load("D:\Project\BioFuel\Biofuel\Farmer_bill_rep.rpt")

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