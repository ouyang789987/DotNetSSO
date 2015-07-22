//删除登录池
function delpool(id) {
    jConfirm("确定要删除？", "提示框？", function (r) {
        $.ajax({
            type: "Post",
            url: "/Manager/SSOPool/SSOPoolDelete",
            dataType: "json",
            data: { id: id},
            success: function (data) {
                if (data.success == true) {
                    jAlert('删除成功！', "提示框", function () {
                        location.reload();
                    });
                } else {
                    jAlert('删除失败！'+data.msg, "提示框", function () {
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
}

//启用池
function OpenPool(id) {
    var param = {
        poolId: id,
        isEnabled: 1
    };
    ChangePoolEnabled(param);
};
//禁用池
function ClosePool(id) {
    var param = {
        poolId: id,
        isEnabled: 2
    };
    ChangePoolEnabled(param);
};
//改变池子的状态
function ChangePoolEnabled(json) {
    $.ajax({
        type: "Post",
        url: "/Manager/SSOPool/ChangePoolEnabled",
        dataType: "json",
        data: json,
        success: function (data) {
            jAlert(data.msg, "提示框", function () {
                location.reload();
            });
        },
        error: function () {
            jAlert('出现异常', "提示框", function () {
            });
        }
    });
};

//在添加单点登录池提交完成之后回调函数
function AfterPoolAdd(data) {
    endLoading();
    if (data.success) {
        jAlert('添加成功!', "提示框", function () {
            window.location.href = "/Manager/SSOPool/SSOPoolList";
        });
    } else {
        jAlert(data.msg, "提示框", function () {
//            window.location.href = "/Manager/SSOPool/SSOPoolList";
        });
    }
};
//在修改单点登录池之后回调函数
function AfterPoolEdit(data) {
    endLoading();
    if (data.success) {
        jAlert('修改成功!', "提示框", function () {
            window.location.href = "/Manager/SSOPool/SSOPoolList";
        });
    } else {
        jAlert(data.msg, "提示框", function () {
        });
    }
};