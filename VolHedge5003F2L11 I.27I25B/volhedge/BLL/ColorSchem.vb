Public Class ColorSchem
    Private Function GetR(ByVal Color As Long) As Long
        GetR = Color And &HFF
    End Function
    Private Function GetG(ByVal Color As Long) As Long
        GetG = ((Color And &HFF00) / &H100) And &HFF
    End Function
    Private Function GetB(ByVal Color As Long) As Long
        GetB = ((Color And &HFF0000) / &H10000) And &HFF
    End Function

    Private Shared Function Bind(ByVal v As Object, ByVal min As Object, ByVal max As Object) As Object
        min = 100
        If v > max Then
            Bind = max
        ElseIf v < min Then
            Bind = min
        Else
            Bind = v
        End If
    End Function


    Public Shared Function GetColorGradient(ByVal Rn!, ByVal Rm!, ByVal Gn!, ByVal Gm!, ByVal Bn!, ByVal Bm!, ByVal Minval As Single, ByVal maxval As Single, ByVal CurVal As Single) As Color
        Try

        
            Dim Rd!, Gd!, Bd!
            Dim Rv!, Gv!, Bv!


            'Rd = Rm - Rn
            'Gd = Gm - Gn
            'Bd = Bm - Bn
            Rd = Rn - Rm
            Gd = Gn - Gm
            Bd = Bn - Bm

            Dim Oper!
            'Oper = CurVal * 100 / (maxval - Minval)
            Oper = CurVal / ((maxval - Minval) / 100)

            'Rv = Oper * Rd / 100
            'Gv = Oper * Gd / 100
            'Bv = Oper * Bd / 100

            Rv = ((Rd / 100) * Oper) + Rm
            Gv = ((Gd / 100) * Oper) + Gm
            Bv = ((Bd / 100) * Oper) + Bm


            GetColorGradient = Color.FromArgb(Bind(Rn + Rv, 0, 255), Bind(Gn + Gv, 0, 255), Bind(Bn + Bv, 0, 255))
        Catch ex As Exception
            GetColorGradient = Color.White
        End Try
    End Function


End Class
