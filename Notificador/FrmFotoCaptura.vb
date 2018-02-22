Imports System
Imports System.Runtime
Imports System.Runtime.InteropServices

Public Class FrmFotoCaptura

    Const WM_CAP As Short = &H400S

    Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP + 10
    Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP + 11
    Const WM_CAP_EDIT_COPY As Integer = WM_CAP + 30

    Const WM_CAP_SET_PREVIEW As Integer = WM_CAP + 50
    Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP + 52
    Const WM_CAP_SET_SCALE As Integer = WM_CAP + 53
    Const WS_CHILD As Integer = &H40000000
    Const WS_VISIBLE As Integer = &H10000000
    Const SWP_NOMOVE As Short = &H2S
    Const SWP_NOSIZE As Short = 1
    Const SWP_NOZORDER As Short = &H4S
    Const HWND_BOTTOM As Short = 1

    Dim iDevice As Integer = 0 ' Current device ID
    Dim hHwnd As Integer ' Handle to preview window

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, _
        <MarshalAs(UnmanagedType.AsAny)> ByVal lParam As Object) As Integer

    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, _
        ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, _
        ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer

    Declare Function DestroyWindow Lib "user32" (ByVal hndw As Integer) As Boolean

    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" _
        (ByVal lpszWindowName As String, ByVal dwStyle As Integer, _
        ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, _
        ByVal nHeight As Short, ByVal hWndParent As Integer, _
        ByVal nID As Integer) As Integer

    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" (ByVal wDriver As Short, _
        ByVal lpszName As String, ByVal cbName As Integer, ByVal lpszVer As String, _
        ByVal cbVer As Integer) As Boolean

    Private Structure BITMAPINFOHEADER
        Dim biSize As Long
        Dim biWidth As Long
        Dim biHeight As Long
        Dim biPlanes As Integer
        Dim biBitCount As Integer
        Dim biCompression As Long
        Dim biSizeImage As Long
        Dim biXPelsPerMeter As Long
        Dim biYPelsPerMeter As Long
        Dim biClrUsed As Long
        Dim biClrImportant As Long
    End Structure

    Private Structure RGBQUAD
        Dim rgbBlue As Byte
        Dim rgbGreen As Byte
        Dim rgbRed As Byte
        Dim rgbReserved As Byte
    End Structure

    Private Structure BITMAPINFO
        Dim bmiHeader As BITMAPINFOHEADER
        Dim bmiColors As RGBQUAD
    End Structure

    Private Const WM_USER = &H400
    Private Const WM_CAP_SET_VIDEOFORMAT = (WM_USER + 45)
    'Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Any) As Long


    Private Sub ChangeTO640x480()

        'Dim tBI As BITMAPINFO
        'Dim retval As Long

        Dim VideoFormat As BITMAPINFO

        VideoFormat.bmiHeader.biWidth = 640
        VideoFormat.bmiHeader.biHeight = 480
        'VideoFormat.Header.Width = 640
        'VideoFormat.Header.Height = 480


        If SendMessage(hHwnd, WM_CAP_SET_VIDEOFORMAT, Len(VideoFormat), VideoFormat) Then
            MsgBox("Supported")
        Else
            MsgBox("Not supported")
        End If

    End Sub

    Private Sub FrmFotoCaptura_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If btnStop.Enabled Then
            ClosePreviewWindow()
        End If
    End Sub

    Private Sub FrmFotoCaptura_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDeviceList()
        If lstDevices.Items.Count > 0 Then
            btnStart.Enabled = True
            lstDevices.SelectedIndex = 0
            btnStart.Enabled = True
        Else
            lstDevices.Items.Add("no supported cameras")
            btnStart.Enabled = False
        End If

        ChangeTO640x480()

        btnStop.Enabled = False
        btnGuardar.Enabled = False
        picFoto.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub LoadDeviceList()
        Dim strName As String = Space(100)
        Dim strVer As String = Space(100)
        Dim bReturn As Boolean
        Dim x As Integer = 0

        ' 
        ' Load name of all avialable devices into the lstDevices
        '

        Do
            '
            '   Get Driver name and version
            '
            bReturn = capGetDriverDescriptionA(x, strName, 100, strVer, 100)

            '
            ' If there was a device add device name to the list
            '
            If bReturn Then lstDevices.Items.Add(strName.Trim)
            x += 1
        Loop Until bReturn = False
    End Sub

    Private Sub OpenPreviewWindow()
        Dim iHeight As Integer = picFoto.Height
        Dim iWidth As Integer = picFoto.Width

        '
        ' Open Preview window in picturebox
        '
        hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE Or WS_CHILD, 0, 0, 640, _
            480, picFoto.Handle.ToInt32, 0)

        '
        ' Connect to device
        '
        If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
            '
            'Set the preview scale
            '
            SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)

            '
            'Set the preview rate in milliseconds
            '
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)

            '
            'Start previewing the image from the camera
            '
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)

            '
            ' Resize window to fit in picturebox
            '
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, picFoto.Width, picFoto.Height, _
                    SWP_NOMOVE Or SWP_NOZORDER)

            btnGuardar.Enabled = True
            btnStop.Enabled = True
            btnStart.Enabled = False
        Else
            '
            ' Error connecting to device close window
            ' 
            DestroyWindow(hHwnd)

            btnGuardar.Enabled = False
        End If
    End Sub


    Private Sub ClosePreviewWindow()
        '
        ' Disconnect from device
        '
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)

        '
        ' close window
        '

        DestroyWindow(hHwnd)
    End Sub


    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        ClosePreviewWindow()
        btnGuardar.Enabled = False
        btnStart.Enabled = True
        btnStop.Enabled = False
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        iDevice = lstDevices.SelectedIndex
        OpenPreviewWindow()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Me.Width = 1366
        Me.Height = 768

        Dim data As IDataObject
        Dim bmap As Image

        '
        ' Copy image to clipboard
        '
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)

        '
        ' Get image from clipboard and convert it to a bitmap
        '
        data = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Image)
            picFoto.Image = bmap
            ClosePreviewWindow()
            btnGuardar.Enabled = False
            btnStop.Enabled = False
            btnStart.Enabled = True



        End If
    End Sub
End Class