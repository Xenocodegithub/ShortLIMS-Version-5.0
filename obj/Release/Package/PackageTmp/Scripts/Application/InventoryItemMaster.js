var InventoryItemMaster = {
    ItemID: "",
    Min_stock: "",
    Available_stock: "",
    initialize: function () {
        InventoryItemMaster.attachEvents();
        //console.debug($.cookie("previousURL"));
    },
    attachEvents: function () {
        $("#InventoryTypeID").on("change", function () {
            debugger;
            if ($("#InventoryTypeID").val() > 0) {
                InventoryItemMaster.fatchCategoryList();
            }
            else {
                $("#CategoryID").val(-1);
            }
        });

        $("#buttonSave").on("click", function () {
            debugger;
            if ($("#UnitID").val() == "") {
                alert('Please Select Unit');
            }
            else {

                if ($("#InventoryItemMaster_Create").valid() == true) {
                    InventoryItemMaster.addInventoryItemMaster();
                    return false;
                }
            }
        });

        $("#buttonSearch").on("click", function () {
            debugger;
            //   if ($("#InventoryItemMaster_Create").valid() == true) {
            InventoryItemMaster.SearchList();
            return false;
            // }
        });

        $("#buttonSaveIssue").on("click", function () {
            debugger;
            if ($("#modelIssueToID").val() != "" && $("#modelIssueDate").val() != "" && $("#modelIssueQty").val() != "") {

                InventoryItemMaster.addIssueTo();
                return false;

            }
            else {
                var data = {
                    "message": ""
                }
                data.message = "Mandatory fields are Required..!"
                applicationSystem.onError(data);
            }
        });

        $('#buttoncloseModelIssue').click(function () {
            debugger;

            $('#IssueToModal').modal('toggle');
            $('#IssueToModal').modal('hide');

        });

        $('#CrossClose').click(function () {
            debugger;

            $('#IssueToModal').modal('toggle');
            $('#IssueToModal').modal('hide');

        });

        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var minstock = $(this).closest('tr').attr('minstock');
            var availablestock = $(this).closest('tr').attr('availablestock');

            InventoryItemMaster.ItemID = listItemId;
            InventoryItemMaster.Min_stock = minstock;
            InventoryItemMaster.Available_stock = availablestock;

            var operation = $(this).attr('operation');

            if (operation == "IssueTo") {
                $("#modelIssueQty").val("");
                $("#modelIssueDate").val("");
                $("#modelIssueToID").val(-1);
                $("#modelIssueQtyError").css("display", "none");
                $("#modelIssueQtyError").text("");
                $('#IssueToModal').show();
                debugger;
                var data = {
                };

                ajaxRequest.makeRequest('/InventoryItemMaster/GetStockIssueToData', "POST", data, InventoryItemMaster.onSuccessStockIssueTo, InventoryItemMaster.onError);
                return false;
            }
            if (operation == "deactive") {
                var data = {
                    "ID": ""
                }
                data.ID = listItemId
                data.IsActive = 'false'
                debugger;
                var dialog = BootstrapDialog.show({
                    type: BootstrapDialog.TYPE_WARNING,
                    title: 'Warning',
                    message: "Are you sure, you wants to Deactivate this record?",
                    buttons: [
                        {
                            icon: 'glyphicon glyphicon-ban-circle',
                            label: 'Deactivate',
                            cssClass: 'btn-warning',
                            action: function () {
                                debugger;
                                InventoryItemMaster.activeInventory(data);
                                dialog.close();
                                // $("#" + listItemId).remove();
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
                return false;
            }
            if (operation == "active") {
                var data = {
                    "ID": ""
                }
                data.ID = listItemId
                data.IsActive = 'true'
                debugger;
                var dialog = BootstrapDialog.show({
                    type: BootstrapDialog.TYPE_WARNING,
                    title: 'Warning',
                    message: "Are you sure, you wants to Activate this record?",
                    buttons: [
                        {
                            icon: 'glyphicon glyphicon-ok-circle',
                            label: 'Activate',
                            cssClass: 'btn-warning',
                            action: function () {
                                debugger;
                                InventoryItemMaster.activeInventory(data);
                                dialog.close();
                                //$("#" + listItemId).remove();
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
                return false;
            }
        });

        $("#modelIssueQty").blur(function () {
            debugger;
            if (!InventoryItemMaster.IsItemQuantityAvailable()) {
                //alert("Quantity is less available than aspected quantity");             

                $("#buttonSaveIssue").attr("disabled", "disabled");
                return false;
            }
            else {
                $("#buttonSaveIssue").removeAttr("disabled");
            }
        });
    },
    activeInventory: function (data) {
        debugger;
        ajaxRequest.makeRequest('/InventoryItemMaster/Activate', "POST", data, InventoryItemMaster.onSuccess, InventoryItemMaster.onError);
    },
    IsItemQuantityAvailable: function () {
        debugger;
        var _modelAvailableQty = $("#modelAvailableQty").val();
        var _modelIssueQty = $("#modelIssueQty").val();
        var _modelMinQty = $("#modelMinQty").val();
        //if (_modelAvailableQty == null && _modelAvailableQty == "") {
        //}

        //if (_modelIssueQty == null) {
        //}
        //if (parseFloat(_modelAvailableQty) > 0 && parseFloat(_modelMinQty) <= parseFloat(_modelIssueQty) && parseFloat(_modelMinQty) > parseFloat(_modelAvailableQty) && parseFloat(_modelIssueQty) > parseFloat(_modelAvailableQty)) {
        if (parseFloat(_modelAvailableQty) > 0) {
            var _differenceQuantity = 0;
            _differenceQuantity = parseFloat(_modelAvailableQty) - parseFloat(_modelIssueQty);
        }
        if (parseFloat(_differenceQuantity) < 0) {
            //$("#modelIssueQtyError").css("display", "none");
            //$("#modelIssueQtyError").text("");
            var data = {
                "message": ""
            }
            data.message = "Entered quantity is greater than available quantity"
            applicationSystem.onError(data);
            return false;
        }
        else if (parseFloat(_modelAvailableQty) == 0) {
            //$("#modelIssueQtyError").css("display", "none");
            //$("#modelIssueQtyError").text("");
            var data = {
                "message": ""
            }
            data.message = "Available Quantity is zero please Add Item"
            applicationSystem.onError(data);
            return false;
        }
        else if (parseFloat(_modelIssueQty) == 0) {
            var data = {
                "message": ""
            }
            data.message = "Quantity can't be zero"
            applicationSystem.onError(data);
            return false;
        }
        //else if (parseFloat(_modelMinQty) < parseFloat(_modelIssueQty) && parseFloat(_modelMinQty) < parseFloat(_modelAvailableQty)) {
        //    $("#modelIssueQtyError").css("display", "block");
        //    $("#modelIssueQtyError").text("Issue quantity is greater than Min. quantity");
        //    return true;
        //}
        //else if (parseFloat(_modelMinQty) > parseFloat(_modelAvailableQty)) {
        //    $("#modelIssueQtyError").css("display", "block");
        //    $("#modelIssueQtyError").text("Available quantity is greater than Min. quantity");
        //    return true;
        //}
        else {
            //$("#modelIssueQtyError").css("display", "none");
            //$("#modelIssueQtyError").text("");
            return true;
        }

    },

    bindCategoryOptionList: function (data) {
        debugger;
        if (data != null) {
            var categoryList = [];
            categoryList = data;
            var count = categoryList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Category Type...</option>";
            for (i = 0; i < categoryList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + categoryList[i].ID + "' title = '" + categoryList[i].CategoryName + "'>" + categoryList[i].CategoryName + "</option>";
            }
            $("#CategoryID").html($cloneRow);

            //if (SampleAndParameterDetails.ModeOfOperation == "Edit") {
            //    $("#PurposeId").val(SampleAndParameterDetails.PurposeIDForEdit).change();
            //    SampleAndParameterDetails.fatchParametersDetails();

            //}

        }
    },
    addInventoryItemMaster: function () {
        debugger;
        var data = InventoryItemMaster.getInventoryItemMaster();
        ajaxRequest.makeRequest('/InventoryItemMaster/Create', "POST", data, InventoryItemMaster.onSuccess, InventoryItemMaster.onError);
    },
    SearchList: function () {
        debugger;
        var data = { "InventoryTypeID": $('#InventoryTypeID').val() }
        ajaxRequest.makeRequest('/InventoryItemMaster/List', "POST", data, InventoryItemMaster.onSuccessSearch, InventoryItemMaster.onError);
      // InventoryItemMaster.onError

    },
    addIssueTo: function () {
        debugger;
        var data = InventoryItemMaster.getIssueTo();
        ajaxRequest.makeRequest('/InventoryItemMaster/addIssueTo', "POST", data, InventoryItemMaster.onSuccess, InventoryItemMaster.onError);
    },

    fatchCategoryList: function () {
        debugger;
        var data = {
            "InventoryTypeId": ""
        };
        data.InventoryTypeId = $("#InventoryTypeID").val();
        ajaxRequest.makeRequest('/CategoryMaster/FatchCategoryByInventoryTypeId', "POST", data, InventoryItemMaster.onSuccessInventoryType, InventoryItemMaster.onError);
    },
    getInventoryItemMaster: function () {
        var data = {
            "ID": ""
        };
        data.ID = $("#ID").val();
        data.InventoryTypeID = $("#InventoryTypeID").val();
        data.CategoryID = $("#CategoryID").val();
        data.InventoryCapacityMasterId = $("#InventoryCapacityMasterId").val();
        data.Name = $("#Name").val();
        data.Code = $("#Code").val();

        data.UnitID = $("#UnitID").val();
        data.MinimumStock = $("#MinimumStock").val();
        data.IsActive = $("#IsActive").val();
        data.ReturnUrl = $("#ReturnUrl").val();
        return data;
    },
    getIssueTo: function () {
        var data = {
            "ID": ""
        };
        data.ID = $("#ID").val();
        data.IssueToNameID = $("#modelIssueToID").val();
        data.IssueQty = $("#modelIssueQty").val();
        data.IssueDate = new Date($("#modelIssueDate").val());
        //data.IssueDate = new Date(
        //                            applicationSystem.convertionOfDateFormat(
        //                            $("#modelIssueDate").val(),
        //                            applicationSystem.aspectedFormat,
        //                            applicationSystem.currentServerFormat,
        //                            applicationSystem.aspectedServerSeparator,
        //                            applicationSystem.currentServerSeparator
        //                        ));
        data.ItemID = $("#modelItemNameID").val();
        data.IsActive = true;
        data.AvailableStock = $("#modelAvailableQty").val();
        return data;
    },


    validateModelSaveCustomer: function () {


    },

    setInventoryItemMaster: function () {

    },
    onSuccess: function (data) {
        debugger;
        if (data.status == "success") {
            if (($("#ReturnUrl").val() == null) || ($("#ReturnUrl").val() == "")) {
                var url = applicationSystem.get_hostname() + "/Inventory/ListItem";
                window.location.href = url;
            }
            else {
                window.location.href = $("#ReturnUrl").val();

            }
            applicationSystem.onSuccess(data);

        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onSuccessSearch: function (data) {
        debugger;
        if (data.status == "success") {
            var $cloneRow = "";
            for (var i = 0; i < data.Result.length; i++) {
                $cloneRow = $cloneRow + "<tr id=" + data.Result[i].ID + " AvailableStock='" + data.Result[i].AvailableStock + "' minstock='" + data.Result[i].MinimumStock + "'>"
                $cloneRow = $cloneRow + "<td>" + (parseInt(i) + 1) + "</td>"
                $cloneRow = $cloneRow + "<td>" + data.Result[i].InventoryTypeMasterDTO.Name + "</td>"
                $cloneRow = $cloneRow + "<td>" + data.Result[i].InventoryCategoryMasterDTO.CategoryName + "</td>"
                $cloneRow = $cloneRow + "<td>" + data.Result[i].Name + "</td>"

                $cloneRow = $cloneRow + "<td>" + data.Result[i].AvailableStock + "</td>"
                $cloneRow = $cloneRow + "<td>" + data.Result[i].MinimumStock + "</td>"

                $cloneRow = $cloneRow + '<td><a href="../Inventory/AddItem?ID=' + data.Result[i].ID + '" title=Edit id=rowedit_' + i + '" operation="edit"><i class="glyphicon glyphicon-pencil" style="color: #3c8dbc; "></i></a>'
                if (data.Result[i].InventoryTypeID == 1 && data.Result[i].IsActive == true) {
                    $cloneRow = $cloneRow + '<a href="#" title="Issue To" id="rowIssueto_' + i + '" operation="IssueTo"><i class="fa fa-file-text" style="color: #3c8dbc; margin-left: 10px"></i></a>&nbsp;&nbsp;'
                }
                else {
                    $cloneRow = $cloneRow + '<a href="#" title="Issue To" id="rowIssueto_' + i + '" operation="IssueTo"></a>&nbsp;&nbsp;'
                }
                if (data.Result[i].IsActive == true) {
                    $cloneRow = $cloneRow + '<a href="#" title="Activate" id="rowDeactivate_' + i + '" operation="deactive"><i class="glyphicon glyphicon-ban-circle" style="color: green;"></i></a>'
                }
                else {
                    $cloneRow = $cloneRow + '<a href="#" title=""De Activate" id="rowDeactivate_' + i + '" operation="active"><i class="glyphicon glyphicon-ban-circle" style="color: red;"></i></a>'
                }
                $cloneRow = $cloneRow + '<td>'
                $cloneRow = $cloneRow + "</tr>"
            }

            $('#myDataTableBody').html($cloneRow);
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onSuccessStockIssueTo: function (data) {
        debugger;

        if (data.Itemdata != null) {
            var ItemList = [];
            debugger;
            ItemList = data.Itemdata;
            var count = ItemList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Item...</option>";
            for (i = 0; i < ItemList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + ItemList[i].ID + "'>" + ItemList[i].Name + "</option>";
            }
            $('#modelItemNameID').html($cloneRow);
            $('#modelItemNameID').val(InventoryItemMaster.ItemID).change();
            //$('#modelSiteCustomerMasterId').attr("disabled");
            $("#modelItemNameID").attr("disabled", "disabled");


            $("#modelMinQty").val(InventoryItemMaster.Min_stock);
            $("#modelAvailableQty").val(InventoryItemMaster.Available_stock);
        }

        if (data.EmpData != null) {
            var EmpDataList = [];
            debugger;
            EmpDataList = data.EmpData;
            var count = EmpDataList.length;
            var $cloneRow1 = "";
            $cloneRow1 = $cloneRow1 + "<option value=''>Select Employee...</option>";
            for (i = 0; i < EmpDataList.length; ++i) {
                $cloneRow1 = $cloneRow1 + "<option value='" + EmpDataList[i].EmpId + "'>" + EmpDataList[i].EmpName + "</option>";
            }

            $('#modelIssueToID').html($cloneRow1);
        }

    },

    onSuccessInventoryType: function (data) {
        if (data.status == "success") {
            if (data.listCategoryMaster != null) {
                InventoryItemMaster.bindCategoryOptionList(data.listCategoryMaster);
            }
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onError: function (data) {
        applicationSystem.onError(data);
    },
};