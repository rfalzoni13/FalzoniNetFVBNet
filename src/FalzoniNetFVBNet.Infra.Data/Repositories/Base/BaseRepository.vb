Imports System.Data.Entity
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base
Imports FalzoniNetFVBNet.Infra.Data.Context
Imports FalzoniNetFVBNet.Infra.Data.Context.MySql
Imports FalzoniNetFVBNet.Infra.Data.Context.SqlServer
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Repositories.Base
    Public MustInherit Class BaseRepository(Of T As Class)
        Implements IDisposable, IBaseRepository(Of T)

        Protected Context As DbContext

        Public Sub New()
            Select Case ConfigurationHelper.ProviderName
                Case "SqlServer"
                    Context = New FalzoniSqlServerContext
                    Exit Select
                Case "MySql"
                    Context = New FalzoniMySqlContext
                    Exit Select
                Case Else
                    Throw New Exception("Erro ao definir provider")
            End Select
        End Sub

        Public Sub New(falzoniContext As FalzoniContext)
            Context = falzoniContext
        End Sub

        Public Overridable Sub Add(obj As T) Implements IBaseRepository(Of T).Add
            Context.Set(Of T).Add(obj)
            Context.SaveChanges()
        End Sub

        Public Overridable Sub Update(obj As T) Implements IBaseRepository(Of T).Update
            Context.Set(Of T).Attach(obj)
            Context.Entry(obj).State = EntityState.Modified
            Context.SaveChanges()
        End Sub

        Public Overridable Sub Delete(Id As Guid) Implements IBaseRepository(Of T).Delete
            Dim obj = Context.Set(Of T).Find(Id)
            If obj IsNot Nothing Then
                Context.Set(Of T).Remove(obj)
                Context.SaveChanges()
            End If
        End Sub

        Public Overridable Function [Get](Id As Guid) As T Implements IBaseRepository(Of T).Get
            Return Context.Set(Of T).Find(Id)
        End Function

        Public Overridable Function GetAll() As IEnumerable(Of T) Implements IBaseRepository(Of T).GetAll
            Return Context.Set(Of T).ToList()
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Context.Dispose()
        End Sub
    End Class
End Namespace
