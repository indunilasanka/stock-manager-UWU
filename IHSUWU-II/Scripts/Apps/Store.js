var Store = function () {
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


    var SearchGRNs = function () {

        $.ajax({
            type: 'POST',
            url: '/StoreKeeper/SearchGRN',
            data: $("#frmSearchGRNs").serialize(),
            success: function (response) {
                $('#grn-grid').html(response);
                $("#grn-grid tbody tr a").unbind('click').bind('click', Store.GRNGridActions)
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
        var IsApproved = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditGRN + '?GRNId=' + UserId;
        }
        if (option == 2) {
            //if (IsApproved == "False") {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.get(AdminUrl.DeleteGRN + '?GRNId=' + UserId, null, function (result) {
                        if (result.Status) {
                            messagebox.success("User Successfully Deleted!");
                            SearchGRNs();
                        }

                    });
                });
            //}
            //else
            //{
            //    messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
            //    });
            //}
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
            $('#secondary-dialog-grnitems').find('#btnSubmitGRMItem').unbind('click').bind('click', Store.SaveGRNItem);
            $('#secondary-dialog-grnitems').modal('show');
        });
    }

    var getProductNameById = function () {

        var e = document.getElementById("poid");
        var id = e.options[e.selectedIndex].text;
        var price;
        if (id != 'Select') {
            $('#secondary-dialog-grnitems').find('#des').val(id);
        }
        else
        {
            $('#secondary-dialog-grnitems').find('#des').val('');
        }
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
                        $('#grnitem-grid tbody tr a').unbind('click').bind('click', Store.GRMItemsGridActions)
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
                $('#secondary-dialog-grnitems').find('#btnSubmitGRMItem').unbind('click').bind('click', Store.SaveGRNItem);
                $('#secondary-dialog-grnitems').modal('show');
            });
        }
        if (option == 2) {
            //if (IsApproved == "False") {
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
            //    messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
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
                        $('#piid').append($("<option></option>").
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



    var SearchIssues = function () {

        $.ajax({
            type: 'POST',
            url: '/StoreKeeper/SearchIssue',
            data: $("#frmSearchIssues").serialize(),
            success: function (response) {
                $('#issue-grid').html(response);
                $("#issue-grid tbody tr a").unbind('click').bind('click', Store.IssueGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var ClearSearchIssue = function () {

        $('#frmSearchIssues #CountryName').val("");
        SearchIssues();
    }

    var IssueGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditIssue + '?IId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteIssue + '?IId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchIssues();
                    }

                });
            });
        }
    }

    var SaveIssue = function () {
        var FrmAEUsers = $('#FrmAEIssues');
        if (!validateIssues(FrmAEUsers)) return false;
        var url = AdminUrl.SaveIssue;
        var data = $('#FrmAEIssues').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewIssue + '?IId=' + data.Id;
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

    var validateIssues = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEIssues').find('#location').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEIssues').find('#location')));
        }
        var username = $('#FrmAEIssues').find('#createddate').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEIssues').find('#createddate')));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateIssueItems = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#AddEditIssueItem').find('#poid').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditIssueItem').find('#poid')));
        }
        var username = $('#AddEditIssueItem').find('#piid').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditIssueItem').find('#piid')));
        }
        var pass = $('#AddEditIssueItem').find('#unitPrice').val();
        if ((pass == 0) | (pass == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditIssueItem').find('#unitPrice')));
        }
        var user = $('#AddEditIssueItem').find('#quantity').val();
        if ((user == 0) | (user == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#AddEditIssueItem').find('#quantity')));
        }
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var AddIssueItem = function () {
        $.get(AdminUrl.AddIssueItem, null, function (result) {
            $('#secondary-dialog-issueitems').html(result);
            $('#secondary-dialog-issueitems').find('#btnSubmitIssueItem').unbind('click').bind('click', Store.SaveIssueItem);
            $('#secondary-dialog-issueitems').modal('show');
        });
    }

    var SaveIssueItem = function () {

        var FrmLeads = $(this).find('#AddEditIssueItem');
        if (!validateIssueItems(FrmLeads)) return false;
        var url = AdminUrl.SaveIssueItem;
        var RequestId = $('#poid').val();
        var RequestItemId = $('#piid').val();
        var IId = $('#IId').val();
        var IItemId = $('#IItemId').val();
        var PROId = $('#PROId').val();
        var IssuedQuantity = $('#quantity').val();
        var ItemUnitPrice = $('#unitPrice').val();

        var Product = {
            "RequestId": RequestId,
            "RequestItemId": RequestItemId,
            "IId": IId,
            "IItemId": IItemId,
            "PROId": PROId,
            "IssuedQuantity": IssuedQuantity,
            "ItemUnitPrice": ItemUnitPrice
        };

        $.ajax({
            url: url,
            data: JSON.stringify(Product),
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {

                        $('#issueitem-grid').html(data.productlist);
                        $('#issueitem-grid tbody tr a').unbind('click').bind('click', Store.IssueItemsGridActions)
                                         .hover(function () {
                                             $(this).css({ cursor: 'pointer' });
                                         }, function () {
                                             $(this).css({ cursor: 'default' });
                                         });
                        reloadTempIssueItemGrid();

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

    var IssueItemsGridActions = function () {

        var IItemId = $(this).attr('data-id1');
        var Id = $(this).attr('data-id2');
        var option = $(this).attr('data-option');

        if (option == 1) {
            $.get(AdminUrl.AddIssueItem + '?IItemId=' + IItemId, null, function (result) {

                $('#secondary-dialog-issueitems').html(result);
                $('#secondary-dialog-issueitems').find('#btnSubmitIssueItem').unbind('click').bind('click', Store.SaveIssueItem);
                $('#secondary-dialog-issueitems').modal('show');
            });
        }
        if (option == 2) {

            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.ajax({
                    type: 'Get',
                    url: AdminUrl.DeleteIssueItem,
                    data: { IItemId: IItemId },
                    success: function (data) {
                        messagebox.success('Successful!', null, function () {
                            reloadTempIssueItemGrid();

                        });
                    },
                });

            });

        }
    }

    var reloadTempIssueItemGrid = function () {

        var IId = $('#IId').val();
        window.location.href = AdminUrl.ReviewIssue + '?IId=' + IId;

    }

    var OnRequestChange = function () {

        var base = $('#AddEditIssueItem #piid');
        var RequestId = $('#AddEditIssueItem #poid').val();
        base.empty();
        if ((RequestId != '') && (RequestId != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetIssueItems,
                data: { RequestId: RequestId },
                success: function (data) {
                    if (data.Designations.length != 0) {
                        base.html('');
                        $.each(data.Designations, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#AddEditIssueItem #piid').empty();
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

    var OnRequestItemChange = function () {

        var RequestItemId = $('#AddEditIssueItem').find('#piid').val();
        if ((RequestItemId != null) & (RequestItemId != 0)) {
            $.ajax({
                type: "POST",
                url: AdminUrl.GetIssueItemsDetails,
                data: { RequestItemId: RequestItemId },
                success: function (data) {
                    var name = "Product Name : " + data.up;
                    var re = "Reqested Quantity : " + data.re;
                    $('#AddEditIssueItem #product').text(name);
                    $('#AddEditIssueItem #rq').text(re);
                    $('#AddEditIssueItem #PROId').val(data.des);
                },
                error: function (e) {
                    messagebox.error("Error loading...");
                }
            });
        }
    }

    var PrintHtmlIssueNote = function () {

        var IId = $('#IId').val();
        window.location.href = AdminUrl.PrintHtmlIssueNote + '?IId=' + IId;
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

    var PrintHtmlIssueNotePDF = function () {

        var IId = $('#IId').val();
        //window.location.href = AdminUrl.PrintHtmlIssueNotePDF + '?IId=' + IId;
        $.ajax({
            type: "GET",
            url: AdminUrl.PrintHtmlIssueNotePDF + '?IId=' + IId,
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


    var SearchRNs = function () {

        $.ajax({
            type: 'POST',
            url: '/StoreKeeper/SearchRN',
            data: $("#frmSearchRNs").serialize(),
            success: function (response) {
                $('#rn-grid').html(response);
                $("#rn-grid tbody tr a").unbind('click').bind('click', Store.RNGridActions)
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
            if (IsApproved == "False") {
                messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                    $.get(AdminUrl.DeleteRN + '?RNId=' + UserId, null, function (result) {
                        if (result.Status) {
                            messagebox.success("User Successfully Deleted!");
                            SearchRNs();
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
            $('#secondary-dialog-rnitems').find('#btnSubmitRMItem').unbind('click').bind('click', Store.SaveRNItem);
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
                        $('#rnitem-grid tbody tr a').unbind('click').bind('click', Store.RMItemsGridActions)
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
                $('#secondary-dialog-rnitems').find('#btnSubmitRMItem').unbind('click').bind('click', Store.SaveRNItem);
                $('#secondary-dialog-rnitems').modal('show');
            });
        }
        if (option == 2) {
            if (IsApproved == "False") {
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
            }
            else
            {
                messagebox.success('Note is accepted!. Contact Head-PD for deletion', null, function () {
                });
            }
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

    var AcceptGRN = function () {
        var GRNId = $('#GRNId').val();
        messagebox.confirm('Are you sure you want to ACCEPT?', 'Confirmation', function () {
            $.get(AdminUrl.AcceptGRN + '?GRNId=' + GRNId, null, function (result) {
                if (result.Status) {
                    messagebox.success("User Successfully Accepted!");
                    window.location.href = AdminUrl.ReviewGRN + '?GRNId=' + GRNId;
                }

            });
        });
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

    return {

        SearchGRNs: SearchGRNs,
        SearchRNs: SearchRNs,
        SearchIssues:SearchIssues,
        ClearSearchGRN: ClearSearchGRN,
        ClearSearchRN: ClearSearchRN,
        GRNGridActions: GRNGridActions,
        RNGridActions: RNGridActions,
        SaveGRN: SaveGRN,
        SaveRN: SaveRN,
        validateGRNs: validateGRNs,
        validateRNs: validateRNs,
        validateGRNItems: validateGRNItems,
        validateRNItems: validateRNItems,
        AddGRNItem: AddGRNItem,
        AddRNItem: AddRNItem,
        SaveGRNItem: SaveGRNItem,
        SaveRNItem: SaveRNItem,
        GRMItemsGridActions: GRMItemsGridActions,
        RMItemsGridActions: RMItemsGridActions,
        reloadTempGRNItemGrid: reloadTempGRNItemGrid,
        reloadTempRNItemGrid: reloadTempRNItemGrid,
        OnPOChange: OnPOChange,
        OnPOIChange:OnPOIChange,
        IssueItemsGridActions: IssueItemsGridActions,
        reloadTempIssueItemGrid: reloadTempIssueItemGrid,
        SaveIssueItem: SaveIssueItem,
        AddIssueItem: AddIssueItem,
        validateIssueItems: validateIssueItems,
        validateIssues: validateIssues,
        SaveIssue: SaveIssue,
        IssueGridActions: IssueGridActions,
        ClearSearchIssue: ClearSearchIssue,
        OnRequestChange: OnRequestChange,
        OnGRChange: OnGRChange,
        OnGRIChange: OnGRIChange,
        OnRequestItemChange: OnRequestItemChange,
        PrintHtmlIssueNote: PrintHtmlIssueNote,
        PrintHtmlRN: PrintHtmlRN,
        PrintHtmlIssueNotePDF:PrintHtmlIssueNotePDF,
        AcceptGRN: AcceptGRN,
        PrintHtmlGRNPDF: PrintHtmlGRNPDF,
        PrintHtmlGRN: PrintHtmlGRN

    };

}();


$(document).ready(function () {
    
    //$('#divisionId').unbind('change').bind('change', user.OnDivisionChange);
    $('#acceptgrn').unbind('click').bind('click', Store.AcceptGRN);
    $('#pdfgrn').unbind('click').bind('click', Store.PrintHtmlGRNPDF);
    $('#printgrn').unbind('click').bind('click', Store.PrintHtmlGRN);

    $('#AddEditGRNItem #poid').unbind('change').bind('change', Store.OnPOChange);
    $('#AddEditGRNItem #piid').unbind('change').bind('change', Store.OnPOIChange);

    $('#AddEditRNItem #GRNId').unbind('change').bind('change', Store.OnGRChange);
    $('#AddEditRNItem #GIId').unbind('change').bind('change', Store.OnGRIChange);

    $('#AddEditIssueItem #poid').unbind('change').bind('change', Store.OnRequestChange);
    $('#AddEditIssueItem #piid').unbind('change').bind('change', Store.OnRequestItemChange);

    $("#demoDate1").datetimepicker();
    $("#demoDate2").datetimepicker();

    $('#btnSearchGRNs').unbind('click').bind('click', Store.SearchGRNs);
    $('#savegrn').unbind('click').bind('click', Store.SaveGRN);
    $('#ClearSearchGRNs').unbind('click').bind('click', Store.ClearSearchGRN);

    $('#btnSearchRNs').unbind('click').bind('click', Store.SearchRNs);
    $('#savern').unbind('click').bind('click', Store.SaveRN);
    $('#ClearSearchRNs').unbind('click').bind('click', Store.ClearSearchRN);

    $('#btnAddgrnitemDetails').unbind('click').bind('click', Store.AddGRNItem);
    $('#printgrn').unbind('click').bind('click', Store.PrintHtmlGRN);
    $('#pdfissue').unbind('click').bind('click', Store.PrintHtmlIssueNotePDF);

    $('#btnAddrnitemDetails').unbind('click').bind('click', Store.AddRNItem);
    $('#printrn').unbind('click').bind('click', Store.PrintHtmlRN);

    $('#printissue').unbind('click').bind('click', Store.PrintHtmlIssueNote);
    $("#grn-grid tbody tr a").unbind('click').bind('click', Store.GRNGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#grnitem-grid tbody tr a").unbind('click').bind('click', Store.GRMItemsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

    $("#rn-grid tbody tr a").unbind('click').bind('click', Store.RNGridActions)
               .hover(function () {
                   $(this).css({ cursor: 'pointer' });
               }, function () {
                   $(this).css({ cursor: 'default' });
               });
    $("#rnitem-grid tbody tr a").unbind('click').bind('click', Store.RMItemsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

    $('#btnSearchIssues').unbind('click').bind('click', Store.SearchIssues);
    $('#saveissue').unbind('click').bind('click', Store.SaveIssue);
    $('#ClearSearchIssues').unbind('click').bind('click', Store.ClearSearchIssue);

    $('#btnAddissueitemDetails').unbind('click').bind('click', Store.AddIssueItem);
    $("#issue-grid tbody tr a").unbind('click').bind('click', Store.IssueGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#issueitem-grid tbody tr a").unbind('click').bind('click', Store.IssueItemsGridActions).hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });

});




