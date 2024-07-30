Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration

Namespace Clients.Interfaces.Configuration
    Public Interface IUserClient
        Inherits IBaseClient(Of UserModel, UserTableModel)
    End Interface
End Namespace
