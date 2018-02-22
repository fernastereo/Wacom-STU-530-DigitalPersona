<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHuellaCaptura
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
        Me.cmdGuardar = New System.Windows.Forms.Button()
        Me.LblContador = New System.Windows.Forms.Label()
        Me.PicHuella = New System.Windows.Forms.PictureBox()
        Me.LblID = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PicHuella, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Enabled = False
        Me.cmdGuardar.Location = New System.Drawing.Point(191, 413)
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(113, 28)
        Me.cmdGuardar.TabIndex = 5
        Me.cmdGuardar.Text = "Guardar"
        Me.cmdGuardar.UseVisualStyleBackColor = True
        '
        'LblContador
        '
        Me.LblContador.AutoSize = True
        Me.LblContador.Location = New System.Drawing.Point(12, 393)
        Me.LblContador.Name = "LblContador"
        Me.LblContador.Size = New System.Drawing.Size(0, 13)
        Me.LblContador.TabIndex = 4
        '
        'PicHuella
        '
        Me.PicHuella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicHuella.Location = New System.Drawing.Point(12, 61)
        Me.PicHuella.Name = "PicHuella"
        Me.PicHuella.Size = New System.Drawing.Size(292, 317)
        Me.PicHuella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicHuella.TabIndex = 3
        Me.PicHuella.TabStop = False
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New System.Drawing.Point(12, 421)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(0, 13)
        Me.LblID.TabIndex = 28
        Me.LblID.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 45)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "INDIQUE AL SOLICITANTE  QUE DEBE COLOCAR SU DEDO INDICE DERECHO EN EL LECTOR DE H" & _
            "UELLAS 4 VECES"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmHuellaCaptura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(315, 449)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.cmdGuardar)
        Me.Controls.Add(Me.LblContador)
        Me.Controls.Add(Me.PicHuella)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmHuellaCaptura"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Captura de Huella"
        CType(Me.PicHuella, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdGuardar As System.Windows.Forms.Button
    Friend WithEvents LblContador As System.Windows.Forms.Label
    Friend WithEvents PicHuella As System.Windows.Forms.PictureBox
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
