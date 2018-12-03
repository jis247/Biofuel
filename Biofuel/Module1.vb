Imports System.Data
Imports System.Data.SqlClient
Module Module1
    Public conn As SqlConnection
    Public Function getconnect()
        'i have used visual studio 2017 for front end
        'and Sql Server 2017 as Back end
        'instead of inbuild report i have used SAP crystal report 21
        conn = New SqlConnection("Data Source=DESKTOP-13DN2FT\SQLEXPRESS;Initial Catalog=Database1;Integrated Security=True")
        Return conn
    End Function
End Module
