Public Class CNetworkMode

    Public Shared ReadOnly mNetMode_UDP As String = "UDP"
    Public Shared ReadOnly mNetMode_TCP As String = "TCP"
    Public Shared ReadOnly mNetMode_JL As String = "JL"
    Public Shared ReadOnly mNetMode_API As String = "API"

    Public Function GetMode() As String
        Return mainmodule.NetMode
    End Function

    Public Function SetMode(pMode As String)
        mainmodule.NetMode = pMode
    End Function

End Class
