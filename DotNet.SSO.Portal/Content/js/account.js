//删除帐号
function delDomain(accountId) {
    jConfirm("确定要删除？", "提示框？", function (r) {
        $.ajax({
            type: "Post",
            url: "/Manager/Account/AccountDelete",
            dataType: "json",
            data: { accountId: accountId },
            success: function (data) {
                if (data.success == true) {
                    jAlert('删除成功！', "提示框", function () {
                        location.reload();
                    });
                } else {
                    jAlert(data.msg, "提示框", function () {
                        location.reload();
                    });
                }
            },
            error: function () {
                jAlert('删除失败！出现异常', "提示框", function () {
                });
            }
        });
    });
};
//添加帐号之后回调方法
function AfterAccountAdd(data) {
    endLoading();
    if (data.success) {
        jAlert('添加成功!', "提示框", function () {
            window.location.href = "/Manager/Account/AccountList?domainId="+data.domainId;
        });
    } else {
        jAlert(data.msg, "提示框", function () {
            //window.location.href = "/Manager/Domain/DomainList";
        });
    }
};