<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReconcile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.liboxReconcile = New System.Windows.Forms.ListBox()
        Me.gpboxOptions = New System.Windows.Forms.GroupBox()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnReconcile = New System.Windows.Forms.Button()
        Me.gpboxOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'liboxReconcile
        '
        Me.liboxReconcile.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.liboxReconcile.FormattingEnabled = True
        Me.liboxReconcile.ItemHeight = 15
        Me.liboxReconcile.Location = New System.Drawing.Point(12, 56)
        Me.liboxReconcile.Name = "liboxReconcile"
        Me.liboxReconcile.Size = New System.Drawing.Size(685, 499)
        Me.liboxReconcile.TabIndex = 0
        '
        'gpboxOptions
        '
        Me.gpboxOptions.BackColor = System.Drawing.Color.Green
        Me.gpboxOptions.Controls.Add(Me.btnReturn)
        Me.gpboxOptions.Controls.Add(Me.btnClear)
        Me.gpboxOptions.Controls.Add(Me.btnReconcile)
        Me.gpboxOptions.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.gpboxOptions.Location = New System.Drawing.Point(731, 56)
        Me.gpboxOptions.Name = "gpboxOptions"
        Me.gpboxOptions.Size = New System.Drawing.Size(104, 222)
        Me.gpboxOptions.TabIndex = 1
        Me.gpboxOptions.TabStop = False
        Me.gpboxOptions.Text = "Options:"
        '
        'btnReturn
        '
        Me.btnReturn.ForeColor = System.Drawing.SystemColors.InfoText
        Me.btnReturn.Location = New System.Drawing.Point(9, 164)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(86, 42)
        Me.btnReturn.TabIndex = 2
        Me.btnReturn.Text = "Rtn to Main"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.ForeColor = System.Drawing.SystemColors.InfoText
        Me.btnClear.Location = New System.Drawing.Point(9, 96)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(86, 42)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnReconcile
        '
        Me.btnReconcile.ForeColor = System.Drawing.SystemColors.InfoText
        Me.btnReconcile.Location = New System.Drawing.Point(9, 28)
        Me.btnReconcile.Name = "btnReconcile"
        Me.btnReconcile.Size = New System.Drawing.Size(86, 42)
        Me.btnReconcile.TabIndex = 0
        Me.btnReconcile.Text = "Reconcile"
        Me.btnReconcile.UseVisualStyleBackColor = True
        '
        'frmReconcile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 579)
        Me.ControlBox = False
        Me.Controls.Add(Me.gpboxOptions)
        Me.Controls.Add(Me.liboxReconcile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmReconcile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reconcilliation Form : Training Funds Tracker"
        Me.gpboxOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents liboxReconcile As System.Windows.Forms.ListBox
    Friend WithEvents gpboxOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnReconcile As System.Windows.Forms.Button
End Class
