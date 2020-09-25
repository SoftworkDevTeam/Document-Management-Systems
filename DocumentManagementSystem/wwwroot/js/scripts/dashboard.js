$(document).ready(function () {
    var $userCount = $("#userCount");
    var $deviceCount = $("#deviceCount");
    var $visitorCount = $("#visitorCount");
    var $scheduleCount = $("#scheduleCount");


    getUsers().then(users => {
        var length = users.length;
        $userCount.html(`<b> ${length} </b>`);
    });

    getDevices().then(devices => {
        var length = devices.length;
        $deviceCount.html(`<b> ${length} </b>`);
    });
    getTodayVisitors().then(visitors => {
        var length = visitors.length;
        var schedules = visitors.filter(v => v.status === PRESCHEDULED);
        var prescheduleLength = schedules.length;
        $visitorCount.html(`<b> ${length} </b>`);
        $scheduleCount.html(`<b> ${prescheduleLength} </b>`);

    });
   
});
