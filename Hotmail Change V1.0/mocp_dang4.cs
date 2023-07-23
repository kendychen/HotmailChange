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
    class mocp_dang4 // xm mail có sẵn ko phải đổi pass
    {
        public static bool mocp_dang4_Buoc1(ChromeDriver chrome,string mailkhoiphuc,string mailkhoiphucmoi)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                // b1 xác minh mail khôi phục lại
                string inboxgetnadaso7 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                string inboxgetso7 = inboxgetnadaso7.Substring(0, inboxgetnadaso7.IndexOf("\",\"ib\":"));
                string uidgetnadaso7 = inboxgetso7.Substring(inboxgetso7.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso7 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso7).ToString();
                string laymaso7 = laymagetnadaso7.Substring(laymagetnadaso7.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso7 = laymaso7.Substring(0, laymaso7.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucso7 = laymaso7.Substring(laymaso7.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(2000);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpmailkkhoiphucso7);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                Thread.Sleep(2000);
                // thêm mail khôi phục mơi
                chrome.Url = ("https://account.live.com/proofs/Add");
                chrome.Url = ("https://account.live.com/proofs/Add");
                // lấy mã khôi phục 
                chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                Thread.Sleep(8000);
                string inboxgetnadaso4 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphucmoi).ToString();
                string inboxgetso4 = inboxgetnadaso4.Substring(0, inboxgetnadaso4.IndexOf("\",\"ib\":"));
                string uidgetnadaso4 = inboxgetso4.Substring(inboxgetso4.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso4 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso4).ToString();
                string laymaso4 = laymagetnadaso4.Substring(laymagetnadaso4.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso4 = laymaso4.Substring(0, laymaso4.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucmoiso4 = laymaso4.Substring(laymaso4.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); 
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpmailkkhoiphucmoiso4);
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
        public static bool mocp_dang4_changepassrandom(ChromeDriver chrome, string passmail,string mkmoi)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
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
        public static bool mocp_dang4_changepassmktudat(ChromeDriver chrome,string passmail,string mkmoitudat)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
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
 