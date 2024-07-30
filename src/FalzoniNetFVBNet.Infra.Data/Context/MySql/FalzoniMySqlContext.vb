Imports System.Data.Entity
Imports MySql.Data.EntityFramework

Namespace Context.MySql
    <DbConfigurationType(GetType(MySqlEFConfiguration))>
    Public Class FalzoniMySqlContext
        Inherits FalzoniContext

    End Class
End Namespace

