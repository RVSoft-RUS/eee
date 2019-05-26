<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class eButton
	Inherits System.Windows.Forms.UserControl

	'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eButton))
		Me.pb1 = New System.Windows.Forms.PictureBox()
		Me.pb2 = New System.Windows.Forms.PictureBox()
		Me.pb3 = New System.Windows.Forms.PictureBox()
		Me.pb4 = New System.Windows.Forms.PictureBox()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.ContextMenu1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pb4, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ContextMenu1.SuspendLayout()
		Me.SuspendLayout()
		'
		'pb1
		'
		Me.pb1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
		Me.pb1.Location = New System.Drawing.Point(0, 0)
		Me.pb1.Name = "pb1"
		Me.pb1.Size = New System.Drawing.Size(30, 20)
		Me.pb1.TabIndex = 0
		Me.pb1.TabStop = False
		'
		'pb2
		'
		Me.pb2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pb2.Image = CType(resources.GetObject("pb2.Image"), System.Drawing.Image)
		Me.pb2.Location = New System.Drawing.Point(0, 0)
		Me.pb2.Name = "pb2"
		Me.pb2.Size = New System.Drawing.Size(30, 20)
		Me.pb2.TabIndex = 1
		Me.pb2.TabStop = False
		Me.pb2.Visible = False
		'
		'pb3
		'
		Me.pb3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pb3.Image = CType(resources.GetObject("pb3.Image"), System.Drawing.Image)
		Me.pb3.Location = New System.Drawing.Point(0, 0)
		Me.pb3.Name = "pb3"
		Me.pb3.Size = New System.Drawing.Size(30, 20)
		Me.pb3.TabIndex = 2
		Me.pb3.TabStop = False
		Me.pb3.Visible = False
		'
		'pb4
		'
		Me.pb4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pb4.Image = CType(resources.GetObject("pb4.Image"), System.Drawing.Image)
		Me.pb4.Location = New System.Drawing.Point(0, 0)
		Me.pb4.Name = "pb4"
		Me.pb4.Size = New System.Drawing.Size(30, 20)
		Me.pb4.TabIndex = 3
		Me.pb4.TabStop = False
		Me.pb4.Visible = False
		'
		'Timer1
		'
		'
		'ContextMenu1
		'
		Me.ContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
		Me.ContextMenu1.Name = "ContextMenu1"
		Me.ContextMenu1.Size = New System.Drawing.Size(219, 26)
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(218, 22)
		Me.ToolStripMenuItem1.Text = "Задать время отключения"
		'
		'eButton
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.pb4)
		Me.Controls.Add(Me.pb3)
		Me.Controls.Add(Me.pb2)
		Me.Controls.Add(Me.pb1)
		Me.Name = "eButton"
		Me.Size = New System.Drawing.Size(30, 20)
		CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pb4, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ContextMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents pb1 As PictureBox
	Friend WithEvents pb2 As PictureBox
	Friend WithEvents pb3 As PictureBox
	Friend WithEvents pb4 As PictureBox
	Friend WithEvents Timer1 As Timer
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents ContextMenu1 As ContextMenuStrip
	Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
