Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Process_rep1
    Private Sub Report_btn_Click(sender As Object, e As EventArgs) Handles Report_btn.Click
        '2 parameters and 1 sql query passed in crystal report
        Dim cryrpt As New ReportDocument
        Dim firstdate As Date
        Dim Seconddate As Date
        firstdate = DateTimePicker1.Value
        Seconddate = DateTimePicker2.Value
        cryrpt.Load("D:\Project\BioFuel\Biofuel\Process_rep.rpt")

        Dim pfield As New ParameterField
        Dim pfields As New ParameterFields
        Dim pdiscrete As New ParameterDiscreteValue

        Dim pfield1 As New ParameterField
        Dim pdiscrete1 As New ParameterDiscreteValue

        pfield.Name = "start_date"
        pdiscrete.Value = firstdate
        pfield.CurrentValues.Add(pdiscrete)
        pfields.Add(pfield)

        pfield1.Name = "end_date"
        pdiscrete1.Value = Seconddate
        pfield1.CurrentValues.Add(pdiscrete1)
        pfields.Add(pfield1)

        CrystalReportViewer1.ReportSource = cryrpt
        CrystalReportViewer1.ParameterFieldInfo = pfields
        CrystalReportViewer1.Refresh()
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Me.Close()
    End Sub

    Private Sub Process_rep1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Today
    End Sub
End Class