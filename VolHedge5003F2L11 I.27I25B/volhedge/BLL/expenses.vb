Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class expenses
#Region "variable"
  
    Dim _ndbl As Double
    Dim _ndblp As Double
    Dim _ndbs As Double
    Dim _ndbsp As Double
    Dim _dbl As Double
    Dim _dblp As Double
    Dim _dbs As Double
    Dim _dbsp As Double
    Dim _futl As Double
    Dim _futlp As Double
    Dim _futs As Double
    Dim _futsp As Double
    Dim _spl As Double
    Dim _splp As Double
    Dim _sps As Double
    Dim _spsp As Double
    Dim _pl As Double
    Dim _plp As Double
    Dim _ps As Double
    Dim _psp As Double

    Dim _currfutl As Double
    Dim _currfutlp As Double
    Dim _currfuts As Double
    Dim _currfutsp As Double
    Dim _currspl As Double
    Dim _currsplp As Double
    Dim _currsps As Double
    Dim _currspsp As Double
    Dim _currpl As Double
    Dim _currplp As Double
    Dim _currps As Double
    Dim _currpsp As Double

    Dim _equity As Double
    Dim _fo As Double
    Dim _sttrate As Double
     

#End Region
#Region "Property"
   
    Public Property NDBL() As Double
        Get
            Return _ndbl
        End Get
        Set(ByVal value As Double)
            _ndbl = value
        End Set
    End Property
    Public Property NDBLP() As Double
        Get
            Return _ndblp
        End Get
        Set(ByVal value As Double)
            _ndblp = value
        End Set
    End Property
    Public Property NDBS() As Double
        Get
            Return _ndbs
        End Get
        Set(ByVal value As Double)
            _ndbs = value
        End Set
    End Property
    Public Property NDBSP() As Double
        Get
            Return _ndbsp
        End Get
        Set(ByVal value As Double)
            _ndbsp = value
        End Set
    End Property
    Public Property DBL() As Double
        Get
            Return _dbl
        End Get
        Set(ByVal value As Double)
            _dbl = value
        End Set
    End Property
    Public Property DBLP() As Double
        Get
            Return _dblp
        End Get
        Set(ByVal value As Double)
            _dblp = value
        End Set
    End Property
    Public Property DBS() As Double
        Get
            Return _dbs
        End Get
        Set(ByVal value As Double)
            _dbs = value
        End Set
    End Property
    Public Property DBSP() As Double
        Get
            Return _dbsp
        End Get
        Set(ByVal value As Double)
            _dbsp = value
        End Set
    End Property
    Public Property FUTL() As Double
        Get
            Return _futl
        End Get
        Set(ByVal value As Double)
            _futl = value
        End Set
    End Property
    Public Property FUTLP() As Double
        Get
            Return _futlp
        End Get
        Set(ByVal value As Double)
            _futlp = value
        End Set
    End Property
    Public Property FUTS() As Double
        Get
            Return _futs
        End Get
        Set(ByVal value As Double)
            _futs = value
        End Set
    End Property
    Public Property FUTSP() As Double
        Get
            Return _futsp
        End Get
        Set(ByVal value As Double)
            _futsp = value
        End Set
    End Property
    Public Property SPL() As Double
        Get
            Return _spl
        End Get
        Set(ByVal value As Double)
            _spl = value
        End Set
    End Property
    Public Property SPLP() As Double
        Get
            Return _splp
        End Get
        Set(ByVal value As Double)
            _splp = value
        End Set
    End Property
    Public Property SPS() As Double
        Get
            Return _sps
        End Get
        Set(ByVal value As Double)
            _sps = value
        End Set
    End Property
    Public Property SPSP() As Double
        Get
            Return _spsp
        End Get
        Set(ByVal value As Double)
            _spsp = value
        End Set
    End Property
    Public Property PL() As Double
        Get
            Return _pl
        End Get
        Set(ByVal value As Double)
            _pl = value
        End Set
    End Property
    Public Property PLP() As Double
        Get
            Return _plp
        End Get
        Set(ByVal value As Double)
            _plp = value
        End Set
    End Property
    Public Property PS() As Double
        Get
            Return _ps
        End Get
        Set(ByVal value As Double)
            _ps = value
        End Set
    End Property
    Public Property PSP() As Double
        Get
            Return _psp
        End Get
        Set(ByVal value As Double)
            _psp = value
        End Set
    End Property

    'currency
    Public Property CURRFUTL() As Double
        Get
            Return _currfutl
        End Get
        Set(ByVal value As Double)
            _currfutl = value
        End Set
    End Property
    Public Property CURRFUTLP() As Double
        Get
            Return _currfutlp
        End Get
        Set(ByVal value As Double)
            _currfutlp = value
        End Set
    End Property
    Public Property CURRFUTS() As Double
        Get
            Return _currfuts
        End Get
        Set(ByVal value As Double)
            _currfuts = value
        End Set
    End Property
    Public Property CURRFUTSP() As Double
        Get
            Return _currfutsp
        End Get
        Set(ByVal value As Double)
            _currfutsp = value
        End Set
    End Property
    Public Property CURRSPL() As Double
        Get
            Return _currspl
        End Get
        Set(ByVal value As Double)
            _currspl = value
        End Set
    End Property
    Public Property CURRSPLP() As Double
        Get
            Return _currsplp
        End Get
        Set(ByVal value As Double)
            _currsplp = value
        End Set
    End Property
    Public Property CURRSPS() As Double
        Get
            Return _currsps
        End Get
        Set(ByVal value As Double)
            _currsps = value
        End Set
    End Property
    Public Property CURRSPSP() As Double
        Get
            Return _currspsp
        End Get
        Set(ByVal value As Double)
            _currspsp = value
        End Set
    End Property
    Public Property CURRPL() As Double
        Get
            Return _currpl
        End Get
        Set(ByVal value As Double)
            _currpl = value
        End Set
    End Property
    Public Property CURRPLP() As Double
        Get
            Return _currplp
        End Get
        Set(ByVal value As Double)
            _currplp = value
        End Set
    End Property
    Public Property CURRPS() As Double
        Get
            Return _currps
        End Get
        Set(ByVal value As Double)
            _currps = value
        End Set
    End Property
    Public Property CURRPSP() As Double
        Get
            Return _currpsp
        End Get
        Set(ByVal value As Double)
            _currpsp = value
        End Set
    End Property

    Public Property EQUITY() As Double
        Get
            Return _equity
        End Get
        Set(ByVal value As Double)
            _equity = value
        End Set
    End Property
    Public Property FO() As Double
        Get
            Return _fo
        End Get
        Set(ByVal value As Double)
            _fo = value
        End Set
    End Property
    Public Property STTRATE() As Double
        Get
            Return _sttrate
        End Get
        Set(ByVal value As Double)
            _sttrate = value
        End Set
    End Property
