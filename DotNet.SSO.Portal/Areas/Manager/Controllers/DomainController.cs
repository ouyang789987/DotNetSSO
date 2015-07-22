using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.IBll;
using DotNet.SSO.Bll;
using DotNet.SSO.Model.Parameter;
//using DotNet.SSO.IDal;
//using DotNet.SSO.Dal;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Enum;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class DomainController : Controller
    {
        // GET: /Manager/Domain/DomainList
        #region 域管理列表
        [HttpGet]
        public ActionResult DomainList()
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

        // GET,POST /Manager/Domain/DomainAdd
        #region 添加域
        [HttpGet]
        public ActionResult DomainAdd()
        {
            List<SelectListItem> selectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="一级域名",Value="1",Selected=true },
                new SelectListItem(){ Text="二级域名",Value="2"},
                new SelectListItem(){ Text="三级域名",Value="3" }
            };
            ViewData["DomainLevel"] = selectList;
            List<SelectListItem> poolSelectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="请选择",Value="0",Selected=true }
            };
            ISSOPoolService service = new SSOPoolService();
            var poolList = service.GetSSOPoolList();
            foreach (var item in poolList)
            {
                SelectListItem selectItem = new SelectListItem()
                {
                    Text = item.PoolName,
                    Value = item.PoolId.ToString()
                };
                poolSelectList.Add(selectItem);
            }
            ViewData["SSOPoolPoolId"] = poolSelectList;


            return View();
        }
        //添加域,提交
        [HttpPost]
        public ActionResult DomainAdd(DomainAddModel addModel)
        {
            IDomainService service = new DomainService();
            var jsonModel = service.AddDomain(addModel);
            if (jsonModel.Success)
            {
                return Json(new { success = true, msg = "添加成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        // GET /Manager/Domain/LoadLevelDomain
        #region 加载某一个级别的所有域
        [HttpGet]
        public ActionResult LoadLevelDomain(int level)
        {
            IDomainService service = new DomainService();
            var domainList = service.GetListDomain(new DomainParam() { DomainLevel=level });
            return Json(domainList, JsonRequestBehavior.AllowGet);
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
            string domainCode= HttpUtility.HtmlEncode(Request["domainCode"]);
            string domainLevel=HttpUtility.HtmlEncode(Request["domainLevel"]);
            string domainUrl=HttpUtility.UrlEncode(Request["domainUrl"]);
            string isSSO = HttpUtility.UrlEncode(Request["isSSO"]);
            return Redirect(string.Format("/Manager/Domain/DomainList?domainName={0}&isEnabled={1}&domainCode={2}&domainLevel={3}&domainUrl={4}&isSSO={5}", domainName, isEnabled, domainCode, domainLevel, domainUrl, isSSO));
        } 
        #endregion

        // POST: /Manager/Domain/ChangeDomainEnabled
        #region 开启或者关闭域名
        [HttpPost]
        public ActionResult ChangeDomainEnabled()
        {
            int domainId = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["domainId"]), 0);
            int isEnabled = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["isEnabled"]), (int)IsEnabledEnum.Enabled);
            IDomainService service = new DomainService();
            var jsonModel = service.ChangeDomainEnabled(domainId, isEnabled);
            if (jsonModel.Success)
            {
                return Json(new { success = true, msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }  
        #endregion

        // POST: /Manager/Domain/DomainDelete
        #region 删除域
        [HttpPost]
        public ActionResult DomainDelete(int domainId)
        {
            IDomainService service = new DomainService();
            var jsonModel = service.DeleteDomain(domainId);
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

        //GET :/Manager/Domain/DomainDisplay
        #region 显示域的详情
        [HttpGet]
        public ActionResult DomainDisplay()
        {
            int domainId = DotNet.Utils.Untility.ConvertFactory.ToInt32(Request["domainId"], 0);
            IDomainService service = new DomainService();
            var domain = service.GetDomain(new DomainSingleParam() { DomainId=domainId });
            return View(domain);
        }
        #endregion

        //GET,POST :/Manager/Domain/DomainEdit
        #region 修改域信息
        [HttpGet]
        public ActionResult DomainEdit(int domainId)
        {
            IDomainService service = new DomainService();
            var model = service.GetEditModel(domainId);
            //域级别select
            List<SelectListItem> levelSelectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="一级域名",Value="1",Selected=(model.DomainLevel==1)},
                new SelectListItem(){ Text="二级域名",Value="2",Selected=(model.DomainLevel==2)},
                new SelectListItem(){ Text="三级域名",Value="3",Selected=(model.DomainLevel==3)},
            };
            //上级域名select
            List<SelectListItem> parentSelectList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="无",Value="0",Selected=(model.ParentDomainId==0)}
            };
            if (model.DomainLevel > 1)
            {
                var domainList = service.GetListDomain(new DomainParam() { DomainLevel = model.DomainLevel - 1 });
                foreach (var domain in domainList)
                {
                    SelectListItem item = new SelectListItem()
                    {
                        Text = domain.DomainName,
                        Value = domain.DomainId.ToString(),
                        Selected = (model.ParentDomainId == domain.DomainId)
                    };
                    parentSelectList.Add(item);
                }
            }
            //单点登录池的select
            List<SelectListItem> poolSelectList = new List<SelectListItem>();
            ISSOPoolService ssoPoolService = new SSOPoolService();
            var poolList = ssoPoolService.GetSSOPoolList();
            foreach (var pool in poolList)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text=pool.PoolName,
                    Value=pool.PoolId.ToString(),
                    Selected=(model.SSOPoolPoolId==pool.PoolId)
                };
                poolSelectList.Add(item);
            }

            this.ViewData["DomainLevel"] = levelSelectList;
            this.ViewData["ParentDomainId"] = parentSelectList;
            this.ViewData["SSOPoolPoolId"] = poolSelectList;
            return View(model);
        }

        [HttpPost]
        public ActionResult DomainEdit(DomainEditModel editModel)
        {
            IDomainService service = new DomainService();
            var jsonModel = service.EditDomain(editModel);
            if (jsonModel.Success)
            {
                return Json(new { success = true, msg = "修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


    }
}
