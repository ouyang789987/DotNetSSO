using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.BllUtility;
using DotNet.SSO.IBll;
using DotNet.SSO.Bll.Manager;
using DotNet.SSO.Model;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class LoginController : Controller
    {
        //登录显示页面
        // GET: /Manager/Login/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //验证码
        // GET: /Manager/Login/LoginCode
        [HttpGet]
        public ActionResult LoginCode()
        {
            var bytes = BllUtility.ManagerHandler.GetLoginCode();
            return File(bytes, "image/jpg");
        }

        // POST: /Manager/Login/Logon
        [HttpPost]
        public ActionResult Logon()
        {
            ManagerLoginModel loginModel = new ManagerLoginModel()
            {
                LoginName = HttpUtility.HtmlEncode(Request["loginName"]),
                LoginPwd = HttpUtility.HtmlEncode(Request["loginPwd"]),
                ImageCode = HttpUtility.HtmlEncode(Request["imgCode"])
            };
            IManagerService managerService = new ManagerService();
            JsonModel<string> jsonModel = managerService.ManagerLogin(loginModel);
            if (jsonModel.Success)
            {
                return Json(new { success=true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
