$(document).ready(function () {
    var $txtEmail = $("#txtEmail");
    var $txtPassword = $("#txtPassword");
    var $txtResetPasswordEmail = $("#txtresetPasswordEmail");
    var $LoginForm = $("#loginForm");
    var $resetPasswordForm = $("#resetPasswordForm");
    var $spinner = $("#spinner");



    //package login data
    function loginData() {
        return {
            Username: $txtEmail.val().split("@")[0],
            Password: $txtPassword.val()
        };
    }


    //validation
    $LoginForm.validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true
            }
          
        },
        submitHandler: function (form) {
            loginUser();
            return false;
        }
    });


    function loginUser() {
        $spinner.removeClass("d-none");
       console.log(loginData());

        axios({
            method: 'post',
            url: "/api/auth/login",
            data: loginData()
        })
            .then(function (userData) {
                const { token, expiration, user, role, imageUrl, fullname } = userData.data;
                localStorage.DeviceInventoryJWT = token;
                localStorage.userId = user;
                localStorage.role = role;
                localStorage.imageUrl = imageUrl;
                localStorage.fullName = fullname;
                console.log(userData.data);
                //reset login form
                $LoginForm[0].reset();
                $spinner.addClass("d-none");


                $txtEmail.val("");
                $txtPassword.val("");
                location.href = "/Dashboard";

            })
            .catch(function (err) {
                $spinner.addClass("d-none");

                notify("danger", "Username or Password not correct");

                console.log(err);
            });


    }

});