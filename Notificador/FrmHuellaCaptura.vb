Imports DPFP
Imports DPFP.Capture
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class FrmHuellaCaptura
    Implements DPFP.Capture.EventHandler

    Dim cnnMySql As New MySqlConnection
    Dim iFile As String = System.Environment.CurrentDirectory & "\" & "fingerprint_img.jpg"

    Private Captura As DPFP.Capture.Capture
    Private Enroller As DPFP.Processing.Enrollment
    Private Delegate Sub _delegadoMuestra(ByVal texto As String)
    Private Delegate Sub _delegadoControles()
    Private template As DPFP.Template

    Private Sub modificarControles()
        If cmdGuardar.InvokeRequired Then
            Dim deleg As New _delegadoControles(AddressOf modificarControles)
            Me.Invoke(deleg, New Object() {})
        Else
            cmdGuardar.Enabled = True
        End If
    End Sub

    Private Sub mostrarVeces(ByVal texto As String)
        If LblContador.InvokeRequired Then
            Dim deleg As New _delegadoMuestra(AddressOf mostrarVeces)
            Me.Invoke(deleg, New Object() {texto})
        Else
            LblContador.Text = texto
        End If
    End Sub

    Protected Overridable Sub Init()
        Try
            Captura = New Capture()
            If Not Captura Is Nothing Then
                Captura.EventHandler = Me
                Enroller = New DPFP.Processing.Enrollment
                Dim text As New StringBuilder
                text.AppendFormat("Necesitas pasar el dedo {0} veces", Enroller.FeaturesNeeded)

                LblContador.Text = text.ToString
            Else
                MessageBox.Show("No se pudo instanciar la captura")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub iniciarCaptura()
        If Not Captura Is Nothing Then
            Try
                Captura.StartCapture()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub pararCaptura()
        If Not Captura Is Nothing Then
            Try
                Captura.StopCapture()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Protected Function convertSampleToPicture(ByVal Sample As DPFP.Sample) As Bitmap
        Dim Convertidor As New DPFP.Capture.SampleConversion()
        Dim mapaBits As Bitmap = Nothing

        Convertidor.ConvertToPicture(Sample, mapaBits)
        Return mapaBits
    End Function

    Protected Function extraerCaracteristicas(ByVal Sample As DPFP.Sample, ByVal Purpose As DPFP.Processing.DataPurpose) As DPFP.FeatureSet
        Dim extractor As New DPFP.Processing.FeatureExtraction()
        Dim alimentacion As DPFP.Capture.CaptureFeedback = DPFP.Capture.CaptureFeedback.None
        Dim caracteristicas As New DPFP.FeatureSet

        extractor.CreateFeatureSet(Sample, Purpose, alimentacion, caracteristicas)
        If alimentacion = DPFP.Capture.CaptureFeedback.Good Then
            Return caracteristicas
        Else
            Return Nothing
        End If
    End Function

    Private Sub FrmHuellaCaptura_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Init()
        iniciarCaptura()
    End Sub

    Private Sub FrmHuellaCaptura_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        pararCaptura()
    End Sub

    Private Sub FrmHuellaCaptura_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        pararCaptura()
    End Sub

    Private Sub FrmHuellaCaptura_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LblID.Text = FrmNotificador.LblID.Text
    End Sub

    Public Sub OnComplete(ByVal Capture As Object, ByVal ReaderSerialNumber As String, ByVal Sample As DPFP.Sample) Implements DPFP.Capture.EventHandler.OnComplete
        PicHuella.Image = convertSampleToPicture(Sample)
        Procesar(Sample)
    End Sub

    Public Sub OnFingerGone(ByVal Capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnFingerGone

    End Sub

    Public Sub OnFingerTouch(ByVal Capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnFingerTouch

    End Sub

    Public Sub OnReaderConnect(ByVal Capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnReaderConnect

    End Sub

    Public Sub OnReaderDisconnect(ByVal Capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnReaderDisconnect

    End Sub

    Public Sub OnSampleQuality(ByVal Capture As Object, ByVal ReaderSerialNumber As String, ByVal CaptureFeedback As DPFP.Capture.CaptureFeedback) Implements DPFP.Capture.EventHandler.OnSampleQuality

    End Sub

    Protected Sub Procesar(ByVal Sample As DPFP.Sample)
        Dim caracteristicas As DPFP.FeatureSet = extraerCaracteristicas(Sample, DPFP.Processing.DataPurpose.Enrollment)

        If Not caracteristicas Is Nothing Then
            Try
                Enroller.AddFeatures(caracteristicas)
            Finally
                Dim text As New StringBuilder
                text.AppendFormat("Se Necesita pasar el dedo {0} veces", Enroller.FeaturesNeeded)
                mostrarVeces(text.ToString)

                Select Case Enroller.TemplateStatus
                    Case Processing.Enrollment.Status.Ready
                        template = Enroller.Template
                        pararCaptura()
                        modificarControles()
                    Case Processing.Enrollment.Status.Failed
                        Enroller.Clear()
                        pararCaptura()
                        iniciarCaptura()
                End Select
            End Try
        End If
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()

        Dim cmd As New MySqlCommand
        cmd = cnnMySql.CreateCommand

        If LblID.Text.ToString.Equals("") Then
            MessageBox.Show("Seleccione la persona a notificar para capturar su huella", "Curaduria1CA", MessageBoxButtons.OK)
        Else
            saveImage2File()
            cmd.CommandText = "update notificaciones set huellatmp=@huellatmp, huellaimg=@huellaimg where id=@id"
            cmd.Parameters.AddWithValue("id", LblID.Text)
            'Pasar template de la huella a bytes para asignarlo a la BD
            Using fm As New MemoryStream(template.Bytes)
                cmd.Parameters.AddWithValue("huellatmp", fm.ToArray)
            End Using
            'Pasar imagen de la huella a bites para asignarlo a la BD
            Dim fs As New FileStream(iFile, FileMode.Open, FileAccess.Read)
            Dim fileSize As Integer = fs.Length
            Dim rawData() As Byte = New Byte(fileSize) {}
            fs.Read(rawData, 0, fileSize)
            fs.Close()
            cmd.Parameters.AddWithValue("huellaimg", rawData)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnnMySql.Close()
            'MessageBox.Show("Todo OK!", "Fingerprint", MessageBoxButtons.OK)
            cmdGuardar.Enabled = False

            pararCaptura()
            FrmNotificador.PicHuella.Image = PicHuella.Image

            Me.Close()
        End If
    End Sub

    Private Sub saveImage2File()
        PicHuella.Image.Save(iFile, Drawing.Imaging.ImageFormat.Jpeg)
    End Sub
End Class