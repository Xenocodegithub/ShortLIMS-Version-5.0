var InventoryVendorMaster = {
    initialize: function () {
        InventoryVendorMaster.attachEvents();
    },

    attachEvents: function () {
        $('#buttonSave').click(function () {
            if ($("#InventoryVendorMaster_Create").valid() == true) {
                InventoryVendorMaster.addInventoryVendorMaster();
                return false;
            }
        });

        $('#buttonUpdate').click(function () {
            if ($("#InventoryVendorMaster_Update").valid() == true) {
                InventoryVendorMaster.updateInventoryVendorMaster();
                return false;
            }
        });

        $('#CountryId').change(function () {
            InventoryVendorMaster.searchStateListByCountryId();
        });

        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var CityId = $(this).closest('tr').attr('CityId');
            var CityName = $(this).closest('tr').attr('name');
            var StateId = $(this).closest('tr').attr('StateId');
            var operation = $(this).attr('operation');
            if (operation == "edit") {
                var url = applicationSystem.get_hostname() + "/InventoryVendorMaster/Edit/" + CityId;
                window.location.href = url;
            }
            if (operation == "deactive") {
                var data = {
                    "CityId": ""
                }
                data.CityId = CityId
                data.StateId = StateId
                data.CityName = CityName
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
                                        InventoryVendorMaster.activeInventoryVendorMaster(data);
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
                    "CityId": ""
                }
                data.CityId = CityId
                data.StateId = StateId
                data.CityName = CityName
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
                                        InventoryVendorMaster.activeInventoryVendorMaster(data);
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
            return true;
            // PurchaseInvoice.getAmountTotal();

        });

    },

    addInventoryVendorMaster: function () {
        debugger;
        var InventoryVendorMasterData = InventoryVendorMaster.getInventoryVendorMaster();

        ajaxRequest.makeRequest('/InventoryVendorMaster/Create', "POST", InventoryVendorMasterData, InventoryVendorMaster.onSuccess, InventoryVendorMaster.onError);
    },

    updateInventoryVendorMaster: function () {
        var InventoryVendorMasterData = InventoryVendorMaster.getInventoryVendorMaster();
        ajaxRequest.makeRequest('/InventoryVendorMaster/Edit', "POST", InventoryVendorMasterData, InventoryVendorMaster.onSuccess, InventoryVendorMaster.onError);
    },

    activeInventoryVendorMaster: function (data) {

        ajaxRequest.makeRequest('/InventoryVendorMaster/Edit', "POST", data, InventoryVendorMaster.onSuccess, InventoryVendorMaster.onError);
    },

    getInventoryVendorMaster: function () {
        debugger;
        var data = {
            "ID": ""
        };
        data.ID = $("#ID").val();
        data.Name = $('#Name').val();
        data.Address = $("#Address").val();
        data.MobileNumber = $("#MobileNumber").val();
        data.PhoneNumber = $("#PhoneNumber").val();
        data.EmailAddress = $("#EmailAddress").val();
        data.IsActive = true;
        //if ($("#IsActive_Value").is(':checked')) {
        //    data.IsActive = true;
        //}
        //else {
        //    data.IsActive = false;
        //}
        return data;
    },

    onSuccess: function (data) {
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            var url = applicationSystem.get_hostname() + "/InventoryVendorMaster/List";
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
