var PDH = function () {
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
            url: '/PDHead/SearchPO',
            data: $("#frmSearchPOs").serialize(),
            success: function (response) {
                $('#po-grid').html(response);
                $("#po-grid tbody tr a").unbind('click').bind('click', PDH.POGridActions)
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
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditPO + '?POId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeletePO + '?POId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchPOs();
                    }

                });
            });
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
            $('#secondary-dialog-products').find('#btnSubmitProduct').unbind('click').bind('click', PDH.SaveProduct);
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
                        $('#products-grid tbody tr a').unbind('click').bind('click', PDH.ProductsGridActions)
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
                $('#secondary-dialog-products').find('#btnSubmitProduct').unbind('click').bind('click', PDH.SaveProduct);
                $('#secondary-dialog-products').modal('show');
            });
        }
        if (option == 2) {
            //if(!IsApproved){
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
            //}
            //else
            //{
            //    messagebox.success('You Cant Delete Items. It is already accepted', null, function () {
            //    });
            //}
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

    var PrintHtmlInovicePDF = function () {

        var POId = $('#POId').val();
        //window.location.href = AdminUrl.PrintHtmlInovicePDF + '?POId=' + POId;
        $.ajax({
            type: "GET",
            url: AdminUrl.PrintHtmlInovicePDF + '?POId=' + POId,
            async: false,
            success: function (data) {
                var divContents = data;
                var WindowObject = window.open("", "_blank");
                var cont = '<html><head><head>';
                cont += divContents;
                cont += '</html>';
                WindowObject.document.writeln(cont);
                WindowObject.document.close();
                WindowObject.focus();
                WindowObject.print();
                WindowObject.close();
            }
        });
    }


    var SearchGRNs = function () {

        $.ajax({
            type: 'POST',
            url: '/PDHead/SearchGRN',
            data: $("#frmSearchGRNs").serialize(),
            success: function (response) {
                $('#grn-grid').html(response);
                $("#grn-grid tbody tr a").unbind('click').bind('click', PDH.GRNGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var ClearSearchGRN = function () {

        $('#frmSearchGRNs #CountryName').val("");
        SearchGRNs();
    }

    var GRNGridActions = function () {

        var UserId = $(this).attr('data-id');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditGRN + '?GRNId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteGRN + '?GRNId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchGRNs();
                    }

                });
            });
        }
    }

    var SaveGRN = function () {
        var FrmAEUsers = $('#FrmAEGRNs');
        if (!validateGRNs(FrmAEUsers)) return false;
        var url = AdminUrl.SaveGRN;
        var data = $('#FrmAEGRNs').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewGRN + '?GRNId=' + data.Id;
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

    var validateGRNs = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEGRNs').find('#supplier').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEGRNs').find('#supplier')));
        }
        var username = $('#FrmAEGRNs').find('#createddate').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEGRNs').find('#createddate')));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateGRNItems = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#AddEditGRNItem').find('#poid').val();
        var rq = $('#AddEditGRNItem').find('#rq').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditGRNItem').find('#poid')));
        }
        var username = $('#AddEditGRNItem').find('#des').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditGRNItem').find('#des')));
        }
        var password = $('#AddEditGRNItem').find('#quantity').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditGRNItem').find('#quantity')));
        }
        var pass = $('#AddEditGRNItem').find('#unitPrice').val();
        if ((pass == 0) | (pass == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditGRNItem').find('#unitPrice')));
        }
        var user = $('#AddEditGRNItem').find('#piid').val();
        if ((user == 0) | (user == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditGRNItem').find('#piid')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var AddGRNItem = function () {
        $.get(AdminUrl.AddGRNItem, null, function (result) {
            $('#secondary-dialog-grnitems').html(result);
            $('#secondary-dialog-grnitems').find('#btnSubmitGRMItem').unbind('click').bind('click', PDH.SaveGRNItem);
            $('#secondary-dialog-grnitems').modal('show');
        });
    }

    var SaveGRNItem = function () {

        var FrmLeads = $(this).find('#AddEditGRNItem');
        if (!validateGRNItems(FrmLeads)) return false;
        var url = AdminUrl.SaveGRNItem;
        var POId = $('#poid').val();
        var PIId = $('#piid').val();
        var GRNId = $('#GRNId').val();
        var GIId = $('#GIId').val();
        var GIDescription = $('#des').val();
        var GIReceivedQuantity = $('#quantity').val();
        var GIUnitPrice = $('#unitPrice').val();

        var Product = {
            "PIId": PIId,
            "POId": POId,
            "GRNId": GRNId,
            "GIId": GIId,
            "GIDescription": GIDescription,
            "GIReceivedQuantity": GIReceivedQuantity,
            "GIUnitPrice": GIUnitPrice
        };

        $.ajax({
            url: url,
            data: JSON.stringify(Product),
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {

                        $('#grnitem-grid').html(data.productlist);
                        $('#grnitem-grid tbody tr a').unbind('click').bind('click', PDH.GRMItemsGridActions)
                                         .hover(function () {
                                             $(this).css({ cursor: 'pointer' });
                                         }, function () {
                                             $(this).css({ cursor: 'default' });
                                         });
                        reloadTempGRNItemGrid();

                        $('#secondary-dialog-grnitems').modal('hide');

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

    var GRMItemsGridActions = function () {

        var GIId = $(this).attr('data-id1');
        var IsApproved = $(this).attr('data-id2');
        var option = $(this).attr('data-option');

        if (option == 1) {
            $.get(AdminUrl.AddGRNItem + '?GIId=' + GIId, null, function (result) {

                $('#secondary-dialog-grnitems').html(result);
                $('#secondary-dialog-grnitems').find('#btnSubmitGRMItem').unbind('click').bind('click', PDH.SaveGRNItem);
                $('#secondary-dialog-grnitems').modal('show');
            });
        }
        if (option == 2) {
            //if (!IsApproved) {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.ajax({
                        type: 'Get',
                        url: AdminUrl.DeleteGRNItem,
                        data: { GIId: GIId },
                        success: function (data) {
                            messagebox.success('Successful!', null, function () {
                                reloadTempGRNItemGrid();

                            });
                        },
                    });

                });
            //}
            //else
            //{
            //    messagebox.success('You Cant Delete Items. It is already accepted', null, function () {
            //    });
            //}
        }
    }

    var reloadTempGRNItemGrid = function () {

        var GRNId = $('#GRNId').val();
        window.location.href = AdminUrl.ReviewGRN + '?GRNId=' + GRNId;

    }

    var PrintHtmlGRN = function () {

        var GRNId = $('#GRNId').val();
        window.location.href = AdminUrl.PrintHtmlGRN + '?GRNId=' + GRNId;
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

    var PrintHtmlGRNPDF = function () {

        var GRNId = $('#GRNId').val();
        //window.location.href = AdminUrl.PrintHtmlGRNPDF + '?GRNId=' + GRNId;
        $.ajax({
            type: "GET",
            url: AdminUrl.PrintHtmlGRNPDF + '?GRNId=' + GRNId,
            async: false,
            success: function (data) {
                var divContents = data;
                var WindowObject = window.open("", "_blank");
                var cont = '<html><head><head>';
                cont += divContents;
                cont += '</html>';
                WindowObject.document.writeln(cont);
                WindowObject.document.close();
                WindowObject.focus();
                WindowObject.print();
                WindowObject.close();
            }
        });
    }

    var OnPOChange = function () {

        var base = $('#AddEditGRNItem #piid');
        var POId = $('#AddEditGRNItem #poid').val();
        base.empty();
        if ((POId != '') && (POId != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetGRMItems,
                data: { POId: POId },
                success: function (data) {
                    if (data.Designations.length != 0) {
                        base.html('');
                        $.each(data.Designations, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#AddEditGRNItem #piid').empty();
                        var tdropdown = "No Item to Show"
                        $('#scid').append($("<option></option>").
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

    var OnPOIChange = function () {

        var base = $('#AddEditGRNItem #piid');
        var PIId = $('#AddEditGRNItem #piid').val();
        if ((PIId != null) & (PIId != 0)) {
            $.ajax({
                type: "POST",
                url: AdminUrl.GetGRMItemsDetails,
                data: { PIId: PIId },
                success: function (data) {
                    var qty = "qunantity purchased: " + data.qty;
                    var up = "unitprice purchased: " + data.up;
                    $('#AddEditGRNItem #des').val(data.des);
                    $('#AddEditGRNItem #rq').val(data.qty);
                    $('#AddEditGRNItem #qty').text(qty);
                    $('#AddEditGRNItem #up').text(up);
                },
                error: function (e) {
                    messagebox.error("Error loading...");
                }
            });
        }
    }

    var AcceptGRN = function ()
    {
        var GRNId = $('#GRNId').val();
        messagebox.confirm('Are you sure you want to ACCEPT?', 'Confirmation', function () {
            $.get(AdminUrl.AcceptGRN + '?GRNId=' + GRNId, null, function (result) {
                if (result.Status) {
                    messagebox.success("User Successfully ACCEPTED!");
                    window.location.href = AdminUrl.ReviewGRN + '?GRNId=' + GRNId;
                }

            });
        });
    }

    var AcceptPO = function () {
        var POId = $('#POId').val();
        messagebox.confirm('Are you sure you want to ACCEPT?', 'Confirmation', function () {
            $.get(AdminUrl.AcceptPO + '?POId=' + POId, null, function (result) {
                if (result.Status) {
                    messagebox.success("User Successfully ACCEPTED!");
                    window.location.href = AdminUrl.ReviewPO + '?POId=' + POId;
                }

            });
        });
    }

    var AcceptRN = function () {
        var RNId = $('#RNId').val();
        messagebox.confirm('Are you sure you want to ACCEPT?', 'Confirmation', function () {
            $.get(AdminUrl.AcceptRN + '?RNId=' + RNId, null, function (result) {
                if (result.Status) {
                    messagebox.success("User Successfully ACCEPTED!");
                    window.location.href = AdminUrl.ReviewRN + '?RNId=' + RNId;
                }

            });
        });
    }


    var SearchRNs = function () {

        $.ajax({
            type: 'POST',
            url: '/PDHead/SearchRN',
            data: $("#frmSearchRNs").serialize(),
            success: function (response) {
                $('#rn-grid').html(response);
                $("#rn-grid tbody tr a").unbind('click').bind('click', PDH.RNGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var ClearSearchRN = function () {

        $('#frmSearchRNs #CountryName').val("");
        SearchRNs();
    }

    var RNGridActions = function () {

        var UserId = $(this).attr('data-id');
        var IsApproved = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditRN + '?RNId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteRN + '?RNId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchRNs();
                    }

                });
            });
        }
    }

    var SaveRN = function () {
        var FrmAEUsers = $('#FrmAERNs');
        if (!validateRNs(FrmAEUsers)) return false;
        var url = AdminUrl.SaveRN;
        var data = $('#FrmAERNs').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewRN + '?RNId=' + data.Id;
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

    var validateRNs = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAERNs').find('#supplier').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAERNs').find('#supplier')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateRNItems = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#AddEditRNItem').find('#GRNId').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRNItem').find('#GRNId')));
        }
        var username = $('#AddEditRNItem').find('#RIDescription').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRNItem').find('#RIDescription')));
        }
        var password = $('#AddEditRNItem').find('#RIQuantity').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRNItem').find('#RIQuantity')));
        }
        var pass = $('#AddEditRNItem').find('#RIUnitPrice').val();
        if ((pass == 0) | (pass == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRNItem').find('#RIUnitPrice')));
        }
        var user = $('#AddEditRNItem').find('#GIId').val();
        if ((user == 0) | (user == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRNItem').find('#GIId')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var AddRNItem = function () {
        $.get(AdminUrl.AddRNItem, null, function (result) {
            $('#secondary-dialog-rnitems').html(result);
            $('#secondary-dialog-rnitems').find('#btnSubmitRMItem').unbind('click').bind('click', PDH.SaveRNItem);
            $('#secondary-dialog-rnitems').modal('show');
        });
    }

    var SaveRNItem = function () {

        var FrmLeads = $(this).find('#AddEditRNItem');
        if (!validateRNItems(FrmLeads)) return false;
        var url = AdminUrl.SaveRNItem;
        var GRNId = $('#GRNId').val();
        var GIId = $('#GIId').val();
        var RNId = $('#RNId').val();
        var RIId = $('#RIId').val();
        var RIDescription = $('#RIDescription').val();
        var RIQuantity = $('#RIQuantity').val();
        var RIUnitPrice = $('#RIUnitPrice').val();

        var Product = {
            "GIId": GIId,
            "RNId": RNId,
            "GRNId": GRNId,
            "RIId": RIId,
            "RIDescription": RIDescription,
            "RIQuantity": RIQuantity,
            "RIUnitPrice": RIUnitPrice
        };

        $.ajax({
            url: url,
            data: JSON.stringify(Product),
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {

                        $('#rnitem-grid').html(data.productlist);
                        $('#rnitem-grid tbody tr a').unbind('click').bind('click', PDH.RMItemsGridActions)
                                         .hover(function () {
                                             $(this).css({ cursor: 'pointer' });
                                         }, function () {
                                             $(this).css({ cursor: 'default' });
                                         });
                        reloadTempRNItemGrid();

                        $('#secondary-dialog-rnitems').modal('hide');

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

    var RMItemsGridActions = function () {

        var RIId = $(this).attr('data-id1');
        var IsApproved = $(this).attr('data-id2');
        var option = $(this).attr('data-option');

        if (option == 1) {
            $.get(AdminUrl.AddRNItem + '?RIId=' + RIId, null, function (result) {

                $('#secondary-dialog-rnitems').html(result);
                $('#secondary-dialog-rnitems').find('#btnSubmitRMItem').unbind('click').bind('click', PDH.SaveRNItem);
                $('#secondary-dialog-rnitems').modal('show');
            });
        }
        if (option == 2) {
            //if (!IsApproved) {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.ajax({
                        type: 'Get',
                        url: AdminUrl.DeleteRNItem,
                        data: { RIId: RIId },
                        success: function (data) {
                            messagebox.success('Successful!', null, function () {
                                reloadTempRNItemGrid();

                            });
                        },
                    });

                });
            //}
            //else {
            //    messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
            //    });
            //}
        }
    }

    var reloadTempRNItemGrid = function () {

        var RNId = $('#RNId').val();
        window.location.href = AdminUrl.ReviewRN + '?RNId=' + RNId;

    }

    var PrintHtmlRN = function () {

        var RNId = $('#RNId').val();
        window.location.href = AdminUrl.PrintHtmlRN + '?RNId=' + RNId;
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

    var PrintHtmlRNPDF = function () {

        var RNId = $('#RNId').val();
        //window.location.href = AdminUrl.PrintHtmlRN + '?RNId=' + RNId;
        $.ajax({
            type: "GET",
            url: AdminUrl.PrintHtmlRNPDF + '?RNId=' + RNId,
            async: false,
            success: function (data) {
                var divContents = data;
                var WindowObject = window.open("", "_blank");
                var cont = '<html><head><head>';
                cont += divContents;
                cont += '</html>';
                WindowObject.document.writeln(cont);
                WindowObject.document.close();
                WindowObject.focus();
                WindowObject.print();
                WindowObject.close();
            }
        });
    }

    var OnGRChange = function () {

        var base = $('#AddEditRNItem #GIId');
        var GRNId = $('#AddEditRNItem #GRNId').val();
        base.empty();
        if ((GRNId != '') && (GRNId != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetRMItems,
                data: { GRNId: GRNId },
                success: function (data) {
                    if (data.Designations.length != 0) {
                        base.html('');
                        $.each(data.Designations, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#AddEditRNItem #GIId').empty();
                        var tdropdown = "No Item to Show"
                        $('#GIId').append($("<option></option>").
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

    var OnGRIChange = function () {

        var base = $('#AddEditRNItem #GIId');
        var GIId = $('#AddEditRNItem #GIId').val();
        if ((GIId != null) & (GIId != 0)) {
            $.ajax({
                type: "POST",
                url: AdminUrl.GetRMItemsDetails,
                data: { GIId: GIId },
                success: function (data) {
                    var qty1 = "qunantity received(grn): " + data.qty;
                    var qty3 = "qunantity pending(grn): " + data.pq;
                    var qty2 = "unitprice in grn: " + data.up;

                    $('#AddEditRNItem #qty1').text(qty1);
                    $('#AddEditRNItem #qty2').text(qty2);
                    $('#AddEditRNItem #qty3').text(qty3);

                },
                error: function (e) {
                    messagebox.error("Error loading...");
                }
            });
        }
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

        SearchRNs:SearchRNs,
        ClearSearchRN:ClearSearchRN,
        RNGridActions:RNGridActions,
        SaveRN:SaveRN,
        validateRNs:validateRNs,
        validateRNItems:validateRNItems,
        AddRNItem:AddRNItem,
        SaveRNItem:SaveRNItem,
        RMItemsGridActions:RMItemsGridActions,
        reloadTempRNItemGrid:reloadTempRNItemGrid,
        PrintHtmlRN:PrintHtmlRN,
        OnGRChange:OnGRChange,
        OnGRIChange:OnGRIChange,
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
        OnPOIChange:OnPOIChange,
        OnPOChange: OnPOChange,
        PrintHtmlGRN: PrintHtmlGRN,
        reloadTempGRNItemGrid: reloadTempGRNItemGrid,
        GRMItemsGridActions: GRMItemsGridActions,
        SearchGRNs: SearchGRNs,
        ClearSearchGRN: ClearSearchGRN,
        GRNGridActions: GRNGridActions,
        SaveGRN: SaveGRN,
        validateGRNs: validateGRNs,
        validateGRNItems: validateGRNItems,
        AddGRNItem: AddGRNItem,
        SaveGRNItem: SaveGRNItem,
        AcceptGRN: AcceptGRN,
        AcceptPO: AcceptPO,
        AcceptRN: AcceptRN,
        PrintHtmlGRNPDF: PrintHtmlGRNPDF,
        PrintHtmlRNPDF: PrintHtmlRNPDF,
        PrintHtmlInovicePDF: PrintHtmlInovicePDF,
        OnMainCatogoryChange: OnMainCatogoryChange,
        OnSubCatogoryChange: OnSubCatogoryChange

    };

}();


$(document).ready(function () {
    
    //$('#divisionId').unbind('change').bind('change', user.OnDivisionChange);
    //$('#FrmAEProducts #mcid').unbind('change').bind('change', user.OnMainCatogoryChange);
    $('#AddEditProduct #MCId').unbind('change').bind('change', PDH.OnMainCatogoryChange);
    $('#AddEditProduct #SCId').unbind('change').bind('change', PDH.OnSubCatogoryChange);

    $('#AddEditRNItem #GRNId').unbind('change').bind('change', PDH.OnGRChange);
    $('#AddEditRNItem #GIId').unbind('change').bind('change', PDH.OnGRIChange);

    $('#btnSearchRNs').unbind('click').bind('click', PDH.SearchRNs);
    $('#savern').unbind('click').bind('click', PDH.SaveRN);
    $('#ClearSearchRNs').unbind('click').bind('click', PDH.ClearSearchRN);

    $('#btnAddrnitemDetails').unbind('click').bind('click', PDH.AddRNItem);
    $('#printrn').unbind('click').bind('click', PDH.PrintHtmlRN);



    $("#demoDate1").datetimepicker();
    $("#demoDate2").datetimepicker();
    $("#demoDate3").datetimepicker();

    $('#acceptgrn').unbind('click').bind('click', PDH.AcceptGRN);
    $('#acceptrn').unbind('click').bind('click', PDH.AcceptRN);
    $('#acceptpo').unbind('click').bind('click', PDH.AcceptPO);

    $('#AddEditGRNItem #poid').unbind('change').bind('change', PDH.OnPOChange);
    $('#AddEditGRNItem #piid').unbind('change').bind('change', PDH.OnPOIChange);
    $('#btnSearchGRNs').unbind('click').bind('click', PDH.SearchGRNs);
    $('#savegrn').unbind('click').bind('click', PDH.SaveGRN);
    $('#ClearSearchGRNs').unbind('click').bind('click', PDH.ClearSearchGRN);
    $('#btnAddgrnitemDetails').unbind('click').bind('click', PDH.AddGRNItem);
    $('#printgrn').unbind('click').bind('click', PDH.PrintHtmlGRN);
    $('#pdfgrn').unbind('click').bind('click', PDH.PrintHtmlGRNPDF);
    $('#pdfrn').unbind('click').bind('click', PDH.PrintHtmlRNPDF);
    $('#pdf').unbind('click').bind('click', PDH.PrintHtmlInovicePDF);

    $('#btnSearchPOs').unbind('click').bind('click', PDH.SearchPOs);
    $('#savepo').unbind('click').bind('click', PDH.SavePO);
    $('#ClearSearchPOs').unbind('click').bind('click', PDH.ClearSearchPO);

    $('#btnAddProductDetails').unbind('click').bind('click', PDH.AddProduct);
    $('#print').unbind('click').bind('click', PDH.PrintHtmlInovice);
    $("#po-grid tbody tr a").unbind('click').bind('click', PDH.POGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#products-grid tbody tr a").unbind('click').bind('click', PDH.ProductsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

    $("#grn-grid tbody tr a").unbind('click').bind('click', PDH.GRNGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#grnitem-grid tbody tr a").unbind('click').bind('click', PDH.GRMItemsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

    $("#rn-grid tbody tr a").unbind('click').bind('click', PDH.RNGridActions)
               .hover(function () {
                   $(this).css({ cursor: 'pointer' });
               }, function () {
                   $(this).css({ cursor: 'default' });
               });
    $("#rnitem-grid tbody tr a").unbind('click').bind('click', PDH.RMItemsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

});




