var Home = {

    TopMessages: [],
    initialize: function () {
        Home.attachEvents();

        //Home.GetUserMenu();
        //alert('d');
        //Home.GetSetChildMenuName();

    },

    attachEvents: function () {
        $('#buttonSearchTypeWise').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;
            
            if ($("#StartDateTypeWise").val() != "") {
                data.StartDate = new Date($("#StartDateTypeWise").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateTypeWise").val() != "") {
                data.EndDate = new Date($("#EndDateTypeWise").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleTypeWise', "POST", data, Home.onSuccessSearchTypeWise, Home.onError);
            }
            else {
                alert("Please enter search criteria !");
                
            }
        });

        $('#buttonSearchWO').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWO").val() != "") {
                data.StartDate = new Date($("#StartDateWO").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWO").val() != "") {
                data.EndDate = new Date($("#EndDateWO").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWO', "POST", data, Home.onSuccessSearchWO, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchWOB').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWOB").val() != "") {
                data.StartDate = new Date($("#StartDateWOB").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWOB").val() != "") {
                data.EndDate = new Date($("#EndDateWOB").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWOB', "POST", data, Home.onSuccessSearchWOB, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchWOH').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWOH").val() != "") {
                data.StartDate = new Date($("#StartDateWOH").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWOH").val() != "") {
                data.EndDate = new Date($("#EndDateWOH").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWOH', "POST", data, Home.onSuccessSearchWOH, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchPOH').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDatePOH").val() != "") {
                data.StartDate = new Date($("#StartDatePOH").val());
                data.SearchStatus = true;
            }
            if ($("#EndDatePOH").val() != "") {
                data.EndDate = new Date($("#EndDatePOH").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalPOH', "POST", data, Home.onSuccessSearchPOH, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchWOExeS').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWOExeS").val() != "") {
                data.StartDate = new Date($("#StartDateWOExeS").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWOExeS").val() != "") {
                data.EndDate = new Date($("#EndDateWOExeS").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWOExeS', "POST", data, Home.onSuccessSearchWOExeS, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleMonthWise').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSampleMonthWise").val() != "") {
                data.StartDate = new Date($("#StartDateSampleMonthWise").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSampleMonthWise").val() != "") {
                data.EndDate = new Date($("#EndDateSampleMonthWise").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleMonthE', "POST", data, Home.onSuccessSearchTotalSampleMonthE, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchCollected').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateCollected").val() != "") {
                data.StartDate = new Date($("#StartDateCollected").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateCollected").val() != "") {
                data.EndDate = new Date($("#EndDateCollected").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleCollected', "POST", data, Home.onSuccessSearchCollected, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleMonthWise').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateCollected").val() != "") {
                data.StartDate = new Date($("#StartDateCollected").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateCollected").val() != "") {
                data.EndDate = new Date($("#EndDateCollected").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleCollected', "POST", data, Home.onSuccessSearchTotalSampleMonthE, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchWOCompS').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWOCompS").val() != "") {
                data.StartDate = new Date($("#StartDateWOCompS").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWOCompS").val() != "") {
                data.EndDate = new Date($("#EndDateWOCompS").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWOCompS', "POST", data, Home.onSuccessSearchWOCompS, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchWOS').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateWOS").val() != "") {
                data.StartDate = new Date($("#StartDateWOS").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateWOS").val() != "") {
                data.EndDate = new Date($("#EndDateWOS").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalWOS', "POST", data, Home.onSuccessSearchWOS, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSample').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSample").val() != "") {
                data.StartDate = new Date($("#StartDateSample").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSample").val() != "") {
                data.EndDate = new Date($("#EndDateSample").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSample', "POST", data, Home.onSuccessSearchSample, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchPO').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDatePO").val() != "") {
                data.StartDate = new Date($("#StartDatePO").val());
                data.SearchStatus = true;
            }
            if ($("#EndDatePO").val() != "") {
                data.EndDate = new Date($("#EndDatePO").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalPO', "POST", data, Home.onSuccessSearchPO, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchPOMonth').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDatePOMonth").val() != "") {
                data.StartDate = new Date($("#StartDatePOMonth").val());
                data.SearchStatus = true;
            }
            if ($("#EndDatePOMonth").val() != "") {
                data.EndDate = new Date($("#EndDatePOMonth").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalPOMonth', "POST", data, Home.onSuccessSearchPOMonth, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchTotalRevenue').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateTotalRevenue").val() != "") {
                data.StartDate = new Date($("#StartDateTotalRevenue").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateTotalRevenue").val() != "") {
                data.EndDate = new Date($("#EndDateTotalRevenue").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalPO', "POST", data, Home.onSuccessSearchTotalRevenue, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchTotalEnquiry').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateTotalEnquiry").val() != "") {
                data.StartDate = new Date($("#StartDateTotalEnquiry").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateTotalEnquiry").val() != "") {
                data.EndDate = new Date($("#EndDateTotalEnquiry").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalEnquiry', "POST", data, Home.onSuccessSearchTotalEnquiry, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchQuotSent').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateQuotSent").val() != "") {
                data.StartDate = new Date($("#StartDateQuotSent").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateQuotSent").val() != "") {
                data.EndDate = new Date($("#EndDateQuotSent").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalQuotSent', "POST", data, Home.onSuccessSearchTotalQuotSent, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleP').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSampleP").val() != "") {
                data.StartDate = new Date($("#StartDateSampleP").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSampleP").val() != "") {
                data.EndDate = new Date($("#EndDateSampleP").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleTypeWiseP', "POST", data, Home.onSuccessSearchTotalSampleTypeWiseP, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleApproved').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSampleApproved").val() != "") {
                data.StartDate = new Date($("#StartDateSampleApproved").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSampleApproved").val() != "") {
                data.EndDate = new Date($("#EndDateSampleApproved").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleTypeWiseP', "POST", data, Home.onSuccessSearchTotalSampleApproved, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleMonthP').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSampleMonthP").val() != "") {
                data.StartDate = new Date($("#StartDateSampleMonthP").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSampleMonthP").val() != "") {
                data.EndDate = new Date($("#EndDateSampleMonthP").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleMonthWiseP', "POST", data, Home.onSuccessSearchTotalSampleMonthP, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchSampleTested').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateSampleTested").val() != "") {
                data.StartDate = new Date($("#StartDateSampleTested").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateSampleTested").val() != "") {
                data.EndDate = new Date($("#EndDateSampleTested").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleTestedP', "POST", data, Home.onSuccessSearchTotalSampleTestedP, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
        $('#buttonSearchTestedMonth').click(function () {
            var data = {
                "StartDate": ""
            };
            data.SearchStatus = false;

            if ($("#StartDateTestedMonth").val() != "") {
                data.StartDate = new Date($("#StartDateTestedMonth").val());
                data.SearchStatus = true;
            }
            if ($("#EndDateTestedMonth").val() != "") {
                data.EndDate = new Date($("#EndDateTestedMonth").val());
                data.SearchStatus = true;
            }

            if (data.SearchStatus == true) {
                ajaxRequest.makeRequest('/Dashboard/GetTotalSampleTestedMonthP', "POST", data, Home.onSuccessSearchTotalSampleTestedMonthP, Home.onError);
            }
            else {
                alert("Please enter search criteria !");

            }
        });
    },
    getSearchRequest: function () {
        ;
        var data = {
            "StartDate": ""
        };
        data.SearchStatus = false;
      
        if ($("#StartDate").val() != "") {
            data.StartDate = new Date($("#StartDate").val());
            data.SearchStatus = true;
        }
        if ($("#EndDate").val() != "") {
            data.EndDate = new Date($("#EndDate").val());
            data.SearchStatus = true;
        }
        return data;
    },
    onSuccessSearch: function (data) {
        ;
        if (data.status == "success") {
            if (data.Total.length > 0) {
                Home.GetTotalOPD(data.Total);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {
            //var message = { "message": "No Record found" }
            //applicationSystem.onInfo(message);
            alert('No Record found')
        }
    },
    onSuccessSearchOPDStatus: function (data) {
        ;
        if (data.status == "success") {
            if (data.Total.length > 0) {
                Home.GetTotalRevenueInMonth(data.Total);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {
            //var message = { "message": "No Record found" }
            //applicationSystem.onInfo(message);
            alert('No Record found');
        }
    },
    GetTotalSample: function (data) {
       
        var LabelMonthName = [];
        var LabelSampleType = [];
        var TotalSample = [];
       
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                LabelMonthName.push(requestItems[i].MonthName)
                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].sampleCount);


            }
        }
       
        $("#SampleChart").remove();
        $('#graphContainerSample').append('<canvas id="SampleChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#SampleChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelSampleType,
            datasets: [
                {

                    label: "Sample Received",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },

    GetTotalWO: function (data) {
        
        var LabelMonthName = [];
        var TotalWO = [];

    //New Designing
    GetTotalSampleTested: function () {
        $.ajax({
            url: '/Dashboard/Dashboard/GetTotalSampleTestedSampleTypeWise',
            type: 'Get',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: 'json',
            success: function (data) {
                var LabelSampleType = [];
                var TotalSample = [];

                if (data != null) {
                    $.each(data.data, function (i, item) {
                        LabelSampleType.push(item.Month)
                        TotalSample.push(item.count);
                    })
                    $("#TotalSampleTestedChart").remove();
                    $('#graphTotalSampleTested').append('<canvas id="TotalSampleTestedChart" style="height:250px"></canvas>');
                    var lineChartCanvas = $("#TotalSampleTestedChart").get(0).getContext("2d");
                    var lineChart = new Chart(lineChartCanvas);

                    var lineChartData = {
                LabelMonthName.push(requestItems[i].MonthWO);
                TotalWO.push(requestItems[i].WOCount);

                        datasets: [
                            {

                                label: "Request Raised",
                                fillColor: "rgba(60,141,188,0.9)",
                                strokeColor: "#F5715F",
                                pointColor: "#F5715F",
                                pointStrokeColor: "rgba(60,141,188,1)",
                                pointHighlightFill: "#FFF",
                                pointHighlightStroke: "rgba(60,141,188,1)",
                                data: TotalSample,


                            },

                        ]
                        , labels: LabelSampleType
                    };

                    var lineChartOptions = {
                        //Boolean - If we should show the scale at all
                        showScale: true,

                        //Boolean - Whether grid lines are shown across the chart
                        scaleShowGridLines: true,
                        //String - Colour of the grid lines
                        scaleGridLineColor: "#a6d4f5",
                        //Number - Width of the grid lines
                        scaleGridLineWidth: 1,
                        //Boolean - Whether to show horizontal lines (except X axis)
                        scaleShowHorizontalLines: true,
                        //Boolean - Whether to show vertical lines (except Y axis)
                        scaleShowVerticalLines: true,
                        //Boolean - Whether the line is curved between points
                        bezierCurve: true,
                        //Number - Tension of the bezier curve between points
                        bezierCurveTension: 0.3,
                        //Boolean - Whether to show a dot for each point
                        pointDot: false,
                        //Number - Radius of each point dot in pixels
                        pointDotRadius: 3,
                        //Number - Pixel width of point dot stroke
                        pointDotStrokeWidth: 1,
                        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
                        pointHitDetectionRadius: 20,
                        //Boolean - Whether to show a stroke for datasets
                        datasetStroke: true,
                        //Number - Pixel width of dataset stroke
                        datasetStrokeWidth: 2,
                        //Boolean - Whether to fill the dataset with a color
                        datasetFill: true,
                        //String - A legend template
                        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                        //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                        maintainAspectRatio: true,
                        //Boolean - whether to make the chart responsive to window resizing
                        responsive: true,

                        multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

                    };
                    lineChartOptions.datasetFill = false;
                    lineChart.Line(lineChartData, lineChartOptions);
                }

            }
        });

        $("#WOExeChart").remove();
        $('#graphWOExe').append('<canvas id="WOExeChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#WOExeChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthName,
            
            datasets: [
                {

                    
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalWO,
                   
                    

                },

            ]
        };
       
        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

                        //Boolean - Whether grid lines are shown across the chart
                        scaleShowGridLines: true,
                        //String - Colour of the grid lines
                        scaleGridLineColor: "#93f9aa",
                        //Number - Width of the grid lines
                        scaleGridLineWidth: 1,
                        //Boolean - Whether to show horizontal lines (except X axis)
                        scaleShowHorizontalLines: true,
                        //Boolean - Whether to show vertical lines (except Y axis)
                        scaleShowVerticalLines: true,
                        //Boolean - Whether the line is curved between points
                        bezierCurve: true,
                        //Number - Tension of the bezier curve between points
                        bezierCurveTension: 0.3,
                        //Boolean - Whether to show a dot for each point
                        pointDot: false,
                        //Number - Radius of each point dot in pixels
                        pointDotRadius: 3,
                        //Number - Pixel width of point dot stroke
                        pointDotStrokeWidth: 1,
                        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
                        pointHitDetectionRadius: 20,
                        //Boolean - Whether to show a stroke for datasets
                        datasetStroke: true,
                        //Number - Pixel width of dataset stroke
                        datasetStrokeWidth: 2,
                        //Boolean - Whether to fill the dataset with a color
                        datasetFill: true,
                        //String - A legend template
                        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                        //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                        maintainAspectRatio: true,
                        //Boolean - whether to make the chart responsive to window resizing
                        responsive: true,

            multiTooltipTemplate: "<%= value %>"

                    };
                    lineChartOptions.datasetFill = false;
                    lineChart.Bar(lineChartData, lineChartOptions);
                }

            }
        });
    },
    GetTotalWOComp: function (data) {
        
        var LabelMonthName = [];
        var LabelCustomerName = [];
        var TotalWO = [];

                if (data != null) {
                    //var requestItems = [];
                    //requestItems = data;
                    //for (var i = 0; i < requestItems.length; ++i) {

                    //    LabelMonthName.push(requestItems[i].Month)
                    //    TotalRevenue.push(requestItems[i].count);

                    //}
                    $.each(data.data, function (i, item) {
                        LabelMonthName.push(item.Month)
                        TotalWO.push(item.count);
                    })

            }
        }

        $("#WOCompChart").remove();
        $('#graphTotalWOComp').append('<canvas id="WOCompChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#WOCompChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthName,

            datasets: [
                {


                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalWO,



                            },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

                        //Boolean - Whether grid lines are shown across the chart
                        scaleShowGridLines: true,
                        //String - Colour of the grid lines
                        scaleGridLineColor: "#fb8e98",
                        //Number - Width of the grid lines
                        scaleGridLineWidth: 1,
                        //Boolean - Whether to show horizontal lines (except X axis)
                        scaleShowHorizontalLines: true,
                        //Boolean - Whether to show vertical lines (except Y axis)
                        scaleShowVerticalLines: true,
                        //Boolean - Whether the line is curved between points
                        bezierCurve: true,
                        //Number - Tension of the bezier curve between points
                        bezierCurveTension: 0.3,
                        //Boolean - Whether to show a dot for each point
                        pointDot: false,
                        //Number - Radius of each point dot in pixels
                        pointDotRadius: 3,
                        //Number - Pixel width of point dot stroke
                        pointDotStrokeWidth: 1,
                        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
                        pointHitDetectionRadius: 20,
                        //Boolean - Whether to show a stroke for datasets
                        datasetStroke: true,
                        //Number - Pixel width of dataset stroke
                        datasetStrokeWidth: 2,
                        //Boolean - Whether to fill the dataset with a color
                        datasetFill: true,
                        //String - A legend template
                        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                        //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                        maintainAspectRatio: true,
                        //Boolean - whether to make the chart responsive to window resizing
                        responsive: true,

                        multiTooltipTemplate: "<%= value %>"

                    };
                    lineChartOptions.datasetFill = false;
                    lineChart.Bar(lineChartData, lineChartOptions);

    },

    GetTotalPurchase: function () {

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                LabelMonthName.push(requestItems[i].MonthWO);
                LabelCustomerName.push(requestItems[i].CustomerName);
                TotalWO.push(requestItems[i].WOCount);


            }
        }

        $("#WOCompChart").remove();
        $('#graphTotalWOComp').append('<canvas id="WOCompChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#WOCompChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthName,

            datasets: [
                {


                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalWO,



                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Bar(lineChartData, lineChartOptions);

    },

    GetTotalPurchase: function (data) {
       
        var LabelItemName = [];
        var TotalPurchase = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                LabelItemName.push(requestItems[i].Item);
                TotalPurchase.push(requestItems[i].Total);


            }
        }

        $("#PurchaseChart").remove();
        $('#graphContainerPurchase').append('<canvas id="PurchaseChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#PurchaseChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelItemName,
            datasets: [
                {

                    label: "Sample Received",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalPurchase,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Bar(lineChartData, lineChartOptions);

    },
    GetTotalPurchaseMonth: function (data) {
        
        var LabelItemName = [];
        var TotalPurchase = [];
        var LabelMonthName = [];
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                LabelItemName.push(requestItems[i].Item);
                TotalPurchase.push(requestItems[i].Total);
                LabelMonthName.push(requestItems[i].MonthName);

            }
        }
        debugger
        $("#PurchaseMonthChart").remove();
        $('#graphContainerPurchaseMonth').append('<canvas id="PurchaseMonthChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#PurchaseMonthChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);
        
        var lineChartData = {
            labels: LabelItemName,
           
            datasets: [
                {

                    label: "Sample Received",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                   
                    data: TotalPurchase,
                    
                },

            ],
            
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,
             toolTip: LabelMonthName,
            multiTooltipTemplate: "<%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Bar(lineChartData, lineChartOptions);

    },

    GetTotalEnquiry: function (data) {
        debugger
        var LabelMonthNameRevenu = [];
        var TotalRevenue = [];
        var TotalInvoiceAmount = [];
        var TotalPaidAmount = [];
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelMonthNameRevenu.push(requestItems[i].EnquiryOn)
                TotalRevenue.push(requestItems[i].countEnquiry);
                
                
            }
        }
        
        $("#EnquiryChart").remove();
        $('#graphContainerEnquiry').append('<canvas id="EnquiryChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#EnquiryChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthNameRevenu,
            datasets: [
              {

                  label: "Request Raised",
                  fillColor: "rgba(60,141,188,0.9)",
                  strokeColor: "#F5715F",
                  pointColor: "#F5715F",
                  pointStrokeColor: "rgba(60,141,188,1)",
                  pointHighlightFill: "#F5715F",
                  pointHighlightStroke: "rgba(60,141,188,1)",
                  data: TotalRevenue,

              },
            
            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Bar(lineChartData, lineChartOptions);

    },

    GetTotalQuotation: function (data) {
        
        var LabelMonthNameRevenu = [];
        var TotalRevenue = [];
        var TotalInvoiceAmount = [];
        var TotalPaidAmount = [];
       
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelMonthNameRevenu.push(requestItems[i].MonthQuotation)
                TotalRevenue.push(requestItems[i].QuotationCount);


            }
        }

        $("#QuotationChart").remove();
        $('#graphQuotation').append('<canvas id="QuotationChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#QuotationChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthNameRevenu,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalRevenue,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },

    GetTotalRevenue: function (data) {

        var LabelMonthNameRevenu = [];
        var TotalRevenue = [];
        var TotalInvoiceAmount = [];
        var TotalPaidAmount = [];
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                if (requestItems[i].EnteredDate != null) {
                    var newdate = data[i].EnteredDate;
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
                    var _date2 = day + "/" + month + "/" + year;
                }

                LabelMonthNameRevenu.push(_date2)
                TotalRevenue.push(requestItems[i].TotalAmount);


            }
        }
        
        $("#TotalChart").remove();
        $('#graphTotal1').append('<canvas id="TotalChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthNameRevenu,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalRevenue,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },
    GetTotalParameter: function (data) {

        var LabelMonthNameRevenu = [];
        var TotalParameter = [];
       
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                if (requestItems[i].EnteredDate != null) {
                    var newdate = data[i].EnteredDate;
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
                    var _date2 = day + "/" + month + "/" + year;
                }

                LabelMonthNameRevenu.push(_date2)
                TotalParameter.push(requestItems[i].countParameter);


            }
        }
        
        $("#ParameterChart").remove();
        $('#graphParameter').append('<canvas id="ParameterChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#ParameterChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthNameRevenu,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalParameter,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },






    GetTotalRevenue: function (data) {

        var LabelMonthNameRevenu = [];
        var TotalRevenue = [];
        var TotalInvoiceAmount = [];
        var TotalPaidAmount = [];
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {

                if (requestItems[i].EnteredDate != null) {
                    var newdate = data[i].EnteredDate;
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
                    var _date2 = day + "/" + month + "/" + year;
                }

                LabelMonthNameRevenu.push(_date2)
                TotalRevenue.push(requestItems[i].TotalAmount);


            }
        }
        
        $("#TotalChart").remove();
        $('#graphTotal1').append('<canvas id="TotalChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelMonthNameRevenu,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalRevenue,

                            },

            ]
        };

                    var lineChartOptions = {
                        //Boolean - If we should show the scale at all
                        showScale: true,

                        //Boolean - Whether grid lines are shown across the chart
                        scaleShowGridLines: true,
                        //String - Colour of the grid lines
                        scaleGridLineColor: "#e6bf4a",
                        //Number - Width of the grid lines
                        scaleGridLineWidth: 1,
                        //Boolean - Whether to show horizontal lines (except X axis)
                        scaleShowHorizontalLines: true,
                        //Boolean - Whether to show vertical lines (except Y axis)
                        scaleShowVerticalLines: true,
                        //Boolean - Whether the line is curved between points
                        bezierCurve: true,
                        //Number - Tension of the bezier curve between points
                        bezierCurveTension: 0.3,
                        //Boolean - Whether to show a dot for each point
                        pointDot: false,
                        //Number - Radius of each point dot in pixels
                        pointDotRadius: 3,
                        //Number - Pixel width of point dot stroke
                        pointDotStrokeWidth: 1,
                        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
                        pointHitDetectionRadius: 20,
                        //Boolean - Whether to show a stroke for datasets
                        datasetStroke: true,
                        //Number - Pixel width of dataset stroke
                        datasetStrokeWidth: 2,
                        //Boolean - Whether to fill the dataset with a color
                        datasetFill: true,
                        //String - A legend template
                        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                        //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                        maintainAspectRatio: true,
                        //Boolean - whether to make the chart responsive to window resizing
                        responsive: true,

                        multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },
    GetTotalParameter: function (data) {

        var LabelMonthNameRevenu = [];
        var TotalParameter = [];
       
        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelMonthNameRevenu.push(_date2)
                TotalParameter.push(requestItems[i].countParameter);


            }
        }
        
        $("#ParameterChart").remove();
        $('#graphParameter').append('<canvas id="ParameterChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#ParameterChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {

            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#F5715F",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalParameter,

                },

            ]
            , labels: LabelSampleType
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;

        lineChart.Line(lineChartData, lineChartOptions);

    },
    GetTotalSampleApproved: function (data) {

        var LabelSampleType = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].TotalSample);


            }
        }


        $("#TotalSampleApprovedChart").remove();
        $('#graphTotalSampleApproved').append('<canvas id="TotalSampleApprovedChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalSampleApprovedChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelSampleType,
            labels: LabelMonthNameRevenu,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,


                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%= value %>",
           

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },

    GetTotalSampleMonth: function (data) {
        
        var LabelSampleType = [];
        var LabelMonth = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {


                LabelMonth.push(requestItems[i].MonthName)
                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].sampleCount);


            }
        }

        debugger
        $("#TotalSampleMonthChart").remove();
        $('#graphTotalSampleMonth').append('<canvas id="TotalSampleMonthChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalSampleMonthChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelSampleType,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },

    GetTotalSampleTested: function (data) {

        var LabelSampleType = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].TotalSample);


            }
        }


        $("#TotalSampleTestedChart").remove();
        $('#graphTotalSampleTested').append('<canvas id="TotalSampleTestedChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalSampleTestedChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {

            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,


                },

            ]
            , labels: LabelSampleType
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;

        lineChart.Bar(lineChartData, lineChartOptions);

    },
    GetTotalSampleTestedMonth: function (data) {
        
        var LabelSampleType = [];
        var LabelMonth = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {


                LabelMonth.push(requestItems[i].MonthName)
                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].sampleCount);


            }
        }


        $("#TotalSampleMonthChart").remove();
        $('#graphTotalSampleTestedMonth').append('<canvas id="TotalSampleMonthChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalSampleMonthChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelSampleType,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,

                },

   
    GetTotalSampleCollectedTypeWise: function (data) {

        var LabelSampleType = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {



                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].TotalSample);


            }
        }


        $("#SampleCollectedTypeChart").remove();
        $('#graphContainersampleCollectedType').append('<canvas id="SampleCollectedTypeChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#SampleCollectedTypeChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {

            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,


                },

            ]
            , labels: LabelSampleType
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;

        lineChart.Bar(lineChartData, lineChartOptions);

    },
    GetTotalSampleCollectedMonth: function (data) {
        
        var LabelSampleType = [];
        var LabelMonth = [];
        var TotalSample = [];

        if (data != null) {
            var requestItems = [];
            requestItems = data;
            for (var i = 0; i < requestItems.length; ++i) {


                LabelMonth.push(requestItems[i].MonthName)
                LabelSampleType.push(requestItems[i].SampleType)
                TotalSample.push(requestItems[i].sampleCount);


            }
        }


        $("#TotalSampleCollectedMonthChart").remove();
        $('#graphTotalSampleCollectedMonth').append('<canvas id="TotalSampleCollectedMonthChart" style="height:250px"></canvas>');
        var lineChartCanvas = $("#TotalSampleCollectedMonthChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);

        var lineChartData = {
            labels: LabelSampleType,
            datasets: [
                {

                    label: "Request Raised",
                    fillColor: "rgba(60,141,188,0.9)",
                    strokeColor: "#F5715F",
                    pointColor: "#F5715F",
                    pointStrokeColor: "rgba(60,141,188,1)",
                    pointHighlightFill: "#FFF",
                    pointHighlightStroke: "rgba(60,141,188,1)",
                    data: TotalSample,

                },

            ]
        };

        var lineChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,

            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 3,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,

            multiTooltipTemplate: "<%=datasetLabel%> : <%= value %>"

        };
        lineChartOptions.datasetFill = false;
        lineChart.Line(lineChartData, lineChartOptions);

    },
    onError: function (data) {
        $('#buttonSave').removeProp("disabled")
   //     applicationSystem.onError(data);
    },

    onSuccessSearchTypeWise: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleTypeWise(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {
            
            alert('No Record found');
        }
    },
    onSuccessSearchWO: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWO(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchWOB: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWO(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchPO: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalPurchase(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalQuotSent: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalQuotation(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalEnquiry: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalEnquiry(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalRevenue: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalRevenue(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchSample: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSample(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchWOH: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWO(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchWOCompS: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWOComp(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchWOExeS: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWOExe(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchWOS: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalWO(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchPOH: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalPurchase(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchCollected: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleCollectedTypeWise(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchCollectedMonth: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleCollectedMonth(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleMonthE: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleMonth(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleTypeWiseP: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleTypeWise(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleMonthP: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleMonth1(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleTestedP: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleTested(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleTestedMonthP: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleTestedMonth(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchTotalSampleApproved: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalSampleApproved(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },
    onSuccessSearchPOMonth: function (data) {
        debugger;
        if (data.status == "success") {
            if (data.Result.length > 0) {
                Home.GetTotalPurchaseMonth(data.Result);
            }
        }
        else if (data.status == "info1") {
            //applicationSystem.onInfo(data);
            return false;
        }
        else {

            alert('No Record found');
        }
    },

};
