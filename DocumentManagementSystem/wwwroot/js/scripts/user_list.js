$(document).ready(function () {
    var $userTable = $("#userTable");
    var $userTableTemplate = $("#userTableTemplate");
    var $userTbody = $("#userTbody");



    getUsers().then(users => {
        var template = $userTableTemplate.html();
        var rendered = Mustache.render(template, { users: users });
        $userTbody.html(rendered);
        $userTable.DataTable({
            ordering: false
        });


    });
    //console.log("working");
});