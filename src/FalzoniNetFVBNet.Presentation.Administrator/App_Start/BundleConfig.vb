Imports System.Web.Optimization

Public Module BundleConfig
    ' Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)
        If FalzoniNetFVBNet.Utils.Helpers.ConfigurationHelper.IsBundleled Then
            bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libraries/jquery/jquery-{version}.js"))

            bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                            "~/Scripts/libraries/jqueryvalidate/jquery.validate*"))

            bundles.Add(New ScriptBundle("~/bundles/jquerymask").Include(
                            "~/Scripts/libraries/jquerymask/jquery.mask.*"))

            ' Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            ' pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/libraries/modernizr-*"))

            bundles.Add(New ScriptBundle("~/bundles/js/admin-lte").Include(
                          "~/Scripts/libraries/icheck/icheck.js",
                          "~/Scripts/libraries/admin-lte/adminlte.js"))

            bundles.Add(New ScriptBundle("~/bundles/js/bootstrap").Include(
                          "~/Scripts/libraries/bootstrap/bootstrap*"))

            bundles.Add(New ScriptBundle("~/bundles/js/bootstrap-datepicker").Include(
                          "~/Scripts/libraries/bootstrap-datepicker/bootstrap-datepicker.js",
                          "~/Scripts/libraries/bootstrap-datepicker/locales/bootstrap-datepicker.pt-BR.min.js",
                          "~/Scripts/libraries/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"
                          ))

            bundles.Add(New ScriptBundle("~/bundles/js/moment").Include(
                          "~/Scripts/libraries/moment/moment.js",
                          "~/Scripts/libraries/moment/moment-with-locales.js"))

            bundles.Add(New ScriptBundle("~/bundles/js/core").Include(
                          "~/Scripts/core/root.js",
                          "~/Scripts/core/routes.js"))

            bundles.Add(New ScriptBundle("~/bundles/js/datatable").Include(
                          "~/Scripts/libraries/datatable/datatable*",
                          "~/Scripts/libraries/datatable.moment.datetime/datatable.moment.datetime.js",
                          "~/Scripts/core/datatable-utils.js"))

            'Styles
            bundles.Add(New StyleBundle("~/bundles/bootstrap").Include(
                          "~/Content/libraries/bootstrap/bootstrap.css"))

            bundles.Add(New StyleBundle("~/bundles/admin-lte").Include(
                    "~/Content/libraries/admin-lte/AdminLTE.css",
                    "~/Content/libraries/admin-lte/skins/_all-skins.css",
                    "~/Content/libraries/icheck/skins/square/_all.css",
                    "~/Content/libraries/font-awesome.css"))

            bundles.Add(New StyleBundle("~/bundles/datatable").Include(
                          "~/Content/libraries/datatable/datatable*"))

            bundles.Add(New StyleBundle("~/bundles/select2").Include(
                          "~/Content/libraries/select2/select2.*",
                          "~/Content/libraries/admin-lte/alt/AdminLTE-select2.*"))

            bundles.Add(New StyleBundle("~/bundles/bootstrap-datepicker").Include(
                          "~/Content/libraries/bootstrap-datepicker/bootstrap-datepicker.css"))
        End If
    End Sub
End Module
