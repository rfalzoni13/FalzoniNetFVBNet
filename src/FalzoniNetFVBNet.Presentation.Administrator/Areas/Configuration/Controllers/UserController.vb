Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Attributes
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Controllers.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration
Imports FalzoniNetFVBNet.Utils.Helpers
Imports NLog

Namespace Areas.Configuration.Controllers
    <Authorize>
    Public Class UserController
        Inherits BaseController(Of UserModel)

        Private ReadOnly _userClient As IUserClient

        Public Sub New(userClient As IUserClient)
            MyBase.New(userClient)
            _userClient = userClient
        End Sub

        'GET Register/User/LoadTable
        <HttpGet>
        Public Async Function LoadTable() As Task(Of JsonResult)

            Dim table = New UserTableModel()

            Try

                table = Await _userClient.GetTableAsync()

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
            End Try

            Return Json(table, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace