<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eXP
	Inherits System.Windows.Forms.UserControl

	'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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
        Me.Label_Name = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Label_Name
        '
        Me.Label_Name.BackColor = System.Drawing.Color.White
        Me.Label_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label_Name.Location = New System.Drawing.Point(0, 0)
        Me.Label_Name.Name = "Label_Name"
        Me.Label_Name.Size = New System.Drawing.Size(30, 15)
        Me.Label_Name.TabIndex = 1
        Me.Label_Name.Text = "X12"
        Me.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'eXP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label_Name)
        Me.Name = "eXP"
        Me.Size = New System.Drawing.Size(30, 75)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label_Name As Label
    Friend WithEvents ToolTip1 As ToolTip
End Class
