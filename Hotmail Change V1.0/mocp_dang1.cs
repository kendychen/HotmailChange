using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace Hotmail_Change_V1._0
{
    class mocp_dang1
    {
        public static bool mpcp_dang1(ChromeDriver chrome, string keyctsc)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                // B1 Xác minh sđt và đổi pass
                chrome.Url = "https://account.microsoft.com/security";
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@aria-describedby='iPageTitle iIntroText']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//select[@id='DisplayPhoneCountryISO']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                Thread.Sleep(1000);
                // xác minh sđt 
                chrome.FindElement(By.XPath("//input[@id='DisplayPhoneNumber']")).Clear();
                //lay sđt và uid sđt
                for (int s = 0; s < 5; s++)// lấy sđt và udi
                {
                    try
                    {
                        string simthue = http.Get("https://chothuesimcode.com/api?act=number&apik=" + keyctsc + "&appId=1017").ToString();
                        JObject jobject = JObject.Parse(simthue);
                        string res = jobject["ResponseCode"].ToString();
                        if (res == "0")
                        {
                            string simthue1 = jobject["Result"].ToString();
                            JObject jobject1 = JObject.Parse(simthue1);
                            string sdtctsc = jobject1["Number"].ToString();
                            NeedInfo.uidsdtsimthue = jobject1["Id"].ToString();
                            chrome.FindElement(By.XPath("//input[@id='iCollectPhoneViewAction']")).Clear();
                            chrome.FindElement(By.XPath("//input[@id='DisplayPhoneNumber']")).SendKeys(sdtctsc);
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@id='iCollectPhoneViewAction']")).Click();
                            Thread.Sleep(10000);
                        }
                    }
                    catch { }
                }

                for (int j = 0; j < 5; j++)
                {
                    try {
                        string layotpctsc = http.Get("https://chothuesimcode.com/api?act=code&apik=" + keyctsc + "&id=" + NeedInfo.uidsdtsimthue).ToString();
                        Thread.Sleep(1000);
                        JObject ojctsc = JObject.Parse(layotpctsc);
                        Thread.Sleep(1000);
                        string res1 = ojctsc["ResponseCode"].ToString();
                        if (res1 == "0")
                        {
                            string simthueotp = ojctsc["Result"].ToString();
                            JObject ojctsc1 = JObject.Parse(simthueotp);
                            string maotpctsc = ojctsc1["Code"].ToString();
                            Thread.Sleep(1000);
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@class='email-input-max-width form-control']")).SendKeys(maotpctsc);
                            chrome.FindElement(By.XPath("//input[@id='iVerifyPhoneViewAction']")).Click();
                            break;
                        }
                        Thread.Sleep(5000);
                    }
                    catch { }
                }
                       
                return true;
            }
            catch
            {
                return false;
            }


        }
        public static bool mocp_dang1_doipassrandom(ChromeDriver chrome,string mkmoi,string mailkhoiphucmoi)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iReviewProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@placeholder='someone@example.com']")).SendKeys(mailkhoiphucmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iCollectProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iFinishViewAction']")).Click();
                Thread.Sleep(2000);
                Thread.Sleep(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool mocp_dang1_doipasstudat(ChromeDriver chrome, string mkmoitudat,  string mailkhoiphucmoi)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoitudat);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iReviewProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@placeholder='someone@example.com']")).SendKeys(mailkhoiphucmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iCollectProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iFinishViewAction']")).Click();
                Thread.Sleep(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }


}
