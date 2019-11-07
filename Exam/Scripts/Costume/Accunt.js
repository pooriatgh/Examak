function Login() {
    var mobile = $("#mobile").val()
    var password = $("#password").val()
    var data = { 'mobile': mobile, 'password': password }
    $.post("/accunt/login",
        data, function (result) {
            if (result == "ok") {
                window.location.replace("/home/about");
            }
            else {
                message = result
            }
    });
}

function Signup() {
    var mobile = $("#mobile").val()
    var password = $("#password").val()
    var confirmPassword = $("#confirmPassword").val()
    var data = { 'mobile': mobile, 'password': password }
    if (password == confirmPassword) {
        $.post("/accunt/signup",
            data, function (result) {
                alert(result);
            });
    }
    else{
        $('#password').css('border-color', 'red');
        $('#confirmPassword').css('border-color', 'red');
    }
   
}

function CompleteSignup() {
    var mobile = $("#mobile").val()
    var code = $("#code").val()
    var data = { 'mobile': mobile, 'code': code }
    $.post("/accunt/CompleteSignup",
        data, function (result) {
            if (result == "ok") {
                window.location.replace("/home/about");
            }
        });
}

function ForgotPassword() {
    var mobile = $("#mobile").val()
    var data = { 'mobile': mobile}
    $.post("/accunt/ForgotPassword",
        data, function (result) {
            if (result == "ok") {
                window.location.replace("/home/about");
            }
        });
}

function ConfirmNewPassword() {
    var mobile = $("#mobile").val()
    var password = $("#password").val()
    var code = $("#code").val()
    var confirmPassword = $("#confirmPassword").val()
    var data = { 'mobile': mobile, 'newPassword': password, 'code': code }
    if (password == confirmPassword) {
        $.post("/accunt/ConfirmNewPassword",
            data, function (result) {
                alert(result);
            });
    }
    else {
        $('#password').css('border-color', 'red');
        $('#confirmPassword').css('border-color', 'red');
    }
}
