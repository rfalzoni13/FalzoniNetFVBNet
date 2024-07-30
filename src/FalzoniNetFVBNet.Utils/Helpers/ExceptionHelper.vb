Imports System.Data.SqlClient

Namespace Helpers
    Public Class ExceptionHelper
        Public Shared Function CatchMessageFromException(ex As Exception) As String
            If ex.InnerException IsNot Nothing Then
                Return ex.InnerException.Message
            End If

            Select Case ex.GetType()
                Case GetType(ApplicationException)
                    Return ex.Message
                Case GetType(SqlException)

#If DEBUG Then
                    Return ex.Message
#Else
                    Return "Erro de comunicação com o servidor! Por favor, tente novamente mais tarde!"
#End If
                Case GetType(TaskCanceledException)
                    Return "Erro de comunicação com o servidor! Por favor, tente novamente mais tarde!"
                Case Else
#If DEBUG Then
                    Return ex.Message
#Else
                    Return "Ocorreu um erro ao processar sua solicitação!";
#End If
            End Select
        End Function
    End Class
End Namespace
