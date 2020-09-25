$(document).ready(function () {
    var $schedulesTableBody = $("#schedulesTableBody");
    var $scheduledVisitorsTableTemplate = $("#scheduledVisitorsTableTemplate");
    var $visitorTable = $("#visitorTable");
    

    getVisitorsByStatus(PRESCHEDULED).then(schedules => {
        var rendered = Mustache.render($scheduledVisitorsTableTemplate.html(), { schedules: schedules });
        $schedulesTableBody.html(rendered);
        $visitorTable.DataTable({
            ordering: false
        });
    });

    function updateStatusData(id, status) {
        return {
            Id: id,
            Status: status
        };
    }
    $(document).on("click", ".btnCheckIn", function () {
        var tr = $(this).closest("tr");

        axios({
            method: "put",
            url: "/api/visitor/status",
            data: updateStatusData(tr.data("id"), CHECKEDIN)
        }).then(res => {

            console.log(res);
            location.reload(); 
        });
        console.log(updateStatusData(tr.data("id"), CHECKEDIN));
    });

})