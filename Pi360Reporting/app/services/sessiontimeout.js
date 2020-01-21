//Flot Line Chart
$(document).ready(function () {

    //$interval(function () {
    //    sessiontimeout();
    //}, 30000);  // in milliseconds

        //120000 = 2 secs



        $('#btn1000').on('click', function () {

            //$.ajax({
            //    url: '@Url.Action("LogOut", "Account")',
            //    type: "GET",
            //    dataType: "json",

            //    success: function () {
            //        //window.location.href = "/account/login";
            //        window.location.replace = "/account/login";
            //    } //end of success
            //});  //end of 1st ajax

            $('#label1000').html('working');

    }); //end of #logout


    $("#fname").autocomplete({
        source: function (request, response) {
            $.ajax({
                //url: '@Url.Action("test", "api/dashboard2")',
                //url: 'http://localhost:43056/api/dashboard2/test/a',
               // url: '@Url.Action("TestAutoComplete", "TestCont")',
                url: '/TestCont/TestAutoComplete/',
                //type: "POST",
                dataType: "json",
                data: { inputvalu: $("#fname").val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item,
                            value: item
                        };
                    }));
                }
            });
        },
        // minChars: 1,
        //delay: 60,
        messages: {
            noResults: "",
            results: ""
        }
    });

});

