$(document).ready(function () {

    var $btnLogout = $("#btnLogout");
    $btnLogout.on("click", function () {
        localStorage.removeItem("DeviceInventoryJWT");
        localStorage.removeItem("userId");
        localStorage.removeItem("role");


        window.location = "/";
    });


})