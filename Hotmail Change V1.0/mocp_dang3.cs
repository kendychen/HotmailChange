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
    class mocp_dang3 //// mở khóa cp mail đã add khôi phục dạng mới bắt xm mail khi login ( Sẽ bắt đổi pass luôn) có mail khôi phục sẵn 
    {
        public static bool mocp_dang3_buoc1(ChromeDriver chrome, string mailkhoiphuc)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                // lấy mã getnada
                string inboxgetnadaso5 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                string inboxgetso5 = inboxgetnadaso5.Substring(0, inboxgetnadaso5.IndexOf("\",\"ib\":"));
                string uidgetnadaso5 = inboxgetso5.Substring(inboxgetso5.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso5 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso5).ToString();
                string laymaso5 = laymagetnadaso5.Substring(laymagetnadaso5.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso5 = laymaso5.Substring(0, laymaso5.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucso5 = laymaso5.Substring(laymaso5.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpmailkkhoiphucso5);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iVerifyCodeAction']")).Click();
                Thread.Sleep(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool mocp_dang3_neudoipasluonrandom(ChromeDriver chrome,string mkmoi)//
        {
            try
            {
                Thread.Sleep(3000);
                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iReviewProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//a[@id='iCollectProofsViewAlternate']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iFinishViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='i0118']")).SendKeys(mkmoi);
                Thread.Sleep(3000);
                chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                Thread.Sleep(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string mocp_dang3_neudoipasluonmktudat(ChromeDriver chrome,string mkmoitudat)//
        {
            try
            {
                chrome.FindElement(By.XPath("//input[@aria-describedby='pNewPwdErrorArea UpdatePasswordTitle iPassHint']")).SendKeys(mkmoitudat);
                Thread.Sleep(3000);
                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iReviewProofsViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//a[@id='iCollectProofsViewAlternate']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iFinishViewAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='i0118']")).SendKeys(mkmoitudat);
                Thread.Sleep(3000);
                chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                Thread.Sleep(1000);
                return mkmoitudat;
            }
            catch
            {
                return null;
            }
        }
        public static bool mocp_dang3_themmailkkhoiphuc(ChromeDriver chrome, string mailkhoiphucmoi)//
        {
            try
            { // thêm mail khôi phục mới
                HttpRequest http = new HttpRequest();
                chrome.Url = ("https://account.live.com/proofs/Add");
                chrome.Url = ("https://account.live.com/proofs/Add");
                // lấy mã khôi phục 
                chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                Thread.Sleep(8000);
                string inboxgetnadaso2 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphucmoi).ToString();
                string inboxgetso2 = inboxgetnadaso2.Substring(0, inboxgetnadaso2.IndexOf("\",\"ib\":"));
                string uidgetnadaso2 = inboxgetso2.Substring(inboxgetso2.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso2 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso2).ToString();
                string laymaso2 = laymagetnadaso2.Substring(laymagetnadaso2.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso2 = laymaso2.Substring(0, laymaso2.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucmoiso2 = laymaso2.Substring(laymaso2.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpmailkkhoiphucmoiso2);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/section/div/form/div[4]/div[2]")).Click();
                Thread.Sleep(1000);

                return true;
            }
            catch
            {
                return false;
            }
        }
       public static bool mocp_dang3_changepassrandom(ChromeDriver chrome,string mailkhoiphuc,string passmail,string mkmoi)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("/html/body/div/form[1]/div/div/div[2]/div[1]/div/div/div/div/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div/div/div/div[2]")).Click();
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                Thread.Sleep(8000);
                // lấy mã getnada 7 số
                string inboxgetnadaso6 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                string inboxgetso6 = inboxgetnadaso6.Substring(0, inboxgetnadaso6.IndexOf("\",\"ib\":"));
                string uidgetnadaso6 = inboxgetso6.Substring(inboxgetso6.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso6 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso6).ToString();
                string laymaso6 = laymagetnadaso6.Substring(laymagetnadaso6.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso6 = laymaso6.Substring(0, laymaso6.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucso6 = laymaso6.Substring(laymaso6.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpmailkkhoiphucso6);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@name='CurrentPassword']")).SendKeys(passmail);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(mkmoi);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='UpdatePasswordAction']")).Click();
                Thread.Sleep(2000);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool mocp_dang3_changepasstudat(ChromeDriver chrome, string mailkhoiphuc, string passmail,string mkmoitudat)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("/html/body/div/form[1]/div/div/div[2]/div[1]/div/div/div/div/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div/div/div/div[2]")).Click();
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                Thread.Sleep(8000);
                // lấy mã getnada 7 số
                string inboxgetnadaso6 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                string inboxgetso6 = inboxgetnadaso6.Substring(0, inboxgetnadaso6.IndexOf("\",\"ib\":"));
                string uidgetnadaso6 = inboxgetso6.Substring(inboxgetso6.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                String laymagetnadaso6 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso6).ToString();
                string laymaso6 = laymagetnadaso6.Substring(laymagetnadaso6.IndexOf("id=\"i4\"")).Replace(" ", "").Replace("\n", "");
                laymaso6 = laymaso6.Substring(0, laymaso6.IndexOf("</span></td></tr>"));
                string otpmailkkhoiphucso6 = laymaso6.Substring(laymaso6.LastIndexOf("#2a2a2a;\">")).Replace("#2a2a2a;\">", ""); Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpmailkkhoiphucso6);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@name='CurrentPassword']")).SendKeys(passmail);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(mkmoitudat);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(mkmoitudat);
                Thread.Sleep(1000);
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
