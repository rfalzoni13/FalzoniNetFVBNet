@ModelType FalzoniNetFVBNet.Presentation.Administrator.Models.Register.CustomerModel
@Code
    Dim action = ViewContext.RouteData.GetRequiredString("action")
End Code
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        @(If(action = "Create", "Novo Cliente", "Editar Cliente"))
    </h1>
    <ol class="breadcrumb">
        <li><a href="@Url.Action("Index", "Home", New With {.Area = String.Empty})"><i class="glyphicon glyphicon-home"></i> Home</a></li>
        <li><a href="@Url.Action("Index", "Cliente", New With {.Area = "Customer"})"><i class="glyphicon glyphicon-edit"></i> Cadastro</a></li>
        <li><a href="@Url.Action("Index", "Cliente", New With {.Area = "Customer"})"><i class="fa fa-user"></i> Cliente</a></li>
        <li class="active"><i class="fa fa-plus-square"></i> @action</li>
    </ol>
</section><!--section -->

<section class="content container-fluid">
    <div class="col-12">
        @Using (Html.BeginForm(action, "Cliente", New With {.Area = "Customer"}, FormMethod.Post, New With {.class = "form-customer", .enctype = "multipart/form-data"}))
            @<div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Dados cadastrais</h3>
                </div>
                <div class="box-body">
                    @Html.Partial("_ReturnMessages")
                    @Html.HiddenFor(Function(x) x.Id)
                    <div class="row mb-15">
                        <div class="col-md-5">
                            @Html.LabelFor(Function(x) x.Name)
                            @Html.TextBoxFor(Function(x) x.Name, New With {.class = "form-control", .autofocus = "autofocus"})
                        </div>
                        <div class="col-md-4">
                            @Html.LabelFor(Function(x) x.Email)
                            @Html.TextBoxFor(Function(x) x.Email, New With {.class = "form-control"})
                        </div>
                        <div class="col-md-3">
                            @Html.LabelFor(Function(x) x.DateBirth)
                            <div class="input-group date" data-provide="datepicker">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                @Html.TextBoxFor(Function(x) x.DateBirth, New With {.Value = If(Model.DateBirth > Date.MinValue, Model.DateBirth.ToString("dd/MM/yyyy"), String.Empty), .class = "form-control pull-right"})
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        @If Model.Genders IsNot Nothing Then
                            @<div class="col-md-3">
                                @Html.Label("Sexo")
                                @Html.DropDownListFor(Function(x) x.Gender, Model.Genders, "Selecione...", New With {.class = "form-control"})
                            </div>
                        End If
                        <div class="col-md-3">
                            @Html.LabelFor(Function(x) x.PhoneNumber)
                            @Html.TextBoxFor(Function(x) x.PhoneNumber, New With {.class = "form-control phone"})
                        </div>
                        <div class="col-md-3">
                            @Html.LabelFor(Function(x) x.CellPhoneNumber)
                            @Html.TextBoxFor(Function(x) x.CellPhoneNumber, New With {.class = "form-control cellphone"})
                        </div>
                        <div class="col-md-3">
                            @Html.LabelFor(Function(x) x.Document)
                            @Html.TextBoxFor(Function(x) x.Document, New With {.class = "form-control cpf"})
                        </div>
                    </div>
                </div>
            </div>
            @<div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Cadastro de endereço</h3>
                </div>
                <div class="box-body">
                    <div class="box-form-address d-none">
                        <input type="hidden" id="IndexTable" value="" />
                        <div class="row mb-15">
                            <div class="col-md-3">
                                <label for="Postal">CEP</label>
                                <input type="text" class="form-control postalcode" id="Postal" />
                            </div>
                            <div class="col-md-7">
                                <label for="Street">Logradouro</label>
                                <input type="text" class="form-control" id="Street" />
                            </div>
                            <div class="col-md-2">
                                <label for="Num">Número</label>
                                <input type="text" class="form-control" id="Num" />
                            </div>
                        </div>
                        <div class="row mb-15">
                            <div class="col-md-4">
                                <label for="Comp">Complemento</label>
                                <input type="text" class="form-control" id="Comp" />
                            </div>
                            <div class="col-md-3">
                                <label for="Region">Bairro</label>
                                <input type="text" class="form-control" id="Region" />
                            </div>
                            <div class="col-md-3">
                                <label for="District">Cidade</label>
                                <input type="text" class="form-control" id="District" />
                            </div>
                            <div class="col-md-2">
                                <label for="UF">Estado</label>
                                <input type="text" class="form-control" id="UF" />
                            </div>
                        </div>
                        <div class="row mb-15">
                            <div class="col-md-2">
                                <button id="AddAddress" class="btn btn-info btn-block" type="button">Adicionar</button>
                            </div>
                            <div class="col-md-2">
                                <button id="HideFormAddress" class="btn btn-default btn-block" type="button">Cancelar</button>
                            </div>
                        </div>
                        <hr />
                    </div>
                    <div class="row mb-15">
                        <div class="col-md-12">
                            <div class="float-right">
                                <button id="ShowFormAddress" class="btn btn-primary btn-sm" type="button"><i class="fa fa-plus"></i></button>
                            </div>

                            <h4 class="title-table-address @(If(Model.Addresses?.Count() <= 0, "d-none", String.Empty))">Endereços</h4>

                            <p class="text-center no-address @(If(Model.Addresses?.Count() > 0, "d-none", String.Empty))">Nenhum endereço cadastrado</p>

                            <div class="table-responsive @(If(Model.Addresses?.Count() <= 0, "d-none", String.Empty))">
                                <table id="CustomerAddressTable" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>
                                                @Html.DisplayName("CEP")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Logradouro")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Número")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Complemento")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Bairro")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Cidade")
                                            </th>
                                            <th>
                                                @Html.DisplayName("Estado")
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @If Model.Addresses?.Count() > 0 Then
                                            For i As Integer = 0 To Model.Addresses?.Count()
                                                If Not Model.Addresses(i).Removed Then
                                                    @<tr>
                                                        @Html.HiddenFor(Function(x) x.Addresses(i).Id)
                                                        @Html.HiddenFor(Function(x) x.Addresses(i).Removed)
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).PostalCode)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).PostalCode)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).AddressName)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).AddressName)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).Number)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).Number)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).Complement)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).Complement)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).Neighborhood)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).Neighborhood)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).City)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).City)
                                                        </td>
                                                        <td>
                                                            @Html.HiddenFor(Function(x) x.Addresses(i).State)
                                                            @Html.DisplayTextFor(Function(x) x.Addresses(i).State)
                                                        </td>
                                                        <td>
                                                            <button type="button" class="btn btn-info edit-address"
                                                                    data-toggle="tooltip" data-placement="top" title="Editar">
                                                                <i class="fa fa-pencil-square-o"></i>
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <button type="button" class="btn btn-danger delete-address" data-toggle="tooltip" data-placement="top" title="Deletar">
                                                                <i class="fa fa-trash-o"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                End If
                                            Next
                                        End If
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <div class="row">
                        <div class="col-md-2">
                            <button id="SendForm" class="btn btn-success btn-block" type="submit">@(If(action = "Create", "Cadastrar", "Salvar"))</button>
                        </div>
                        <div class="col-md-2">
                            @Html.ActionLink("Voltar", "Index", Nothing, New With {.class = "btn btn-default btn-block"})
                        </div>
                    </div>
                </div>
            </div>
        End Using
    </div> <!-- /container -->
</section>