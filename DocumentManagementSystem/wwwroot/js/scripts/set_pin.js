$(document).ready(function () {
    var $keypad = $(".keypad");
    var $txtPin = $("#txtPin");
    var $btnClear = $("#btnClear");
    var $btnSubmit = $("#btnSubmit");
    var $hiddenToken = $("#hiddenToken");
    var $setPinForm = $("#setPinForm");
    var $fullNameHeader = $("#fullNameHeader");
  


    let pinEntered = "";
    let txtValue = "";
    $txtPin.val(txtValue);
    $btnSubmit.prop('disabled', true);

    if ($hiddenToken.val() === "") {
        $setPinForm.html('<div class="alert alert-danger"><h1>Invalid or Expired Token</h1><br/><a href="/">Go Back to Home Page</a></div>');
    } else {
        getProfileByToken($hiddenToken.val()).then(profile => {
            console.log(profile);

            //TODO: Check if profile.status === 1 instead of object keys
            if (Object.keys(profile).length === 0) {
                $setPinForm.html('<div class="alert alert-danger"><h1>Invalid or Expired Token</h1><br/><a href="/">Go Back to Home Page</a></div>');
            } else {
                $fullNameHeader.html(profile.fullName);

            }
        });
    }

    //make ajax call to validate token

    $keypad.on("click", function (e) {
        e.preventDefault();
        pinEntered += $(this).data("value");
        txtValue += "\u2022";

        $txtPin.val(txtValue);
        if (pinEntered.length === 4) {
            $keypad.prop('disabled', true);
            $btnSubmit.prop('disabled', false);

        }


        console.log($(this).data("value"));

    });
    $btnClear.on("click", function (e) {
        e.preventDefault();

        resetValues();
    });

    function setPinData() {
        return {
            Pin: pinEntered,
            Token: $hiddenToken.val()
        };
    }

    $setPinForm.on("submit", function (e) {
        e.preventDefault();
        axios({
            method: "put",
            url: "/api/auth/setpin",
            data: setPinData()
        })
            .then(function (res) {

                $setPinForm.html('<div class="alert alert-success"><h3>You have successfully set your pin.</h3><br/>please wait while we redirect you to the home page</div>');
                setTimeout(function () {
                    location.href = "/";
                }, 3000);
                console.log(res.data);

            })
            .catch(function (err) {

                console.log(err);
            });

        console.log(setPinData());
        resetValues();


    });


    function resetValues() {
        pinEntered = "";
        txtValue = "";
        $txtPin.val("");
        $keypad.prop('disabled', false);
        $btnSubmit.prop('disabled', true);
    }

});