Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Service.Register

Namespace ServiceApplication.Register
    Public Class CustomerServiceApplication
        Private ReadOnly _customerService As CustomerService

        Public Sub New(customerService As CustomerService)
            _customerService = customerService
        End Sub

        Public Function [Get](Id As Guid) As CustomerDTO
            If Id = Guid.Empty Then
                Throw New ApplicationException("Erro ao buscar usuário!")
            End If

            Return _customerService.Get(Id)
        End Function

        Public Function GetAll() As List(Of CustomerDTO)
            Return _customerService.GetAll()
        End Function

        Public Sub Add(customerDTO As CustomerDTO)
            Validate(customerDTO)

            _customerService.Add(customerDTO)
        End Sub

        Public Sub Update(customerDTO As CustomerDTO)
            Validate(customerDTO)

            _customerService.Update(customerDTO)
        End Sub

        Public Sub Delete(customerDTO As CustomerDTO)
            If customerDTO.Id = Guid.Empty Then
                Throw New ApplicationException("Erro ao buscar usuário!")
            End If

            _customerService.Delete(customerDTO)
        End Sub

        'Private METHODS
        Private Sub Validate(customerDTO As CustomerDTO)
            If String.IsNullOrEmpty(customerDTO.Name) Then
                Throw New ApplicationException("Necessário informar o nome do Cliente")
            End If

            If (Date.Now.Year - customerDTO.DateBirth.Year) < 17 Then
                Throw New ApplicationException("O Cliente deve ser maior de 18 anos")
            End If

            If String.IsNullOrEmpty(customerDTO.Document) Then
                Throw New ApplicationException("Necessário informar um documento válido")
            End If

            If String.IsNullOrEmpty(customerDTO.Gender) Then
                Throw New ApplicationException("Necessário informar o gênero")
            End If

            If String.IsNullOrEmpty(customerDTO.CellPhoneNumber) Then
                Throw New ApplicationException("Necessário informar o celular")
            End If

            If customerDTO.Addresses Is Nothing Or customerDTO.Addresses.Count = 0 Then
                Throw New ApplicationException("Necessário informar ao menos um endereço")
            End If
        End Sub
    End Class
End Namespace
