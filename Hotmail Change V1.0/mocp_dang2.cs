using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace Hotmail_Change_V1._0
{
    class mocp_dang2
    {
        public static string layuidctsc(ChromeDriver chrome,string keyctsc)
        {
            try
            {
                string uidsdtsimthue = "";
                HttpRequest http = new HttpRequest();
                Thread.Sleep(2000);
                // lấy sđt
                for (int s = 0; s < 5; s++)// lấy sđt và udi
                {
                    string simthue = http.Get("https://chothuesimcode.com/api?act=number&apik=" + keyctsc + "&appId=1017").ToString();
                    JObject jobject = JObject.Parse(simthue);
                    string res = jobject["ResponseCode"].ToString();
                    
                        if (res == "0")
                        {
                        try
                        {
                            string simthue1 = jobject["Result"].ToString();
                            JObject jobject1 = JObject.Parse(simthue1);
                            string sdtctsc = jobject1["Number"].ToString();
                            uidsdtsimthue = jobject1["Id"].ToString();

                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[2]/div[2]/input")).Clear(); ;
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[2]/div[2]/input")).SendKeys(sdtctsc);
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[3]/a[1]")).Click();
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).Clear();
                            Thread.Sleep(2000);
                            break;
                        }
                        catch { }
                        }
                   
              }
                return uidsdtsimthue;
            }
            catch 
            {
                return "";
            }

        }
        public static bool xmsdtsimthue(ChromeDriver chrome,string keyctsc, string uidsimthue)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                string layotpctsc = http.Get("https://chothuesimcode.com/api?act=code&apik=" + keyctsc + "&id=" + uidsimthue).ToString();
                Thread.Sleep(2000);
                JObject ojctsc = JObject.Parse(layotpctsc);
                Thread.Sleep(2000);
                string res1 = ojctsc["ResponseCode"].ToString();
                if (res1 == "0")
                {
                   
                        string simthueotp = ojctsc["Result"].ToString();
                        JObject ojctsc1 = JObject.Parse(simthueotp);
                        string maotpctsc = ojctsc1["Code"].ToString();
                        Thread.Sleep(2000);
                        chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).Clear();
                        Thread.Sleep(2000);
                        chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).SendKeys(maotpctsc);
                        Thread.Sleep(2000);
                        chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[5]/div/div/input")).Click();
                        Thread.Sleep(2000);
                        chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[4]/div/div/input")).Click();
                        Thread.Sleep(2000);
                        chrome.FindElement(By.XPath("/html/body/div/form/div/div/div[2]/div[1]/div/div/div/div/div/div[3]/div/div[2]/div/div[3]/div[2]/div/div/div[2]/input")).Click();
                        Thread.Sleep(2000);
                   
                }
                return true;
            }
            catch
            {
                return false;

            }
        }
        public static bool mocp_dang1_xmsdt(ChromeDriver chrome,string keyctsc)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                Thread.Sleep(2000);
                // lấy sđt
                for (int s = 0; s < 5; s++)// lấy sđt và udi
                {
                        string simthue = http.Get("https://chothuesimcode.com/api?act=number&apik=" + keyctsc + "&appId=1017").ToString();
                        JObject jobject = JObject.Parse(simthue);
                        string res = jobject["ResponseCode"].ToString();
                    try
                    {
                        if (res == "0")
                        {
                            string simthue1 = jobject["Result"].ToString();
                            JObject jobject1 = JObject.Parse(simthue1);
                            string sdtctsc = jobject1["Number"].ToString();
                            string uidsdtsimthue = jobject1["Id"].ToString();

                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[2]/div[2]/input")).Clear(); ;
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[2]/div[2]/input")).SendKeys(sdtctsc);
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[3]/a[1]")).Click();
                            Thread.Sleep(2000);
                            chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).Clear();
                            Thread.Sleep(15000);
                                string layotpctsc = http.Get("https://chothuesimcode.com/api?act=code&apik=" + keyctsc + "&id=" + uidsdtsimthue).ToString();
                                Thread.Sleep(2000);
                                JObject ojctsc = JObject.Parse(layotpctsc);
                                Thread.Sleep(2000);
                                string res1 = ojctsc["ResponseCode"].ToString();
                                if(res1 == "0")
                                {
                                    string simthueotp = ojctsc["Result"].ToString();
                                    JObject ojctsc1 = JObject.Parse(simthueotp);
                                    string maotpctsc = ojctsc1["Code"].ToString();
                                    Thread.Sleep(2000);
                                    chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).Clear();
                                    Thread.Sleep(2000);
                                    chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[3]/div/div/div[1]/div/div/div/div/div[4]/div/input")).SendKeys(maotpctsc);
                                    Thread.Sleep(2000);
                                    chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[5]/div/div/input")).Click();
                                    Thread.Sleep(2000);
                                    chrome.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/section/div/div[4]/div/div/input")).Click();
                                    Thread.Sleep(2000);
                                    chrome.FindElement(By.XPath("/html/body/div/form/div/div/div[2]/div[1]/div/div/div/div/div/div[3]/div/div[2]/div/div[3]/div[2]/div/div/div[2]/input")).Click();
                                    Thread.Sleep(2000);
                                    break;
                                }   
                        }

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
        public static bool mocp_dang2_themmailkhoiphuc(ChromeDriver chrome,string mailkhoiphucmoi)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                chrome.Url = ("https://account.live.com/proofs/Add");
                chrome.Url = ("https://account.live.com/proofs/Add");
                // lấy mã khôi phục 
                chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi.ToLower());
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                Thread.Sleep(10000);
                string inboxgetnada1 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphucmoi.ToLower()).ToString();
                string inboxget1 = inboxgetnada1.Substring(0, inboxgetnada1.IndexOf("\",\"ib\":"));
                string uidgetnada1 = inboxget1.Substring(inboxget1.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnada1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnada1).ToString();
                string layma1 = laymagetnada1.Substring(laymagetnada1.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                layma1 = layma1.Substring(0, layma1.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucmoi1 = layma1.Substring(layma1.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", "");
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpmailkkhoiphucmoi1);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool mocp_dang2_xmmaildoipass(ChromeDriver chrome,string email,string mailkhoiphucmoi)
        { 
            try
            {
                HttpRequest http = new HttpRequest();
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("/html/body/div/form[1]/div/div/div[2]/div[1]/div/div/div/div/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div/div/div/div[2]")).Click();
                Thread.Sleep(2000);  
                chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphucmoi.ToLower());                         
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                Thread.Sleep(8000);
                string inboxgetnadaso1 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphucmoi.ToLower()).ToString();
                string inboxgetso1 = inboxgetnadaso1.Substring(0, inboxgetnadaso1.IndexOf("\",\"ib\":"));
                string uidgetnadaso1 = inboxgetso1.Substring(inboxgetso1.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso1).ToString();
                string laymaso1 = laymagetnadaso1.Substring(laymagetnadaso1.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso1 = laymaso1.Substring(0, laymaso1.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucmoiso1 = laymaso1.Substring(laymaso1.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); 
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpmailkkhoiphucmoiso1);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                Thread.Sleep(2000);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool mocp_dang2_xmmailkhoiphucdoipass(ChromeDriver chrome, string email, string mailkhoiphuc)
        {
            try { 
            HttpRequest http = new HttpRequest();
            chrome.Url = "https://account.live.com/password/Change";
            Thread.Sleep(2000); 
            chrome.FindElement(By.XPath("//div[@class='table-cell text-left content']")).Click() ;
            Thread.Sleep(2000); 
            chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCS_ProofConfirmation']")).SendKeys(mailkhoiphuc);
            Thread.Sleep(2000);
            chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
            Thread.Sleep(8000);
            string inboxgetnadaso1 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
            string inboxgetso1 = inboxgetnadaso1.Substring(0, inboxgetnadaso1.IndexOf("\",\"ib\":"));
            string uidgetnadaso1 = inboxgetso1.Substring(inboxgetso1.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
            String laymagetnadaso1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso1).ToString();
            string laymaso1 = laymagetnadaso1.Substring(laymagetnadaso1.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
            laymaso1 = laymaso1.Substring(0, laymaso1.IndexOf("</span></td></tr>"));
            string otpmailkkhoiphucmoiso1 = laymaso1.Substring(laymaso1.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(2000);
            chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpmailkkhoiphucmoiso1);
            Thread.Sleep(2000);
            chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
            Thread.Sleep(2000);

            return true;
        }
            catch
            {
                return false;
            }
        }
        // đổi pass 
       
        public static bool mocp_dang2_changepassrandom(ChromeDriver chrome,string passmail,string mkmoi)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='CurrentPassword']")).SendKeys(passmail);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(mkmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='UpdatePasswordAction']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool mocp_dang2_changepasstudatm(ChromeDriver chrome,string passmail,string mkmoitudat)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='CurrentPassword']")).SendKeys(passmail);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoitudat);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(mkmoitudat);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='UpdatePasswordAction']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
