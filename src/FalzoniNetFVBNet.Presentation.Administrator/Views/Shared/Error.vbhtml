@Code
    Layout = Nothing
End Code

@If FalzoniNetFVBNet.Utils.Helpers.ConfigurationHelper.IsBundleled Then
    @Styles.Render("~/bundles/bootstrap")
    @Styles.Render("~/bundles/admin-lte")
    @Scripts.Render("~/bundles/modernizr")
Else
    @<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous">
    @<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/admin-lte/2.4.0/css/AdminLTE.min.css" integrity="sha512-WwxBYWUrN/LPCceidkNpgYFBiIjrickdz+Ts+55PAzTJ9sSP8EVfId6lq0cl3/kSnGECF/7v3p3BnCLkvVhs/w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    @<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/admin-lte/2.4.0/css/skins/_all-skins.min.css" integrity="sha512-D231SkmJ+61oWzyBS0Htmce/w1NLwUVtMSA05ceaprOG4ZAszxnScjexIQwdAr4bZ4NRNdSHH1qXwu1GwEVnvA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    @<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha512-SfTiTlX6kk+qitfevl/7LibUOeJWlt9rbyDn92a1DqWOw9vWG2MFoays0sgObmWazO5BQPiFucnnEAjpAB+/Sw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    @<script src="https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js" integrity="sha512-3n19xznO0ubPpSwYCRRBgHh63DrV+bdZfHK52b1esvId4GsfwStQNPJFjeQos2h3JwCmZl0/LgLxSKMAI55hgw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
End If

<title>Projeto Falzoni - Erro interno</title>

<section>

</section>
<div class="container">
    <div class="row">
        <h1 class="box-title text-center">Projeto <b>Falzoni</b></h1>
    </div>
    <div class="row">
        <h1 class="text-center">Erro interno!</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12 col-xs-12">
                <h3 class="text-center">@(If(System.Diagnostics.Debugger.IsAttached, Model?.Exception?.Message, "Entre em contato com o administrador para obter maiores informações."))</h3>
            </div>
        </div>
    </div>
</div>

@If FalzoniNetFVBNet.Utils.Helpers.ConfigurationHelper.IsBundleled Then
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/js/bootstrap")
    @Scripts.Render("~/bundles/js/admin-lte")
    @Scripts.Render("~/bundles/js/core")
Else
    @<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    @<script src="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/js/bootstrap.min.js" integrity="sha384-aJ21OjlMXNL5UyIl/XNwTMqvzeRMZH2w8c5cRVpzpU8Y5bApTppSuUkhZXN0VxHd" crossorigin="anonymous"></script>
    @<script src="https://cdnjs.cloudflare.com/ajax/libs/admin-lte/2.4.0/js/adminlte.min.js" integrity="sha512-4LW2vmg8t+drPiNqhkUrtVZ3M/UCyhEhVasHYx7d+mXKjcw/G0BSuQ78FnkzPyWU23QBXtTUrKoPmX95KTLE4A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
End If