'Title                  Training Funds Tracker
'Purpose                To help keep track of funds available for
'                       Training
'Created                December 2009
'Last Updated           Updated April 2017


Option Explicit On
Imports System.Globalization

Public Class frmMain

    Private title As String = "Training Funds Tracker"
    Private newDailyBalance As Decimal
    Private tempBankBal As Decimal
    Private previousBalance As Decimal
    Private fileLocation As String = "C:\Training\"
    Private bankPath As String = "C:\Training\training.txt"
    Private heading As String = "Date Entered" & Strings.Space(5) & "Type" & Strings.Space(7) & "Payee" & Strings.Space(22) & _
                "Debit(-)" & Strings.Space(7) & "Credit(+)" & Strings.Space(7) & "Balance"


    Private Sub mainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'makes the preview txt box read only
        Me.txtPreview.ReadOnly = True

        'puts numeric values in the credit and debit txt boxes
        Me.txtDebit.Text = "0.00"
        Me.txtCredit.Text = "0.00"

        'loads the typeComboBox
        Me.cmboxType.Items.Add("[Enter No.]")
        Me.cmboxType.Items.Add("ATM")
        Me.cmboxType.Items.Add("Debit")
        Me.cmboxType.Items.Add("Dep")
        Me.cmboxType.Items.Add("Wthdrw")
        Me.cmboxType.Items.Add("Txfr")

        Me.cmboxType.SelectedIndex = 0


        'initializes variables
        Dim button As DialogResult


        'varifies the training.txt exists else asks if the user wants to create it
        If My.Computer.FileSystem.FileExists(fileLocation & "training.txt") Then
            Me.lblPrevBal.Text = My.Computer.FileSystem.ReadAllText(fileLocation & "training.txt")
            Me.lblNewBal.Text = "0.00"

            'pulls heading information from text file if the file exists, otherwise the program
            'recongnizes it as a new project
            If My.Computer.FileSystem.FileExists(fileLocation & "trainingrun.txt") Then
                Dim text As String
                Dim nameIndex As Integer 'numer of chars accessed or what you are putting in txt box
                Dim name As String  'variable to put accessed text into
                Dim NewLineIndex As Integer 'length of Line
                Dim colonIndex As Integer 'location of each colon on line 
                Dim dashIndex As Integer 'location of the dash between dates
                Dim location As String
                Dim startDate As String
                Dim endDate As String

                'read entire file into text variable for string manipulation
                text = My.Computer.FileSystem.ReadAllText(fileLocation & "trainingrun.txt")


                ' read name of training ---------------------------------------
                NewLineIndex = text.IndexOf(ControlChars.NewLine, nameIndex)
                colonIndex = text.IndexOf(":", nameIndex)

                name = text.Substring(colonIndex + 1, NewLineIndex - colonIndex)
                name = name.TrimStart(" ")

                nameIndex = NewLineIndex + 2
                NewLineIndex = text.IndexOf(ControlChars.NewLine, nameIndex)

                Me.lblName.Text = name
                Me.lblName.ForeColor = Color.Maroon


                ' read loction ------------------------------------------------
                NewLineIndex = text.IndexOf(ControlChars.NewLine, nameIndex)
                colonIndex = text.IndexOf(":", nameIndex)

                location = text.Substring(colonIndex + 1, NewLineIndex - colonIndex)
                location = location.TrimStart(" ")

                nameIndex = NewLineIndex + 2
                NewLineIndex = text.IndexOf(ControlChars.NewLine, nameIndex)

                Me.lblLocation.Text = location
                Me.lblLocation.ForeColor = Color.Maroon


                ' read dates ---------------------------------------------------
                NewLineIndex = text.IndexOf(ControlChars.NewLine, nameIndex)
                colonIndex = text.IndexOf(":", nameIndex)
                dashIndex = text.IndexOf("-", nameIndex)

                startDate = text.Substring(colonIndex + 1, (dashIndex - colonIndex) - 1)
                startDate = startDate.TrimStart(" ")

                Me.lblStartDate.Text = startDate
                Me.lblStartDate.ForeColor = Color.Maroon

                endDate = text.Substring(dashIndex, NewLineIndex - dashIndex)
                endDate = endDate.TrimStart("-", " ")

                Me.lblEndDate.Text = endDate
                Me.lblEndDate.ForeColor = Color.Maroon

            End If


        Else 'sets up the Training folder and creates the training.txt file
            button = MessageBox.Show _
            ("The current training file does not exist.  This is your bank, would you like to create it?", _
            title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            'declares another button result and asks the user to enter a beginning balance
            If button = DialogResult.Yes Then

                'loops till the user enters a beginning balance and verifies it is numeric
                Do Until IsNumeric(Me.lblPrevBal.Text) And Me.lblPrevBal.Text <> String.Empty
                    tempBankBal = Convert.ToDecimal(InputBox("Please enter starting balance.", title, "0.00"))
                    Me.lblPrevBal.Text = tempBankBal.ToString("N2") 'turns inmput box to 2 placed decimal string
                    If Not IsNumeric(Me.lblPrevBal.Text) Then
                        MessageBox.Show("Number must be numeric.", title, MessageBoxButtons.OK)
                    End If
                Loop

                'Opens the trainingID form for basic info
                trainingIDForm.ShowDialog()

                Me.Show()
                Me.txtPreview.Text = "Ready"
                Me.lblNewBal.Text = "0.00"
                Me.lblName.Text = "New Project"
                Me.lblName.ForeColor = Color.Blue


                'user has decided not to enter a intial balance and closes the form
            Else : button = Windows.Forms.DialogResult.No

                Me.Close()

            End If
        End If

        'prevents accidental entries till previewed.
        Me.btnApply.Enabled = False

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        ClearForm()

    End Sub

    Private Sub btnCalc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalc.Click

        PreviewCalculations()

    End Sub

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click

        ApplyCalculation()

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        CloseApp()

    End Sub

    Private Sub cmboxType_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmboxType.Enter
        Me.cmboxType.SelectAll()
    End Sub

    Private Sub txtPayee_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayee.Enter
        Me.txtPayee.SelectAll()

    End Sub

    Private Sub txtDebit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDebit.Enter
        Me.txtDebit.SelectAll()

    End Sub

    Private Sub txtCredit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredit.Enter
        Me.txtCredit.SelectAll()

    End Sub

    Private Sub cmboxType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmboxType.GotFocus
        Me.cmboxType.BackColor = Color.LightBlue
        Me.txtPayee.BackColor = Color.White
        Me.txtDebit.BackColor = Color.White
        Me.txtCredit.BackColor = Color.White
    End Sub

    Private Sub txtPayee_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayee.GotFocus
        Me.cmboxType.BackColor = Color.White
        Me.txtPayee.BackColor = Color.LightBlue
        Me.txtDebit.BackColor = Color.White
        Me.txtCredit.BackColor = Color.White
    End Sub

    Private Sub txtDebit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDebit.GotFocus
        Me.cmboxType.BackColor = Color.White
        Me.txtPayee.BackColor = Color.White
        Me.txtDebit.BackColor = Color.LightBlue
        Me.txtCredit.BackColor = Color.White
    End Sub

    Private Sub txtCredit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredit.GotFocus
        Me.cmboxType.BackColor = Color.White
        Me.txtPayee.BackColor = Color.White
        Me.txtDebit.BackColor = Color.White
        Me.txtCredit.BackColor = Color.LightBlue
    End Sub

    Private Sub dtpEntryDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEntryDate.GotFocus
        Me.cmboxType.BackColor = Color.White
        Me.txtPayee.BackColor = Color.White
        Me.txtDebit.BackColor = Color.White
        Me.txtCredit.BackColor = Color.White
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click

        CloseApp()

    End Sub

    Private Sub ActivitySheetToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActivitySheetToolStripMenuItem.Click

        If My.Computer.FileSystem.FileExists(fileLocation & "trainingrun.txt") And My.Computer.FileSystem.FileExists(bankPath) Then
            Dim proc As New System.Diagnostics.Process
            proc.StartInfo.FileName = "notepad.exe"
            proc.StartInfo.Arguments = "C:\Training\trainingrun.txt"
            proc.Start()

        Else
            MessageBox.Show("File is in the process of being created on first run.  Please make a calculation and press reply before re-opening.", _
            title, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Show()
            Me.dtpEntryDate.Focus()

        End If
    End Sub

    Private Sub BankFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BankFileToolStripMenuItem.Click

        If My.Computer.FileSystem.FileExists(bankPath) And My.Computer.FileSystem.FileExists(fileLocation & "trainingrun.txt") Then
            Dim proc As New System.Diagnostics.Process
            proc.StartInfo.FileName = "notepad.exe"
            proc.StartInfo.Arguments = "C:\Training\training.txt"
            proc.Start()

        Else
            MessageBox.Show("File is in the process of being created on first run.  Please make a calculation and press reply before re-opening.", _
            title, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Show()
            Me.dtpEntryDate.Focus()

        End If
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearFormToolStripMenuItem.Click

        ClearForm()

    End Sub

    Private Sub CalculateToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalculateToolStripMenuItem.Click

        PreviewCalculations()

    End Sub

    Private Sub ApplyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ApplyToolStripMenuItem.Click

        ApplyCalculation()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click

        trainingAboutBox.ShowDialog()


    End Sub

    Private Sub cmboxType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmboxType.TextChanged

        If Me.cmboxType.SelectedIndex = 0 Or Me.cmboxType.SelectedIndex = 5 Then
            Me.txtDebit.Enabled = True
            Me.txtCredit.Enabled = True

        ElseIf Me.cmboxType.SelectedIndex = 1 Or Me.cmboxType.SelectedIndex = 2 Or _
        Me.cmboxType.SelectedIndex = 4 Then
            Me.txtDebit.Enabled = True
            Me.txtCredit.Enabled = False

        ElseIf Me.cmboxType.SelectedIndex = 3 Then
            Me.txtDebit.Enabled = False
            Me.txtCredit.Enabled = True
        End If

    End Sub

    Private Sub ReadMeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReadMeToolStripMenuItem.Click

        If My.Computer.FileSystem.FileExists("C:\Training\trainingreadme.txt") Then
            Dim proc As New System.Diagnostics.Process
            proc.StartInfo.FileName = "notepad.exe"
            proc.StartInfo.Arguments = "C:\Training\trainingreadme.txt"
            proc.Start()

        Else
            MessageBox.Show("File was not copied manually at install.  Check with deloyment file for trainingreadme.txt file and copy to (C:\Training\) root directory.", _
            title, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Show()
            Me.dtpEntryDate.Focus()

        End If


    End Sub

    Private Sub InfoFormToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        trainingIDForm.ShowDialog()

    End Sub

    Private Sub ClearForm()

        'delcare proceedure variables
        Dim button As DialogResult

        button = MessageBox.Show("Do you wish to add to the new balance to the bank?", title, MessageBoxButtons.YesNo, _
        MessageBoxIcon.Question)

        If button = Windows.Forms.DialogResult.Yes Then
            'declare block variables
            Dim curdate As String
            Dim type As String
            Dim payee As String
            Dim previous As String 'formally line2


            'make calculations
            newDailyBalance = Convert.ToDecimal(Me.lblNewBal.Text)
            newDailyBalance = Math.Round(newDailyBalance, 2)
            previousBalance = newDailyBalance
            newDailyBalance = 0D
            Me.lblPrevBal.Text = Convert.ToString(previousBalance)

            'convert data
            type = Me.cmboxType.Text
            payee = Me.txtPayee.Text
            curdate = Me.dtpEntryDate.Text
            previous = Convert.ToString(previousBalance)

            'Write the current balance to the txt file
            If button = Windows.Forms.DialogResult.Yes And My.Computer.FileSystem.FileExists(fileLocation & _
            "trainingrun.txt") And My.Computer.FileSystem.FileExists(bankPath) Then
                My.Computer.FileSystem.WriteAllText(bankPath, previous, False)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", _
                curdate & Strings.Space(7) & _
                type.PadRight(7, " ") & Strings.Space(4) & _
                payee.PadRight(20, " ") & Strings.Space(7) & _
                Me.txtDebit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Me.txtCredit.Text.PadRight(6, " ") & Strings.Space(10) & _
                Convert.ToString(previousBalance).PadLeft(6, " ") & ControlChars.NewLine, True)

                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", "".PadLeft(94, "-") & _
                ControlChars.NewLine, True)



            Else
                'set up for the first run
                My.Computer.FileSystem.CreateDirectory("C:\Training")
                My.Computer.FileSystem.WriteAllText(bankPath, previous, False)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", heading & ControlChars.NewLine & _
                "------------" & Strings.Space(5) & _
                "-----" & Strings.Space(6) & _
                "-------" & Strings.Space(20) & _
                "----------" & Strings.Space(5) & _
                "----------" & Strings.Space(6) & _
                "--------" & ControlChars.NewLine, True)

                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", _
                curdate & Strings.Space(7) & _
                type.PadRight(7, " ") & Strings.Space(4) & _
                payee.PadRight(20, " ") & Strings.Space(7) & _
                Me.txtDebit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Me.txtCredit.Text.PadRight(6, " ") & Strings.Space(10) & _
                Convert.ToString(previousBalance).PadLeft(6, " ") & ControlChars.NewLine, True)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", "".PadLeft(94, "-") & _
                ControlChars.NewLine, True)


            End If

            'clears and returns to form

            Initialize_Form()


        Else
            'clears and returns to form

            Initialize_Form()



        End If

    End Sub

    Private Sub ApplyCalculation()

        'saves current balance to the text file

        'declares variables
        Dim addTextButton As DialogResult
        Dim returnButton As DialogResult

        'program asks user if they would like to save the current transaction
        addTextButton = MessageBox.Show("Do you wish to add the new daily balance to the bank?", title, _
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If addTextButton = Windows.Forms.DialogResult.Yes Then

            'make calculations
            newDailyBalance = Convert.ToDecimal(Me.lblNewBal.Text)
            newDailyBalance = Math.Round(newDailyBalance, 2)
            previousBalance = newDailyBalance
            newDailyBalance = 0D
            Me.lblPrevBal.Text = Convert.ToString(previousBalance)

            'declare text writing variables
            Dim previous As String ' formally line
            Dim curdate As String
            Dim type As String
            Dim payee As String

            'convert the data from the lblPrevBal and store it in the previous variable
            previous = Convert.ToString(previousBalance)
            type = Me.cmboxType.Text
            curdate = Me.dtpEntryDate.Text
            payee = Me.txtPayee.Text

            'If the training file exists, the prog writes current balance to the text file
            If My.Computer.FileSystem.FileExists(fileLocation & _
            "trainingrun.txt") And My.Computer.FileSystem.FileExists(bankPath) Then
                My.Computer.FileSystem.WriteAllText(bankPath, previous, False)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", _
                curdate & Strings.Space(7) & _
                type.PadRight(7, " ") & Strings.Space(4) & _
                payee.PadRight(20, " ") & Strings.Space(7) & _
                Me.txtDebit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Me.txtCredit.Text.PadRight(6, " ") & Strings.Space(10) & _
                Convert.ToString(previousBalance).PadLeft(6, " ") & ControlChars.NewLine, True)

                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", "".PadLeft(94, "-") & _
                ControlChars.NewLine, True)

                MessageBox.Show("Processing complete. The form will be cleared.", _
                                title, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Initialize_Form()


            Else 'set up for the first run
                My.Computer.FileSystem.CreateDirectory("C:\Training")
                My.Computer.FileSystem.WriteAllText(bankPath, previous, False)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", heading & ControlChars.NewLine & _
                "------------" & Strings.Space(5) & _
                "-----" & Strings.Space(6) & _
                "-------" & Strings.Space(20) & _
                "----------" & Strings.Space(5) & _
                "----------" & Strings.Space(6) & _
                "--------" & ControlChars.NewLine, True)

                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", _
                curdate & Strings.Space(7) & _
                type.PadRight(7, " ") & Strings.Space(4) & _
                payee.PadRight(20, " ") & Strings.Space(7) & _
                Me.txtDebit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Me.txtCredit.Text.PadRight(6, " ") & Strings.Space(10) & _
                Convert.ToString(previousBalance).PadLeft(6, " ") & ControlChars.NewLine, True)
                My.Computer.FileSystem.WriteAllText(fileLocation & "trainingrun.txt", "".PadLeft(94, "-") & _
                ControlChars.NewLine, True)

                MessageBox.Show("Processing complete. The form will be cleared.", _
                                title, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Initialize_Form()


            End If


        Else 'If the user does not want to make a calculation, the program will ask if the user
            'wants to return to the program to perform another calculation
            'if the user does not, then the program will direct the user to exit
            returnButton = MessageBox.Show("Do you wan to make another calculation?", title, _
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If returnButton = Windows.Forms.DialogResult.Yes Then

                Initialize_Form()

            Else
                returnButton = Windows.Forms.DialogResult.No
                MessageBox.Show("No calculation will be made and the form will be reset. You may exit the program.", _
                title, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Initialize_Form()

            End If
        End If

    End Sub

    Private Sub PreviewCalculations()

        'Declare Procedure Level Variables for Calculations
        Dim isAdded As Boolean
        Dim isSubtracted As Boolean
        Dim credit As Decimal
        Dim debit As Decimal
        Dim calcTransactionBal As Decimal
        Dim previewBankBal As Decimal

        'Convert Input
        isAdded = Decimal.TryParse(Me.txtCredit.Text, credit)
        isSubtracted = Decimal.TryParse(Me.txtDebit.Text, debit)


        If IsNumeric(Me.txtCredit.Text) And IsNumeric(Me.txtDebit.Text) Then
            If isAdded And isSubtracted Then

                'Make calculations
                calcTransactionBal = credit - debit
                calcTransactionBal = Math.Round(calcTransactionBal, 2)
                newDailyBalance = Convert.ToDecimal(Me.lblPrevBal.Text) + calcTransactionBal
                Me.lblTransAction.Text = Convert.ToString(calcTransactionBal)
                Me.lblNewBal.Text = Convert.ToString(newDailyBalance)

                'fills preview pane (txtPreview)
                previewBankBal = calcTransactionBal + Convert.ToDecimal(Me.lblPrevBal.Text)
                Me.txtPreview.Text = "Preview of Entry to Activity Sheet:" & ControlChars.NewLine & _
                ControlChars.NewLine & _
                "Date Entered" & Strings.Space(6) & _
                "Type" & Strings.Space(10) & _
                "Payee" & Strings.Space(20) & _
                "Debit(-)" & Strings.Space(7) & _
                "Credit(+)" & Strings.Space(7) & _
                "Balance" & ControlChars.NewLine & _
                "------------" & Strings.Space(6) & _
                "----------" & Strings.Space(4) & _
                "----------------" & Strings.Space(9) & _
                "--------" & Strings.Space(7) & _
                "---------" & Strings.Space(7) & _
                "-------" & ControlChars.NewLine & _
                Me.dtpEntryDate.Text.PadRight(10, " ") & Strings.Space(8) & _
                Me.cmboxType.Text.PadRight(11, " ") & Strings.Space(3) & _
                Me.txtPayee.Text.PadRight(20, " ") & Strings.Space(5) & _
                Me.txtDebit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Me.txtCredit.Text.PadRight(6, " ") & Strings.Space(9) & _
                Convert.ToString(previewBankBal).PadLeft(6)


            Else
                'show error message and highlight the problematic area
                MessageBox.Show("Number for calculations must be numeric.", title, _
                MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Not isSubtracted Then

                    Me.txtDebit.Focus()

                ElseIf Not isAdded Then

                    Me.txtCredit.Focus()

                End If

            End If


        Else 'show error message and highlight the problematic area
            MessageBox.Show("Number for calculations must be numeric.", title, _
            MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Not IsNumeric(Me.txtDebit.Text) Then

                Me.txtDebit.Focus()

            ElseIf Not IsNumeric(Me.txtCredit.Text) Then

                Me.txtCredit.Focus()

            End If


        End If

        'activates apply button
        Me.btnApply.Enabled = True

    End Sub

    Private Sub CloseApp()

        'closes form

        'declare variable
        Dim exitButton As DialogResult

        exitButton = MessageBox.Show("Are you sure that you are ready to exit?", title, _
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If exitButton = Windows.Forms.DialogResult.No Then


            'clears everything and returns to the form
            Initialize_Form()


        Else 'exits the program
            exitButton = Windows.Forms.DialogResult.Yes

            Me.Close()

        End If

    End Sub

    Sub Initialize_Form()

        'Clears and re-initializes everything
        Me.Show()
        Me.txtPayee.Clear()
        Me.lblNewBal.Text = "0.00"
        Me.lblTransAction.Text = ""
        Me.txtPreview.Text = "Ready"
        Me.cmboxType.SelectedIndex = 0
        Me.txtDebit.Text = "0.00"
        Me.txtCredit.Text = "0.00"
        newDailyBalance = 0D
        Me.dtpEntryDate.Focus()

        Me.btnApply.Enabled = False

    End Sub

    Private Sub mainForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.FormClosing


        'To delete trainingrun.txt if an initial transation is not put in the file
        'If both are missing due to not completing the input box, then the app just closes
        If Not My.Computer.FileSystem.FileExists(fileLocation & "training.txt") And _
            Not My.Computer.FileSystem.FileExists(fileLocation & "trainingrun.txt") Then

            Exit Sub
        Else
            If Not My.Computer.FileSystem.FileExists(fileLocation & "training.txt") Then
                My.Computer.FileSystem.DeleteFile(fileLocation & "trainingrun.txt")

            End If

        End If

    End Sub

    Private Sub btnReconcile_Click(sender As Object, e As EventArgs) Handles btnReconcile.Click

        Me.Hide()
        frmReconcile.Show()

    End Sub
End Class
