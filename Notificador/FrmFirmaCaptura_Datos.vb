Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class FrmFirmaCaptura_Datos
    Dim cnnMySql As New MySqlConnection
    Dim tiempoEspera As Integer = 6
    Dim swHuella As Boolean = True
    Dim swFoco As Boolean = False
    Public usbDevice As wgssSTU.IUsbDevice
    Private m_tablet As wgssSTU.Tablet
    Private m_capability As wgssSTU.ICapability
    Private m_information As wgssSTU.IInformation

    'In order to simulate buttons, we have our own Button class that stores the bounds and event handler.
    'Using an array of these makes it easy to add or remove buttons as desired.
    Private Delegate Sub ButtonClick()

    Private Structure Label
        Public Bounds As Rectangle
        Public Text As String
    End Structure

    Private Structure Picture
        Public Bounds As Rectangle
        Public Imagen As Image
    End Structure

    Private Structure Button
        Public Bounds As Rectangle
        Public Text As String
        Public Click As EventHandler

        Public Sub PerformClick()
            Click(Me, Nothing)
        End Sub
    End Structure

    Private m_penInk As Pen 'cached object.

    'The isDown flag is used like this:
    '0 = up
    '+ve = down, pressed on button number
    '-1 = down, inking
    '-2 = down, ignoring
    Private m_isDown As Integer

    Private m_penData As List(Of wgssSTU.IPenData) 'Array of data being stored. This can be subsequently used as desired.

    Private m_btns(2) As Button 'The array of buttons that we are emulating.

    Private m_lbls(1) As Label 'The array of labels that I'm emulating too
    Private m_pics(0) As Picture 'The array of one picture that I'm emulating too

    Private m_bitmap As Bitmap 'This bitmap that we display on the screen.
    Private m_encodingMode As wgssSTU.encodingMode 'How we send the bitmap to the device.
    Private m_bitmapData() As Byte 'This is the flattened data of the bitmap that we send to the device.

    'As per the file comment, there are three coordinate systems to deal with.
    'To help understand, we have left the calculations in place rather than optimise them.

    Private Function tabletToClient(ByVal penData As wgssSTU.IPenData) As PointF
        'Client means the Windows Form coordinates.
        Dim x As Single = CType(penData.x, Single) * Me.ClientSize.Width / m_capability.tabletMaxX
        Dim y As Single = CType(penData.y, Single) * Me.ClientSize.Height / m_capability.tabletMaxY
        Return New PointF(x, y)
    End Function

    Private Function tabletToScreen(ByVal penData As wgssSTU.IPenData) As Point
        'Screen means LCD screen of the tablet.
        Dim x As Single = CType(penData.x, Single) * m_capability.screenWidth / m_capability.tabletMaxX
        Dim y As Single = CType(penData.y, Single) * m_capability.screenHeight / m_capability.tabletMaxY
        Dim pf As New PointF(x, y)
        Return Point.Round(pf)
    End Function

    Private Function clientToScreen(ByVal pt As Point) As Point
        'client (window) coordinates to LCD screen coordinates. 
        'This is needed for converting mouse coordinates into LCD bitmap coordinates as that's
        'what this application uses as the coordinate space for buttons.
        Dim x As Single = CType(pt.X, Single) * m_capability.screenWidth / Me.ClientSize.Width
        Dim y As Single = CType(pt.Y, Single) * m_capability.screenHeight / Me.ClientSize.Height
        Dim pf As New PointF(x, y)
        Return Point.Round(pf)
    End Function

    Private Sub clearScreen()
        'note: There is no need to clear the tablet screen prior to writing an image.
        'No es necesario borrar la pantalla de la tableta antes de escribir una imagen
        m_tablet.writeImage(CType(m_encodingMode, Byte), m_bitmapData)

        m_penData.Clear()
        m_isDown = 0
        Me.Invalidate()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Save te image
        SaveImage()
        saveImage2BD()
        FrmNotificador.btnGuardar.Enabled = True
        Me.Close()
    End Sub

    'Save the image in a local file
    Private Sub SaveImage()
        Try
            Dim bitmap As Bitmap = GetImage(New Rectangle(0, 0, m_capability.screenWidth, m_capability.screenHeight))
            Dim saveLocation As String = System.Environment.CurrentDirectory & "\" & "signature_" & Format(CDbl(LblID.Text), "0000") & ".jpg"
            bitmap.Save(saveLocation, ImageFormat.Jpeg)
        Catch ex As Exception
            MessageBox.Show("Exception: " & ex.Message)
        End Try
    End Sub

    'Draw an image with the existed points.
    Public Function GetImage(ByVal rect As Rectangle) As Bitmap
        Dim retVal As Bitmap = Nothing
        Dim bitmap As Bitmap = Nothing
        Dim brush As SolidBrush = Nothing
        Try
            bitmap = New Bitmap(rect.Width, rect.Height)
            Dim graphics As Graphics = graphics.FromImage(bitmap)
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            brush = New SolidBrush(Color.White)
            graphics.FillRectangle(brush, 0, 0, rect.Width, rect.Height)
            For i As Integer = 1 To m_penData.Count - 1
                Dim p1 As PointF = tabletToScreen(m_penData(i - 1))
                Dim p2 As PointF = tabletToScreen(m_penData(i))
                If m_penData(i - 1).sw > 0 OrElse m_penData(i).sw > 0 Then
                    graphics.DrawLine(m_penInk, p1, p2)
                End If
            Next

            retVal = bitmap
            bitmap = Nothing
        Finally
            If brush IsNot Nothing Then brush.Dispose()
            If bitmap IsNot Nothing Then bitmap.Dispose()
        End Try

        Return retVal
    End Function

    Private Sub saveImage2BD()
        cnnMySql = New MySqlConnection
        cnnMySql = openMySql(cServidor, cUsuario, cPassword, cBaseDatos)
        cnnMySql.Open()

        Dim cmd As New MySqlCommand
        cmd = cnnMySql.CreateCommand

        If LblID.Text.ToString.Equals("") Then
            MessageBox.Show("Seleccione la persona a notificar para capturar su firma", "Curaduria1CA", MessageBoxButtons.OK)
        Else
            Dim iFile As String = System.Environment.CurrentDirectory & "\" & "signature_" & Format(CDbl(LblID.Text), "0000") & ".jpg"
            PicFirma.Image = Image.FromFile(iFile)
            Dim fs As New FileStream(iFile, FileMode.Open, FileAccess.Read)
            Dim fileSize As Integer = fs.Length
            Dim rawData() As Byte = New Byte(fileSize) {}
            fs.Read(rawData, 0, fileSize)
            fs.Close()
            cmd.CommandText = "update notificaciones set firma=@firma where id=@id"
            cmd.Parameters.AddWithValue("id", LblID.Text)
            cmd.Parameters.AddWithValue("firma", rawData)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnnMySql.Close()
            FrmNotificador.picFirma.Image = PicFirma.Image

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        'You probably want to add additional processing here.
        'Me.m_penData = Nothing
        'Me.Close()

        'MessageBox.Show("NO AUTORIZO")
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs)
        If m_penData.Count <> 0 Then
            clearScreen()
        End If
    End Sub

    'Pass in the device you want to connect to!
    Public Sub New(ByVal usbDevice As wgssSTU.IUsbDevice)
        'This is a DPI aware application, so ensure you understand how .NET client coordinates work.
        'Testing using a Windows installation set to a high DPI is recommended to understand how
        'values get scaled or not.

        'This is a DPI aware application, so ensure you understand how .NET client coordinates work.
        'Testing using a Windows installation set to a high DPI is recommended to understand how
        'values get scaled or not.

        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        Call InitializeComponent()

        m_penData = New List(Of wgssSTU.IPenData)
        m_tablet = New wgssSTU.Tablet

        Dim protocolHelper As wgssSTU.ProtocolHelper = New wgssSTU.ProtocolHelper
        ' A more sophisticated applications should cycle for a few times as the
        ' connection may only be temporarily unavailable for a second or so. 
        ' For example, if a background process such as Wacom STU Display
        ' is running, this periodically updates a slideshow of images to the device.

        Dim ec As wgssSTU.IErrorCode = m_tablet.usbConnect(usbDevice, True)
        If ec.value = 0 Then
            m_capability = m_tablet.getCapability
            m_information = m_tablet.getInformation
        Else
            Throw New Exception(ec.message)
        End If

        Me.SuspendLayout()
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        ' Set the size of the client window to be actual size, 
        ' based on the reported DPI of the monitor.
        Dim w As Integer = CType(m_capability.tabletMaxX / 2540.0! * 96.0!, Integer)
        Dim h As Integer = CType(m_capability.tabletMaxY / 2540.0! * 96.0!, Integer)
        Dim clientSize As Size = New Size(w, h)
        Me.ClientSize = clientSize
        Me.ResumeLayout()

        'm_btns(3) = New Button

        If usbDevice.idProduct <> 162 Then
            'Para nuestro modelo de tablet siempre entra aqui
            'Place the buttons across the bottom of the screen.
            Dim w2 As Integer = m_capability.screenWidth / 3
            Dim w3 As Integer = m_capability.screenWidth / 3
            Dim w1 As Integer = m_capability.screenWidth - w2 - w3
            Dim y As Integer = m_capability.screenHeight * 6 / 7
            h = m_capability.screenHeight - y

            m_lbls(0).Bounds = New Rectangle(100, 310, 600, 40)
            m_lbls(1).Bounds = New Rectangle(100, 370, 600, 40)

            m_pics(0).Bounds = New Rectangle(320, 80, 180, 210)
        Else
            ' The STU-300 is very shallow, so it is better to utilise
            ' the buttons to the side of the display instead.
            Dim x As Integer = m_capability.screenWidth * 3 / 4
            w = m_capability.screenWidth - x

            Dim h2 As Integer = m_capability.screenHeight / 3
            Dim h3 As Integer = m_capability.screenHeight / 3
            Dim h1 As Integer = m_capability.screenHeight - h2 - h3

            m_lbls(0).Bounds = New Rectangle(x, 0, w, h1)
            m_lbls(1).Bounds = New Rectangle(x, h1 + h2, w, h3)

            m_pics(0).Bounds = New Rectangle(20, 60, 240, 270)
        End If
        m_lbls(0).Text = "N° IDENTIFICACION: " & FrmNotificador.TxtIdentificacion.Text
        m_lbls(1).Text = "NOMBRE : " & Mid(FrmNotificador.TxtNombre.Text, 1, 32)
        m_pics(0).Imagen = FrmNotificador.PicHuella.Image

        ' Disable color if the STU-520 bulk driver isn't installed.
        ' This isn't necessary, but uploading colour images with out the driver
        ' is very slow.

        ' Calculate the encodingMode that will be used to update the image
        Dim idP As UShort = m_tablet.getProductId
        Dim encodingFlag As wgssSTU.encodingFlag = CType(protocolHelper.simulateEncodingFlag(idP, 0), wgssSTU.encodingFlag)
        Dim useColor As Boolean = False
        If (encodingFlag And (wgssSTU.encodingFlag.EncodingFlag_16bit Or wgssSTU.encodingFlag.EncodingFlag_24bit)) <> 0 Then
            If m_tablet.supportsWrite Then
                useColor = True
            End If
        End If
        If (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_24bit) <> 0 Then
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_24bit_Bulk
        ElseIf (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_16bit) <> 0 Then
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_16bit_Bulk
        Else
            'assumes 1bit is available
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_1bit
        End If

        ' Size the bitmap to the size of the LCD screen.
        ' This application uses the same bitmap for both the screen and client (window).
        ' However, at high DPI, this bitmap will be stretch and it would be better to 
        ' create individual bitmaps for screen and client at native resolutions.

        m_bitmap = New Bitmap(m_capability.screenWidth, m_capability.screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        'Dim iFile As String = System.Environment.CurrentDirectory & "\" & "800x480-530.png"
        'm_bitmap = New Bitmap(iFile)
        Dim gfx As Graphics = Graphics.FromImage(m_bitmap)
        gfx.Clear(Color.White)

        'Uses pixels for units as DPI won't be accurate for tablet LCD.
        Dim font As Font = New Font(FontFamily.GenericSansSerif, 22, GraphicsUnit.Pixel)
        Dim sf As StringFormat = New StringFormat
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Center

        If useColor Then
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
        Else
            ' Anti-aliasing should be turned off for monochrome devices.
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel
        End If

        ' Draw the buttons
        Dim i As Integer
        '****
        Dim iFile As String = System.Environment.CurrentDirectory & "\" & "03.jpg"
        Dim m_image As Image = Image.FromFile(iFile)
        gfx.DrawImage(m_image, 0, 0)
        '****
        For i = 0 To m_lbls.Length - 1
            If useColor Then
                gfx.FillRectangle(Brushes.Transparent, m_lbls(i).Bounds)
            End If
            gfx.DrawRectangle(Pens.Black, m_lbls(i).Bounds)
            gfx.DrawString(m_lbls(i).Text, font, Brushes.Black, m_lbls(i).Bounds, sf)
        Next
        gfx.FillRectangle(Brushes.Transparent, m_pics(0).Bounds)
        gfx.DrawRectangle(Pens.Black, m_pics(0).Bounds)
        gfx.DrawImage(m_pics(0).Imagen, m_pics(0).Bounds)
        gfx.Dispose()
        font.Dispose()

        ' Finally, use this bitmap for the window background.
        Me.BackgroundImage = m_bitmap
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Now the bitmap has been created, it needs to be converted to device-native
        ' format.
        ' Unfortunately it is not possible for the native COM component to
        ' understand .NET bitmaps. We have therefore convert the .NET bitmap
        ' into a memory blob that will be understood by COM.

        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        m_bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        m_bitmapData = CType(protocolHelper.resizeAndFlatten(stream.ToArray, 0, 0, CType(m_bitmap.Width, UInteger), CType(m_bitmap.Height, UInteger), m_capability.screenWidth, m_capability.screenHeight, CType(m_encodingMode, Byte), wgssSTU.Scale.Scale_Fit, 0, 0), Byte())
        protocolHelper = Nothing
        stream.Dispose()

        ' If you wish to further optimize image transfer, you can compress the image using 
        ' the Zlib algorithm.

        Dim useZlibCompression As Boolean = False
        If (Not useColor AndAlso useZlibCompression) Then
            ' m_bitmapData = compress_using_zlib(m_bitmapData); // insert compression here!
            m_encodingMode = (m_encodingMode Or wgssSTU.encodingMode.EncodingMode_Zlib)
        End If

        'Calculate the size and cache the inking pen.
        Dim s As SizeF = Me.AutoScaleDimensions
        Dim inkWidthMM As Single = 0.7F
        m_penInk = New Pen(Color.DarkBlue, inkWidthMM / 25.4! * ((s.Width + s.Height) / 2.0!))
        m_penInk.EndCap = System.Drawing.Drawing2D.LineCap.Round
        m_penInk.LineJoin = System.Drawing.Drawing2D.LineJoin.Round

        ' Add the delagate that receives pen data.
        AddHandler m_tablet.onPenData, AddressOf Me.onPenData
        AddHandler m_tablet.onGetReportException, AddressOf Me.onGetReportException

        'Initialize the screen
        clearScreen()

        ' Enable the pen data on the screen (if not already)
        m_tablet.setInkingMode(1)

        m_tablet.disconnect()

        System.Threading.Thread.Sleep(2000)
        Dim usbDevices As wgssSTU.UsbDevices = New wgssSTU.UsbDevices()
        If usbDevices.Count <> 0 Then
            Dim usbDevice2 As wgssSTU.IUsbDevice = usbDevices(0)

            capturaFirma(usbDevice2)

            Dim penData As List(Of wgssSTU.IPenData) = Me.getPenData
            If (Not (penData) Is Nothing) Then
                'process penData here!
                Dim information As wgssSTU.IInformation = Me.getInformation
                Dim capability As wgssSTU.ICapability = Me.getCapability
            End If
        Else
            MessageBox.Show("No STU Devices attached!")
        End If
    End Sub

    Private Sub onGetReportException(ByVal tabletEventsException As wgssSTU.ITabletEventsException)
        Try
            tabletEventsException.getException()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            m_tablet.disconnect()
            m_tablet = Nothing
            m_penData = Nothing
            Me.Close()
        End Try
    End Sub

    Private Sub onPenData(ByVal penData As wgssSTU.IPenData)
        Dim pt As Point = tabletToScreen(penData)

        Dim btn As Integer = 0 'will be +ve if the pen is over a button.
        Dim i As Integer
        For i = 0 To m_btns.Length - 1
            If m_btns(i).Bounds.Contains(pt) Then
                btn = i + 1
                Exit For
            End If
        Next

        Dim isDown As Boolean = (penData.sw <> 0)
        ' This code uses a model of four states the pen can be in:
        ' down or up, and whether this is the first sample of that state.

        If isDown Then
            If (m_isDown = 0) Then
                ' transition to down
                If (btn > 0) Then
                    ' We have put the pen down on a button.
                    ' Track the pen without inking on the client.
                    m_isDown = btn
                Else
                    ' We have put the pen down somewhere else.
                    ' Treat it as part of the signature.
                    m_isDown = -1
                End If

            Else
                ' already down, keep doing what we're doing!
            End If

            ' draw
            If ((m_penData.Count <> 0) AndAlso (m_isDown = -1)) Then
                ' Draw a line from the previous down point to this down point.
                ' This is the simplist thing you can do; a more sophisticated program
                ' can perform higher quality rendering than this!
                Dim gfx As Graphics = Me.CreateGraphics
                gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                Dim prevPenData As wgssSTU.IPenData = m_penData((m_penData.Count - 1))
                Dim prev As PointF = tabletToClient(prevPenData)
                gfx.DrawLine(m_penInk, prev, tabletToClient(penData))
                gfx.Dispose()
            End If

            ' The pen is down, store it for use later.
            If (m_isDown = -1) Then
                m_penData.Add(penData)
            End If

        Else
            If (m_isDown <> 0) Then
                ' transition to up
                If (btn > 0) Then
                    ' The pen is over a button
                    If (btn = m_isDown) Then
                        ' The pen was pressed down over the same button as is was lifted now. 
                        ' Consider that as a click!
                        m_btns((btn - 1)).PerformClick()
                    End If

                End If

                m_isDown = 0
            Else
                ' still up
            End If

            ' Add up data once we have collected some down data.
            If (m_penData.Count <> 0) Then
                m_penData.Add(penData)
            End If

        End If
    End Sub

    Private Sub FrmFirmaCaptura_Datos_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Ensure that you correctly disconnect from the tablet, otherwise you are 
        ' likely to get errors when wanting to connect a second time.

        If (Not (m_tablet) Is Nothing) Then
            RemoveHandler m_tablet.onPenData, AddressOf Me.onPenData
            RemoveHandler m_tablet.onGetReportException, AddressOf Me.onGetReportException
            m_tablet.setInkingMode(0)
            m_tablet.setClearScreen()
            m_tablet.disconnect()
        End If

        m_penInk.Dispose()

    End Sub

    Private Sub FrmFirmaCaptura_Datos_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Dim pt As Point = clientToScreen(e.Location)
        For Each btn As Button In m_btns
            If btn.Bounds.Contains(pt) Then
                btn.PerformClick()
                Exit For
            End If
        Next
    End Sub

    Private Sub FrmFirmaCaptura_Datos_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If (m_penData.Count <> 0) Then
            ' Redraw all the pen data up until now!
            Dim gfx As Graphics = e.Graphics
            gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            Dim isDown As Boolean = False
            Dim prev As PointF = New PointF
            Dim i As Integer = 0
            Do While (i < m_penData.Count)
                If (m_penData(i).sw <> 0) Then
                    If Not isDown Then
                        isDown = True
                        prev = tabletToClient(m_penData(i))
                    Else
                        Dim curr As PointF = tabletToClient(m_penData(i))
                        gfx.DrawLine(m_penInk, prev, curr)
                        prev = curr
                    End If

                ElseIf isDown Then
                    isDown = False
                End If

                i = (i + 1)
            Loop

        End If
    End Sub

    Public Function getPenData() As List(Of wgssSTU.IPenData)
        Return m_penData
    End Function

    Public Function getCapability() As wgssSTU.ICapability
        Return If(m_penData IsNot Nothing, m_capability, Nothing)
    End Function

    Public Function getInformation() As wgssSTU.IInformation
        Return If(m_penData IsNot Nothing, m_information, Nothing)
    End Function

    Public Sub capturaFirma(ByVal usbDevice As wgssSTU.IUsbDevice)
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        m_penData = New List(Of wgssSTU.IPenData)
        m_tablet = New wgssSTU.Tablet

        Dim protocolHelper As wgssSTU.ProtocolHelper = New wgssSTU.ProtocolHelper
        ' A more sophisticated applications should cycle for a few times as the
        ' connection may only be temporarily unavailable for a second or so. 
        ' For example, if a background process such as Wacom STU Display
        ' is running, this periodically updates a slideshow of images to the device.

        Dim ec As wgssSTU.IErrorCode = m_tablet.usbConnect(usbDevice, True)
        If ec.value = 0 Then
            m_capability = m_tablet.getCapability
            m_information = m_tablet.getInformation
        Else
            Throw New Exception(ec.message)
        End If

        Me.SuspendLayout()
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        ' Set the size of the client window to be actual size, 
        ' based on the reported DPI of the monitor.
        Dim w As Integer = CType(m_capability.tabletMaxX / 2540.0! * 96.0!, Integer)
        Dim h As Integer = CType(m_capability.tabletMaxY / 2540.0! * 96.0!, Integer)
        Dim clientSize As Size = New Size(w, h)
        Me.ClientSize = clientSize
        Me.ResumeLayout()

        'm_btns(3) = New Button

        If usbDevice.idProduct <> 162 Then
            'Place the buttons across the bottom of the screen.
            Dim w2 As Integer = m_capability.screenWidth / 3
            Dim w3 As Integer = m_capability.screenWidth / 3
            Dim w1 As Integer = m_capability.screenWidth - w2 - w3
            Dim y As Integer = m_capability.screenHeight * 6 / 7
            h = m_capability.screenHeight - y

            m_btns(0).Bounds = New Rectangle(0, y, w1, h)
            '            m_btns(1).Bounds = New Rectangle(w1, y, w2, h)
            m_btns(1).Bounds = New Rectangle(w1 + w2, y, w3, h)
        Else
            ' The STU-300 is very shallow, so it is better to utilise
            ' the buttons to the side of the display instead.
            Dim x As Integer = m_capability.screenWidth * 3 / 4
            w = m_capability.screenWidth - x

            Dim h2 As Integer = m_capability.screenHeight / 3
            Dim h3 As Integer = m_capability.screenHeight / 3
            Dim h1 As Integer = m_capability.screenHeight - h2 - h3

            m_btns(0).Bounds = New Rectangle(x, 0, w, h1)
            '           m_btns(1).Bounds = New Rectangle(x, h1, w, h2)
            m_btns(1).Bounds = New Rectangle(x, h1 + h2, w, h3)
        End If
        m_btns(0).Text = "Borrar"
        m_btns(1).Text = "Firmar"
        m_btns(0).Click = AddressOf Me.btnClear_Click
        m_btns(1).Click = AddressOf Me.btnOK_Click

        ' Disable color if the STU-520 bulk driver isn't installed.
        ' This isn't necessary, but uploading colour images with out the driver
        ' is very slow.

        ' Calculate the encodingMode that will be used to update the image
        Dim idP As UShort = m_tablet.getProductId
        Dim encodingFlag As wgssSTU.encodingFlag = CType(protocolHelper.simulateEncodingFlag(idP, 0), wgssSTU.encodingFlag)
        Dim useColor As Boolean = False
        If (encodingFlag And (wgssSTU.encodingFlag.EncodingFlag_16bit Or wgssSTU.encodingFlag.EncodingFlag_24bit)) <> 0 Then
            If m_tablet.supportsWrite Then
                useColor = True
            End If
        End If
        If (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_24bit) <> 0 Then
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_24bit_Bulk
        ElseIf (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_16bit) <> 0 Then
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_16bit_Bulk
        Else
            'assumes 1bit is available
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_1bit
        End If

        ' Size the bitmap to the size of the LCD screen.
        ' This application uses the same bitmap for both the screen and client (window).
        ' However, at high DPI, this bitmap will be stretch and it would be better to 
        ' create individual bitmaps for screen and client at native resolutions.

        m_bitmap = New Bitmap(m_capability.screenWidth, m_capability.screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        Dim gfx As Graphics = Graphics.FromImage(m_bitmap)
        gfx.Clear(Color.White)

        'Uses pixels for units as DPI won't be accurate for tablet LCD.
        Dim font As Font = New Font(FontFamily.GenericSansSerif, m_btns(0).Bounds.Height / 2.0!, GraphicsUnit.Pixel)
        Dim sf As StringFormat = New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        If useColor Then
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
        Else
            ' Anti-aliasing should be turned off for monochrome devices.
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel
        End If

        ' Draw the buttons
        Dim i As Integer
        '****
        Dim iFile As String = System.Environment.CurrentDirectory & "\" & "04.jpg"
        Dim m_image As Image = Image.FromFile(iFile)
        gfx.DrawImage(m_image, 0, 0)
        '****
        For i = 0 To m_btns.Length - 1
            If useColor Then
                If i = 0 Then
                    gfx.FillRectangle(Brushes.Red, m_btns(i).Bounds)
                ElseIf i = 1 Then
                    gfx.FillRectangle(Brushes.Green, m_btns(i).Bounds)
                Else
                    gfx.FillRectangle(Brushes.LightGray, m_btns(i).Bounds)
                End If
            End If
            gfx.DrawRectangle(Pens.Black, m_btns(i).Bounds)
            gfx.DrawString(m_btns(i).Text, font, Brushes.Black, m_btns(i).Bounds, sf)
        Next
        gfx.Dispose()
        font.Dispose()

        ' Finally, use this bitmap for the window background.
        Me.BackgroundImage = m_bitmap
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Now the bitmap has been created, it needs to be converted to device-native
        ' format.
        ' Unfortunately it is not possible for the native COM component to
        ' understand .NET bitmaps. We have therefore convert the .NET bitmap
        ' into a memory blob that will be understood by COM.

        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        m_bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        m_bitmapData = CType(protocolHelper.resizeAndFlatten(stream.ToArray, 0, 0, CType(m_bitmap.Width, UInteger), CType(m_bitmap.Height, UInteger), m_capability.screenWidth, m_capability.screenHeight, CType(m_encodingMode, Byte), wgssSTU.Scale.Scale_Fit, 0, 0), Byte())
        protocolHelper = Nothing
        stream.Dispose()

        ' If you wish to further optimize image transfer, you can compress the image using 
        ' the Zlib algorithm.

        Dim useZlibCompression As Boolean = False
        If (Not useColor AndAlso useZlibCompression) Then
            ' m_bitmapData = compress_using_zlib(m_bitmapData); // insert compression here!
            m_encodingMode = (m_encodingMode Or wgssSTU.encodingMode.EncodingMode_Zlib)
        End If

        'Calculate the size and cache the inking pen.
        Dim s As SizeF = Me.AutoScaleDimensions
        Dim inkWidthMM As Single = 0.7F
        m_penInk = New Pen(Color.Black, inkWidthMM / 25.4! * ((s.Width + s.Height) / 2.0!))
        m_penInk.EndCap = System.Drawing.Drawing2D.LineCap.Round
        m_penInk.LineJoin = System.Drawing.Drawing2D.LineJoin.Round

        ' Add the delagate that receives pen data.
        AddHandler m_tablet.onPenData, AddressOf Me.onPenData
        AddHandler m_tablet.onGetReportException, AddressOf Me.onGetReportException

        'Initialize the screen
        clearScreen()

        ' Enable the pen data on the screen (if not already)
        m_tablet.setInkingMode(1)
    End Sub

    Private Sub FrmFirmaCaptura_Datos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LblID.Text = FrmNotificador.LblID.Text
    End Sub
End Class