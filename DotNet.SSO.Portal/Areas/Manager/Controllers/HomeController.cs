using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: /Manager/Home/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        //工作内容
        // GET:/Manager/Home/WorkSpace
        [HttpGet]
        public ActionResult WorkSpace()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        #region 基础配置
        [HttpGet]
        public ActionResult Basic()
        {
            return View();
        } 
        #endregion


        [HttpGet]
        public ActionResult Test()
        {
            DotNet.SSO.IDal.IManagerDal managerDal = new DotNet.SSO.Dal.ManagerDal();

            DotNet.SSO.Model.Manager manager = new Model.Manager()
            {
                LoginName="admin",
                DelFlag=1,
                EncryptKey="abc123",
                LoginPwd=DotNet.Utils.Encrypt.EncryptHelper.AESEncryString("123456","abc123"),
                ReMark=""
            };
            managerDal.AddEntity(manager);

            return View();
        }

    }
}
