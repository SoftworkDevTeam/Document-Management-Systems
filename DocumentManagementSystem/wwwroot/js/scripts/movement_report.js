$(document).ready(function () {
    var $txtStartDate = $("#txtStartDate");
    var $txtEndDate = $("#txtEndDate");
    var $movementFilter = $("#movementFilter");
    var $btnSearch = $("#btnSearch");
    var $movementTable = $("#movementTable");
    var $movementTableTemplate = $("#movementTableTemplate");
    var $movementTableBody = $("#movementTableBody");
    var $ddlMovementType = $("#ddlMovementType");


    let minimumDate;
    let movementList;
    let table = $movementTable.DataTable();


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
            getInventoryByOwnerStartEndDate(localStorage.userId, $txtStartDate.val(), $txtEndDate.val()).then(movement => {
                var indexedMovement = includeIndex(movement);

                var rendered = Mustache.render($movementTableTemplate.html(), { movement: indexedMovement });
                $movementTableBody.html(rendered);
                $movementFilter.removeClass("d-none");
                movementList = indexedMovement;
                table = $movementTable.DataTable({
                    ordering: false
                });
            });
          
        }
    });

    $ddlMovementType.on("change", function () {
        table.destroy();

        console.log($(this).val());
        let filteredmovement;
        if ($(this).val() !== "-1") {
            filteredmovement = movementList.filter(v => v.movementType === $(this).val());


        } else {
            filteredmovement = movementList;

        }
        var rendered = Mustache.render($movementTableTemplate.html(), { movement: filteredmovement });
        $movementTableBody.html(rendered);

        table = $movementTable.DataTable({
            ordering: false
        });


    });
   

})