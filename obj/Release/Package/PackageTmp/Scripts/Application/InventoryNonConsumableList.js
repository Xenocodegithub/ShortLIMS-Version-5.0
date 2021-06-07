var InventoryNonConsumableList = {
    initialize: function () {
        InventoryNonConsumableList.attachEvents();
    },
    attachEvents: function () {
        $('#myDataTable tbody').on('click', 'a', function () {
            var ControlID = $(this).attr('id');
            var splitedID = ControlID.split('_');
            if (splitedID[0] == "MaintainanceDetails") {
                InventoryNonConsumableList.fatchInventoryScheduledDateDetails(splitedID[1], "Maintainance", "Completed");
            }
            else if (splitedID[0] == "CalibrationDetails") {
                InventoryNonConsumableList.fatchInventoryScheduledDateDetails(splitedID[1], "Calibration", "Completed");
            }

            //var listItemId = $(this).closest('tr').attr('id');
            //var datatableid = $(this).closest('tr').attr('datatableid');
            //var operation = $(this).attr('operation');
            //if (operation == "Edit") {
            //    SampleAndParameterDetails.fatchRequisitionSampleDetaildBySampleID(datatableid);
            //    $("#buttonSaveSampleMaster").css("display", "none");
            //    $("#buttonClearSampleMaster").css("display", "none");
            //    $("#buttonSaveParameterList").val("Update Sample");
            //    $('#collapse_Parameter_option').click();
            //}
            //if (operation == "Delete") {
            //    debugger;

            //    var dialog = BootstrapDialog.show({
            //        type: BootstrapDialog.TYPE_WARNING,
            //        title: 'Warning',
            //        message: "Are you sure, you wants to delete this sameple?",
            //        buttons: [
            //                    {
            //                        icon: 'glyphicon glyphicon-trash',
            //                        label: 'Delete',
            //                        cssClass: 'btn-warning',
            //                        action: function () {
            //                            SampleAndParameterDetails.deleteSampleDetails(datatableid);
            //                            dialog.close();
            //                            $("#" + listItemId).remove();
            //                            //$(this).closest('tr').remove();
            //                        }
            //                    },
            //                    {
            //                        label: 'Close',
            //                        cssClass: 'btn-warning',
            //                        action: function (dialogRef) {
            //                            dialogRef.close();
            //                        }
            //                    }
            //        ]
            //    });





            //}
            return false;
        });
    },

    fatchInventoryScheduledDateDetails: function (ID, Type, CompletionStatus) {
        debugger;
        var data = {
            "ID": "0"
        };
        data.ID = ID;
        data.Type = Type;
        data.CompletionStatus = CompletionStatus;
        ajaxRequest.makeRequest('/Inventory/GetScheduledDateDetails', "POST", data, InventoryNonConsumableList.onSuccessFatchInventoryScheduledDateDetails, InventoryNonConsumableList.onError);
    },

    bindRecords: function (data) {
        debugger;
        if (data != null) {
            var ParameterItems = [];
            ParameterItems = data;
            var count = ParameterItems.length;
            //$('#myParameterTableBody').html("");
            var $cloneRow = "";


            $cloneRow = "<!-- Your Page Content Here --><div class='row'><div class='col-md-12'><div class='box box-primary'><div class='box-header with-border' style='background-color: #3c8dbc;'>";
            $cloneRow = $cloneRow + "<h3 class='box-title' style='color:white;'> Non-Consumable Items List</h3></div><div class='table-responsive'>";
            $cloneRow = $cloneRow + "<table id='myDataTable' style='text-align:center;' class='table table-bordered table-striped display'>";
            $cloneRow = $cloneRow + "<thead><tr><th style='width:6%;'>#</th><th style='width:20%;'>Inventory Item Number</th><th style='width:20%;'>Last Completion Date</th><th style='width:20%;'>Next Date</th></tr></thead>";
            $cloneRow = $cloneRow + "<tbody id='myParameterTableBody'>";
            
            for (i = 0; i < ParameterItems.length; ++i) {
                var newdate = ParameterItems[i].CompletionDate;
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
                ReceivedDate = month + "/" + day + "/" + year;
                var ReceivedDate1 = "Inventory to be Renewed";
                if (ParameterItems[i].NextDueDate != null) {
                    var newdate1 = ParameterItems[i].NextDueDate;
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
                    ReceivedDate1 = month1 + "/" + day1 + "/" + year1;
                }
                
                

                $cloneRow = $cloneRow + '<tr id="' + i + '" datatableID = "' + ParameterItems[i].ParameterId + '" style="width:100%;">';
                $cloneRow = $cloneRow + "<td>" + (parseInt(i) + 1) + "</td>";
                //$cloneRow = $cloneRow + "<td>INV/" + ParameterItems[i].InventoryBasicItemDetailsID + "</td>";
                $cloneRow = $cloneRow + "<td>" + ParameterItems[i].InventoryBasicItemDetailsNum + "</td>";
                $cloneRow = $cloneRow + "<td>" + ReceivedDate + "</td>";
                $cloneRow = $cloneRow + "<td>" + ReceivedDate1 + "</td>";
                $cloneRow = $cloneRow + '</tr>';
            }
            $cloneRow = $cloneRow + "</tbody></table></div></div></div></div>";


            var dialog = BootstrapDialog.show({
                type: BootstrapDialog.TYPE_INFO,
                title: 'List',
                message: $cloneRow,
                buttons: [
                            {
                                label: 'Close',
                                cssClass: 'btn btn-primary',
                                action: function (dialogRef) {
                                    dialogRef.close();
                                }
                            }
                ]
            });


            //$('#myParameterTableBody').append($cloneRow);
        }
    },

    onSuccessFatchInventoryScheduledDateDetails: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.BasicItemsScheduledDateDetails != null) {
                InventoryNonConsumableList.bindRecords(data.BasicItemsScheduledDateDetails);
            }
        }
        else if (data.status == "info") {
            //applicationSystem.onError(data);
        }
        else if (data.status == "fail") {
            //applicationSystem.onError(data);
        }
    },


};