Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class FrmNotificador
    Dim cnnMySql As New MySqlConnection

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim cnnMySql As MySqlConnection

        If Not txtRadicado.Text Is Nothing Then
            Dim rstExp As OleDbDataReader
            Dim cmdExp As New OleDbCommand

            Dim cnnAccess As New OleDbConnection
            cnnAccess = OpenAccess(cRutaAccess)
            cnnAccess.Open()

            cmdExp.CommandText = "select e.idexpediente, e.fecharad, m.nombre as modalidad, l.nombre as licencia, r.resolucion, r.fechalicencia from expediente e, tipoproyecto m, tipolicencia l, licencia r where e.idexpediente=r.idexpediente and e.tipoproyecto=m.idtipo and m.idtipolic=l.idtipolic and e.idradicacion=? and e.vigencia=?"
            cmdExp.Parameters.AddWithValue("idradicacion", txtRadicado.Text)
            cmdExp.Parameters.AddWithValue("vigencia", cboVigencia.Text)
            cmdExp.Connection = cnnAccess
            rstExp = cmdExp.ExecuteReader
            If rstExp.Read Then
                txtExpediente.Text = "13001-1-" & Mid(cboVigencia.Text, 3, 2) & "-" & Format(CDbl(txtRadicado.Text), "0000")
                txtFechaRad.Text = rstExp!fecharad
                txtModalidad.Text = rstExp!modalidad
                txtLicencia.Text = rstExp!licencia
                txtResolucion.Text = rstExp!resolucion
                txtFechaResolucion.Text = rstExp!fechalicencia
                txtIDExpediente.Text = rstExp!idexpediente
                txtSolcitante.Text = nombreSolicitante(rstExp!idexpediente)
            End If
            rstExp.Close()
            cmdExp.Dispose()

            'Consultar de mysql los solicitantes y vecinos
            If Not existeNotificacion() Then
                'Consultar de access los solicitantes y vecinos, si no estan en la tabla de mysql
                'y guardarlos en la tabla de notificaciones en mysql
                cmdExp.Parameters.Clear()
                cmdExp.CommandText = "select s.identificacion, s.nombre from solicitante s, expesol e where e.idsolicitante=s.identificacion and e.idexpediente=?"
                cmdExp.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
                cmdExp.Connection = cnnAccess
                rstExp = cmdExp.ExecuteReader
                If rstExp.HasRows Then
                    Dim cmdNot As New MySqlCommand

                    cnnMySql = New MySqlConnection
                    cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
                    cnnMySql.Open()

                    cmdNot.Connection = cnnMySql
                    While rstExp.Read
                        cmdNot.Parameters.Clear()
                        cmdNot.CommandText = "insert into notificaciones (idexpediente, idsolicitante, nombre, tipo, resolucion, fecharesolucion, notificado) values (?, ?, ?, ?, ?, ?, ?)"
                        cmdNot.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
                        cmdNot.Parameters.AddWithValue("idsolicitante", rstExp!identificacion)
                        cmdNot.Parameters.AddWithValue("nombre", rstExp!nombre)
                        cmdNot.Parameters.AddWithValue("tipo", "1")
                        cmdNot.Parameters.AddWithValue("resolucion", txtResolucion.Text)
                        cmdNot.Parameters.AddWithValue("fecharesolucion", String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtFechaResolucion.Text)))
                        cmdNot.Parameters.AddWithValue("notificado", "N")
                        cmdNot.ExecuteNonQuery()
                    End While
                    cmdNot.Dispose()
                    cnnMySql.Close()
                    cnnMySql.Dispose()
                End If
                rstExp.Close()
                cmdExp.Dispose()

                'Consultar vecinos colindantes
                cmdExp.Parameters.Clear()
                cmdExp.CommandText = "select v.idvecino, v.nombrevec from vecino v where v.idexpediente=?"
                cmdExp.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
                cmdExp.Connection = cnnAccess
                rstExp = cmdExp.ExecuteReader
                If rstExp.HasRows Then
                    Dim cmdNot As New MySqlCommand

                    cnnMySql = New MySqlConnection
                    cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
                    cnnMySql.Open()

                    cmdNot.Connection = cnnMySql
                    While rstExp.Read
                        cmdNot.Parameters.Clear()
                        cmdNot.CommandText = "insert into notificaciones (idexpediente, idsolicitante, nombre, tipo, resolucion, fecharesolucion, notificado) values (?, ?, ?, ?, ?, ?, ?)"
                        cmdNot.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
                        cmdNot.Parameters.AddWithValue("idsolicitante", rstExp!idvecino)
                        cmdNot.Parameters.AddWithValue("nombre", rstExp!nombrevec)
                        cmdNot.Parameters.AddWithValue("tipo", "3")
                        cmdNot.Parameters.AddWithValue("resolucion", txtResolucion.Text)
                        cmdNot.Parameters.AddWithValue("fecharesolucion", String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtFechaResolucion.Text)))
                        cmdNot.Parameters.AddWithValue("notificado", "N")
                        cmdNot.ExecuteNonQuery()
                    End While
                    cmdNot.Dispose()
                    cnnMySql.Close()
                    cnnMySql.Dispose()
                End If
                rstExp.Close()
                cmdExp.Dispose()


                cnnAccess.Close()
                cnnAccess.Dispose()

                If Not existeNotificacion() Then
                    MessageBox.Show("Ocurrió un error al consultar los datos!", "Curaduria1CA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End If
    End Sub

    Public Function existeNotificacion() As Boolean
        Dim cmdDat As New MySqlCommand
        Dim rstDat As MySqlDataReader

        existeNotificacion = False
        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()
        cmdDat.CommandText = "select * from notificaciones where idexpediente=?"
        cmdDat.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
        cmdDat.Connection = cnnMySql
        rstDat = cmdDat.ExecuteReader
        If rstDat.HasRows Then
            'LLENAR EL GRID CON LOS DATOS
            With dtgPersonas
                .RowCount = 1
                .ColumnCount = 5
                .Columns(0).Name = "Id"
                .Columns(1).Name = "Identificacion"
                .Columns(2).Name = "Nombre"
                .Columns(3).Name = "Tipo"
                .Columns(4).Name = "Notificado"
                Dim row As String()
                While rstDat.Read
                    row = New String() {rstDat!id, rstDat!idsolicitante, rstDat!nombre, rstDat!tipo, rstDat!notificado}
                    .Rows.Add(row)
                End While
                .Columns(0).Width = 0
                .Columns(1).Width = 80
                .Columns(2).Width = 250
                .Columns(3).Width = 0
                .Columns(4).Width = 0
            End With
            btnAgregar.Enabled = True
            Return True
        End If

    End Function

    Private Function nombreSolicitante(ByVal IDExpediente As Double) As String
        Dim cmdSol As New OleDbCommand
        Dim rstSol As OleDbDataReader

        Dim cnnAccess As New OleDbConnection
        cnnAccess = OpenAccess(cRutaAccess)
        cnnAccess.Open()

        cmdSol.CommandText = "select s.nombre from expesol e, solicitante s where e.idsolicitante=s.identificacion and e.idexpediente=?"
        cmdSol.Parameters.AddWithValue("idexpediente", IDExpediente)
        cmdSol.Connection = cnnAccess
        rstSol = cmdSol.ExecuteReader
        If rstSol.Read Then
            Return rstSol!nombre
        Else
            Return ""
        End If
        rstSol.Close()
        cmdSol.Dispose()
        cnnAccess.Close()
        cnnAccess.Dispose()
    End Function

    Private Sub FrmNotificador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Double

        RutaDB()
        RutaAccess.Text = cRutaAccess
        Servidor.Text = cServidor
        Usuario.Text = cUsuario
        Password.Text = cPassword
        BaseDatos.Text = cBaseDatos
        For i = Year(Date.Now) To 2008 Step -1
            cboVigencia.Items.Add(i)
        Next

        btnFirma.Enabled = False
        btnGuardar.Enabled = False
        btnFirma.Enabled = False
        btnImprimir.Enabled = False
        btnAgregar.Enabled = False
    End Sub

    Private Sub txtRadicado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRadicado.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub dtgPersonas_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgPersonas.CellClick
        Dim i As Integer = e.RowIndex

        LblFila.Text = i
        LblID.Text = dtgPersonas.Item(0, i).Value
        TxtIdentificacion.Text = dtgPersonas.Item(1, i).Value
        TxtNombre.Text = dtgPersonas.Item(2, i).Value
        Dim notificado As String = dtgPersonas.Item(4, i).Value
        If notificado = "S" Then
            lblNotificado.Text = "NOTIFICADO"
            lblNotificado.ForeColor = Color.Blue
            btnImprimir.Enabled = True
            btnGuardar.Enabled = True
        Else
            lblNotificado.Text = "SIN NOTIFICAR"
            lblNotificado.ForeColor = Color.Red
            btnImprimir.Enabled = False
            btnGuardar.Enabled = False
        End If
        PicHuella.Image = getHuella(LblID.Text)
        picFirma.Image = getFirma(LblID.Text)
        btnFirma.Enabled = True
    End Sub

    Private Function getFirma(ByVal id As Double) As Image
        Try
            Dim con As New MySqlConnection
            con = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
            Dim cmd As New MySqlCommand("Select firma From notificaciones where id=@id", con)
            cmd.Parameters.AddWithValue("@id", id)
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds, "notificaciones")
            Dim c As Integer = ds.Tables("notificaciones").Rows.Count
            If c > 0 Then
                Dim bytBLOBData() As Byte = ds.Tables("notificaciones").Rows(c - 1)("firma")
                Dim stmBLOBData As New MemoryStream(bytBLOBData)
                Return Image.FromStream(stmBLOBData)
            Else
                Return Nothing
            End If
            ds.Dispose()
            da.Dispose()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function getHuella(ByVal id As Double) As Image
        Try
            Dim con As New MySqlConnection
            con = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
            Dim cmd As New MySqlCommand("Select huellaimg From notificaciones where id=@id", con)
            cmd.Parameters.AddWithValue("@id", id)
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds, "notificaciones")
            Dim c As Integer = ds.Tables("notificaciones").Rows.Count
            If c > 0 Then
                Dim bytBLOBData() As Byte = ds.Tables("notificaciones").Rows(c - 1)("huellaimg")
                Dim stmBLOBData As New MemoryStream(bytBLOBData)
                Return Image.FromStream(stmBLOBData)
            Else
                Return Nothing
            End If
            ds.Dispose()
            da.Dispose()
            cmd.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        '*******
        Dim iFile As String = System.Environment.CurrentDirectory & "\" & "qr_img.jpg"
        Dim i As Integer = Val(LblFila.Text)

        dtgPersonas.Item(1, i).Value = TxtIdentificacion.Text
        dtgPersonas.Item(2, i).Value = TxtNombre.Text
        Dim codValidacion As String = generarCodigo(LblID.Text, TxtNombre.Text)
        Dim qrCode As String = "www.curaduria1cartagena.com/valnotificacion.aspx?c=" & codValidacion
        Dim qrImg As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        qrImg.QRCodeVersion = 0
        picQR.Image = qrImg.Encode(qrCode)
        picQR.Image.Save(iFile, Drawing.Imaging.ImageFormat.Jpeg)

        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()

        Dim cmd As New MySqlCommand
        cmd = cnnMySql.CreateCommand
        cmd.CommandText = "update notificaciones set idsolicitante=@idsolicitante, nombre=@nombre, notificado=@notificado, fechanotificado=@fechanotificado, codseguridad=@codseguridad, qrcode=@qrcode where id=@id and idexpediente=@idexpediente"
        cmd.Parameters.AddWithValue("id", LblID.Text)
        cmd.Parameters.AddWithValue("idsolicitante", TxtIdentificacion.Text)
        cmd.Parameters.AddWithValue("nombre", TxtNombre.Text)
        cmd.Parameters.AddWithValue("notificado", "S")
        cmd.Parameters.AddWithValue("fechanotificado", String.Format("{0:yyyy-MM-dd HH:mm:ss}", Date.Now))
        cmd.Parameters.AddWithValue("idexpediente", txtIDExpediente.Text)
        cmd.Parameters.AddWithValue("codseguridad", codValidacion)
        'Pasar imagen de la huella a bites para asignarlo a la BD

        Dim fs As New FileStream(iFile, FileMode.Open, FileAccess.Read)
        Dim fileSize As Integer = fs.Length
        Dim rawData() As Byte = New Byte(fileSize) {}
        fs.Read(rawData, 0, fileSize)
        fs.Close()
        cmd.Parameters.AddWithValue("qrcode", rawData)

        cmd.ExecuteNonQuery()

        dtgPersonas.Item(4, i).Value = "S"
        lblNotificado.Text = "NOTIFICADO"
        lblNotificado.ForeColor = Color.Blue
        btnGuardar.Enabled = False
        btnImprimir.Enabled = True
        cmd.Dispose()
        cnnMySql.Close()

        btnGuardar.Enabled = False
        btnImprimir.Enabled = True
    End Sub

    Private Function generarCodigo(ByVal id As Double, ByVal nombre As String) As String
        Dim codigo As String
        Dim letra As String

        Dim numAleatorio As New Random()
        Dim valorAleatorio As Integer = numAleatorio.Next(1, 99)
        codigo = Format(valorAleatorio, "00")

        codigo = codigo & Mid(nombre, 1, 1).ToLower

        codigo = codigo & Format(id, "000")

        valorAleatorio = numAleatorio.Next(65, 90)
        letra = Chr(valorAleatorio)
        codigo = codigo & letra

        valorAleatorio = numAleatorio.Next(97, 122)
        letra = Chr(valorAleatorio)
        codigo = codigo & letra

        valorAleatorio = numAleatorio.Next(65, 90)
        letra = Chr(valorAleatorio)
        codigo = codigo & letra

        Return codigo
    End Function

    Private Sub btnHuella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Formulario As New FrmHuellaCaptura
        Formulario.ShowDialog()
    End Sub

    Private Sub btnFirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirma.Click
        Dim usbDevices As wgssSTU.UsbDevices = New wgssSTU.UsbDevices()
        If usbDevices.Count <> 0 Then
            Try
                Dim usbDevice As wgssSTU.IUsbDevice = usbDevices(0)

                Dim Formulario As New FrmFirmaCaptura(usbDevice)
                Formulario.ShowDialog()

                Dim penData As List(Of wgssSTU.IPenData) = Formulario.getPenData
                If (Not (penData) Is Nothing) Then
                    'process penData here!
                    Dim information As wgssSTU.IInformation = Formulario.getInformation
                    Dim capability As wgssSTU.ICapability = Formulario.getCapability
                End If
                Formulario.Dispose()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        Else
            MessageBox.Show("No STU Devices attached!")
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim Formulario As New FrmAgregarPersona
        Formulario.Show()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim Formulario As New FrmReporte
        Formulario.Show()
    End Sub

End Class