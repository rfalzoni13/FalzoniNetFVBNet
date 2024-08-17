Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration

Namespace Clients.Interfaces.Configuration
    Public Interface IRoleClient
        Inherits IBaseClient(Of RoleModel)

        Function GetAllNames() As List(Of String)
    End Interface
End Namespace
