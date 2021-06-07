var Validation = {

    initialize: function () {
        Validation.Events();
    },

    Events: function () {

        $('.allowNumbersOnly').keydown(function (e) {
            var keyCode = (e.keyCode ? e.keyCode : e.which);
            if (keyCode > 64 && keyCode < 91 || keyCode > 218 && keyCode < 223 || keyCode > 185 && keyCode < 193 || keyCode > 105 && keyCode < 112 || keyCode == 16 || keyCode == 17 || keyCode == 18 || (((e.shiftKey && e.which) > 47) && ((e.shiftKey && e.which) < 58)) || (((e.shiftKey && e.which) > 218) && ((e.shiftKey && e.which) < 223)) || (((e.shiftKey && e.which) > 185) && ((e.shiftKey && e.which) < 193)) || (((e.shiftKey && e.which) > 105) && ((e.shiftKey && e.which) < 112))) {
                e.preventDefault();
            }
        });
    
        // ASCII VALUE of [.] == 191 and 110

        $('.allowNumbersWithDecimalOnly').keydown(function (e) {
            var keyCode = (e.keyCode ? e.keyCode : e.which);
            if (keyCode > 64 && keyCode < 91 || keyCode > 218 && keyCode < 223 || keyCode > 185 && keyCode < 190 || keyCode == 191 || keyCode == 192 || keyCode > 105 && keyCode < 110 || keyCode == 111 || keyCode == 16 || keyCode == 17 || keyCode == 18 || (((e.shiftKey && e.which) > 47) && ((e.shiftKey && e.which) < 58)) || (((e.shiftKey && e.which) > 218) && ((e.shiftKey && e.which) < 223)) || (((e.shiftKey && e.which) > 185) && ((e.shiftKey && e.which) < 193)) || (((e.shiftKey && e.which) > 105) && ((e.shiftKey && e.which) < 112))) {
                e.preventDefault();
            }
            else {
                if (keyCode == 190 || keyCode == 110) {
                    var id = $(this).attr("id");
                    if ($('#' + id).val().indexOf(".") >= 0) {
                        e.preventDefault();
                    }
                }
            }
        });


    }
}