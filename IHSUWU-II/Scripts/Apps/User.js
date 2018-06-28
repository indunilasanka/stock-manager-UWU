var user = function () {
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
    

    var OnDivisionChange = function () {

        var base = $('#designationId');
        var Division = $('#FrmAEUsers #divisionId').val();
        base.empty();
        if ((Division != '') &&  (Division != '-1')) {
            base.append($('<option/>').text('Loading...').val('-1'));

            $.ajax({
                type: "POST",
                url: AdminUrl.GetDesignations,
                data: {Division:Division },
                success: function (data) {
                    if (data.Designations.length != 0) {
                        base.html('');
                        $.each(data.Designations, function (id, option) {
                            //ddlmodels.empty();
                            base.append($('<option></option>').val(option.id).html(option.name));
                        });
                        base.trigger("change");
                    } else {
                        $('#designationId').empty();
                        var tdropdown = "No Designations to Show"
                        $('#designationId').append($("<option></option>").
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

    var SearchUsers = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/SearchUsers',
            data: $("#frmSearchUsers").serialize(),
            success: function (response) {
                $('#user-grid').html(response);
                $("#user-grid tbody tr a").unbind('click').bind('click', user.UserGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SearchSuppliers = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/SearchSupplier',
            data: $("#frmSearchSuppliers").serialize(),
            success: function (response) {
                $('#supplier-grid').html(response);
                $("#supplier-grid tbody tr a").unbind('click').bind('click', user.SupplierGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SearchLocations = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/SearchLocation',
            data: $("#frmSearchLocations").serialize(),
            success: function (response) {
                $('#location-grid').html(response);
                $("#location-grid tbody tr a").unbind('click').bind('click', user.LocationGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SearchMainAssests = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/SearchMainAssests',
            data: $("#frmSearchMainAssests").serialize(),
            success: function (response) {
                $('#mainassest-grid').html(response);
                $("#mainassest-grid tbody tr a").unbind('click').bind('click', user.MainAssestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SearchSubAssests = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/ManageSubAssests',
            data: $("#frmSearchSubAssests").serialize(),
            success: function (response) {
                $('#subassest-grid').html(response);
                $("#subassest-grid tbody tr a").unbind('click').bind('click', user.SubAssestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SearchProducts = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/ManageProducts',
            data: $("#frmSearchProducts").serialize(),
            success: function (response) {
                $('#product-grid').html(response);
                $("#product-grid tbody tr a").unbind('click').bind('click', user.ProductGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var SaveUser = function ()
    {
        var FrmAEUsers = $('#FrmAEUsers');
        if (!validateUsers(FrmAEUsers)) return false;
        var url = AdminUrl.SaveUser;
        var data = $('#FrmAEUsers').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewUser + '?UserId=' + data.Id;
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

    var SaveSupplier = function () {
        var FrmAESuppliers = $('#FrmAESuppliers');
        if (!validateSuppliers(FrmAESuppliers)) return false;
        var url = AdminUrl.SaveSupplier;
        var data = $('#FrmAESuppliers').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewSupplier + '?SId=' + data.Id;
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

    var SaveLocation = function () {
        var FrmAELocations = $('#FrmAELocations');
        if (!validateLocations(FrmAELocations)) return false;
        var url = AdminUrl.SaveLocation;
        var data = $('#FrmAELocations').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewLocation + '?LocationId=' + data.Id;
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

    var SaveMainAssest = function () {
        var FrmAEMainAssests = $('#FrmAEMainAssests');
        if (!validateMainAssests(FrmAEMainAssests)) return false;
        var url = AdminUrl.SaveMainAssest;
        var data = $('#FrmAEMainAssests').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewMainCatogory + '?MCId=' + data.Id;
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

    var SaveSubAssest = function () {
        var FrmAESubAssests = $('#FrmAESubAssests');
        if (!validateSubAssests(FrmAESubAssests)) return false;
        var url = AdminUrl.SaveSubAssest;
        var data = $('#FrmAESubAssests').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewSubCatogory + '?SCId=' + data.Id;
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

    var SaveProduct = function () {
        var FrmAEProducts = $('#FrmAEProducts');
        if (!validateProducts(FrmAEProducts)) return false;
        var url = AdminUrl.SaveProduct;
        var data = $('#FrmAEProducts').serialize();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        window.location.href = AdminUrl.ReviewProduct + '?PROId=' + data.Id;
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

    var validateUsers = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEUsers').find('#fullname').val();
        if ((fullname == 0)|(fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEUsers').find('#fullname')));
        }
        var username = $('#FrmAEUsers').find('#username').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEUsers').find('#username')));
        }
        var password = $('#FrmAEUsers').find('#password').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEUsers').find('#password')));
        }
        var divisionId = $('#FrmAEUsers').find('#divisionId').val();
        if ((divisionId == 0) | (divisionId == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEUsers').find('#divisionId')));
        }
        var designationId = $('#FrmAEUsers').find('#designationId').val();
        if ((designationId == 0) | (designationId == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEUsers').find('#designationId')));
        }
        if ($('#FrmAEUsers').find('#email').val() != 0) {
            leadsValidator.isEmail($('#FrmAEUsers').find('#email'));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateSuppliers = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAESuppliers').find('#sname').val();
        if ((fullname == 0)|(fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESuppliers').find('#sname')));
        }
        var username = $('#FrmAESuppliers').find('#saddress').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESuppliers').find('#saddress')));
        }
        var password = $('#FrmAESuppliers').find('#smobile').val();
        if ((password == 0) | (password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESuppliers').find('#smobile')));
        }     
        if ($('#FrmAESuppliers').find('#semail').val() != 0) {
            leadsValidator.isEmail($('#FrmAESuppliers').find('#semail'));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateLocations = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAELocations').find('#locationname').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAELocations').find('#locationname')));
        }
        var username = $('#FrmAELocations').find('#locationsymbol').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAELocations').find('#locationsymbol')));
        }
       
        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateMainAssests = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEMainAssests').find('#mcname').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEMainAssests').find('#mcname')));
        }
        var username = $('#FrmAEMainAssests').find('#mcsymbol').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEMainAssests').find('#mcsymbol')));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateSubAssests = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAESubAssests').find('#scname').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESubAssests').find('#scname')));
        }
        var username = $('#FrmAESubAssests').find('#scsymbol').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESubAssests').find('#scsymbol')));
        }
        var id = $('#FrmAESubAssests').find('#CountryName').val();
        if ((id == 0) | (id == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAESubAssests').find('#CountryName')));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var validateProducts = function (form) {
        var leadsValidator = new validator();

        var fullname = $('#FrmAEProducts').find('#prname').val();
        if ((fullname == 0) | (fullname == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#prname')));
        }
        var username = $('#FrmAEProducts').find('#prno').val();
        if ((username == 0) | (username == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#prno')));
        }
        var id = $('#FrmAEProducts').find('#mcid').val();
        if ((id == 0) | (id == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#mcid')));
        }
        var scid = $('#FrmAEProducts').find('#scid').val();
        if ((scid == 0) | (scid == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#scid')));
        }

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    var ClearSearch = function () {

        $('#frmSearchUsers #Name').val("");
        $('#frmSearchUsers #CountryName').val("");
        $('#frmSearchUsers #email').val("");
        SearchUsers();
    }

    var ClearSearchSupplier = function () {

        $('#frmSearchSuppliers #name').val("");
        $('#frmSearchSuppliers #email').val("");
        $('#frmSearchSuppliers #contact').val("");
        SearchSuppliers();
    }

    var ClearSearchLocaton = function () {

        $('#frmSearchLocations #Name').val("");
        $('#frmSearchLocations #Symbol').val("");
        SearchLocations();
    }

    var ClearSearchMainAssest = function () {

        $('#frmSearchMainAssests #Name').val("");
        $('#frmSearchMainAssests #Symbol').val("");
        SearchMainAssests();
    }

    var ClearSearchSubAssest = function () {

        $('#frmSearchSubAssests #Name').val("");
        $('#frmSearchSubAssests #Symbol').val("");
        $('#frmSearchSubAssests #CountryName').val("");
        SearchSubAssests();
    }

    var ClearSearchProduct = function () {

        $('#frmSearchProducts #Name').val("");
        $('#frmSearchProducts #Symbol').val("");
        $('#frmSearchProducts #CountryName').val("");
        SearchProducts();
    }

    var UserGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditUser + '?UserId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteUser + '?UserId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchUsers();
                    }

                });
            });
        }
    }

    var SupplierGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditSupplier + '?SId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteSupplier + '?SId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchSuppliers();
                    }

                });
            });
        }
    }

    var LocationGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditLocation + '?LocationId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteLocation + '?LocationId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchLocations();
                    }

                });
            });
        }
    }

    var MainAssestGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditMainAssest + '?MCId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteMainAssest + '?MCId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchMainAssests();
                    }

                });
            });
        }
    }

    var SubAssestGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditSubAssest + '?SCId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteSubAssest + '?SCId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchSubAssests();
                    }

                });
            });
        }
    }

    var ProductGridActions = function () {

        var UserId = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditProduct + '?PROId=' + UserId;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteProduct + '?PROId=' + UserId, null, function (result) {
                    if (result.Status) {
                        messagebox.success("User Successfully Deleted!");
                        SearchProducts();
                    }

                });
            });
        }
    }

    var OnMainCatogoryChange = function () {

        var base = $('#scid');
        var MC = $('#FrmAEProducts #mcid').val();
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
                        $('#scid').empty();
                        var tdropdown = "No Designations to Show"
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

    return {
        
        OnDivisionChange: OnDivisionChange,
        OnMainCatogoryChange:OnMainCatogoryChange,
        SearchUsers: SearchUsers,
        SearchSuppliers: SearchSuppliers,
        SearchLocations: SearchLocations,
        SaveUser: SaveUser,
        SaveLocation: SaveLocation,
        SaveSupplier: SaveSupplier,
        validateUsers: validateUsers,
        validateSuppliers: validateSuppliers,
        validateLocations: validateLocations,
        ClearSearch: ClearSearch,
        ClearSearchLocaton: ClearSearchLocaton,
        ClearSearchSupplier: ClearSearchSupplier,
        UserGridActions: UserGridActions,
        LocationGridActions: LocationGridActions,
        SupplierGridActions: SupplierGridActions,
        MainAssestGridActions: MainAssestGridActions,
        SubAssestGridActions: SubAssestGridActions,
        ProductGridActions:ProductGridActions,
        ClearSearchMainAssest: ClearSearchMainAssest,
        ClearSearchSubAssest: ClearSearchSubAssest,
        ClearSearchProduct:ClearSearchProduct,
        validateMainAssests: validateMainAssests,
        validateSubAssests: validateSubAssests,
        validateProducts:validateProducts,
        SaveMainAssest: SaveMainAssest,
        SaveSubAssest: SaveSubAssest,
        SaveProduct:SaveProduct,
        SearchMainAssests: SearchMainAssests,
        SearchSubAssests: SearchSubAssests,
        SearchProducts: SearchProducts

    };

}();


