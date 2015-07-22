using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.Model;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class CommonController : Controller
    {

        #region 顶部导航加头部
        //顶部导航
        [ChildActionOnly]
        [HttpGet]
        public ActionResult TopNav()
        {
            return PartialView();
        } 
        #endregion

        #region 头部导航
        [ChildActionOnly]
        [HttpGet]
        public ActionResult TopHeader()
        {
            ViewBag.CurrentManager = new DotNet.SSO.Model.Manager()
            {
                LoginName="测试帐号",
                ManagerId=1
            };
            return PartialView();
        } 
        #endregion


        #region 加载左侧
        [HttpGet]
        public ActionResult LoadLeft()
        {
            int menuId = DotNet.Utils.Untility.ConvertFactory.ToInt32(Request["menuId"], 0);
            if (menuId == 1)
            {
                WebMenu menu1 = new WebMenu()
                {
                    Id = 1,
                    MenuName = "单点登录配置",
                    Level = 1,
                    ParentMenuId = 0,
                    SubMenus = new List<WebMenu>()
                    {
                        new WebMenu(){ Id=11,MenuName="单点登录池管理",Level=2,ParentMenuId=1,Url="/Manager/SSOPool/SSOPoolList"},
                        new WebMenu(){ Id=12,MenuName="域管理",Level=2,ParentMenuId=1,Url="/Manager/Domain/DomainList"}
                    }
                };
                //WebMenu menu3 = new WebMenu()
                //{
                //    Id = 33,
                //    MenuName = "测试",
                //    Level = 1,
                //    ParentMenuId = 0,
                //    SubMenus = new List<WebMenu>()
                //    {
                //        new WebMenu(){ Id=3,MenuName="测试",Level=2,ParentMenuId=1,Url="/Manager/ttt/ttt1"},
                //        new WebMenu(){ Id=4,MenuName="测试",Level=2,ParentMenuId=1,Url="/Manager/ttt/ttt2"}
                //    }
                //};
                List<WebMenu> menuList1 = new List<WebMenu>();
                menuList1.Add(menu1);
                //menuList1.Add(menu3);
                return Json(menuList1,JsonRequestBehavior.AllowGet);
            }
            else if (menuId == 2)
            {
                WebMenu menu2 = new WebMenu()
                {
                    Id = 2,
                    MenuName = "用户帐号配置",
                    Level = 1,
                    ParentMenuId = 0,
                    SubMenus = new List<WebMenu>()
                    {
                        new WebMenu(){ Id=21,MenuName="帐号管理",Level=2,ParentMenuId=2,Url="/Manager/Account/AccountList"}
                    }
                };
                List<WebMenu> menuList2 = new List<WebMenu>();
                menuList2.Add(menu2);
                return Json(menuList2, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);

        } 
        #endregion



    }
}
