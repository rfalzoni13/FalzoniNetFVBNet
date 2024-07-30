Imports System.Data.Entity

Namespace Interfaces.Repositories.Base
    Public Interface IUnitOfWork
        Inherits IDisposable

        Function BeginTransaction() As DbContextTransaction

    End Interface
End Namespace