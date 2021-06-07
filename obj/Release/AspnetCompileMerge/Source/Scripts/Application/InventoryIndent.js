var InventoryIndent = {
    InventoryTypeObject: [],
    InventoryTypeOptionList: null,
    InventoryItemObject: [],
    InventoryItemOptionList: null,
    ControlRowID: null,
    initialize: function () {
        debugger;
        var InventoryTypeDtoObject = $('#InventoryType').val();
        InventoryIndent.InventoryTypeObject = $.parseJSON(InventoryTypeDtoObject);
        InventoryIndent.bindInventoryTypeOptionList(InventoryIndent.InventoryTypeObject);
        debugger;
        var InventoryItemDtoObject = $('#InventoryItem').val();
        InventoryIndent.InventoryItemObject = $.parseJSON(InventoryItemDtoObject);
        //InventoryIndent.bindInventoryItemOptionList(InventoryIndent.InventoryItemObject);



        InventoryIndent.attachEvents();




    },
    attachEvents: function () {
        var counter = 1;
        debugger;
        $("#addIndent").on("click", function () {

            var taxdetailsRowID = $('#myItemTableBody tr:last').attr('id');
            if (typeof taxdetailsRowID === "undefined") {
                $('#myItemTableBody').html("");
                counter = 0;
            }
            else {
                //var splitExpenseRowID = taxdetailsRowID.split('_');
                //counter = splitExpenseRowID[1];
                counter = parseInt(counter) + 1;
            }
            var $cloneRow = "";
            $cloneRow = $cloneRow + '<tr id="' + counter + '" datatableID = "0" style="width:100%;">';
            $cloneRow = $cloneRow + '<td>' + (parseInt(counter) + 1) + '</td>';
            $cloneRow = $cloneRow + '<td><select id="inventoryType_' + counter + '" name="inventoryType_' + counter + '" class="form-control" style="width: 100%;"></select></td>';
            $cloneRow = $cloneRow + '<td><select id="inventoryItem_' + counter + '" name="inventoryItem_' + counter + '" class="form-control" style="width: 100%;"></select></td>';
            //$cloneRow = $cloneRow + '<td><select id="inventorySerialNumber_' + counter + '" name="inventorySerialNumber_' + counter + '" class="form-control" style="width: 100%;"></select></td>';
            $cloneRow = $cloneRow + '<td><input type="text" id="unit_' + counter + '" name="unit_' + counter + '" class="form-control" disabled="disabled" style="width: 100%;"/></td>';
            $cloneRow = $cloneRow + '<td><input type="text" id="requestQuantity_' + counter + '" name="requestQuantity_' + counter + '" class="form-control" style="width: 100%;"/></td>';
            $cloneRow = $cloneRow + '<td><input type="text" id="availableQuantity_' + counter + '" name="availableQuantity_' + counter + '" class="form-control" disabled="disabled" style="width: 100%;"/></td>';
            $cloneRow = $cloneRow + '<td style="text-align: center;"><a href="#" title="Delete" id="rowDelete_' + counter + '" name="rowDelete_' + counter + '" style="width: 100%;">delete</a></td>';
            $cloneRow = $cloneRow + '</tr>';

            $('#myItemTableBody').append($cloneRow);
            $('#inventoryType_' + counter).html(InventoryIndent.InventoryTypeOptionList);
        });


        $('#myItemTable tbody').on('change', 'select', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var controlID = $(this).attr('id');
            var SplitControlID = controlID.split("_");

            if (SplitControlID[0] == "inventoryType") {
                var TestType = $("#" + controlID + " option:selected").val();
                var _inventoryType = $("#" + controlID + " option:selected").text();
                if (_inventoryType == "Consumable") {
                    //$("#requestQuantity_" + SplitControlID[1]).removeAttr("disabled");
                    $("#inventorySerialNumber_" + SplitControlID[1]).attr("disabled", "disabled");
                }
                else if (_inventoryType == "Nonconsumable") {
                    //$("#requestQuantity_" + SplitControlID[1]).attr("disabled", "disabled");
                    $("#inventorySerialNumber_" + SplitControlID[1]).removeAttr("disabled");
                }
                InventoryIndent.bindInventoryItemOptionList(TestType, SplitControlID[1]);
            }
            else if (SplitControlID[0] == "inventoryItem") {
                debugger;
                var ItemID = $("#" + controlID + " option:selected").val();
                var AvailableQuantity = $("#" + controlID + " option:selected").attr("AvailableQuantity");
                var Unit = $("#" + controlID + " option:selected").attr("Unit");
                $("#unit_" + SplitControlID[1]).val(Unit);
                $("#availableQuantity_" + SplitControlID[1]).val(AvailableQuantity);

                var _inventoryItemType = $("#inventoryType_" + SplitControlID[1] + " option:selected").text();
                //alert(_inventoryItemType);
                //if (_inventoryItemType == "Nonconsumable") {
                //    InventoryIndent.ControlRowID = SplitControlID[1];
                //    InventoryIndent.fatchInventoryItemSerialNumber(ItemID);
                //}
            }
            else if (SplitControlID[0] == "inventorySerialNumber") {
                $("#requestQuantity_" + SplitControlID[1]).val("1");
            }

            return false;
        });

        $('#myItemTable tbody').on('blur', 'input', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var datatableid = $(this).closest('tr').attr('datatableid');
            var controlID = $(this).attr('id');
            var SplitControlID = controlID.split("_");

            if (SplitControlID[0] == "requestQuantity") {
                var _availableQuantity = $("#availableQuantity_" + SplitControlID[1]).val();
                var _requestQuantity = $(this).val();
                if (parseFloat(_availableQuantity) < parseFloat(_requestQuantity)) {
                    $(this).val("");
                    var data = {
                        "message":"Quantity should be less than available quantity."
                    };
                    applicationSystem.onError(data);
                }
            }
            return false;
        });


        $('#myItemTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var splitedRowID = listItemId.split("_");
            var datatableid = $(this).closest('tr').attr('datatableid');
            if (datatableid != 0) {
                $(this).closest('tr').attr('isdelete', '1');
                $(this).closest('tr').hide();
            }
            else {
                $(this).closest('tr').remove();
                var myTaxDetailsItemsNoOfRows = $('#myItemTableBody tr').length;
                if (parseInt(myTaxDetailsItemsNoOfRows) == 0) {
                    var string = "<tr class='odd'><td valign='top' colspan='7' class='dataTables_empty'>No data available in table</td></tr>";
                    $('#myItemTableBody').html(string);
                }
            }
            return false;
        });

        $('#buttonSave').click(function () {
            debugger;
            InventoryIndent.addInventoryIndent();
        });

    },
    addInventoryIndent: function () {
        debugger;
        var data = InventoryIndent.getInventoryIndent();
        ajaxRequest.makeRequest('/InventoryAssignmentMaster/Indent', "POST", data, InventoryIndent.onSuccess, InventoryIndent.onError);
    },
    fatchInventoryItemSerialNumber: function (ItemID) {
        debugger;
        var data = {
            "ItemID": ""
        };
        data.ItemID = parseInt(ItemID);
        ajaxRequest.makeRequest('/InventoryMaintainanceAndCalibration/FatchSerialNumbersByItem', "POST", data, InventoryIndent.onSuccessFatchSerialNumberList, InventoryIndent.onError);


    },
    getInventoryIndent: function () {
        var data = {
            "ID": ""
        };
        data.ListInventoryAssignmentDetail = InventoryIndent.getInventoryAssignmentDetail();
        return data;
    },
    getInventoryAssignmentDetail: function(){
        debugger;
        var InventoryAssignmentDetailArray = [];
        $("#myItemTableBody tr").each(function () {
            var _rowID = $(this).attr("id");
            var InventoryAssignmentDetail = {
                "ID": "",
                "InventoryAssignmentMasterID": "",
                "InventoryTypeID": "",
                "InventoryItemID": "",
                //"InventorySerialNumber": "",
                "RequestQuantity": "",
                "IsDeleted": ""
            };
            if (($('#' + _rowID).attr("datatableid") == "undefined") || ($('#' + _rowID).attr("datatableid") == "") || ($('#' + _rowID).attr("datatableid") == null)) {
                InventoryAssignmentDetail.ReqDetailsId = 0;
            }
            else {
                InventoryAssignmentDetail.ReqDetailsId = $('#' + _rowID).attr("datatableid");
            }
            InventoryAssignmentDetail.InventoryTypeID = $('#inventoryType_' + _rowID).val();
            InventoryAssignmentDetail.InventoryItemID = $('#inventoryItem_' + _rowID).val();
            //InventoryAssignmentDetail.InventorySerialNumber = $('#inventorySerialNumber_' + _rowID).val();
            InventoryAssignmentDetail.RequestQuantity = $('#requestQuantity_' + _rowID).val();
            //InventoryAssignmentDetail.IsDeleted = $('#' + _rowID).attr("isdeleted");
            InventoryAssignmentDetailArray.push(InventoryAssignmentDetail);
        });
        return InventoryAssignmentDetailArray;
    },
    bindInventoryTypeOptionList: function (data) {
        debugger;
        if (data != null) {
            var InventoryTypeList = [];
            InventoryTypeList = data;
            var count = InventoryTypeList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Inventory Type...</option>";
            for (i = 0; i < InventoryTypeList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + InventoryTypeList[i].ID + "' title = '" + InventoryTypeList[i].Name + "'>" + InventoryTypeList[i].Name + "</option>";
            }
            InventoryIndent.InventoryTypeOptionList = $cloneRow;
        }
    },
    bindInventoryItemOptionList: function (TestType, ID) {
        debugger;
        if (InventoryIndent.InventoryItemObject != null) {
            var InventoryItemList = [];
            

            var result = $.grep(InventoryIndent.InventoryItemObject, function (e) { return e.InventoryTypeID == TestType; });
            InventoryItemList = result;
            var count = InventoryItemList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Inventory Item...</option>";
            for (i = 0; i < InventoryItemList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + BasicItems[i].ID + "' title = '" + BasicItems[i].ID + "'>INV/" + BasicItems[i].ID + "</option>";
            }
            InventoryIndent.InventoryItemOptionList = $cloneRow;
            $("#inventoryItem_" + ID).html($cloneRow);
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
                $cloneRow = $cloneRow + "<option value='" + ItemsList[i].ID + "' title = '" + ItemsList[i].ID + "'>INV/" + ItemsList[i].ID + "</option>";
            }
            $("#inventorySerialNumber_" + InventoryIndent.ControlRowID).html($cloneRow);
        }
    },
    onSuccessFatchSerialNumberList: function (data) {
        if (data.status == "success") {
            InventoryIndent.bindBasicItemsOptionList(data.BasicItemsDetails);
        }
        if (data.status == "fail") {
        }
    },
    onSuccess: function (data) {
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            $("#myItemTableBody").html("");
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onError: function (data) {
        debugger;
        applicationSystem.onError(data);
    }
};