var Dp = function () {
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


    var SearchRequests = function () {

        $.ajax({
            type: 'POST',
            url: '/Department/SearchRequest',
            data: $("#frmSearchRequests").serialize(),
            success: function (response) {
                $('#request-grid').html(response);
                $("#request-grid tbody tr a").unbind('click').bind('click', Dp.RequestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var ClearSearchRequest = function () {

        $('#frmSearchRequests #CountryName').val("");
        SearchRequests();
    }

    var RequestGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditRequest + '?RequestId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteRequest + '?RequestId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchRequests();
                    }

                });
            });
        }
    }

    var SaveRequest = function () {
        var FrmAEUsers = $('#FrmAERequests');
        if (!validateRequests(FrmAEUsers)) return false;
        var url = AdminUrl.SaveRequest;
        var data = $('#FrmAERequests').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewRequest + '?RequestId=' + data.Id;
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

    var validateRequests = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAERequests').find('#supplier').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAERequests').find('#supplier')));
        }
        var username = $('#FrmAERequests').find('#createddate').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAERequests').find('#createddate')));
        }
        
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateRequestItems = function (form) {
        var leadsValidator = new validator();

        var fname = $('#AddEditRequestItem').find('#MCId').val();
        if ((fname == 0) | (fname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRequestItem').find('#MCId')));
        }
        var fe = $('#AddEditRequestItem').find('#SCId').val();
        if ((fe == 0) | (fe == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRequestItem').find('#SCId')));
        }
        var fullname = $('#AddEditRequestItem').find('#proid').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRequestItem').find('#proid')));
        }
        var username = $('#AddEditRequestItem').find('#desc').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRequestItem').find('#desc')));
        }
        var password = $('#AddEditRequestItem').find('#quanty').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditRequestItem').find('#quanty')));
        }
      
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var AddRequestItem = function () {
        $.get(AdminUrl.AddRequestItem, null, function (result) {
            $('#secondary-dialog-requestitems').html(result);
            $('#secondary-dialog-requestitems').find('#btnSubmitRequestItem').unbind('click').bind('click', Dp.SaveRequestItem);
            $('#secondary-dialog-requestitems').modal('show');
        });
    }

    var SaveRequestItem = function () {

        var FrmLeads = $(this).find('#AddEditRequestItem');
        if (!validateRequestItems(FrmLeads)) return false;
        var url = AdminUrl.SaveRequestItem;
        var PROId = $('#proid').val();
        var RequestId = $('#RequestId').val();
        var RequestItemId = $('#RequestItemId').val();
        var RequestDescription = $('#desc').val();
        var RequestQuantity = $('#quanty').val();

        var Product = {
            "PROId": PROId,
            "RequestId": RequestId,
            "RequestItemId": RequestItemId,
            "RequestDescription": RequestDescription,
            "RequestQuantity": RequestQuantity
        };

        $.ajax({
            url: url,
            data: JSON.stringify(Product),
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        
                        $('#requestitem-grid').html(data.productlist);
                        $('#requestitem-grid tbody tr a').unbind('click').bind('click', Dp.RequestItemsGridActions)
                                         .hover(function () {
                                             $(this).css({ cursor: 'pointer' });
                                         }, function () {
                                             $(this).css({ cursor: 'default' });
                                         });
                        reloadTempRequestItemGrid();

                        $('#secondary-dialog-requestitems').modal('hide');
                    
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

    var RequestItemsGridActions = function () {

        var RequestItemId = $(this).attr('data-id1');
        var Id = $(this).attr('data-id2');
        var option = $(this).attr('data-option');

        if (option == 1) {
            $.get(AdminUrl.AddRequestItem + '?RequestItemId=' + RequestItemId, null, function (result) {

                $('#secondary-dialog-requestitems').html(result);
                $('#secondary-dialog-requestitems').find('#btnSubmitRequestItem').unbind('click').bind('click', Dp.SaveRequestItem);
                $('#secondary-dialog-requestitems').modal('show');
            });
        }
        if (option == 2) {

            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.ajax({
                    type: 'Get',
                    url: AdminUrl.DeleteRequestItem,
                    data: { RequestItemId: RequestItemId },
                    success: function (data) {
                        messagebox.success('Successful!', null, function () {
                            reloadTempRequestItemGrid();

                        });
                    },
                });

            });

        }
    }

    var reloadTempRequestItemGrid = function () {

        var RequestId = $('#RequestId').val();
        window.location.href = AdminUrl.ReviewRequest + '?RequestId=' + RequestId;

    }

    var PrintHtmlRequest = function () {

        var RequestId = $('#RequestId').val();
        window.location.href = AdminUrl.PrintHtmlRequest + '?RequestId=' + RequestId;
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

    var PrintHtmlRequestPDF = function () {

        var RequestId = $('#RequestId').val();
        //window.location.href = AdminUrl.PrintHtmlRequestPDF + '?RequestId=' + RequestId;
        $.ajax({
            type: "GET",
            url: AdminUrl.PrintHtmlRequestPDF + '?RequestId=' + RequestId,
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

   /* var OnPOChange = function () {

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
        if ((PIId!=null)&(PIId!=0)) {
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
    */
    var getProductNameById = function () {

        var e = document.getElementById("proid");
        var id = e.options[e.selectedIndex].text;
        var price;
        if (id != 'Select') {
            $('#secondary-dialog-requestitems').find('#desc').val(id);
        }
        else {
            $('#secondary-dialog-requestitems').find('#desc').val('');
        }
    }

    var OnMainCatogoryChange = function () {

        var base = $('#AddEditRequestItem #SCId');
        var MC = $('#AddEditRequestItem #MCId').val();
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
                        $('#AddEditRequestItem #SCId').empty();
                        var tdropdown = "No Designations to Show"
                        $('#AddEditRequestItem #SCId').append($("<option></option>").
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

        var base = $('#AddEditRequestItem #proid');
        var MC = $('#AddEditRequestItem #SCId').val();
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
                        $('#AddEditRequestItem #proid').empty();
                        var tdropdown = "No Designations to Show"
                        $('#AddEditRequestItem #proid').append($("<option></option>").
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

        SearchRequests: SearchRequests,
        ClearSearchRequest: ClearSearchRequest,
        RequestGridActions: RequestGridActions,
        SaveRequest: SaveRequest,
        validateRequests: validateRequests,
        validateRequestItems: validateRequestItems,
        AddRequestItem: AddRequestItem,
        SaveRequestItem: SaveRequestItem,
        RequestItemsGridActions: RequestItemsGridActions,
        reloadTempRequestItemGrid: reloadTempRequestItemGrid,
        //OnPOChange: OnPOChange,
        //OnPOIChange:OnPOIChange,
        PrintHtmlRequest: PrintHtmlRequest,
        getProductNameById: getProductNameById,
        PrintHtmlRequestPDF: PrintHtmlRequestPDF,
        OnMainCatogoryChange: OnMainCatogoryChange,
        OnSubCatogoryChange: OnSubCatogoryChange

    };

}();


$(document).ready(function () {
    
    //$('#divisionId').unbind('change').bind('change', user.OnDivisionChange);
    //$('#AddEditGRNItem #poid').unbind('change').bind('change', Dp.OnPOChange);
    //$('#AddEditGRNItem #piid').unbind('change').bind('change', Dp.OnPOIChange);
    $('#AddEditRequestItem #MCId').unbind('change').bind('change', Dp.OnMainCatogoryChange);
    $('#AddEditRequestItem #SCId').unbind('change').bind('change', Dp.OnSubCatogoryChange);
    $("#demoDate1").datetimepicker();

    $('#btnSearchRequests').unbind('click').bind('click', Dp.SearchRequests);
    $('#saverequest').unbind('click').bind('click', Dp.SaveRequest);
    $('#ClearSearchRequests').unbind('click').bind('click', Dp.ClearSearchRequest);

    $('#btnAddrequestitemDetails').unbind('click').bind('click', Dp.AddRequestItem);
    $('#printrequest').unbind('click').bind('click', Dp.PrintHtmlRequest);
    $('#pdfrequest').unbind('click').bind('click', Dp.PrintHtmlRequestPDF);
    $("#request-grid tbody tr a").unbind('click').bind('click', Dp.RequestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#requestitem-grid tbody tr a").unbind('click').bind('click', Dp.RequestItemsGridActions).hover(function () {
        $(this).css({ cursor: 'pointer' });
    }, function () {
        $(this).css({ cursor: 'default' });
    });
});




