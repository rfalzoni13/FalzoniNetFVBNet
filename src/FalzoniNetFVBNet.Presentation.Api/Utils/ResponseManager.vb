Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Presentation.Api.Models.Common
Imports FalzoniNetFVBNet.Utils.Helpers
Imports NLog

Namespace Utils
    Public NotInheritable Class ResponseManager
        Public Shared Function ReturnExceptionNotFound(ex As HttpResponseException, request As HttpRequestMessage, logger As Logger, action As String, message As String) As HttpResponseMessage

            logger.Warn(action + " - Error: " + ex.ToString())

            Dim status As StatusCodeModel = New StatusCodeModel With
            {
                .Status = ex.Response.StatusCode,
                .Message = "Nenhum registro encontrado!"
            }

            logger.Info(action + " - Finalizado")
            Return request.CreateResponse(HttpStatusCode.NotFound, status)
        End Function

        Public Shared Function ReturnExceptionInternalServerError(ex As Exception, request As HttpRequestMessage, logger As Logger, action As String) As HttpResponseMessage
            logger.Error(action + " - Error: " + ex.ToString())

            Dim status As StatusCodeModel = New StatusCodeModel With
            {
                .Status = HttpStatusCode.InternalServerError,
                .Message = ExceptionHelper.CatchMessageFromException(ex)
            }

            logger.Info(action + " - Finalizado")
            Return request.CreateResponse(HttpStatusCode.InternalServerError, status)
        End Function


        Public Shared Function ReturnBadRequest(request As HttpRequestMessage, logger As Logger, action As String, message As String) As HttpResponseMessage
            logger.Warn(action + " - " + message)

            Dim status As StatusCodeModel = New StatusCodeModel With
            {
                .Status = HttpStatusCode.BadRequest,
                .Message = message
            }

            logger.Info(action + " - Finalizado")
            Return request.CreateResponse(HttpStatusCode.BadRequest, status)
        End Function


        Public Shared Function ReturnErrorResult(request As HttpRequestMessage, logger As Logger, action As String, errors As IEnumerable(Of String)) As HttpResponseMessage
            Dim message As String = String.Empty

            Dim i As Integer = 0

            Dim errorsArray As String() = errors.ToArray()

            Do
                message += errorsArray(i) + " "
                i += 1
            Loop While i < errorsArray.Count()

            logger.Warn(action + " - " + message)

            Dim status As StatusCodeModel = New StatusCodeModel With
            {
                .Status = HttpStatusCode.BadRequest,
                .ErrorsResult = errorsArray
            }

            logger.Info(action + " - Finalizado")
            Return request.CreateResponse(HttpStatusCode.BadRequest, status)
        End Function

    End Class
End Namespace

