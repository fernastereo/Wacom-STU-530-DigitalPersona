<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFirmaCaptura_Datos
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
        Me.PicFirma = New System.Windows.Forms.PictureBox()
        Me.LblID = New System.Windows.Forms.Label()
        CType(Me.PicFirma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicFirma
        '
        Me.PicFirma.Location = New System.Drawing.Point(119, 166)
        Me.PicFirma.Name = "PicFirma"
        Me.PicFirma.Size = New System.Drawing.Size(139, 70)
        Me.PicFirma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicFirma.TabIndex = 31
        Me.PicFirma.TabStop = False
        Me.PicFirma.Visible = False
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New System.Drawing.Point(12, 223)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(0, 13)
        Me.LblID.TabIndex = 32
        Me.LblID.Visible = False
        '
        'FrmFirmaCaptura_Datos
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.PicFirma)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFirmaCaptura_Datos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Firma Captura"
        CType(Me.PicFirma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PicFirma As System.Windows.Forms.PictureBox
    Friend WithEvents LblID As System.Windows.Forms.Label
End Class
