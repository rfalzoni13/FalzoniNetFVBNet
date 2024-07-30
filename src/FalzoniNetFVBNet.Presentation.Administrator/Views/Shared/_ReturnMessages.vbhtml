@If ViewData.ModelState.Any(Function(x) x.Value.Errors.Any()) Then
    @<div class="col-md-12">
        <div class="alert alert-back alert-danger">
            <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
            @Html.ValidationSummary(False, String.Empty)
        </div>
    </div>
End If

@If TempData("Return") IsNot Nothing Then
    @<div class="row">
        <div class="col-md-12">
            @Select Case CType(TempData("Return"), FalzoniNetFVBNet.Presentation.Administrator.Models.Common.ReturnModel).Type
                Case "Error"
                    @<div class="alert alert-back alert-danger">
                        <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
                        @(CType(TempData("Return"), FalzoniNetFVBNet.Presentation.Administrator.Models.Common.ReturnModel).Message)
                    </div>
                    Exit Select
                Case "Warning"
                    @<div class="alert alert-back alert-warning">
                        <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
                        @(CType(TempData("Return"), FalzoniNetFVBNet.Presentation.Administrator.Models.Common.ReturnModel).Message)
                    </div>
                    Exit Select
                Case "Success"
                    @<div class="alert alert-back alert-success">
                        <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
                        @(CType(TempData("Return"), FalzoniNetFVBNet.Presentation.Administrator.Models.Common.ReturnModel).Message)
                    </div>
                    Exit Select
                Case Else
                    @<div class="alert alert-back alert-info">
                        <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
                        @(CType(TempData("Return"), FalzoniNetFVBNet.Presentation.Administrator.Models.Common.ReturnModel).Message)
                    </div>
                    Exit Select
            End Select
        </div>
    </div>
End If

<!--Jquery Validations-->
<div class="alert alert-jquery d-none">
    <button type="button" class="close alert-close"><span aria-hidden="true">&times;</span></button>
    <p class="text-alert-jquery"></p>
</div>
