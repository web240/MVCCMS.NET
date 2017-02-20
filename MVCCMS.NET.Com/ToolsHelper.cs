﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace MVCCMS.NET.Com
{
    public class ToolsHelper
    {
        #region 检测是否有Sql危险字符

        /// <summary>
        ///     检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str , @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        #endregion

        /// <summary>
        ///     是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip , @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #region 删除最后结尾的一个逗号

        /// <summary>
        ///     删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            if (str.Length < 1)
            {
                return "";
            }
            return str.Substring(0 , str.LastIndexOf(","));
        }

        #endregion

        #region 删除最后结尾的指定字符后的字符

        /// <summary>
        ///     删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str , string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0 , str.LastIndexOf(strchar));
            }
            return str;
        }

        #endregion

        #region 对象转换处理

        /// <summary>
        ///     判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
                return IsNumeric(expression.ToString());

            return false;
        }

        /// <summary>
        ///     判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                var str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str , @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') ||
                        (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            if (expression != null)
                return Regex.IsMatch(expression.ToString() , @"^([0-9])[0-9]*(\.\w*)?$");

            return false;
        }

        /// <summary>
        ///     检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail , @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail ,
                @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        ///     检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl ,
                @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        ///     将字符串转换为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }

        /// <summary>
        ///     将数组转换为字符串
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="speater">分隔符</param>
        /// <returns>String</returns>
        public static string GetArrayStr(List<string> list , string speater)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        ///     object型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression , bool defValue)
        {
            if (expression != null)
                return StrToBool(expression , defValue);

            return defValue;
        }

        /// <summary>
        ///     string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression , bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression , "true" , true) == 0)
                    return true;
                if (string.Compare(expression , "false" , true) == 0)
                    return false;
            }
            return defValue;
        }

        /// <summary>
        ///     将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjToInt(object expression , int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString() , defValue);

            return defValue;
        }

        /// <summary>
        ///     将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression , int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 ||
                !Regex.IsMatch(expression.Trim() , @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (int.TryParse(expression , out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression , defValue));
        }

        /// <summary>
        ///     Object型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ObjToDecimal(object expression , decimal defValue)
        {
            if (expression != null)
                return StrToDecimal(expression.ToString() , defValue);

            return defValue;
        }

        /// <summary>
        ///     string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string expression , decimal defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            var intValue = defValue;
            if (expression != null)
            {
                var IsDecimal = Regex.IsMatch(expression , @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsDecimal)
                    decimal.TryParse(expression , out intValue);
            }
            return intValue;
        }

        /// <summary>
        ///     Object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjToFloat(object expression , float defValue)
        {
            if (expression != null)
                return StrToFloat(expression.ToString() , defValue);

            return defValue;
        }

        /// <summary>
        ///     string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression , float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            var intValue = defValue;
            if (expression != null)
            {
                var IsFloat = Regex.IsMatch(expression , @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression , out intValue);
            }
            return intValue;
        }

        /// <summary>
        ///     将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str , DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str , out dateTime))
                    return dateTime;
            }
            return defValue;
        }

        /// <summary>
        ///     将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str , DateTime.Now);
        }

        /// <summary>
        ///     将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }

        /// <summary>
        ///     将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj , DateTime defValue)
        {
            return StrToDateTime(obj.ToString() , defValue);
        }

        /// <summary>
        ///     将对象转换为字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的string类型结果</returns>
        public static string ObjectToStr(object obj)
        {
            if (obj == null)
                return "";
            return obj.ToString().Trim();
        }

        public static string StrArrToStr(string[] strarr)
        {
            if (strarr == null) return "";
            if (strarr.Length <= 0) return "";
            var str = "";
            for (var i = 0; i < strarr.Length; i++)
            {
                str += strarr[i] + ",";
            }
            return str.Remove(str.Length - 1 , 1);
        }

        #endregion

        #region 读取或写入cookie

        /// <summary>
        ///     写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName , string strValue)
        {
            var cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName , string key , string strValue)
        {
            var cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName , string key , string strValue , int expires)
        {
            var cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName , string strValue , int expires)
        {
            var cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName].Value);
            return "";
        }

        /// <summary>
        ///     读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName , string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null &&
                HttpContext.Current.Request.Cookies[strName][key] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName][key]);

            return "";
        }

        #endregion

        #region URL处理

        /// <summary>
        ///     URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'" , "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        ///     URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        ///     组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url , string _keys , params string[] _values)
        {
            var urlParams = new StringBuilder();
            try
            {
                var keyArr = _keys.Split('&');
                for (var i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i] , _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0 , "?");
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString() , "&");
        }

        #endregion

        #region 替换指定的字符串

        /// <summary>
        ///     替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr , string oldStr , string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr , newStr);
        }

        #endregion

        #region 显示分页

        /// <summary>
        ///     返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize , int pageIndex , int totalCount , string linkUrl , int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            var pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<ul class=\"paginList\"><li class=\"paginItem\"><a href=\"" + ReplaceStr(linkUrl , pageId , (pageIndex - 1).ToString()) + "\"><<上一页</a></li>";
            string lastBtn = "<li class=\"paginItem\"><a href=\"" + ReplaceStr(linkUrl , pageId , (pageIndex + 1).ToString()) + "\">下一页>></a></li></ul>";
            string firstStr = "<li class=\"paginItem\"><a href=\"" + ReplaceStr(linkUrl , pageId , "1") + "\">1</a></li>";
            string lastStr = "<li class=\"paginItem\"><a href=\"" + ReplaceStr(linkUrl , pageId , pageCount.ToString()) + "\">" + pageCount + "</a></li>";

            if (pageIndex <= 1)
            {
                firstBtn = "<ul class=\"paginList\"><li class=\"paginItem disabled\"><a><<上一页</a></li>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<li class=\"paginItem disabled\"><a>下一页>></a></li></ul>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<li class=\"paginItem active\"><a>1</a></li>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<li class=\"paginItem active\"><a>" + pageCount + "</a></li>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<div class=\"message\">共 <i class=\"blue\">" + totalCount + "</i> 条记录，当前显示第 " + pageIndex + " 页</div>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<li class=\"paginItem\"><a>...</a></li>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<li class=\"paginItem active\"><a>" + i + "</a></li>");
                }
                else
                {
                    pageStr.Append("<li class=\"paginItem\"><a href=\"" + ReplaceStr(linkUrl , pageId , i.ToString()) + "\">" + i + "</a></li>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<li class=\"paginItem\"><a>...</a></li>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }

        #endregion

        #region string 转int数组

        public static int[] StringToIntArray(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return new int[0];
                if (str.EndsWith(","))
                {
                    str = str.Remove(str.Length - 1 , 1);
                }
                var idstrarr = str.Split(',');
                var idintarr = new int[idstrarr.Length];

                for (int i = 0; i < idstrarr.Length; i++)
                {
                    idintarr[i] = Convert.ToInt32(idstrarr[i]);
                }
                return idintarr;
            }
            catch
            {
                return new int[0];
            }
        }
        #endregion

        #region String转数组
        public static string[] StringToStringArray(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return new string[0];
                if (str.EndsWith(",")) str = str.Remove(str.Length - 1 , 1);
                return str.Split(',');
            }
            catch
            {
                return new string[0];
            }
        }
        #endregion

        #region string转Guid数组
        public static System.Guid[] StringToGuidArray(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return new System.Guid[0];
                if (str.EndsWith(",")) str = str.Remove(str.Length - 1 , 1);
                var strarr = str.Split(',');
                System.Guid[] guids = new System.Guid[strarr.Length];
                for (int index = 0; index < strarr.Length; index++)
                {
                    guids[index] = System.Guid.Parse(strarr[index]);
                }
                return guids;
            }
            catch
            {
                return new System.Guid[0];
            }
        }
        #endregion

        #region 生成指定长度的字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong , string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion

        #region 将枚举转换成ArrayList
        /// <summary>
        /// 将枚举转换成ArrayList
        /// </summary>
        /// <returns></returns>
        public static IList EnumToList(Type enumType)
        {
            ArrayList list = new ArrayList();

            foreach (int i in System.Enum.GetValues(enumType))
            {
                ListItem listitem = new ListItem(System.Enum.GetName(enumType , i) , i.ToString());
                list.Add(listitem);
            }
            return list;
        }
        #endregion


        #region stringarray去重复
        public static List<string> Purge(List<string> needToPurge)
        {

            for (int i = 0; i < needToPurge.Count - 1; i++)
            {
                string deststring = needToPurge[i];
                for (int j = i + 1; j < needToPurge.Count; j++)
                {
                    if (deststring.CompareTo(needToPurge[j]) == 0)
                    {
                        needToPurge.RemoveAt(j);
                    }
                }
            }
            return needToPurge;
        }
        #endregion


        #region 判断是否为图片
        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        public static bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add(".bmp");
            al.Add(".jpeg");
            al.Add(".jpg");
            al.Add(".gif");
            al.Add(".png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }
        #endregion


        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/" , "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\' , 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory , strPath);
            }
        }
        #endregion


        #region MD5加密
        public static string Md5(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2 , '0');
            }
            return str;
        }
        #endregion

    }
}
