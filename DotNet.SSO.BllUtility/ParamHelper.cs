using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:参数帮助类
    /// 作者:Tieria
    /// 时间:2015.07.13
    /// 文件名:ParamHelper.cs
    /// </summary>
    public class ParamHelper
    {
        public static ParamHelper GetInstance()
        {
            return new ParamHelper();
        }

        #region 获取所有的参数
        public SortedDictionary<string, string> GetParams()
        {
            SortedDictionary<string, string> dicParam = new SortedDictionary<string, string>();
            var paramList = HttpContext.Current.Request.QueryString.AllKeys;
            for (int i = 0; i < paramList.Length; i++)
            {
                if (!string.IsNullOrEmpty(paramList[i]) && !dicParam.ContainsKey(paramList[i]))
                {
                    dicParam.Add(paramList[i], HttpContext.Current.Request.QueryString[paramList[i]]);
                }
            }
            return dicParam;
        }
        #endregion

        #region 参数过滤,并且去除不合法的参数
        public Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Value != "" && temp.Value != null)
                {
                    dicArray.Add(temp.Key, HttpUtility.HtmlEncode(temp.Value));
                }
            }

            return dicArray;
        }
        #endregion

        #region 获取所有的参数,并且拼接成字符串
        public string GetRequestParamWithOutPage()
        {
            StringBuilder sb = new StringBuilder();
            SortedDictionary<string, string> dicParam = GetParams();
            Dictionary<string, string> parameter = FilterPara(dicParam);
            if (parameter.ContainsKey("page"))
            {
                parameter.Remove("page");
            }
            int current = 0;
            foreach (KeyValuePair<string, string> temp in parameter)
            {
                string conbine = "&";
                if (current == 0)
                {
                    conbine = "?";
                }
                sb.AppendFormat(@"{0}{1}={2}", conbine, temp.Key, temp.Value);
                current++;
            }
            return sb.ToString();
        }
        #endregion
    }
}
