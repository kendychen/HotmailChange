using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace Hotmail_Change_V1._0
{
    class chucnang
    {

        public static string accfile;
        public static bool checkluufile;
        public static bool xmmailkhoiphuc;
        public static string linkxoamailsmaall(string mailkhoiphuc)
        {
            string url = "";
            try
            {
                HttpRequest http = new HttpRequest();

                string doccodemail = http.Get("https://sellallmail.com/api/messages/" + mailkhoiphuc + "/MAILMMO").ToString();
                Regex code1 = new Regex("account.live.com.+\\d");
                Match url1 = code1.Match(doccodemail);
                url = "http://" + url1.ToString();
                return url;

            }
            catch
            {
                return url;
            }
        }
            public static string laymacodesellallmail(string mailkhoiphuc)
        {
            HttpRequest http = new HttpRequest();
            string code = "";
            try
            {
                string doccodemail = http.Get("https://sellallmail.com/api/messages/"+mailkhoiphuc+"/MAILMMO").ToString();
                Regex code1 = new Regex("\\d+<./span>");
                Match code2 = code1.Match(doccodemail);
                code = code2.ToString();
                do
                {
                    code2 = code2.NextMatch();
                    if (code2 != Match.Empty)
                    {// Chuyển qua kết quả trùng khớp kế tiếp
                        code = code2.ToString();
                    }
                }
                while (code2 != Match.Empty); // Kiểm tra xem đã hết kết quả trùng khớp chưa
                code = code.Replace("<\\/span>", "");
                return code;
            }
            catch
            {
                return code;
            }
        }
        public static bool xmsdtsimthue(ChromeDriver chrome, string keyctsc, string uidsimthue)
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
                    try
                    {
                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@id=\"iOttText\"]"));
                        Thread.Sleep(2000);
                        ele[0].Clear();
                        Thread.Sleep(1000);
                        ele[0].SendKeys(maotpctsc);
                    }
                    catch
                    {
                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@type='text']"));
                        Thread.Sleep(2000);
                        ele[1].Clear();
                        Thread.Sleep(1000);
                        ele[1].SendKeys(maotpctsc);
                    }
                    Thread.Sleep(2000);
                    try
                    {
                        chrome.FindElement(By.XPath("//input[@id=\"iVerifyPhoneViewAction\"]")).Click();
                        Thread.Sleep(2000);
                    }
                    catch
                    {
                        chrome.FindElement(By.XPath("//input[@id=\"ProofAction\"]")).Click();
                        Thread.Sleep(2000);
                    }
                    try
                    {
                        chrome.FindElement(By.XPath("//input[@id='FinishAction']")).Click();
                    }
                    catch { }
                    Thread.Sleep(2000);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;

            }
        }

        public static string layuidctsc(ChromeDriver chrome, string keyctsc)
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
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                            }
                            Thread.Sleep(2000);
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).SendKeys(sdtctsc);
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//input[@type='text']")).SendKeys(sdtctsc);

                            }
                            Thread.Sleep(2000);
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"iCollectPhoneViewAction\"]")).Click();
                            }
                            catch
                            {
                                IList<IWebElement> codesdt = chrome.FindElements(By.XPath("//a[@role='button']"));
                                Thread.Sleep(1000);
                                codesdt[0].Click();
                            }
                            Thread.Sleep(2000);
                            try
                            {
                                IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@id=\"iOttText\"]"));
                                Thread.Sleep(1000);
                                ele[0].Clear();
                            }
                            catch
                            {
                                IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@type=\"text\"]"));
                                Thread.Sleep(2000);
                                ele[0].Click();
                            }
                            break;
                        }
                        catch
                        {
                            try
                            {
                                chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                Thread.Sleep(1000);
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//select[@id='DisplayPhoneCountryISO']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                                Thread.Sleep(1000);

                            }
                        }
                    }

                }
                return uidsdtsimthue;
            }
            catch
            {
                return "";
            }

        }
        public static string layuidviotp(ChromeDriver chrome, string keyctsc)
        {
            try
            {
                string requet_id = "";
                HttpRequest http = new HttpRequest();
                Thread.Sleep(2000);
                // lấy sđt
                for (int s = 0; s < 5; s++)// lấy sđt và udi
                {
                    string simthue = http.Get("https://api.viotp.com/request/getv2?token=" + keyctsc + "&serviceId=5").ToString();
                    //JObject jobject = JObject.Parse(simthue);
                    //string res = jobject["status_code"].ToString();
                    Regex c1 = new Regex("status_code\":\\d+");
                    Match c2 = c1.Match(simthue);
                    string res = c2.ToString();
                    res = res.Replace("status_code\":", "");
                    if (res == "200")
                    {
                        try
                        {
                            Regex s1 = new Regex("re_phone_number\":\"\\d+");
                            Match s2 = s1.Match(simthue);
                            Regex id1 = new Regex("request_id\":\\d+");
                            Match id2 = id1.Match(simthue);
                            string sdt = s2.ToString();
                            sdt = sdt.Replace("re_phone_number\":\"", "");
                            requet_id = id2.ToString();

                            requet_id = requet_id.Replace("request_id\":", "");
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                            }
                            Thread.Sleep(2000);
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).SendKeys(sdt);
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//input[@type='text']")).SendKeys(sdt);

                            }
                            Thread.Sleep(2000);
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id=\"iCollectPhoneViewAction\"]")).Click();
                            }
                            catch
                            {
                                IList<IWebElement> codesdt = chrome.FindElements(By.XPath("//a[@role='button']"));
                                Thread.Sleep(1000);
                                codesdt[0].Click();
                            }
                            Thread.Sleep(2000);
                            try
                            {
                                IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@id=\"iOttText\"]"));
                                Thread.Sleep(1000);
                                ele[0].Clear();
                            }
                            catch
                            {
                                IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@type=\"text\"]"));
                                Thread.Sleep(2000);
                                ele[0].Click();
                            }
                            break;
                        }
                        catch
                        {
                            try
                            {
                                chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                Thread.Sleep(1000);
                            }
                            catch
                            {
                                chrome.FindElement(By.XPath("//select[@id='DisplayPhoneCountryISO']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                                Thread.Sleep(1000);

                            }
                        }
                    }

                }
                return requet_id;
            }
            catch
            {
                return "";
            }

        }
        public static bool xmsdtviotp(ChromeDriver chrome, string keyapi, string requet_id)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                string layotpctsc = http.Get("https://api.viotp.com/session/getv2?requestId=" + requet_id + "&token=" + keyapi).ToString();
                Thread.Sleep(2000);
                JObject ojctsc = JObject.Parse(layotpctsc);
                Thread.Sleep(2000);
                string res1 = ojctsc["status_code"].ToString();
                if (res1 == "200")
                {
                    string simthueotp = ojctsc["data"].ToString();
                    JObject ojctsc1 = JObject.Parse(simthueotp);
                    string maotpctsc = ojctsc1["Code"].ToString();
                    Thread.Sleep(1000);
                    try
                    {
                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@id=\"iOttText\"]"));
                        Thread.Sleep(2000);
                        ele[0].Clear();
                        Thread.Sleep(1000);
                        ele[0].SendKeys(maotpctsc);
                    }
                    catch
                    {
                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//input[@type='text']"));
                        Thread.Sleep(2000);
                        ele[1].Clear();
                        Thread.Sleep(1000);
                        ele[1].SendKeys(maotpctsc);
                    }
                    Thread.Sleep(2000);
                    try
                    {
                        chrome.FindElement(By.XPath("//input[@id=\"iVerifyPhoneViewAction\"]")).Click();
                        Thread.Sleep(2000);
                    }
                    catch
                    {
                        chrome.FindElement(By.XPath("//input[@id=\"ProofAction\"]")).Click();
                        Thread.Sleep(2000);
                    }
                    try
                    {
                        chrome.FindElement(By.XPath("//input[@id='FinishAction']")).Click();
                    }
                    catch { }
                    Thread.Sleep(2000);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;

            }
        }
        public static string doipassdang5(ChromeDriver chrome, string passcu, string passmoi)
        {
            try
            {
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(passmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                Thread.Sleep(2000);
                return passmoi;
            }
            catch
            {
                return "";
            }
        }

        public static string doipasskhoiphuc(ChromeDriver chrome, string passmoi)
        {
            try
            {
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(passmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(passmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iResetPasswordAction']")).Click();
                Thread.Sleep(2000);
                return passmoi;
            }
            catch
            {
                return "";
            }
        }
        public static string doipass(ChromeDriver chrome, string passcu, string passmoi)
        {
            try
            {
                chrome.Url = "https://account.live.com/password/Change";
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='CurrentPassword']")).SendKeys(passcu);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iPassword']")).SendKeys(passmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='iRetypePassword']")).SendKeys(passmoi);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='UpdatePasswordAction']")).Click();
                Thread.Sleep(2000);
                return passmoi;
            }
            catch
            {
                return "";
            }
        }
        public static string layuidgetnada(HttpRequest http, string mailkkhoiphuc)
        {
            try
            {
                string inboxgetnada1 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkkhoiphuc.ToLower()).ToString();
                Regex get1 = new Regex("uid\":\"\\w+");
                Match get2 = get1.Match(inboxgetnada1);
                string uidgetnada = get2.ToString().Replace("uid\":\"", "");
                return uidgetnada;
            }
            catch
            {
                return "";
            }

        }
        public static string laymaotpgetnada(HttpRequest http, string uidgetnada)
        {
            try
            {
                string laymagetnada1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnada).ToString();
                Regex get3 = new Regex("\\d+</span>");
                Match get4 = get3.Match(laymagetnada1);
                string otpgetnada = get4.ToString().Replace("</span>", "");
                return otpgetnada;
            }
            catch
            {
                return "";
            }
        }
        public static string msgtext;
        public static bool killchorme()
        {
            try
            {
                Process[] processesChrome = Process.GetProcessesByName("chromedriver");
                foreach (Process processChrome in processesChrome)
                {
                    processChrome.Kill();
                }
                Process[] processeshost = Process.GetProcessesByName("conhost");
                foreach (Process procehost in processeshost)
                {
                    procehost.Kill();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool startstop;
        public static string otpmailnesia(string mailkkhoiphuc, string idcanlay)
        {
            string maotp = "";
            try
            {
                mailkkhoiphuc = mailkkhoiphuc.Substring(0, mailkkhoiphuc.IndexOf("@"));
                //try
                //{
                var client = new RestClient("http://mailnesia.com/mailbox/" + mailkkhoiphuc + "/" + idcanlay + "?noheadernofooter=ajax");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cookie", "language=en; _ga=GA1.2.1507483214.1662135497; _gid=GA1.2.1212599142.1662135497; prefetchAd_4061430=true; mailbox=regcaimail; language=en; mailbox=regcaimail");
                request.AddHeader("Referer", "http://mailnesia.com/mailbox/regcaimail");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                IRestResponse response = client.Execute(request);
                string layotp = response.Content;

                Regex otp1 = new Regex(": \\d+");
                Match otp2 = otp1.Match(layotp);
                maotp = otp2.ToString().Replace(": ", "");


                return maotp;
            }
            catch
            {
                return "";

            }
            //}
            //catch
            //{
            //    return "";

            //}
        }
        public static string layuidmailnesia(string mailkhoiphucmoi)
        {
            string emailoke = mailkhoiphucmoi.Replace("@mailnesia.com", "");
            string idcanlay = "";
            try
            {
                var client = new RestClient("http://mailnesia.com/mailbox/" + emailoke);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cookie", "language=en; _ga=GA1.2.1507483214.1662135497; _gid=GA1.2.1212599142.1662135497; prefetchAd_4061430=true; mailbox=docaimail; _gat_gtag_UA_17894100_2=1; language=en; mailbox=regcaimail");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";
                IRestResponse response = client.Execute(request);
                string layuid = response.Content;
                //string layuid = http.Get("http://mailnesia.com/mailbox/docaimail").ToString();
                Regex b1 = new Regex("<tr id=.\\d+");
                Match b2 = b1.Match(layuid);
                idcanlay = b2.ToString().Replace("<tr id=\"", "");
                return idcanlay;
            }
            catch
            {
                return "";

            }
        }

        public static bool Login_hotmail(ChromeDriver chrome, string email, string passmail)
        {
            try
            {
                chrome.Url = "https://login.live.com/login.srf";
                //login hotmail
                chrome.FindElement(By.XPath("//input[@name='loginfmt']")).SendKeys(email);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@name='passwd']")).SendKeys(passmail);
                Thread.Sleep(2000);
                chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                Thread.Sleep(2000);
                try
                {
                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                }
                catch
                {
                }
                try
                {
                    Thread.Sleep(1000);
                    chrome.FindElement(By.XPath("//button[@type='button']")).Click();
                }
                catch
                {

                }
                try
                {
                    chrome.FindElement(By.XPath("//button[@type='button']")).Click();

                }
                catch {  }
                try
                {
                    chrome.FindElement(By.XPath("//span[@id='id__0']")).Click();
                }

                catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool getnadaopen(string mailkhoiphuc, HttpRequest http)
        {
            bool opengetnada = false;
            try
            {
               string open =  http.Get("https://inboxes.com/api/v2/inbox/" + mailkhoiphuc).ToString();
                Regex get1 = new Regex("msgs");
                Match get2 = get1.Match(open);
                if(get2 != Match.Empty)
                {
                    opengetnada = true;
                }
            }
            catch 
            {
            }
            return opengetnada;
        }
        public static string GetnadaGetUrlDelete(string mailkhoiphuc, HttpRequest http)
        {
            string url = "";
            try
            {
                string open = http.Get("https://inboxes.com/api/v2/inbox/" + mailkhoiphuc).ToString();
                MatchCollection matches = Regex.Matches(open, "uid\":\"\\w+");
                foreach (Match match in matches)
                {
                    //Console.WriteLine(match.Value);
                    string uidgetnada = match.Value.Replace("uid\":\"", "");
                    string ok = http.Get("https://inboxes.com/read/" + uidgetnada).ToString();
                    Regex url1 = new Regex("https://account.live.com.+\\d");
                    Match url2 = url1.Match(ok);
                    if (url2 != Match.Empty)
                    {
                        url = url2.ToString().Replace(": ", "");
                        break;
                    }
                }
            }
            catch { }
            return url;
        }
            public static string opengetnada(string mailkhoiphuc, ChromeDriver chrome, HttpRequest http)
        {
            //string url = "";
            // GET TOKEN VÀ UID  
            chrome.Url = "https://inboxes.com";
            Thread.Sleep(2000);

            chrome.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(2000);
            string cookie = chrome.Manage().Cookies.AllCookies.ToString();
            for (int c = 0; c < chrome.Manage().Cookies.AllCookies.Count; c++)
            {
                string name = chrome.Manage().Cookies.AllCookies[c].Name.ToString();
                string value = chrome.Manage().Cookies.AllCookies[c].Value.ToString();
                string cc = name + "=" + value;
                cookie = cc + ";" + cookie;
            }
              //HttpRequest http = new HttpRequest();
            http.AddHeader("authority", "inboxes.com");
            http.AddHeader("accept", "*/*");
            http.AddHeader("accept-language", "en-US,en;q=0.9");
            http.AddHeader("cookie", cookie);
            http.AddHeader("origin", "https://inboxes.com");
            http.AddHeader("referer", "https://inboxes.com/");
            http.AddHeader("sec-ch-ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"");
            http.AddHeader("sec-ch-ua-mobile", "?0");
            http.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            http.AddHeader("sec-fetch-dest", "empty");
            http.AddHeader("sec-fetch-mode", "cors");
            http.AddHeader("sec-fetch-site", "same-origin");
            http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
            var data = "{}";
            string getuid = http.Post("https://inboxes.com/api/v2/signup", data, "application/json").ToString();
            return cookie;
          

        }
        public static string getotpgetnadanew(string mailkhoiphuc, ChromeDriver chromedocma)
        {
            string otp = "";
            try
            {
                mailkhoiphuc = mailkhoiphuc.Substring(0, mailkhoiphuc.IndexOf("@"));
                chromedocma.Url = "https://inboxes.com/";
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("/html/body/div/main/div/div/div/div[1]/button[4]")).Click();
                Thread.Sleep(2000);
                chromedocma.FindElement(By.XPath("//input[@id='username']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(2000);
                chromedocma.FindElement(By.XPath("/html/body/div/main/div/div/div/div[1]/div[2]/div/div/div/form/div[2]/div[2]/select")).Click();
                Thread.Sleep(2000);
                chromedocma.FindElement(By.XPath("//option[@value='getnada.com']")).Click();
                Thread.Sleep(2000);
                chromedocma.FindElement(By.XPath("/html/body/div/main/div/div/div/div[1]/div[2]/div/div/div/form/button[1]")).Click();
                Thread.Sleep(5000);
                chromedocma.FindElement(By.XPath("/html/body/div/main/div/div/div/div[2]/div/div[1]/div[1]/div[2]/div[1]/button")).Click();
                IList<IWebElement> ele = chromedocma.FindElements(By.XPath("//span[text() = 'Microsoft account team <account-security-noreply@accountprotection.microsoft.com>']"));
                if(ele.Count > 0)
                {
                    ele[0].Click();
                }
                else
                {
                    return otp;
                }
                Regex otp1 = new Regex(": \\d+");
                Match otp2 = otp1.Match(chromedocma.PageSource);
                if (otp2 != Match.Empty)
                {
                    otp = otp2.ToString().Replace(": ", "");
                }
                return otp;
            }
            catch
            {
                return otp;
            }
        }
        public static string getcodemailgetnada(string mailkhoiphuc, HttpRequest http)
        {
            string otpgetnada = "";
            try
            {
                //http.AddHeader("authority", "inboxes.com");
                //http.AddHeader("accept", "*/*");
                //http.AddHeader("accept-language", "en-US,en;q=0.9");
                //http.AddHeader("cookie", cookie);
                //http.AddHeader("origin", "https://inboxes.com");
                //http.AddHeader("referer", "https://inboxes.com/");
                //http.AddHeader("sec-ch-ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"");
                //http.AddHeader("sec-ch-ua-mobile", "?0");
                //http.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                //http.AddHeader("sec-fetch-dest", "empty");
                //http.AddHeader("sec-fetch-mode", "cors");
                //http.AddHeader("sec-fetch-site", "same-origin");
                //http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
                //var data = "{}";
                //string getuid = http.Post("https://inboxes.com/api/v2/signup", data, "application/json").ToString();
                //JObject jObject = JObject.Parse(getuid);
                //string user_id = jObject["user_id"].ToString();
                //string token = jObject["token"].ToString();
                //string refresh_token = jObject["refresh_token"].ToString();
                //string cookieuserid = "user_id=" + user_id + ";";
                //cookie = cookieuserid + cookie;
                //http.AddHeader("cookie", cookie);
                //http.AddHeader("authorization", "Bearer " + token);
                //string mail = http.Get("https://inboxes.com/api/v2/inbox/" + mailkhoiphuc).ToString();
                //Regex get1 = new Regex("uid\":\"\\w+");
                //Match get2 = get1.Match(mail);
                //string uidgetnada = get2.ToString().Replace("uid\":\"", "");
                //string ok = http.Get("https://inboxes.com/read/" + uidgetnada).ToString();
                //Regex get3 = new Regex("\\d+</span>");
                //Match get4 = get3.Match(ok);
                //otpgetnada = get4.ToString().Replace("</span>", "");
                string open = http.Get("https://inboxes.com/api/v2/inbox/" + mailkhoiphuc).ToString();
                MatchCollection matches = Regex.Matches(open, "uid\":\"\\w+");
                foreach (Match match in matches)
                {
                    //Console.WriteLine(match.Value);
                    string uidgetnada = match.Value.Replace("uid\":\"", "");
                    string ok = http.Get("https://inboxes.com/read/" + uidgetnada).ToString();
                    Regex url1 = new Regex("\\d+</span>");
                    Match url2 = url1.Match(ok);
                    if (url2 != Match.Empty)
                    {
                        otpgetnada = url2.ToString().Replace("</span>", "");
                        break;
                    }
                }
                return otpgetnada;
            }
            catch
            {
                // chromedocma.Quit();
                return otpgetnada;
            }
        }
        public static string getlinkdeletemailgetnda(string mailkhoiphuc,HttpRequest http,string cookie)
        {
            string url = "";
            try
            {
                http.AddHeader("authority", "inboxes.com");
                http.AddHeader("accept", "*/*");
                http.AddHeader("accept-language", "en-US,en;q=0.9");
                http.AddHeader("cookie", cookie);
                http.AddHeader("origin", "https://inboxes.com");
                http.AddHeader("referer", "https://inboxes.com/");
                http.AddHeader("sec-ch-ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"");
                http.AddHeader("sec-ch-ua-mobile", "?0");
                http.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                http.AddHeader("sec-fetch-dest", "empty");
                http.AddHeader("sec-fetch-mode", "cors");
                http.AddHeader("sec-fetch-site", "same-origin");
                http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
                var data = "{}";
                string getuid = http.Post("https://inboxes.com/api/v2/signup", data, "application/json").ToString();
                JObject jObject = JObject.Parse(getuid);
                string user_id = jObject["user_id"].ToString();
                string token = jObject["token"].ToString();
                string refresh_token = jObject["refresh_token"].ToString();
                string cookieuserid = "user_id=" + user_id + ";";
                cookie = cookieuserid + cookie;
                http.AddHeader("cookie", cookie);
                http.AddHeader("authorization", "Bearer " + token);
                string mail = http.Get("https://inboxes.com/api/v2/inbox/" + mailkhoiphuc).ToString();
            //Regex get1 = new Regex("uid\":\"\\w+");
            //Match get2 = get1.Match(mail);
                MatchCollection matches = Regex.Matches(mail, "uid\":\"\\w+");
                foreach (Match match in matches)
                {
                    //Console.WriteLine(match.Value);
                    string uidgetnada = match.Value.Replace("uid\":\"", "");
                    string ok = http.Get("https://inboxes.com/read/" + uidgetnada).ToString();
                    Regex url1 = new Regex("https://account.live.com.+\\d");
                    Match url2 = url1.Match(ok);
                    if (url2 != Match.Empty)
                    {
                        url = url2.ToString().Replace(": ", "");
                        break;
                    }
                }
                return url;
            }
                
            catch
            {
               // chromedocma.Quit();
                return url;
            }
        }
        public static string taomail(string mailkhoiphuc, ChromeDriver chromedocma)
        {
            try
            {
                mailkhoiphuc = mailkhoiphuc.Substring(0, mailkhoiphuc.IndexOf("@"));
                chromedocma.Url = "https://www.moakt.com/en/";
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//input[@name='username']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//select[@name='domain']")).Click();
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//option[@value='" + NeedInfo.duoimoakkt + "']")).Click();
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//input[@name='setemail']")).Click();
                Thread.Sleep(2000);
                chromedocma.Url = "https://www.moakt.com/en/inbox";
                Thread.Sleep(1000);

                return "success";
            }
            catch
            {
                return "";

            }
        }
        public static string nhanmaotp(ChromeDriver chromedocma)
        {
            try
            {
                string maotp = "";
                string docma = chromedocma.PageSource;
                Regex m1 = new Regex("<td><a href=\"/en/email/.+\"");
                Match m2 = m1.Match(docma);
                if (m2 != Match.Empty)
                {
                    string uidcandoc = m2.ToString().Replace("<td><a href=\"/en/email/", "").Replace("\"", "");
                    chromedocma.Url = "https://www.moakt.com/en/email/" + uidcandoc + "/content/";
                    Regex otp1 = new Regex(": \\d+");
                    Match otp2 = otp1.Match(chromedocma.PageSource);
                    if (otp2 != Match.Empty)
                    {
                        maotp = otp2.ToString().Replace(": ", "");
                        return maotp;
                    }

                }
                else
                {
                    return "";
                }
                return maotp;
            }
            catch
            {
                return "";
            }
        }
        public static string themmoaktcc(string mailkhoiphuc, ChromeDriver chromedocma)
        {
            try
            {
                chromedocma.Url = "https://www.moakt.com/en/inbox/logout";
                Thread.Sleep(1000);
                string daumailkhoiphuc = mailkhoiphuc.Substring(0, mailkhoiphuc.IndexOf("@"));
                string duoimailkhoiphuc = mailkhoiphuc.Substring(mailkhoiphuc.IndexOf("@") + 1);
                chromedocma.Url = "https://www.moakt.com/en/";
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//input[@name='username']")).SendKeys(daumailkhoiphuc);
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//select[@name='domain']")).Click();
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//option[@value='" + duoimailkhoiphuc + "']")).Click();
                Thread.Sleep(1000);
                chromedocma.FindElement(By.XPath("//input[@name='setemail']")).Click();
                Thread.Sleep(2000);
                chromedocma.Url = "https://www.moakt.com/en/inbox";
                Thread.Sleep(1000);

                return "success";
            }
            catch
            {
                return "";

            }
        }
        public static string Get_cookiegetnada(ChromeDriver chrome, HttpRequest http)
        {
            try
            {
                http.Cookies = new CookieDictionary(false);
                string cookie = chrome.Manage().Cookies.AllCookies.ToString();
                for (int c = 0; c < chrome.Manage().Cookies.AllCookies.Count; c++)
                {
                    string name = chrome.Manage().Cookies.AllCookies[c].Name.ToString();
                    string value = chrome.Manage().Cookies.AllCookies[c].Value.ToString();
                    string cc = name + "=" + value;
                    cookie = cc + ";" + cookie;
                }
                //cookie = "__utmc=213295240; __utmz=213295240.1662798618.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); tm_session=ca724fed58131facd11931343ce4047e; __gads=ID=84635eb24e369e7a-228af92c10d5007d:T=1662798812:RT=1662798812:S=ALNI_Ma8tYlKJ_8_txZBuKpWwhWtxS9lSg; __gpi=UID=000008ac437a7efb:T=1662798812:RT=1662822478:S=ALNI_Ma-08F99xU-JiiQsQXxzqwGGcdqpA; __utma=213295240.807032019.1662798618.1662822297.1662833948.4; __utmt=1; __utmb=213295240.55.10.1662833948";
                for (int c = 0; c < cookie.Split(';').Length; c++)
                {
                    try
                    {
                        string name = cookie.Split(';')[c].Split('=')[0].Trim();
                        string value = cookie.Split(';')[c].Substring(cookie.Split(';')[c].IndexOf('=') + 1).Trim();
                        if (http.Cookies.ContainsKey(name))
                            http.Cookies.Remove(name);

                        if ("__utmc".Equals(name) || "utmccn".Equals(name) || "tm_session".Equals(name) || "__gads".Equals(name) || "__gpi".Equals(name) || "__utma".Equals(name) || "__utmt".Equals(name) || "__utmb".Equals(name))
                            http.Cookies.Add(name, value);
                    }
                    catch (Exception ex) { }

                }
                return cookie;
            }
            catch
            {
                return "";
            }
        }
        public static string Get_cookie(ChromeDriver chrome, HttpRequest http)
        {
            try
            {
                http.Cookies = new CookieDictionary(false);
                string cookie = chrome.Manage().Cookies.AllCookies.ToString();
                for (int c = 0; c < chrome.Manage().Cookies.AllCookies.Count; c++)
                {
                    string name = chrome.Manage().Cookies.AllCookies[c].Name.ToString();
                    string value = chrome.Manage().Cookies.AllCookies[c].Value.ToString();
                    string cc = name + "=" + value;
                    cookie = cc + ";" + cookie;
                }
                cookie = "__utmc=213295240; __utmz=213295240.1662798618.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); tm_session=ca724fed58131facd11931343ce4047e; __gads=ID=84635eb24e369e7a-228af92c10d5007d:T=1662798812:RT=1662798812:S=ALNI_Ma8tYlKJ_8_txZBuKpWwhWtxS9lSg; __gpi=UID=000008ac437a7efb:T=1662798812:RT=1662822478:S=ALNI_Ma-08F99xU-JiiQsQXxzqwGGcdqpA; __utma=213295240.807032019.1662798618.1662822297.1662833948.4; __utmt=1; __utmb=213295240.55.10.1662833948";
                for (int c = 0; c < cookie.Split(';').Length; c++)
                {
                    try
                    {
                        string name = cookie.Split(';')[c].Split('=')[0].Trim();
                        string value = cookie.Split(';')[c].Substring(cookie.Split(';')[c].IndexOf('=') + 1).Trim();
                        if (http.Cookies.ContainsKey(name))
                            http.Cookies.Remove(name);

                        if ("__utmc".Equals(name) || "utmccn".Equals(name) || "tm_session".Equals(name) || "__gads".Equals(name) || "__gpi".Equals(name) || "__utma".Equals(name) || "__utmt".Equals(name) || "__utmb".Equals(name))
                            http.Cookies.Add(name, value);
                    }
                    catch (Exception ex) { }

                }
                return cookie;
            }
            catch
            {
                return "";
            }
        }
        public static string getmamoakt(ChromeDriver chromedocma)
        {
            try
            {
                chromedocma.Url = "https://www.moakt.com/en/inbox/";
                Thread.Sleep(1000);
                string maotp = "";
                string docma = chromedocma.PageSource;
                Regex m1 = new Regex("<td><a href=\"/en/email/.+\"");
                Match m2 = m1.Match(docma);
                if (m2 != Match.Empty)
                {
                    string uidcandoc = m2.ToString().Replace("<td><a href=\"/en/email/", "").Replace("\"", "");
                    chromedocma.Url = "https://www.moakt.com/en/email/" + uidcandoc + "/content/";
                    Thread.Sleep(1000);
                    string docmail = chromedocma.PageSource;
                    Regex otp1 = new Regex(">\\d+<");
                    Match otp2 = otp1.Match(docmail);
                    if (otp2 != Match.Empty)
                    {
                        maotp = otp2.ToString().Replace("<", "").Replace(">", "");
                        return maotp;
                    }

                }
                else
                {
                    return "";
                }
                return maotp;
            }
            catch
            {
                return "";
            }
        }
        public static bool check_dang1(ChromeDriver chrome) // cp sđt dạng mớ chưa có mail khôi phục
        {
            try
            {
                // B1 Xác minh sđt và đổi pass
                chrome.FindElement(By.XPath("//input[@aria-describedby='iPageTitle iIntroText']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//select[@id='DisplayPhoneCountryISO']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool check_dang2(ChromeDriver chrome) // dạng thường 
        {
            try
            {
                chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//select[@class='form-control input-max-width']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;

            }
        }
        public static bool check_dang3(ChromeDriver chrome, string mailkhoiphuc) // mở khóa cp mail đã add khôi phục dạng mới bắt xm mail khi login ( Sẽ bắt đổi pass luôn) có mail khôi phục sẵn 
        {
            try
            {
                chrome.FindElement(By.XPath("//input[@id='iProofEmail']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='iSelectProofAction']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool check_dang4(ChromeDriver chrome, string mailkhoiphuc)//xm mail có sẵn tk ko phải đổi pass
        {
            try
            {
                chrome.FindElement(By.XPath("//div[@class='table-cell text-left content']")).Click(); ;
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                Thread.Sleep(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool xoasdt(ChromeDriver chrome)
        {
            try
            {

                chrome.Url = "https://account.live.com/proofs/manage/additional";
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//div[@id='SMS0']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//button[@id='Remove']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//button[@id='iBtn_action']")).Click();
                Thread.Sleep(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool xoamail(ChromeDriver chrome)
        {
            try
            {
                chrome.Url = "https://account.live.com/proofs/manage/additional";
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//div[@id='Email0']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//button[@id='Remove']")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//button[@id='iBtn_action']")).Click();
                chrome.Url = "https://account.live.com/proofs/manage/additional";
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
