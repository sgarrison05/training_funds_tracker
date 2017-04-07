Public Class trainingIDForm

    Private rootfileLocation As String = "C:\Training\"
    Private activityFileLocation As String = "C:\Training\trainingrun.txt"
    Private bankFileLocation As String = "C:\Training\training.txt"


    Private Sub btnDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone.Click

        'declares variables
        Dim trainingname As String
        Dim location As String
        Dim startDate As String
        Dim endDate As String
        Dim jpoName As String


        'get input
        jpoName = Me.txtJPOName.Text
        trainingname = Me.txtTrainingName.Text
        location = Me.txtTrainingLocation.Text
        startDate = Me.dtpTrainingStart.Text
        endDate = Me.dtpTrainingEnd.Text

        'Enters Initial text to the trainingrun.txt file
        If My.Computer.FileSystem.DirectoryExists(rootfileLocation) Then
            My.Computer.FileSystem.WriteAllText(activityFileLocation, _
            "Name:" & Strings.Space(10) & trainingname & ControlChars.NewLine & "Location:" & _
            Strings.Space(6) & location & ControlChars.NewLine & "Dates:" & _
            Strings.Space(9) & startDate & Strings.Space(2) & "-" & _
            Strings.Space(2) & endDate & ControlChars.NewLine & "For Officer: " & _
            Strings.Space(2) & jpoName & ControlChars.NewLine & _
            ControlChars.NewLine, _
            True)
           

        Else
            'it creates the root directory tracking files
            'and the trainingrun.txt file with initial information
            My.Computer.FileSystem.CreateDirectory(rootfileLocation)
            My.Computer.FileSystem.WriteAllText(activityFileLocation, _
            "Name:" & Strings.Space(10) & trainingname & ControlChars.NewLine & "Location:" & _
            Strings.Space(6) & location & ControlChars.NewLine & "Dates:" & _
            Strings.Space(9) & startDate & Strings.Space(2) & "-" & _
            Strings.Space(2) & endDate & ControlChars.NewLine & "For Officer:" & _
            Strings.Space(2) & jpoName & ControlChars.NewLine & _
            ControlChars.NewLine, _
            True)
           

        End If

        'closes the form
        Me.Close()

    End Sub

    Private Sub trainingIDForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'sets focus to first text box
        Me.txtJPOName.Focus()

    End Sub
End Class