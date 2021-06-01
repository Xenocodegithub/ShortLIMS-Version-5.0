/// <reference path="bootstrap.js" />


var applicationSystem = {
    weekStartDate: null,
    weekEndDate: null,
    error_Dialog: null,
    success_Dialog: null,
    aspectedFormat: "MM/dd/yyyy",
    currentServerFormat: "MM/dd/yyyy",
    currentServerSeparator: "/",
    aspectedServerSeparator: "/",
    initialize: function () {
        applicationSystem.attachEvents();
    },

    attachEvents: function () {
        successdialog = $("#success-dialog-message").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    successdialog.dialog("close");
                }
            },
        });



    },

    getBaseUrl: function () {
        var url;
        url = location.protocol + "//" + location.host;
        alert(url);
    },

    get_hostname: function () {
        debugger;
        var url = window.location.href;
        var m = url.match(/^http:\/\/[^/]+/);
     //   m[0] = m[0] + "/VTIC";
        return m ? m[0] : null;
    },

    sortSelect: function (selElem) {
        var selEle = document.getElementById(selElem);
        var tmpAry = new Array();
        for (var i = 0; i < selEle.options.length; i++) {
            tmpAry[i] = new Array();
            tmpAry[i][0] = selEle.options[i].text;
            tmpAry[i][1] = selEle.options[i].value;
        }
        tmpAry.sort();
        alert(tmpAry);
        while (selEle.options.length > 0) {
            selEle.options[0] = null;
        }
        for (var i = 0; i < tmpAry.length; i++) {
            var op = new Option(tmpAry[i][0], tmpAry[i][1]);
            selEle.options[i] = op;
        }
        return;
    },

    convertSelectOptionIntoHtml: function (selEle) {
        var selEle = document.getElementById(selElem);
        for (var i = 0; i < selEle.options.length; i++) {
            
            tmpAry[i][0] = selEle.options[i].text;
            tmpAry[i][1] = selEle.options[i].value;
        }
    },

    changeDateFormatFromDDMMYYToMMDDYYYY: function(dateString){
        var _splitDateString = dateString.split("/");
        var newDateString = _splitDateString[1] + "/" + _splitDateString[0] + "/" + _splitDateString[2];
        return newDateString;
    },
    changeDateFormatToSeprate: function (dateString) {
        var _splitDateString = dateString.split("/");
        var newDateString = _splitDateString[1] + "/" + _splitDateString[0] + "/" + _splitDateString[2];
        return newDateString;
    },
    ReplaceDashToForSlash: function (dateString) {
        var _splitDateString = dateString.split("-");
        var newDateString = _splitDateString[0] + "/" + _splitDateString[1] + "/" + _splitDateString[2];
        return newDateString;
    },
    ReplaceSlashToForDash: function (dateString) {
        var _splitDateString = dateString.split("/");
        var newDateString = _splitDateString[2] + "-" + _splitDateString[1] + "-" + _splitDateString[0];
        return newDateString;
    },
    clearAddFields: function () {
        $('input:text').val("");
        $('input:number').val("");
        //$("select#TransportationChargesType").prop('selectedIndex', 0);
        $("select").prop('selectedIndex', 0);
    },

    onError: function (data) {
        debugger;
        //alert("Status Code : " + data.status + "\nMessage : " + data.responseText);
        //$('#errorHeading').text(data.statusText);
        //$('#errorDescription').text(data.responseText);
        //exceptionHandler.show();
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_DANGER,
            title: 'Error',
            message: data.message,
            buttons: [{
                label: 'Close',
                action: function (dialogRef) {
                    dialogRef.close();
                }
            }]
        });



    },

    onSuccess: function (data) {
        //alert("Status Code : " + data.status + "\nMessage : " + data.responseText);
        //$("#dialog-message").attr("title", "Success");
        //$('#successDescription').text(data);
        //successdialog.dialog("open");


        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_SUCCESS,
            title: 'Success',
            message: data.message,
            buttons: [{
                label: 'Close',
                action: function (dialogRef) {
                    dialogRef.close();
                }
            }]
        });

    },

    onWarning: function (data) {
        debugger;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            title: 'Warning',
            message: data.message,
            buttons: [{
                label: 'Close',
                cssClass: 'btn-warning',
                action: function (dialogRef) {
                    dialogRef.close();
                }
            }]
        });

    },

    onInfo: function (data) {
        debugger;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_INFO,
            title: 'Information',
            message: data.message,
            buttons: [{
                label: 'Close',
                action: function (dialogRef) {
                    dialogRef.close();
                }
            }]
        });

    },

    convertJsonDateFormatwithSeperator: function (jsonFormatedDate, dateFormat, seperator) {
        if (jsonFormatedDate != null) {
            var newdate = jsonFormatedDate;
            var dateString = newdate.toString().substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            if (parseInt(month) < 10) {
                month = "0" + month;
            }
            if (parseInt(day) < 10) {
                day = "0" + day;
            }
            if (dateFormat == 'mmddyy') {
                var ReceivedDate = month + seperator + day + seperator + year;
                return ReceivedDate;
            }
            else if (dateFormat == 'ddmmyy') {
                var ReceivedDate = day + seperator + month + seperator + year;
                return ReceivedDate;
            }
            else if (dateFormat == 'yymmdd') {
                var ReceivedDate = year + seperator + month + seperator + day;
                return ReceivedDate;
            }
        }
        else {
            return "";
        }

    },
    getWeekStartDateAndEndDate: function (year, month, date) {

        var _yearValue = year;
        var _monthValue = month;
        var _dateValue = date;
        var _date = new Date(_yearValue, parseInt(_monthValue) - 1, _dateValue);
        var _day = _date.getDay(_date);
        var _startDate = new Date(_date);
        _startDate.setDate(_date.getDate() - _day);
        var _endDate = new Date(_startDate);
        _endDate.setDate(_startDate.getDate() + 6);
        applicationSystem.weekStartDate = _startDate;
        applicationSystem.weekEndDate = _endDate;
        return _endDate;
    },

    addDays: function (_date, _day) {
        var _resultDate = new Date(_date);
        _resultDate.setDate(_date.getDate() + _day);
        return _resultDate;
    },

    subtractDays: function (_date, _day) {
        var _resultDate = new Date(_date);
        _resultDate.setDate(_date.getDate() - _day);
        return _resultDate;
    },

    getWeekStartDateAndEndDate1: function (date) {

        var _yearValue = date.getFullYear();
        var _monthValue = parseInt(date.getMonth()) + 1;
        var _dateValue = date.getDate();
        var _date = new Date(_yearValue, parseInt(_monthValue) - 1, _dateValue);
        var _day = _date.getDay(_date);
        var _startDate = new Date(_date);
        _startDate.setDate(_date.getDate() - _day);
        var _endDate = new Date(_startDate);
        _endDate.setDate(_startDate.getDate() + 6);
        applicationSystem.weekStartDate = _startDate;
        applicationSystem.weekEndDate = _endDate;
        return _endDate;
    },
    changeDateFormat: function (_date, dateFormat) {
        var currentTime = new Date(_date);
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        if (parseInt(month) < 10) {
            month = "0" + month;
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        if (dateFormat == 'mmddyy') {
            var ReceivedDate = month + "/" + day + "/" + year;
            return ReceivedDate;
        }
        else if (dateFormat == 'ddmmyy') {
            var ReceivedDate = day + "/" + month + "/" + year;
            return ReceivedDate;
        }
        else if (dateFormat == 'yymmdd') {
            var ReceivedDate = year + "/" + month + "/" + day;
            return ReceivedDate;
        }

    },

    convertJsonDateFormat: function (jsonFormatedDate, dateFormat) {
        if (jsonFormatedDate != null) {
            var newdate = jsonFormatedDate;
            var dateString = newdate.toString().substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            if (parseInt(month) < 10) {
                month = "0" + month;
            }
            if (parseInt(day) < 10) {
                day = "0" + day;
            }
            if (dateFormat == 'mmddyy') {
                var ReceivedDate = month + "/" + day + "/" + year;
                return ReceivedDate;
            }
            else if (dateFormat == 'ddmmyy') {
                var ReceivedDate = day + "/" + month + "/" + year;
                return ReceivedDate;
            }
            else if (dateFormat == 'yymmdd') {
                var ReceivedDate = year + "/" + month + "/" + day;
                return ReceivedDate;
            }
        }
        else {
            return "";
        }


    },


    convertionOfDateFormat: function (dateString, aspectedFormat, currentFormat, aspectedSeparator, currentSeparator) {
        if (dateString == null || dateString == "") {
            return "";
        }
        var _dateString = dateString;
        var _splitedDate = _dateString.split(currentSeparator);
        if (currentFormat == "dd/MM/yyyy") {
            if (aspectedFormat == "MM/dd/yyyy") {
                var newDateString = _splitedDate[1] + aspectedSeparator + _splitedDate[0] + aspectedSeparator + _splitedDate[2];
                return newDateString;
            }
        }
    },


    convertionFromJsonDateFormat: function (jsonFormatedDate, aspectedFormat, aspectedSeparator) {
        if (jsonFormatedDate == null || jsonFormatedDate == "") {
            return "";
        }
        var newdate = jsonFormatedDate;
        var dateString = newdate.toString().substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        if (parseInt(month) < 10) {
            month = "0" + month;
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        if (aspectedFormat == 'MM/dd/yyyy') {
            var ReceivedDate = month + aspectedSeparator + day + aspectedSeparator + year;
            return ReceivedDate;
        }
        else if (aspectedFormat == 'dd/MM/yyyy') {
            var ReceivedDate = day + aspectedSeparator + month + aspectedSeparator + year;
            return ReceivedDate;
        }
    },
    convertionFromJsonDateFormatNew: function (jsonFormatedDate) {
        if (jsonFormatedDate == null || jsonFormatedDate == "") {
            return "";
        }
        var newdate = jsonFormatedDate;
        var dateString = newdate.toString().substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        //if (parseInt(month) < 10) {
        //    month = "0" + month;
        //}
        if (parseInt(month) == 1)
        {
            month = "Jan";
        }
        else if (parseInt(month) == 2) {
            month = "Feb";
        }
        else if (parseInt(month) == 3) {
            month = "Mar";
        }
        else if (parseInt(month) == 4) {
            month = "Apr";
        }
        else if (parseInt(month) == 5) {
            month = "May";
        }
        else if (parseInt(month) == 6) {
            month = "Jun";
        }
        else if (parseInt(month) == 7) {
            month = "Jul";
        }
        else if (parseInt(month) == 8) {
            month = "Aug";
        }
        else if (parseInt(month) == 9) {
            month = "Sep";
        }
        else if (parseInt(month) == 10) {
            month = "Oct";
        }
        else if (parseInt(month) == 11) {
            month = "Nov";
        }
        else if (parseInt(month) == 12) {
            month = "Dec";
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        var ReceivedDate = day + '-' + month + '-' + year;
        return ReceivedDate;

        //if (aspectedFormat == 'dd/MM/yyyy') {
        //    var ReceivedDate = month + aspectedSeparator + day + aspectedSeparator + year;
        //    return ReceivedDate;
        //}
        //else if (aspectedFormat == 'dd/MM/yyyy') {
        //    var ReceivedDate = day + aspectedSeparator + month + aspectedSeparator + year;
        //    return ReceivedDate;
        //}
    },
    convertionIntoSpecificDateFormat: function (_date, aspectedFormat, aspectedSeparator) {
        if (_date == null || _date == "") {
            return "";
        }
        var currentTime = new Date(_date);
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        if (parseInt(month) < 10) {
            month = "0" + month;
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        if (aspectedFormat == 'MM/dd/yyyy') {
            var ReceivedDate = month + aspectedSeparator + day + aspectedSeparator + year;
            return ReceivedDate;
        }
        else if (aspectedFormat == 'dd/MM/yyyy') {
            var ReceivedDate = day + aspectedSeparator + month + aspectedSeparator + year;
            return ReceivedDate;
        }
    },
    sortResults: function (json, prop, asc) {
        json = json.sort(function (a, b) {
            if (asc) {
                return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
            } else {
                return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
            }
        });
        return json;
    },
    convertionFromJsonDateFormatNew: function (jsonFormatedDate) {
        if (jsonFormatedDate == null || jsonFormatedDate == "") {
            return "";
        }
        var newdate = jsonFormatedDate;
        var dateString = newdate.toString().substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        //if (parseInt(month) < 10) {
        //    month = "0" + month;
        //}
        if (parseInt(month) == 1) {
            month = "Jan";
        }
        else if (parseInt(month) == 2) {
            month = "Feb";
        }
        else if (parseInt(month) == 3) {
            month = "Mar";
        }
        else if (parseInt(month) == 4) {
            month = "Apr";
        }
        else if (parseInt(month) == 5) {
            month = "May";
        }
        else if (parseInt(month) == 6) {
            month = "Jun";
        }
        else if (parseInt(month) == 7) {
            month = "Jul";
        }
        else if (parseInt(month) == 8) {
            month = "Aug";
        }
        else if (parseInt(month) == 9) {
            month = "Sep";
        }
        else if (parseInt(month) == 10) {
            month = "Oct";
        }
        else if (parseInt(month) == 11) {
            month = "Nov";
        }
        else if (parseInt(month) == 12) {
            month = "Dec";
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        var ReceivedDate = day + '-' + month + '-' + year;
        return ReceivedDate;
        //if (aspectedFormat == 'dd/MM/yyyy') {
        //    var ReceivedDate = month + aspectedSeparator + day + aspectedSeparator + year;
        //    return ReceivedDate;
        //}
        //else if (aspectedFormat == 'dd/MM/yyyy') {
        //    var ReceivedDate = day + aspectedSeparator + month + aspectedSeparator + year;
        //    return ReceivedDate;
        //}
    },

    convertionFromJsonDateFormatNewWithTime: function (jsonFormatedDate) {
        debugger
        if (jsonFormatedDate == null || jsonFormatedDate == "") {
            return "";
        }
        var newdate = jsonFormatedDate;
        var dateString = newdate.toString().substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        //if (parseInt(month) < 10) {
        //    month = "0" + month;
        //}
        var hours = currentTime.getHours();
        var minutes = currentTime.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;

        if (parseInt(month) == 1) {
            month = "Jan";
        }
        else if (parseInt(month) == 2) {
            month = "Feb";
        }
        else if (parseInt(month) == 3) {
            month = "Mar";
        }
        else if (parseInt(month) == 4) {
            month = "Apr";
        }
        else if (parseInt(month) == 5) {
            month = "May";
        }
        else if (parseInt(month) == 6) {
            month = "Jun";
        }
        else if (parseInt(month) == 7) {
            month = "Jul";
        }
        else if (parseInt(month) == 8) {
            month = "Aug";
        }
        else if (parseInt(month) == 9) {
            month = "Sep";
        }
        else if (parseInt(month) == 10) {
            month = "Oct";
        }
        else if (parseInt(month) == 11) {
            month = "Nov";
        }
        else if (parseInt(month) == 12) {
            month = "Dec";
        }
        if (parseInt(day) < 10) {
            day = "0" + day;
        }
        var ReceivedDate = day + '-' + month + '-' + year + " " + hours + ':' + minutes;
        return ReceivedDate;
    },
};

function roundToTwo(num) {
    //var getRound = +(Math.round(num + "e+2") + "e-2");
    //return getRound.toFixed(2);
    return +(Math.round(num + "e+2") + "e-2");
}