var PD = function () {
    var preStatus;
    var JSDateTimeFormat, DateTimeFormat, shortDateFormat, maskbydate, maskbydatetime;
    var SetDates = function (datetimedetails) {

        JSDateTimeFormat = datetimedetails.JSDateTimeFormat;
        shortDateFormat = datetimedetails.shortDateFormat;
        maskbydatetime = datetimedetails.maskbydatetime;
        maskbydate = datetimedetails.maskbydate;
        DateTimeFormat = datetimedetails.DateTimeFormat;
        DateFormat = datetimedetails.DateFormat;
        JsTimeFormat = datetimedetails.JsTimeFormat;

    }


    var SearchPOs = function () {

        $.ajax({
            type: 'POST',
            url: '/PDWorker/SearchPO',
            data: $("#frmSearchPOs").serialize(),
            success: function (response) {
                $('#po-grid').html(response);
                $("#po-grid tbody tr a").unbind('click').bind('click', PD.POGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var ClearSearchPO = function () {

        $('#frmSearchPOs #CountryName').val("");
        $('#frmSearchPOs #file').val("");
        $('#frmSearchPOs #tender').val("");
        SearchPOs();
    }

    var POGridActions = function () {

        var UserId = $(this).attr('data-id');
        var IsApproved = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditPO + '?POId=' + UserId;
        }
        if (option == 2) {
            if (IsApproved=="False") {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.get(AdminUrl.DeletePO + '?POId=' + UserId, null, function (result) {
                        if (result.Status) {
                            messagebox.success("User Successfully Deleted!");
                            SearchPOs();
                        }

                    });
                });
            }
            else
            {
                messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
                });
            }
        }
    }

    var SavePO = function () {
        var FrmAEUsers = $('#FrmAEPOs');
        if (!validatePOs(FrmAEUsers)) return false;
        var url = AdminUrl.SavePO;
        var data = $('#FrmAEPOs').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewPO + '?POId=' + data.Id;
                    });
                }
                else {
                    messagebox.error(data.Message);
                }
            },
            error: function (error) {
                messagebox.error("Unsuccessful!", null);
            }
        });
    }

    var validatePOs = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEPOs').find('#tno').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEPOs').find('#tno')));
        }
        var username = $('#FrmAEPOs').find('#tfile').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEPOs').find('#tfile')));
        }
        var password = $('#FrmAEPOs').find('#supplier').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEPOs').find('#supplier')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateProducts = function (form) {
        var leadsValidator = new validator();

        var fname = $('#AddEditProduct').find('#MCId').val();
        if ((fname == 0) | (fname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#MCId')));
        }
        var fe = $('#AddEditProduct').find('#SCId').val();
        if ((fe == 0) | (fe == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#SCId')));
        }
        var fullname = $('#AddEditProduct').find('#Productid').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#Productid')));
        }
        var username = $('#AddEditProduct').find('#des').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#des')));
        }
        var password = $('#AddEditProduct').find('#quantity').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#quantity')));
        }
        var pass = $('#AddEditProduct').find('#unitPrice').val();
        if ((pass == 0) | (pass == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditProduct').find('#unitPrice')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var AddProduct = function () {
        $.get(AdminUrl.AddProduct, null, function (result) {
            $('#secondary-dialog-products').html(result);
            $('#secondary-dialog-products').find('#btnSubmitProduct').unbind('click').bind('click', PD.SaveProduct);
            $('#secondary-dialog-products').modal('show');
        });
    }

    var getProductNameById = function () {

        var e = document.getElementById("Productid");
        var id = e.options[e.selectedIndex].text;
        var price;
        if (id != 'Select') {
            $('#secondary-dialog-products').find('#des').val(id);
        }
        else
        {
            $('#secondary-dialog-products').find('#des').val('');
        }
    }

    var SaveProduct = function () {

        var FrmLeads = $(this).find('#AddEditProduct');
        if (!validateProducts(FrmLeads)) return false;
        var url = AdminUrl.SaveProduct;
        var POId = $('#POId').val();
        var PIId = $('#PIId').val();
        var PROId = $('#Productid').val();
        var PIDescription = $('#des').val();
        var PIQuantity = $('#quantity').val();
        var PIUnitPrice = $('#unitPrice').val();

        var Product = {
            "PIId": PIId,
            "POId": POId,
            "PROId": PROId,
            "PIDescription": PIDescription,
            "PIQuantity": PIQuantity,
            "PIUnitPrice": PIUnitPrice
        };

        $.ajax({
            url: url,
            data: JSON.stringify(Product),
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        
                        $('#products-grid').html(data.productlist);
                        $('#products-grid tbody tr a').unbind('click').bind('click', PD.ProductsGridActions)
                                         .hover(function () {
                                             $(this).css({ cursor: 'pointer' });
                                         }, function () {
                                             $(this).css({ cursor: 'default' });
                                         });
                        reloadTempProductGrid();

                        $('#secondary-dialog-products').modal('hide');
                    
                    });
                }
                else {
                    messagebox.error(data.messagMessage);
                }
            },
            error: function (error) {
                messagebox.error("Unsuccessful!", null);
            }
        });
    }

    var ProductsGridActions = function () {

        var PIId = $(this).attr('data-id1');
        var IsApproved = $(this).attr('data-id2');
        var option = $(this).attr('data-option');

        if (option == 1) {
            $.get(AdminUrl.AddProduct + '?PIId=' + PIId, null, function (result) {

                $('#secondary-dialog-products').html(result);
                $('#secondary-dialog-products').find('#btnSubmitProduct').unbind('click').bind('click', PD.SaveProduct);
                $('#secondary-dialog-products').modal('show');
            });
        }
        if (option == 2) {
            if (IsApproved == "False") {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.ajax({
                        type: 'Get',
                        url: AdminUrl.DeleteProduct,
                        data: { PIId: PIId },
                        success: function (data) {
                            messagebox.success('Successful!', null, function () {
                                reloadTempProductGrid();

                            });
                        },
                    });

                });
            }
            else {
                messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
                });
            }
        }
    }

    var reloadTempProductGrid = function () {

        var POId = $('#POId').val();
        window.location.href = AdminUrl.ReviewPO + '?POId=' + POId;

    }

    var PrintHtmlInovice = function () {

        var POId = $('#POId').val();
        window.location.href = AdminUrl.PrintHtmlInovice + '?POId=' + POId;
        //$.ajax({
        //    type: "GET",
        //    url: AdminUrl.PrintHtmlInovice + '?POId=' + POId,
        //    async: false,
        //    success: function (data) {
        //        var divContents = data;
        //        var WindowObject = window.open("", "_blank");
        //        var cont = '<html><head><head>';
        //        cont += divContents;
        //        cont += '</html>';
        //        WindowObject.document.writeln(cont);
        //        WindowObject.document.close();
        //        WindowObject.focus();
        //        WindowObject.print();
        //        WindowObject.close();
        //    }
        //});
    }

    var OnMainCatogoryChange = function () {

        var base = $('#AddEditProduct #SCId');
        var MC = $('#AddEditProduct #MCId').val();
        base.empty();
        if ((MC != '') && (MC != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetSubCatogory,
                data: { MC: MC },
                success: function (data) {
                    if (data.SubCatogory.length != 0) {
                        base.html('');
                        $.each(data.SubCatogory, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#AddEditProduct #SCId').empty();
                        var tdropdown = "No Designations to Show"
                        $('#AddEditProduct #SCId').append($("<option></option>").
                         attr("value", tdropdown).
                         text(tdropdown));
                    }

                },
                error: function (e) {
                    base.empty();
                    messagebox.error("Error loading...");
                }
            });
        } else {
        }
    }

    var OnSubCatogoryChange = function () {

        var base = $('#AddEditProduct #Productid');
        var MC = $('#AddEditProduct #SCId').val();
        base.empty();
        if ((MC != '') && (MC != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetProductSC,
                data: { MC: MC },
                success: function (data) {
                    if (data.SubCatogory.length != 0) {
                        base.html('');
                        $.each(data.SubCatogory, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#AddEditProduct #Productid').empty();
                        var tdropdown = "No Designations to Show"
                        $('#AddEditProduct #Productid').append($("<option></option>").
                         attr("value", tdropdown).
                         text(tdropdown));
                    }

                },
                error: function (e) {
                    base.empty();
                    messagebox.error("Error loading...");
                }
            });
        } else {
        }
    }

    return {

        SearchPOs: SearchPOs,
        ClearSearchPO: ClearSearchPO,
        POGridActions: POGridActions,
        SavePO: SavePO,
        validatePOs: validatePOs,
        AddProduct: AddProduct,
        getProductNameById: getProductNameById,
        SaveProduct: SaveProduct,
        validateProducts: validateProducts,
        ProductsGridActions: ProductsGridActions,
        reloadTempProductGrid: reloadTempProductGrid,
        PrintHtmlInovice: PrintHtmlInovice,
        OnMainCatogoryChange: OnMainCatogoryChange,
        OnSubCatogoryChange: OnSubCatogoryChange

    };

}();


$(document).ready(function () {
    
    //$('#divisionId').unbind('change').bind('change', user.OnDivisionChange);
    $('#AddEditProduct #MCId').unbind('change').bind('change', PD.OnMainCatogoryChange);
    $('#AddEditProduct #SCId').unbind('change').bind('change', PD.OnSubCatogoryChange);
    $("#demoDate1").datetimepicker();
    $("#demoDate2").datetimepicker();
    $("#demoDate3").datetimepicker();

    $('#btnSearchPOs').unbind('click').bind('click', PD.SearchPOs);
    $('#savepo').unbind('click').bind('click', PD.SavePO);
    $('#ClearSearchPOs').unbind('click').bind('click', PD.ClearSearchPO);

    $('#btnAddProductDetails').unbind('click').bind('click', PD.AddProduct);
    $('#print').unbind('click').bind('click', PD.PrintHtmlInovice);
    $("#po-grid tbody tr a").unbind('click').bind('click', PD.POGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#products-grid tbody tr a").unbind('click').bind('click', PD.ProductsGridActions).hover(function () {
        $(this).css({ cursor: 'pointer' });
    }, function () {
        $(this).css({ cursor: 'default' });
    });
});




