<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFotoCaptura
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
        Me.LblID = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.PicFoto = New System.Windows.Forms.PictureBox()
        Me.btnCapturar = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.lstDevices = New System.Windows.Forms.ListBox()
        CType(Me.PicFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New System.Drawing.Point(11, 345)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(0, 13)
        Me.LblID.TabIndex = 32
        '
        'btnGuardar
        '
        Me.btnGuardar.Enabled = False
        Me.btnGuardar.Location = New System.Drawing.Point(403, 359)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(113, 28)
        Me.btnGuardar.TabIndex = 31
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'PicFoto
        '
        Me.PicFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicFoto.Location = New System.Drawing.Point(312, 12)
        Me.PicFoto.Name = "PicFoto"
        Me.PicFoto.Size = New System.Drawing.Size(292, 322)
        Me.PicFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicFoto.TabIndex = 29
        Me.PicFoto.TabStop = False
        '
        'btnCapturar
        '
        Me.btnCapturar.Location = New System.Drawing.Point(101, 359)
        Me.btnCapturar.Name = "btnCapturar"
        Me.btnCapturar.Size = New System.Drawing.Size(113, 28)
        Me.btnCapturar.TabIndex = 33
        Me.btnCapturar.Text = "Capturar"
        Me.btnCapturar.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(38, 253)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(113, 34)
        Me.btnStart.TabIndex = 34
        Me.btnStart.Text = "iniciar"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(169, 255)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(117, 31)
        Me.btnStop.TabIndex = 35
        Me.btnStop.Text = "detener"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lstDevices
        '
        Me.lstDevices.FormattingEnabled = True
        Me.lstDevices.Location = New System.Drawing.Point(3, 12)
        Me.lstDevices.Name = "lstDevices"
        Me.lstDevices.Size = New System.Drawing.Size(303, 121)
        Me.lstDevices.TabIndex = 36
        '
        'FrmFotoCaptura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 409)
        Me.Controls.Add(Me.lstDevices)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnCapturar)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.PicFoto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmFotoCaptura"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Captura de Foto"
        CType(Me.PicFoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents PicFoto As System.Windows.Forms.PictureBox
    Friend WithEvents btnCapturar As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents lstDevices As System.Windows.Forms.ListBox
End Class
