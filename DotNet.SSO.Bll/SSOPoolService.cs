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
    /// 说明:单点登录池的业务
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:SSOPoolService.cs
    /// </summary>
    public class SSOPoolService : ISSOPoolService
    {
        #region 添加一个单点登录池
        /// <summary>
        /// 添加一个单点登录池
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<string> AddSSOPool(SSOPoolAddModel model)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                SuccessMsg = "添加成功",
                ErrMsg = "添加失败"
            };
            //对实体进行验证
            var validate = DotNet.Utils.DataValidate.ValidateHelper<SSOPoolAddModel>.ValidateModel(model);
            if (!validate.Pass)
            {
                jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                return jsonModel;
            }
            //字符过滤
            model.ReMark = DotNet.Utils.Untility.StringHelper.FilterHtml(model.ReMark);
            //判断主域是否存在
            IDomainDal domainDal = new DomainDal();
            if (model.MainDomainId > 0)
            {
                var domain = domainDal.GetEntity(model.MainDomainId);
                if (domain == null)
                {
                    jsonModel.ErrMsg = "主域不存在";
                    return jsonModel;
                }
            }
            //构建实体
            SSOPool pool = new SSOPool()
            {
                PoolName = model.PoolName,
                IsEnabled = model.IsEnabled,
                MaxAmount = model.MaxAmount,
                MainDomainId = model.MainDomainId,
                DelFlag = (int)DelFlagEnum.Noraml,
                ReMark = model.ReMark
            };
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var r = ssoPoolDal.AddEntity(pool);
            if (r != null)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion

        #region 根据池子Id查询池子
        /// <summary>
        ///  根据池子Id查询池子
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public SSOPool GetSSOPool(int poolId)
        {
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var ssoPool = ssoPoolDal.GetEntity(poolId);
            return ssoPool;
        }
        #endregion

        #region 带分页的单点登录池
        /// <summary>
        /// 带分页的单点登录池
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<SSOPool> GetPagingModel(SSOPoolParam parameter)
        {
            //过滤
            parameter.PoolName = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.PoolName);
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var pagingModel = ssoPoolDal.GetPaging(parameter);
            return pagingModel;
        }
        #endregion

        #region 删除单点登录池
        /// <summary>
        /// 删除单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public JsonModel<string> DeleteSSOPool(int poolId)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "删除失败",
                SuccessMsg = "删除成功"
            };

            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var dbPool = ssoPoolDal.GetEntity(poolId);
            if (dbPool == null)
            {
                jsonModel.ErrMsg = "该单点池不存在";
                return jsonModel;
            }
            dbPool.DelFlag = (int)DelFlagEnum.LogicalDelete;
            var r = ssoPoolDal.UpdateEntity(dbPool);
            if (r != null && r.PoolId > 0)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion

        #region 改变池子的启用或者禁用的状态
        /// <summary>
        /// 改变池子的启用或者禁用的状态
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public JsonModel<string> ChangeSSOPoolEnabled(int poolId, int isEnabled)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "操作失败",
                SuccessMsg = "操作成功"
            };
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var ssoPool = ssoPoolDal.GetEntity(poolId);
            if (ssoPool == null || ssoPool.PoolId == 0)
            {
                jsonModel.ErrMsg = "当前池子不存在";
                return jsonModel;
            }
            if (!Enum.IsDefined(typeof(IsEnabledEnum), isEnabled))
            {
                jsonModel.ErrMsg = "池子状态不正确";
                return jsonModel;
            }
            ssoPool.IsEnabled = isEnabled;
            var r = ssoPoolDal.UpdateEntity(ssoPool);
            if (r != null && r.PoolId > 0)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion

        #region 获取所有的单点登录池
        /// <summary>
        /// 获取所有的单点登录池
        /// </summary>
        /// <returns></returns>
        public List<SSOPool> GetSSOPoolList()
        {
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var poolList = ssoPoolDal.GetEntitiyList();
            return poolList;
        }
        #endregion

        #region 查询用来展示的单点登录池
        /// <summary>
        /// 查询用来展示的单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public SSOPoolDisplayModel GetDisplayModel(int poolId)
        {
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var ssoPool = ssoPoolDal.GetEntity(poolId);

            SSOPoolDisplayModel displayModel = new SSOPoolDisplayModel()
            {
                PoolId = ssoPool.PoolId,
                PoolName = ssoPool.PoolName,
                IsEnabled = ssoPool.IsEnabled,
                MaxAmount = ssoPool.MaxAmount,
                MainDomainId = ssoPool.MainDomainId,
                DelFlag = ssoPool.DelFlag,
                ReMark = ssoPool.ReMark,
                Domains = new List<Domain>()
            };
            if (ssoPool != null)
            {
                IDomainDal domainDal = new DomainDal();
                var domainList = domainDal.GetPoolDomain(poolId);
                if (domainList != null)
                {
                    displayModel.Domains = domainList;
                }
            }
            return displayModel;
        }
        #endregion

        #region 查询用来修改的单点登录池实体
        /// <summary>
        /// 查询用来修改的单点登录池实体
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public SSOPoolEditModel GetEditModel(int poolId)
        {
            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            SSOPoolEditModel editModel = new SSOPoolEditModel();
            var ssoPool = ssoPoolDal.GetEntity(poolId);
            if (ssoPool != null)
            {
                editModel = new SSOPoolEditModel()
               {
                   PoolId = ssoPool.PoolId,
                   PoolName = ssoPool.PoolName,
                   IsEnabled = ssoPool.IsEnabled,
                   MainDomainId = ssoPool.MainDomainId,
                   MaxAmount = ssoPool.MaxAmount,
                   ReMark = ssoPool.ReMark
               };
            }
            return editModel;
        }
        #endregion

        #region 修改单点登录池
        /// <summary>
        /// 修改单点登录池
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<string> EditSSOPool(SSOPoolEditModel model)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                SuccessMsg = "修改成功",
                ErrMsg = "修改失败"
            };

            //对实体进行验证
            var validate = DotNet.Utils.DataValidate.ValidateHelper<SSOPoolEditModel>.ValidateModel(model);
            if (!validate.Pass)
            {
                jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                return jsonModel;
            }
            //字符过滤
            model.ReMark = DotNet.Utils.Untility.StringHelper.FilterHtml(model.ReMark);
            IDomainDal domainDal = new DomainDal();
            //检测选择的主要验证域是否正确
            if (model.MainDomainId > 0)
            {
                var domain = domainDal.GetEntity(model.MainDomainId);
                if (domain == null || domain.SSOPoolPoolId != model.PoolId)
                {
                    jsonModel.ErrMsg = "您选择的主要验证域不正确";
                    return jsonModel;
                }
            }
            //查看最大的域数量是否超出限制
            var poolDomainCount = domainDal.GetPoolDomain(model.PoolId).Count;
            if (poolDomainCount > model.MaxAmount)
            {
                jsonModel.ErrMsg = string.Format("您输入的最大域数量不正确,应大于{0}", poolDomainCount);
                return jsonModel;
            }

            ISSOPoolDal ssoPoolDal = new SSOPoolDal();
            var dbPool = ssoPoolDal.GetEntity(model.PoolId);
            if (dbPool != null)
            {
                dbPool.PoolName = model.PoolName;
                dbPool.MainDomainId = model.MainDomainId;
                dbPool.IsEnabled = model.IsEnabled;
                dbPool.MaxAmount = model.MaxAmount;
                dbPool.ReMark = model.ReMark;
            }
            var r = ssoPoolDal.UpdateEntity(dbPool);
            if (r != null && r.PoolId > 0)
            {
                jsonModel.Success = true;
            }

            return jsonModel;
        }
        #endregion

    }
}
