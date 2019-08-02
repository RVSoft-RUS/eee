<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eMotor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eMotor))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cPic = New System.Windows.Forms.PictureBox()
        CType(Me.cPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 175
        '
        'cPic
        '
        Me.cPic.BackColor = System.Drawing.Color.White
        Me.cPic.Image = CType(resources.GetObject("cPic.Image"), System.Drawing.Image)
        Me.cPic.Location = New System.Drawing.Point(5, 5)
        Me.cPic.Name = "cPic"
        Me.cPic.Size = New System.Drawing.Size(20, 20)
        Me.cPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.cPic.TabIndex = 0
        Me.cPic.TabStop = False
        Me.cPic.Visible = False
        '
        'eMotor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.cPic)
        Me.Name = "eMotor"
        Me.Size = New System.Drawing.Size(30, 30)
        CType(Me.cPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents cPic As PictureBox
End Class
