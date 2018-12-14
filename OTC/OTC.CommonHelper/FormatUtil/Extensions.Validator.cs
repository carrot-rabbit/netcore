using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OTC.CommonHelper.StringUtil
{
    /// <summary>
    /// 系统扩展 - 格式验证
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 验证正浮点数(保留两位小数)
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>验证结果</returns>
        public static bool IsFloat(this string num, bool allowNull)
        {
            //^[1-9]\d*\.?\d{0,2}$|^0?\.\d?[1-9]$
            return IsMatchRegEx(num, allowNull, @"^\d*\.?\d{0,2}$");
        }

        /// <summary>
        /// 验证QQ格式
        /// </summary>
        /// <param name="qq">邮箱</param>
        /// <param name="allowNull">是否允许空值</param>
        /// <returns>验证结果</returns>
        public static bool IsQQ(this string qq, bool allowNull)
        {
            return IsMatchRegEx(qq, allowNull, @"\d{5,11}");
        }

        /// <summary>
        /// 验证邮箱格式
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="allowNull">是否允许空值</param>
        /// <returns>验证结果</returns>
        public static bool IsEmail(this string email, bool allowNull)
        {//^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$
            return IsMatchRegEx(email, allowNull, @"^((?=[a-zA-Z0-9])[a-zA-Z0-9._-]+)?[a-zA-Z0-9]{1}@[a-zA-Z0-9]+(-?[a-zA-Z0-9]+)?(\.[a-zA-Z0-9]+)*(\.[a-zA-Z]{2,}){1}$");
        }

        /// <summary>
        /// 验证手机格式
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="allowNull">是否允许空值</param>
        /// <returns>验证结果</returns>
        public static bool IsPhone(this string phone, bool allowNull)
        {
            //中国地区手机正则：^13[0-9][0-9]{8}|14[57][0-9]{8}|15[012356789][0-9]{8}|18[0-9][0-9]{8}|17[678][0-9]{8}
            return IsMatchRegEx(phone, allowNull, @"^13[0-9]{9}$|14[0-9]{9}|15[0-9]{9}$|18[0-9]{9}$");
        }
        /// <summary>
        /// 验证账号格式
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <returns>验证结果</returns>
        public static bool IsUserName(this string userName)
        {
            return IsMatchRegEx(userName, false, @"[A-Za-z0-9_]{6,16}");
        }

        /// <summary>
        /// 验证密码格式
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <returns>验证结果</returns>
        public static bool IsPwd(this string pwd, bool allowNull)
        {//[A-Za-z0-9_\~\!\@\#\$\%\^\&\*\(\)\-\+\=\[\]\{\}\<\>\?\:\;]{6,16}
            return IsMatchRegEx(pwd, allowNull, @"^(?!^\d+$)(?!^[a-zA-Z]+$)(?!^[\~\`\!\@\#\$\%\^\&\*\(\)\-\+\=\{\[\}\]\|\\\:\;\”\’\<\,\>\.\?\/]+$)[\w\~\`\!\@\#\$\%\^\&\*\(\)\-\+\=\{\[\}\]\|\\\:\;\”\’\<\,\>\.\?\/]{8,16}$");
        }
        /// <summary>
        /// 验证标识符格式
        /// </summary>
        /// <param name="code">标识符（大小写字母、下划线、数字，1到20位）</param>
        /// <returns>验证结果</returns>
        public static bool IsCode(this string code)
        {
            return IsMatchRegEx(code, false, @"[A-Za-z0-9_]{1,20}");
        }
        /// <summary>
        /// 验证ip
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static bool IsIPAddress(this string ip)
        {
            if (ip == null || ip == string.Empty || ip.Length < 7 || ip.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            return IsMatchRegEx(ip, false, regformat);
        }
        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="idNo">身份证号(包含15位和18位身份证号)</param>
        /// <returns></returns>
        public static bool IsIdNo(this string idNo)
        {
            string regformat = @"(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(\d{1}|X)$)";
            return IsMatchRegEx(idNo, false, regformat);
        }
        
        ///// <summary>
        ///// 验证域名格式
        ///// </summary>
        ///// <param name="domain">域名</param>
        ///// <returns>验证结果</returns>
        //public static bool IsDomain(this string domain)
        //{
        //    int len = domain.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length;
        //    return len == 2;
        //}
        ///// <summary>
        ///// 验证是否域名格式-可不带后缀
        ///// </summary>
        ///// <param name="domainName">域名</param>
        ///// <returns></returns>
        //public static bool IsDomain(this string domainName)
        //{
        //    bool ret = false;
        //    if (domainName.Contains("xn--"))
        //    {
        //        return ret;
        //    }
        //    Regex r = new Regex(@"[\u4e00-\u9fa5]+");
        //    Match mc = r.Match(domainName);
        //    if (mc.Length != 0)//含中文域名
        //    {
        //        IdnMapping dd = new IdnMapping();
        //        domainName = dd.GetAscii(domainName);
        //    }
        //    if (domainName.Substring(0, 1) == "-")
        //    {
        //        return ret;
        //    }
        //    if (domainName.Contains("-."))
        //    {
        //        return ret;
        //    }
        //    if (!domainName.Contains("."))
        //    {
        //        domainName += ".com";
        //    }
        //    //英文域名格式
        //    System.Text.RegularExpressions.MatchCollection mc2 = System.Text.RegularExpressions.Regex.Matches(domainName, @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$");
        //    if (mc2 != null && mc2.Count > 0)
        //    {
        //        return true;
        //    }
        //    return ret;
        //}
        /// <summary>
        /// 验证是否域名格式-带后缀
        /// </summary>
        /// <param name="domainName">域名</param>
        /// <returns></returns>
        public static bool IsDomain(this string domainName)
        {
            bool ret = false;
            if (!domainName.Contains("."))
            {
                return ret;
            }
            
            Regex r = new Regex(@"[\u4e00-\u9fa5]+");
            Match mc = r.Match(domainName);
            if (mc.Length != 0)//含中文域名
            {
                IdnMapping dd = new IdnMapping();
                domainName = dd.GetAscii(domainName);
            }
            //英文域名格式
            System.Text.RegularExpressions.MatchCollection mc2 = System.Text.RegularExpressions.Regex.Matches(domainName, @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$");
            if (mc2 != null && mc2.Count > 0)
            {
                return true;
            }
            return ret;
        }
        /// <summary>
        /// 验证比特币地址格式
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>验证结果</returns>
        public static bool IsCoinAddress(this string address)
        {
            return IsMatchRegEx(address, false, @"^[a-zA-Z0-9]{16,100}$");
        }
        /// <summary>
        /// 匹配正则
        /// </summary>
        /// <param name="text">要验证的文本</param>
        /// <param name="allowNull">是否允许空值</param>
        /// <param name="regExPattern">正则表达式</param>
        /// <returns>验证结果</returns>
        public static bool IsMatchRegEx(string text, bool allowNull, string regExPattern)
        {
            bool isEmpty = String.IsNullOrEmpty(text);
            if (isEmpty && allowNull) return true;
            if (isEmpty && !allowNull) return false;

            return Regex.IsMatch(text, regExPattern);
        }
        /// <summary>
        /// 获取两字符间字符串
        /// </summary>
        /// <param name="str">源字符</param>
        /// <param name="beginStr">开始字符</param>
        /// <param name="endStr">结束字符</param>
        /// <param name="splitStr">返回的分隔字符</param>
        /// <param name="isSetSmall">是否统一设置为小写</param>
        /// <param name="letterType">是否进行大/小写转换0不转换1转小写2转大写</param>
        /// <returns></returns>
        public static string GetBetweenStr(string str, string beginStr, string endStr, string splitStr = ",", int letterType = 0)
        {
            string ret = "";
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (letterType == 1)
            {
                str = str.ToLower();
                beginStr = beginStr.ToLower();
                endStr = endStr.ToLower();
            }
            else if (letterType == 2)
            {
                str = str.ToUpper();
                beginStr = beginStr.ToUpper();
                endStr = endStr.ToUpper();
            }
            Regex clearString = new Regex(beginStr + "(.+?)" + endStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = null;
            for (m = clearString.Match(str); m.Success; m = m.NextMatch())
            {
                ret += m.Groups[0].ToString().Replace(beginStr, "").Replace(endStr, "").Trim() + splitStr;
            }
            ret = ret.Substring(0, ret.Length - 1);
            return ret;// ret.TrimEnd(',')
        }
        /// <summary>
        /// 获取两字符间字符串
        /// </summary>
        /// <param name="str">源字符</param>
        /// <param name="beginStr">开始字符</param>
        /// <param name="endStr">结束字符</param>
        /// <returns></returns>
        public static List<string> GetBetweenArr(string str, string beginStr, string endStr)
        {
            List<string> ret = new List<string>();
            if (string.IsNullOrEmpty(str))
            {
                return ret;
            }
            Regex clearString = new Regex(beginStr + "(.+?)" + endStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = null;
            int index = 0;
            for (m = clearString.Match(str); m.Success; m = m.NextMatch())
            {
                string rstr = m.Groups[0].ToString().Replace(beginStr, "").Replace(endStr, "").Trim();
                ret.Add(rstr);
                index++;
            }
            return ret;
        }

        ///<summary>
        ///验证字符串是否有sql注入字段
        ///</summary>
        ///<param name="input"></param>
        ///<returns></returns>
        public static bool IsSafeInput(this string objInput)
        {
            bool ret = true;
            if (string.IsNullOrEmpty(objInput))    //字段可为空,修改为返回TRUE
                ret = true;
            else
            {
                string input = objInput.ToString();
                //替换单引号
                input = input.Replace("'", "''").Trim();

                //检测攻击性危险字符串
                string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                string[] testArray = testString.Split('|');
                foreach (string testStr in testArray)
                {
                    if (input.ToLower().IndexOf(testStr) != -1)
                    {
                        //检测到攻击字符串,清空传入的值
                        input = "";
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
