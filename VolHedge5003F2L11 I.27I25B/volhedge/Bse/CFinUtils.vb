Public Enum EExpiryCategory
    W0
    W1
    W2
    W3
    M0
    M1
    M2
    ANNUAL
    EXPIRED
End Enum

Public Class CFinUtils
    Public Shared Function GetExpiryCategory(today As Date, expiry As Date) As EExpiryCategory
        today = today.Date
        expiry = expiry.Date

        If expiry < today Then
            Return EExpiryCategory.EXPIRED
        End If

        Dim daysDiff As Integer = (expiry - today).Days
        Dim weekNum As Integer = daysDiff \ 7

        ' Last Friday of expiry month
        Dim lastFriday As Date = LastWeekdayOfMonth(expiry.Year, expiry.Month, DayOfWeek.Friday)

        If expiry.Month = today.Month AndAlso expiry.Year = today.Year Then
            If expiry = lastFriday Then
                Return EExpiryCategory.M0
            End If
            Select Case weekNum
                Case 0 : Return EExpiryCategory.W1
                Case 1 : Return EExpiryCategory.W2
                Case 2 : Return EExpiryCategory.W3
                Case 3 : Return EExpiryCategory.M0
            End Select
        End If

        Dim nextMonth As Date = today.AddMonths(1)
        If expiry.Month = nextMonth.Month AndAlso expiry.Year = nextMonth.Year Then
            Return EExpiryCategory.M1
        End If

        Dim month2 As Date = today.AddMonths(2)
        If expiry.Month = month2.Month AndAlso expiry.Year = month2.Year Then
            Return EExpiryCategory.M2
        End If
        Return EExpiryCategory.ANNUAL
    End Function
    Public Shared Function LastWeekdayOfMonth(year As Integer, month As Integer, weekday As DayOfWeek) As Date
        Dim lastDay As New Date(year, month, Date.DaysInMonth(year, month))
        Do While lastDay.DayOfWeek <> weekday
            lastDay = lastDay.AddDays(-1)
        Loop
        Return lastDay
    End Function
End Class
