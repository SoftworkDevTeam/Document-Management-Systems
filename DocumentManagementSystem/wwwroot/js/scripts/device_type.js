$(document).ready(function () {
    var $deviceTypeTbody = $("#deviceTypeTbody");
    var $deviceTypeTemplate = $("#deviceTypeTemplate");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");

    var $deviceTypeForm = $("#deviceTypeForm");
    var $txtDeviceType = $("#txtDeviceType");

    //validation
    $deviceTypeForm.validate({
        rules: {
            name: {
                required: true,

            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveDeviceType();
            return false;
        }
    });
    //get employee type data
    function deviceTypeData() {
        return {
            Name: $txtDeviceType.val(),
            Description: $taDescription.val().trim()
        };
    }

    function saveDeviceType() {
        $spinner.removeClass("d-none");

        axios({
            method: 'post',
            url: "/api/deviceType",
            data: deviceTypeData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Successfully create device type");
                location.reload();
                console.log(res.data);


            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while saving device type");
                console.log(err);
            });
    }

    getDeviceType().then(types => {
        var template = $deviceTypeTemplate.html();
        var render = Mustache.render(template, { types: types });
        $deviceTypeTbody.html(render);

        console.log(types);
    });
})