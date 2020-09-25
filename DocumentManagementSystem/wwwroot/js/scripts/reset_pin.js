$(document).ready(function () {
    var $employeeIdTemplate = $("#employeeIdTemplate");
    var $ddlEmployeeId = $("#ddlEmployeeId");
    var $txtEmail = $("#txtEmail");
    var $resetPinForm = $("#resetPinForm");
    var $spinner = $("#spinner");

    let employeeDetails = [];
    let selectedUser;

    getUsers().then(employeeids => {
        employeeDetails = employeeids;
        var template = $employeeIdTemplate.html();
        var rendered = Mustache.render(template, { employeeids: employeeids });
        $ddlEmployeeId.html(rendered);

    });
    $txtEmail.val("");
    $ddlEmployeeId.on("change", function () {

        selectedUser = employeeDetails.filter(i => i.id === parseInt($(this).val()));
        if ($(this).val() !== "-1") {
            $txtEmail.val(selectedUser[0].email);

        } else {
            $txtEmail.val(" ");

        }

    });

    function resetPinData() {
        return {
            Id: $ddlEmployeeId.val(),
            Email: $txtEmail.val(),
            FullName: selectedUser[0].fullName
        };
    }

    $resetPinForm.on("submit", function (e) {
        e.preventDefault();
        $spinner.removeClass("d-none");
        if ($ddlEmployeeId.val() !== "-1") {
            console.log(resetPinData());
            
            axios({
                method: 'put',
                url: "/api/auth/resetpin",
                data: resetPinData()
            })
              .then(function (res) {
                    $spinner.addClass("d-none");

                 notify("success", "An Email has been sent successfully to the user");
                    console.log(res.data);


             })
             .catch(function (err) {
                 $spinner.addClass("d-none");
                 notify("danger", "An error occured while sending an email to user");

                    console.log(err);
             });

        } else {
            $spinner.addClass("d-none");

            notify("warning", "Please select an employee id");
        }

    });



});