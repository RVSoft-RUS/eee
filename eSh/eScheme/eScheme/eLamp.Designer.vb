﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eLamp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eLamp))
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ЗадатьСопротивлениеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pb2
        '
        Me.pb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb2.Image = CType(resources.GetObject("pb2.Image"), System.Drawing.Image)
        Me.pb2.Location = New System.Drawing.Point(0, 0)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(30, 30)
        Me.pb2.TabIndex = 1
        Me.pb2.TabStop = False
        Me.pb2.Visible = False
        '
        'pb1
        '
        Me.pb1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
        Me.pb1.Location = New System.Drawing.Point(0, 0)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(30, 30)
        Me.pb1.TabIndex = 0
        Me.pb1.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ЗадатьСопротивлениеToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(199, 26)
        '
        'ЗадатьСопротивлениеToolStripMenuItem
        '
        Me.ЗадатьСопротивлениеToolStripMenuItem.Name = "ЗадатьСопротивлениеToolStripMenuItem"
        Me.ЗадатьСопротивлениеToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ЗадатьСопротивлениеToolStripMenuItem.Text = "Задать сопротивление"
        '
        'eLamp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pb2)
        Me.Controls.Add(Me.pb1)
        Me.Name = "eLamp"
        Me.Size = New System.Drawing.Size(30, 30)
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pb1 As PictureBox
    Friend WithEvents pb2 As PictureBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ЗадатьСопротивлениеToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
End Class
