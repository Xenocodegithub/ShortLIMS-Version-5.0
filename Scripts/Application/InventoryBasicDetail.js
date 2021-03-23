var InventoryBasicDetail = {
    FileUploadData: null,
    initialize: function () {
        InventoryBasicDetail.attachEvents();
        // $.removeCookie("previousURL");
    },
    attachEvents: function () {

        //$("#add_new_item").on('click', function () {
        //    //$.cookie("visits", 10);
        //    //console.debug($.cookie("visits"));
        //   
        //    var ID = $("#CommercialDetailsID").val();
        //    var previousURL = applicationSystem.get_hostname() + "/Inventory/Index/" + ID;

        //    //$.cookie("previousURL", previousURL);

        //    var url = applicationSystem.get_hostname() + "/Inventory/SetCookies?previousURL=" + previousURL;
        //    window.location.href = url;

        //});

        $("#buttonSaveInventoryBasicDetails").on('click', function () {
            debugger;
            //if ($("#_BasicDetails").valid() == true) {
            debugger;

            InventoryBasicDetail.addInventoryBasicDetail();
            return false;
            //}
            debugger;
            //if ($("#_BasicDetails").valid() == true) {
                debugger;

                InventoryBasicDetail.addInventoryBasicDetail();
                return false;
            //}
        });
        $("#TypeID").on("change", function () {

            //InventoryBasicDetail.fatchCategoryList();
        });

        $("#CategoryID").on("change", function () {

            //InventoryBasicDetail.fatchItemList();
        });

        $("#ItemID").change(function () {
            if ($("#ItemID").val() > 0) {
                InventoryBasicDetail.fatchItemDetails();
            }
        });
        $("#QuantityType").on('change', function () {
            if ($("#QuantityType").val() == "Quantity") {
                if ($("#myItemTableBody tr").length > 0) {
                    $("#myItemTableBody").html("");
                    $("#Quantity").val("");
                }
                $("#Quantity").removeAttr("disabled");
            }
            else if ($("#QuantityType").val() == "Batch Quantity") {
                if ($("#myItemTableBody tr").length > 0) {
                    $("#myItemTableBody").html("");
                    $("#Quantity").val("");
                }
                $("#Quantity").removeAttr("disabled");
            }
            else {
                $("#Quantity").attr("disabled", "disabled");
            }
        });

        $("#Quantity").change(function () {
            debugger
            var totalRows = $("#Quantity").val();
            var Pre1 = $("#QuantityLeft").val();
            var _tableLastRowId = $('#myItemTableBody tr:last').attr('id');
            if (typeof _tableLastRowId === "undefined") {
                $('#myItemTableBody').html("");
                _tableLastRowId = 0;
            }
            if (totalRows >= Pre1) {
                if (parseInt(totalRows) > parseInt(_tableLastRowId)) {
                    InventoryBasicDetail.createRowsInventoryBasicIntemDetails();
                }
                else {

                    var _countRow = parseInt(_tableLastRowId) - parseInt(totalRows);
                    for (var counter = 0; counter <= parseInt(_countRow); ++counter) {
                        $('#myItemTableBody tr:last').remove();
                    }
                }
            }
            else {

                alert('Received Quantity Must be greater than Pre-Received Quantity');
                $("#Quantity").val(Pre1);
            }
            if ($("#QuantityType").val() == "Quantity") {
                $("#TotalQuantity").val($("#Quantity").val());
            }
        });
        $('#myItemTable tbody').on('click', 'a', function () {
            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var operation = $(this).attr('operation');
            var controlId = $(this).attr('id');
            splitedRowID = controlId.split("_");

            if (operation == "Delete Item") {
                if (parseInt(datatableid) > 0) {
                    $(this).closest('tr').attr('isdeleted', '1');
                    $(this).closest('tr').remove();



                    //var siblings = $(this).closest('tr').siblings();
                    //row.remove();
                    //siblings.each(function (index) {
                    //    $(this).children('td').first().text(index + 1);
                    //});
                }
                else {
                    $(this).closest('tr').remove();
                    var inventoryItemsNoOfRows = $('#myItemTableBody tr').length;
                    if (parseInt(inventoryItemsNoOfRows) == 0) {
                        var string = "<tr class='odd'><td valign='top' colspan='6' class='dataTables_empty'>No data available in table</td></tr>";
                        $('#myItemTableBody').html(string);
                    }

                }
                var _totalQuantity = 0;
                var _counter = 0;
                $("#myItemTableBody tr").each(function () {

                    var _rowID = $(this).attr("id");
                    var _Quantity = ($('#Quantity_' + _rowID).val() == "") ? 0 : parseInt($('#Quantity_' + _rowID).val());
                    if ((parseInt(_Quantity) == 0) || (parseInt(_Quantity) == NaN) || _Quantity == "null") {

                    }
                    else {
                        if (!isNaN(_Quantity)) {
                            _totalQuantity = _totalQuantity + _Quantity;
                        }
                    }

                    $(this).children('td').first().text(_counter + 1);
                    _counter = _counter + 1;

                });
                $("#Quantity").val(_counter);
                if (_totalQuantity == 0) {
                    $("#TotalQuantity").val(_counter);
                }
                else {
                    $("#TotalQuantity").val(_totalQuantity);
                }


            }




        });

        $('#myItemTable tbody').on('blur', 'input', function () {

            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var operation = $(this).attr('operation');
            var controlId = $(this).attr('id');
            splitedRowID = controlId.split("_");
            if (splitedRowID[0] == "Quantity") {
                var _totalQuantity = 0;
                $("#myItemTableBody tr").each(function () {

                    var _rowID = $(this).attr("id");
                    var _Quantity = ($('#Quantity_' + _rowID).val() == "") ? 0 : parseInt($('#Quantity_' + _rowID).val());
                    if ((parseInt(_Quantity) == 0) || (parseInt(_Quantity) == NaN)) {

                    }
                    else {
                        _totalQuantity = _totalQuantity + _Quantity;
                    }

                });
                $("#TotalQuantity").val(_totalQuantity);

            }
            //_rowID = splitedRowID[1];
        });
        //$('#QuantityLeft').onchange(function () {
        //    debugger
        //    var a = $('#Quantity').val();
        //    var b = $('#QuantityLeft').val();
        //    $('#QuantityLeft').val() = a + b;

        //});

    },
    addInventoryBasicDetail: function () {
        debugger;
        var data = InventoryBasicDetail.getInventoryBasicDetail();
        //  ajaxRequest.makeRequest('/Inventory/Inventory/InventoryBasicDetails', "POST", data, InventoryBasicDetail.onSuccess, InventoryBasicDetail.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/InventoryBasicDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryBasicDetail.onSuccess(data)

            },
            failure: function (data) {
                InventoryBasicDetail.onError()
            },
        });
    },
    fatchInventoryBasicItemDetailByCommercialID: function () {
        debugger
        var data = {
            "ID": "0"
        };
        data.ID = $("#BasicDetailsID").val();
        //ajaxRequest.makeRequest('/Inventory/Inventory/FatchBasicItemsDetails', "POST", data, InventoryBasicDetail.onSuccessFatchInventoryBasicItemDetailByCommercialID, InventoryBasicDetail.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/Inventory/FatchBasicItemsDetails",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryBasicDetail.onSuccessFatchInventoryBasicItemDetailByCommercialID(data)
            },
            failure: function (data) {
                InventoryBasicDetail.onError()
            },
        });


    },
    fatchCategoryList: function () {

        var data = {
            "InventoryTypeId": ""
        };
        data.InventoryTypeId = $("#TypeID").val();
        // ajaxRequest.makeRequest('/Inventory/CategoryMaster/FatchCategoryByInventoryTypeId', "POST", data, InventoryBasicDetail.onSuccessInventoryType, InventoryBasicDetail.onError);
        $.ajax({
            type: 'Post',
            url: "/Inventory/CategoryMaster/FatchCategoryByInventoryTypeId",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InventoryBasicDetail.onSuccessInventoryType(data)
            },
            failure: function (data) {
                InventoryBasicDetail.onError()
            },
        });
    },
    fatchItemList: function () {

        var data = {
            "TypeID": ""
        };
        data.TypeID = $("#TypeID").val();
        data.CategoryID = $("#CategoryID").val();
        ajaxRequest.makeRequest('/InventoryItemMaster/fathcItemListByTypeAndCategory', "POST", data, InventoryBasicDetail.onSuccessInventoryCategory, InventoryBasicDetail.onError);
    },
    fatchItemDetails: function () {
       
        var data = {
            "ItemID": ""
        };
        data.ItemID = $("#ItemID").val();
    //    alert('asd')
        ajaxRequest.makeRequest('/InventoryItemMaster/fathcItemDetails', "POST", data, InventoryBasicDetail.onSuccessFatchItemDetails, InventoryBasicDetail.onError);
    },
    bindCategoryOptionList: function (data) {

        if (data != null) {
            var categoryList = [];
            categoryList = data;
            var count = categoryList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Category Type...</option>";
            for (i = 0; i < categoryList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + categoryList[i].ID + "' title = '" + categoryList[i].Name + "'>" + categoryList[i].Name + "</option>";
            }
            $("#CategoryID").html($cloneRow);
        }
    },
    bindItemsOptionList: function (data) {

        if (data != null) {
            var itemList = [];
            itemList = data;
            var count = itemList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Item...</option>";
            for (i = 0; i < itemList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + itemList[i].ID + "' title = '" + itemList[i].Name + "'>" + itemList[i].Name + "</option>";
            }
            $("#ItemID").html($cloneRow);
        }
    },
    createRowsInventoryBasicIntemDetails: function () {
        debugger
        var totalRows = $("#Quantity").val();//Quantity Received
        var Type = $("#QuantityType").val(); // Purchase Received
        var Pre = $("#QuantityLeft").val(); // PreReceived Quantity
        if (totalRows <= Type) {
            var _tableLastRowId = $('#myItemTableBody tr:last').attr('id');
            if (typeof _tableLastRowId === "undefined") {
                $('#myItemTableBody').html("");
                loopStartValue = 0;
                _tableLastRowId = 0;
            }
            else {
                ++_tableLastRowId;
            }

            if ($("#QuantityType").val() == "Quantity") {
                $("#Quantity").removeAttr("disabled");
            }


            for (var counter = _tableLastRowId; counter < parseInt(totalRows); ++counter) {
                var $cloneRow = "";
                $cloneRow = $cloneRow + '<tr id="' + counter + '" datatableID = "0" style="width:100%;" isdeleted="0" >';
                $cloneRow = $cloneRow + '<td>' + (parseInt(counter) + 1) + '</td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="BatchNumber_' + counter + '"   class="form-control" name="BatchNumber_' + counter + '" style="width: 100%;" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="LOTNo_' + counter + '"   class="form-control" name="LOTNo_' + counter + '" style="width: 100%;" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="ModelNumber_' + counter + '"   class="form-control" name="ModelNumber_' + counter + '" style="width: 100%;" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="SRNO_' + counter + '"   class="form-control" name="SRNO_' + counter + '" style="width: 100%;" /></td>';
                $cloneRow = $cloneRow + '<td><input type="text"id="BarcodeNumber_' + counter + '"   class="form-control" name="BarcodeNumber_' + counter + '" style="width: 100%;" /></td>';
                if (Type == "Quantity") {
                    $cloneRow = $cloneRow + '<td></td>';
                }
                else if (Type == "Batch Quantity") {
                    $cloneRow = $cloneRow + '<td><input type="text"id="Quantity_' + counter + '"   class="form-control allowNumbersWithDecimalOnly" name="Quantity_' + counter + '" style="width: 100%;" /></td>';
                }

                $cloneRow = $cloneRow + '<td><input type="text"id="Description_' + counter + '"   class="form-control"  name="Description_' + counter + '" style="width: 100%;" /></td>';
                //$cloneRow = $cloneRow + '<td><a href="#" title="Delete" class="glyphicon glyphicon-trash" data-toggle="modal" data-target="#myModal" id="rowDelete_' + counter + '" name="rowDelete_' + counter + '" operation="Delete Item"> </a>';
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

        }
        else {
            alert('Received Quantity must be less than or equal to quantity');
            $("#Quantity").val(Pre);
        }
    },
    getInventoryBasicDetail: function () {
        debugger;
        var data = {
            "ID": ""
        };
        data.ID = ($("#BasicDetailsID").val() == "") ? 0 : $("#BasicDetailsID").val();
        data.ItemName = $("#ItemName").val();
        data.ItemID = $("#ItemID").val();
        data.TypeID = $("#TypeID").val();
        data.UnitID = $("#UnitID").val();
        data.CategoryID = $("#CategoryID").val();
        data.QuantityType = $("#QuantityType").val();
        data.QuantityLeft = $("#QuantityLeft").val();
        data.Quantity = $("#Quantity").val();
        data.TQuantity = $("#TQuantity").val();
        data.Warranty = $("#Warranty").val();
        data.TotalQuantity = $("#TotalQuantity").val();
        data.InventoryBasicDetailsNumber = $("#InventoryBasicDetailsNumber").val();
        data.Manufacturer = $('#Manufacturer').val();
        data.GradeReceived = $('#GradeReceived').val();
        data.ConditionOfPackaging = $('#ConditionOfPackaging').val();
        data.IntegrityOfPackaging = $('#IntegrityOfPackaging').val();
        data.CertifiedConcentration = $('#CertifiedConcentration').val();
        data.Praceability = $('#Praceability').val();
        //data.Manufacturer = $('#Manufacturer').val();
        data.Remark = $('#Remark').val();

        data.PurchaseRequestID = $('#PurchaseRequestID').val();

        data.BrandReceived = $('#BrandReceived').val();

        //    var from = $("#DOMval").val().split("/")
        //    var f = new Date(from[2], from[1] - 1, from[0])
        //    data.DOM = f;
        //}

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




            var from = $("#DOMval").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            data.DOM = f;
        }
        //data.DOM = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#DOM").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
      
        if ($('#DOE').val() != "") {

        //    data.DOE = new Date($('#DOE').val());
        //}
        //else {

        //    var from = $("#DOEval").val().split("/")
        //    var f = new Date(from[2], from[1] - 1, from[0])
        //    data.DOE = f;
        //}

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

      
        //data.DOE = new Date(
        //                             applicationSystem.convertionOfDateFormat(
        //                             $("#DOE").val(),
        //                             applicationSystem.aspectedFormat,
        //                             applicationSystem.currentServerFormat,
        //                             applicationSystem.aspectedServerSeparator,
        //                             applicationSystem.currentServerSeparator
        //                         ));
        data.ListInventoryBasicItemDetail = InventoryBasicDetail.getInventoryBasicItemDetail();
        return data;
    },
    getInventoryBasicItemDetail: function () {
        debugger;
        var ItemsArray = [];
        $("#myItemTableBody tr").each(function () {
            var _rowID = $(this).attr("id");
            debugger
            //splitedRowID = _rowID.split("_");
            //_rowID = splitedRowID[1];
            var Item = {
                "ID": "",
                "InventoryBasicDetailsID": "",
                "BatchNumber": "",
                "LOTNo": "",
                "ModelNumber": "",
                "SRNO": "",
                "BarcodeNumber": "",
                "Quantity": "",
                "Description": "",
                "IsActive": "",
                "IsDeleted": ""
            };
            var valueOfRowID = $('#' + _rowID).attr("datatableid");
            if ((valueOfRowID == "undefined") || (valueOfRowID == "") || (valueOfRowID == null)) {
                Item.ID = 0;
            }
            else {
                Item.ID = valueOfRowID;
            }

            Item.InventoryBasicDetailsID = $("#BasicDetailsID").val();
            Item.BatchNumber = $('#BatchNumber_' + _rowID).val();
            Item.LOTNo = $('#LOTNo_' + _rowID).val();
            Item.ModelNumber = $('#ModelNumber_' + _rowID).val();
            Item.SRNO = $('#SRNO_' + _rowID).val();
            Item.BarcodeNumber = $('#BarcodeNumber_' + _rowID).val();
            Item.Quantity = $("#Quantity").val();
            Item.Description = $('#Description_' + _rowID).val();

            var isDeletedValue = $('#' + _rowID).attr("isdeleted");
            if (isDeletedValue == 1) {
                Item.IsDeleted = true;
            }
            else {
                Item.IsDeleted = false;
            }
            ItemsArray.push(Item);
        });
        return ItemsArray;
    },

    setInventoryBasicDetails: function (data) {
        debugger;
        if (data != null) {
            $("#ItemID").val(data.ItemID).change();
            $("#ID").val(data.ID);
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
            $('#StorageLocation').val(data.StorageLocation);


            //$('#DOM').val(applicationSystem.convertJsonDateFormat(data.DOM, "dd/MM/yyyy"));
            //$('#DOM').val(applicationSystem.convertionFromJsonDateFormat(data.DOM, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator));
            //var x1 = applicationSystem.convertionFromJsonDateFormat(data.DOM, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator);
            //var y1 = x1.split("/");
            //var z1 = y1[1] + "/" + y1[0] + "/" + y1[2];
            ////$('#DOMval').val(z1);
            //if (z1 == "undefined//undefined") {
            //    $("#DOMval").val('');
            //}
            //else {
            //    $('#DOMval').val(z1);
            //}

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

            //$('#DOE').val(applicationSystem.convertionFromJsonDateFormat(data.DOE, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator));
            //var x2 = applicationSystem.convertionFromJsonDateFormat(data.DOE, applicationSystem.currentServerFormat, applicationSystem.currentServerSeparator);
            //var y2 = x2.split("/");
            //var z2 = y2[1] + "/" + y2[0] + "/" + y2[2];
            ////$('#DOEval').val(z2);
            //if (z2 == "undefined//undefined") {
            //    $("#DOEval").val('');
            //}
            //else {
            //    $("#DOEval").val(z2);
            //}

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
            //$('#DOE').val(data.DOE);
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
        debugger;
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
    setInventoryBasicDetail: function () {

    },
    onSuccess: function (data) {
        debugger;
        if (data.status == "success") {
            applicationSystem.onSuccess(data);

            if (parseInt($("#BasicDetailsID").val()) > 0) {
                InventoryCommercialDetails.fatchInventoryCommercialDetails();
                $("#tabHead_CommercialDetails").trigger("click");
            }
            else {
                $("#BasicDetailsID").val(data.ID);
                $("#tabHead_CommercialDetails").attr("data-toggle", "tab");
                $("#tabHead_CommercialDetails").trigger("click");
                //$("#CommercialDetailsID").val(data.ID);
                $("#add_new_item_li").css("display", "none");
            }
        }
        if (data.status == "fail") {
            Swal.fire('Error!!!!')
        }
    },
    onbasicDetailSuccess: function (data) {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: `Save`,
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                Swal.fire('Saved!', '', 'success')
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    },
    onSuccessFatchInventoryBasicItemDetailByCommercialID: function (data) {
        debugger
        if (data.status == "success") {
            InventoryBasicDetail.bindBasicItemsOptionList(data.BasicItemsDetails);
        }
        if (data.status == "fail") {
        }
    },
    onSuccessInventoryType: function (data) {
        if (data.status == "success") {
            if (data.listCategoryMaster != null) {
                InventoryBasicDetail.bindCategoryOptionList(data.listCategoryMaster);
            }
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onSuccessInventoryCategory: function (data) {

        if (data.status == "success") {
            if (data.ListOfItems != null) {
                InventoryBasicDetail.bindItemsOptionList(data.ListOfItems);
            }
        }
        else if (data.status == "info") {
            //applicationSystem.onError(data);
        }
        else if (data.status == "fail") {
            //applicationSystem.onError(data);
        }
    },
    onSuccessFatchItemDetails: function (data) {
        debugger
        if (data.status == "success") {
            if (data.ItemDetails != null) {
                //Set inventory type and category select values
                $("#ItemID").val(data.ItemDetails.ItemID);
                $("#TypeID").val(data.ItemDetails.TypeID);
                $("#TypeID").val(data.ItemDetails.InventoryTypeID);
                $("#CategoryID").val(data.ItemDetails.CategoryID);
                $("#CategoryID").val(data.ItemDetails.CatagoryMasterID);
                $("#UnitID").val(data.ItemDetails.UnitID);

                //$("#InventoryCapacityMasterId").val(data.ItemDetails.InventoryCapacityMasterId);
                //InventoryBasicDetail.bindItemsDetailsToControls(data.ItemDetails);
            }
        }
        else if (data.status == "info") {
            //applicationSystem.onError(data);
        }
        else if (data.status == "fail") {
            //applicationSystem.onError(data);
        }
    },
    onError: function (data) {
        applicationSystem.onError(data);
    },

};