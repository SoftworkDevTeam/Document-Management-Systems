$(document).ready(function () {
    var $devicePropertyTypeTbody = $("#devicePropertyTypeTbody");
    var $devicePropertyTypeTemplate = $("#devicePropertyTypeTemplate");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");

    var $devicePropertyTypeForm = $("#devicePropertyTypeForm");
    var $txtDevicePropertyType = $("#txtDevicePropertyType");

    //validation
    $devicePropertyTypeForm.validate({
        rules: {
            name: {
                required: true,

            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveDeviceAssetType();
            return false;
        }
    });

    //get employee PropertyType data
    function devicePropertyTypeData() {
        return {
            Name: $txtDevicePropertyType.val(),
            Description: $taDescription.val().trim()
        };
    }

    function saveDeviceAssetType() {
        $spinner.removeClass("d-none");
        axios({
            method: 'post',
            url: "/api/devicePropertyType",
            data: devicePropertyTypeData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Successfully saved device asset type");
                location.reload();
                console.log(res.data);


            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while trying to save device asset type");
                console.log(err);
            });
    }

    getDevicePropertyType().then(propertyTypes => {
        var template = $devicePropertyTypeTemplate.html();
        var render = Mustache.render(template, { propertyTypes: propertyTypes });
        $devicePropertyTypeTbody.html(render);

        console.log(propertyTypes);
    });
})