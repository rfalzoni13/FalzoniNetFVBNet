Public NotInheritable Class PathUtils
    Public Shared _pathUrlApi As String

    Public Shared Sub LoadPath()
        _pathUrlApi = If(Debugger.IsAttached,
                ConfigurationManager.AppSettings("PathLocal"),
                ConfigurationManager.AppSettings("PathProduction"))
    End Sub

    Public Shared Function GetApiPath() As String
        Return _pathUrlApi
    End Function
End Class
