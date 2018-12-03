Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class Purchase_rept
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '2 parameters And 1 query passed in crystal report
        Dim cryrpt As New ReportDocument
        Dim firstdate As Date
        Dim Seconddate As Date
        firstdate = DateTimePicker1.Value
        Seconddate = DateTimePicker2.Value
        cryrpt.Load("D:\Project\BioFuel\Biofuel\temp.rpt")

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

    Private Sub Purchase_rept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Today

    End Sub
End Class