#End Region
#Region "SP"
    Private Const SP_UPDATE_EXPENSES As String = "update_expenses"
    Private Const SP_SELECT_EXPENSES As String = "select_expenses"
    Private Const SP_Select_Equity_ExpenseCalc As String = "Select_Equity_ExpenseCalc"
#End Region
    Public Sub update()
        exptable = Select_Expenses()
        'If exptable.Rows.Count = 0 Then
        '    Dim qry As String = "Insert Into expenses_setting (ndbl, ndblp, ndbs, ndbsp, dbl, dblp, dbs, dbsp, futl, futlp, futs, futsp, spl, splp, sps , spsp, prel, prelp, pres, presp, currfutl , currfutlp , currfuts, currfutsp , currspl , currsplp , currsps , currspsp , currprel , currprelp , currpres , currpresp , equity , fo, sttrate) values (ndbl, ndblp, ndbs, ndbsp, dbl, dblp, dbs, dbsp, futl, futlp, futs, futsp, spl, splp, sps , spsp, prel, prelp, pres, presp, currfutl , currfutlp , currfuts, currfutsp , currspl , currsplp , currsps , currspsp , currprel , currprelp , currpres , currpresp , equity , fo, sttrate);"
        '    data_access.ParamClear()
        '    data_access.Cmd_Text = qry
        '    data_access.cmd_type = CommandType.Text
        '    data_access.ExecuteNonQuery()
        '    data_access.cmd_type = CommandType.StoredProcedure
        '    Exit Sub
        'End If
        
        data_access.ParamClear()
        data_access.AddParam("@ndbl", OleDbType.Double, 18, NDBL)
        data_access.AddParam("@ndblp", OleDbType.Double, 18, NDBLP)
        data_access.AddParam("@ndbs", OleDbType.Double, 18, NDBS)
        data_access.AddParam("@ndbsp", OleDbType.Double, 18, NDBSP)
        data_access.AddParam("@dbl", OleDbType.Double, 18, DBL)
        data_access.AddParam("@dblp", OleDbType.Double, 18, DBLP)
        data_access.AddParam("@dbs", OleDbType.Double, 18, DBS)
        data_access.AddParam("@dbsp", OleDbType.Double, 18, DBSP)
        data_access.AddParam("@futl", OleDbType.Double, 18, FUTL)
        data_access.AddParam("@futlp", OleDbType.Double, 18, FUTLP)
        data_access.AddParam("@futs", OleDbType.Double, 18, FUTS)
        data_access.AddParam("@futsp", OleDbType.Double, 18, FUTSP)
        data_access.AddParam("@spl", OleDbType.Double, 18, SPL)
        data_access.AddParam("@splp", OleDbType.Double, 18, SPLP)
        data_access.AddParam("@sps", OleDbType.Double, 18, SPS)
        data_access.AddParam("@spsp", OleDbType.Double, 18, SPSP)
        data_access.AddParam("@pl", OleDbType.Double, 18, PL)
        data_access.AddParam("@plp", OleDbType.Double, 18, PLP)
        data_access.AddParam("@ps", OleDbType.Double, 18, PS)
        data_access.AddParam("@psp", OleDbType.Double, 18, PSP)

        'currency
        data_access.AddParam("@currfutl", OleDbType.Double, 18, CURRFUTL)
        data_access.AddParam("@currfutlp", OleDbType.Double, 18, CURRFUTLP)
        data_access.AddParam("@currfuts", OleDbType.Double, 18, CURRFUTS)
        data_access.AddParam("@currfutsp", OleDbType.Double, 18, CURRFUTSP)
        data_access.AddParam("@currspl", OleDbType.Double, 18, CURRSPL)
        data_access.AddParam("@currsplp", OleDbType.Double, 18, CURRSPLP)
        data_access.AddParam("@currsps", OleDbType.Double, 18, CURRSPS)
        data_access.AddParam("@currspsp", OleDbType.Double, 18, CURRSPSP)
        data_access.AddParam("@currpl", OleDbType.Double, 18, CURRPL)
        data_access.AddParam("@currplp", OleDbType.Double, 18, CURRPLP)
        data_access.AddParam("@currps", OleDbType.Double, 18, CURRPS)
        data_access.AddParam("@currpsp", OleDbType.Double, 18, CURRPSP)

        data_access.AddParam("@equity", OleDbType.Double, 18, EQUITY)
        data_access.AddParam("@fo", OleDbType.Double, 18, FO)
        data_access.AddParam("@sttrate", OleDbType.Double, 18, STTRATE)

        data_access.Cmd_Text = SP_UPDATE_EXPENSES
        data_access.ExecuteNonQuery()

    End Sub

    Public Function Select_Expenses() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_EXPENSES
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Expense :: Select_Expenses ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function Select_Equity_ExpenseCalc() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_Equity_ExpenseCalc
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Expense :: Select_Equity_ExpenseCalc() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
End Class
