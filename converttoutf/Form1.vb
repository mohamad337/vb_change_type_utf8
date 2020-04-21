Imports System.IO

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
        If RadioButton1.Checked = False Then
            ChangeFileEncoding(TextBox1.Text, TextBox2.Text, IO.SearchOption.TopDirectoryOnly)
            'For Each oFile In IO.Directory.GetFiles(TextBox1.Text, "*.*", IO.SearchOption.AllDirectories)
            'IO.File.WriteAllText(oFile, IO.File.ReadAllText(oFile), System.Text.Encoding.UTF8)
            'ListBox1.Items.Add("<br>Saved #:  - <i>Encoding ")
            'Next
        Else
            Dim DirList As New ArrayList
            GetDirectories(TextBox1.Text, DirList)
            For Each direcc In DirList
                ListBox1.Items.Add((direcc.ToString).Replace(TextBox1.Text, "/"))
                ChangeFileEncoding(direcc.ToString, TextBox2.Text, IO.SearchOption.TopDirectoryOnly)
            Next

            ChangeFileEncoding(TextBox1.Text, TextBox2.Text, IO.SearchOption.TopDirectoryOnly)

        End If
    End Sub

    Sub GetDirectories(ByVal StartPath As String, ByRef DirectoryList As ArrayList)
        Dim Dirs() As String = Directory.GetDirectories(StartPath)
        DirectoryList.AddRange(Dirs)
        For Each Dir As String In Dirs
            GetDirectories(Dir, DirectoryList)
        Next
        ' For Each item In DirectoryList
        'ListBox1.Items.Add(item)
        'Next
    End Sub

    Function ChangeFileEncoding(ByVal pPathFolder As String, ByVal pExtension As String, ByVal pDirOption As IO.SearchOption) As Integer

        Dim Counter As Integer
        Dim s As String
        Dim reader As IO.StreamReader
        Dim gEnc As System.Text.Encoding
        Dim direc As IO.DirectoryInfo = New IO.DirectoryInfo(pPathFolder)
        For Each fi As IO.FileInfo In direc.GetFiles(pExtension, pDirOption)
            s = ""
            reader = New IO.StreamReader(fi.FullName, System.Text.Encoding.Default, True)
            s = reader.ReadToEnd
            gEnc = reader.CurrentEncoding
            reader.Close()

            If (gEnc.EncodingName <> System.Text.Encoding.UTF8.EncodingName) Then
                s = IO.File.ReadAllText(fi.FullName, gEnc)
                IO.File.WriteAllText(fi.FullName, s, System.Text.Encoding.UTF8)
                Counter += 1
                ListBox1.Items.Add("<br>Saved #" & Counter & ": " & fi.FullName & " - <i>Encoding was: " & gEnc.EncodingName & "</i>")
            End If
        Next

        Return Counter
    End Function
End Class
