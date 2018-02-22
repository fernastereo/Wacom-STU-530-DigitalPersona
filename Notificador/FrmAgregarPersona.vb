Imports MySql.Data.MySqlClient

Public Class FrmAgregarPersona
    Dim cnnMySql As New MySqlConnection

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If TxtIdentificacion.Text <> "" And TxtNombre.Text <> "" Then
            cnnMySql = New MySqlConnection
            cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
            cnnMySql.Open()

            Dim cmdNot As New MySqlCommand
            cmdNot.Connection = cnnMySql
            cmdNot.Parameters.Clear()
            cmdNot.CommandText = "insert into notificaciones (idexpediente, idsolicitante, nombre, tipo, resolucion, fecharesolucion, notificado) values (?, ?, ?, ?, ?, ?, ?)"
            cmdNot.Parameters.AddWithValue("idexpediente", FrmNotificador.txtIDExpediente.Text)
            cmdNot.Parameters.AddWithValue("idsolicitante", TxtIdentificacion.Text)
            cmdNot.Parameters.AddWithValue("nombre", TxtNombre.Text.ToUpper)
            cmdNot.Parameters.AddWithValue("tipo", dbcTipo.SelectedValue)
            cmdNot.Parameters.AddWithValue("resolucion", FrmNotificador.txtResolucion.Text)
            cmdNot.Parameters.AddWithValue("fecharesolucion", String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FrmNotificador.txtFechaResolucion.Text)))
            cmdNot.Parameters.AddWithValue("notificado", "N")
            cmdNot.ExecuteNonQuery()
            cmdNot.Dispose()
            cnnMySql.Close()
            cnnMySql.Dispose()

            Me.Close()

            If Not FrmNotificador.existeNotificacion Then
                MessageBox.Show("Ocurrió un error al consultar los datos!", "Curaduria1CA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No hay datos para guardar!", "Curaduria1CA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub FrmAgregarPersona_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TraerTipos()
    End Sub

    Private Sub TraerTipos()
        Dim dtaArq As New MySqlDataAdapter
        Dim dttArq As DataTable
        Dim SqlStmt As String

        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()

        SqlStmt = "select id, descripcion from tiponotificado where estado=1"
        dtaArq = New MySqlDataAdapter(SqlStmt, cnnMySql)
        dttArq = New DataTable
        dtaArq.Fill(dttArq)
        With dbcTipo
            .DataSource = dttArq
            .DisplayMember = "descripcion"
            .ValueMember = "id"
        End With

    End Sub
End Class