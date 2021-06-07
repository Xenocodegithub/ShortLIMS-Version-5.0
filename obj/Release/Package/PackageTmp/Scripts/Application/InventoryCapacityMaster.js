var InventoryCapacityMaster = {
    initialize: function () {
        InventoryCapacityMaster.attachEvents();
    },

    attachEvents: function () {
        $('#buttonSave').click(function () {
            if ($("#InventoryCapacityMaster_Create").valid() == true) {
                InventoryCapacityMaster.addInventoryCapacityMaster();
                return false;
            }
        });

        $('#buttonUpdate').click(function () {
            if ($("#InventoryCapacityMaster_Update").valid() == true) {
                InventoryCapacityMaster.updateInventoryCapacityMaster();
                return false;
            }
        });

        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var InventoryCapacityMasterId = $(this).closest('tr').attr('InventoryCapacityMasterId');
            var Description = $(this).closest('tr').attr('Description');
            var InventoryTypeName = $(this).closest('tr').attr('InventoryTypeName');            
            var operation = $(this).attr('operation');
            if (operation == "edit") {
                var url = applicationSystem.get_hostname() + "/Inventory/CapacityEdit?ID=" + InventoryCapacityMasterId;
                window.location.href = url;
            }
            if (operation == "deactive") {
                var data = {
                    "InventoryCapacityMasterId": ""
                }
                data.InventoryCapacityMasterId = InventoryCapacityMasterId            
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
                                        InventoryCapacityMaster.activeInventoryCapacityMaster(data);
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
                return false;
            }
            if (operation == "active") {
                var data = {
                    "InventoryCapacityMasterId": ""
                }
                data.InventoryCapacityMasterId = InventoryCapacityMasterId    
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
                                        InventoryCapacityMaster.activeInventoryCapacityMaster(data);
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
                return false;
            }
            return true;
            // PurchaseInvoice.getAmountTotal();

        });
    },
    addInventoryCapacityMaster: function () {
        var InventoryCapacityMasterData = InventoryCapacityMaster.getInventoryCapacityMaster();
        ajaxRequest.makeRequest('/InventoryCapacityMaster/Create', "POST", InventoryCapacityMasterData, InventoryCapacityMaster.onSuccess, InventoryCapacityMaster.onError);
    },
    updateInventoryCapacityMaster: function () {
        var InventoryCapacityMasterData = InventoryCapacityMaster.getInventoryCapacityMaster();
        ajaxRequest.makeRequest('/InventoryCapacityMaster/Edit', "POST", InventoryCapacityMasterData, InventoryCapacityMaster.onSuccess, InventoryCapacityMaster.onError);
    },
    activeInventoryCapacityMaster: function (data) {
        ajaxRequest.makeRequest('/InventoryCapacityMaster/Activate', "POST", data, InventoryCapacityMaster.onSuccess, InventoryCapacityMaster.onError);
    },
    getInventoryCapacityMaster: function () {
        var data = {
            "InventoryCapacityMasterId": ""
        };
        data.InventoryCapacityMasterId = $('#InventoryCapacityMasterId').val();
        data.Capacity = $("#Capacity").val();
        data.Description = $("#Description").val();
        data.IsActive = $("#IsActive").val();
        return data;
    },
    setInventoryCapacityMaster: function (data) {

    },
    onSuccess: function (data) {
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            var url = applicationSystem.get_hostname() + "/Inventory/CapacityList";
            window.location.href = url;
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },
    onError: function (data) {
        applicationSystem.onError(data);
    },

};
