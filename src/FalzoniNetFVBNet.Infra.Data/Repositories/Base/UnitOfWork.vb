Imports System.Data.Entity
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base

Namespace Repositories.Base
    Public Class UnitOfWork
        Implements IUnitOfWork

        Private ReadOnly _context As DbContext

        Public Sub New(context As DbContext)
            _context = context
        End Sub

        Public Function BeginTransaction() As DbContextTransaction Implements IUnitOfWork.BeginTransaction
            Return _context.Database.BeginTransaction()
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace
