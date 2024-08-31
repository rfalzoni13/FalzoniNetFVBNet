Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports FalzoniNetFVBNet.Service.Base
Imports NLog

Namespace Controllers.Base
    Public Class BaseController(Of TDTO As Class)
        Inherits ApiController

#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _serviceBase As ServiceBase(Of TDTO)
#End Region

#Region "Constructor"
        Public Sub New(serviceBase As ServiceBase(Of TDTO))
            _serviceBase = serviceBase
        End Sub
#End Region

#Region "Getters"
        ' GET Api/GetAll
        <HttpGet>
        Public Overridable Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim retorno = _serviceBase.GetAll()

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Get/{Id}
        <HttpGet>
        Public Overridable Function [Get](Id As Guid) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                If Guid.Equals(Id, Guid.Empty) Then
                    Throw New ApplicationException("Parâmetro inválido")
                End If

                Dim user = _serviceBase.Get(Id)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, user)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Add"
        ' POST Api/Add
        <HttpPost>
        Public Overridable Function Add(<FromBody> dto As TDTO) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    _serviceBase.Add(dto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Registro incluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST Api/AddAsync
        '<HttpPost>
        'Public Overridable Async Function AddAsync(<FromBody> dto As TDTO) As Task(Of HttpResponseMessage)
        '    Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Await _serviceBase.AddAsync(dto)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Registro incluído com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try
        'End Function
#End Region

#Region "Update"
        ' PUT Api/Update
        <HttpPut>
        Public Overridable Function Update(<FromBody> dto As TDTO) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    _serviceBase.Update(dto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Registro atualizado com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' PUT Api/UpdateAsync
        '<HttpPut>
        'Public Overridable Async Function UpdateAsync(<FromBody> dto As TDTO) As Task(Of HttpResponseMessage)
        '    Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Await _serviceBase.UpdateAsync(dto)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.OK, "Registro atualizado com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try
        'End Function
#End Region

#Region "Delete"
        ' DELETE Api/Delete
        <HttpDelete>
        Public Overridable Function Delete(<FromUri> Id As Guid) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    _serviceBase.Delete(Id)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Registro excluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' DELETE Api/DeleteAsync
        '<HttpDelete>
        'Public Overridable Async Function DeleteAsync(<FromUri> Id As Guid) As Task(Of HttpResponseMessage)
        '    Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Await _serviceBase.DeleteAsync(Id)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.OK, "Registro excluído com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try

        'End Function
#End Region
    End Class
End Namespace