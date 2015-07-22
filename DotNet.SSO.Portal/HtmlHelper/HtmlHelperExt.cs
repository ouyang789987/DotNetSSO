using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace DotNet.SSO.Portal
{
    public static class HtmlHelperExt
    {
        #region 第一种分页样式
        public static MvcHtmlString PagingOne(this HtmlHelper helper, string paramUrl, int currentPage, int pageSize, int totalCount)
        {

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            int totalPage = Convert.ToInt32(Math.Ceiling(totalCount / pageSize * 1.0) + 1);
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            if (currentPage > totalPage)
            {
                currentPage = totalPage;
            }
            string pUrl = string.Empty;
            if (string.IsNullOrEmpty(paramUrl))
            {
                pUrl = "?page=";
            }
            else
            {
                pUrl = string.Format("{0}&page=", paramUrl);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td colspan=\"6\">");
            sb.Append("<div class=\"paging\">");
            sb.Append("<ul class=\"pagination\">");
            //首页
            sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"首页\">首页</a></li>", pUrl, 1);

            #region 上一页按钮
            if (currentPage >= 1)
            {
                sb.AppendFormat("<li class=\"disabled\"><a href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a></li>", pUrl, currentPage - 1);
            }
            #endregion

            //当前页前的页数
            int beforPage = currentPage - 4 > 1 ? 4 : currentPage - 1;
            for (int i = currentPage - beforPage; i < currentPage; i++)
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, i);
            }
            //当前页数
            sb.AppendFormat("<li class=\"active\"><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, currentPage);

            //当前页数后面有多少页
            int afterPage = currentPage + 4 < totalPage ? 4 : totalPage - currentPage;
            for (int k = currentPage + 1; k < currentPage + afterPage + 1; k++)
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, k);
            }
            #region 下一页按钮
            if (currentPage >= totalPage)
            {
                sb.AppendFormat("<li class=\"disabled\"><a href=\"#\" aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a></li>", pUrl, currentPage + 1);
            }
            #endregion

            //尾页
            sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"尾页\">尾页</a></li>", pUrl, totalPage);

            sb.AppendFormat("<li><span>共{0}页</span></li>", totalPage);
            //sb.AppendFormat("<li><input type=\"text\" class=\"form-control\"></li><li><a href=\"javascript:;\">Go</a></li>");
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</td>");
            sb.Append("</tr>");
            return new MvcHtmlString(sb.ToString());
        } 
        #endregion
        
        #region 第二种分页样式
        public static MvcHtmlString PagingTwo(this HtmlHelper helper, string paramUrl, int currentPage, int pageSize, int totalCount)
        {

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            int totalPage = Convert.ToInt32(Math.Ceiling(totalCount / pageSize * 1.0) + 1);
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            if (currentPage > totalPage)
            {
                currentPage = totalPage;
            }
            string pUrl = string.Empty;
            if (string.IsNullOrEmpty(paramUrl))
            {
                pUrl = "?page=";
            }
            else
            {
                pUrl = string.Format("{0}&page=", paramUrl);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"paging\">");
            sb.AppendFormat("<ul class=\"pagination\">");
            //首页
            sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"首页\">首页</a></li>", pUrl, 1);
            //上一页
            if (currentPage >= 1)
            {
                sb.AppendFormat("<li class=\"disabled\"><a href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a></li>", pUrl, currentPage - 1);
            }
            //当前页前的页数的前几页
            int beforPage = currentPage - 4 > 1 ? 4 : currentPage - 1;
            for (int i = currentPage - beforPage; i < currentPage; i++)
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, i);
            }
            //当前页数
            sb.AppendFormat("<li class=\"active\"><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, currentPage);
            //当前页数的后几页
            int afterPage = currentPage + 4 < totalPage ? 4 : totalPage - currentPage;
            for (int k = currentPage + 1; k < currentPage + afterPage + 1; k++)
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"第{1}页\">{1}</a></li>", pUrl, k);
            }
            //下一页
            if (currentPage >= totalPage)
            {
                sb.AppendFormat("<li class=\"disabled\"><a href=\"#\" aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a></li>", pUrl, currentPage + 1);
            }
            //尾页
            sb.AppendFormat("<li><a href=\"{0}{1}\"  alt=\"尾页\">尾页</a></li>", pUrl, totalPage);
            //总页数
            sb.AppendFormat("<li><span>共{0}页</span></li>", totalPage);
            //总条数
            sb.AppendFormat("<li><span>{0}条记录</span></li>", totalCount);
            sb.AppendFormat("</ul>");
            sb.AppendFormat("</div>");
            return new MvcHtmlString(sb.ToString());
        } 
        #endregion

    }
}