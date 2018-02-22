Imports System.IO
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Module MdlDeclaraciones
    Public cRutaAccess As String
    Public cServidor As String
    Public cUsuario As String
    Public cPassword As String
    Public cBaseDatos As String

    Public Sub RutaDB()
        Dim fic As String
        fic = System.AppDomain.CurrentDomain.BaseDirectory & "\Config.dat"
        Dim Texto As String = ""
        Dim i As Integer = 1
        Dim sr As New System.IO.StreamReader(fic)

        While Not sr.EndOfStream
            Texto = sr.ReadLine
            Select Case i
                Case 1 : cRutaAccess = Texto
                Case 2 : cServidor = Texto
                Case 3 : cUsuario = Texto
                Case 4 : cPassword = Texto
                Case 5 : cBaseDatos = Texto
            End Select
            i = i + 1
        End While
        sr.Close()
    End Sub

    Public Function OpenAccess(ByVal BD As String) As OleDbConnection

        Dim Conexion As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & BD & ";Persist Security Info=True;Jet OLEDB:Database Password=03310321")
        Return Conexion

    End Function

    Public Function openMySql(ByVal Servidor As String, ByVal Usuario As String, ByVal Password As String, ByVal basedatos As String) As MySqlConnection

        Dim builderConex = New MySqlConnectionStringBuilder
        With builderConex
            .Server = Servidor
            .UserID = Usuario
            .Password = Password
            .Database = basedatos
        End With
        Dim Conexion As New MySqlConnection(builderConex.ToString)
        Return Conexion
    End Function

End Module
