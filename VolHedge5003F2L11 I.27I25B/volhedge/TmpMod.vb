
Module TmpMod
    'Sub ExcelToHtmlMain(ByVal args As String())
    '    'Imports GemBox.Spreadsheet
    '    ' TODO: If using GemBox.Spreadsheet Professional, put your serial key below.
    '    ' Otherwise, if you are using GemBox.Spreadsheet Free, comment out the 
    '    ' following line (Free version doesn't have SetLicense method). 
    '    'SpreadsheetInfo.SetLicense("YOUR-SERIAL-KEY-HERE")

    '    Dim excelFile As New ExcelFile
    '    Dim inputFileName As String = "..\..\HtmlExportSampleIN.xls"
    '    Dim outputFileName As String = "..\..\HtmlExportSampleOUT.html"

    '    excelFile.LoadXls(inputFileName, XlsOptions.None)

    '    'Here you can set HTML exporter options.
    '    Dim options As New HtmlExporterOptions

    '    options.ShowColumnLetters = True
    '    options.ShowRowNumbers = True

    '    excelFile.Worksheets.ActiveWorksheet.GetUsedCellRange.ExportToHtml(outputFileName, options, True)

    '    ' You can use simple ExcelFile.SaveHtml method for the same result.
    '    'excelFile.SaveHtml(outputFileName, Nothing, True)

    '    TryToDisplayGeneratedFile(outputFileName)
    'End Sub


    'Private Sub TryToDisplayGeneratedFile(ByVal fileName As String)
    '    Try
    '        Process.Start(fileName)
    '    Catch exception1 As Exception
    '        Console.WriteLine((fileName & " created in application folder."))
    '    End Try
    'End Sub

End Module
