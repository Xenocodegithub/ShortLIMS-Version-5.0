// the class to work with all the ajax request and
var ajaxRequest =
{
    makeRequest: function (requestUrl, type, contextData, successCallback, errorCallback, dataType, isasync) {

        //requestUrl = bspSystem.getBaseURL() + requestUrl;
        requestUrl = applicationSystem.get_hostname() + requestUrl;

        //requestUrl = applicationSystem.get_hostname() + requestUrl;
        //if ($('#myModalCommon').is(":visible")) {
        //    $("body").css("cursor", "default");
        //    $('#myModalCommon').modal('hide');
        //}
        //else {
        //    $("body").css("cursor", "progress");
        //    $('#myModalCommon').modal('show');
        //}
        if (dataType == null) {


            dataType = "json";
        }
        if (isasync == null) {
            isasync = true;
        }
        //    ajaxRequest.showBusyIndicator();
        switch (type) {
            case "GET":
                $.ajax({
                    type: "GET",
                    cache: false,
                    async: isasync,
                    contentType: "application/json; charset=utf-8",
                    url: requestUrl,
                    data: contextData,
                    dataType: dataType,
                    success: function (response) {
                        if (successCallback) {
                            //if ($('#myModalCommon').is(":visible")) {
                            //    $("body").css("cursor", "default");
                            //    $('#myModalCommon').modal('hide');
                            //}


                            successCallback(response);
                        }
                    },
                    error: function (response) {

                        //if ($('#myModalCommon').is(":visible")) {
                        //    $("body").css("cursor", "default");
                        //    $('#myModalCommon').modal('hide');
                        //}
                    },
                    complete: function (jqXHR, textStatus) {
                        //if ($('#myModalCommon').is(":visible")) {
                        //    $("body").css("cursor", "default");
                        //    $('#myModalCommon').modal('hide');
                        //}
                        //ajaxRequest.hideBusyIndicator();
                    }
                });
                break;

            case "POST":
                // Make POST http ajax request
                $.ajax({
                    type: "POST",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    url: requestUrl,
                    data: JSON.stringify(contextData),
                    dataType: dataType,
                    success: function (response) {
                        if (successCallback) {
                            
                            //if ($('#myModalCommon').is(":visible")) {
                            //    $("body").css("cursor", "default");
                            //    $('#myModalCommon').modal('hide');
                            //}
                            successCallback(response);
                        }
                    },
                    error: function (request, status, errorThrown) {
                        if (errorCallback) {
                            
                            //if ($('#myModalCommon').is(":visible")) {
                            //    $("body").css("cursor", "default");
                            //    $('#myModalCommon').modal('hide');
                            //}
                            errorCallback(request);

                        }
                        //alert(status);
                    },
                    statusCode:
                    {
                        400: function (data) {
                            
                            //if ($('#myModalCommon').is(":visible")) {
                            //    $("body").css("cursor", "default");
                            //    $('#myModalCommon').modal('hide');
                            //}
                            var validationResult = $.parseJSON(data.responseText);
                            $.publish("ShowValidationError", [validationResult]);
                        }
                    },
                    complete: function (jqXHR, textStatus) {
                        
                        // if ($('#myModalCommon').is(":visible")) {
                      //  $("body").css("cursor", "default");
                       
                      //  $('#myModalCommon').modal('hide');
                        //}
                        //ajaxRequest.hideBusyIndicator();
                    }
                });
                break;

        }
    },

    showBusyIndicator: function () {
        ajaxRequest.ajaxindicatorstart('loading data.. please wait..');

        //$(document.body).css({ 'cursor': 'wait' });
        //$.blockUI({
        //    message: '<img style="" src="/Images/n6d4w.gif" />',
        //    css: { backgroundColor: "transparent", border: 'none' }
        //}); ;
    },

    hideBusyIndicator: function () {
        ajaxRequest.ajaxindicatorstop();
        //$(document.body).css({ 'cursor': 'default' });
        //$.unblockUI();
    },

    ajaxindicatorstart: function (text) {
        if (jQuery('body').find('#resultLoading').attr('id') != 'resultLoading') {
            //jQuery('body').append('<div id="resultLoading" style="display:none"><div><span style="color:darkblue; "><b>' + text + '</b></span></div><div class="bg"></div></div>');
            jQuery('body').append('<div id="resultLoading" style="display:none"><div class="overlay" id="overlapContent"><i class="fa fa-spinner fa-spin" style="color:blue";></i></div></div>');
        }

        jQuery('#resultLoading').css({
            'width': '100%',
            'height': '100%',
            'position': 'fixed',
            'z-index': '10000000',
            'top': '0',
            'left': '0',
            'right': '0',
            'bottom': '0',
            'margin': 'auto'
        });

        jQuery('#resultLoading .bg').css({
            'background': '#000000',
            'opacity': '0.7',
            'width': '100%',
            'height': '100%',
            'position': 'absolute',
            'top': '0'
        });

        jQuery('#resultLoading>div:first').css({
            'width': '250px',
            'height': '75px',
            'text-align': 'center',
            'position': 'fixed',
            'top': '0',
            'left': '0',
            'right': '0',
            'bottom': '0',
            'margin': 'auto',
            'font-size': '16px',
            'z-index': '10',
            'color': '#ffffff'

        });

        jQuery('#resultLoading .bg').height('100%');
        jQuery('#resultLoading').fadeIn(300);
        jQuery('body').css('cursor', 'wait');
    },

    ajaxindicatorstop: function () {
        jQuery('#resultLoading .bg').height('100%');
        jQuery('#resultLoading').fadeOut(300);
        jQuery('body').css('cursor', 'default');
    }

};