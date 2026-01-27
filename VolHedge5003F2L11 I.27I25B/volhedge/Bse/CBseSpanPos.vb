Imports System.Text
Imports System.IO

Public Class CPosXmlBuilder

    Private sb As StringBuilder
    Public mHtBseComp As Hashtable
    Public Sub HtBseCompInit()
        mHtBseComp = New Hashtable()
        mHtBseComp.Add("SENSEX", "BSX")
        mHtBseComp.Add("BANKNIFTY", "BKNF")
    End Sub

    Public Function GetCompCode(pComp As String) As String
        If mHtBseComp.ContainsKey(pComp) Then
            Return mHtBseComp(pComp)
        End If
        Return pComp
    End Function

    Public Function GetPfCode(pComp As String, pType As String) As String
        Dim type As String = GetPfCodeType(pType)
        If mHtBseComp.ContainsKey(pComp) Then
            Return mHtBseComp(pComp) + type
        End If
        Return pComp
    End Function
    Public Function GetPfCodeType(pType As String) As String
        If pType = "FUT" Then
            Return "FUT"
        ElseIf pType = "OOP" Then
            Return "OPT"
        ElseIf pType = "C" Then
            Return "OPT"
        ElseIf pType = "P" Then
            Return "OPT"
        End If
        Return ""
    End Function


    Public Sub New()
        HtBseCompInit()
        sb = New StringBuilder()
        sb.AppendLine("<?xml version=""1.0""?>")
        sb.AppendLine("<posFile>")
    End Sub

    ' ---------------- ROOT HEADER ----------------
    Public Sub AddHeader(fileFormat As String, created As String)
        sb.AppendLine("  <fileFormat>" & fileFormat & "</fileFormat>")
        sb.AppendLine("  <created>" & created & "</created>")
    End Sub

    ' ---------------- POINT IN TIME ----------------
    Public Sub StartPointInTime(dateStr As String, isSetl As Integer, timeStr As String, run As Integer)
        sb.AppendLine("  <pointInTime>")
        sb.AppendLine("    <date>" & dateStr & "</date>")
        sb.AppendLine("    <isSetl>" & isSetl & "</isSetl>")
        sb.AppendLine("    <time>" & timeStr & "</time>")
        sb.AppendLine("    <run>" & run & "</run>")
    End Sub

    Public Sub EndPointInTime()
        sb.AppendLine("  </pointInTime>")
    End Sub

    ' ---------------- PORTFOLIO ----------------
    Public Sub StartPortfolio(firm As String, acctId As String, acctType As String,
                          isCust As Integer, seg As String,
                          currency As String, isNew As Integer, custPortUseLov As Integer,
                          ledgerBal As Decimal, ote As Decimal,
                          securities As Decimal, lue As Decimal)
        acctId = firm
        firm = "BSE"
        sb.AppendLine("    <portfolio>")
        sb.AppendLine("      <firm>" & firm & "</firm>")
        sb.AppendLine("      <acctId>" & acctId & "</acctId>")
        sb.AppendLine("      <acctType>" & acctType & "</acctType>")
        sb.AppendLine("      <isCust>" & isCust & "</isCust>")
        sb.AppendLine("      <seg>" & seg & "</seg>")
        sb.AppendLine("      <isNew>" & isNew & "</isNew>")
        sb.AppendLine("      <custPortUseLov>" & custPortUseLov & "</custPortUseLov>")
        sb.AppendLine("      <currency>" & currency & "</currency>")

        ' ---- File A specific ----
        sb.AppendLine("      <ledgerBal>" & ledgerBal.ToString("0.00") & "</ledgerBal>")
        sb.AppendLine("      <ote>" & ote.ToString("0.00") & "</ote>")
        sb.AppendLine("      <securities>" & securities.ToString("0.00") & "</securities>")
        sb.AppendLine("      <lue>" & lue.ToString("0.00") & "</lue>")
    End Sub


    Public Sub EndPortfolio()
        sb.AppendLine("    </portfolio>")
    End Sub

    ' ---------------- ACCT SUBTYPE ----------------
    Public Sub AddAcctSubType(code As String, value As Integer)
        sb.AppendLine("      <acctSubType>")
        sb.AppendLine("        <acctSubTypeCode>" & code & "</acctSubTypeCode>")
        sb.AppendLine("        <value>" & value & "</value>")
        sb.AppendLine("      </acctSubType>")
    End Sub

    ' ---------------- EC PORT ----------------
    Public Sub StartEcPort(ec As String, cc As String, r As Integer, currency As String, pss As Integer)
        sb.AppendLine("      <ecPort>")
        sb.AppendLine("        <ec>" & ec & "</ec>")
        sb.AppendLine("        <ccPort>")
        sb.AppendLine("          <cc>" & cc & "</cc>")
        sb.AppendLine("          <r>" & r & "</r>")
        sb.AppendLine("          <currency>" & currency & "</currency>")
        sb.AppendLine("          <pss>" & pss & "</pss>")
    End Sub

    Public Sub EndEcPort()
        sb.AppendLine("        </ccPort>")
        sb.AppendLine("      </ecPort>")
    End Sub

    ' ---------------- POSITION (NP) ----------------
    'Public Sub AddPosition(exch As String, pfCode As String, pfType As String,
    '                       pe As String, undPe As String,
    '                       o As String, strike As Decimal, qty As Integer)

    '    sb.AppendLine("          <np>")
    '    sb.AppendLine("            <exch>" & exch & "</exch>")
    '    sb.AppendLine("            <pfCode>" & pfCode & "</pfCode>")
    '    sb.AppendLine("            <pfType>" & pfType & "</pfType>")
    '    sb.AppendLine("            <pe>" & pe & "</pe>")
    '    sb.AppendLine("            <undPe>" & undPe & "</undPe>")
    '    sb.AppendLine("            <o>" & o & "</o>")
    '    sb.AppendLine("            <k>" & strike & "</k>")
    '    sb.AppendLine("            <net>" & qty & "</net>")
    '    sb.AppendLine("          </np>")
    'End Sub

    Public Sub AddPosition(exch As String, pfCode As String, pfType As String,
                       pe As String, undPe As String,
                       o As String, strike As Decimal, qty As Integer)

        pfCode = Me.GetPfCode(pfCode, pfType)
        sb.AppendLine("          <np>")
        sb.AppendLine("            <exch>" & exch & "</exch>")
        sb.AppendLine("            <pfCode>" & pfCode & "</pfCode>")
        sb.AppendLine("            <pfType>" & pfType & "</pfType>")
        sb.AppendLine("            <pe>" & pe & "</pe>")

        ' -------- FUT : DO NOT emit undPe / o / k --------
        If pfType <> "FUT" Then
            sb.AppendLine("            <undPe>" & undPe & "</undPe>")
            sb.AppendLine("            <o>" & o & "</o>")
            sb.AppendLine("            <k>" & strike.ToString("0.00") & "</k>")
        End If

        sb.AppendLine("            <net>" & qty & "</net>")
        sb.AppendLine("          </np>")
    End Sub


    Public Sub AddPositionFuture(exch As String,
                             pfCode As String,
                             expiry As String,
                             qty As Integer)

        AddPosition(exch, pfCode, "FUT", expiry, "", "", 0D, qty)
    End Sub

    Public Sub AddPositionOpt(exch As String,
                          pfCode As String,
                          expiry As String,
                          undExpiry As String,
                          optType As String,   ' "C" or "P"
                          strike As Decimal,
                          qty As Integer)

        AddPosition(exch, pfCode, "OOP", expiry, undExpiry, optType, strike, qty)
    End Sub


    Public Sub AddPositionCall(exch As String,
                           pfCode As String,
                           expiry As String,
                           undExpiry As String,
                           strike As Decimal,
                           qty As Integer)

        AddPositionOpt(exch, pfCode, expiry, undExpiry, "C", strike, qty)
    End Sub
    Public Sub AddPositionPut(exch As String,
                          pfCode As String,
                          expiry As String,
                          undExpiry As String,
                          strike As Decimal,
                          qty As Integer)

        AddPositionOpt(exch, pfCode, expiry, undExpiry, "P", strike, qty)
    End Sub


    ' ---------------- FINALIZE ----------------
    Public Function GetXml() As String
        sb.AppendLine("</posFile>")
        Return sb.ToString()
    End Function

    Public Sub Save(path As String)
        File.WriteAllText(path, GetXml())
    End Sub


    'Public Sub test()
    '    Dim b As New CPosXmlBuilder()

    '    b.AddHeader("4.00", "20251212")

    '    b.StartPointInTime("20251209", 0, "23:21", 1)

    '    b.StartPortfolio("BSE", "SENSEX", "S", 1, "N/A", "INR", 1, 1)

    '    b.AddAcctSubType("GSCIER", 0)
    '    b.AddAcctSubType("TRAKRS", 0)

    '    b.StartEcPort("ICCL", "BSX", 1, "INR", 0)

    '    b.AddPosition("BSE", "BSXOPT", "OOP", "20251211", "000000", "C", 85000, -50)
    '    b.AddPosition("BSE", "BSXOPT", "OOP", "20251211", "000000", "P", 85000, -100)
    '    b.AddPosition("BSE", "BSXOPT", "OOP", "20251211", "000000", "C", 85100, 200)

    '    b.EndEcPort()

    '    b.EndPortfolio()


    '    ''------------------------------------------------------------------------
    '    'b.StartPortfolio("BSE", "RELIANCE", "S", 1, "N/A", "INR", 1, 1)

    '    'b.AddAcctSubType("GSCIER", 0)
    '    'b.AddAcctSubType("TRAKRS", 0)

    '    'b.StartEcPort("ICCL", "BSX", 1, "INR", 0)

    '    'b.AddPosition("BSE", "RELIOPT", "OOP", "20251224", "000000", "C", 1400, 50)
    '    'b.AddPosition("BSE", "RELIOPT", "OOP", "20251224", "000000", "P", 1410, -100)
    '    'b.AddPosition("BSE", "RELIOPT", "OOP", "20251224", "000000", "C", 1420, 200)

    '    'b.EndEcPort()

    '    'b.EndPortfolio()

    '    b.EndPointInTime()

    '    Dim xml As String = b.GetXml()
    '    b.Save("C:\Shailesh\bse\span\SpanBseNse\Bse\posOnlyBse.xml")
    '    End
    'End Sub


    Public Sub test()

        Dim b As New CPosXmlBuilder()

        b.AddHeader("4.00", "20251210")

        b.StartPointInTime("20251209", 0, ":::::", 1)

        b.StartPortfolio("BSX", "SENSEX", "S", 1, "N/A", "INR", 1, 1, 0D, 0D, 0D, 0D)

        b.StartEcPort("ICCL", "BSX", 1, "INR", 0)

        ' -------- FUT --------
        ' b.AddPosition("BSE", "BSXFUT", "FUT", "20251224", "", "", 0, 30)

        b.AddPositionFuture("BSE", "BSXFUT", "20251224", 30)

        ' -------- OPTIONS --------
        'b.AddPosition("BSE", "BSXOPT", "OOP", "20251211", "000000", "C", 84000, 30)
        'b.AddPosition("BSE", "BSXOPT", "OOP", "20251211", "000000", "P", 84000, -30)

        b.AddPositionCall("BSE", "BSXOPT", "20251211", "000000", 84000, 30)
        b.AddPositionPut("BSE", "BSXOPT", "20251211", "000000", 84000, -30)

        b.EndEcPort()
        b.EndPortfolio()

        '-----------------------------------------------------2.00 reliance
        b.StartPortfolio("BSX", "RELIANCE", "S", 1, "N/A", "INR", 1, 1, 0D, 0D, 0D, 0D)
        b.StartEcPort("ICCL", "BSX", 1, "INR", 0)

        ' -------- OPTIONS --------
        b.AddPosition("BSE", "RELIOPT", "OOP", "20251224", "000000", "C", 1400, 50)
        b.AddPosition("BSE", "RELIOPT", "OOP", "20251224", "000000", "P", 1410, -100)


        b.EndEcPort()
        b.EndPortfolio()


        b.EndPointInTime()

        b.Save("C:\Shailesh\bse\span\SpanBseNse\Bse\pos10.xml")
        End
    End Sub

End Class
