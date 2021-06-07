var InventoryMaintainanceAndCalibration = {
    Type: null,
    
    initialize: function () {
        InventoryMaintainanceAndCalibration.attachEvents();
    },
    attachEvents: function () {
        $('#buttonSave').click(function () {
            debugger;
                InventoryMaintainanceAndCalibration.addInventoryMaintainanceAndCalibration();
                return false;
           
        });

        $("#ItemID").change(function () {
            debugger;
            $('#InventoryBasicItemDetailsID').val(-1);
            if (parseInt($("#ItemID").val()) > 0) {
                InventoryMaintainanceAndCalibration.fatchSerialNumberList();
            }
        });
        $("#InventoryMaintainanceAndCalibrationScheduleDatesID").change(function () {             
            debugger;
            var _selectedIndex = parseInt($('#InventoryMaintainanceAndCalibrationScheduleDatesID').prop('selectedIndex'));
            var _length = parseInt($('#InventoryMaintainanceAndCalibrationScheduleDatesID').prop('length'));
            if (_selectedIndex === _length - 1) {
                //var __firstSelectedDateValue = $("#InventoryMaintainanceAndCalibrationScheduleDatesID option:eq(1)").val();
                //var _secondSelectedDateValue = $("#InventoryMaintainanceAndCalibrationScheduleDatesID option:eq(2)").val();
                //var _firstSelectedDateText = $("#InventoryMaintainanceAndCalibrationScheduleDatesID option[value='" + __firstSelectedDateValue + "']").text();
                //var _secondSelectedDateText = $("#InventoryMaintainanceAndCalibrationScheduleDatesID option[value='" + _secondSelectedDateValue + "']").text();
                //var _firstSplitDate = _firstSelectedDateText.split("/");
                //var _secondSplitDate = _secondSelectedDateText.split("/");

                //console.log(_firstSplitDate);
                //console.log(_secondSplitDate);
                //console.log($("#InventoryMaintainanceAndCalibrationScheduleDatesID option[value='" + __firstSelectedDateValue + "']").text());
                //console.log($("#InventoryMaintainanceAndCalibrationScheduleDatesID option[value='" + _secondSelectedDateValue + "']").text());

                //var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                //var firstDate = new Date(_firstSplitDate[2], _firstSplitDate[0], _firstSplitDate[1]);
                //var secondDate = new Date(_secondSplitDate[2], _secondSplitDate[0], _secondSplitDate[1]);
                //var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
                
                //var date = new Date($('#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected').text());
                //var newdate = new Date(date);
                //newdate.setDate(newdate.getDate() + parseInt(diffDays));
                //alert(newdate);
                //var dd = newdate.getDate();
                //var mm = newdate.getMonth() + 1;
                //var y = newdate.getFullYear();

                //$("#NextCalibrationDate").val(applicationSystem.changeDateFormat(newdate, "mmddyy"));

                var date = new Date($('#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected').text());
                //var date = new Date(
                //                        applicationSystem.convertionOfDateFormat(
                //                        $("#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected").text(),
                //                        applicationSystem.aspectedFormat,
                //                        applicationSystem.currentServerFormat,
                //                        applicationSystem.aspectedServerSeparator,
                //                        applicationSystem.currentServerSeparator
                //                    ));
                //alert(date);
                var _occuranceFrequency = $('#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected').attr('occurancefrequency');
                if (_occuranceFrequency != null) {
                    //alert(date.getMonth());
                    if (parseInt(_occuranceFrequency) == 1) {
                        date.setMonth(date.getMonth() + 1);
                    }
                    else if (parseInt(_occuranceFrequency) == 2) {
                        date.setMonth(date.getMonth() + 3);
                    }
                    else if (parseInt(_occuranceFrequency) == 3) {
                        date.setMonth(date.getMonth() + 6);
                    }
                    else if (parseInt(_occuranceFrequency) == 4) {
                        date.setMonth(date.getMonth() + 12);
                    }
                }
                debugger;
                //$("#NextCalibrationDate").val(applicationSystem.changeDateFormat(date, "mmddyy"));
                var d = applicationSystem.convertionIntoSpecificDateFormat(date, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator);
                if (d == "NaN/NaN/NaN") {
                    //For Last Maintenance date NextCalibrationDate will be not available
                    $("#NextCalibrationDate").val('');
                }
                else {
                    $("#NextCalibrationDate").val(applicationSystem.convertionIntoSpecificDateFormat(date, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator));
                }
            }
            else {
                $("#NextCalibrationDate").val($('#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected').next().text());
            }
            

            //alert($("#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected").text());
            //var tableRow = $("#myTable tbody td").filter(function () {
            //    return $(this).text() == $("#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected").text();
            //}).closest("tr").attr('datatableid');
            
            InventoryMaintainanceAndCalibration.fatchItemsByID($("#myTable tbody td").filter(function () {
                return $(this).text() == $("#InventoryMaintainanceAndCalibrationScheduleDatesID option:selected").text();
            }).closest("tr").attr('datatableid'));
            debugger;
        });

        $("#InventoryBasicItemDetailsID").change(function () {
            debugger;
            if (parseInt($("#InventoryBasicItemDetailsID").val()) > 0) {
                $("#InventoryBasicDetailsID").val($('#InventoryBasicItemDetailsID option:selected').attr("inventorybasicdetailsid"));

                InventoryMaintainanceAndCalibration.fatchListByBasicItemsDetailsAndItemsID();
                InventoryMaintainanceAndCalibration.fatchFrequencyDates();
                
            }
        });

        $('#myTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var operation = $(this).attr('operation');
            if (operation == "Edit") {
                if (datatableid != undefined) {
                    InventoryMaintainanceAndCalibration.fatchItemsByID(datatableid);
                }
                
            }
            else if (operation == "Delete") {
                 

                var dialog = BootstrapDialog.show({
                    type: BootstrapDialog.TYPE_WARNING,
                    title: 'Warning',
                    message: "Are you sure, you wants to delete this sameple?",
                    buttons: [
                                {
                                    icon: 'glyphicon glyphicon-trash',
                                    label: 'ReNew Inventory',
                                    cssClass: 'btn-warning',
                                    action: function () {
                                        SampleAndParameterDetails.deleteSampleDetails(datatableid);
                                        dialog.close();
                                        $("#" + listItemId).remove();
                                        //$(this).closest('tr').remove();
                                    }
                                },
                                {
                                    label: 'Close',
                                    cssClass: 'btn-warning',
                                    action: function (dialogRef) {
                                        dialogRef.close();
                                    }
                                }
                    ]
                });
            }
            return false;
        });
    },
    addInventoryMaintainanceAndCalibration: function () {
       debugger
        var data = InventoryMaintainanceAndCalibration.getInventoryMaintainanceAndCalibration();
        //ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/Maintainance', "POST", data, InventoryMaintainanceAndCalibration.onSuccess, InventoryMaintainanceAndCalibration.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/InventoryMaintainanceAndCalibration/Maintainance",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryMaintainanceAndCalibration.onSuccess(data)
            },
            failure: function (data) {
                InventoryMaintainanceAndCalibration.onError()
            },
        });
    },
    fatchSerialNumberList: function () {
        debugger;
        var data = {
            "ItemID": ""
        };
        data.ItemID = $("#ItemID").val();
        data.InventoryBasicDetailsID = $("#InventoryBasicDetailsID").val();
       // ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchSerialNumbers', "POST", data, InventoryMaintainanceAndCalibration.onSuccessFatchSerialNumberList, InventoryMaintainanceAndCalibration.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/InventoryMaintainanceAndCalibration/FatchSerialNumbers",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryMaintainanceAndCalibration.onSuccessFatchSerialNumberList(data)
            },
            failure: function (data) {
                InventoryMaintainanceAndCalibration.onError()
            },
        });

    },
    fatchFrequencyDates: function () {
         debugger
        var data = {
            "InventoryBasicDetailsID": ""
        };
        data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID").val();
        data.Type = InventoryMaintainanceAndCalibration.Type
       // ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchFrequencyDates', "POST", data, InventoryMaintainanceAndCalibration.onSuccessFatchFrequencyDates, InventoryMaintainanceAndCalibration.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/InventoryMaintainanceAndCalibration/FatchFrequencyDates",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryMaintainanceAndCalibration.onSuccessFatchFrequencyDates(data)
            },
            failure: function (data) {
                InventoryMaintainanceAndCalibration.onError()
            },
        });

    },
    fatchListByBasicItemsDetailsAndItemsID: function () {
         debugger
        var data = {
            "InventoryBasicDetailsID": ""
        };
        data.ItemID = $("#ItemID").val();
        data.InventoryBasicDetailsID = $("#InventoryBasicDetailsID").val();
        data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID").val();
        data.Type = InventoryMaintainanceAndCalibration.Type;
      //  ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchListByBasicItemsDetailsAndItemsID', "POST", data, InventoryMaintainanceAndCalibration.onSuccessFatchListByBasicItemsDetailsAndItemsID, InventoryMaintainanceAndCalibration.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/InventoryMaintainanceAndCalibration/FatchListByBasicItemsDetailsAndItemsID",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryMaintainanceAndCalibration.onSuccessFatchListByBasicItemsDetailsAndItemsID(data)
            },
            failure: function (data) {
                InventoryMaintainanceAndCalibration.onError()
            },
        });

    },
    fatchItemsByID: function (datatableid) {
     debugger
        if (datatableid != undefined) {
            var data = {
                "ID": ""
            };
            data.ID = datatableid;
            data.Type = InventoryMaintainanceAndCalibration.Type;
            //ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchItemsByID', "POST", data, InventoryMaintainanceAndCalibration.onSuccessFatchItemsByID, InventoryMaintainanceAndCalibration.onError);

            $.ajax({
                type: 'Post',
                url: "/Inventory/InventoryMaintainanceAndCalibration/FatchItemsByID",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    InventoryMaintainanceAndCalibration.onSuccessFatchItemsByID(data)
                },
                failure: function (data) {
                    InventoryMaintainanceAndCalibration.onError()
                },
            });

        }
        else {
            $("#ID").val("");
            $("#Auditor").val("");
            $("#AuditDate").val("");
            $("#AuditObservations").val("");
            $("#ActionTaken").val("");
            $("#CompletionStatus").val(-1);
            $('#CalibrationStartDate').val("");
            $('#CalibrationEndDate').val("");
            
        }
    },
    bindBasicItemsOptionList: function (data) {
        if (data != null) {
            var ItemsList = [];
            ItemsList = data;
            var count = ItemsList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Serial Number...</option>";
            for (i = 0; i < ItemsList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + ItemsList[i].ID + "'  InventoryBasicDetailsID = '" + ItemsList[i].InventoryBasicDetailsID + "'>" + ItemsList[i].InventoryBasicItemDetailsNumber + "</option>";
            }//title = '" + ItemsList[i].ID + "'
            $("#InventoryBasicItemDetailsID").html($cloneRow);
            if (parseInt($("#HiddenInventoryBasicItemsDetailsID").val()) > 0) {
                $("#InventoryBasicItemDetailsID").val($("#HiddenInventoryBasicItemsDetailsID").val()).change();
            }
            //InventoryBasicDetail.BasicItemsOptionList = $cloneRow;
        }
    },
    bindFrequencyDatesOptionList: function (data) {
   
        if (data != null) {
            var ItemsList = [];
            ItemsList = data;
            var count = ItemsList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Frequency Dates...</option>";
            for (i = 0; i < ItemsList.length; ++i) {

                var newdate = ItemsList[i].ScheduleDate;
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
                var ReceivedDate = day + "/" + month + "/" + year;

                $cloneRow = $cloneRow + "<option value='" + ItemsList[i].ID + "'  AMCPeriods = '" + ItemsList[i].AMCPeriods + "' OccuranceFrequency = '" + ItemsList[i].OccuranceFrequency + "'>" + ReceivedDate + "</option>";
            }//title = '" + ItemsList[i].ID + "'
            $("#InventoryMaintainanceAndCalibrationScheduleDatesID").html($cloneRow);
            InventoryMaintainanceAndCalibration.removeDatesFromListOptions();
            if (parseInt($("#HiddenDateID").val()) > 0) {
                $("#InventoryMaintainanceAndCalibrationScheduleDatesID").val($("#HiddenDateID").val()).change();
            }
            //InventoryBasicDetail.BasicItemsOptionList = $cloneRow;
        }
    },
    bindTableByBasicItemsDetailsAndItemsID: function (data) {
         
        if (data != null) {
            var MaintainanceAndCalibrationItems = [];
            MaintainanceAndCalibrationItems = data;
            var count = MaintainanceAndCalibrationItems.length;
            $('#mytableBody').html("");
            var $cloneRow = "";
            for (i = 0; i < MaintainanceAndCalibrationItems.length; ++i) {


                var newdate = MaintainanceAndCalibrationItems[i].ScheduleDate;
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
                var ReceivedDate = day + "/" + month + "/" + year;

                var newdate1 = MaintainanceAndCalibrationItems[i].AuditDate;
                
                var dateString1 = newdate1.toString().substr(6);
                var currentTime1 = new Date(parseInt(dateString1));
                var month1 = currentTime1.getMonth() + 1;
                var day1 = currentTime1.getDate();
                var year1 = currentTime1.getFullYear();
                if (parseInt(month1) < 10) {
                    month1 = "0" + month1;
                }
                if (parseInt(day1) < 10) {
                    day1 = "0" + day1;
                }
                var ReceivedDate1 = day1 + "/" + month1 + "/" + year1;



                $cloneRow = $cloneRow + '<tr id="sample_' + i + '" datatableID = "' + MaintainanceAndCalibrationItems[i].ID + '" isdelete = "0" style="width:100%;">';
                $cloneRow = $cloneRow + '<td>' + (parseInt(i) + 1) + '</td>';
                //$cloneRow = $cloneRow + '<td>INV/' + MaintainanceAndCalibrationItems[i].InventoryBasicDetailsID + '</td>';
                $cloneRow = $cloneRow + '<td>' + MaintainanceAndCalibrationItems[i].InventoryBasicItemDetailsNumber + '</td>';
                $cloneRow = $cloneRow + '<td>' + ReceivedDate + '</td>';
                $cloneRow = $cloneRow + '<td>' + ReceivedDate1 + '</td>';
                $cloneRow = $cloneRow + '<td>' + MaintainanceAndCalibrationItems[i].Auditor + '</td>';
                $cloneRow = $cloneRow + '<td>' + MaintainanceAndCalibrationItems[i].CompletionStatus + '</td>';
                $cloneRow = $cloneRow + '<td><a href="#" title="Edit" class="glyphicon glyphicon-pencil" data-toggle="modal" data-target="#myModal" id="rowEdit_' + i + '" name="rowEdit_' + i + '" operation="Edit"></a>';
            }
            $('#mytableBody').html($cloneRow);
            InventoryMaintainanceAndCalibration.removeDatesFromListOptions();
        }
    },
    getInventoryMaintainanceAndCalibration: function () {
        debugger
        var data = {
            "ID":""
        };
        //data.ID = 0;
        data.ID = ($("#ID").val() == "") ? 0 : $("#ID").val();

        data.InventoryBasicItemDetailsNumber = $("#InventoryBasicItemDetailsNumber").val();
        data.ItemName = $("#ItemName").val();
        data.InventoryBasicDetailsID = $("#InventoryBasicDetailsID").val();
        data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID").val();
      
        data.Type = InventoryMaintainanceAndCalibration.Type;
        data.Auditor = $("#Auditor").val();

        //data.AuditDate = new Date($("#AuditDate").val());
        //data.MaintainanceStartDate = new Date($("#MaintainanceStartDate").val());
        //data.MaintainanceEndDate = new Date($("#MaintainanceEndDate").val());

        if ($('#AuditDate').val() != "") {
            data.AuditDate = new Date($('#AuditDate').val());
        }
        else {
            var from = $("#AuditDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.AuditDate = f;
        }

        if ($('#MaintainanceStartDate').val() != "") {
            data.MaintainanceStartDate = new Date($('#MaintainanceStartDate').val());
        }
        else {
            var from = $("#MaintainanceStartDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.MaintainanceStartDate = f;
        }

        if ($('#MaintainanceEndDate').val() != "") {
            data.MaintainanceEndDate = new Date($('#MaintainanceEndDate').val());
        }
        else {
            var from = $("#MaintainanceEndDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.MaintainanceEndDate = f;
        }


        //data.CalibrationStartDate = new Date($("#CalibrationStartDate").val());
        //data.CalibrationEndDate = new Date($("#CalibrationEndDate").val());

        if ($('#CalibrationStartDate').val() != "") {
            data.CalibrationStartDate = new Date($('#CalibrationStartDate').val());
        }
        else {
            var from = $("#CalibrationStartDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.CalibrationStartDate = f;
        }

        if ($('#CalibrationEndDate').val() != "") {
            data.CalibrationEndDate = new Date($('#CalibrationEndDate').val());
        }
        else {
            var from = $("#CalibrationEndDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.CalibrationEndDate = f;
        }

        //data.AuditDate = new Date(
        //                               applicationSystem.convertionOfDateFormat(
        //                               $("#AuditDate").val(),
        //                               applicationSystem.aspectedFormat,
        //                               applicationSystem.currentServerFormat,
        //                               applicationSystem.aspectedServerSeparator,
        //                               applicationSystem.currentServerSeparator
        //                           ));

        data.AuditObservations = $("#AuditObservations").val();
        data.ActionTaken = $("#ActionTaken").val();
        data.ItemID = $("#ItemID").val();
        data.CompletionStatus = $("#CompletionStatus").val();
        data.InventoryMaintainanceAndCalibrationScheduleDatesID = $("#InventoryMaintainanceAndCalibrationScheduleDatesID").val();
        data.CalibratorName = $("#CalibratorName").val();
        if ($('#NextCalibrationDate').val() != "") {
            var from1 = $("#NextCalibrationDate").val().split("/")
            var f1 = new Date(from1[2], from1[1] - 1, from1[0])
            data.NextCalibrationDate = f1;
            
        }
       
        //data.NextCalibrationDate = new Date(
        //                               applicationSystem.convertionOfDateFormat(
        //                               $("#NextCalibrationDate").val(),
        //                               applicationSystem.aspectedFormat,
        //                               applicationSystem.currentServerFormat,
        //                               applicationSystem.aspectedServerSeparator,
        //                               applicationSystem.currentServerSeparator
        //));

        return data;
    },
    setInventoryMaintainanceAndCalibration: function (data) {
        debugger
        $("#ID").val(data.ID);
        $("#InventoryBasicDetailsID").val(data.InventoryBasicDetailsID);
        $("#InventoryBasicItemDetailsID").val(data.InventoryBasicItemDetailsID);
        $("#Auditor").val(data.Auditor);
        if (data.AuditDate != null) {
            var newdate = data.AuditDate;
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
            var ReceivedDate = day + "/" + month + "/" + year;
            $("#AuditDate").val(ReceivedDate);
            $("#AuditDateval").val(ReceivedDate);
        }
        if (data.CalibrationStartDate != null) {
            var newdateMain = data.CalibrationStartDate;
            var dateStringMain = newdateMain.toString().substr(6);
            var currentTimeMain = new Date(parseInt(dateStringMain));
            var month1 = currentTimeMain.getMonth() + 1;
            var day1 = currentTimeMain.getDate();
            var year1 = currentTimeMain.getFullYear();
            if (parseInt(month1) < 10) {
                month1 = "0" + month1;
            }
            if (parseInt(day) < 10) {
                day1 = "0" + day1;
            }
            var CalibrationStartDate1 = day1 + "/" + month1 + "/" + year1;
            $("#CalibrationStartDate").val(CalibrationStartDate1);
            $("#CalibrationStartDateval").val(CalibrationStartDate1);
        }
        
        if (data.CalibrationEndDate != null) {
            var newdateEnd = data.CalibrationEndDate;
            var dateStringEnd = newdateEnd.toString().substr(6);
            var currentTimeEnd = new Date(parseInt(dateStringEnd));
            var month2 = currentTimeEnd.getMonth() + 1;
            var day2 = currentTimeEnd.getDate();
            var year2 = currentTimeEnd.getFullYear();
            if (parseInt(month2) < 10) {
                month1 = "0" + month1;
            }
            if (parseInt(day2) < 10) {
                day1 = "0" + day1;
            }
            var CalibrationEndDate2 = day2 + "/" + month2 + "/" + year2;
            $("#CalibrationEndDate").val(CalibrationEndDate2);
            $("#CalibrationEndDateval").val(CalibrationEndDate2);
        }
        $("#AuditObservations").val(data.AuditObservations);
        $("#ActionTaken").val(data.ActionTaken);
        $("#ItemID").val(data.ItemID);
        $("#CompletionStatus").val(data.CompletionStatus);
        $("#CalibratorName").val(data.CalibratorName);
         
        //$("#NextCalibrationDate").val(applicationSystem.convertJsonDateFormat(data.NextCalibrationDate, "mmddyy"));
        var x1 = applicationSystem.convertionFromJsonDateFormat(data.NextCalibrationDate, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator);
        var y1 = x1.split("/");
        var z1 = y1[1] + "/" + y1[0] + "/" + y1[2];
        if (z1 == "undefined//undefined") {
            $("#NextCalibrationDate").val('');
        }
        else {
            $("#NextCalibrationDate").val(z1);
        }
        $("#InventoryMaintainanceAndCalibrationScheduleDatesID").val(data.InventoryMaintainanceAndCalibrationScheduleDatesID);
    },
    onSuccess: function (data) {
   debugger
        if (data.status=="success") {
            //applicationSystem.onSuccess(data);
            var url = "";
            if (data.TypeCM=="Calibration") {
                //url = applicationSystem.get_hostname() + "/Inventory/InventoryMaintainanceAndCalibration/CalibrationList";
                location.href = '/Inventory/InventoryMaintainanceAndCalibration/CalibrationList';
            }
            if (data.TypeCM=="Maintainance") {
               // url = applicationSystem.get_hostname() + "/Inventory/InventoryMaintainanceAndCalibration/MaintainanceList";
                location.href = '/Inventory/InventoryMaintainanceAndCalibration/MaintainanceList';
            }
           // window.location.href = url;
            //var _itemID = $("#ItemID").val();
            //$("#ItemID").val(-1).change();
            //$("#InventoryBasicDetailsID").val(-1).change();

            //$("#CalibratorName").val("");
            //$("#Auditor").val("");
            //$("#InventoryMaintainanceAndCalibrationScheduleDatesID").val(-1).change();
            //$("#AuditDate").val("");
            //$("#AuditObservations").val("");

            //$("#ActionTaken").val("");
            //$("#CompletionStatus").val(-1).change();
            //$("#NextCalibrationDate").val("");
            //$("#txtSampleUploadFile").val("");
            // 
            //if (parseInt($("#InventoryBasicDetailsID").val()) > 0) {
            //    //InventoryMaintainanceAndCalibration.fatchFrequencyDates();
            //     
            //    InventoryMaintainanceAndCalibration.fatchListByBasicItemsDetailsAndItemsID();
            //}

            //$("input").val("");
            //$("textarea").val("");
            //$('#mytableBody').html("");
            //$("select").val(-1).change();
            //$("#buttonSave").val("Submit");

            //$("#ItemID").val(_itemID);
        }
        else if (data.status == "info") {
            applicationSystem.onInfo(data);
        }
        else if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onSuccessFatchSerialNumberList: function (data) {
        debugger;

        if (data.status == "success") {
            InventoryMaintainanceAndCalibration.bindBasicItemsOptionList(data.BasicItemsDetails);
        }
        if (data.status == "fail") {
        }
    },
    onSuccessFatchFrequencyDates: function (data) {
         
        
        if (data.status == "success") {
            InventoryMaintainanceAndCalibration.bindFrequencyDatesOptionList(data.FrequencyDate);
        }
        if (data.status == "fail") {
        }
    },
    onSuccessFatchListByBasicItemsDetailsAndItemsID: function (data) {
         
        
        if (data.status == "success") {
            InventoryMaintainanceAndCalibration.bindTableByBasicItemsDetailsAndItemsID(data.List);
        }
        if (data.status == "fail") {
        }
    },
    onSuccessFatchItemsByID: function(data){
        if (data.status == "success") {
            InventoryMaintainanceAndCalibration.setInventoryMaintainanceAndCalibration(data.Entity[0]);
        }
        if (data.status == "fail") {
        }
    },
    onError: function (data) {
        //applicationSystem.onError(data);
    },
    removeDatesFromListOptions: function () {
      
        var datesOptionLength = $('#InventoryMaintainanceAndCalibrationScheduleDatesID > option').length;
        if (parseInt(datesOptionLength) <= 1) {
            return false;
        }
        var tableNoOfRows = $('#mytableBody tr').length;
        if (parseInt(tableNoOfRows) == 0) {
            return false;
        }

        $("#mytableBody tr").each(function () {
            debugger
            //var tableNoOfRows = $('#mytableBody tr').length;
            //if (parseInt(tableNoOfRows) > 0) {
            //    var datesOptionLength = $('#InventoryMaintainanceAndCalibrationScheduleDatesID > option').length;
            //    if (parseInt(datesOptionLength) > 1) {

            //    }
            //}
        
            var value = $(this).find("td:eq(2)").text();
            //alert(value);
            var CheckStatus = $(this).find("td:eq(5)").text();
            //alert(CheckStatus);
            $('#InventoryMaintainanceAndCalibrationScheduleDatesID > option').each(function () {
                debugger;
                if ($(this).text() == value && CheckStatus != "In Progress") {
                    //alert($(this).text());
                    $(this).remove();
                }
            });

        });
    }
};