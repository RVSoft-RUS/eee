<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EResist
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EResist))
		Me.PictureBox1 = New System.Windows.Forms.PictureBox()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.ContextMenu1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ЗадатьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ContextMenu1.SuspendLayout()
		Me.SuspendLayout()
		'
		'PictureBox1
		'
		Me.PictureBox1.Location = New System.Drawing.Point(4, 5)
		Me.PictureBox1.Name = "PictureBox1"
		Me.PictureBox1.Size = New System.Drawing.Size(15, 30)
		Me.PictureBox1.TabIndex = 0
		Me.PictureBox1.TabStop = False
		'
		'ContextMenu1
		'
		Me.ContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ЗадатьToolStripMenuItem})
		Me.ContextMenu1.Name = "ContextMenuStrip1"
		Me.ContextMenu1.Size = New System.Drawing.Size(199, 26)
		'
		'ЗадатьToolStripMenuItem
		'
		Me.ЗадатьToolStripMenuItem.Name = "ЗадатьToolStripMenuItem"
		Me.ЗадатьToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.ЗадатьToolStripMenuItem.Text = "Задать сопротивление"
		'
		'EResist
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
		Me.Controls.Add(Me.PictureBox1)
		Me.Name = "EResist"
		Me.Size = New System.Drawing.Size(30, 40)
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ContextMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents PictureBox1 As PictureBox
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents ContextMenu1 As ContextMenuStrip
	Friend WithEvents ЗадатьToolStripMenuItem As ToolStripMenuItem
End Class
