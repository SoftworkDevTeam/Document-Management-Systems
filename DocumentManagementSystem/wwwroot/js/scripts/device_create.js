$(document).ready(function () {
    var $employeeIdTemplate = $("#employeeIdTemplate");
    var $ddlEmployeeId = $("#ddlEmployeeId");
    var $txtEmail = $("#txtEmail");
    var $txtSerialNumber = $("#txtSerialNumber");
    var $createDeviceForm = $("#createDeviceForm");
    var $createDeviceTile = $("#createDeviceTile");
    var $barcodeDiv = $("#barcodeDiv");
    var $spinner = $("#spinner");

    //device type
    var $deviceTypeTemplate = $("#deviceTypeTemplate");
    var $ddlDeviceType = $("#ddlDeviceType");

    //device model
    var $deviceModelTemplate = $("#deviceModelTemplate");
    var $ddlDeviceModel = $("#ddlDeviceModel");

    //Property types
    var $devicePropertyTypeTemplate = $("#devicePropertyTypeTemplate");
    var $ddlDevicePropertyType = $("#ddlDevicePropertyType");

    //Property color
    var $ddlDeviceColor = $("#ddlDeviceColor");
    var $deviceColorTemplate = $("#deviceColorTemplate");


    let employeeDetails = [];
    let selectedUser;


    //validation
    $createDeviceForm.validate({
        rules: {
            eid: {
                required: true,

            },
            serialnumber: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveDevice();
            return false;
        }
    });
    function deviceData() {
        return {
            ProfileId: $ddlEmployeeId.val(),
            EmployeeId: selectedUser[0].employeeId,
            Email: $txtEmail.val(),
            SerialNumber: $txtSerialNumber.val().toUpperCase(),
            DeviceTypeId: $ddlDeviceType.val(),
            DeviceModelId: $ddlDeviceModel.val(),
            DeviceColorId: $ddlDeviceColor.val(),
            PropertyTypeId: $ddlDevicePropertyType.val()
        };
    }


    function saveDevice() {
        $spinner.removeClass("d-none");
        axios({
            method: 'post',
            url: "/api/device",
            data: deviceData()
        })
            .then(function (res) {
                $spinner.addClass("d-none");
                notify("success", "Device successfully created");
                $createDeviceTile.addClass("d-none");
                $barcodeDiv.removeClass("d-none");
                $createDeviceForm[0].reset();
                console.log(res.data);


            })
            .catch(function (err) {
                $spinner.addClass("d-none");
                notify("danger", "An error occured while creating device");

                console.log(err);
            });
    }

    getDevicePropertyType().then(propertytypes => {
       
        var template = $devicePropertyTypeTemplate.html();
        var rendered = Mustache.render(template, { propertytypes: propertytypes });
        $ddlDevicePropertyType.html(rendered);

    });

    getUsers().then(employeeids => {
        employeeDetails = employeeids;
        var template = $employeeIdTemplate.html();
        var rendered = Mustache.render(template, { employeeids: employeeids });
        $ddlEmployeeId.html(rendered);

    });

    $ddlEmployeeId.on("change", function () {

         selectedUser = employeeDetails.filter(i => i.id === parseInt($(this).val()));
        if ($(this).val() !== "-1") {
            $txtEmail.val(selectedUser[0].email);

        } else {
            $txtEmail.val(" ");
            
        }
      
    });

    getDeviceType().then(devicetypes => {
        var template = $deviceTypeTemplate.html();
        var rendered = Mustache.render(template, { devicetypes: devicetypes });
        $ddlDeviceType.html(rendered);
    });

    getDeviceModel().then(devicemodels => {
        var template = $deviceModelTemplate.html();
        var rendered = Mustache.render(template, { devicemodels: devicemodels });
        $ddlDeviceModel.html(rendered);
    });
    getDeviceColor().then(devicecolors => {
        var template = $deviceColorTemplate.html();
        var rendered = Mustache.render(template, { devicecolors: devicecolors });
        $ddlDeviceColor.html(rendered);
    });

});