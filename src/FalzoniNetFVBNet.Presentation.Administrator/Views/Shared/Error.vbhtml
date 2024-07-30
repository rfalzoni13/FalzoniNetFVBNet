﻿@Code
    Layout = Nothing
End Code

@Styles.Render("~/bundles/bootstrap")
@Styles.Render("~/bundles/admin-lte")
@Styles.Render("~/bundles/admin-lte/skins")
@Styles.Render("~/Content/css")
@Scripts.Render("~/bundles/modernizr")

<title>Projeto Falzoni - @Response.StatusCode - Erro interno</title>

<div class="container">
    <div class="row">
        <h1 class="box-title">Projeto <b>Falzoni</b></h1>
    </div>
    <div class="row">
        <h1 class="text-center">@Response.StatusCode - Erro interno!</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12 col-xs-12">
                <h3 class="text-center">@(If(System.Diagnostics.Debugger.IsAttached, Model?.Exception?.Message, "Entre em contato com o administrador para obter maiores informações."))</h3>
            </div>
        </div>
    </div>
</div>

@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/js/bootstrap")