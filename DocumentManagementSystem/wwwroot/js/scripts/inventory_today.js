$(document).ready(function () {
    var $inventoryTable = $("#inventoryTable");
    var $inventoryTableTemplate = $("#inventoryTableTemplate");
    var $inventoriesTableBody = $("#inventoriesTableBody");
    var $ddlMovementType = $("#ddlMovementType");

    console.log("working");

    let inventoryList;
    let table = $inventoryTable.DataTable();


    function populateInventoryTable() {

        table.destroy();
    getInventoriesForToday().then(inventories => {

        var rendered = Mustache.render($inventoryTableTemplate.html(), { inventories: inventories });
        $inventoriesTableBody.html(rendered);
        inventoryList = inventories;

                table = $inventoryTable.DataTable({
                    ordering: false
                });
        });
    }
    populateInventoryTable();


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
        $inventoriesTableBody.html(rendered);

        table = $inventoryTable.DataTable({
            ordering: false
        });


    });
});
