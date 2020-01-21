var fe2 = function () {
    $.ajax({
        url: '@Url.Action("sessionTimeOut", "Home")',
        type: "GET",
        dataType: "json",

        success: function (result) {
            if (result.v === "00") {
                //window.location = result.redirectUrl;
                window.location = "/account/login";
                //window.location.href = result.redirectUrl;;
                //document.location.reload();
            }
        } //end of success
    });  //end of 1st ajax
};