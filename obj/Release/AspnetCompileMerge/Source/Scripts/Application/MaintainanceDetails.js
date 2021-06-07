var MaintainanceDetails = {
    files: null,
    Type: null,
    uploadedFileName: "",
    initialize: function () {
        MaintainanceDetails.attachEvents();
    },
    attachEvents: function () {
        $("#buttonSaveInventoryMaintainanceDetails3").on('click', function () {
            debugger;
           // if ($("#_Maintainance").valid() == true) {
                var data = {
                    "InventoryBasicItemDetailsNumber": "",
                    "InventoryBasicItemDetailsID": ""
                };
                data.InventoryBasicItemDetailsNumber = $("#InventoryBasicItemDetailsID  option:selected").text();
                data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID option:selected").val();
               // ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/CheckMaintainance', "POST", data, MaintainanceDetails.onSuccessFatchSerialNumberList);

                $.ajax({
                    type: 'Post',
                    url: "/Inventory/InventoryMaintainanceAndCalibration/CheckMaintainance",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        MaintainanceDetails.onSuccessFatchSerialNumberList(data)
                    },
                    failure: function (data) {
                        MaintainanceDetails.onError()
                    },
                });
           // }
        });
        $("#buttonNextToCalibration").on('click', function () {
            debugger;
            $("#InventoryBasicItemDetailsID").val(-1);
            $("#AMCNumber").val("");           
            $("#AMCDate").val("");
            $("#AMCStartDate").val("");
            $("#AMCValue").val("");
            $("#AMCPeriod").val("");
            $("#AMCDateval").val("");
            $("#AMCStartDateval").val("");
            $("#Frequency").val(-1);
            $("#tabHead_Calibration").attr("data-toggle", "tab");
            $("#tabHead_Calibration").trigger("click");
           // $("#CommercialDetailsID").val(data.ID);
        });
        $('#txtMaintainanceUploadFile').on('change', function (e) {
            debugger;
            MaintainanceDetails.files = e.target.files;
        });

        $('#buttonUploadMaintainanceFile').on('click', function (e) {
            debugger;
            $('#uploadedMaintenanceImagesContainer').html("");
            MaintainanceDetails.previewImages();
            var myID = $("#BasicDetailsID").val(); //uncomment this to make sure the ajax URL works
            if (MaintainanceDetails.files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < MaintainanceDetails.files.length; x++) {
                        var fileName = MaintainanceDetails.files[x].name;
                        data.append(fileName, MaintainanceDetails.files[x]);
                    }
                    debugger;
                    //InventoryBasicDetail.FileUploadData = data;
                    $.ajax({
                        type: "POST",
                        url: applicationSystem.get_hostname() + '/Inventory/UploadMaintainanceAndCalibrationFile?id=' + myID + "&&Type=Maintainance",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            //alert(MaintainanceDetails.uploadedFileName);
                            MaintainanceDetails.uploadedFileName = MaintainanceDetails.uploadedFileName + result;
                            console.log(MaintainanceDetails.uploadedFileName);
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

        $("#InventoryBasicItemDetailsID").on("change", function () {
            debugger;
            if (parseInt($("#InventoryBasicItemDetailsID").val()) > 0) {
                MaintainanceDetails.fatchMaintainanceDetailsByBasicItemDetailsID();
            }
            else {
               
             //   $('#AMCVendorName').val("");
                $('#AMCNumber').val("");
                $('#AMCDate').val("");
                $('#AMCPeriod').val("");
                //$('#Frequency').val(-1);
                $('#AMCStartDate').val("");
                $('#AMCDateval').val(""); 
                $('#AMCStartDateval').val("");
            }
        });



    },
    onSuccessFatchSerialNumberList: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.data.length > 0) {
                if (parseFloat(data.data[0].InventoryBasicItemDetailsID) > 0) {
                    alert("Are you sure, you wants to ReNew Inventory?");
                    MaintainanceDetails.addMaintainanceDetails();
                }
            }
            else {
                MaintainanceDetails.addMaintainanceDetails();
            }
            
        }
        if (data.status == "fail") {
           
           
        }

    },
    previewImages: function () {

        var fileList = this.files;

        var anyWindow = window.URL || window.webkitURL;
        $('#uploadedMaintenanceImagesContainer').html("");
        for (var i = 0; i < fileList.length; i++) {
            var $cloneRow = "";
            //get a blob to play with
            var objectUrl = anyWindow.createObjectURL(fileList[i]);
            // for the next line to work, you need something class="preview-area" in your html
            //$('#uploadedImagesContainer').append('<img src="' + objectUrl + '" />');
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a href="#" class="pop" onclick="SampleAndParameterDetails.slideshowImage(event);"><img src="' + objectUrl + '" style="height:100px; margin-left: -12px;" class="margin"/></a></div></div>';
            $('#uploadedMaintenanceImagesContainer').append($cloneRow);
            // get rid of the blob
            window.URL.revokeObjectURL(fileList[i]);
        }
        $("#uploadedMaintenanceImagesContainerDiv").css("display", "none");
        var data = {
            "message": "Image Uploaded Sucessfully!"
        };
        //applicationSystem.onSuccess(data);
        //$("#uploadedImagesContainerDiv").css("display", "block");   
    },

    setMaintenanceImages: function (data, path) {
        debugger;
        var SampleItems = [];
        SampleItems = data;
        var count = SampleItems.length;
        $('#uploadedMaintenanceImagesContainer').html("");
        var $cloneRow = "";
        for (i = 0; i < SampleItems.length; ++i) {
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a class="close clickEventClass" id="deleteMaintenanceImage_' + SampleItems[i].ID + '" onclick="MaintainanceDetails.deleteImage(event);">&times;</a><a href="#" class="pop" id="maintenanceImageAnkerID_' + SampleItems[i].ID + '" onclick="MaintainanceDetails.slideshowImage(event);"><img src="' + path + SampleItems[i].FileName + '" alt="..." class="margin" id="maintenanceImageID_' + SampleItems[i].ID + '" style="height:100px; margin-left: -12px;"></a></div></div>';
        }
        $('#uploadedMaintenanceImagesContainer').html($cloneRow);
        $("#uploadedMaintenanceImagesContainerDiv").css("display", "none");
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
        var src = $("#maintenanceImageID_" + SplitID[1]).attr('src');
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
    addMaintainanceDetails: function () {
        debugger;
        var data = MaintainanceDetails.getMaintainanceDetails();
      //  ajaxRequest.makeRequest('/Inventory/InventoryMaintainanceAndCalibrationDetails', "POST", data, MaintainanceDetails.onSuccess, MaintainanceDetails.onError);
       
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/InventoryMaintainanceAndCalibrationDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                 onSuccess(data)
              //  MaintainceSuccess(result)
            },
            failure: function (data) {
                onError()
            },
        });
    },
    fatchMaintainanceDetailsByBasicItemDetailsID: function () {
        debugger;
        var data = {
            "Type": "Maintainance"
        };
        data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID").val();
       // ajaxRequest.makeRequest('/Inventory/GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID', "POST", data, MaintainanceDetails.onSuccessFatchMaintainanceDetailsByBasicItemDetailsID, MaintainanceDetails.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                MaintainanceDetails.onSuccessFatchMaintainanceDetailsByBasicItemDetailsID(data)
            },
            failure: function (data) {
                MaintainanceDetails.onError()
            },
        });
    },
    getMaintainanceDetails: function () {
        debugger;
        var data = {
            "ID": ""
        };
        //data.ID = $("#MaintainanceID").val();
        data.ID = ($("#MaintainanceID").val() == "") ? 0 : $("#MaintainanceID").val();
        data.InventoryCommercialDetailsID = $('#InventoryCommercialDetailsID').val();
        data.InventoryBasicItemDetailsID = $("#InventoryBasicItemDetailsID").val();
        data.AMCVendorName = $("#AMCVendorName").val();
        data.AMCNumber = $("#AMCNumber").val();
        data.AMCDate = new Date($("#AMCDate").val());
        //data.AMCDate = new Date(
        //                                     applicationSystem.convertionOfDateFormat(
        //                                     $("#AMCDate").val(),
        //                                     applicationSystem.aspectedFormat,
        //                                     applicationSystem.currentServerFormat,
        //                                     applicationSystem.aspectedServerSeparator,
        //                                     applicationSystem.currentServerSeparator
        //                                 ));
        data.AMCStartDate = new Date($("#AMCStartDate").val());
        //data.AMCStartDate = new Date(
        //                                   applicationSystem.convertionOfDateFormat(
        //                                   $("#AMCStartDate").val(),
        //                                   applicationSystem.aspectedFormat,
        //                                   applicationSystem.currentServerFormat,
        //                                   applicationSystem.aspectedServerSeparator,
        //                                   applicationSystem.currentServerSeparator
        //                               ));
        data.AMCValue = $("#AMCValue").val();
        data.AMCPeriod = $("#AMCPeriod").val();
        data.Frequency = $("#Frequency").val();
        data.Type = "Maintainance";
        data.UploadedFileNames = MaintainanceDetails.uploadedFileName + ",";
        return data;
    },
    onSuccess: function (data) {
        debugger;
        if (data.status == "success") {
           // applicationSystem.onSuccess(data);
            $("#MaintainanceID").val("");
            $("#InventoryCommercialDetailsID").val("");
            $("#InventoryBasicItemDetailsID").val(-1).change();
            $("#AMCVendorName").val("");
            $("#AMCNumber").val("");
            $("#AMCVendorName").val("");
            $("#AMCDate").val("");
            $("#AMCStartDate").val("");
            $("#AMCValue").val(""); 
            $("#AMCPeriod").val("");
            $("#AMCDateval").val("");
            $("#AMCStartDateval").val("");
            $("#Frequency").val(-1).change();
            data.UploadedFileNames = null;
            $("#txtMaintainanceUploadFile").val("");
            $('#uploadedMaintenanceImagesContainer').html("");
            $("#uploadedMaintenanceImagesContainerDiv").css("display", "none");
            //var url = applicationSystem.get_hostname() + "/Inventory/List";
            //window.location.href = url;
        }
        if (data.status == "fail") {
            //applicationSystem.onError(data);
           
        }
    },

    onSuccessFatchMaintainanceDetailsByBasicItemDetailsID: function (data) {
        debugger;
        if (data.status == "success") {

            if (data != null) {

                $("#AMCVendorName").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCVendorName);
                $("#AMCNumber").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCNumber);

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
                $("#AMCDate").val(ReceivedDate);
                $("#AMCDateval").val(ReceivedDate);

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
                    ReceivedDate1 = day1+ "/" + month1 + "/" + year1;
                    $("#AMCStartDate").val(ReceivedDate1);
                    $("#AMCStartDateval").val(ReceivedDate1);
                }

                if (data.InventoryMaintainanceAndCalibrationScheduleDetails[0].ListFiles != null) {
                    var path = data.ImagePath;
                    var url = window.location.href;
                    var serverName = applicationSystem.get_hostname();
                    var full_Url = serverName + path;
                    MaintainanceDetails.setMaintenanceImages(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].ListFiles, full_Url);
                }

                $("#AMCValue").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCValue);
                $("#AMCPeriod").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].AMCPeriod);
                $("#Frequency").val(data.InventoryMaintainanceAndCalibrationScheduleDetails[0].Frequency).change();
            }
            else {

            }
        }
        else if (data.status == "info") {
            //applicationSystem.onError(data);
           // $('#AMCVendorName').val("");
            $('#AMCNumber').val("");
            $('#AMCDate').val("");
            $('#AMCPeriod').val("");
            $('#Frequency').val(-1).change();
            $('#AMCStartDate').val("");
            $("#AMCDateval").val("");
            $("#AMCStartDateval").val("");
        }
        else if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    onError: function (data) {
        applicationSystem.onError(data);
    },
};