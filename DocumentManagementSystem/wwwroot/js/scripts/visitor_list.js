$(document).ready(function () {
    var $visitorsTableBody = $("#visitorsTableBody");
    var $visitorsTableTemplate = $("#visitorsTableTemplate");
    var $ddlSelectStatus = $("#ddlSelectStatus");
    var $visitorsTable = $("#visitorsTable");

    let visitorsList;
    let table = $visitorsTable.DataTable();

    function populatevisitorsTable() {
        table.destroy();

        getTodayVisitors().then(visitors => {
            var rendered = Mustache.render($visitorsTableTemplate.html(), { visitors: visitors });
            $visitorsTableBody.html(rendered);
            visitorsList = visitors;
           table = $visitorsTable.DataTable({
                ordering: false
            });
           
        });
    }

    populatevisitorsTable();

    $ddlSelectStatus.on("change", function () {
        table.destroy();

        console.log($(this).val());
        let filteredVisitors;
        if ($(this).val() !== "-1") {
            filteredVisitors = visitorsList.filter(v => v.status === $(this).val());
          
           
        } else {
            filteredVisitors = visitorsList;
            
        }
        var rendered = Mustache.render($visitorsTableTemplate.html(), { visitors: filteredVisitors });
        $visitorsTableBody.html(rendered);

        table = $visitorsTable.DataTable({
            ordering: false
        });
        
       
    });

});