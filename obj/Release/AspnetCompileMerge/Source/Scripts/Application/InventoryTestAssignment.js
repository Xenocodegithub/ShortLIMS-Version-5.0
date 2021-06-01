var InventoryTestAssignment = {
    Object: [],

    InventiryListObject: [],
    InventoryOptionList: null,
    AssignObject: [],
    AssignOptionList: null,
    AssignInitialObject: [],

    initialize: function () {

        InventoryTestAssignment.attachEvents();
        debugger;
        var InventoryDtoObject = $('#InventoryList').val();
        if (InventoryDtoObject != null) {
            InventoryTestAssignment.InventiryListObject = $.parseJSON(InventoryDtoObject);
            InventoryTestAssignment.bindInventoryOptionList();
            $('#InventoryTypeIDHeader').html(InventoryTestAssignment.InventoryOptionList);
        }


    },

    attachEvents: function () {

        $('#buttonSearch').click(function () {
            InventoryTestAssignment.addAssignmentSearchRequest();
        });
        $("#buttonReset").click(function () {
            $("select").val(-1).change();
            $("select").val(-1);
            $("input[type = 'text']").val("");
            $("#mytableBody").html("");
        });
        $('#ReqSampleDetailsId').change(function () {



            InventoryTestAssignment.addTestAssignmentDetails();



            //  TestAssignment.setTestAssignmentDetails();
            return false;
        });

        $('#myTable tbody').on('click', 'a', function () {
            debugger;
            //  var listItemId = $(this).closest('tr').attr('id');
            //var datatableid = $(this).closest('tr').attr('ReqDetailsId');
            var _trID = $(this).closest('tr').attr('id');
            var _status = $(this).closest('tr').attr('reqstatus');
            //var controlID = $(this).attr('id');
            if (_status < 9) {
                $("#InventoryTypeID_" + _trID).val(-1);
                $("#InventorySerialID_" + _trID).val(-1);

            }
            //$("#testerID_" + _trID).val(-1);
            return false;
        });

        $('#myTable').on('change', 'select', function () {
            debugger;
            //for (var i = 0; i < $("#myTable tbody").length; ++i) {
            //    $('#LabID_' + i).val($('#LabIDHeader').val());
            var operation = $(this).attr("operation");
            if (operation == "inventoryheader") {
                if ($('#InventoryTypeIDHeader').val() > 0) {
                    InventoryTestAssignment.FatchBasicItemsDetails();
                }

                $("#mytableBody tr").each(function () {

                    var _rowID = $(this).attr("id");
                    var _status = $(this).attr("reqstatus");
                    $('#' + _rowID).attr("ReqDetailsId");
                    if (_status < 9) {
                        if ($('#InventoryTypeIDHeader').val() > 0) {

                            $("#InventoryTypeID_" + _rowID).val($('#InventoryTypeIDHeader').val())
                        }
                        else {
                            $("#InventoryTypeID_" + _rowID).val($('#InventoryTypeIDHeader').val())
                        }
                    }

                    ;

                });

            }




            if (operation == "inventory") {

                var _rowID = $(this).closest('tr').attr('id');
                var _status = $(this).attr("reqstatus");
                var id = $(this).val();
                var data = {}

                data.id = id;
                data.RowId = _rowID;
                InventoryTestAssignment.FatchBasicItemsDetailsRowWise(data);

            }
            debugger;
            if (operation == "serialheader") {

                if ($('#InventorySerialHeader').val() > 0) {

                    var data = {}
                    data.id = $('#InventorySerialHeader').val();
                    InventoryTestAssignment.CheckSerialStatus(data);
                }
                // InventoryTestAssignment.FatchBasicItemsDetails();


                $("#mytableBody tr").each(function () {

                    var _rowID = $(this).attr("id");
                    var _status = $(this).attr("reqstatus");
                    $('#' + _rowID).attr("ReqDetailsId");
                    if (_status < 9) {
                        if ($('#InventorySerialHeader').val() > 0) {

                            $("#InventorySerialID_" + _rowID).val($('#InventorySerialHeader').val())
                        }
                        else {
                            $("#InventorySerialID_" + _rowID).val($('#InventorySerialHeader').val())
                        }
                    }

                    ;

                });
            }
        });
        $('#myDataTable tbody').on('click', 'a', function () {
            debugger;
            var listItemId = $(this).closest('tr').attr('id');
            // var datatableid = $(this).closest('tr').attr('datatableid');
            var ReqId = $(this).closest('tr').attr('ReqId');
            //var SplitedProformaID = ProformaID.split("_");
            //if (SplitedProformaID[0] == "ReqId") {

            var url = applicationSystem.get_hostname() + "/TestAssignment/ListInventoryAssignment/" + ReqId;
            window.location.href = url;
            //}
            return false;
        });
        $('#buttonSaveTessAssignment').click(function () {

            InventoryTestAssignment.addTestAssignmentListDetails();
            return false;
        });

    },
    addTestAssignmentListDetails: function () {
        debugger;
        var addTestAssignmentListData = InventoryTestAssignment.getTestAssignemtnListDetailsData();
        if (addTestAssignmentListData.ListTestAssignementDetails.length > 0) {
            ajaxRequest.makeRequest('/TestAssignment/UpdateInventoryAssignment', "POST", addTestAssignmentListData, InventoryTestAssignment.onSuccess, InventoryTestAssignment.onError);
        }
        else {
            var data = {
                "message": "Please Select Record !"
            }
            applicationSystem.onInfo(data);
        }
    },
    getTestAssignemtnListDetailsData: function () {
        debugger;
        var data = {}

        data.ListTestAssignementDetails = InventoryTestAssignment.getTestListDetails();
        return data;
    },
    getTestListDetails: function () {


        var SPPDetailsArray = [];
        $("#mytableBody tr").each(function () {
            var _rowID = $(this).attr("id");
            var _status = $(this).attr("reqstatus");
            if (_status < 9) {
                var SPPDetails = {
                    "ReqDetailsId": "",

                };
                SPPDetails.ReqDetailsId = $('#' + _rowID).attr("ReqDetailsId");
                SPPDetails.InventoryItemMastertId = $("#InventoryTypeID_" + _rowID).val();
                SPPDetails.AssetSerialNo = $("#InventorySerialID_" + _rowID).val();

                SPPDetailsArray.push(SPPDetails);
            }
        });
        debugger;
        return SPPDetailsArray;

    },

    CheckSerialStatus: function (data) {

        ajaxRequest.makeRequest('/TestAssignment/CheckSerialStatus/', "POST", data, InventoryTestAssignment.onSuccessCheckStatus, InventoryTestAssignment.onError);
    },
    FatchBasicItemsDetails: function () {
        debugger;
        var data = {
            id: ""

        }
        data.id = $('#InventoryTypeIDHeader').val();
        ajaxRequest.makeRequest('/TestAssignment/FatchBasicItemsDetails/', "POST", data, InventoryTestAssignment.onSuccessBasicDetails, InventoryTestAssignment.onError);
    },
    FatchBasicItemsDetailsRowWise: function (data) {


        ajaxRequest.makeRequest('/TestAssignment/FatchBasicItemsDetails/', "POST", data, InventoryTestAssignment.onSuccessBasicDetailsRowWise, InventoryTestAssignment.onError);
    },
    addAssignmentSearchRequest: function () {
        debugger;
        var Data = InventoryTestAssignment.getAssignmentSearchRequest();

        ajaxRequest.makeRequest('/TestAssignment/ListInventory', "POST", Data, InventoryTestAssignment.onSuccessTest, InventoryTestAssignment.onError);
    },
    addTestAssignmentDetails: function () {
        debugger;
        var addTestAssignmentData = InventoryTestAssignment.getRequisitionDetailsData();

        ajaxRequest.makeRequest('/TestAssignment/ListRequisitionDetails_TestAssignment', "POST", addTestAssignmentData, InventoryTestAssignment.onSuccessListRequisitionDetails_TestAssignment, InventoryTestAssignment.onError);
    },
    getRequisitionDetailsData: function () {
        debugger;
        var data = {
            "ReqSampleDetailsId": ""
        };
        data.ReqSampleId = $("#ReqSampleDetailsId").val();

        return data;
    },
    onSuccessBasicDetails: function (data) {
        if (data.status == "success") {



            if (data.BasicItemsDetails != null) {
                var BasicItemList = [];
                BasicItemList = data.BasicItemsDetails;
                var count = BasicItemList.length;
                var $cloneRow = "";
                $cloneRow = $cloneRow + "<option value=''>Select Serial No...</option>";
                for (i = 0; i < BasicItemList.length; ++i) {
                    $cloneRow = $cloneRow + "<option value='" + BasicItemList[i].ID + "' title = '" + BasicItemList[i].ID + "'>INV/" + BasicItemList[i].ID + "</option>";
                }
                $cloneRow;
                debugger;
                //var operation = $(this).attr("operation");
                //if (operation == "inventoryheader") {
                $('#InventorySerialHeader').html($cloneRow);

                $("#mytableBody tr").each(function () {
                    debugger;

                    var _rowID = $(this).attr("id");
                    var operation = $(this).attr("operation");
                    var _status = $(this).attr("reqstatus");
                    $('#' + _rowID).attr("ReqDetailsId");
                    if (_status < 9) {
                        if ($('#InventoryTypeIDHeader').val() > 0) {

                            $("#InventorySerialID_" + _rowID).html($cloneRow)
                        }

                    }

                    ;

                });
                //}
            }

        }

    },

    onSuccessBasicDetailsRowWise: function (data) {
        if (data.status == "success") {


            if (data.BasicItemsDetails != null) {
                var BasicItemList = [];
                BasicItemList = data.BasicItemsDetails;
                var count = BasicItemList.length;
                var $cloneRow = "";
                $cloneRow = $cloneRow + "<option value=''>Select Serial No...</option>";
                for (i = 0; i < BasicItemList.length; ++i) {
                    $cloneRow = $cloneRow + "<option value='" + BasicItemList[i].ID + "' title = '" + BasicItemList[i].ID + "'>INV/" + BasicItemList[i].ID + "</option>";
                }
                $cloneRow;

                var id = data.id;
                $("#InventorySerialID_" + id).html($cloneRow);

                //  $("#InventorySerialID_" + id).val($("#InventoryTypeID_" + id).val());
            }

        }

    },
    onSuccessCheckStatus: function (data) {
        if (data.status == "success") {
            debugger;


            if (data.CheckAssignmentStatus == "True") {
                var dataMessage = {};
                dataMessage.message = "Equipment is already assigned!";
                applicationSystem.onError(dataMessage);
                return false;
            }
            if (data.CheckStatus == "True") {
                var dataMessage = {};
                dataMessage.message = "Equipment in under maintenance!";
                applicationSystem.onError(data);
                return false;
            }

            //  $("#InventorySerialID_" + id).val($("#InventoryTypeID_" + id).val());


        }

    },
    onSuccessListRequisitionDetails_TestAssignment: function (data) {
        debugger;
        if (data.status == "success") {

            InventoryTestAssignment.seRequisitionDetailsRequest(data.TestAssignmentMasterData);

            InventoryTestAssignment.seAssignMasterRequest(data.TestAssignmentMasterData);
            if ($('#CheckAssigned').val() == "True") {
                InventoryTestAssignment.setserialNumberRowWise();
            }
            debugger;
            InventoryTestAssignment.seAssignMasterRequest(data.TestAssignmentMasterData);
        }
    },
    getAssignmentSearchRequest: function () {
        debugger;
        var data = {
            "CustomerId": ""
        };
        data.CustomerId = $("#CustomerMasterId").val();
        //  data.SampleId = $("#SampleId").val();
        // data.ReqNumber = $('#ReqNumber').val();
        if ($('#ReqNumber').val() > 0) {
            var ReqNumber1 = $("#ReqNumber option:selected").text();
            data.ReqNumber = ReqNumber1;
        }
        else {
            data.ReqNumber = '';
        }
        if ($("#StartDate").val() != null) {
             data.StartDate = new Date($("#StartDate").val());
            //data.StartDate = new Date(
            //                         applicationSystem.convertionOfDateFormat(
            //                         $("#StartDate").val(),
            //                         applicationSystem.aspectedFormat,
            //                         applicationSystem.currentServerFormat,
            //                         applicationSystem.aspectedServerSeparator,
            //                         applicationSystem.currentServerSeparator
            //                     ));
        }

        if ($("#EndDate").val() != null) {
             data.EndDate = new Date($("#EndDate").val());
            //data.EndDate = new Date(
            //                         applicationSystem.convertionOfDateFormat(
            //                         $("#EndDate").val(),
            //                         applicationSystem.aspectedFormat,
            //                         applicationSystem.currentServerFormat,
            //                         applicationSystem.aspectedServerSeparator,
            //                         applicationSystem.currentServerSeparator
            //                     ));
        }
        return data;
    },


    onSuccessTest: function (data) {
        debugger;
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
        else if (data.status == "info") {
            applicationSystem.onInfo(data);
        }
        else {
            InventoryTestAssignment.setAssignmentDetails(data);
            //applicationSystem.onSuccess(data);
        }
    },

    onSuccess: function (data) {
        debugger;
        if (data.status == "fail") {
            applicationSystem.onError(data);
        }
        else if (data.status == "info") {
            applicationSystem.onInfo(data);
        }
        else {
            applicationSystem.onSuccess(data);
            InventoryTestAssignment.seRequisitionDetailsRequest(data.TestAssignmentMasterData);

            //    InventoryTestAssignment.seAssignMasterRequest(data.TestAssignmentMasterData);
            $('#InventoryTypeIDHeader').val(-1);
            $('#InventorySerialHeader').val(-1);

        }
    },

    setAssignmentDetailsIntial: function () {
        debugger;
        if (InventoryTestAssignment.AssignInitialObject != null) {
            var requestItems = [];
            requestItems = InventoryTestAssignment.AssignInitialObject;
            var count = requestItems.length;
            $('#myDataTableBody').html("");

            var iCount = 1;
            var _timeLimit = 1;

            for (i = 0; i < requestItems.length; ++i) {
                var $cloneRow = "";
                var ReceivedDate = "";
                var ReportDate = "";
                if (requestItems[i].ReceivedDate != null) {
                    var newdate = requestItems[i].ReceivedDate;
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
                }
                if (requestItems[i].ReportDate != null) {
                    var newdate = requestItems[i].ReportDate;
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
                    ReportDate = month + "/" + day + "/" + year;
                }

                if (requestItems[i].ReqStatus == 8) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #00FFFF;">';
                }
                else if (requestItems[i].ReqStatus == 9) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #90EE90;">';
                }
                else if (requestItems[i].ReqStatus == 10) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #00FF00;">';
                }
                else if (requestItems[i].ReqStatus == 11) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #DAA520;">';
                }
                else if (requestItems[i].ReqStatus == 12) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #FFFF00;">';
                }
                else if (requestItems[i].ReqStatus == 13) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #FA8072;">';
                }
                else {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;">';
                }
                $cloneRow = $cloneRow + '<td>' + parseInt(iCount) + '</td>';

                if (requestItems[i].ReqStatus == 13) {

                    $cloneRow = $cloneRow + '<td>' + requestItems[i].PersonName + '</a></td>';
                }
                else {

                    $cloneRow = $cloneRow + '<td><a href="#" id="ReqId_' + i + '" name="ReqId_' + i + '">' + requestItems[i].PersonName + '</a></td>';
                }
                $cloneRow = $cloneRow + '<td>' + requestItems[i].ReqNumber + '</td>';
                
                $cloneRow = $cloneRow + '<td>' + ReceivedDate + '</td>';
                //$cloneRow = $cloneRow + '<td>' +  + '</td>';
                $cloneRow = $cloneRow + '<td>' + requestItems[i].TimeLimit + '</td>';
                //$cloneRow = $cloneRow + '<td>' + requestItems[i].TotalAmount + '</td>';
                $cloneRow = $cloneRow + '</tr>';
                ++iCount;
                $('#myDataTableBody').append($cloneRow);
            }


        }
    },
    setAssignmentDetails: function (data) {
        debugger;
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            var count = requestItems.length;
            $('#myDataTableBody').html("");

            var iCount = 1;
            var _timeLimit = 1;

            for (i = 0; i < requestItems.length; ++i) {
                var $cloneRow = "";
                var ReceivedDate = "";
                var ReportDate = "";
                if (requestItems[i].ReceivedDate != null) {
                    var newdate = requestItems[i].ReceivedDate;
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
                }
                if (requestItems[i].ReportDate != null) {
                    var newdate = requestItems[i].ReportDate;
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
                    ReportDate = month + "/" + day + "/" + year;
                }

                if (requestItems[i].RequestMasterDTO.ReqStatus == 8) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #00FFFF;">';
                }
                else if (requestItems[i].RequestMasterDTO.ReqStatus == 9) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #90EE90;">';
                }
                else if (requestItems[i].RequestMasterDTO.ReqStatus == 10) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #00FF00;">';
                }
                else if (requestItems[i].RequestMasterDTO.ReqStatus == 11) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #DAA520;">';
                }
                else if (requestItems[i].RequestMasterDTO.ReqStatus == 12) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #FFFF00;">';
                }
                else if (requestItems[i].RequestMasterDTO.ReqStatus == 13) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;background-color: #FA8072;">';
                }
                else {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqId=' + requestItems[i].ReqId + ' style="width:100%;">';
                }
                $cloneRow = $cloneRow + '<td>' + parseInt(iCount) + '</td>';

                if (requestItems[i].RequestMasterDTO.ReqStatus == 13) {

                    $cloneRow = $cloneRow + '<td>' + requestItems[i].PersonName + '</a></td>';
                }
                else {

                    $cloneRow = $cloneRow + '<td><a href="#" id="ReqId_' + i + '" name="ReqId_' + i + '">' + requestItems[i].PersonName + '</a></td>';
                }
                $cloneRow = $cloneRow + '<td>' + requestItems[i].ReqNumber + '</td>';
                
                $cloneRow = $cloneRow + '<td>' + ReceivedDate + '</td>';
                //$cloneRow = $cloneRow + '<td>' +  + '</td>';
                $cloneRow = $cloneRow + '<td>' + requestItems[i].TimeLimit + '</td>';
                //$cloneRow = $cloneRow + '<td>' + requestItems[i].TotalAmount + '</td>';
                $cloneRow = $cloneRow + '</tr>';
                ++iCount;
                $('#myDataTableBody').append($cloneRow);
            }

        }
    },

    seRequisitionDetailsRequest: function (data) {

        if (data != null) {
            debugger;
            var requestItems = [];
            requestItems = data;
            var count = requestItems.length;
            $('#mytableBody').html("");

            var isrNo = 1;
            for (i = 0; i < requestItems.length; ++i) {
                var $cloneRow = "";

                isrNo = i + 1;
                if (requestItems[i].TestAssignmentDTO.ReqStatus == 8) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;background-color: #00FFFF;">';
                }
                else if (requestItems[i].TestAssignmentDTO.ReqStatus == 9) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;background-color: #90EE90;">';
                }
                else if (requestItems[i].TestAssignmentDTO.ReqStatus == 10) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;background-color: #00FF00;">';
                }
                else if (requestItems[i].TestAssignmentDTO.ReqStatus == 11) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '"style="width:100%;background-color: #DAA520;">';
                }
                else if (requestItems[i].TestAssignmentDTO.ReqStatus == 12) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;background-color: #FFFF00;">';
                }
                else if (requestItems[i].TestAssignmentDTO.ReqStatus == 13) {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;background-color: #FA8072;">';
                }
                else {
                    $cloneRow = $cloneRow + '<tr id="' + i + '" ReqStatus=' + requestItems[i].TestAssignmentDTO.ReqStatus + ' ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;">';
                }
                //   $cloneRow = $cloneRow + '<tr id="' + i + '" ReqDetailsId = "' + requestItems[i].ReqDetailsId + '" style="width:100%;">';
                $cloneRow = $cloneRow + '<td>' + parseInt(isrNo) + '</td>';
                //$cloneRow = $cloneRow + '<td>' + requestItems[i].ReqDetailsId + '</td>';           
                $cloneRow = $cloneRow + '<td>' + requestItems[i].ParameterName + '</td>';
                $cloneRow = $cloneRow + '<td><select operation="inventory" id="InventoryTypeID_' + i + '" name="InventoryTypeID_' + i + '" class="form-control formatDropdownOfTable" style="width: 100%;"></select></td>';
                $cloneRow = $cloneRow + '<td><select operation="serial" id="InventorySerialID_' + i + '" name="InventorySerialID_' + i + '" class="form-control formatDropdownOfTable" style="width: 100%;"></select></td>';
                //$cloneRow = $cloneRow + '<td><select id="veriferID_' + i + '" name="veriferID_' + i + '" class="form-control formatDropdownOfTable" style="width: 100%;"></select></td>';
                if (requestItems[i].TestAssignmentDTO.ReqStatus < 9) {
                    $cloneRow = $cloneRow + '<td id="a_' + i + '"> <a  href="#"><span class="glyphicon glyphicon-refresh"></span></a></td>';
                }
                else {
                    $cloneRow = $cloneRow + '<td></td>';
                }
                $cloneRow = $cloneRow + '</tr>';

                $('#mytableBody').append($cloneRow);

                $('#InventoryTypeID_' + i).html(InventoryTestAssignment.InventoryOptionList);
               
                //$('#veriferID_' + i).html(InventoryTestAssignment.VerifierOptionList);
                //$('#LabID_' + i).html(InventoryTestAssignment.LabOptionList);
            }
            $('#InventoryTypeIDHeader').val(-1);


        }
    },
    setserialNumberRowWise: function (data) {

        $("#mytableBody tr").each(function () {

            var _rowID = $(this).attr("id");

            var _status = $(this).attr("reqstatus");
            var id = $("#InventoryTypeID_" + _rowID).val();
            var data = {}
            data.id = id;
            data.RowId = _rowID;
            InventoryTestAssignment.FatchBasicItemsDetailsRowWise(data);


        });
    },
    seAssignMasterRequest: function (data) {
        if (data != null) {
            debugger;
            var requestItems = [];
            requestItems = data;
            var count = requestItems.length;
            //$('#mytableBody').html("");
            var isrNo = 1;
            for (i = 0; i < requestItems.length; ++i) {

                $('#InventoryTypeID_' + i).val(data[i].InventoryItemMastertId);

                if (parseFloat(data[i].InventoryItemMastertId) > 0) {
                    $("#CheckAssigned").val("True");
                }
                //  $('#InventorySerialID_' + i).val(data[i].AssetSerialNo);

            }
        }

    },

    bindInventoryOptionList: function () {
        debugger;
        if (InventoryTestAssignment.InventiryListObject != null) {
            var InventoryList = [];
            InventoryList = InventoryTestAssignment.InventiryListObject;
            var count = InventoryList.length;
            var $cloneRow = "";
            $cloneRow = $cloneRow + "<option value=''>Select Item...</option>";
            for (i = 0; i < InventoryList.length; ++i) {
                $cloneRow = $cloneRow + "<option value='" + InventoryList[i].ID + "' title = '" + InventoryList[i].Name + "'>" + InventoryList[i].Name + "</option>";
            }
            InventoryTestAssignment.InventoryOptionList = $cloneRow;
        }
    },
    onError: function (data) {
        applicationSystem.onError(data);
    },

};
