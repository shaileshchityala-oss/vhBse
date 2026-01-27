Public Class ClsLoginData
    Dim MyAccessSQL As New UDAL.data_access_sql

    Dim SP_UserMaster As String = "Spf_001OTP" '"Spf_001TCP" '"SP_UserMaster"
    Dim Sp_Spf_001Userdata As String = "Spf_001UserdataOTP_New" '"Spf_001UserdataOTP" '"Spf_001Userdata" '"SP_UserMaster"
    Dim Sp_Spf_001Alldata As String = "Spf_001UserdataOTP"
    Dim SP_InsertUserMaster As String = "Spf_002"
    Dim sp_ResetPassword As String = "Spf_005"
    Dim SP_DeleteUserMaster As String = "Spf_006"
    Dim sp_InsertRC As String = "dbo.InsertRC"
    Dim sp_UpdateRC As String = "dbo.Sp_UpdateLocalRangeData"
    Dim sp_SelectRC As String = "dbo.SelectRC"
    Dim Sp_useSupport As String = "getSupport" '"Spf_001Userdata" '"SP_UserMaster"
    Dim Sp_getmargin As String = "dbo.Sp_GetMargin"
    Dim SP_UpdateUserMaster As String = "sp_verifyEmail"
    Dim Sp_Spf_001CheckUser As String = "Spf_001CheckEmail"
    Dim Sp_Spf_001CheckUserPh As String = "Spf_001CheckPhone"
    Dim sp_verifyemail As String = "Spf_001VerifyEmail"

    Dim Sp_getcurrmargin As String = "dbo.Sp_GetMarginCur"
    Dim Sp_tblExposure As String = "V_tblExposure"
    Public _isAdmin As String
    Public _Userid As String
    Public _pwd As String


    Public _Username As String
    Public _Address As String
    Public _Mobile As String
    Public _Email As String
    Public _DOB As String

    Public _Firm As String
    Public _FirmAddress As String
    Public _FirmContactNo As String
    Public _Reference As String

    Public _Product As String
    Public _Allowed As String
    Public _Limited As String
    Public _ExDate As String
    Public _Status As String

    Public _M As String
    Public _H As String
    Public _P As String

    Public _City As String

    Public _BillNo As String 'F26
    Public _TCP As Integer  '_TCP
    Public _PreOTP As String  'PreOTP
    Public _PrePhOTP As String  'PreOTP
    Public _Lic As String 'F27

    Public _UsernameServer As String 'F27
    Public _DatabaseName As String 'F27
    Public _Password As String 'F27


    Public Property UsernameServer() As String
        Get
            Return _UsernameServer
        End Get
        Set(ByVal value As String)
            _UsernameServer = value
        End Set
    End Property
    Public Property DatabaseName() As String
        Get
            Return _DatabaseName
        End Get
        Set(ByVal value As String)
            _DatabaseName = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    Public Property PreOTP() As String
        Get
            Return _PreOTP
        End Get
        Set(ByVal value As String)
            _PreOTP = value
        End Set
    End Property
    Public Property PrePhOTP() As String
        Get
            Return _PrePhOTP
        End Get
        Set(ByVal value As String)
            _PrePhOTP = value
        End Set
    End Property
    Public Property Lic() As String
        Get
            Return clsUEnDe.FDec(_Lic)
        End Get
        Set(ByVal value As String)
            _Lic = clsUEnDe.FEnc(value)
        End Set
    End Property
    Public Property TCP() As Integer
        Get
            Return _TCP
        End Get
        Set(ByVal value As Integer)
            _TCP = value
        End Set
    End Property

    Public Property BillNo() As String
        Get
            Return clsUEnDe.FDec(_BillNo)
        End Get
        Set(ByVal value As String)
            _BillNo = clsUEnDe.FEnc(value)
        End Set
    End Property


    Public Property City() As String
        Get
            Return clsUEnDe.FDec(_City)
        End Get
        Set(ByVal value As String)
            _City = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Username() As String
        Get
            Return clsUEnDe.FDec(_Username)
        End Get
        Set(ByVal value As String)
            _Username = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Address() As String
        Get
            Return clsUEnDe.FDec(_Address)
        End Get
        Set(ByVal value As String)
            _Address = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Mobile() As String
        Get
            Return clsUEnDe.FDec(_Mobile)
        End Get
        Set(ByVal value As String)
            _Mobile = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Email() As String
        Get
            Return clsUEnDe.FDec(_Email)
        End Get
        Set(ByVal value As String)
            _Email = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property DOB() As String
        Get
            Return clsUEnDe.FDec(_DOB)
        End Get
        Set(ByVal value As String)
            _DOB = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Firm() As String
        Get
            Return clsUEnDe.FDec(_Firm)
        End Get
        Set(ByVal value As String)
            _Firm = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property FirmAddress() As String
        Get
            Return clsUEnDe.FDec(_FirmAddress)
        End Get
        Set(ByVal value As String)
            _FirmAddress = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property FirmContactNo() As String
        Get
            Return clsUEnDe.FDec(_FirmContactNo)
        End Get
        Set(ByVal value As String)
            _FirmContactNo = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Reference() As String
        Get
            Return clsUEnDe.FDec(_Reference)
        End Get
        Set(ByVal value As String)
            _Reference = clsUEnDe.FEnc(value)
        End Set
    End Property


    Public Property Product() As String
        Get
            Return clsUEnDe.FDec(_Product)
        End Get
        Set(ByVal value As String)
            _Product = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Allowed() As String
        Get
            Return clsUEnDe.FDec(_Allowed)
        End Get
        Set(ByVal value As String)
            _Allowed = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Limited() As String
        Get
            Return clsUEnDe.FDec(_Limited)
        End Get
        Set(ByVal value As String)
            _Limited = clsUEnDe.FEnc(value)
        End Set
    End Property


    Public Property ExDate() As String
        Get
            Return clsUEnDe.FDec(_ExDate)
        End Get
        Set(ByVal value As String)
            _ExDate = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property Status() As String
        Get
            Return clsUEnDe.FDec(_Status)
        End Get
        Set(ByVal value As String)
            _Status = clsUEnDe.FEnc(value)
        End Set
    End Property

    '==================

    Public Property isAdmin() As String
        Get
            Return clsUEnDe.FDec(_isAdmin)
        End Get
        Set(ByVal value As String)
            _isAdmin = clsUEnDe.FEnc(value)
        End Set
    End Property


    Public Property Userid() As String
        Get
            Return clsUEnDe.FDec(_Userid)
        End Get
        Set(ByVal value As String)
            _Userid = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property pwd() As String
        Get
            Return clsUEnDe.FDec(_pwd)
        End Get
        Set(ByVal value As String)
            _pwd = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property M() As String
        Get
            Return clsUEnDe.FDec(_M)
        End Get
        Set(ByVal value As String)
            _M = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property H() As String
        Get
            Return clsUEnDe.FDec(_H)
        End Get
        Set(ByVal value As String)
            _H = clsUEnDe.FEnc(value)
        End Set
    End Property

    Public Property P() As String
        Get
            Return clsUEnDe.FDec(_P)
        End Get
        Set(ByVal value As String)
            _P = clsUEnDe.FEnc(value)
        End Set
    End Property
    
    Public Function Select_User_Master(ByVal sUserid As String, ByVal sM As String, ByVal sH As String, ByVal sP As String, ByVal sProduct As String, ByVal isEnc As Boolean) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                If sUserid Is Nothing Then sUserid = ""
                If sProduct Is Nothing Then sProduct = ""
                If sM Is Nothing Then sM = ""
                If sH Is Nothing Then sH = ""
                If sP Is Nothing Then sP = ""

                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, sUserid)
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, sProduct)
                MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, sM)
                MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, sH)
                MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, sP)
                MyAccessSQL.Cmd_Text = SP_UserMaster
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & sUserid & sProduct & sM & sH & sP)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            '  Return ObjWebCon.Select_User_Master(sUserid, sM, sH, sP, sProduct, isEnc)
        End If
    End Function
    Public Function Update()
        Dim UpdStr As String = "Update tbl001 Set F7='" & "False" & "' Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(UpdStr)
        ElseIf ConState = "WEBCON" Then
            '  ObjWebCon.Update_Data(UpdStr)
        End If
    End Function
    Public Function Select_User_Master(ByVal isEnc As Boolean) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, "")
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
                MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, "")
                MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, "")
                MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, "")
                MyAccessSQL.Cmd_Text = SP_UserMaster
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            ' Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function
    Public Function Update_User_Master()
        If ConState = "SQLCON" Then
            Dim UpdStr As String
            Dim pretop As String = ObjLoginData.PreOTP + "_" + Now.Date.ToString("ddMMMyyyy")
            UpdStr = "Update tbl001 Set PreOTP='" & pretop & "' Where F2='" & clsUEnDe.FEnc(ObjLoginData.Userid) & "' And F3='" & clsUEnDe.FEnc(ObjLoginData.pwd) & "' And F21='" & clsUEnDe.FEnc(ObjLoginData.M) & "' And F22='" & clsUEnDe.FEnc(ObjLoginData.H) & "' And F23='" & clsUEnDe.FEnc(ObjLoginData.P) & "' And F6 ='" & clsUEnDe.FEnc(ObjLoginData.Product) & "'"
            If ConState = "SQLCON" Then
                MyAccessSQL.ExecuteQuery(UpdStr)
            ElseIf ConState = "WEBCON" Then
                '    ObjWebCon.Update_Data(UpdStr)
            End If

        ElseIf ConState = "WEBCON" Then
            Dim InStr As String = ""
            '    ObjWebCon.Insert_User_Data(_Userid, _pwd, _Username, _Firm, _Product, _Allowed, _Limited, _ExDate, _Status, _Address, _Mobile, _Email, _DOB, _FirmAddress, _FirmContactNo, _Reference, _M, _H, _P, _City, _BillNo)
        End If
        Return ""
    End Function
    'Public Function Select_User_Master1(ByVal isEnc As Boolean, ByVal F2 As String, ByVal F21 As String, ByVal F22 As String, ByVal F23 As String) As DataTable
    '    If ConState = "SQLCON" Then
    '        Try
    '            MyAccessSQL.ParamClear()
    '            MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, F2)
    '            MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
    '            MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, F21)
    '            MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, F22)
    '            MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, F23)
    '            MyAccessSQL.Cmd_Text = Sp_Spf_001Userdata
    '            If isEnc Then
    '                Return MyAccessSQL.FillList()
    '            Else
    '                Return decTable(MyAccessSQL.FillList())
    '            End If

    '        Catch ex As Exception
    '            'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
    '            MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
    '            Return Nothing
    '        End Try

    '    ElseIf ConState = "WEBCON" Then
    '        'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
    '    End If
    'End Function

    Public Function Select_User_Master1(ByVal isEnc As Boolean, ByVal F2 As String, ByVal F3 As String) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, F2)
                MyAccessSQL.AddParam("@F3", SqlDbType.NVarChar, 50, F3)
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))

                MyAccessSQL.Cmd_Text = Sp_Spf_001Userdata
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If


    End Function

    Public Function Select_Supportteam_info(ByVal srno As Int64) As DataTable

        Try
            MyAccessSQL.ParamClear()
            MyAccessSQL.AddParam("@srno", SqlDbType.NVarChar, 50, srno)
            MyAccessSQL.Cmd_Text = Sp_useSupport
            Return MyAccessSQL.FillList()
        Catch ex As Exception
            MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
            Return Nothing
        End Try


    End Function


    Public Function Select_User_Master_all(ByVal isEnc As Boolean, ByVal F2 As String, ByVal F21 As String, ByVal F22 As String, ByVal F23 As String) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, "")
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
                MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, F21)
                MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, F22)
                MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, F23)
                MyAccessSQL.Cmd_Text = Sp_Spf_001Alldata
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function

    Public Sub UpdateActivation(ByVal flg As Boolean)
        Dim UpdStr As String = ""
        If flg Then
            If _BillNo = "MNVX" Then
                Dim dt As New DataTable
                'ObjWebCon.Select_User_Master(sUserid, sM, sH, sP, sProduct, isEnc)
                Dim StrSelect As String = "Select * From BillMaster Where 1 = 1 And F6 = '" & clsUEnDe.FEnc(gstr_ProductName) & "' And F26 = 'MNVX'"
                If ConState = "SQLCON" Then
                    dt = MyAccessSQL.FillDatatable(StrSelect)
                ElseIf ConState = "WEBCON" Then
                    'dt = ObjWebCon.Select_Data(StrSelect)
                End If

                Dim ExD As String = clsUEnDe.FDec(dt.Rows(0)("F9"))
                Dim BD As String = clsUEnDe.FDec(dt.Rows(0)("F16"))
                Dim f27 As String = clsUEnDe.FDec(dt.Rows(0)("F27")) 'lic
                Dim iday As Integer = DateDiff(DateInterval.Day, CDate(BD), CDate(ExD))

                'dt = Select_User_Master(_Userid, _M, _H, _P, _Product, True)
                'BD = clsUEnDe.FDec(dt.Rows(0)("F16"))

                UpdStr = "Update tbl001 Set F7='" & clsUEnDe.FEnc(flg.ToString) & "', F26='" & _BillNo & "', F9 = '" & clsUEnDe.FEnc(DateAdd(DateInterval.Day, iday, Today.Date).ToString("dd-MMM-yyyy")) & "',F27='" & clsUEnDe.FEnc(f27) & "' Where F2='" & _Userid & "' And F3='" & _pwd & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "'"
                If ConState = "SQLCON" Then
                    MyAccessSQL.ExecuteQuery(UpdStr)
                ElseIf ConState = "WEBCON" Then
                    ' ObjWebCon.Update_Data(UpdStr)
                End If

            Else
                UpdStr = "Update tbl001 Set F7='" & clsUEnDe.FEnc(flg.ToString) & "', F26='" & _BillNo & "' Where F2='" & _Userid & "' And F3='" & _pwd & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "'"
                If ConState = "SQLCON" Then
                    MyAccessSQL.ExecuteQuery(UpdStr)
                ElseIf ConState = "WEBCON" Then
                    ' ObjWebCon.Update_Data(UpdStr)
                End If

            End If

        Else
            UpdStr = "Update tbl001 Set F7='" & clsUEnDe.FEnc(flg.ToString) & "', F26='" & "" & "' Where F2='" & _Userid & "' And F3='" & _pwd & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "'"
            If ConState = "SQLCON" Then
                MyAccessSQL.ExecuteQuery(UpdStr)
            ElseIf ConState = "WEBCON" Then
                ' ObjWebCon.Update_Data(UpdStr)
            End If

        End If

    End Sub

    Public Sub SetLoginState(ByVal strStatus As String)
        Dim UpdStr As String = "Update tbl001 Set F10='" & clsUEnDe.FEnc(strStatus) & "' Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"

        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(UpdStr)
        ElseIf ConState = "WEBCON" Then
            '  ObjWebCon.Update_Data(UpdStr)
        End If

        If strStatus = "in" Then
            SetLoginState_Status_in()
        Else
            SetLoginState_Status_out()
        End If
    End Sub

    ''' <summary>
    ''' ExecuteReturn
    ''' </summary>
    ''' <param name="Str"></param>
    ''' <returns></returns>
    ''' <remarks>This method call to return Object according to string</remarks>
    Private Function ExecuteReturn(ByVal Str As String) As Object
        If ConState = "SQLCON" Then
            Return MyAccessSQL.ExecuteReturn(Str)
        ElseIf ConState = "WEBCON" Then
            ' Return ObjWebCon.ExecuteScalar(Str)
        End If
    End Function
    Public Function GetTodayDate() As String
        Try
            Dim str As String = Convert.ToString(ExecuteReturn("Select replace(convert(NVARCHAR, getdate(), 106), ' ', '/')   as TodayDate"))
            'return Convert.ToDateTime(ExecuteReturn("Select DateAdd(mi,750,GetDate()) as TodayDate"));

            '  return Convert.ToDateTime(ExecuteReturn("Select CONVERT(varchar(15), GETDATE(), 103)   as TodayDate"));

            Return str
        Catch ex As Exception
            Return ""
        End Try
    End Function
    'Public Function GetTodayDate() As Date
    '    Try
    '        Return CDate(ExecuteReturn("Select DateAdd(mi,750,GetDate()) as TodayDate"))
    '    Catch ex As Exception

    '    End Try
    'End Function
    Public Function GetLicCnt() As Integer

        Return Val(ExecuteReturn("Select Count(F27) as lic From tbl001 Where F26='" & _BillNo & "'"))

    End Function
    Public Function GetLicNO(ByVal ActCode As String) As Integer

        Return clsUEnDe.FDec(ExecuteReturn("Select F27 From BillMaster Where Right(Convert(varchar,DStamp,112) + replace(Convert(varchar,DStamp,108),':',''),12) + convert(varchar,[UID])='" & ActCode & "'"))

    End Function
    Public Function GetBillNo(ByVal ActCode As String) As String

        Return clsUEnDe.FDec(ExecuteReturn("Select F26 From BillMaster Where Right(Convert(varchar,DStamp,112) + replace(Convert(varchar,DStamp,108),':',''),12) + convert(varchar,[UID])='" & ActCode & "'"))

    End Function
    Public Function GetUserStatus() As String
        Try
            'lbl:
            Dim client As Net.Sockets.TcpClient
            Try
                client = New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                client.Client.Disconnect(True)
                client.Close()
                client = Nothing
            Catch ex As Exception
                If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                    Return "NetWorkProblem"
                    Exit Function
                End If

                'If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                '    GoTo lbl
                'Else
                '    Application.Exit()
                '    End
                'End If
                'Return "out"
                
            End Try

            'Return clsUEnDe.FDec(ExecuteReturn("Select F10 From tbl001 Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"))
            Return clsUEnDe.FDec(ExecuteReturn("Select F7 From tbl001 Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"))
        Catch ex As Exception
            'Return "out"
            Return "ServerDisconnected"

        End Try
    End Function


    Public Sub ThrdSetLoginState_Status_in()
        Dim UpdStr As String = "Update tbl001 Set Status=" & "DateAdd(mi,750,GetDate())" & " Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(UpdStr)
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Update_Data(UpdStr)
        End If
    End Sub

    Public Sub SetLoginState_Status_in()
        Dim UpdStr As String = "Update tbl001 Set Status=" & "DateAdd(mi,750,GetDate())" & " Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(UpdStr)
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Update_Data(UpdStr)
        End If

    End Sub

    Public Sub SetLoginState_Status_out()
        Dim UpdStr As String = "Update tbl001 Set Status=" & "DateAdd(mi,700,GetDate())" & " Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' And F6 ='" & _Product & "' And F26='" & _BillNo & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(UpdStr)
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Update_Data(UpdStr)
        End If
    End Sub


    Public Sub Insert_User_Master()
        If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 100, _Userid)
            MyAccessSQL.AddParam("@F3", SqlDbType.NVarChar, 100, _pwd)
            MyAccessSQL.AddParam("@F4", SqlDbType.NVarChar, 100, _Username)
            MyAccessSQL.AddParam("@F5", SqlDbType.NVarChar, 100, _Firm)
            MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 100, _Product)
            MyAccessSQL.AddParam("@F7", SqlDbType.NVarChar, 100, _Allowed)
            MyAccessSQL.AddParam("@F8", SqlDbType.NVarChar, 100, _Limited)
            MyAccessSQL.AddParam("@F9", SqlDbType.NVarChar, 100, _ExDate)
            MyAccessSQL.AddParam("@F10", SqlDbType.NVarChar, 100, _Status)
            MyAccessSQL.AddParam("@F11", SqlDbType.NVarChar, 100, clsUEnDe.FEnc("False")) 'IsAdmin

            MyAccessSQL.AddParam("@F13", SqlDbType.NVarChar, 100, _Address)
            MyAccessSQL.AddParam("@F14", SqlDbType.NVarChar, 100, _Mobile)
            MyAccessSQL.AddParam("@F15", SqlDbType.NVarChar, 100, _Email)
            MyAccessSQL.AddParam("@F16", SqlDbType.NVarChar, 100, _DOB)
            MyAccessSQL.AddParam("@F17", SqlDbType.NVarChar, 100, _Firm)
            MyAccessSQL.AddParam("@F18", SqlDbType.NVarChar, 100, _FirmAddress)
            MyAccessSQL.AddParam("@F19", SqlDbType.NVarChar, 100, _FirmContactNo)
            MyAccessSQL.AddParam("@F20", SqlDbType.NVarChar, 100, _Reference)
            MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, _M)
            MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, _H)
            MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, _P)
            MyAccessSQL.AddParam("@F24", SqlDbType.NVarChar, 100, _City)
            MyAccessSQL.AddParam("@F25", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(GetTodayDate))

            If _BillNo Is Nothing Then
                BillNo = ""
            End If
            MyAccessSQL.AddParam("@F26", SqlDbType.NVarChar, 100, _BillNo)
            MyAccessSQL.AddParam("@TCP", SqlDbType.NVarChar, 100, "False")
            MyAccessSQL.AddParam("@API", SqlDbType.NVarChar, 100, clsUEnDe.FEnc("False"))
            MyAccessSQL.AddParam("@API_Exp", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(Now.ToString("dd-MMM-yyyy")))
            MyAccessSQL.AddParam("@API_K", SqlDbType.NVarChar, 100, "")
            MyAccessSQL.Cmd_Text = SP_InsertUserMaster
            MyAccessSQL.ExecuteNonQuery()
        ElseIf ConState = "WEBCON" Then
            Dim InStr As String = ""
            If _BillNo Is Nothing Then
                BillNo = ""
            End If
            'ObjWebCon.Insert_User_Data(_Userid, _pwd, _Username, _Firm, _Product, _Allowed, _Limited, _ExDate, _Status, _Address, _Mobile, _Email, _DOB, _FirmAddress, _FirmContactNo, _Reference, _M, _H, _P, _City, _BillNo)
        End If
    End Sub

    Public Sub Delete_User_Master(ByVal uid As String, ByVal sM As String, ByVal sH As String, ByVal sP As String, ByVal sProduct As String)
        If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(uid))
            MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(sProduct))
            MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(sM))
            MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(sH))
            MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, clsUEnDe.FEnc(sP))
            MyAccessSQL.Cmd_Text = SP_DeleteUserMaster
            MyAccessSQL.ExecuteNonQuery()
        ElseIf ConState = "WEBCON" Then
            'ObjWebCon.Delete_User_Data(clsUEnDe.FEnc(uid), clsUEnDe.FEnc(sProduct), clsUEnDe.FEnc(sM), clsUEnDe.FEnc(sH), clsUEnDe.FEnc(sP))
        End If
    End Sub

    'Public Sub ReSetPassword()
    '    MyAccessSQL.ParamClear()
    '    MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 100, _Userid)
    '    MyAccessSQL.AddParam("@F3", SqlDbType.NVarChar, 100, DTSetting.Compute("Max(F2)", "F1='DEFAULTPASS'").ToString())
    '    MyAccessSQL.Cmd_Text = sp_ResetPassword
    '    MyAccessSQL.ExecuteNonQuery()
    'End Sub

    Public Sub LogOutUser()
        If FLG_REG_SERVER_CONN = True Then
            Exit Sub
        End If
        Dim updStr As String = "Update tbl001 Set F10='" & clsUEnDe.FEnc("out") & "' Where F2='" & _Userid & "' And F21='" & _M & "' And F22='" & _H & "' And F23='" & _P & "' and F6='" & _Product & "' And F26='" & _BillNo & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(updStr)
        ElseIf ConState = "WEBCON" Then
            'ObjWebCon.Update_Data(updStr)
        End If
        SetLoginState_Status_out()
    End Sub

    Public Function Update(ByVal colname As String, ByVal colValue As String, ByVal pUserId As String, ByVal sProduct As String, ByVal sM As String, ByVal sH As String, ByVal sP As String) As Boolean
        If colname.ToUpper = "ALLOWED" Or colname.ToUpper = "LIMITED" Or colname.ToUpper = "ISADMIN" Then
            If colValue = "" Then
                colValue = "False"
            End If
        End If
        If colname.ToUpper = "ALLOWED" Then
            If colValue.ToUpper = "TRUE" Then
                Dim AllowCnt As Integer
                Dim DTUserMasterde As New DataTable
                AllowCnt = Select_User_Master(False).Compute("count(F7)", "F7=true")
                'If GFun_CheckLicUserCount(AllowCnt) = False Then
                '    Return False
                '    Exit Function
                'End If
            End If
        End If


        If colname.ToUpper = "ALLOWED" Then
            colname = "F7"
        ElseIf colname.ToUpper = "LIMITED" Then
            colname = "F8"
        ElseIf colname.ToUpper = "ISADMIN" Then
            colname = "F11"
        ElseIf colname.ToUpper = "EXDATE" Then
            colname = "F9"
        End If

        'MyAccessSQL.ExecuteQuery("Update UserMaster Set " & colname & "='" & clsEnDe.FEnc(colValue) & "' Where Userid='" & clsEnDe.FEnc(pUserId) & "'")
        Dim updStr As String = "Update tbl001 Set " & colname & "='" & clsUEnDe.FEnc(colValue) & "' Where F2='" & clsUEnDe.FEnc(pUserId) & "' And F6='" & clsUEnDe.FEnc(sProduct) & "' And F21='" & clsUEnDe.FEnc(sM) & "' And F22='" & clsUEnDe.FEnc(sH) & "' And F23='" & clsUEnDe.FEnc(sP) & "'"
        If ConState = "SQLCON" Then
            MyAccessSQL.ExecuteQuery(updStr)
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Update_Data(updStr)
        End If
        Return True
    End Function

    'Public Sub ReSetPassword()
    '    MyAccessSQL.ParamClear()
    '    MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 100, _Userid)
    '    MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 100, _Product)
    '    MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, _M)
    '    MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, _H)
    '    MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, _P)
    '    MyAccessSQL.AddParam("@F3", SqlDbType.NVarChar, 100, DTSetting.Compute("Max(F2)", "F1='DEFAULTPASS'").ToString())
    '    MyAccessSQL.Cmd_Text = sp_ResetPassword
    '    MyAccessSQL.ExecuteNonQuery()
    'End Sub

    Public Sub New()

    End Sub

    Public Sub Insert_Range_Data(ByVal DtColProfile As DataTable)
        If ConState = "SQLCON" Then


            Try
                MyAccessSQL.ParamClear()
                For Each Dr As DataRow In DtColProfile.Rows
                    MyAccessSQL.AddParam("@DATE", SqlDbType.DateTime, 18, Dr("Date"))
                    MyAccessSQL.AddParam("@SYMBOL", SqlDbType.NVarChar, 18, Dr("Symbol"))
                    MyAccessSQL.AddParam("@CLOSEPRICE", SqlDbType.NVarChar, 18, Dr("ClosePrice"))
                    MyAccessSQL.AddParam("@PRECLOSEPRICE", SqlDbType.NVarChar, 18, Dr("PreClosePrice"))
                    MyAccessSQL.AddParam("@LOGRETURNS", SqlDbType.NVarChar, 18, Dr("LogReturns"))
                    MyAccessSQL.AddParam("@PREDAYVOL", SqlDbType.NVarChar, 18, Dr("PreDayVol"))
                    MyAccessSQL.AddParam("@CURDAYVOL", SqlDbType.NVarChar, 18, Dr("CurDayVol"))
                    MyAccessSQL.AddParam("@ANNUALVOL", SqlDbType.NVarChar, 18, Dr("AnnualVol"))
                    MyAccessSQL.AddParam("@FUTCLOSEPRICE", SqlDbType.NVarChar, 18, Dr("FutClosePrice"))
                    MyAccessSQL.AddParam("@FUTPREDAYCLOSE", SqlDbType.NVarChar, 18, Dr("FutPreDayClose"))
                    MyAccessSQL.AddParam("@FUTLOGRETURNS", SqlDbType.NVarChar, 18, Dr("FutLogReturns"))
                    MyAccessSQL.AddParam("@PREDAYFUTVOL", SqlDbType.NVarChar, 18, Dr("PreDayFutVol"))
                    MyAccessSQL.AddParam("@CURDAYFUTVOL", SqlDbType.NVarChar, 18, Dr("CurDayFutVol"))
                    MyAccessSQL.AddParam("@FUTANNUALVOL", SqlDbType.NVarChar, 18, Dr("FutAnnualVol"))
                    MyAccessSQL.AddParam("@AppliDailyVol", SqlDbType.NVarChar, 18, Dr("AppliDailyVol"))
                    MyAccessSQL.AddParam("@AppliAnnualVol", SqlDbType.NVarChar, 18, Dr("AppliAnnualVol"))
                Next
                MyAccessSQL.Cmd_Text = sp_InsertRC
                MyAccessSQL.ExecuteMultipleRC(16)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Insert_Range_Data(DtColProfile)
        End If
    End Sub

    Public Sub update_local_Range_Data(ByVal DtColProfile As DataTable)
        If ConState = "SQLCON" Then


            Try
                MyAccessSQL.ParamClear()
                For Each Dr As DataRow In DtColProfile.Rows
                    MyAccessSQL.AddParam("@DATE", SqlDbType.DateTime, 18, Dr("Date"))
                    MyAccessSQL.AddParam("@SYMBOL", SqlDbType.NVarChar, 18, Dr("Symbol"))
                    MyAccessSQL.AddParam("@CLOSEPRICE", SqlDbType.NVarChar, 18, Dr("ClosePrice"))
                    MyAccessSQL.AddParam("@PRECLOSEPRICE", SqlDbType.NVarChar, 18, Dr("PreClosePrice"))
                    MyAccessSQL.AddParam("@LOGRETURNS", SqlDbType.NVarChar, 18, Dr("LogReturns"))
                    MyAccessSQL.AddParam("@PREDAYVOL", SqlDbType.NVarChar, 18, Dr("PreDayVol"))
                    MyAccessSQL.AddParam("@CURDAYVOL", SqlDbType.NVarChar, 18, Dr("CurDayVol"))
                    MyAccessSQL.AddParam("@ANNUALVOL", SqlDbType.NVarChar, 18, Dr("AnnualVol"))
                    MyAccessSQL.AddParam("@FUTCLOSEPRICE", SqlDbType.NVarChar, 18, Dr("FutClosePrice"))
                    MyAccessSQL.AddParam("@FUTPREDAYCLOSE", SqlDbType.NVarChar, 18, Dr("FutPreDayClose"))
                    MyAccessSQL.AddParam("@FUTLOGRETURNS", SqlDbType.NVarChar, 18, Dr("FutLogReturns"))
                    MyAccessSQL.AddParam("@PREDAYFUTVOL", SqlDbType.NVarChar, 18, Dr("PreDayFutVol"))
                    MyAccessSQL.AddParam("@CURDAYFUTVOL", SqlDbType.NVarChar, 18, Dr("CurDayFutVol"))
                    MyAccessSQL.AddParam("@FUTANNUALVOL", SqlDbType.NVarChar, 18, Dr("FutAnnualVol"))
                    MyAccessSQL.AddParam("@AppliDailyVol", SqlDbType.NVarChar, 18, Dr("AppliDailyVol"))
                    MyAccessSQL.AddParam("@AppliAnnualVol", SqlDbType.NVarChar, 18, Dr("AppliAnnualVol"))
                Next
                MyAccessSQL.Cmd_Text = sp_InsertRC
                MyAccessSQL.ExecuteMultiple(16)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.Insert_Range_Data(DtColProfile)
        End If
    End Sub
    Public Function GETMARGIN(ByVal str As String, ByVal iscurr As Boolean) As DataTable
        Try

            Dim DT As New DataTable
            'Dim ddt As New DataTable
            'If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.AddParam("@strpos", SqlDbType.NVarChar, 1000, str)
            If iscurr = True Then
                MyAccessSQL.Cmd_Text = Sp_getcurrmargin
            Else
                MyAccessSQL.Cmd_Text = Sp_getmargin
            End If

            DT = MyAccessSQL.FillList()
            'ElseIf ConState = "WEBCON" Then
            'DT = ObjWebCon.SelectRangeData(ddt, FromDate, ToDate)
            'End If

            Return DT

        Catch ex As Exception

        End Try
    End Function
    Public Function GETtblExposure() As DataTable
        Dim DT As New DataTable
        'Dim ddt As New DataTable
        'If ConState = "SQLCON" Then
        MyAccessSQL.ParamClear()
        'MyAccessSQL.AddParam("@strpos", SqlDbType.NVarChar, 1000, str)

        MyAccessSQL.Cmd_Text = Sp_tblExposure
        DT = MyAccessSQL.FillList()
        'ElseIf ConState = "WEBCON" Then
        'DT = ObjWebCon.SelectRangeData(ddt, FromDate, ToDate)
        'End If

        Return DT
    End Function
    Public Function Select_Range_Data(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim DT As New DataTable
        'Dim ddt As New DataTable
        'If ConState = "SQLCON" Then
        MyAccessSQL.ParamClear()
        MyAccessSQL.AddParam("@FromDate", SqlDbType.DateTime, 18, fDate(CDate(FromDate)))
        MyAccessSQL.AddParam("@ToDate", SqlDbType.DateTime, 18, fDate(CDate(ToDate)))
        MyAccessSQL.Cmd_Text = sp_SelectRC
        DT = MyAccessSQL.FillList()
        'ElseIf ConState = "WEBCON" Then
        'DT = ObjWebCon.SelectRangeData(ddt, FromDate, ToDate)
        'End If

        Return DT
    End Function

    Public Function Select_dailyTip() As DataTable
        'dtDailyTip()
        Dim DT As New DataTable

        If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.Cmd_Text = "Spf_DailyTip"
            DT = MyAccessSQL.FillList()
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.SelectDailyTip(DT)
        End If

        Return DT

    End Function

    Public Function Select_Setting() As DataTable
        'dtDailyTip()
        Dim DT As New DataTable
        If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.Cmd_Text = "Spf_003"
            DT = MyAccessSQL.FillList()
        ElseIf ConState = "WEBCON" Then
            ' ObjWebCon.SelectSetting(DT)
        End If

        Return DT

    End Function

    Public Function User_Master_Update()
        Dim rowsAffected As Integer = 0
        If ConState = "SQLCON" Then
            MyAccessSQL.ParamClear()
            MyAccessSQL.AddParam("@Email", SqlDbType.NVarChar, 100, _Userid)
            MyAccessSQL.AddParam("@Password", SqlDbType.NVarChar, 100, _pwd)

            MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, _M)
            MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, _H)
            MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, _P)
            MyAccessSQL.Cmd_Text = SP_UpdateUserMaster
            rowsAffected = MyAccessSQL.ExecuteNonQuery()
            Return rowsAffected
        End If
    End Function

    Public Function Select_Regesterd_User(ByVal isEnc As Boolean, ByVal Email As String)
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 100, Email)
                MyAccessSQL.Cmd_Text = Sp_Spf_001CheckUser
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function
    Public Function Select_Regesterd_User_Phone(ByVal isEnc As Boolean, ByVal Phone As String)
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
                MyAccessSQL.AddParam("@F14", SqlDbType.NVarChar, 100, Phone)
                MyAccessSQL.Cmd_Text = Sp_Spf_001CheckUserPh
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function
    Public Function UniqueNumber(ByVal characters As String) As String
        Dim unique1 As New Random()
        Dim s As String = "IN"
        Dim unique As Integer
        Dim n As Integer = 0

        While n < 10
            If n Mod 2 = 0 Then
                s &= unique1.Next(10).ToString()
            Else
                unique = unique1.Next(52)
                If unique < characters.Length Then
                    s = String.Concat(s, characters(unique))
                End If
            End If

            n += 1
        End While

        Return s
    End Function

    Public Sub GenerateAndStoreUniqueNumber(ByVal UserName As String, ByVal password As String)

        Dim characters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"



        ' Call the UniqueNumber function and pass the characters string as an argument
        Dim uniqueNumber2 As String = UniqueNumber(characters)
        'Dim uniqueNumber1 As String = F2 + F21 + F22 + F23
        'Dim uniqueNumber As String = "1234"
        clsGlobal.SetLoginId(uniqueNumber2)
        clsGlobal.SetLoginUser(UserName)
        Dim Date2 As String = Date.Now.ToString("MM/dd/yyyy h:mm:ss tt")
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                Dim Query As String = "Exec Sp_UpdateLoginKeyAndLastLogin '" + UserName + "','" + clsUEnDe.FEnc(gstr_ProductName) + "', '" + password + "' ,'" + uniqueNumber2 + "', '" + Date2 + "'"


                MyAccessSQL.ExecuteQuery(Query)
            Catch ex As Exception
                MsgBox("Loginid update failed")
            End Try
        End If
        clsGlobal.SetUsername(UserName)
        clsGlobal.SetPassword(password)

    End Sub

    Public Function verifyemail(ByVal isEnc As Boolean, ByVal F2 As String, ByVal F21 As String, ByVal F22 As String, ByVal F23 As String) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, "")
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))
                MyAccessSQL.AddParam("@F21", SqlDbType.NVarChar, 100, F21)
                MyAccessSQL.AddParam("@F22", SqlDbType.NVarChar, 100, F22)
                MyAccessSQL.AddParam("@F23", SqlDbType.NVarChar, 100, F23)
                MyAccessSQL.Cmd_Text = sp_verifyemail
                If isEnc Then
                    Return MyAccessSQL.FillList()
                Else
                    Return decTable(MyAccessSQL.FillList())
                End If

            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function

    Public Function LoginData(ByVal isEnc As Boolean, ByVal F2 As String, ByVal F3 As String) As DataTable
        If ConState = "SQLCON" Then
            Try
                MyAccessSQL.ParamClear()
                MyAccessSQL.AddParam("@F2", SqlDbType.NVarChar, 50, F2)
                MyAccessSQL.AddParam("@F3", SqlDbType.NVarChar, 50, F3)
                MyAccessSQL.AddParam("@F6", SqlDbType.NVarChar, 50, clsUEnDe.FEnc(gstr_ProductName))

                MyAccessSQL.Cmd_Text = Sp_Spf_001Userdata

                Return MyAccessSQL.FillList()


            Catch ex As Exception
                'MsgBox("Logindata::Select_User_Master() :: " & ex.ToString)
                MsgBox("Logindata::Select_User_Master() :: " & ex.ToString & gstr_ProductName)
                Return Nothing
            End Try

        ElseIf ConState = "WEBCON" Then
            'Return ObjWebCon.Select_User_Master("", "", "", "", clsUEnDe.FEnc(gstr_ProductName), isEnc)
        End If
    End Function



End Class
