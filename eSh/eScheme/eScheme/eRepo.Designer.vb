<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eRepo
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eRepo))
		Me.pb0 = New System.Windows.Forms.PictureBox()
		Me.pb1 = New System.Windows.Forms.PictureBox()
		Me.pb2 = New System.Windows.Forms.PictureBox()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.ЗадатьСопротивлениеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		CType(Me.pb0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'pb0
		'
		Me.pb0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pb0.Location = New System.Drawing.Point(5, 18)
		Me.pb0.Name = "pb0"
		Me.pb0.Size = New System.Drawing.Size(170, 8)
		Me.pb0.TabIndex = 9
		Me.pb0.TabStop = False
		'
		'pb1
		'
		Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
		Me.pb1.Location = New System.Drawing.Point(0, 0)
		Me.pb1.Name = "pb1"
		Me.pb1.Size = New System.Drawing.Size(180, 15)
		Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.pb1.TabIndex = 10
		Me.pb1.TabStop = False
		Me.pb1.Visible = False
		'
		'pb2
		'
		Me.pb2.Image = CType(resources.GetObject("pb2.Image"), System.Drawing.Image)
		Me.pb2.Location = New System.Drawing.Point(0, 0)
		Me.pb2.Name = "pb2"
		Me.pb2.Size = New System.Drawing.Size(15, 180)
		Me.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.pb2.TabIndex = 11
		Me.pb2.TabStop = False
		Me.pb2.Visible = False
		'
		'Timer1
		'
		'
		'ЗадатьСопротивлениеToolStripMenuItem
		'
		Me.ЗадатьСопротивлениеToolStripMenuItem.Name = "ЗадатьСопротивлениеToolStripMenuItem"
		Me.ЗадатьСопротивлениеToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
		Me.ЗадатьСопротивлениеToolStripMenuItem.Text = "Задать время переключения"
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ЗадатьСопротивлениеToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(233, 48)
		'
		'eRepo
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.Silver
		Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Controls.Add(Me.pb0)
		Me.Controls.Add(Me.pb1)
		Me.Controls.Add(Me.pb2)
		Me.Name = "eRepo"
		Me.Size = New System.Drawing.Size(180, 30)
		CType(Me.pb0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents pb0 As PictureBox
    Friend WithEvents pb1 As PictureBox
    Friend WithEvents pb2 As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents ЗадатьСопротивлениеToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
