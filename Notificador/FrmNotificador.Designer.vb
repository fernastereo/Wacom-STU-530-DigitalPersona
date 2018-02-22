<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNotificador
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIDExpediente = New System.Windows.Forms.TextBox()
        Me.txtFechaResolucion = New System.Windows.Forms.TextBox()
        Me.txtFechaRad = New System.Windows.Forms.TextBox()
        Me.txtResolucion = New System.Windows.Forms.TextBox()
        Me.txtModalidad = New System.Windows.Forms.TextBox()
        Me.txtLicencia = New System.Windows.Forms.TextBox()
        Me.txtSolcitante = New System.Windows.Forms.TextBox()
        Me.txtExpediente = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.cboVigencia = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRadicado = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.dtgPersonas = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BaseDatos = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Usuario = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Servidor = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.RutaAccess = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.picQR = New System.Windows.Forms.PictureBox()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.lblNotificado = New System.Windows.Forms.Label()
        Me.LblID = New System.Windows.Forms.Label()
        Me.btnFirma = New System.Windows.Forms.Button()
        Me.picFirma = New System.Windows.Forms.PictureBox()
        Me.LblFila = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.PicHuella = New System.Windows.Forms.PictureBox()
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.TxtIdentificacion = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dtgPersonas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFirma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicHuella, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtIDExpediente)
        Me.GroupBox1.Controls.Add(Me.txtFechaResolucion)
        Me.GroupBox1.Controls.Add(Me.txtFechaRad)
        Me.GroupBox1.Controls.Add(Me.txtResolucion)
        Me.GroupBox1.Controls.Add(Me.txtModalidad)
        Me.GroupBox1.Controls.Add(Me.txtLicencia)
        Me.GroupBox1.Controls.Add(Me.txtSolcitante)
        Me.GroupBox1.Controls.Add(Me.txtExpediente)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnBuscar)
        Me.GroupBox1.Controls.Add(Me.cboVigencia)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtRadicado)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(435, 226)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del Expediente"
        '
        'txtIDExpediente
        '
        Me.txtIDExpediente.Enabled = False
        Me.txtIDExpediente.Location = New System.Drawing.Point(19, 42)
        Me.txtIDExpediente.Name = "txtIDExpediente"
        Me.txtIDExpediente.Size = New System.Drawing.Size(47, 20)
        Me.txtIDExpediente.TabIndex = 19
        Me.txtIDExpediente.Visible = False
        '
        'txtFechaResolucion
        '
        Me.txtFechaResolucion.Enabled = False
        Me.txtFechaResolucion.Location = New System.Drawing.Point(318, 188)
        Me.txtFechaResolucion.Name = "txtFechaResolucion"
        Me.txtFechaResolucion.Size = New System.Drawing.Size(103, 20)
        Me.txtFechaResolucion.TabIndex = 18
        '
        'txtFechaRad
        '
        Me.txtFechaRad.Enabled = False
        Me.txtFechaRad.Location = New System.Drawing.Point(318, 67)
        Me.txtFechaRad.Name = "txtFechaRad"
        Me.txtFechaRad.Size = New System.Drawing.Size(103, 20)
        Me.txtFechaRad.TabIndex = 17
        '
        'txtResolucion
        '
        Me.txtResolucion.Enabled = False
        Me.txtResolucion.Location = New System.Drawing.Point(85, 188)
        Me.txtResolucion.Name = "txtResolucion"
        Me.txtResolucion.Size = New System.Drawing.Size(132, 20)
        Me.txtResolucion.TabIndex = 16
        '
        'txtModalidad
        '
        Me.txtModalidad.Enabled = False
        Me.txtModalidad.Location = New System.Drawing.Point(85, 157)
        Me.txtModalidad.Name = "txtModalidad"
        Me.txtModalidad.Size = New System.Drawing.Size(336, 20)
        Me.txtModalidad.TabIndex = 15
        '
        'txtLicencia
        '
        Me.txtLicencia.Enabled = False
        Me.txtLicencia.Location = New System.Drawing.Point(85, 126)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Size = New System.Drawing.Size(336, 20)
        Me.txtLicencia.TabIndex = 14
        '
        'txtSolcitante
        '
        Me.txtSolcitante.Enabled = False
        Me.txtSolcitante.Location = New System.Drawing.Point(85, 96)
        Me.txtSolcitante.Name = "txtSolcitante"
        Me.txtSolcitante.Size = New System.Drawing.Size(336, 20)
        Me.txtSolcitante.TabIndex = 13
        '
        'txtExpediente
        '
        Me.txtExpediente.Enabled = False
        Me.txtExpediente.Location = New System.Drawing.Point(85, 67)
        Me.txtExpediente.Name = "txtExpediente"
        Me.txtExpediente.Size = New System.Drawing.Size(132, 20)
        Me.txtExpediente.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(223, 191)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Fecha Resolución:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 191)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Resolución:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(223, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Fecha Radicado:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 160)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Modalidad:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Licencia:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Solicitante:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Expediente:"
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(318, 20)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(103, 23)
        Me.btnBuscar.TabIndex = 4
        Me.btnBuscar.Text = "&Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'cboVigencia
        '
        Me.cboVigencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVigencia.FormattingEnabled = True
        Me.cboVigencia.Location = New System.Drawing.Point(226, 21)
        Me.cboVigencia.Name = "cboVigencia"
        Me.cboVigencia.Size = New System.Drawing.Size(68, 21)
        Me.cboVigencia.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(169, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Vigencia:"
        '
        'txtRadicado
        '
        Me.txtRadicado.Location = New System.Drawing.Point(85, 22)
        Me.txtRadicado.Name = "txtRadicado"
        Me.txtRadicado.Size = New System.Drawing.Size(78, 20)
        Me.txtRadicado.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "N° Radicado:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAgregar)
        Me.GroupBox2.Controls.Add(Me.dtgPersonas)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 244)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(435, 281)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Personas a Notificar"
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(308, 242)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(113, 28)
        Me.btnAgregar.TabIndex = 5
        Me.btnAgregar.Text = "&Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'dtgPersonas
        '
        Me.dtgPersonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgPersonas.Location = New System.Drawing.Point(19, 28)
        Me.dtgPersonas.Name = "dtgPersonas"
        Me.dtgPersonas.Size = New System.Drawing.Size(402, 204)
        Me.dtgPersonas.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BaseDatos)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Usuario)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Password)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Servidor)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.RutaAccess)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Location = New System.Drawing.Point(456, 383)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(441, 142)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Parámetros de Conexión"
        Me.GroupBox3.Visible = False
        '
        'BaseDatos
        '
        Me.BaseDatos.Enabled = False
        Me.BaseDatos.Location = New System.Drawing.Point(322, 100)
        Me.BaseDatos.Name = "BaseDatos"
        Me.BaseDatos.Size = New System.Drawing.Size(103, 20)
        Me.BaseDatos.TabIndex = 27
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(227, 103)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Base de Datos:"
        '
        'Usuario
        '
        Me.Usuario.Enabled = False
        Me.Usuario.Location = New System.Drawing.Point(322, 65)
        Me.Usuario.Name = "Usuario"
        Me.Usuario.Size = New System.Drawing.Size(103, 20)
        Me.Usuario.TabIndex = 25
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(227, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Usuario:"
        '
        'Password
        '
        Me.Password.Enabled = False
        Me.Password.Location = New System.Drawing.Point(113, 100)
        Me.Password.Name = "Password"
        Me.Password.Size = New System.Drawing.Size(103, 20)
        Me.Password.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(18, 103)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Password:"
        '
        'Servidor
        '
        Me.Servidor.Enabled = False
        Me.Servidor.Location = New System.Drawing.Point(113, 65)
        Me.Servidor.Name = "Servidor"
        Me.Servidor.Size = New System.Drawing.Size(103, 20)
        Me.Servidor.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(18, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Servidor:"
        '
        'RutaAccess
        '
        Me.RutaAccess.Enabled = False
        Me.RutaAccess.Location = New System.Drawing.Point(113, 30)
        Me.RutaAccess.Name = "RutaAccess"
        Me.RutaAccess.Size = New System.Drawing.Size(312, 20)
        Me.RutaAccess.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Ruta Access:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.picQR)
        Me.GroupBox4.Controls.Add(Me.btnImprimir)
        Me.GroupBox4.Controls.Add(Me.lblNotificado)
        Me.GroupBox4.Controls.Add(Me.LblID)
        Me.GroupBox4.Controls.Add(Me.btnFirma)
        Me.GroupBox4.Controls.Add(Me.picFirma)
        Me.GroupBox4.Controls.Add(Me.LblFila)
        Me.GroupBox4.Controls.Add(Me.btnGuardar)
        Me.GroupBox4.Controls.Add(Me.PicHuella)
        Me.GroupBox4.Controls.Add(Me.TxtNombre)
        Me.GroupBox4.Controls.Add(Me.TxtIdentificacion)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(456, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(441, 365)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Persona a Notificar"
        '
        'picQR
        '
        Me.picQR.Location = New System.Drawing.Point(21, 286)
        Me.picQR.Name = "picQR"
        Me.picQR.Size = New System.Drawing.Size(109, 78)
        Me.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picQR.TabIndex = 30
        Me.picQR.TabStop = False
        Me.picQR.Visible = False
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(312, 243)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(113, 28)
        Me.btnImprimir.TabIndex = 29
        Me.btnImprimir.Text = "I&mprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'lblNotificado
        '
        Me.lblNotificado.AutoSize = True
        Me.lblNotificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotificado.ForeColor = System.Drawing.Color.Blue
        Me.lblNotificado.Location = New System.Drawing.Point(252, 24)
        Me.lblNotificado.Name = "lblNotificado"
        Me.lblNotificado.Size = New System.Drawing.Size(0, 13)
        Me.lblNotificado.TabIndex = 28
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New System.Drawing.Point(246, 25)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(0, 13)
        Me.LblID.TabIndex = 27
        Me.LblID.Visible = False
        '
        'btnFirma
        '
        Me.btnFirma.Location = New System.Drawing.Point(18, 243)
        Me.btnFirma.Name = "btnFirma"
        Me.btnFirma.Size = New System.Drawing.Size(113, 28)
        Me.btnFirma.TabIndex = 22
        Me.btnFirma.Text = "Iniciar &Captura"
        Me.btnFirma.UseVisualStyleBackColor = True
        '
        'picFirma
        '
        Me.picFirma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFirma.Location = New System.Drawing.Point(177, 99)
        Me.picFirma.Name = "picFirma"
        Me.picFirma.Size = New System.Drawing.Size(248, 124)
        Me.picFirma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFirma.TabIndex = 26
        Me.picFirma.TabStop = False
        '
        'LblFila
        '
        Me.LblFila.AutoSize = True
        Me.LblFila.Location = New System.Drawing.Point(12, 335)
        Me.LblFila.Name = "LblFila"
        Me.LblFila.Size = New System.Drawing.Size(0, 13)
        Me.LblFila.TabIndex = 25
        Me.LblFila.Visible = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(177, 243)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(113, 28)
        Me.btnGuardar.TabIndex = 24
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'PicHuella
        '
        Me.PicHuella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicHuella.Location = New System.Drawing.Point(15, 100)
        Me.PicHuella.Name = "PicHuella"
        Me.PicHuella.Size = New System.Drawing.Size(116, 124)
        Me.PicHuella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicHuella.TabIndex = 18
        Me.PicHuella.TabStop = False
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(108, 51)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(317, 20)
        Me.TxtNombre.TabIndex = 17
        '
        'TxtIdentificacion
        '
        Me.TxtIdentificacion.Location = New System.Drawing.Point(108, 21)
        Me.TxtIdentificacion.Name = "TxtIdentificacion"
        Me.TxtIdentificacion.Size = New System.Drawing.Size(132, 20)
        Me.TxtIdentificacion.TabIndex = 16
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(12, 54)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(94, 13)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Nombre Completo:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(12, 25)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "N° Identificación:"
        '
        'FrmNotificador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 537)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmNotificador"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmNotificador"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dtgPersonas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFirma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicHuella, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents cboVigencia As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRadicado As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFechaResolucion As System.Windows.Forms.TextBox
    Friend WithEvents txtFechaRad As System.Windows.Forms.TextBox
    Friend WithEvents txtResolucion As System.Windows.Forms.TextBox
    Friend WithEvents txtModalidad As System.Windows.Forms.TextBox
    Friend WithEvents txtLicencia As System.Windows.Forms.TextBox
    Friend WithEvents txtSolcitante As System.Windows.Forms.TextBox
    Friend WithEvents txtExpediente As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtgPersonas As System.Windows.Forms.DataGridView
    Friend WithEvents txtIDExpediente As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BaseDatos As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Usuario As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Servidor As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents RutaAccess As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtNombre As System.Windows.Forms.TextBox
    Friend WithEvents TxtIdentificacion As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnFirma As System.Windows.Forms.Button
    Friend WithEvents PicHuella As System.Windows.Forms.PictureBox
    Friend WithEvents LblFila As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents picFirma As System.Windows.Forms.PictureBox
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents lblNotificado As System.Windows.Forms.Label
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents picQR As System.Windows.Forms.PictureBox
End Class