$(document).ready(function () {
    $('#divisionId').unbind('change').bind('change', user.OnDivisionChange);
    $('#FrmAEProducts #mcid').unbind('change').bind('change', user.OnMainCatogoryChange);

    $('#btnSearchUsers').unbind('click').bind('click', user.SearchUsers);
    $('#btnSearchLocations').unbind('click').bind('click', user.SearchLocations);
    $('#btnSearchSuppliers').unbind('click').bind('click', user.SearchSuppliers);
    $('#btnSearchMainAssests').unbind('click').bind('click', user.SearchMainAssests);
    $('#btnSearchSubAssests').unbind('click').bind('click', user.SearchSubAssests);
    $('#btnSearchProducts').unbind('click').bind('click', user.SearchProducts);

    $('#saveuser').unbind('click').bind('click', user.SaveUser);
    $('#savelocation').unbind('click').bind('click', user.SaveLocation);
    $('#savesupplier').unbind('click').bind('click', user.SaveSupplier);
    $('#savemainassest').unbind('click').bind('click', user.SaveMainAssest);
    $('#savesubassest').unbind('click').bind('click', user.SaveSubAssest);
    $('#saveproduct').unbind('click').bind('click', user.SaveProduct);

    $('#ClearSearch').unbind('click').bind('click', user.ClearSearch);
    $('#ClearSearchSuppliers').unbind('click').bind('click', user.ClearSearchSupplier);
    $('#ClearSearchLocations').unbind('click').bind('click', user.ClearSearchLocaton);
    $('#ClearSearchMainAssests').unbind('click').bind('click', user.ClearSearchMainAssest);
    $('#ClearSearchSubAssests').unbind('click').bind('click', user.ClearSearchSubAssest);
    $('#ClearSearchProducts').unbind('click').bind('click', user.ClearSearchProduct);

    $("#user-grid tbody tr a").unbind('click').bind('click', user.UserGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#location-grid tbody tr a").unbind('click').bind('click', user.LocationGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#supplier-grid tbody tr a").unbind('click').bind('click', user.SupplierGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#mainassest-grid tbody tr a").unbind('click').bind('click', user.MainAssestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#subassest-grid tbody tr a").unbind('click').bind('click', user.SubAssestGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
    $("#product-grid tbody tr a").unbind('click').bind('click', user.ProductGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
});




