using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.IBll;
using DotNet.SSO.Bll;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Enum;
using System.Threading;
using DotNet.SSO.Model;
using DotNet.SSO.Portal.CustomsAttribute;

namespace DotNet.SSO.Portal.Areas.Manager.Controllers
{
    public class SSOPoolController : Controller
    {


        // GET: /Manager/SSOPool/SSOPoolList
        #region 单点登录池展示页面
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult SSOPoolList()
        {
           
            List<SelectListItem> selectItem = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="全部",Value="0",Selected=true },
                new SelectListItem(){ Text="已启用",Value=((int)Model.Enum.IsEnabledEnum.Enabled).ToString() },
                new SelectListItem(){ Text="未启用",Value=((int)Model.Enum.IsEnabledEnum.Disabled).ToString() }
            };
            ViewBag.Select = selectItem;
            ISSOPoolService service = new SSOPoolService();
            SSOPoolParam parameter = new SSOPoolParam()
            {
                PageIndex = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["page"]), 1),
                PageSize =12,
                IsEnabled = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.UrlEncode(Request["isEnabled"]), 0),
                PoolName = HttpUtility.HtmlEncode(Request["poolName"]),
                IsPaging = true,
                PoolId = 0
            };
            var model = service.GetPagingModel(parameter);
            return View(model);
        } 
        #endregion

        // HttpPost: /Manager/SSOPool/SSOPoolSearch
        #region 单点登录池搜索
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SSOPoolSearch()
        {
            string poolName = HttpUtility.HtmlEncode(Request["poolName"]);
            string isEnabled = HttpUtility.HtmlEncode(Request["isEnabled"]);
            return Redirect(string.Format("/Manager/SSOPool/SSOPoolList?poolName={0}&isEnabled={1}", poolName, isEnabled));
        } 
        #endregion

        // Get: /Manager/SSOPool/SSOPoolAdd
        #region 添加单点登录池
        [HttpGet]
        public ActionResult SSOPoolAdd()
        {
            return View();
        }
       
        //提交
        [HttpPost]
        public ActionResult SSOPoolAdd(SSOPoolAddModel addModel)
        {
            ISSOPoolService service = new SSOPoolService();
            var jsonModel = service.AddSSOPool(addModel);
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

        // POST: /Manager/SSOPool/SSOPoolDelete
        #region 删除单点登录池
        [HttpPost]
        public ActionResult SSOPoolDelete(int id)
        {
            ISSOPoolService service = new SSOPoolService();
            var jsonModel = service.DeleteSSOPool(id);
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

        // POST: /Manager/SSOPool/ChangePoolEnabled
        #region 开始或者关闭池子
        [HttpPost]
        public ActionResult ChangePoolEnabled()
        {
            int poolId = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["poolId"]),0);
            int isEnabled = DotNet.Utils.Untility.ConvertFactory.ToInt32(HttpUtility.HtmlEncode(Request["isEnabled"]),(int)IsEnabledEnum.Enabled);
            ISSOPoolService service = new SSOPoolService();
            var jsonModel = service.ChangeSSOPoolEnabled(poolId,isEnabled);
            if (jsonModel.Success)
            {
                return Json(new { success=true,msg="操作成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, msg = jsonModel.ErrMsg }, JsonRequestBehavior.AllowGet);
            }
        } 
        #endregion

        // GET: /Manager/SSOPool/SSOPoolEdit
        #region 修改单点登录池
        [HttpGet]
        public ActionResult SSOPoolEdit(int poolId)
        {
            ISSOPoolService service = new SSOPoolService();
            var model = service.GetEditModel(poolId);
            //主要的域名验证的 select
            List<SelectListItem> domainSelectList = new List<SelectListItem>();
            IDomainService domainService = new DomainService();
            var domainList = domainService.GetPoolDomain(poolId);
            domainSelectList.Add(new SelectListItem()
            {
                Text="无",
                Value="0",
                Selected=(model.MainDomainId==0)
            });
            foreach (var domain in domainList)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text=domain.DomainName,
                    Value=domain.DomainId.ToString(),
                    Selected=(domain.DomainId==model.MainDomainId)
                };
                domainSelectList.Add(item);
            }
            this.ViewData["MainDomainId"] = domainSelectList.AsEnumerable();
            return View(model);
        }
        //修改单点登录池提交
        [HttpPost]
        public ActionResult SSOPoolEdit(SSOPoolEditModel editModel)
        {
            ISSOPoolService service = new SSOPoolService();
            var jsonModel = service.EditSSOPool(editModel);
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

        //GET :/Manager/SSOPool/SSOPoolDisplay
        #region 显示单点登录池的详情
        [HttpGet]
        public ActionResult SSOPoolDisplay(int poolId)
        {
            ISSOPoolService service = new SSOPoolService();
            var displayModel = service.GetDisplayModel(poolId);

            return View(displayModel);
        } 
        #endregion
    }
}
