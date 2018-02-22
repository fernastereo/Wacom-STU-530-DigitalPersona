Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class FrmReporte
    Dim cnnMySql As MySqlConnection

    Private Sub FrmReporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dsDatos As DataSet1 = getData(FrmNotificador.LblID.Text)
        'Dim nRow As DataRow
        'nRow = dsDatos.Tables("datos").Rows(0)
        'MessageBox.Show(nRow("nombre"))

        Dim dataSource As ReportDataSource = New ReportDataSource("DataSet1", dsDatos.Tables("datos"))

        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(dataSource)
        ReportViewer1.DocumentMapCollapsed = True
        ReportViewer1.RefreshReport()
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Function getData(ByVal id As Double) As DataSet1
        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()

        Dim dta As New MySqlDataAdapter
        Dim cmd As New MySqlCommand("select n.idsolicitante, n.nombre, concat(UCASE(substr(t.descripcion,1,1)), LCASE(substr(t.descripcion,2))) as tipo, n.resolucion, n.fecharesolucion, n.fechanotificado, n.huellaimg, n.firma, n.codseguridad, n.qrcode from notificaciones n, tiponotificado t where t.id=n.tipo and n.id=@id")
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Connection = cnnMySql
        dta.SelectCommand = cmd
        Dim dsn As DataSet1 = New DataSet1
        dta.Fill(dsn, "datos")
        Return dsn
    End Function

End Class