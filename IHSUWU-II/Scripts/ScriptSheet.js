$(document).ready(function () {

    $("#loginWindow").show();
    $("#btnSubmit").click(function() {
        
        var UserName = $("#userName").val();
        var Password = $("#password").val();
        if (UserName == "")
        {
            $("#error").text("Username is Empty").show();
            $("#error").fadeOut(5000);
        }
        else if (Password == "")
        {
            $("#error").text("Password is Empty").show();
            $("#error").fadeOut(5000);
        }
        else
        {
            $("#error").hide();
            $("#spinner").show();
            $("#loginWindow").fadeTo("fast", 0.8);
            $.ajax(
            {
                type: "POST",
                url: '/Home/Login',
                data: { UserName: UserName , Password: Password },
                success: function (data) {
                    if (data.Status == 'error') {
                        $("#loginWindow").stop(true).fadeTo(200, 1);
                        $("#spinner").hide();
                        $("#error").text(data.Message).show();
                        $("#error").fadeOut(5000);
                    }

                    else if(data.Status == 'url')
                    {
                        window.location.href = data.Message;
                    }
                },
                error: function (error) {
                }
            });
        }
    });
});
