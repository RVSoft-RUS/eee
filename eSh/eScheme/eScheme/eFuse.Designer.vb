﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eFuse
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eFuse))
        Me.PictureBoxH = New System.Windows.Forms.PictureBox()
        Me.PictureBoxV = New System.Windows.Forms.PictureBox()
        Me.PictureBoxHC = New System.Windows.Forms.PictureBox()
        Me.PictureBoxVC = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.УдалитьПредохранительToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВставитьПредохранительToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBoxH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxHC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBoxH
        '
        Me.PictureBoxH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxH.Image = CType(resources.GetObject("PictureBoxH.Image"), System.Drawing.Image)
        Me.PictureBoxH.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxH.Name = "PictureBoxH"
        Me.PictureBoxH.Size = New System.Drawing.Size(30, 12)
        Me.PictureBoxH.TabIndex = 0
        Me.PictureBoxH.TabStop = False
        '
        'PictureBoxV
        '
        Me.PictureBoxV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxV.Image = CType(resources.GetObject("PictureBoxV.Image"), System.Drawing.Image)
        Me.PictureBoxV.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxV.Name = "PictureBoxV"
        Me.PictureBoxV.Size = New System.Drawing.Size(30, 12)
        Me.PictureBoxV.TabIndex = 1
        Me.PictureBoxV.TabStop = False
        '
        'PictureBoxHC
        '
        Me.PictureBoxHC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxHC.Image = CType(resources.GetObject("PictureBoxHC.Image"), System.Drawing.Image)
        Me.PictureBoxHC.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxHC.Name = "PictureBoxHC"
        Me.PictureBoxHC.Size = New System.Drawing.Size(30, 12)
        Me.PictureBoxHC.TabIndex = 2
        Me.PictureBoxHC.TabStop = False
        Me.PictureBoxHC.Visible = False
        '
        'PictureBoxVC
        '
        Me.PictureBoxVC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxVC.Image = CType(resources.GetObject("PictureBoxVC.Image"), System.Drawing.Image)
        Me.PictureBoxVC.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxVC.Name = "PictureBoxVC"
        Me.PictureBoxVC.Size = New System.Drawing.Size(30, 12)
        Me.PictureBoxVC.TabIndex = 3
        Me.PictureBoxVC.TabStop = False
        Me.PictureBoxVC.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'ContextMenu1
        '
        Me.ContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.УдалитьПредохранительToolStripMenuItem, Me.ВставитьПредохранительToolStripMenuItem})
        Me.ContextMenu1.Name = "ContextMenuStrip1"
        Me.ContextMenu1.Size = New System.Drawing.Size(215, 92)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(214, 22)
        Me.ToolStripMenuItem1.Text = "Задать номинал"
        '
        'УдалитьПредохранительToolStripMenuItem
        '
        Me.УдалитьПредохранительToolStripMenuItem.Name = "УдалитьПредохранительToolStripMenuItem"
        Me.УдалитьПредохранительToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.УдалитьПредохранительToolStripMenuItem.Text = "Удалить предохранитель"
        '
        'ВставитьПредохранительToolStripMenuItem
        '
        Me.ВставитьПредохранительToolStripMenuItem.Name = "ВставитьПредохранительToolStripMenuItem"
        Me.ВставитьПредохранительToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ВставитьПредохранительToolStripMenuItem.Text = "Вставить предохранитель"
        '
        'eFuse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PictureBoxVC)
        Me.Controls.Add(Me.PictureBoxHC)
        Me.Controls.Add(Me.PictureBoxV)
        Me.Controls.Add(Me.PictureBoxH)
        Me.Name = "eFuse"
        Me.Size = New System.Drawing.Size(30, 12)
        CType(Me.PictureBoxH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxHC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenu1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBoxH As PictureBox
    Friend WithEvents PictureBoxV As PictureBox
    Friend WithEvents PictureBoxHC As PictureBox
    Friend WithEvents PictureBoxVC As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ContextMenu1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents УдалитьПредохранительToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ВставитьПредохранительToolStripMenuItem As ToolStripMenuItem
End Class
