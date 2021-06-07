var InventoryTypeMaster = {
    initialize: function () {
        InventoryTypeMaster.attachEvents();
    },

    attachEvents: function () {
        $('#buttonSave').click(function () {
            if ($("#InventoryTypeMaster_Create").valid() == true) {
                InventoryTypeMaster.addInventoryTypeMaster();
                return false;
            }
        });

        $('#buttonUpdate').click(function () {
            if ($("#InventoryTypeMaster_Update").valid() == true) {
                InventoryTypeMaster.updateInventoryTypeMaster();
                return false;
            }
        });

        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var InventoryTypeId = $(this).closest('tr').attr('InventoryTypeId');
            var Description = $(this).closest('tr').attr('Description');
            var InventoryTypeName = $(this).closest('tr').attr('InventoryTypeName');            
            var operation = $(this).attr('operation');
            if (operation == "edit") {
                var url = applicationSystem.get_hostname() + "/InventoryTypeMaster/Edit/" + InventoryTypeId;
                window.location.href = url;
            }
            if (operation == "deactive") {
                var data = {
                    "ID": ""
                }
                data.ID = InventoryTypeId
                data.Description = Description
                data.Name = InventoryTypeName
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
                                        InventoryTypeMaster.activeInventoryTypeMaster(data);
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
                    "ID": ""
                }
                data.ID = InventoryTypeId
                data.Description = Description
                data.Name = InventoryTypeName
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
                                        InventoryTypeMaster.activeInventoryTypeMaster(data);
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



    addInventoryTypeMaster: function () {

        var InventoryTypeMasterData = InventoryTypeMaster.getInventoryTypeMaster();

        ajaxRequest.makeRequest('/InventoryTypeMaster/Create', "POST", InventoryTypeMasterData, InventoryTypeMaster.onSuccess, InventoryTypeMaster.onError);
    },

    updateInventoryTypeMaster: function () {
        var InventoryTypeMasterData = InventoryTypeMaster.getInventoryTypeMaster();
        ajaxRequest.makeRequest('/InventoryTypeMaster/Edit', "POST", InventoryTypeMasterData, InventoryTypeMaster.onSuccess, InventoryTypeMaster.onError);
    },

    activeInventoryTypeMaster: function (data) {

        ajaxRequest.makeRequest('/InventoryTypeMaster/Edit', "POST", data, InventoryTypeMaster.onSuccess, InventoryTypeMaster.onError);
    },

    getInventoryTypeMaster: function () {

        var data = {
            "ID": ""
        };
        data.ID = $('#ID').val();
        data.Name = $("#Name").val();
        data.Description = $("#Description").val();
        data.IsActive = $("#IsActive").val();
        return data;
    },

    setInventoryTypeMaster: function (data) {

    },

    onSuccess: function (data) {
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            var url = applicationSystem.get_hostname() + "/InventoryTypeMaster/List";
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
