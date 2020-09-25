$(document).ready(function () {
    var $employeetypeTbody = $("#employeetypeTbody");
    var $employeetypeTemplate = $("#employeetypeTemplate");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");

    var $employeeTypeForm = $("#employeeTypeForm");
    var $txtEmployeeType = $("#txtEmployeeType");

    //validation
    $employeeTypeForm.validate({
        rules: {
            name: {
                required: true,

            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveEmployeeType();
            return false;
        }
    });

    //get employee type data
    function employeetypeData() {
        return {
            Name: $txtEmployeeType.val(),
            Description: $taDescription.val().trim()
        };
    }

    function saveEmployeeType() {
        $spinner.removeClass("d-none");
        axios({
            method: 'post',
            url: "/api/employeetype",
            data: employeetypeData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Successfully saved Employee type");
                console.log(res.data);
                location.reload();


            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while saving Employee type");

                console.log(err);
            });
    }

    getEmployeeTypes().then(types => {
        var template = $employeetypeTemplate.html();
        var render = Mustache.render(template, { types: types });
        $employeetypeTbody.html(render);
        
        console.log(types);
    });

    //delete employeee type
    $(document).on("click", ".delete", function () {
        var tr = $(this).closest("tr");
        var id = tr.data("id");
        var name = tr.find("td.name").text();

        console.log(id + " " + name);
        deleteItemInTable(id, "api/user", name, tr);

    });
})