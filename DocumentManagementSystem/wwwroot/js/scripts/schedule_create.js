$(document).ready(function () {
    var $txtExpectedTime = $("#txtExpectedTime");
    var $txtExpectedDate = $("#txtExpectedDate");
    var $txtPhoneNumber = $("#txtPhoneNumber");
    var $txtFullName = $("#txtFullName");
    var $txtEmail = $("#txtEmail");
    var $taReason = $("#taReason");
    var $spinner = $("#spinner");
    var $gender = $("input[name=gender]:checked");
    var $createScheduleForm = $("#createScheduleForm");



    //validation
    $createScheduleForm.validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            fullname: {
                required: true
            },
            phone: {
                required: true
            },
            date: {
                required: true
            },
            time: {
                required: true
            },
            reason: {
                required: true
            }


        },
        submitHandler: function (form) {
            CreateSchedule();
            return false;
        }
    });


    function scheduleData() {
        return {
            Email: $txtEmail.val(),
            FullName: $txtFullName.val(),
            PhoneNumber: $txtPhoneNumber.val(),
            ExpectedTime: $txtExpectedTime.val(),
            Date: $txtExpectedDate.val(),
            UserID: localStorage.userId,
            Reason: $taReason.val(),
            Gender: $("input[name=gender]:checked").val()
        };
    }

    function CreateSchedule() {
        $spinner.removeClass("d-none");
            axios({
                method: "post",
                url: "/api/visitor",
                data: scheduleData()
            })
            .then(function (res) {

                $spinner.addClass("d-none");
                notify("success", "Your Visitor has been scheduled successfully");
                $createScheduleForm[0].reset();

                console.log(res.data);

            })
                .catch(function (err) {
                    $spinner.addClass("d-none");
                    notify("success", "An error occured while trying to schedule your visitor");

                console.log(err);
            });
    }

    $txtExpectedDate.datepicker();

    $txtExpectedTime.timepicker({
        timeFormat: 'HH:mm:ss',
        interval: 15,
        minTime: '7',
        maxTime: '6:00pm',
        defaultTime: '8',
        startTime: '7:00',
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });
    console.log("working");
});