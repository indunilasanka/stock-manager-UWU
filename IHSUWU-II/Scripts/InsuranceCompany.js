var InsuranceCompanies = function () {
    var EditInsuranceComapany = function () {
        
        var id = $(this).data('id');
        var option = $(this).data('option');
        if (option == 1) {
            $.get(UrlConfig.EditInsuranceComapany + '/' + id, null, function (result) {
                $('#ManageInsuranceCompany').html(result);
                $("#ManageInsuranceCompany").modal('show');
            });
        } else {
            $.get(UrlConfig.DeleteInsuranceComapany + '/' + id, null, function (result) {
                $('#ManageInsuranceCompany').html(result);
                $("#ManageInsuranceCompany").modal('show');
            });
        }
    }

        var AddNewInsuranceCompany = function () {

            $.get(UrlConfig.EditInsuranceComapany, null, function (result) {
                $('#ManageInsuranceCompany').html(result);
                $("#ManageInsuranceCompany").modal('show');
            });
        }
        var onCountryChange = function () {
            //$.uniform.update('select');
            progress.show()
            var base = $('#StateId');
            base.empty();

            if ($(this).val() != '' && $(this).val() != '-1') {
                base.append($('<option/>').text('Loading...').val('-1'));

                $.ajax({
                    type: "POST",
                    url: UrlConfig.GetStatesForCountry,
                    data: "{'CountryId' : '" + $(this).val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        base.empty();

                        var states = eval(data);

                        $.each(states, function (index) {
                            var id = states[index].Value;
                            var name = states[index].Text;
                            base.append($('<option/>').text(name).val(id));
                        });
                        progress.hide()
                    },
                    error: function () {
                        base.empty();

                        messagebox.error("Error loading State");
                    }
                });
            } else {

            }

        }
        var onEditCountryChange = function () {
            progress.show()
            var base = $('#secondary-dialog').find('#frmEditInsuranceCompany #StateEId');
            base.empty();

            if ($(this).val() != '' && $(this).val() != '-1') {
                base.append($('<option/>').text('Loading...').val('-1'));

                $.ajax({
                    type: "POST",
                    url: UrlConfig.GetStatesForCountry,
                    data: "{'CountryId' : '" + $(this).val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        base.empty();

                        var states = eval(data);

                        $.each(states, function (index) {
                            var id = states[index].Value;
                            var name = states[index].Text;
                            base.append($('<option/>').text(name).val(id));
                        });
                        progress.hide()
                    },
                    error: function () {
                        base.empty();

                        messagebox.error("Error loading State");
                    }
                });
            } else {

            }

        }
        var gridActions = function () {



            var id = $(this).attr('lid');
            var popup = $('#secondary-dialog');

            progress.show("Loading.....");

            popup.empty().dialog({
                title: 'Update Insurance Company',
                width: '490',
                height: '400',
                model: true,
                buttons: {
                    'Save': UpdateInsuranceCompany,
                    'Close': closeDialog
                }
            }).load(UrlConfig.EditInsuranceCompany + "?InsuranceCompanyId=" + id, function () {
                popup.find('#frmEditInsuranceCompany #CountryEId').unbind('change').bind('change', InsuranceCompanies.onEditCountryChange);
                popup.find('#frmEditInsuranceCompany #CountryEId').trigger('change');
                progress.hide();
            });

        }
        var closeDialog = function () {
            $('#secondary-dialog').dialog('close');
        }
        var UpdateInsuranceCompany = function () {
            var frmEditInsuranceCompany = $(this).find('#frmEditInsuranceCompany');


            frmEditInsuranceCompany.submit(function (e) {

                e.preventDefault();
                if (!validateInsuranceCompany(frmEditInsuranceCompany)) return false;

                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {

                        if (result.Status) {
                            closeDialog();
                            messagebox.success(result.Message);
                            $('#InsuranceSearch').load(UrlConfig.GetAllInsuranceCompanies, function () {
                                $('#InsuranceSearch tbody tr a').unbind('click').bind('click', InsuranceCompanies.EditInsuranceComapany)


                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
                            });

                        }
                        else {
                            messagebox.error(result.Message);
                        }


                    }
                });

                return false;
            }).trigger('submit');

        }
        var clearAll = function () {
            $("input[type=text]").val("");
            $("#CountryId").prop('selectedIndex', "-- Select Make --");
            $("#StateId").prop('selectedIndex', "-- Select Make --");
        }
        var validateInsuranceCompany = function (form) {

            var addInsuranceCompanyValidator = new validator();
            addInsuranceCompanyValidator.isEmpty(form.find('#CompanyName'))
                            .isEmpty(form.find('#Address'));

            if (form.find('#Phone1').val() != '') {
                addInsuranceCompanyValidator.isPhoneNumber(form.find('#Phone1'));

            }

            if (!addInsuranceCompanyValidator.isValid) {
                messagebox.error(addInsuranceCompanyValidator.getErrors());
                return false;
            }
            return true;
        }
        var PostInsuranceCompany = function (e) {

            e.preventDefault();
            if (!validateInsuranceCompany($("#frmInsuranceCompany"))) return false;
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                async: false,
                success: function (result) {
                    if (result.Status) {
                        messagebox.success(result.Message);
                        InsuranceCompanies.clearAll();
                    }
                    else {
                        messagebox.error(result.Message);
                    }
                }
            });
            return false;
        }
        return {
            onCountryChange: onCountryChange,
            onEditCountryChange: onEditCountryChange,
            PostInsuranceCompany: PostInsuranceCompany,
            clearAll: clearAll,
            gridActions: gridActions,
            AddNewInsuranceCompany: AddNewInsuranceCompany,
            EditInsuranceComapany: EditInsuranceComapany
        }
    }();
    $(document).ready(function () {
       
        //$('#frmInsuranceCompany').unbind('submit').bind('submit', InsuranceCompanies.PostInsuranceCompany);
        //$('#frmInsuranceCompany #CountryId').unbind('change').bind('change', InsuranceCompanies.onCountryChange);
        //$('#buttonCancel').unbind('click').bind('click', InsuranceCompanies.clearAll);
        $('#InsuranceSearch tbody tr a').unbind('click').bind('click', InsuranceCompanies.EditInsuranceComapany)
       .hover(function () {
           $(this).css({ cursor: 'pointer' });
       }, function () {
           $(this).css({ cursor: 'default' });
       });
        $('#btnAddInsuranceCompany').unbind('click').bind('click', InsuranceCompanies.AddNewInsuranceCompany);

        $("#UpdateInsuranceCompany").live('click', function () {

            $.ajax({
                type: 'post',
                dataType: 'html',
                url: UrlConfig.AddNewInsuranceCompany,
                async: true,
                data: $("#frmEditInsuranceCompany").serialize(),
                success: function (result) {
                    $('#ManageInsuranceCompany').modal('hide');
                    if (result.Status) {


                        $('#InsuranceSearch').load(UrlConfig.GetAllInsuranceCompanies, function () {
                            $('#InsuranceSearch tbody tr a').unbind('click').bind('click', InsuranceCompanies.EditInsuranceComapany)
      .hover(function () {
          $(this).css({ cursor: 'pointer' });
      }, function () {
          $(this).css({ cursor: 'default' });
      });
                        });
                        messagebox.success(result.Message);

                    }
                    else {
                        messagebox.error(result.Message);
                    }


                }
            });
        });
    });
