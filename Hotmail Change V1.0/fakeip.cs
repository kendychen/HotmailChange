using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xNet;

namespace Hotmail_Change_V1._0
{
    public class fakeip
    {
        public static string shoplike(string keyshoplike)
        {
            string proxy = "";
            try
            {
                // lấy ip mới 
                HttpRequest http = new HttpRequest();
                string response = http.Get("https://proxy.shoplike.vn/Api/getNewProxy?access_token=" + keyshoplike).ToString();
                Regex sucess = new Regex("status\":\"success");
                Match cucess1 = sucess.Match(response);
                if (cucess1 != Match.Empty)
                {
                    Regex pr = new Regex("proxy\":\"\\d+.\\d+.\\d+.\\d+:\\d+");
                    Match proxy1 = pr.Match(response);
                    proxy = proxy1.ToString().Replace("proxy\":\"", "");
                    return proxy;
                }
                else
                {
                    // lấy ip hiện tại 
                    string iphientai = http.Get("https://proxy.shoplike.vn/Api/getCurrentProxy?access_token=" + keyshoplike).ToString();
                    Regex ip1 = new Regex("proxy\":\"\\d+.\\d+.\\d+.\\d+:\\d+");
                    Match ip2 = ip1.Match(iphientai);
                    proxy = ip2.ToString().Replace("proxy\":\"", "");
                    return proxy;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return error;
            }
        }
    }
}
