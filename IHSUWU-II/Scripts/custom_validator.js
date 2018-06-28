var validator = function () {

    this.errors = new Object(),
    this.isValid = true,

    this.isEmpty = function (o) {
        if (this.isNullOrEmpty(o.val())) {
            this.pushError(o);
        }
        return this;
    }
    this.isPercentage = function (o) {
        if (!this.isValidPercentage(o.val())) {
            this.pushError(o);
        }
        return this;
    }
    this.isNotEqual = function (s, d) {
        if (s.val() != d.val()) {
            this.pushError(d);
        }
        return this;
    }

    this.isPhoneNumber = function (o) {
        if (!this.isVaildPhoneNumber(o.val())) {
            this.pushError(o);
        }
        return this;
    }
    this.isEmail = function (o) {
        if (!this.isValidEmail(o.val())) {
            this.pushError(o);
        }
        return this;
    }
    this.getErrors = function () {
        return generateErrorMessage(this.errors)
    }

    this.isNullOrEmpty = function (s) {
        return (s == null || s == "" || s.length == 0);
    }

    this.isVaildPhoneNumber = function (s) {
        //var regx = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        //var regx = /^\(?([0-9]{7,20})$/;
        var regx = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
        return String(s).search(regx) != -1;
    }

    this.isNumber = function (s) {
        var regx = /^[0-9]*$/;
        return String(s).search(regx) != -1;
    }
    this.isValidPercentage = function (s) {
      
        var regx = /^100(\.0{0,2}?)?$|^\d{0,2}(\.\d{0,2})?$/;
        return String(s).search(regx) != -1;
        
    }
    this.isValidEmail = function (s) {
        var regx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return String(s).search(regx) != -1;
    }

    this.pushError = function (o) {
        this.isValid = false;
        this.errors[o.attr('id')] = o.attr('verrormsg');
    }

    this.setError = function (id, s) {
        this.isValid = false;
        this.errors[id] = s;
    }

   

    this.isName = function (s) {
        var regx = /^([a-zA-Z])$/;
        return String(s).search(regx) != -1;
    }

    this.isNumeric = function (o) {

        if (!this.isNumber(o.val())) {

            this.pushError(o);
        }
        return this;
    }

}

function generateErrorMessage(errors) {
    var s = '<b style="color:#d8192c">Please correct following errors</b>';
    s += "<ul>";
    $.each(errors, function (k, v) {
        s += "<li>" + v + "</li>";
    });
    s += "</ul>";

    return s;
}