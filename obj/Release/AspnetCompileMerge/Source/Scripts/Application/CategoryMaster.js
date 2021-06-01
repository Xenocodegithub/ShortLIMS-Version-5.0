var CategoryMaster = {
    initialize: function () {
        CategoryMaster.attachEvents();
    },

    attachEvents: function () {
        $('#buttonSave').click(function () {
            if ($("#CategoryMaster_Create").valid() == true) {
                debugger
                CategoryMaster.addCategoryMaster();
                return false;
            }

        });

        $('#buttonUpdate').click(function () {
            debugger;
            if ($("#CategoryMaster_Update").valid() == true) {
                CategoryMaster.updateCategoryMaster();
                return false;
            }
        });
        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            var CategoryId = $(this).closest('tr').attr('CategoryId');
            var InventoryTypeId = $(this).closest('tr').attr('InventoryTypeId');
            var CategoryName = $(this).closest('tr').attr('CategoryName');
            var operation = $(this).attr('operation');
            if (operation == "edit") {
                var url = applicationSystem.get_hostname() + "/Inventory/EditCategory?ID=" + CategoryId;
                window.location.href = url;
            }
            if (operation == "deactive") {
                var data = {
                    "ID": ""
                }
                data.ID = CategoryId
                data.InventoryTypeId = InventoryTypeId
                data.Name = CategoryName
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
                                        CategoryMaster.activeCategoryMaster(data);
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
                data.ID = CategoryId
                data.InventoryTypeId = InventoryTypeId
                data.Name = CategoryName
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
                                        CategoryMaster.activeCategoryMaster(data);
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
    validate: function () {
    },
    addCategoryMaster: function () {
        var CategoryMasterData = CategoryMaster.getCategoryMaster();
        ajaxRequest.makeRequest('/CategoryMaster/Create', "POST", CategoryMasterData, CategoryMaster.onSuccess);
    },
    updateCategoryMaster: function () {
        var CategoryMasterData = CategoryMaster.getCategoryMaster();
        ajaxRequest.makeRequest('/CategoryMaster/Edit', "POST", CategoryMasterData, CategoryMaster.onSuccess, CategoryMaster.onError);
    },
    activeCategoryMaster: function (data) {

        ajaxRequest.makeRequest('/CategoryMaster/Edit', "POST", data, CategoryMaster.onSuccess, CategoryMaster.onError);
    },
    getCategoryMaster: function () {

        var data = {
            "ID": ""
        };
        data.ID = $("#ID").val();
        data.InventoryTypeID = $('#InventoryTypeID').val();
        data.CategoryName = $("#CategoryName").val();
        data.IsActive = $("#IsActive").val();
        return data;
    },

    setCategoryMaster: function (data) {

    },

    onSuccess: function (data) {
        if (data.status == "success") {
            applicationSystem.onSuccess(data);
            var url = applicationSystem.get_hostname() + "/Inventory/ListCategory";
            window.location.href = url;
        }
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
    },

    //onError: function (data) {
    //    applicationSystem.onError(data);
    //},

};
