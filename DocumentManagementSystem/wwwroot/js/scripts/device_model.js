$(document).ready(function () {
    var $deviceModelTbody = $("#deviceModelTbody");
    var $deviceModelTemplate = $("#deviceModelTemplate");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");


    var $deviceModelForm = $("#deviceModelForm");
    var $txtDeviceModel = $("#txtDeviceModel");

    //validation
    $deviceModelForm.validate({
        rules: {
            name: {
                required: true,

            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveDeviceModel();
            return false;
        }
    });
    //get employee type data
    function deviceModelData() {
        return {
            Name: $txtDeviceModel.val(),
            Description: $taDescription.val().trim()
        };
    }

    function saveDeviceModel() {
        $spinner.removeClass("d-none");
        axios({
            method: 'post',
            url: "/api/deviceModel",
            data: deviceModelData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Successfully saved device model");
                location.reload();
                console.log(res.data);


            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while trying to save device Model");
                console.log(err);
            });
    }

    getDeviceModel().then(Models => {
        var template = $deviceModelTemplate.html();
        var render = Mustache.render(template, { Models: Models });
        $deviceModelTbody.html(render);

        console.log(Models);
    });
})