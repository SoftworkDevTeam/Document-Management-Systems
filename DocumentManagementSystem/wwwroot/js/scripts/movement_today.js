$(document).ready(function () {
    var $movementTable = $("#movementTable");
    var $movementTableTemplate = $("#movementTableTemplate");
    var $movementTableBody = $("#movementTableBody");
    var $ddlMovementType = $("#ddlMovementType");

    console.log("working");

    let movementList;
    let table = $movementTable.DataTable();


    function populatemovementTable() {

        table.destroy();
        getInventoriesForTodayOwner(localStorage.userId).then(movement => {
            var indexedMovement = includeIndex(movement);
            var rendered = Mustache.render($movementTableTemplate.html(), { movement: indexedMovement });
        $movementTableBody.html(rendered);
            movementList = indexedMovement;

                table = $movementTable.DataTable({
                    ordering: false
                    });
            console.log(movement);
        });
    }
    populatemovementTable();


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
});
