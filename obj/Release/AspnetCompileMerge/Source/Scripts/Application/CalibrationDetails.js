var CalibrationDetails = {
    files: null,
    Type: null,
    uploadedFileName: "",
    initialize: function () {
        CalibrationDetails.attachEvents();

        //$.getJSON("http://jsonip.com/?callback=?", function (data) {
        //    console.log(data);
        //    alert(data.ip);
        //});
    },
    attachEvents: function () {
        $("#buttonSaveInventoryCalibrationDetails4").on('click', function () {
            debugger;
          //  if ($("_CalibrationDetails").valid() == true) {
                var data = {
                    "InventoryBasicItemDetailsNumber": ""
                };
                data.InventoryBasicItemDetailsNumber = $("#CalibrationInventoryBasicItemDetailsID  option:selected").text();
               // ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/CheckMaintainance', "POST", data, CalibrationDetails.onSuccessFatchSerialNumberList);
                $.ajax({
                    type: 'Post',
                    url: "/Inventory/InventoryMaintainanceAndCalibration/CheckMaintainance",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        CalibrationDetails.onSuccessFatchSerialNumberList(data)
                    },
                    failure: function (data) {
                        CalibrationDetails.onError()
                    },
                });
           // }

            //if ($("#BasicDetailsID").val() > 0) {
            //    var dialog = BootstrapDialog.show({
            //        type: BootstrapDialog.TYPE_WARNING,
            //        title: 'Warning',
            //        message: "Are you sure, you wants to update? After update old data will get delete !",
            //        buttons: [
            //                    {
            //                        icon: 'glyphicon glyphicon-trash',
            //                        label: 'Delete',
            //                        cssClass: 'btn-warning',
            //                        action: function () {
            //                            dialog.close();
            //                            CalibrationDetails.addCalibrationDetails();

            //                        }
            //                    },
            //                    {
            //                        label: 'Close',
            //                        cssClass: 'btn-warning',
            //                        action: function (dialogRef) {

            //                            dialogRef.close();
            //                        }
            //                    }
            //        ]
            //    });
            //}
            //else {
            //    CalibrationDetails.addCalibrationDetails();
            //}

           
        });

        $('#txtCalibrationUploadFile').on('change', function (e) {
            debugger;
            CalibrationDetails.files = e.target.files;
        });

        $("#buttonFinishInventoryCalibrationDetails").on('click', function () {
            debugger;
            if ($("#InventoryTypeMasterID").val() == "1") {
                location.href = '/Inventory/Inventory/ConsumableList';
            }
            else if ($("#InventoryTypeMasterID").val() == "2") {
                location.href = '/Inventory/Inventory/NonConsumableList';
            }
        });

        $('#buttonUploadCalibrationFile').on('click', function (e) {
            debugger;
            $('#uploadedCalibrationImagesContainer').html("");
            CalibrationDetails.previewImages();
            var myID = $("#BasicDetailsID").val(); //uncomment this to make sure the ajax URL works
            if (CalibrationDetails.files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < CalibrationDetails.files.length; x++) {
                        var fileName = CalibrationDetails.files[x].name;
                        data.append(fileName, CalibrationDetails.files[x]);
                    }
                    debugger;
                    //InventoryBasicDetail.FileUploadData = data;
                    $.ajax({
                        type: "POST",
                        url: applicationSystem.get_hostname() + '/Inventory/UploadMaintainanceAndCalibrationFile?id=' + myID + "&&Type=Calibration",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            //alert(CalibrationDetails.uploadedFileName);
                            CalibrationDetails.uploadedFileName = CalibrationDetails.uploadedFileName + result;
                            console.log(CalibrationDetails.uploadedFileName);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                        }
                    });
                } else {
                    alert("This browser doesn't support HTML5 file uploads!");
                }
            }
        });
        
        $("#CalibrationInventoryBasicItemDetailsID").on("change", function () {
            CalibrationDetails.fatchMaintainanceDetailsByBasicItemDetailsID();
        });

    },
    onSuccessFatchSerialNumberList: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.dataCalibration.length > 0) {
                if (parseFloat(data.dataCalibration[0].InventoryBasicItemDetailsID) > 0) {
                    alert("Are you sure, you wants to ReNew Inventory?");
                    CalibrationDetails.addCalibrationDetails();
                }
            }
            else {
                CalibrationDetails.addCalibrationDetails();
            }
        }
        if (data.status == "fail") {
        }
    },
    previewImages: function () {

        var fileList = this.files;

        var anyWindow = window.URL || window.webkitURL;
        $('#uploadedCalibrationImagesContainer').html("");
        for (var i = 0; i < fileList.length; i++) {
            var $cloneRow = "";
            //get a blob to play with
            var objectUrl = anyWindow.createObjectURL(fileList[i]);
            // for the next line to work, you need something class="preview-area" in your html
            //$('#uploadedImagesContainer').append('<img src="' + objectUrl + '" />');
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a href="#" class="pop" onclick="CalibrationDetails.slideshowImage(event);"><img src="' + objectUrl + '" style="height:100px; margin-left: -12px;" class="margin"/></a></div></div>';
            $('#uploadedCalibrationImagesContainer').append($cloneRow);
            // get rid of the blob
            window.URL.revokeObjectURL(fileList[i]);
        }
        $("#uploadedCalibrationImagesContainerDiv").css("display", "none");
        var data = {
            "message": "Image Uploaded Sucessfully!"
        };
        //applicationSystem.onSuccess(data);
        //$("#uploadedImagesContainerDiv").css("display", "block");   
    },

    setCalibrationImages: function (data, path) {
        debugger;
        var SampleItems = [];
        SampleItems = data;
        var count = SampleItems.length;
        $('#uploadedCalibrationImagesContainer').html("");
        var $cloneRow = "";
        for (i = 0; i < SampleItems.length; ++i) {
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a class="close clickEventClass" id="deleteCalibrationImage_' + SampleItems[i].ID + '" onclick="CalibrationDetails.deleteImage(event);">&times;</a><a href="#" class="pop" id="calibrationImageAnkerID_' + SampleItems[i].ID + '" onclick="CalibrationDetails.slideshowImage(event);"><img src="' + path + SampleItems[i].FileName + '" alt="..." class="margin" id="calibrationImageID_' + SampleItems[i].ID + '" style="height:100px; margin-left: -12px;"></a></div></div>';
        }
        $('#uploadedCalibrationImagesContainer').html($cloneRow);
        $("#uploadedCalibrationImagesContainerDiv").css("display", "none");
    },

    deleteImage: function (e) {
        var _id = e.target.id;
        var splitID = _id.split('_');
        SampleAndParameterDetails.deleteSampleImage(splitID[1]);
    },

    slideshowImage: function (e) {
        debugger;
        var _id = e.target.id;
        var SplitID = _id.split("_");
        var src = $("#calibrationImageID_" + SplitID[1]).attr('src');
        $('.imagepreview').attr('src', src);
        //$('.imagepreview').attr('src', $("#" + _id).find('img').attr('src'));
        $('#imagemodal').modal('show');
    },

    addProposedDateRow: function () {
        debugger;
        counter = $('#myTableProposedDateBody tr:last').attr('id');
        if (typeof counter === "undefined") {
            $('#myTableProposedDateBody').html("");
            counter = 0;
        }
        else {
            counter = parseInt(counter) + 1;
        }
        var $cloneRow = "";
        $cloneRow = $cloneRow + '<tr id="' + counter + '" datatableID = "0" style="width:100%;">';
        $cloneRow = $cloneRow + '<td>' + (parseInt(counter) + 1) + '</td>';

        $cloneRow = $cloneRow + '<td><input type="text"id="ProposedDate_' + counter + '"   class="form-control OnlyDate" name="Mass_' + counter + '" style="width: 100%;" /></td>';
        $cloneRow = $cloneRow + '<td style="text-align: center;"><a href="#" title="Delete" id="rowDelete_' + counter + '" name="rowDelete_' + counter + '" style="width: 100%;"><span class="glyphicon glyphicon-remove-circle"></span></a></td>';
        $cloneRow = $cloneRow + '</tr>';

        $('#myTableProposedDateBody').append($cloneRow);

        $('.OnlyDate').datetimepicker({
            format: 'DD/MM/YYYY'
        });
        
    },
    addCalibrationDetails: function () {
        debugger;
        var data = CalibrationDetails.getCalibrationDetails();
       // ajaxRequest.makeRequest('/Inventory/InventoryMaintainanceAndCalibrationDetails', "POST", data, CalibrationDetails.onSuccess, CalibrationDetails.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/InventoryCalibrationDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                 onSuccess(data)
               
               
            },
            failure: function (data) {
                CalibrationDetails.onError()
            },
        });



    },
    fatchMaintainanceDetailsByBasicItemDetailsID: function () {
        debugger;
        var data = {
            "Type": "Calibration"
        };
        data.InventoryBasicItemDetailsID = $("#CalibrationInventoryBasicItemDetailsID").val();
      //  ajaxRequest.makeRequest('/Inventory/GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID', "POST", data, CalibrationDetails.onSuccessFatchMaintainanceDetailsByBasicItemDetailsID, CalibrationDetails.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                CalibrationDetails.onSuccessFatchMaintainanceDetailsByBasicItemDetailsID(data)
            },
            failure: function (data) {
                CalibrationDetails.onError()
            },
        });
    },
    getCalibrationDetails: function () {
        debugger;
        var data = {
            "ID": ""
        };
        data.ID = ($("#CalibrationID").val() == "") ? 0 : $("#CalibrationID").val();
        data.InventoryCommercialDetailsID = $('#InventoryCommercialDetailsID').val();
        data.InventoryBasicItemDetailsID = $("#CalibrationInventoryBasicItemDetailsID").val();
        data.AMCVendorName = $("#CalibrationAMCVendorName").val();
        data.AMCNumber = $("#CalibrationAMCNumber").val();
        data.AMCDate = new Date($("#CalibrationAMCDate").val());
        //data.AMCDate = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#CalibrationAMCDate").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
        data.AMCStartDate = new Date($("#CalibrationAMCStartDate").val());
        //data.AMCStartDate = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#CalibrationAMCStartDate").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
        data.AMCValue = $("#CalibrationAMCValue").val();
        data.AMCPeriod = $("#CalibrationAMCPeriod").val();
        data.Frequency = $("#CalibrationFrequency").val();
        data.Type = "Calibration";
        data.UploadedFileNames = CalibrationDetails.uploadedFileName + ",";
        return data;
    },
    onSuccess: function (data) {
        debugger;
        if (data.status == "success") {
            //applicationSystem.onSuccess(data);
            $("#CalibrationID").val("");
            $("#CalibrationInventoryBasicItemDetailsID").val(-1);
           // $("#CalibrationAMCVendorName").val("");
            $("#CalibrationAMCNumber").val("");
            $("#CalibrationAMCDate").val("");
            $("#CalibrationAMCStartDate").val("");
            $("#CalibrationAMCValue").val("");
            $("#CalibrationAMCPeriod").val("");
            $("#CalibrationFrequency").val(-1);
            //data.UploadedFileNames = null;
            //$("#txtCalibrationUploadFile").val("");
            //$('#uploadedCalibrationImagesContainer').html("");
            //$("#uploadedCalibrationImagesContainerDiv").css("display", "none");
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    onSuccessFatchMaintainanceDetailsByBasicItemDetailsID: function (data) {
        debugger;
        if (data.status == "success") {

            $("#CalibrationAMCVendorName").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCVendorName);
            $("#CalibrationAMCNumber").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCNumber);

            var newdate = data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCDate;
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
            ReceivedDate = day + "/" + month + "/" + year;
            $("#CalibrationAMCDate").val(ReceivedDate);
            $("#CalibrationAMCDateval").val(ReceivedDate);

            if (data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCStartDate != null) {
                var newdate1 = data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCStartDate;
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
                ReceivedDate1 = day1 + "/" + month1 + "/" + year1;
                $("#CalibrationAMCStartDate").val(ReceivedDate1);
                $("#CalibrationAMCStartDateval").val(ReceivedDate1);
            }

            if (data.InventoryMaintainanceAndCalibrationScheduleDetails[0].ListFiles != null) {
                var path = data.ImagePath;
                var url = window.location.href;
                var serverName = applicationSystem.get_hostname();
                var full_Url = serverName + path;
                CalibrationDetails.setCalibrationImages(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].ListFiles, full_Url);
            }

            //$("#AMCValue").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCValue);
            $("#CalibrationAMCPeriod").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCPeriod);
            $("#CalibrationFrequency").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].Frequency).change();

        }
        else if (data.status == "info") {
            //$("#MaintainanceID").val("");
            //$("#InventoryCommercialDetailsID").val("");
            //$("#InventoryBasicItemDetailsID").val(-1).change();
            //$("#AMCVendorName").val("");
            //$("#AMCNumber").val("");
            //$("#AMCVendorName").val("");
            //$("#AMCDate").val("");
            //$("#AMCStartDate").val("");
            ////$("#AMCValue").val("");
            //$("#AMCPeriod").val("");
            //$("#Frequency").val(-1).change();

           // $('#CalibrationAMCVendorName').val("");
            $('#CalibrationAMCNumber').val("");
            $('#CalibrationAMCDate').val("");
            $('#CalibrationAMCPeriod').val("");
            $('#CalibrationFrequency').val(-1);
            $('#CalibrationAMCStartDate').val("");

            data.UploadedFileNames = null;
            $('#uploadedMaintenanceImagesContainer').html("");
            $('#uploadedMaintenanceImagesContainerDiv').css("display", "none");
        }
        else if (data.status == "fail") {
            //applicationSystem.onError(data);
        }
    },

    onError: function (data) {
        applicationSystem.onError(data);
    },
};