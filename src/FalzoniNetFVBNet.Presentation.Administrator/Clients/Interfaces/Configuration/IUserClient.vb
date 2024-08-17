Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration

Namespace Clients.Interfaces.Configuration
    Public Interface IUserClient
        Inherits IBaseClient(Of UserModel)
        Function GetTableAsync(url As String) As Task(Of UserTableModel)
    End Interface
End Namespace
