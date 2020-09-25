$(document).ready(function () {
    var $userProfile = $("#userProfile");
    var $userProfileTemplate = $("#userProfileTemplate");

    var $userManagement = $("#userManagement");
    var $deviceManagement = $("#deviceManagement");
    var $visitorManagement = $("#visitorManagement");
    var $schedule = $("#schedule");
    var $inventory = $("#inventory");
    var $movement = $("#movement");

    if (localStorage.role === "administrator") {

        $userManagement.removeClass("d-none");
        $deviceManagement.removeClass("d-none");
        $inventory.removeClass("d-none");
        $schedule.removeClass("d-none");

    } else if (localStorage.role === "frontdesk") {
        $visitorManagement.removeClass("d-none");

    } else if (localStorage.role === "frontdesk") {
        $visitorManagement.removeClass("d-none");
        $schedule.removeClass("d-none");

    } else if (localStorage.role === "user") {
        $schedule.removeClass("d-none");

    } else if (localStorage.role === "superAdmin") {
        $userManagement.removeClass("d-none");

    }


    const userData = {
        fullName: localStorage.fullName.split(" ")[0],
        roleName: localStorage.role,
        imageUrl:"/" + localStorage.imageUrl
    };

    var rendered = Mustache.render($userProfileTemplate.html(), { userData: userData });
    $userProfile.html(rendered);


})