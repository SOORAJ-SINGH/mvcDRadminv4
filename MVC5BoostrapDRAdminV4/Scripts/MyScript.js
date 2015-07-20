$(function () {
    $('.datePicker').datetimepicker({
        format : "DD-MM-YYYY",
        
       
        
    });


    $("#startDate").on("dp.change", function (e) {
        $('#endDate').data("DateTimePicker").minDate(e.date);
    });
    $("#endDate").on("dp.change", function (e) {
        $('#startDate').data("DateTimePicker").maxDate(e.date);
    });
});