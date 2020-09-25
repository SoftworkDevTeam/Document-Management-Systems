$(document).ready(function () {
    var $txtStartDate = $("#txtStartDate");
    var $txtEndDate = $("#txtEndDate");
    var $visitorFilter = $("#visitorFilter");
    var $btnSearch = $("#btnSearch");
    var $visitorTable = $("#visitorTable");
    var $visitorTableTemplate = $("#visitorTableTemplate");
    var $visitorTableBody = $("#visitorTableBody");
    var $ddlSelectStatus = $("#ddlSelectStatus");


    let minimumDate;
    let visitorList;
    let table = $visitorTable.DataTable();


    $txtStartDate.datepicker({
        dateFormat: "yy-mm-dd",
        beforeShowDay: $.datepicker.noWeekends
    });
    $txtStartDate.on("change", function () {

        $txtEndDate.removeClass('calendarclass');
        $txtEndDate.removeClass('hasDatepicker');
        $txtEndDate.unbind();


        minimumDate = $(this).val();
        $txtEndDate.datepicker({
            minDate: new Date(minimumDate),
            dateFormat: "yy-mm-dd",
            beforeShowDay: $.datepicker.noWeekends
        });
        $txtEndDate.prop("disabled", false);
        console.log(minimumDate);

    });

    $btnSearch.on("click", function () {
        table.destroy();

        if ($txtEndDate.val() !== "") {
            getVisitorByStartEndDate($txtStartDate.val(), $txtEndDate.val()).then(visitor => {
                var indexedvisitor = includeIndex(visitor);

                var rendered = Mustache.render($visitorTableTemplate.html(), { visitors: indexedvisitor });
                $visitorTableBody.html(rendered);

                $visitorFilter.removeClass("d-none");
                visitorList = indexedvisitor;
                table = $visitorTable.DataTable({
                    ordering: false
                });
                console.log(visitor);
            });

        }
    });

    $ddlSelectStatus.on("change", function () {
        table.destroy();

        console.log($(this).val());
        let filteredVisitors;
        if ($(this).val() !== "-1") {
            filteredVisitors = visitorList.filter(v => v.status == $(this).val());


        } else {
            filteredVisitors = visitorList;

        }
        var rendered = Mustache.render($visitorTableTemplate.html(), { visitors: filteredVisitors });
        $visitorTableBody.html(rendered);

        table = $visitorTable.DataTable({
            ordering: false
        });


    });


});