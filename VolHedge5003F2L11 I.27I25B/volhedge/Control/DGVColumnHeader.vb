Class DGVColumnHeader
    Inherits DataGridViewColumnHeaderCell
    Private CheckBoxRegion As Rectangle
    Private m_checkAll As Boolean = True
    Private m_RdOnly As Boolean = True
    Private Mgraphics As Graphics
    'Private m_cellStyle As DataGridViewCellStyle
    'Private m_advancedBorderStyle As DataGridViewAdvancedBorderStyle


    Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal dataGridViewElementState As DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
        Mgraphics = graphics
        'cellStyle = m_cellStyle
        'advancedBorderStyle = m_advancedBorderStyle


        'cellStyle.Font = New System.Drawing.Font("", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, _
         formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)

        graphics.FillRectangle(New SolidBrush(cellStyle.BackColor), cellBounds)

        'CheckBoxRegion = New Rectangle(cellBounds.Location.X + 1, cellBounds.Location.Y + 2, 25, cellBounds.Size.Height - 4)
        CheckBoxRegion = New Rectangle(cellBounds.Location.X + 2, cellBounds.Location.Y + 2, 29, cellBounds.Size.Height - 5)


        If Me.m_checkAll Then
            ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Checked)
        Else
            ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Normal)
        End If

        'Dim normalRegion As New Rectangle(cellBounds.Location.X + 1 + 25, cellBounds.Location.Y, cellBounds.Size.Width - 26, cellBounds.Size.Height)
        'sdfDim normalRegion As New Rectangle(cellBounds.Location.X + 1 + 25, cellBounds.Location.Y + 26, cellBounds.Size.Width - 26, cellBounds.Size.Height)
        Dim normalRegion As New Rectangle(cellBounds.Location.X, cellBounds.Location.Y, cellBounds.Width, cellBounds.Height)

        'graphics.DrawString(value.ToString(), cellStyle.Font, New SolidBrush(cellStyle.ForeColor), normalRegion)
        graphics.DrawString("", cellStyle.Font, New SolidBrush(cellStyle.ForeColor), CheckBoxRegion)

        'MsgBox(Me.Value)
        'Me.Value = ""

        

    End Sub

    Protected Overrides Sub OnMouseClick(ByVal e As DataGridViewCellMouseEventArgs)
        'Convert the CheckBoxRegion 
        If Me.m_RdOnly = False Then
            Dim rec As New Rectangle(New Point(0, 0), Me.CheckBoxRegion.Size)
            Me.m_checkAll = Not Me.m_checkAll

            If rec.Contains(e.Location) Then
                Me.DataGridView.Invalidate()
            End If
            MyBase.OnMouseClick(e)
        End If
    End Sub

    Public Property CheckAll() As Boolean
        Get
            Return Me.m_checkAll
        End Get
        Set(ByVal value As Boolean)
            Me.m_checkAll = value
        End Set
    End Property

    Public Property RdOnly() As Boolean
        Get
            Return Me.m_RdOnly
        End Get
        Set(ByVal value As Boolean)
            Me.m_RdOnly = value
        End Set
    End Property

    'Public Sub New(ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle)
    '    m_cellStyle = cellStyle
    '    m_advancedBorderStyle = advancedBorderStyle
    'End Sub

    Public Sub New()
        'CheckAll = True
    End Sub
End Class