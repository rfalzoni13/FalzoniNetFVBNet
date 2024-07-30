@ModelType FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration.UserModel
@Code
    ViewData("Title") = "Editar usuário"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@section styles
    @Html.Partial("_Styles")
End Section

<!-- Content Wrapper. Contains page content -->
<div class="content-wrapper">
    @Html.Partial("_Form", Model)
</div>

@section modals
    @Html.Partial("_ModalPhoto")
    @Html.Partial("_ModalSuccess")
    @Html.Partial("_ModalWarning")
    @Html.Partial("_ModalError")
End Section

@section scripts
    @Html.Partial("_Scripts")
End Section

