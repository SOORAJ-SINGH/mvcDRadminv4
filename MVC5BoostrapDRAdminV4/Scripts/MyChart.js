function GetData(dateStart, dateEnd) {
    var url = '/Charts/DrFilledBarChart?startDate=' + dateStart + '&endDate=' + dateEnd;
    var asd;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        url: url,



        data: '{}',
        success: function (chartsdata) {

            // Callback that creates and populates a data table,
            // instantiates the pie chart, passes in the data and
            // draws it.

            var data = new google.visualization.DataTable();

            data.addColumn('string', 'FirstName');
            data.addColumn('number', 'TOTAL_DATES_FILLED');

            for (var i = 0; i < chartsdata.length; i++) {
                data.addRow([chartsdata[i].FirstName, chartsdata[i].TOTAL_DATES_FILLED]);
            }

            // Instantiate and draw our chart, passing in some options
            var chart = new google.visualization.BarChart(document.getElementById('chartdiv'));

            var options = {

                title: 'DR report For Number of dates filled ',
                animation: {
                    duration: 1500,
                    easing: 'inAndOut',
                    startup: true
                },
                titleTextStyle: {
                    color: 'orange',
                    fontSize: 19,
                    bold: true,
                    italic: false
                },
                hAxis: {
                    title: 'Total Number of Dates DR Filled ---->', titleTextStyle: { color: '#FF0000' }
                },

                //bars: 'horizontal', // Required for Material Bar Charts.
                bar: { groupWidth: "95%" },
                legend: { position: "none" },
            }

            chart.draw(data, options);

        },
        error: function () {
            alert("Error loading data! Please try again.");
        }
    });
}


