Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Proc_rep6
    Private Sub Proc_rep6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cryrpt As New ReportDocument
        Dim s As String
        s = InputBox("Enter Process id")
        cryrpt.Load("D:\Project\BioFuel\Biofuel\Process_rep2.rpt")

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