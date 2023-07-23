using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace Hotmail_Change_V1._0
{
   public class xoathumailcu
    {
        public static bool xoathu(string mailkhoiphuc)
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.AddHeader("authority", "getnada.com");
                http.AddHeader("accept", "application/json, text/plain, */*");
                http.AddHeader("accept-language", "en-US,en;q=0.9");
                http.AddHeader("origin", "https://getnada.com");
                http.AddHeader("referer", "https://getnada.com/");
                http.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"100\", \"Google Chrome\";v=\"100\"");
                http.AddHeader("sec-ch-ua-mobile", "?0");
                http.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                http.AddHeader("sec-fetch-dest", "empty");
                http.AddHeader("sec-fetch-mode", "cors");
                http.AddHeader("sec-fetch-site", "same-origin");
                http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36";
                string mailcu = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                mailcu = mailcu.Substring(mailcu.IndexOf("msgs\":[")).Replace("msgs\":[", "").Replace("]}", "").Trim();
                string[] catmailcu = mailcu.Split('}');
                for (int i = 0; i < catmailcu.Length; i++)
                {
                    string mail1 = catmailcu[i];
                    if(mail1 == "")
                    {
                        break;
                    }
                    string uidxoa = mail1.Substring(mail1.IndexOf("uid\":\"")).Replace("uid\":\"", "").Substring(0, 30);
                    var client = new RestClient("https://getnada.com/api/v1/messages/" + uidxoa);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.DELETE);
                    IRestResponse response = client.Execute(request);
                    string responseData = response.Content;
                    
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
