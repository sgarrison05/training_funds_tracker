Public Class frmReconcile
    Dim path As String = "C:\Training\trainingrun.txt"
    Dim readtxt As String
    Dim entry As String
    Dim newLineIndex As Integer
    Dim entryIndex As Integer
    Dim entryYr As String
    Dim temp As String


    Private Sub frmReconcile_Load(sender As Object, e As EventArgs) Handles Me.Load

        'clear out variables for recalculation
        readtxt = ""
        entry = ""
        entryYr = ""
        newLineIndex = 0
        entryIndex = 0
        temp = ""

        Me.liboxReconcile.Items.Clear()

        'checks for existing training folder to pull trainingrun.txt from 
        If My.Computer.FileSystem.FileExists(path) Then
            readtxt = My.Computer.FileSystem.ReadAllText(path)

            'saves copy of orig on load
            temp = readtxt

            'primer for first read
            newLineIndex = readtxt.IndexOf(ControlChars.NewLine, entryIndex)

            Do Until newLineIndex = -1

                'get each line
                entry = readtxt.Substring(entryIndex, newLineIndex - entryIndex)

                'finds every line with a year in it
                entryYr = entry.Contains("/")

                'if found, it adds it to the listbox
                If entryYr = True Then
                    liboxReconcile.Items.Add(entry)
                End If

                'updates every entryIndex with the next line
                entryIndex = newLineIndex + 2
                newLineIndex = readtxt.IndexOf(ControlChars.NewLine, entryIndex)

            Loop


        End If

    End Sub


    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Me.Close()
        frmMain.Show()


    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        Me.liboxReconcile.Items.Clear()

    End Sub
End Class