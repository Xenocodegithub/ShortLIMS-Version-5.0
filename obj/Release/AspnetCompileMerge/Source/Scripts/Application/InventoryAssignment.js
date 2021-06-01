var InventoryAssignment = {
    InventoryItemObject: [],
    InventoryItemOptionList: null,
    InventorySerialNumberList: null,
    InventoryItemType: null,
    InventoryAssignmentDetailsID: null,
    Operation: null,
    initialize: function () {
        InventoryAssignment.attachEvents();

        debugger;
        var InventoryItemDtoObject = $('#InventoryItemList').val();
        InventoryAssignment.InventoryItemObject = $.parseJSON(InventoryItemDtoObject);
        InventoryAssignment.bindInventoryItemOptionList();
    },
    attachEvents: function () {

        $("#buttonSave").click(function () {
            InventoryAssignment.addAssignmentItems();
        });

        $("#buttonReturn").click(function () {
            InventoryAssignment.returnAssignmentItems();
        });

        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var _operation = $(this).attr('operation');
            if (_operation == "Assignment") {
                InventoryAssignment.Operation = "Assignment";
                $("#insert-assignment").css("display", "block");
                $("#return-assignment").css("display", "none");
                var _itemID = $(this).attr('itemid');
                var _id = $(this).attr('id');
                var _requestquantity = $(this).attr('requestquantity');
                var _itemTypeName = $(this).attr('ItemTypeName');
                InventoryAssignment.InventoryItemType = _itemTypeName;
                InventoryAssignment.InventoryAssignmentDetailsID = _id;
                if (_itemTypeName == 'Nonconsumable') {
                    InventoryAssignment.fatchInventoryItemSerialNumber(_itemID);
                }
                $("#ItemID").val(_itemID);
                $("#RequestQuantity").val(_requestquantity);
                $("#AvailableQuantity").val($('#ItemID').find(":selected").attr('availablequantity'));
            }
            else if (_operation == "Return") {
                InventoryAssignment.Operation = "Return";
                $("#return-assignment").css("display", "block");
                $("#insert-assignment").css("display", "none");
                var _itemID = $(this).attr('itemid');
                var _id = $(this).attr('id');
                var _assignedquantity = $(this).attr('assignedquantity');
                var _itemTypeName = $(this).attr('ItemTypeName');
                InventoryAssignment.InventoryItemType = _itemTypeName;
                InventoryAssignment.InventoryAssignmentDetailsID = _id;
                if (_itemTypeName == 'Nonconsumable') {
                    InventoryAssignment.fatchInventoryItemSerialNumber(_itemID);
                }
                $("#AssignedItemID").val(_itemID);
                $("#AssignedItemQuantity").val(_assignedquantity);
                $("#AvailableQuantity").val($('#ItemID').find(":selected").attr('availablequantity'));
            }
            
        });

        $('#serialNumberDataTableForReturn tbody').on('click', 'a', function () {
            debugger;

            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var _inventoryassignmentdetailsid = $(this).closest('tr').attr('inventoryassignmentdetailsid');

            var data = {
                "ID": ""
            };
            data.ID = datatableid;
            data.InventoryAssignmentDetailsID = _inventoryassignmentdetailsid;
            data.InventorySerialNumber = $("#inventorySerialNumber_" + listItemId).val();
            data.ItemID = $("#AssignedItemID").val();

            var dialog = BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Warning',
                message: "Are you sure, you want to return this item?",
                buttons: [
                            {
                                icon: 'glyphicon glyphicon-pencil',
                                label: 'Yes',
                                cssClass: 'btn-warning',
                                action: function () {
                                    
                                    ajaxRequest.makeRequest('/InventoryAssignmentMaster/ReturnNonConsumableProduct', "POST", data, InventoryAssignment.onSuccessReturnNonconsumableData, InventoryAssignment.onError);

                                    dialog.close();
                                    //$("#" + listItemId).remove();
                                    //$(this).closest('tr').remove();
                                }
                            },
                            {
                                label: 'No',
                                cssClass: 'btn-warning',
                                action: function (dialogRef) {
                                    dialogRef.close();
                                }
                            }
                ]
            });


            
        });

        $("#AssignQuantity").blur(function () {
            var _value = $(this).val();
            var _maxValue = $('#AvailableQuantity').val();
            if (parseFloat(_maxValue) < parseFloat(_value)) {
                $(this).val('');
                var data = {
                    "message":"quantity should be less than or equals to available quantity."
                };
                applicationSystem.onError(data);
            }
            else {
                if (InventoryAssignment.InventoryItemType == "Nonconsumable") {
                    InventoryAssignment.setSerialNumberTable(_value);
                }
            }
        });

    },
    addAssignmentItems: function(){
        debugger;
        var data = {
            "ItemID": ""
        };
        data.AssignedQuantity = $("#AssignQuantity").val();
        data.ID = InventoryAssignment.InventoryAssignmentDetailsID;
        data.InventoryItemID = $("#ItemID").val();
        data.InventoryAssignedSerialNumberDetails = InventoryAssignment.getAssignmentSerialNumber();
        ajaxRequest.makeRequest('/InventoryAssignmentMaster/InsertAssignmentDetails', "POST", data, InventoryAssignment.onSuccessAddAssignmentItems, InventoryAssignment.onError);
    },
    returnAssignmentItems: function(){
        debugger;
        var data = {
            "ID": ""
        };
        data.ID = InventoryAssignment.InventoryAssignmentDetailsID;
        data.InventoryItemID = $("#AssignedItemID").val();
        data.ReturnQuantity = $("#ReturnQuantity").val();
        ajaxRequest.makeRequest('/InventoryAssignmentMaster/ReturnConsumableProduct', "POST", data, InventoryAssignment.onSuccessReturnAssignmentItems, InventoryAssignment.onError);
    },
    getAssignmentSerialNumber: function(){
        debugger;
        var InventoryAssignmentSerialNumberArray = [];
        $("#serialNumberDataTableBody tr").each(function () {
            var _rowID = $(this).attr("id");
            var InventoryAssignmentSerialNumber = {
                "ID": "0",
                "InventoryAssignmentDetailsID": "",
                "InventorySerialNumber": "",
                "ItemID": "",
                "IsActive": ""
            };
            InventoryAssignmentSerialNumber.InventoryAssignmentDetailsID = InventoryAssignment.InventoryAssignmentDetailsID;
            InventoryAssignmentSerialNumber.InventorySerialNumber = $('#inventorySerialNumber_' + _rowID).val();
            InventoryAssignmentSerialNumberArray.push(InventoryAssignmentSerialNumber);
        });
        return InventoryAssignmentSerialNumberArray;
    },
    fatchInventoryItemSerialNumber: function (ItemID) {
        debugger;
        var data = {
            "ItemID": ""
        };
        data.ItemID = parseInt(ItemID);
        ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchSerialNumbersByItem', "POST", data, InventoryAssignment.onSuccessFatchSerialNumberList, InventoryAssignment.onError);
    },
    fatchAssignedItemSerialNumber: function (ItemID) {
        debugger;
        var data = {
            "InventoryAssignmentDetailsID": ""
        };
        data.InventoryAssignmentDetailsID = parseInt(InventoryAssignment.InventoryAssignmentDetailsID);
        ajaxRequest.makeRequest('/InventoryAssignmentMaster/FatchSerialNumberDetails', "POST", data, InventoryAssignment.onSuccessFatchAssignedItemSerialNumber, InventoryAssignment.onError);
    },
    bindInventoryItemOptionList: function () {
        debugger;
        if (InventoryAssignment.InventoryItemObject != null) {
            var InventoryItemList = [];
            InventoryItemList = InventoryAssignment.InventoryItemObject;
            var count = InventoryItemList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Inventory Item...</option>";
            for (i = 0; i < InventoryItemList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + InventoryItemList[i].ID + "' title = '" + InventoryItemList[i].Name + "' AvailableQuantity='" + InventoryItemList[i].AvailableStock + "' Unit='" + InventoryItemList[i].UnitMasterDTO.Unit + "'>" + InventoryItemList[i].Name + "</option>";
            }
            InventoryAssignment.InventoryItemOptionList = $cloneRow;
            $("#ItemID").html($cloneRow);
            $("#AssignedItemID").html($cloneRow);
        }
    },
    bindBasicItemsOptionList: function (data) {
        debugger;
        if (data != null) {
            var ItemsList = [];
            ItemsList = data;
            var count = ItemsList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Serial Number...</option>";
            for (i = 0; i < ItemsList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + ItemsList[i].ID + "' title = '" + ItemsList[i].ID + "'>INV/" + ItemsList[i].ID + "</option>";
            }
            InventoryAssignment.InventorySerialNumberList = $cloneRow;

            if (InventoryAssignment.Operation == "Return") {
                InventoryAssignment.fatchAssignedItemSerialNumber();
            }

            //$("#inventorySerialNumber_" + InventoryAssignment.ControlRowID).html($cloneRow);
        }
    },
    setSerialNumberTable: function (quantity) {
        debugger;
        if (quantity != null) {

            if (InventoryAssignment.Operation == "Assignment") {
                $('#serialNumberDataTableBody').html("");
                for (i = 0; i < parseInt(quantity) ; ++i) {
                    var $cloneRow = "";
                    $cloneRow = $cloneRow + '<tr id="' + i + '" datatableID = "' + InventoryAssignment.InventoryAssignmentDetailsID + '" isdelete = "0" style="width:100%;">';
                    $cloneRow = $cloneRow + '<td>' + (parseInt(i) + 1) + '</td>';
                    $cloneRow = $cloneRow + '<td><select id="inventorySerialNumber_' + i + '" name="inventorySerialNumber_' + i + '" class="form-control" style="width: 100%;"></select></td>';
                    $cloneRow = $cloneRow + '<td style="text-align: center;"><a href="#" title="Delete" id="rowDelete_' + i + '" name="rowDelete_' + i + '" style="width: 100%;" Operation="Assignment">delete</a></td>';
                    $cloneRow = $cloneRow + '</tr>';
                    $('#serialNumberDataTableBody').append($cloneRow);
                    $('#inventorySerialNumber_' + i).html(InventoryAssignment.InventorySerialNumberList);
                }
            }
            else if (InventoryAssignment.Operation == "Return") {
                $('#serialNumberDataTableForReturnBody').html("");
                for (i = 0; i < parseInt(quantity) ; ++i) {
                    var $cloneRow = "";
                    $cloneRow = $cloneRow + '<tr id="' + i + '" datatableID = "' + InventoryAssignment.InventoryAssignmentDetailsID + '" isdelete = "0" style="width:100%;">';
                    $cloneRow = $cloneRow + '<td>' + (parseInt(i) + 1) + '</td>';
                    $cloneRow = $cloneRow + '<td><select id="inventorySerialNumber_' + i + '" name="inventorySerialNumber_' + i + '" class="form-control" style="width: 100%;"></select></td>';
                    $cloneRow = $cloneRow + '<td style="text-align: center;"><a href="#" title="Delete" id="rowDelete_' + i + '" name="rowDelete_' + i + '" style="width: 100%;" Operation="Return">Return</a></td>';
                    $cloneRow = $cloneRow + '</tr>';
                    $('#serialNumberDataTableForReturnBody').append($cloneRow);
                    $('#inventorySerialNumber_' + i).html(InventoryAssignment.InventorySerialNumberList);
                }
            }
            debugger;
            
            
            
            if (InventoryAssignment.Operation == "Return") {
                $("#serialNumberDetailsTableRowForReturn").css("display", "block");
            }
            else if (InventoryAssignment.Operation == "Assignment") {
                $("#serialNumberDetailsTableRow").css("display", "block");
            }
            
            
        }
    },
    setSerialNumberTableData: function (data) {
        debugger;
        if (data != null) {
            var serialNumberItems = [];
            serialNumberItems = data;
            var count = serialNumberItems.length;
            for (i = 0; i < parseInt(count) ; ++i) {
                $('#inventorySerialNumber_' + i).val(serialNumberItems[i].InventorySerialNumber);
                $("#" + i).attr("datatableid", serialNumberItems[i].ID);
                $("#" + i).attr("InventoryAssignmentDetailsID", serialNumberItems[i].InventoryAssignmentDetailsID);

            }
            debugger;
        }
    },
    onSuccessFatchSerialNumberList: function (data) {
        debugger;
        if (data.status == "success") {
            InventoryAssignment.bindBasicItemsOptionList(data.BasicItemsDetails);
        }
        if (data.status == "fail") {
        }
    },

    onSuccessAddAssignmentItems: function (data) {
        debugger;
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            $("#serialNumberDetailsTableRow").css("display", "none");
            $('#serialNumberDataTableBody').html("");
            $("#AssignQuantity").val("");
            $("#AvailableQuantity").val("");
            $("#RequestQuantity").val("");
            $("#ItemID").val(-1);
            location.reload(true);
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    onSuccessFatchAssignedItemSerialNumber: function(data){
        debugger;
        if (data.CollectionResponse != null) {
            var _len = data.CollectionResponse.length;
            InventoryAssignment.setSerialNumberTable(_len);
            InventoryAssignment.setSerialNumberTableData(data.CollectionResponse);

        }
    },

    onSuccessReturnNonconsumableData: function(data){
        debugger;
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    onSuccessReturnAssignmentItems: function (data) {
        debugger;
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    onError: function (data) {
        applicationSystem.onError(data);
    },

};