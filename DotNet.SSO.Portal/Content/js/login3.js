/*
 登录界面
 */
$(function(){
	$('#oa_login').click(function(){
	var username=$('#username').val(),userpassword= $('#password').val();
		if(username=="") {
			$('.loginmsg').text("用户名不能为空！")
			$('.nousername').fadeIn();
			return false;	
		}else if(userpassword==""){
			$('.loginmsg').text("密码不能为空！")			
			$('.nousername').fadeIn();
			
		}else if(code==""){
			$('.loginmsg').text("验证码不能为空！")			
			$('.nousername').fadeIn();
			
		}else{
			$("#oa_login").attr("disabled", true).html("正在登录中<span class='dotting '></span>");
			if(username=="1" && userpassword=="1"){
				setTimeout(function(){
					window.location.href = 'index.html';//登录后跳转
				},1000);	
			}else{
				setTimeout(function(){
					$('.loginmsg').text("用户名或密码错误！");
					$('.nousername').fadeIn();
					$("#oa_login").attr("disabled", false).html("登录");
				},1000);
			}

		}
	});

});
