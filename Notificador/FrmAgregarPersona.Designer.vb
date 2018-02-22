<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAgregarPersona
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
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.TxtIdentificacion = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dbcTipo = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(314, 117)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(113, 28)
        Me.btnGuardar.TabIndex = 29
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(109, 44)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(317, 20)
        Me.TxtNombre.TabIndex = 28
        '
        'TxtIdentificacion
        '
        Me.TxtIdentificacion.Location = New System.Drawing.Point(109, 14)
        Me.TxtIdentificacion.Name = "TxtIdentificacion"
        Me.TxtIdentificacion.Size = New System.Drawing.Size(132, 20)
        Me.TxtIdentificacion.TabIndex = 27
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 47)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(94, 13)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Nombre Completo:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 18)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "N° Identificación:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(166, 117)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(113, 28)
        Me.btnCancelar.TabIndex = 30
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Tipo Notificado:"
        '
        'dbcTipo
        '
        Me.dbcTipo.FormattingEnabled = True
        Me.dbcTipo.Location = New System.Drawing.Point(109, 77)
        Me.dbcTipo.Name = "dbcTipo"
        Me.dbcTipo.Size = New System.Drawing.Size(132, 21)
        Me.dbcTipo.TabIndex = 32
        '
        'FrmAgregarPersona
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 157)
        Me.Controls.Add(Me.dbcTipo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.TxtNombre)
        Me.Controls.Add(Me.TxtIdentificacion)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAgregarPersona"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agregar Persona"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents TxtNombre As System.Windows.Forms.TextBox
    Friend WithEvents TxtIdentificacion As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dbcTipo As System.Windows.Forms.ComboBox
End Class
