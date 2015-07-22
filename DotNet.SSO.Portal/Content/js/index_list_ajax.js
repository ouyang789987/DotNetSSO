$(function () {
    //显示二级分类
    $(document).on("click", ".left_nav > ul li a", function () {
        var obj = $(this).next("ul");
        if ($(obj).css("display") == "none") {
            obj.show(300);
        } else {
            obj.hide(300);
        }
    });
});

//加载导航
function loadLeft(id) {
    $.ajax({
        type: "Get",
        url: "/Manager/Common/LoadLeft",
        dataType: "json",
        data: { menuId: id },
        success: function (data) {
            var list_box = "";
            $.each(data, function (idx, menu) {
                list_box += '<li class="one_list"><a href="javascript:;" class="tables">' + menu.MenuName + '<span class="arrow"></span></a><ul>';
                $.each(menu.SubMenus, function (idx2, item) {
                    list_box += '<li><a href="javascript:;" onclick=\"create_ifr(\'' + item.Id + "\',\'" + item.Url + "\',\'" + item.MenuName + '\')\" >' + item.MenuName + '</a></li>';
                });
                list_box += '</ul></li>';
            });
            $("#if_url ul").html(list_box);
        },
        error: function () {
        }
    });
};

//创建iframe
function create_ifr(Id, Url, MenuName) {
    if (document.getElementById("div_" + Id) == null) {
        //创建iframe
        var ifrbox = $("<iframe id=\"div_" + Id + "\" src=\"" + Url + "\"></iframe>");
        $("#iframeleft").append(ifrbox);


        //li和iframe 的数量
        var tablist = document.getElementById("ifr_libox").getElementsByTagName('li');
        var pannellist = document.getElementById("iframeleft").getElementsByTagName('iframe');
        if (tablist.length > 0) {
            for (i = 0; i < tablist.length; i++) {
                tablist[i].className = "";
                pannellist[i].style.display = "none";
            }
        }
        //创建li菜单

        //刷新的时候,刷新Url,并且刷新当前页面
        var tab = $("<li onclick=\"change_tab('" + Id + "','" + Url + "','" + MenuName + "')\" class=\"action\" id=\"li_" + Id + "\">" + MenuName + "<i onclick=\"del_ifr('" + Id + "')\" title=\"关闭当前窗口\" class=\"ir\"></i><i onclick=\"reload_ifr('" + Id + "')\" title=\"刷新页面\" class=\"il\"></i></li>")

        $("#ifr_libox").append(tab);
    } else {
        var tablist = document.getElementById("ifr_libox").getElementsByTagName('li');
        var pannellist = document.getElementById("iframeleft").getElementsByTagName('iframe');
        //alert(tablist.length);
        for (i = 0; i < tablist.length; i++) {
            tablist[i].className = "";
            pannellist[i].style.display = "none"
        }
        document.getElementById("li_" + Id).className = 'action';
        document.getElementById("div_" + Id).style.display = 'block';
        document.getElementById("div_" + Id).src = Url;
    }
}
//切换tab
function change_tab(Id) {
    var tablist = document.getElementById("ifr_libox").getElementsByTagName('li');
    var pannellist = document.getElementById("iframeleft").getElementsByTagName('iframe');
    //alert(tablist.length);
    for (i = 0; i < tablist.length; i++) {
        tablist[i].className = "";
        pannellist[i].style.display = "none"
    }
    document.getElementById("li_" + Id).className = 'action';
    document.getElementById("div_" + Id).style.display = 'block';
}
//刷新iframe
function reload_ifr(Id) {
    document.getElementById("div_" + Id).contentWindow.location.reload();
    stopPropagation(event);
}
//删除iframe
function del_ifr(Id) {
    $("#li_" + Id).remove();
    $("#div_" + Id).remove();
    var tablist = document.getElementById("ifr_libox").getElementsByTagName('li');
    var pannellist = document.getElementById("iframeleft").getElementsByTagName('iframe');
    if (tablist.length > 0) {
        tablist[tablist.length - 1].className = 'action';
        pannellist[tablist.length - 1].style.display = 'block';
    }
    stopPropagation(event);
}
//阻止冒泡
function stopPropagation(e) {
    e = e || window.event;
    if (e.stopPropagation) { //W3C阻止冒泡方法  
        e.stopPropagation();
    } else {
        e.cancelBubble = true; //IE阻止冒泡方法  
    }
}

