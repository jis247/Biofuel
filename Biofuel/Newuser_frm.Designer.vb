<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Newuser_frm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cpass_txt = New System.Windows.Forms.TextBox()
        Me.pass_txt = New System.Windows.Forms.TextBox()
        Me.user_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Save_btn = New System.Windows.Forms.Button()
        Me.Close_btn = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.GroupBox1.Controls.Add(Me.cpass_txt)
        Me.GroupBox1.Controls.Add(Me.pass_txt)
        Me.GroupBox1.Controls.Add(Me.user_txt)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(130, 221)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 164)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cpass_txt
        '
        Me.cpass_txt.Location = New System.Drawing.Point(196, 110)
        Me.cpass_txt.Name = "cpass_txt"
        Me.cpass_txt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.cpass_txt.Size = New System.Drawing.Size(100, 20)
        Me.cpass_txt.TabIndex = 5
        '
        'pass_txt
        '
        Me.pass_txt.Location = New System.Drawing.Point(196, 71)
        Me.pass_txt.Name = "pass_txt"
        Me.pass_txt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.pass_txt.Size = New System.Drawing.Size(100, 20)
        Me.pass_txt.TabIndex = 4
        '
        'user_txt
        '
        Me.user_txt.Location = New System.Drawing.Point(196, 33)
        Me.user_txt.Name = "user_txt"
        Me.user_txt.Size = New System.Drawing.Size(100, 20)
        Me.user_txt.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Confirm Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(91, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(118, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User"
        '
        'Save_btn
        '
        Me.Save_btn.BackColor = System.Drawing.SystemColors.Control
        Me.Save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Save_btn.Location = New System.Drawing.Point(299, 391)
        Me.Save_btn.Name = "Save_btn"
        Me.Save_btn.Size = New System.Drawing.Size(75, 23)
        Me.Save_btn.TabIndex = 1
        Me.Save_btn.Text = "SAVE"
        Me.Save_btn.UseVisualStyleBackColor = False
        '
        'Close_btn
        '
        Me.Close_btn.BackColor = System.Drawing.SystemColors.Control
        Me.Close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Close_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Close_btn.Location = New System.Drawing.Point(390, 391)
        Me.Close_btn.Name = "Close_btn"
        Me.Close_btn.Size = New System.Drawing.Size(75, 23)
        Me.Close_btn.TabIndex = 2
        Me.Close_btn.Text = "CLOSE"
        Me.Close_btn.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Biofuel.My.Resources.Resources.homepageheadline
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(130, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(344, 203)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Newuser_frm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Biofuel.My.Resources.Resources.Textured_Bamboo_Wallpaper
        Me.ClientSize = New System.Drawing.Size(604, 442)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Close_btn)
        Me.Controls.Add(Me.Save_btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Newuser_frm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New User"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cpass_txt As TextBox
    Friend WithEvents pass_txt As TextBox
    Friend WithEvents user_txt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Save_btn As Button
    Friend WithEvents Close_btn As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
