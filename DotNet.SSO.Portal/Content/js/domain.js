$(function () {
    //域级别改变之后,改变其他值
    $("#DomainLevel").change(function () {
        var level = parseInt($("#DomainLevel").val());
        if (level > 1) {
            var parentLevel = level - 1;
            loadLevelDomain(parentLevel);
        }
    });
//    $("input:radio[name='IsSSO']").change(function () {
//        var isSSO = $("input:radio[name='IsSSO']:checked").val();
//        if (isSSO == 1) {
//            $("#tr_ssourl").show();
//        } else {
//            $("#tr_ssourl").hide();
//        }
//    });
});
//加载某个级别的域
function loadLevelDomain(level) {
    $.ajax({
        type: "Get",
        url: "/Manager/Domain/LoadLevelDomain",
        dataType: "json",
        data: { level: level },
        success: function (data) {
            $("#ParentDomainId").html("");
            $("#ParentDomainId").append("<option value='1'>无</option>");
            $(data).each(function (idx, item) {
                $("#ParentDomainId").append("<option value=" + item.DomainId +">"+item.DomainName+"</option>");
            });
        },
        error: function () {

        }
    });
};

//删除域
function delDomain(domainId) {
    jConfirm("确定要删除？", "提示框？", function (r) {
        $.ajax({
            type: "Post",
            url: "/Manager/Domain/DomainDelete",
            dataType: "json",
            data: { domainId: domainId },
            success: function (data) {
                if (data.success == true) {
                    jAlert('删除成功！', "提示框", function () {
                        location.reload();
                    });
                } else {
                    jAlert('删除失败！' + data.msg, "提示框", function () {
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

//启用池
function OpenDomain(id) {
    var param = {
        domainId: id,
        isEnabled: 1
    };
    ChangeDomainEnabled(param);
};
//禁用池
function CloseDomain(id) {
    var param = {
        domainId: id,
        isEnabled: 2
    };
    ChangeDomainEnabled(param);
};
//改变域的状态
function ChangeDomainEnabled(json) {
    $.ajax({
        type: "Post",
        url: "/Manager/Domain/ChangeDomainEnabled",
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
//添加域名成功之后回调方法
function AfterDomainAdd(data) {
    endLoading();
    if (data.success) {
        jAlert('添加成功!', "提示框", function () {
            window.location.href = "/Manager/Domain/DomainList";
        });
    } else {
        jAlert(data.msg, "提示框", function () {
            //window.location.href = "/Manager/Domain/DomainList";
        });
    }
};
//修改域成功之后回调函数
function AfterDomainEdit(data) {
    endLoading();
    if (data.success) {
        jAlert('修改成功!', "提示框", function () {
            window.location.href = "/Manager/Domain/DomainList";
        });
    } else {
        jAlert(data.msg, "提示框", function () {
            //window.location.href = "/Manager/Domain/DomainList";
        });
    }
};