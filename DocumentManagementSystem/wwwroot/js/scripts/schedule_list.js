$(document).ready(function () {
    var $scheduleTableBody = $("#scheduleTableBody");
    var $scheduleTableTemplate = $("#scheduleTableTemplate");
    var $scheduleTable = $("#scheduleTable");

    getVisitorsByHostId(localStorage.userId).then(schedules => {
        var rendered = Mustache.render($scheduleTableTemplate.html(), { schedules: schedules });
        $scheduleTableBody.html(rendered);
        $scheduleTable.DataTable({
            ordering: false
        });
    });

})