Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Paackage_frm
    Private Sub Paackage_frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cryrpt As New ReportDocument
        cryrpt.Load("D:\Project\BioFuel\Biofuel\Package_rep.rpt")
        CrystalReportViewer1.ReportSource = cryrpt
        CrystalReportViewer1.Refresh()
    End Sub
End Class