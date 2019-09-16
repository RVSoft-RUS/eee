<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOptions
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOptions))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelRam = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelComment = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabelTXT = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LabelMas = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabelNeu = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxRele = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxLamp = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxResist = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxNomPred = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxUdef = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelRam)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.LabelComment)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.LabelTXT)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label30)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.LabelMas)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.LabelNeu)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(204, 192)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Цвета"
        '
        'LabelRam
        '
        Me.LabelRam.AutoSize = True
        Me.LabelRam.BackColor = System.Drawing.Color.Red
        Me.LabelRam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelRam.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelRam.Location = New System.Drawing.Point(153, 165)
        Me.LabelRam.Name = "LabelRam"
        Me.LabelRam.Size = New System.Drawing.Size(33, 15)
        Me.LabelRam.TabIndex = 12
        Me.LabelRam.Text = "        "
        Me.ToolTip1.SetToolTip(Me.LabelRam, "Изменить цвет")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Цвет рамки"
        '
        'LabelComment
        '
        Me.LabelComment.AutoSize = True
        Me.LabelComment.BackColor = System.Drawing.Color.Red
        Me.LabelComment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelComment.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelComment.Location = New System.Drawing.Point(153, 140)
        Me.LabelComment.Name = "LabelComment"
        Me.LabelComment.Size = New System.Drawing.Size(33, 15)
        Me.LabelComment.TabIndex = 10
        Me.LabelComment.Text = "        "
        Me.ToolTip1.SetToolTip(Me.LabelComment, "Изменить цвет")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Цвет комментариев"
        '
        'LabelTXT
        '
        Me.LabelTXT.AutoSize = True
        Me.LabelTXT.BackColor = System.Drawing.Color.Red
        Me.LabelTXT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTXT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelTXT.Location = New System.Drawing.Point(153, 116)
        Me.LabelTXT.Name = "LabelTXT"
        Me.LabelTXT.Size = New System.Drawing.Size(33, 15)
        Me.LabelTXT.TabIndex = 8
        Me.LabelTXT.Text = "        "
        Me.ToolTip1.SetToolTip(Me.LabelTXT, "Изменить цвет")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Цвет текста"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Red
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label30.Location = New System.Drawing.Point(153, 92)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(33, 15)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "        "
        Me.ToolTip1.SetToolTip(Me.Label30, "Изменить цвет")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Сигнал ""+30"""
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Red
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label15.Location = New System.Drawing.Point(153, 67)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(33, 15)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "        "
        Me.ToolTip1.SetToolTip(Me.Label15, "Изменить цвет")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Сигнал ""+15"""
        '
        'LabelMas
        '
        Me.LabelMas.AutoSize = True
        Me.LabelMas.BackColor = System.Drawing.Color.Red
        Me.LabelMas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelMas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelMas.Location = New System.Drawing.Point(153, 43)
        Me.LabelMas.Name = "LabelMas"
        Me.LabelMas.Size = New System.Drawing.Size(33, 15)
        Me.LabelMas.TabIndex = 2
        Me.LabelMas.Text = "        "
        Me.ToolTip1.SetToolTip(Me.LabelMas, "Изменить цвет")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Сигнал ""Масса"""
        '
        'LabelNeu
        '
        Me.LabelNeu.AutoSize = True
        Me.LabelNeu.BackColor = System.Drawing.Color.Red
        Me.LabelNeu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelNeu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelNeu.Location = New System.Drawing.Point(153, 20)
        Me.LabelNeu.Name = "LabelNeu"
        Me.LabelNeu.Size = New System.Drawing.Size(33, 15)
        Me.LabelNeu.TabIndex = 1
        Me.LabelNeu.Text = "        "
        Me.ToolTip1.SetToolTip(Me.LabelNeu, "Изменить цвет")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Нейтральный провод"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.TextBoxRele)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.TextBoxLamp)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.TextBoxResist)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.TextBoxNomPred)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.TextBoxUdef)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(222, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(258, 192)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Значения по умолчанию"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(9, 161)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(243, 20)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "Значения полей основной надписи"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(186, 137)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(66, 20)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.Text = "1000"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 140)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(162, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Время отключения кнопки, мс"
        '
        'TextBoxRele
        '
        Me.TextBoxRele.Location = New System.Drawing.Point(186, 113)
        Me.TextBoxRele.Name = "TextBoxRele"
        Me.TextBoxRele.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxRele.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 116)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(179, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Сопротивление катушки реле, Ом"
        '
        'TextBoxLamp
        '
        Me.TextBoxLamp.Location = New System.Drawing.Point(186, 89)
        Me.TextBoxLamp.Name = "TextBoxLamp"
        Me.TextBoxLamp.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxLamp.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(144, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Сопротивление лампы, Ом"
        '
        'TextBoxResist
        '
        Me.TextBoxResist.Location = New System.Drawing.Point(186, 66)
        Me.TextBoxResist.Name = "TextBoxResist"
        Me.TextBoxResist.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxResist.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 69)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(174, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Сопротивление потребителя, Ом"
        '
        'TextBoxNomPred
        '
        Me.TextBoxNomPred.Location = New System.Drawing.Point(186, 40)
        Me.TextBoxNomPred.Name = "TextBoxNomPred"
        Me.TextBoxNomPred.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxNomPred.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(151, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Номинал предохранителя, А"
        '
        'TextBoxUdef
        '
        Me.TextBoxUdef.Location = New System.Drawing.Point(186, 17)
        Me.TextBoxUdef.Name = "TextBoxUdef"
        Me.TextBoxUdef.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxUdef.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Напряжение батареи, В"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(11, 293)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(338, 17)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "Использовать правую кнопку мыши для отмены (аналог Esc)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Enabled = False
        Me.CheckBox2.Location = New System.Drawing.Point(10, 19)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(267, 17)
        Me.CheckBox2.TabIndex = 3
        Me.CheckBox2.Text = "Сохранять в файле размеры и положение окна"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(355, 328)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 28)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Сохранить"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(377, 296)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 26)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Отмена"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBox3)
        Me.GroupBox3.Controls.Add(Me.CheckBox2)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 210)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(469, 77)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Файл проекта"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(10, 42)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(367, 17)
        Me.CheckBox3.TabIndex = 4
        Me.CheckBox3.Text = "При открытии файла восстанавливать размеры и положение окна"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Checked = True
        Me.CheckBox4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox4.Location = New System.Drawing.Point(11, 316)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(204, 17)
        Me.CheckBox4.TabIndex = 7
        Me.CheckBox4.Text = "Рассчитывать токи автоматически"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'FormOptions
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(492, 364)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormOptions"
        Me.Text = "Настройки"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents LabelMas As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelNeu As Label
    Friend WithEvents LabelRam As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents LabelComment As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents LabelTXT As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBoxUdef As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxLamp As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBoxResist As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBoxNomPred As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxRele As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents CheckBox4 As CheckBox
End Class
