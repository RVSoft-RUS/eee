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
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.pb3 = New System.Windows.Forms.PictureBox()
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb4 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenu1.SuspendLayout()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(5, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 15)
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
        'pb1
        '
        Me.pb1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
        Me.pb1.Location = New System.Drawing.Point(0, 0)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(40, 30)
        Me.pb1.TabIndex = 1
        Me.pb1.TabStop = False
        Me.pb1.Visible = False
        '
        'pb3
        '
        Me.pb3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb3.Image = CType(resources.GetObject("pb3.Image"), System.Drawing.Image)
        Me.pb3.Location = New System.Drawing.Point(0, 0)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(40, 30)
        Me.pb3.TabIndex = 2
        Me.pb3.TabStop = False
        Me.pb3.Visible = False
        '
        'pb2
        '
        Me.pb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb2.Image = CType(resources.GetObject("pb2.Image"), System.Drawing.Image)
        Me.pb2.Location = New System.Drawing.Point(0, 0)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(40, 30)
        Me.pb2.TabIndex = 3
        Me.pb2.TabStop = False
        Me.pb2.Visible = False
        '
        'pb4
        '
        Me.pb4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb4.Image = CType(resources.GetObject("pb4.Image"), System.Drawing.Image)
        Me.pb4.Location = New System.Drawing.Point(0, 0)
        Me.pb4.Name = "pb4"
        Me.pb4.Size = New System.Drawing.Size(40, 30)
        Me.pb4.TabIndex = 4
        Me.pb4.TabStop = False
        Me.pb4.Visible = False
        '
        'EResist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.pb4)
        Me.Controls.Add(Me.pb2)
        Me.Controls.Add(Me.pb3)
        Me.Controls.Add(Me.pb1)
        Me.Name = "EResist"
        Me.Size = New System.Drawing.Size(40, 30)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenu1.ResumeLayout(False)
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents ContextMenu1 As ContextMenuStrip
	Friend WithEvents ЗадатьToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents pb1 As PictureBox
    Friend WithEvents pb3 As PictureBox
    Friend WithEvents pb2 As PictureBox
    Friend WithEvents pb4 As PictureBox
End Class
