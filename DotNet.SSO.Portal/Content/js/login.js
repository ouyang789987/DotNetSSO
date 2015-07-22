/*
 登录界面
 */
$(function () {
    getVerify();
    $("#codeimg img").click(function () {
        getVerify();
    });
    $('#account_login').click(function () {
        var loginName = $('#loginName').val();
        var loginPwd = $('#loginPwd').val();
        var imgCode = $('#imgCode').val();
        if (loginName == "") {
            MAlert("用户名不能为空");
            return;
        }
        if (loginPwd == "") {
            MAlert("登录密码不能为空");
            return;
        }
        if (imgCode == "") {
            MAlert("验证码不能为空");
            return;

        }
        btnLoging();
        $.ajax({
            type: "Post",
            url: "/Manager/Login/Logon",
            dataType: "json",
            data: { loginName: loginName, loginPwd: loginPwd, imgCode: imgCode },
            success: function (data) {
                if (data.success == true) {
                    MAlert("登录成功,正在为您跳转!");
                    setTimeout(function () {
                        window.location.href = '/Manager/Home/Index'; //登录后跳转
                    }, 1000);
                } else {
                    setTimeout(function () {
                        MAlert(data.msg);
                        reshow();
                        getVerify();
                    }, 1000);
                }
            },
            error: function () {
                setTimeout(function () {
                    MAlert("出现异常了!");
                    reshow();
                    getVerify();
                }, 1000);
            }
        });
    });
});
//获取验证码
function getVerify() {
    $("#codeimg img").attr('src', "/Manager/Login/LoginCode?r=" + Math.random());
}
//提示
function MAlert(msg) {
    $('.loginmsg').text(msg)
    $('.nousername').fadeIn();
}
//正在登陆
function btnLoging() {
    $("#account_login").attr("disabled", true).html("正在登录中<span class='dotting '></span>");
}
function reshow() {
    $("#account_login").attr("disabled", false).html("登录");
}
