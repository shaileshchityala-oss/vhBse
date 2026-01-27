
Imports System.Drawing
Imports System.Windows.Forms

Namespace Helper
    Class ControlMover
        Public Enum Direction
            Any
            Horizontal
            Vertical
        End Enum

        Public Shared Sub Init(ByVal control As Control)
            Init(control, Direction.Any)
        End Sub

        Public Shared Sub Init(ByVal control As Control, ByVal direction As Direction)
            Init(control, control, direction)
        End Sub

        Public Shared Sub Init(ByVal control As Control, ByVal container As Control, ByVal direction__1 As Direction)
            Dim Dragging As Boolean = False
            Dim DragStart As Point = Point.Empty
            control.MouseDown += Sub(sender As Object, e As MouseEventArgs)
                                     Dragging = True
                                     DragStart = New Point(e.X, e.Y)
                                     control.Capture = True

                                 End Sub
            control.MouseUp += Sub(sender As Object, e As MouseEventArgs)
                                   Dragging = False
                                   control.Capture = False

                               End Sub
			control.MouseMove += Sub(sender As Object, e As MouseEventArgs) If Dragging Then
            If direction__1 <> Direction.Vertical Then
                container.Left = Math.Max(0, e.X + container.Left - DragStart.X)
            End If
            If direction__1 <> Direction.Horizontal Then
                container.Top = Math.Max(0, e.Y + container.Top - DragStart.Y)
            End If

        End Sub
    End Class
End Namespace

