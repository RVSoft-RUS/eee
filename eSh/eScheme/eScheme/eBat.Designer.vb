﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eBat
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eBat))
		Me.ContextMenu1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ЗадатьНапряжениеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.ContextMenu1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ContextMenu1
		'
		Me.ContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ЗадатьНапряжениеToolStripMenuItem})
		Me.ContextMenu1.Name = "ContextMenuStrip1"
		Me.ContextMenu1.Size = New System.Drawing.Size(182, 48)
		'
		'ЗадатьНапряжениеToolStripMenuItem
		'
		Me.ЗадатьНапряжениеToolStripMenuItem.Name = "ЗадатьНапряжениеToolStripMenuItem"
		Me.ЗадатьНапряжениеToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
		Me.ЗадатьНапряжениеToolStripMenuItem.Text = "Задать напряжение"
		'
		'eBat
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.DoubleBuffered = True
		Me.Name = "eBat"
		Me.Size = New System.Drawing.Size(50, 32)
		Me.ContextMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ContextMenu1 As ContextMenuStrip
	Friend WithEvents ЗадатьНапряжениеToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ToolTip1 As ToolTip
End Class
