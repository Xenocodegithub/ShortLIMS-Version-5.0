var InventoryCommercialDetails = {
    files: null,
    initialize: function () {
        InventoryCommercialDetails.attachEvents();
    },
    attachEvents: function () {

        $("#buttonSaveInventoryCommercialDetails2").on('click', function () {
            debugger;
           
                InventoryCommercialDetails.addInventoryCommercialDetails();
                return false;
          
        });

        $("#buttonNextToMaintainanceAndCalibration").on('click', function () {
            $("#tabHead_Maintainance").attr("data-toggle", "tab");
            $("#tabHead_Maintainance").trigger("click");
            //$("#CommercialDetailsID").val(data.ID);
            //$("#add_new_item_li").css("display", "none");
            //  InventoryBasicDetail.fatchInventoryBasicItemDetailByCommercialID();
            InventoryCommercialDetails.fatchInventoryBasicItemDetailByCommercialID();
        });

        $('#txtUploadFile').on('change', function (e) {
          
            InventoryCommercialDetails.files = e.target.files;
        });

        $('#buttonUploadCommercialFile').on('click', function (e) {
           
            $('#uploadedCommercialImagesContainer').html("");
            InventoryCommercialDetails.previewImages();
            var myID = $("#BasicDetailsID").val(); //uncomment this to make sure the ajax URL works
            if (InventoryCommercialDetails.files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < InventoryCommercialDetails.files.length; x++) {
                        var fileName = InventoryCommercialDetails.files[x].name;
                        data.append(fileName, InventoryCommercialDetails.files[x]);
                    }
                  
                    InventoryBasicDetail.FileUploadData = data;
                    $.ajax({
                        type: "POST",
                        url: applicationSystem.get_hostname() + '/Inventory/UploadCommercialFile?id=' + myID,
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                           
                            console.log(result.imageName);
                            console.log(result.imagePath);

                            console.log(result);
                            var _data = {
                                "message": result
                            };
                            applicationSystem.onSuccess(_data);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(status);
                            console.log(xhr);
                            console.log(err);
                        }
                    });
                } else {
                    alert("This browser doesn't support HTML5 file uploads!");
                }
            }
        });
    },
    fatchInventoryBasicItemDetailByCommercialID: function () {
        debugger
        var data = {
            "ID": "0"
        };
        data.ID = $("#BasicDetailsID").val();
       // ajaxRequest.makeRequest('/Inventory/FatchBasicItemsDetails', "POST", data, InventoryBasicDetail.onSuccessFatchInventoryBasicItemDetailByCommercialID, InventoryBasicDetail.onError);

        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/FatchBasicItemsDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryCommercialDetails.onSuccessFatchInventoryBasicItemDetailByCommercialID(data)
            },
            failure: function (data) {
                InventoryCommercialDetails.onError()
            },
        });
    },
    onSuccessFatchInventoryBasicItemDetailByCommercialID: function (data) {
        debugger
        if (data.status == "success") {
            InventoryCommercialDetails.bindBasicItemsOptionList(data.BasicItemsDetails);
        }
        if (data.status == "fail") {
            alert('Error');
        }
    },
    bindBasicItemsOptionList: function (data) {
        if (data != null) {
            var ItemsList = [];
            ItemsList = data;
            var count = ItemsList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Field Type...</option>";
            for (i = 0; i < ItemsList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + ItemsList[i].ID + "' title = '" + ItemsList[i].ID + "'>" + ItemsList[i].InventoryBasicItemDetailsNumber + "</option>";
            }
            $("#InventoryBasicItemDetailsID").html($cloneRow);
            $("#CalibrationInventoryBasicItemDetailsID").html($cloneRow);


            $("#tabHead_Maintainance").attr("data-toggle", "tab");
            $("#tabHead_Maintainance").trigger("click");

            //InventoryBasicDetail.BasicItemsOptionList = $cloneRow;
        }
    },

    previewImages: function () {

        var fileList = this.files;

        var anyWindow = window.URL || window.webkitURL;
        $('#uploadedCommercialImagesContainer').html("");
        for (var i = 0; i < fileList.length; i++) {
            var $cloneRow = "";
            //get a blob to play with
            var objectUrl = anyWindow.createObjectURL(fileList[i]);
            // for the next line to work, you need something class="preview-area" in your html
            //$('#uploadedImagesContainer').append('<img src="' + objectUrl + '" />');
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a href="#" class="pop" onclick="InventoryCommercialDetails.slideshowImage(event);"><img src="' + objectUrl + '" style="height:100px; margin-left: -12px;" class="margin"/></a></div></div>';
            $('#uploadedCommercialImagesContainer').append($cloneRow);
            // get rid of the blob
            window.URL.revokeObjectURL(fileList[i]);
        }
        var data = {
            "message": "Image Uploaded Sucessfully!"
        };
        applicationSystem.onSuccess(data);
        //$("#uploadedImagesContainerDiv").css("display", "block");   
    },

    //SET COMMERCIAL IMAGES OR FILES THAT UPLOADED. THIS FUNCTION RUN AT THE EDIT TIME OF THE CONTENT.
    setCommercialImages: function (data, path) {
        debugger;
        var SampleItems = [];
        SampleItems = data;
        var count = SampleItems.length;
        $('#uploadedCommercialImagesContainer').html("");
        var $cloneRow = "";
        for (i = 0; i < SampleItems.length; ++i) {
            $cloneRow = $cloneRow + '<div class="col-md-2" style="border-style:groove;"><div class="img-wrap"><a class="close clickEventClass" id="deletecommercialImage_' + SampleItems[i].ID + '" onclick="InventoryCommercialDetails.deleteImage(event);">&times;</a><a href="#" class="pop" id="commercialImageAnkerID_' + SampleItems[i].ID + '" onclick="InventoryCommercialDetails.slideshowImage(event);"><img src="' + path + SampleItems[i].FileName + '" alt="..." class="margin" id="commercialImageID_' + SampleItems[i].ID + '" style="height:100px; margin-left: -12px;"></a></div></div>';
        }
        $('#uploadedCommercialImagesContainer').html($cloneRow);
        $("#uploadedCommercialImagesContainerDiv").css("display", "none");
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
        var src = $("#commercialImageID_" + SplitID[1]).attr('src');
        $('.imagepreview').attr('src', src);
        //$('.imagepreview').attr('src', $("#" + _id).find('img').attr('src'));
        $('#imagemodal').modal('show');
    },

    addInventoryCommercialDetails: function () {
        debugger;
        var data = InventoryCommercialDetails.getInventoryCommercialDetails();
       // ajaxRequest.makeRequest('/Inventory/Inventory/InventoryCommercialDetails', "POST", data, InventoryCommercialDetails.onSuccess, InventoryCommercialDetails.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/InventoryCommercialDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryCommercialDetails.onSuccess(data)
            },
            failure: function (data) {
                InventoryCommercialDetails.onError()
            },
        });
    },

    fatchInventoryCommercialDetails: function () {
        debugger;
        var data = {
            "ID": ""
        };

        data.ID = $("#BasicDetailsID").val();
      //  ajaxRequest.makeRequest('/Inventory/Inventory/FatchCommercialDetailsByID', "POST", data, InventoryCommercialDetails.onSuccessFatchCommercialDetailsByID, InventoryCommercialDetails.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/FatchCommercialDetailsByID",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryCommercialDetails.onSuccessFatchCommercialDetailsByID(data)
            },
            failure: function (data) {
                alert("Failed");
            },
        });

    },

    getInventoryCommercialDetails: function () {
        debugger;
        var data = {
            "ID": ""
        };       
        if ($("#CommercialDetailsID").val() != "") {
            
            data.ID = $("#CommercialDetailsID").val();
        }
        else {
            data.ID = 0
        }
       
        data.InventoryBasicDetailsID = $("#BasicDetailsID").val();
        data.VendorName = $("#VendorName").val();
        data.PurchaseOrderNumber = $('#PurchaseOrderNumber').val();
        data.PurchaseOrderDate = $("#PurchaseOrderDate").val();

      
        //if ($('#PurchaseOrderDate').val() != "") {

        //    data.PurchaseOrderDate = new Date($('#PurchaseOrderDate').val());
        //}
        //else {

        //    var from = $("#PurchaseOrderDateval").val().split("/")
        //    var f = new Date(from[2], from[1] - 1, from[0])
        //    data.PurchaseOrderDate = f;
        //}
        //data.PurchaseOrderDate = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#PurchaseOrderDate").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
        data.PurchaseOrderValue = $("#PurchaseOrderValue").val();

        //data.PurchaseDate = new Date($("#PurchaseDate").val());
        if ($('#PurchaseDate').val() != "") {

            data.PurchaseDate = new Date($('#PurchaseDate').val());
        }
        else {

            var from = $("#PurchaseDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.PurchaseDate = f;
        }
        //data.PurchaseDate = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#PurchaseDate").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
        data.InvoiceNumber = $("#InvoiceNumber").val();
        data.DeliveryChallanNo = $('#DeliveryChallanNo').val();
        data.DeliveryTime = $('#DeliveryTime').val();

        //data.DeliveryChallanDate = new Date($("#DeliveryChallanDate").val());
        if ($('#DeliveryChallanDate').val() != "") {

            data.DeliveryChallanDate = new Date($('#DeliveryChallanDate').val());
        }
        else {
            var from = $("#DeliveryChallanDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.DeliveryChallanDate = f;
        }

       //data.BillDate = new Date($("#BillDate").val());
        if ($('#BillDate').val() != "") {

            data.BillDate = new Date($('#BillDate').val());
        }
        else {

            var from = $("#BillDateval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.BillDate = f;
        }
        return data;
    },
    setInventoryCommercialDetails: function () {

    },
    setInventoryBasicDetails: function (data) {
        debugger;
        if (data != null) {
            $("#ItemID").val(data.ItemID).change();
            $("#TypeID").val(data.TypeID);
            $("#CategoryID").val(data.CategoryID);
            $("#UnitID").val(data.UnitID);
            $("#QuantityType").val(data.QuantityType);
            if ($("#QuantityType").val() == "Quantity" || $("#QuantityType").val() == "Batch Quantity") {
                $("#QuantityLeft").removeAttr("disabled");
            }
            else {
                $("#QuantityLeft").attr("disabled", "disabled");
            }

            $("#QuantityLeft").val(data.Quantity);
            $("#TQuantity").val(data.TQuantity);
            $("#Warranty").val(data.Warranty);
            $("#BasicDetailsID").val(data.ID);
            $("#TotalQuantity").val(data.TotalQuantity);
            $("#InventoryBasicDetailsNumber").val(data.InventoryBasicDetailsNumber);
            $('#Manufacturer').val(data.Manufacturer);
            $('#GradeReceived').val(data.GradeReceived);
            $('#ConditionOfPackaging').val(data.ConditionOfPackaging);
            $('#IntegrityOfPackaging').val(data.IntegrityOfPackaging);
            $('#CertifiedConcentration').val(data.CertifiedConcentration);
            $('#Praceability').val(data.Praceability);
            $('#PurchaseRequestID').val(data.PurchaseRequestID);

            $('#Remark').val(data.Remark);
            $('#BrandReceived').val(data.BrandReceived);


            //$('#DOM').val(applicationSystem.convertJsonDateFormat(data.DOM, "dd/MM/yyyy"));
             if (data.DOM != null) {
                var newdate = data.DOM;
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
                $("#DOM").val(ReceivedDate);
                $("#DOMval").val(ReceivedDate);
            }
            if (data.DOE != null) {
                var newdate = data.DOE;
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
                $("#DOE").val(ReceivedDate);
                $("#DOEval").val(ReceivedDate);
            }
        }
    },
    setInventoryBasicItemsDetails: function (data) {
        debugger;
        if (data != null) {
            var BasicItems = [];
            BasicItems = data;
            var count = BasicItems.length;
            $('#myItemTableBody').html("");

            var Type = $("#QuantityType").val();
            for (i = 0; i < BasicItems.length; ++i) {
                var $cloneRow = "";
                $cloneRow = $cloneRow + '<tr id="' + i + '" datatableID = "' + BasicItems[i].ID + '" style="width:100%;"  isdeleted="0">';
                $cloneRow = $cloneRow + '<td>' + (parseInt(i) + 1) + '</td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="BatchNumber_' + i + '"   class="form-control" name="BatchNumber_' + i + '" style="width: 100%;" value="' + BasicItems[i].BatchNumber + '" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="LOTNo_' + i + '"   class="form-control" name="LOTNumber_' + i + '" style="width: 100%;" value="' + BasicItems[i].LOTNo + '" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="ModelNumber_' + i + '"   class="form-control" name="ModelNumber_' + i + '" style="width: 100%;" value="' + BasicItems[i].ModelNumber + '" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="SRNO_' + i + '"   class="form-control" name="SRNumber_' + i + '" style="width: 100%;" value="' + BasicItems[i].SRNO + '" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="BarcodeNumber_' + i + '"   class="form-control" name="BarcodeNumber_' + i + '" style="width: 100%;" value="' + BasicItems[i].BarcodeNumber + '" /></td>';

                if (Type == "Quantity") {
                    $cloneRow = $cloneRow + '<td></td>';
                }
                else if (Type == "Batch Quantity") {
                    $cloneRow = $cloneRow + '<td><input type="text"id="Quantity_' + i + '"   class="form-control allowNumbersWithDecimalOnly" name="Quantity_' + i + '" style="width: 100%;" value="' + BasicItems[i].Quantity + '" /></td>';
                }

                $cloneRow = $cloneRow + '<td><input type="text"id="Description_' + i + '"   class="form-control"  name="Description_' + i + '" style="width: 100%;" value="' + BasicItems[i].Description + '" /></td>';
                //$cloneRow = $cloneRow + '<td><a href="#" title="Delete" class="glyphicon glyphicon-trash" data-toggle="modal" data-target="#myModal" id="rowDelete_' + i + '" name="rowDelete_' + i + '" operation="Delete Item"> </a>';
                //$cloneRow = $cloneRow + '<td></td>';
                $cloneRow = $cloneRow + '</tr>';
                $('#myItemTableBody').append($cloneRow);


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
            $("#Quantity").val(BasicItems.length);
            if ($("#QuantityType").val() == "Quantity") {
                $("#TotalQuantity").val($("#Quantity").val());
            }
        }
    },
    setInventoryBasicItemsDetailsOptionList: function (data) {

        if (data != null) {
            var BasicItems = [];
            BasicItems = data;
            var count = BasicItems.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Item...</option>";
            for (i = 0; i < BasicItems.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + BasicItems[i].ID + "' title = '" + BasicItems[i].ID + "'>" + BasicItems[i].InventoryBasicItemDetailsNumber + "</option>";
            }
            $("#InventoryBasicItemDetailsID").html($cloneRow);
            $("#CalibrationInventoryBasicItemDetailsID").html($cloneRow);
        }
    },
    onSuccess: function (data) {
        debugger;
        $("#CommercialDetailsID").val(data.ID);
        $("#tabHead_Maintainance").attr("data-toggle", "tab");
        $("#tabHead_Maintainance").trigger("click");
        $("#add_new_item_li").css("display", "none");
        

        if ($("#InventoryTypeMasterID").val() == "1") {
            location.href = '/Inventory/Inventory/ConsumableList';
        }
        else
            if ($("#InventoryTypeMasterID").val() == "2") {
                debugger;
                InventoryCommercialDetails.fatchInventoryBasicItemDetailByCommercialID();
        }

    },

    onSuccessFatchCommercialDetailsByID: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.CommercialDetails.length > 0) {
                $("#VendorName").val(data.CommercialDetails[0].VendorName);
                $("#PurchaseOrderNumber").val(data.CommercialDetails[0].PurchaseOrderNumber);
                $("#CommercialDetailsID").val(data.CommercialDetails[0].ID);
                if (data.CommercialDetails[0].PurchaseOrderDate != null) {
                    var newdate = data.CommercialDetails[0].PurchaseOrderDate;
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
                    $("#PurchaseOrderDate").val(ReceivedDate);
                    $("#PurchaseOrderDateval").val(ReceivedDate);
                }
              
                $("#PurchaseOrderValue").val(data.CommercialDetails[0].PurchaseOrderValue);

                $("#DeliveryChallanNo").val(data.CommercialDetails[0].DeliveryChallanNo);
                $("#DeliveryTime").val(data.CommercialDetails[0].DeliveryTime);
               
                if (data.CommercialDetails[0].PurchaseDate != null) {
                    var tempdate = data.CommercialDetails[0].PurchaseDate;
                    var tempString = tempdate.toString().substr(6);
                    var tempcurrentTime1 = new Date(parseInt(tempString));
                    var tempmonth1 = tempcurrentTime1.getMonth() + 1;
                    var tempday1 = tempcurrentTime1.getDate();
                    var tempyear1 = tempcurrentTime1.getFullYear();
                    if (parseInt(tempmonth1) < 10) {
                        tempmonth1 = "0" + tempmonth1;
                    }
                    if (parseInt(tempday1) < 10) {
                        tempday1 = "0" + tempday1;
                    }
                    var RtempeceivedDate1 = tempday1 + "/" + tempmonth1 + "/" + tempyear1;
                    $("#PurchaseDate").val(RtempeceivedDate1);
                    $("#PurchaseDateval").val(RtempeceivedDate1);
                }
                if (data.CommercialDetails[0].DeliveryChallanDate != null) {
                    var tempdate = data.CommercialDetails[0].DeliveryChallanDate;
                    var tempString = tempdate.toString().substr(6);
                    var tempcurrentTime1 = new Date(parseInt(tempString));
                    var tempmonth1 = tempcurrentTime1.getMonth() + 1;
                    var tempday1 = tempcurrentTime1.getDate();
                    var tempyear1 = tempcurrentTime1.getFullYear();
                    if (parseInt(tempmonth1) < 10) {
                        tempmonth1 = "0" + tempmonth1;
                    }
                    if (parseInt(tempday1) < 10) {
                        tempday1 = "0" + tempday1;
                    }
                    var RtempeceivedDate1 = tempday1 + "/" + tempmonth1 + "/" + tempyear1;
                    $("#DeliveryChallanDate").val(RtempeceivedDate1);
                    $("#DeliveryChallanDateval").val(RtempeceivedDate1);
                }
                if (data.CommercialDetails[0].BillDate != null) {
                    var tempdate = data.CommercialDetails[0].BillDate;
                    var tempString = tempdate.toString().substr(6);
                    var tempcurrentTime1 = new Date(parseInt(tempString));
                    var tempmonth1 = tempcurrentTime1.getMonth() + 1;
                    var tempday1 = tempcurrentTime1.getDate();
                    var tempyear1 = tempcurrentTime1.getFullYear();
                    if (parseInt(tempmonth1) < 10) {
                        tempmonth1 = "0" + tempmonth1;
                    }
                    if (parseInt(tempday1) < 10) {
                        tempday1 = "0" + tempday1;
                    }
                    var RtempeceivedDate1 = tempday1 + "/" + tempmonth1 + "/" + tempyear1;
                    $("#BillDate").val(RtempeceivedDate1);
                    $("#BillDateval").val(RtempeceivedDate1);
                }
                $("#InvoiceNumber").val(data.CommercialDetails[0].InvoiceNumber);
                //InventoryBasicDetail.setInventoryBasicDetails(data.InventoryBasicDetail[0]);
                //InventoryBasicDetail.setInventoryBasicItemsDetails(data.InventoryBasicItemDetail);
                //InventoryBasicDetail.setInventoryBasicItemsDetailsOptionList(data.InventoryBasicItemDetail);

            }
            if (data.InventoryCommercialFileDetail.length > 0) {
                if (data.InventoryCommercialFileDetail[0].FileName != null) {
                    var path = data.ImagePath;
                    var url = window.location.href;
                    var serverName = applicationSystem.get_hostname();
                    var full_Url = serverName + path;
                    InventoryCommercialDetails.setCommercialImages(data.InventoryCommercialFileDetail, full_Url);
                }
            }

            if (data.InventoryBasicDetail.length > 0) {
                InventoryCommercialDetails.setInventoryBasicDetails(data.InventoryBasicDetail[0]);
            }
            if (data.InventoryBasicItemDetail.length > 0) {
                InventoryCommercialDetails.setInventoryBasicItemsDetails(data.InventoryBasicItemDetail);
                InventoryCommercialDetails.setInventoryBasicItemsDetailsOptionList(data.InventoryBasicItemDetail);
            }
        }
        else if (data.status == "info") {
            applicationSystem.onError(data);
        }
        else if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
};