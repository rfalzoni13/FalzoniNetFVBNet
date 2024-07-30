Namespace Helpers
    Public NotInheritable Class GuidHelper
        Public Shared Function StringToGuid(value As String) As Guid
            Dim result As Guid = Guid.Empty
            Guid.TryParse(value, result)
            Return result
        End Function

        Public Shared Function GuidToString(g As Guid) As String
            Return g.ToString()
        End Function
    End Class
End Namespace
