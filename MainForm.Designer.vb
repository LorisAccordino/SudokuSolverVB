<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        solveBtn = New Button()
        clearBtn = New Button()
        infoTimeLbl = New Label()
        timeLbl = New Label()
        uiTimer = New Timer(components)
        SuspendLayout()
        ' 
        ' solveBtn
        ' 
        solveBtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        solveBtn.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        solveBtn.Location = New Point(388, 398)
        solveBtn.Name = "solveBtn"
        solveBtn.Size = New Size(140, 35)
        solveBtn.TabIndex = 0
        solveBtn.Text = "Solve"
        solveBtn.UseVisualStyleBackColor = True
        ' 
        ' clearBtn
        ' 
        clearBtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        clearBtn.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        clearBtn.Location = New Point(242, 398)
        clearBtn.Name = "clearBtn"
        clearBtn.Size = New Size(140, 35)
        clearBtn.TabIndex = 1
        clearBtn.Text = "Clear"
        clearBtn.UseVisualStyleBackColor = True
        ' 
        ' infoTimeLbl
        ' 
        infoTimeLbl.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        infoTimeLbl.AutoSize = True
        infoTimeLbl.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        infoTimeLbl.Location = New Point(21, 403)
        infoTimeLbl.Name = "infoTimeLbl"
        infoTimeLbl.Size = New Size(120, 25)
        infoTimeLbl.TabIndex = 2
        infoTimeLbl.Text = "Time elapsed:"
        ' 
        ' timeLbl
        ' 
        timeLbl.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        timeLbl.AutoSize = True
        timeLbl.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        timeLbl.Location = New Point(147, 403)
        timeLbl.Name = "timeLbl"
        timeLbl.Size = New Size(80, 25)
        timeLbl.TabIndex = 3
        timeLbl.Text = "00:00:00"
        ' 
        ' uiTimer
        ' 
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(556, 450)
        Controls.Add(timeLbl)
        Controls.Add(infoTimeLbl)
        Controls.Add(clearBtn)
        Controls.Add(solveBtn)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "MainForm"
        Text = "Sudoku Solver"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents solveBtn As Button
    Friend WithEvents clearBtn As Button
    Friend WithEvents infoTimeLbl As Label
    Friend WithEvents timeLbl As Label
    Friend WithEvents uiTimer As Timer

End Class
