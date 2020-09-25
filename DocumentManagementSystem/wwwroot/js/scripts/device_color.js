$(document).ready(function () {
    var $deviceColorTbody = $("#deviceColorTbody");
    var $deviceColorTemplate = $("#deviceColorTemplate");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");


    var $deviceColorForm = $("#deviceColorForm");
    var $txtDeviceColor = $("#txtDeviceColor");

    //validation
    $deviceColorForm.validate({
        rules: {
            name: {
                required: true,

            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveDeviceColor();
            return false;
        }
    });

    //get employee type data
    function deviceColorData() {
        return {
            Name: $txtDeviceColor.val(),
            Description: $taDescription.val().trim()
        };
    }

    function saveDeviceColor() {
        $spinner.removeClass("d-none");
        axios({
            method: 'post',
            url: "/api/devicecolor",
            data: deviceColorData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Successfully saved device color");
                location.reload();
                console.log(res.data);
                
            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while trying to save device Color");

                console.log(err);
            });
    }

    getDeviceColor().then(colors => {
        var template = $deviceColorTemplate.html();
        var render = Mustache.render(template, { colors: colors });
        $deviceColorTbody.html(render);

        console.log(colors);
    });
})