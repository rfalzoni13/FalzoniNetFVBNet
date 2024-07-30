Imports System.Configuration

Namespace Helpers
    Public NotInheritable Class ConfigurationHelper
        Public Shared Property ProviderName As String

        Public Shared ReadOnly Property IsBundleled As Boolean
            Get
                Return Convert.ToBoolean(ConfigurationManager.AppSettings("IsBundleled"))
            End Get
        End Property
    End Class
End Namespace
