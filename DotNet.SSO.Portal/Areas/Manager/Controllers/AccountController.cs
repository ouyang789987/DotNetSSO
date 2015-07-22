using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.IBll;
using DotNet.SSO.Bll;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.Model.ViewModel;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Manager/Account/DomainAccount
        #region 域帐号管理,帐号都是在域下面
        [HttpGet]
        public ActionResult DomainAccountList()
        {
            IDomainService domainService = new DomainService();
            DomainParam parameter = new DomainParam()
            {
                PageIndex = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["page"]), 1),
                PageSize = 12,
                IsEnabled = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["isEnabled"]), 0),
                DomainName = HttpUtility.HtmlEncode(Request["domainName"]),
                IsPaging = true,
                DomainCode = HttpUtility.HtmlEncode(Request["domainCode"]),
                DomainLevel = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["domainLevel"]), 0),
                IsSSO = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["isSSO"]), 0),
                DomainUrl = HttpUtility.UrlEncode(Request["domainUrl"])
            };
            var pagingModel = domainService.GetPagingDomain(parameter);
            return View(pagingModel);
        }
        #endregion

        // HttpPost: /Manager/Domain/DomainSearch
        #region 域搜索
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DomainSearch()
        {
            string domainName = HttpUtility.HtmlEncode(Request["domainName"]);
            string isEnabled = HttpUtility.HtmlEncode(Request["isEnabled"]);
            string domainCode = HttpUtility.HtmlEncode(Request["domainCode"]);
            string domainLevel = HttpUtility.HtmlEncode(Request["domainLevel"]);
            string domainUrl = HttpUtility.UrlEncode(Request["domainUrl"]);
            string isSSO = HttpUtility.UrlEncode(Request["isSSO"]);
            return Redirect(string.Format("/Manager/Account/DomainAccountList?domainName={0}&isEnabled={1}&domainCode={2}&domainLevel={3}&domainUrl={4}&isSSO={5}", domainName, isEnabled, domainCode, domainLevel, domainUrl, isSSO));
        }
        #endregion


        // GET: /Manager/Account/AccountList
        #region 帐号列表
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult AccountList()
        {
            #region 域选择
            IDomainService domainService = new DomainService();
            var domainList = domainService.GetListDomain(new DomainParam() { });
            List<SelectListItem> domainSelectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="全部",Value="0",Selected=true }
            };
            foreach (var domain in domainList)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = domain.DomainName,
                    Value = domain.DomainId.ToString(),
                };
                domainSelectList.Add(item);
            }
            this.ViewData["submitDomainId"] = domainSelectList; 
            #endregion

            int submitDomainId = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["submitDomainId"]), 0);
            string openId = HttpUtility.HtmlEncode(Request["openId"]);
            string loginName = HttpUtility.HtmlEncode(Request["loginName"]);
            string mobile = HttpUtility.HtmlEncode(Request["mobile"]);
            string email = HttpUtility.HtmlEncode(Request["email"]);
            IAccountService accountService = new AccountService();
            AccountParam parameter = new AccountParam()
            {
                PageIndex = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["page"]), 1),
                PageSize = 12,
                SubmitDomainId = submitDomainId,
                IsPaging = true,
                OpenId = openId,
                LoginName=loginName,
                Mobile=mobile,
                Email=email,
                AccountId = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["accountId"]), 0)
            };
            var model = accountService.GetPagingAccount(parameter);
            return View(model);
        }
        #endregion

        // GET: /Manager/Account/AccountSearch
        #region 帐号搜索
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AccountSearch()
        {
            string openId = HttpUtility.HtmlEncode(Request["openId"]);
            string loginName = HttpUtility.HtmlEncode(Request["loginName"]);
            string mobile = HttpUtility.HtmlEncode(Request["mobile"]);
            string email = HttpUtility.HtmlEncode(Request["email"]);
            int submitDomainId =DotNet.Utils.Untility.ConvertFactory.ToInt32(Request["submitDomainId"],0);
            return Redirect(string.Format("/Manager/Account/AccountList?openId={0}&loginName={1}&mobile={2}&email={3}&submitDomainId={4}", openId, loginName, mobile, email, submitDomainId));
        }
        #endregion

        // GET: /Manager/Account/AccountAdd
        #region 添加某个域登录的会员帐号
        [HttpGet]
        public ActionResult AccountAdd()
        {
            IDomainService domainService = new DomainService();
            var domainList = domainService.GetListDomain(new DomainParam() { });
            List<SelectListItem> domainSelectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="请选择",Value="0",Selected=true }
            };
            foreach (var domain in domainList)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text=domain.DomainName,
                    Value=domain.DomainCode
                };
                domainSelectList.Add(item);
            }
            this.ViewData["SubmitDomainCode"] = domainSelectList;
            return View();
        }
        //添加帐号提交
        [HttpPost]
        public ActionResult AccountAdd(AccountAddModel addModel)
        {
            IAccountService accountService = new AccountService();
            var jsonModel = accountService.AddAccount(addModel);
            if (jsonModel.Success)
            {
                return Json(new { success = true, msg = "添加成功", openId=jsonModel.Data.OpenId }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        // POST: /Manager/Account/AccountDelete
        #region 删除单点登录的帐号
        [HttpPost]
        public ActionResult AccountDelete(int accountId)
        {
            IDomainService domainService = new DomainService();
            IAccountService service = new AccountService();
            var jsonModel = service.DeleteAccount(new AccountDeleteModel() { AccountId = accountId });
            if (jsonModel.Success)
            {
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

         // GET: /Manager/Account/AccountDisplay
        #region 帐号显示
        [HttpGet]
        public ActionResult AccountDisplay()
        {
            int accountId = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["accountId"]), 0);
            IAccountService service = new AccountService();
            var model = service.GetAccount(new AccountSingleParam() { AccountId = accountId });
            IDomainService domainService = new DomainService();
            var subdomain = domainService.GetDomain(new DomainSingleParam() { DomainId=model.SubmitDomainId });
            ViewBag.SubmitDomain = subdomain;
            return View(model);
        }
        #endregion


        #region 修改用户的
        [HttpGet]
        public ActionResult AccountEdit(int accountId)
        {
            return View();
        } 
        #endregion

    }
}
