$(document).ready(function () {
    var $txtStartDate = $("#txtStartDate");
    var $txtEndDate = $("#txtEndDate");
    var $movementFilter = $("#movementFilter");
    var $btnSearch = $("#btnSearch");
    var $inventoryTable = $("#inventoryTable");
    var $inventoryTableTemplate = $("#inventoryTableTemplate");
    var $inventoryTableBody = $("#inventoryTableBody");
    var $ddlMovementType = $("#ddlMovementType");


    let minimumDate;
    let inventoryList;
    let table = $inventoryTable.DataTable();


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
            getInventoryByStartEndDate($txtStartDate.val(), $txtEndDate.val()).then(inventories => {
                inventoryList = inventories;

                var rendered = Mustache.render($inventoryTableTemplate.html(), { inventories: inventories });
                $inventoryTableBody.html(rendered);
                $movementFilter.removeClass("d-none");
                table = $inventoryTable.DataTable({
                    ordering: false
                });
            });
          
        }
    });

    $ddlMovementType.on("change", function () {
        table.destroy();

        console.log($(this).val());
        let filteredInventory;
        if ($(this).val() !== "-1") {
            filteredInventory = inventoryList.filter(v => v.movementType === $(this).val());


        } else {
            filteredInventory = inventoryList;

        }
        var rendered = Mustache.render($inventoryTableTemplate.html(), { inventories: filteredInventory });
        $inventoryTableBody.html(rendered);

        table = $inventoryTable.DataTable({
            ordering: false
        });


    });
   

})