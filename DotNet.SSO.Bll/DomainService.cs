using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IBll;
using DotNet.SSO.Model;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.Bll
{
    /// <summary>
    /// 说明:域的业务
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:DomainService.cs
    /// </summary>
    public class DomainService : IDomainService
    {
        #region 添加域的实体
        /// <summary>
        /// 添加域的实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<Domain> AddDomain(DomainAddModel model)
        {
            JsonModel<Domain> jsonModel = new JsonModel<Domain>()
            {
                Success = false,
                ErrMsg = "添加失败",
                SuccessMsg = "添加成功"
            };
            try
            {
                //对实体进行验证
                var validate = DotNet.Utils.DataValidate.ValidateHelper<DomainAddModel>.ValidateModel(model);
                if (!validate.Pass)
                {
                    jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                    return jsonModel;
                }

                //过滤
                model.DomainName = DotNet.Utils.Untility.StringHelper.FilterHtml(model.DomainName);
                model.ReMark = DotNet.Utils.Untility.StringHelper.FilterHtml(model.ReMark);
                //生成当前域的级别
                int domainLevel = model.DomainLevel;
                int parentDomainId = model.ParentDomainId;
                BllUtility.DomainHandler.GetDomainLevel(ref domainLevel, ref parentDomainId);

                string domainKey = BllUtility.DomainHandler.GetDomainKey();
                string domainCode = BllUtility.DomainHandler.GetDomainCode();
                string domainPassword = BllUtility.DomainHandler.EncryptDomainPassword(model.DomainPassword, domainCode, domainKey);
                //构造实体
                Domain domain = new Domain()
                {
                    DomainName = model.DomainName,
                    DomainCode = domainCode,
                    DomainUrl = model.DomainUrl,
                    DomainLevel = domainLevel,
                    ParentDomainId = parentDomainId,
                    IsEnabled = model.IsEnabled,
                    IsSSO = model.IsSSO,
                    SSOUrl = model.SSOUrl,
                    DomainKey = domainKey,
                    DomainPassword = domainPassword,
                    CookieDomain = model.CookieDomain,
                    DelFlag = (int)DelFlagEnum.Noraml,
                    ReMark = model.ReMark,
                    SSOPoolPoolId = model.SSOPoolPoolId
                };

                IDomainDal domainDal = new DomainDal();
                var r = domainDal.AddEntity(domain);
                if (r != null)
                {
                    jsonModel.Success = true;
                    jsonModel.Data = r;
                }
                else
                {
                    jsonModel.ErrMsg = "数据插入失败";
                }
            }
            catch (Exception ex)
            {
                jsonModel.ErrMsg = ex.Message;
            }
            return jsonModel;
        }
        #endregion

        #region 分页查询域
        /// <summary>
        /// 分页查询域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<Domain> GetPagingDomain(DomainParam parameter)
        {
            IDomainDal domainDal = new DomainDal();
            var paging = domainDal.GetPaging(parameter);

            return paging;
        }
        #endregion

        #region 根据参数查询所有的域
        /// <summary>
        /// 根据参数查询所有的域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Domain> GetListDomain(DomainParam parameter)
        {
            List<Domain> domainList = new List<Domain>();
            IDomainDal domainDal = new DomainDal();
            //过滤参数
            parameter.DomainName = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.DomainName);
            domainList = domainDal.GetList(parameter);
            return domainList;
        }
        #endregion

        #region 开启或者关闭域
        /// <summary>
        /// 开启或者关闭域
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public JsonModel<string> ChangeDomainEnabled(int domainId, int isEnabled)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "操作失败",
                SuccessMsg = "操作成功"
            };
            IDomainDal domainDal = new DomainDal();
            var domain = domainDal.GetEntity(domainId);
            if (domain == null || domain.DomainId == 0)
            {
                jsonModel.ErrMsg = "当前域不存在";
                return jsonModel;
            }
            if (!Enum.IsDefined(typeof(IsEnabledEnum), isEnabled))
            {
                jsonModel.ErrMsg = "域的状态不正确";
                return jsonModel;
            }
            domain.IsEnabled = isEnabled;
            var r = domainDal.UpdateEntity(domain);
            if (r != null && r.DomainId > 0)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion

        #region 删除域
        /// <summary>
        /// 删除域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public JsonModel<string> DeleteDomain(int domainId)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "删除失败",
                SuccessMsg = "删除成功"
            };

            IDomainDal domainDal = new DomainDal();
            var domain = domainDal.GetEntity(domainId);
            if (domain == null)
            {
                jsonModel.ErrMsg = "该域不存在";
                return jsonModel;
            }
            domain.DelFlag = (int)DelFlagEnum.LogicalDelete;
            var r = domainDal.UpdateEntity(domain);
            if (r != null && r.DomainId > 0)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion

        #region 根据唯一参数查询出域
        /// <summary>
        /// 根据唯一参数查询域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public Domain GetDomain(DomainSingleParam parameter)
        {
            IDomainDal domainDal = new DomainDal();
            //过滤
            parameter.DomainCode = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.DomainCode);
            var domain = domainDal.GetEntity(parameter);
            return domain;
        }
        #endregion

        #region 查询某个池子下的所有域
        /// <summary>
        /// 查询某个池子下的所有域
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public List<Domain> GetPoolDomain(int poolId)
        {
            List<Domain> domainList = new List<Domain>();
            IDomainDal domainDal = new DomainDal();
            domainList = domainDal.GetPoolDomain(poolId);
            return domainList;
        }
        #endregion

        #region 查询需要修改的域实体
        /// <summary>
        /// 查询需要修改的域实体
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public DomainEditModel GetEditModel(int domainId)
        {
            IDomainDal domainDal = new DomainDal();
            DomainEditModel editModel = new DomainEditModel();
            var domain = domainDal.GetEntity(domainId);
            if (domain != null)
            {
                editModel = new DomainEditModel()
               {
                   DomainId = domain.DomainId,
                   DomainName = domain.DomainName,
                   //DomainPassword=BllUtility.DomainHandler.DecryptDomainPassword(domain.DomainPassword,domain.DomainCode,domain.DomainKey),
                   DomainUrl = domain.DomainUrl,
                   DomainLevel = domain.DomainLevel,
                   IsEnabled = domain.IsEnabled,
                   IsSSO = domain.IsSSO,
                   SSOUrl = domain.SSOUrl,
                   CookieDomain = domain.CookieDomain,
                   ParentDomainId = domain.ParentDomainId,
                   ReMark = domain.ReMark,
                   SSOPoolPoolId = domain.SSOPoolPoolId
               };
            }
            return editModel;
        }
        #endregion

        #region 修改域
        /// <summary>
        /// 修改域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<Domain> EditDomain(DomainEditModel model)
        {
            JsonModel<Domain> jsonModel = new JsonModel<Domain>()
            {
                Success = false,
                ErrMsg = "修改失败",
                SuccessMsg = "修改成功"
            };
            //对实体进行验证
            var validate = DotNet.Utils.DataValidate.ValidateHelper<DomainEditModel>.ValidateModel(model);
            if (!validate.Pass)
            {
                jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                return jsonModel;
            }
            //字符过滤
            model.ReMark = DotNet.Utils.Untility.StringHelper.FilterHtml(model.ReMark);
            IDomainDal domainDal = new DomainDal();
            var dbDomain = domainDal.GetEntity(model.DomainId);
            if (dbDomain == null)
            {
                jsonModel.ErrMsg = "当前域不存在";
                return jsonModel;
            }
            //先判断当前域的级别,如果为1级,则没有上一级,否则,就检测上一级是否存在
            if (model.DomainLevel > 1)
            {
                var parentDomain = domainDal.GetEntity(model.ParentDomainId);
                if (parentDomain == null)
                {
                    jsonModel.ErrMsg = "父域不存在";
                    return jsonModel;
                }
            }
            else
            {
                model.DomainLevel = 1;
                model.ParentDomainId = 0;
            }
            int oldPoolId = dbDomain.SSOPoolPoolId;
            //检测单点登录池是否存在
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var pool = ssoPoolDal.GetEntity(model.SSOPoolPoolId);
            if (pool == null)
            {
                jsonModel.ErrMsg = "你选择的单点登录池不存在";
                return jsonModel;
            }

            #region 生成修改的属性
            //域密码
            //string encryptPassword = BllUtility.DomainHandler.EncryptDomainPassword(model.DomainPassword,dbDomain.DomainCode,dbDomain.DomainKey);

            dbDomain.DomainName = model.DomainName;
            dbDomain.DomainUrl = model.DomainUrl;
            dbDomain.DomainLevel = model.DomainLevel;
            dbDomain.ParentDomainId = model.ParentDomainId;
            dbDomain.CookieDomain = model.CookieDomain;
            dbDomain.IsEnabled = model.IsEnabled;
            dbDomain.IsSSO = model.IsSSO;
            dbDomain.SSOUrl = model.SSOUrl;
            dbDomain.ReMark = model.ReMark;
            dbDomain.SSOPoolPoolId = model.SSOPoolPoolId;
            //   dbDomain.DomainPassword = encryptPassword;
            #endregion

            var r = domainDal.UpdateEntity(dbDomain);
            if (r != null && r.DomainId > 0)
            {
                jsonModel.Success = true;
                jsonModel.Data = r;
            }
            //最后,判断是否修改了池子
            if (oldPoolId != dbDomain.SSOPoolPoolId)
            {
                //判断池子的主域是否是这个
                if (pool.MainDomainId == oldPoolId)
                {
                    pool.MainDomainId = 0;
                    ssoPoolDal.UpdateEntity(pool);
                }
            }
            return jsonModel;
        }
        #endregion

    }
}
