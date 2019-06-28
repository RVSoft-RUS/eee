<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eSwitch3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eSwitch3))
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.pb3 = New System.Windows.Forms.PictureBox()
        Me.pb4 = New System.Windows.Forms.PictureBox()
        Me.pb5 = New System.Windows.Forms.PictureBox()
        Me.pb6 = New System.Windows.Forms.PictureBox()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pb2
        '
        Me.pb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb2.Image = CType(resources.GetObject("pb2.Image"), System.Drawing.Image)
        Me.pb2.Location = New System.Drawing.Point(0, 0)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(52, 52)
        Me.pb2.TabIndex = 5
        Me.pb2.TabStop = False
        Me.pb2.Visible = False
        '
        'pb1
        '
        Me.pb1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
        Me.pb1.Location = New System.Drawing.Point(0, 0)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(52, 52)
        Me.pb1.TabIndex = 4
        Me.pb1.TabStop = False
        Me.pb1.Visible = False
        '
        'pb3
        '
        Me.pb3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb3.Image = CType(resources.GetObject("pb3.Image"), System.Drawing.Image)
        Me.pb3.Location = New System.Drawing.Point(0, 0)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(52, 52)
        Me.pb3.TabIndex = 6
        Me.pb3.TabStop = False
        Me.pb3.Visible = False
        '
        'pb4
        '
        Me.pb4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb4.Image = CType(resources.GetObject("pb4.Image"), System.Drawing.Image)
        Me.pb4.Location = New System.Drawing.Point(0, 0)
        Me.pb4.Name = "pb4"
        Me.pb4.Size = New System.Drawing.Size(52, 52)
        Me.pb4.TabIndex = 7
        Me.pb4.TabStop = False
        Me.pb4.Visible = False
        '
        'pb5
        '
        Me.pb5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb5.Image = CType(resources.GetObject("pb5.Image"), System.Drawing.Image)
        Me.pb5.Location = New System.Drawing.Point(0, 0)
        Me.pb5.Name = "pb5"
        Me.pb5.Size = New System.Drawing.Size(52, 52)
        Me.pb5.TabIndex = 8
        Me.pb5.TabStop = False
        Me.pb5.Visible = False
        '
        'pb6
        '
        Me.pb6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb6.Image = CType(resources.GetObject("pb6.Image"), System.Drawing.Image)
        Me.pb6.Location = New System.Drawing.Point(0, 0)
        Me.pb6.Name = "pb6"
        Me.pb6.Size = New System.Drawing.Size(52, 52)
        Me.pb6.TabIndex = 9
        Me.pb6.TabStop = False
        Me.pb6.Visible = False
        '
        'eSwitch3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pb4)
        Me.Controls.Add(Me.pb3)
        Me.Controls.Add(Me.pb2)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.pb6)
        Me.Controls.Add(Me.pb5)
        Me.Name = "eSwitch3"
        Me.Size = New System.Drawing.Size(52, 52)
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pb1 As PictureBox
    Friend WithEvents pb2 As PictureBox
    Friend WithEvents pb3 As PictureBox
    Friend WithEvents pb4 As PictureBox
    Friend WithEvents pb5 As PictureBox
    Friend WithEvents pb6 As PictureBox
End Class
