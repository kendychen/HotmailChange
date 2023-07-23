using AutoUpdaterDotNET;
using DeviceId;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace Hotmail_Change_V1._0
{
    public partial class Form1 : Form
    {

        Queue<int> qu_position_pro5 = new Queue<int>();
        public Form1()
        {

            InitializeComponent();
            this.FormClosing += this.fmain_FormClosing;
            this.dataGridView1.CurrentCellDirtyStateChanged += dataGridViewTable_CurrentCellDirtyStateChanged;
            loadfilelog();
        }
        void loadfilelog()
        {
            try
            {
                string[] data = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Log Data\\" + $"fullacc.txt").Trim().Split('\n');
                List<NeedInfo> needInfos = new List<NeedInfo>();
                for (int i = 0; i < data.Length; i++)
                {
                    NeedInfo needInfo = new NeedInfo();

                    needInfo.stt = Int32.Parse(data[i].Split(new[] { "<>" }, StringSplitOptions.None)[0].Trim());
                    needInfo.tick = false;
                    if (data[i].Split(new[] { "<>" }, StringSplitOptions.None)[1].Trim().Equals("True"))
                        needInfo.tick = true;

                    needInfo.fullacc = data[i].Split(new[] { "<>" }, StringSplitOptions.None)[2].Trim();
                    needInfo.mailkhoiphucu = data[i].Split(new[] { "<>" }, StringSplitOptions.None)[3].Trim();
                    needInfo.success = data[i].Split(new[] { "<>" }, StringSplitOptions.None)[4].Trim();
                    needInfo.status = data[i].Split(new[] { "<>" }, StringSplitOptions.None)[5].Trim();

                    try
                    {
                        needInfo.errorCodeStatus = Int32.Parse(data[i].Split(new[] { "<>" }, StringSplitOptions.None)[5].Trim());
                    }
                    catch
                    {
                        needInfo.errorCodeStatus = 0;
                    }

                    needInfos.Add(needInfo);
                }
                // Update to table
                dataGridView1.Rows.Clear();
                for (int i = 0; i < needInfos.Count; i++)
                {
                    int index = dataGridView1.Rows.Add();
                    //dataGridView1.Invoke((MethodInvoker)delegate ()
                    //{
                    dataGridView1.Rows[index].Cells["stt"].Value = needInfos[i].stt;
                    dataGridView1.Rows[index].Cells["tick"].Value = needInfos[i].tick;
                    dataGridView1.Rows[index].Cells["fullacc"].Value = needInfos[i].fullacc;
                    dataGridView1.Rows[index].Cells["mailcu"].Value = needInfos[i].mailkhoiphucu.ToLower();
                    dataGridView1.Rows[index].Cells["success"].Value = needInfos[i].success;
                    dataGridView1.Rows[index].Cells["status"].Value = needInfos[i].status;
                    //});
                }
            }
            catch (Exception ex) { }
        }
        void dataGridViewTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.mktudat = textBoxdoipass.Text;
            Settings1.Default.Save();
        }

        private void ctmntthemkoxoatkcu_Click(object sender, EventArgs e)
        {
            try
            {

                string[] data = Clipboard.GetText().Replace(" ", "").Trim().Split('\n');
                List<NeedInfo> needInfos = new List<NeedInfo>();
                for (int i = 0; i < data.Length; i++)
                {
                    NeedInfo needInfo = new NeedInfo();
                    needInfo.acc = data[i].Trim();
                    needInfos.Add(needInfo);
                }
                // Update to table
                int max_curr_row = dataGridView1.Rows.Count;
                for (int i = 0; i < needInfos.Count; i++)
                {

                    int index = dataGridView1.Rows.Add();
                    needInfos[i].index = index;

                    dataGridView1.Invoke((MethodInvoker)delegate ()
                    {
                        try
                        {
                            dataGridView1.Rows[index].Cells["stt"].Value = needInfos[i].index + 1;
                            dataGridView1.Rows[index].Cells["fullacc"].Value = needInfos[i].acc;
                            string xacdinhvitri1 = (string)dataGridView1.Rows[i].Cells[2].Value;
                            string[] tk1 = xacdinhvitri1.Split('|');
                            dataGridView1.Rows[index].Cells[3].Value = tk1[2].ToLower();
                        }
                        catch { }

                    });
                }
                MessageBox.Show($"Nhập thành công {needInfos.Count} tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
            WriteLog();
        }

        private void ctmntthemxoatkcu_Click(object sender, EventArgs e)
        {
            try
            {

                string[] data = Clipboard.GetText().Trim().Split('\n');
                List<NeedInfo> needInfos = new List<NeedInfo>();
                for (int i = 0; i < data.Length; i++)
                {
                    NeedInfo needInfo = new NeedInfo();
                    needInfo.acc = data[i].Trim();
                    needInfos.Add(needInfo);
                }
                dataGridView1.Rows.Clear();
                for (int i = 0; i < needInfos.Count; i++)
                {
                    int index = dataGridView1.Rows.Add();
                    needInfos[i].index = index;

                    dataGridView1.Invoke((MethodInvoker)delegate ()
                    {
                        try
                        {
                            dataGridView1.Rows[index].Cells["stt"].Value = needInfos[i].index + 1;
                            dataGridView1.Rows[index].Cells["fullacc"].Value = needInfos[i].acc;
                            string xacdinhvitri1 = (string)dataGridView1.Rows[i].Cells[2].Value;
                            string[] tk1 = xacdinhvitri1.Split('|');
                            dataGridView1.Rows[index].Cells[3].Value = tk1[2].ToLower();

                        }
                        catch { }
                    });
                }
                MessageBox.Show($"Nhập thành công {needInfos.Count} tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch { }
            WriteLog();
        }

        private void ctmntchontatca_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridView1.Rows.Count; r++)
            {
                dataGridView1.Rows[r].Cells["Tick"].Value = true;
            }

        }

        private void ctmntbochontatca_Click(object sender, EventArgs e)
        {
            try
            {
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    dataGridView1.Rows[r].Cells["Tick"].Value = false;
                }
            }
            catch { }
        }

        private void ctmntchondongboiden_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Invoke((MethodInvoker)delegate ()
                {
                    for (int r = 0; r < dataGridView1.Rows.Count; r++)
                    {
                        if (dataGridView1.Rows[r].Cells["fullacc"].Selected)
                        {
                            dataGridView1.Rows[r].Cells[1].Value = true;
                        }
                        else
                        {
                            if ((bool)dataGridView1.Rows[r].Cells[1].FormattedValue)
                            {
                                dataGridView1.Rows[r].Cells[1].Value = false;
                            }
                        }
                    }
                });
            }
            catch { }
        }

        private void ctmntchondongstatus_Click(object sender, EventArgs e)
        {
            new TickByStatus().ShowDialog();

            // có status rồi tích thôi
            for (int r = 0; r < dataGridView1.Rows.Count; r++)
            {
                if (dataGridView1.Rows[r].Cells["status"].Value != null &&
                    dataGridView1.Rows[r].Cells["status"].Value.ToString().Contains(ConfigInfo.keyword))
                {
                    dataGridView1.Rows[r].Cells[1].Value = true;
                }
                else
                {
                    if ((bool)dataGridView1.Rows[r].Cells[1].FormattedValue)
                    {
                        dataGridView1.Rows[r].Cells[1].Value = false;
                    }
                }
            }
        }

        private void ctmntxoadongchon_Click(object sender, EventArgs e)
        {
            try
            {
                for (int r = dataGridView1.Rows.Count - 1; r >= 0; r--)
                {
                    if ((bool)dataGridView1.Rows[r].Cells[1].FormattedValue)
                        dataGridView1.Rows.Remove(dataGridView1.Rows[r]);
                }
            }
            catch { }
        }

        private void ctmntxoadongkochon_Click(object sender, EventArgs e)
        {

            try
            {
                for (int r = dataGridView1.Rows.Count - 1; r >= 0; r--)
                {
                    if (!(bool)dataGridView1.Rows[r].Cells[1].FormattedValue)
                        dataGridView1.Rows.Remove(dataGridView1.Rows[r]);
                }
            }
            catch { }
        }

        private void ctmntxoatoanbodong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Chắc chắn xóa toàn bộ dòng Tài Khoản Không?", "Thông Báo Quan Trọng?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                chucnang.checkluufile = true;
                try
                {
                    string data_init = File.ReadAllText(Directory.GetCurrentDirectory() + "\\init_info.json"); // doc file
                    JObject jObject = JObject.Parse(data_init);
                    ConfigInfo.chromeWidth = Int32.Parse(jObject["chromeWidth"].ToString());
                    ConfigInfo.chromeHeight = Int32.Parse(jObject["chromeHeight"].ToString());
                    ConfigInfo.chromeDistanceX = Int32.Parse(jObject["chromeDistanceX"].ToString());
                    ConfigInfo.chromeDistanceY = Int32.Parse(jObject["chromeDistanceY"].ToString());
                    string ok = "";
                }
                catch { }
                try
                {
                    JObject jObject = new JObject();
                    jObject["chromeWidth"] = ConfigInfo.chromeWidth;
                    jObject["chromeHeight"] = ConfigInfo.chromeHeight;
                    jObject["chromeDistanceX"] = ConfigInfo.chromeDistanceX;
                    jObject["chromeDistanceY"] = ConfigInfo.chromeDistanceY;
                    string init_info = JsonConvert.SerializeObject(jObject);
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\init_info.json", init_info);

                }
                catch
                {
                }
                List<string> data = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    string acc = dataGridView1.Rows[i].Cells["fullacc"].Value.ToString();
                    data.Add($"{acc}");
                }
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\output"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\output");
                }
                string filename = $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}Gio_{DateTime.Now.Minute}phut_{DateTime.Now.Second}giay";
                chucnang.accfile = string.Join("\n", data);
                string urlfile = Directory.GetCurrentDirectory() + $"\\output\\full_account_{filename}.txt";

                int so_luong_dang_chay = 0; // lưu số luồng đang chạy đồng thời
                qu_position_pro5.Clear();
                for (int i = 1; i <= (int)numericUpDown1.Value; i++)
                    qu_position_pro5.Enqueue(i);
                chucnang.startstop = true;
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                Thread t = new Thread(() => {

                    int so_luong_toi_da = (int)numericUpDown1.Value; // số luồng tối đa

                    // nguyen tac 2: Thằng chạy xong thì gaim sô luong dang chay --
                    // nguyên tắc 3: Trước khi chạy thì kiểm tra so_luong_dang_chay < so_luong_toi_da
                    //  neu dat max thi cho den khi nao so_luong_dang_chay < so_luong_toi_da đúng [có thằng đang chạy mà hoàn thành]

                    // duyệt từ đầu tới cuối
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (chucnang.startstop == false)
                            break;
                        if (!(bool)dataGridView1.Rows[i].Cells[1].FormattedValue)
                            continue;

                        // chờ đến khi nào đủ điều kiện được chạy
                        while (true)
                        {
                            if (so_luong_dang_chay < so_luong_toi_da)
                                break;
                        }


                        // đã đạt yc được chạy

                        so_luong_dang_chay++;
                        int dong = i;

                        Thread t2 = new Thread(() =>
                        {
                            int pro5 = qu_position_pro5.Dequeue();

                            try
                            {
                                string xacdinhvitri1 = (string)dataGridView1.Rows[dong].Cells[2].Value;
                                string[] tk1 = xacdinhvitri1.Split('|');
                                NeedInfo acc = new NeedInfo();
                                acc.email = tk1[0];
                                acc.passmail = tk1[1];
                                try
                                {
                                    acc.mailkhoiphuc = tk1[2].ToLower();
                                }
                                catch (Exception) { }
                                string duoigetnada = textBoxduoimailkp.Text;
                                try
                                {
                                    if (radioButtongetnada.Checked)
                                    {
                                        acc.mailkhoiphucmoi = acc.email.Substring(0, acc.email.IndexOf("@")) + duoigetnada + "@getnada.com";
                                    }
                                    if (radioButtonmailnesia.Checked)
                                    {
                                        acc.mailkhoiphucmoi = acc.email.Substring(0, acc.email.IndexOf("@")) + duoigetnada + "@mailnesia.com";
                                    }
                                    if (radioButtonMoakt.Checked)
                                    {
                                        acc.mailkhoiphucmoi = acc.email.Substring(0, acc.email.IndexOf("@")) + duoigetnada + "@" + NeedInfo.duoimoakkt;
                                    }
                                }
                                catch { }
                                if (checkBoxxoamailkhoiphuc.Checked)
                                {
                                    xoamailkhoiphuc(dong, acc.email, acc.mailkhoiphuc, pro5);
                                }
                                else
                                {
                                    if (checkBoximap.Checked)
                                    {
                                        batimap(dong, acc.email, acc.passmail, acc.mailkhoiphuc, pro5);
                                    }
                                    else
                                    {
                                        mkpcosan(so_luong_dang_chay, dong, acc.email, acc.passmail, acc.mailkhoiphuc, acc.mailkhoiphucmoi, pro5, urlfile, duoigetnada);

                                    }
                                }
                                Thread.Sleep(1000);

                            }
                            catch (Exception) { }

                            so_luong_dang_chay--;
                            qu_position_pro5.Enqueue(pro5);
                            dataGridView1.Rows[dong].Cells[1].Value = false;
                            // chạy xong hết dòng này thì update lại số luồng để dòng khác được chạy
                            // khi chạy xong
                        });
                        t2.Start();
                        Thread.Sleep(1000);

                    }
                    while (true)
                    {
                        if (so_luong_dang_chay == 0)
                        {
                            dataGridView1.Invoke((MethodInvoker)delegate ()
                            {
                                buttonStart.Enabled = true;
                            });
                            chucnang.killchorme();
                            MessageBox.Show("KẾT THÚC AUTO TOOL", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                    }
                });
                t.Start(); // gọi hàm chạy thread

            }
            catch { }

        }
        void batimap(int dong, string email, string passmail, string mailkhoiphuc, int pro5)
        {
            ChromeDriver chrome = null;
            bool checklgok = false;
            bool checpasss = false;
            ChromeDriverService server = ChromeDriverService.CreateDefaultService();
            server.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("-disable-site-isolation-trials");
            options.AddArgument("-disable-application-cache");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");
            options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            options.AddArgument("--disable-images");
            // 
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments("--disable-notifications", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage", "--disable-web-security", "--disable-rtc-smoothness-algorithm", "--disable-webrtc-hw-decoding", "--disable-webrtc-hw-encoding", "--disable-webrtc-multiple-routes", "--disable-webrtc-hw-vp8-encoding", "--enforce-webrtc-ip-permission-check", "--force-webrtc-ip-handling-policy", "--ignore-certificate-errors", "--disable-infobars", "--mute-audio", "--disable-popup-blocking");
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.popups", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.auto_select_certificate", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.mixed_script", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.media_stream", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_mic", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.protocol_handlers", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.midi_sysex", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.push_messaging", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.ssl_cert_decisions", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.metro_switch_to_desktop", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.protected_media_identifier", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.site_engagement", 1);
            options.AddUserProfilePreference("profile.default_content_setting_values.durable_storage", 1);
            options.AddUserProfilePreference("profile.managed_default_content_settings.images", 1);
            options.AddUserProfilePreference("useAutomationExtension", true);
            //options.AddArguments("headless");

            #region calc position for profile
            {
                // calc size
                int max_width = Screen.PrimaryScreen.Bounds.Width;
                int max_height = Screen.PrimaryScreen.Bounds.Height;
                int width = ConfigInfo.chromeWidth;
                int height = ConfigInfo.chromeHeight;
                options.AddArgument($"--window-size={width},{height}");

                // calc max position for pro5
                int distance_x = ConfigInfo.chromeDistanceX;
                int distance_y = ConfigInfo.chromeDistanceY;
                int max_column = (max_width - width) / distance_x + 1;
                int max_row = (max_height - height) / distance_y + 1;

                // calc position (pro5 % max_column == 0) ? (pro5 / max_column) % max_row : 
                int row = (pro5 % max_column == 0) ? (((pro5 / max_column) % max_row == 0) ? (pro5 / max_column) % max_row + 1 : (pro5 / max_column) % max_row) : (pro5 / max_column) % max_row + 1;
                int column = (pro5 % max_column == 0) ? max_column : pro5 % max_column;

                int margin_width_postion = (column - 1) * distance_x;
                int margin_height_position = (row - 1) * distance_y;

                string position = $"--window-position={margin_width_postion},{margin_height_position}";
                options.AddArgument(position);
            }
            #endregion
            chrome = new ChromeDriver(server, options);
            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Login Hotmail";
            if (chucnang.Login_hotmail(chrome, email, passmail) == true)
            {

                try
                {
                    IWebElement checklg = chrome.FindElement(By.XPath("//input[@id='idSIButton9']")); // login thẳng
                    checklgok = checklg.Displayed;
                }
                catch { }
                try
                {
                    IWebElement checksaipass = chrome.FindElement(By.XPath("//a[@id='idA_IL_ForgotPassword0']")); // sai pass
                    checpasss = checksaipass.Displayed;
                }
                catch
                {

                }
                //string checklogin = chrome.Url;
                //Regex checkok = new Regex("login.live.com/ppsecure");
                //Match checklg = checkok.Match(checklogin);
                if (checklgok == true)
                {
                    if (checpasss == true)
                    {
                        dataGridView1.Rows[dong].Cells[5].Value = "Sai Pass Hotmail";
                        chrome.Quit();
                    }
                    else
                    {
                        dataGridView1.Rows[dong].Cells[5].Value = "Login Hotmail Thành Công";
                        chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                        try
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Bật IMAP";
                            chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";
                            try
                            {
                                chrome.FindElement(By.XPath("//a[@id='iShowSkip']")).Click();
                                Thread.Sleep(1000);
                                chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";

                            }
                            catch
                            {

                            }
                            Thread.Sleep(2000);

                            for (int ch = 0; ch < 10; ch++)
                            {
                                string urlok = chrome.Url;
                                Regex checkurl = new Regex("options/mail/layout");
                                Match checkurl1 = checkurl.Match(urlok);
                                Regex checkurl2 = new Regex("options/mail/accounts");
                                Match checkurl3 = checkurl2.Match(urlok);
                                if (checkurl1 != Match.Empty | checkurl3 != Match.Empty)
                                {
                                    break;
                                }
                            }
                            chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";
                            Thread.Sleep(2000);
                            for (int ch1 = 0; ch1 < 10; ch1++)
                            {
                                string urlok = chrome.Url;
                                Regex checkurl = new Regex("options/mail/layout");
                                Match checkurl1 = checkurl.Match(urlok);
                                Regex checkurl2 = new Regex("options/mail/accounts");
                                Match checkurl3 = checkurl2.Match(urlok);
                                if (checkurl1 != Match.Empty | checkurl3 != Match.Empty)
                                {
                                    break;
                                }
                            }
                            try
                            {
                                chrome.FindElement(By.XPath("/html/body/div[5]/div[3]/div/div/div/div[2]/div[2]/div/div[1]/div[2]/button/span")).Click();
                            }
                            catch
                            {

                            }
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@type='radio']")).Click();
                            Thread.Sleep(3000);
                            try
                            {
                                chrome.FindElement(By.XPath("/html/body/div[5]/div[1]/div/div/div/div[2]/div[2]/div/div[2]/div[3]/button[1]/span/span/span")).Click();
                                dataGridView1.Rows[dong].Cells[5].Value = "Bật IMAP Thành Công";
                            }
                            catch
                            {
                                dataGridView1.Rows[dong].Cells[5].Value = "Đã bật IMAP";
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    dataGridView1.Rows[dong].Cells[5].Value = "Hotmail CP";
                }
            }
            else
            {
                dataGridView1.Rows[dong].Cells[5].Value = "Login hotmail thất bại";
                chrome.Quit();
            }


            try
            {
                chrome.Quit();
            }
            catch { }
            WriteLog();
        }
        void xoamailkhoiphuc(int i, string email, string mailkhoiphuc, int pro5)
        {



            string duoimailkhoiphuccu = "";
            try
            {
                duoimailkhoiphuccu = mailkhoiphuc.Substring(mailkhoiphuc.IndexOf("@"));
            }
            catch { }
            ChromeDriver chromedocma = null;

            if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
            {
                ChromeDriverService server1 = ChromeDriverService.CreateDefaultService();
                server1.HideCommandPromptWindow = true;
                ChromeOptions options1 = new ChromeOptions();
                options1.AddArguments("headless");
                options1.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                options1.AddArgument("--no-sandbox");
                options1.AddArgument("--disable-web-security");
                options1.AddArgument("--disable-blink-features=AutomationControlled");
                options1.AddArgument("-disable-site-isolation-trials");
                options1.AddArgument("-disable-application-cache");
                options1.AddExcludedArgument("enable-automation");
                options1.AddArgument("--start-maximized");
                options1.AddUserProfilePreference("credentials_enable_service", false);
                options1.AddUserProfilePreference("profile.password_manager_enabled", false);
                options1.AddArguments("--disable-notifications", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage", "--disable-web-security", "--disable-rtc-smoothness-algorithm", "--disable-webrtc-hw-decoding", "--disable-webrtc-hw-encoding", "--disable-webrtc-multiple-routes", "--disable-webrtc-hw-vp8-encoding", "--enforce-webrtc-ip-permission-check", "--force-webrtc-ip-handling-policy", "--ignore-certificate-errors", "--disable-infobars", "--mute-audio", "--disable-popup-blocking");
                options1.AddUserProfilePreference("profile.default_content_setting_values.notifications", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.popups", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.auto_select_certificate", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.mixed_script", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream_mic", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.protocol_handlers", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.midi_sysex", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.push_messaging", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.ssl_cert_decisions", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.metro_switch_to_desktop", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.protected_media_identifier", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.site_engagement", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.durable_storage", 1);
                options1.AddUserProfilePreference("profile.managed_default_content_settings.images", 1);
                options1.AddUserProfilePreference("useAutomationExtension", true);

                chromedocma = new ChromeDriver(server1, options1);
            }
            ChromeDriver chrome = null;
            try
            {
                ChromeDriverService server = ChromeDriverService.CreateDefaultService();
                server.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddArgument("-disable-site-isolation-trials");
                options.AddArgument("-disable-application-cache");
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--start-maximized");
                options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                options.AddArgument("--disable-images");
                // 
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddArguments("--disable-notifications", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage", "--disable-web-security", "--disable-rtc-smoothness-algorithm", "--disable-webrtc-hw-decoding", "--disable-webrtc-hw-encoding", "--disable-webrtc-multiple-routes", "--disable-webrtc-hw-vp8-encoding", "--enforce-webrtc-ip-permission-check", "--force-webrtc-ip-handling-policy", "--ignore-certificate-errors", "--disable-infobars", "--mute-audio", "--disable-popup-blocking");
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.popups", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.auto_select_certificate", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.mixed_script", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_mic", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.protocol_handlers", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.midi_sysex", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.push_messaging", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.ssl_cert_decisions", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.metro_switch_to_desktop", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.protected_media_identifier", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.site_engagement", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.durable_storage", 1);
                options.AddUserProfilePreference("profile.managed_default_content_settings.images", 1);
                options.AddUserProfilePreference("useAutomationExtension", true);
                options.AddArguments("headless");

                #region calc position for profile
                {
                    // calc size
                    int max_width = Screen.PrimaryScreen.Bounds.Width;
                    int max_height = Screen.PrimaryScreen.Bounds.Height;
                    int width = ConfigInfo.chromeWidth;
                    int height = ConfigInfo.chromeHeight;
                    options.AddArgument($"--window-size={width},{height}");

                    // calc max position for pro5
                    int distance_x = ConfigInfo.chromeDistanceX;
                    int distance_y = ConfigInfo.chromeDistanceY;
                    int max_column = (max_width - width) / distance_x + 1;
                    int max_row = (max_height - height) / distance_y + 1;

                    // calc position (pro5 % max_column == 0) ? (pro5 / max_column) % max_row : 
                    int row = (pro5 % max_column == 0) ? (((pro5 / max_column) % max_row == 0) ? (pro5 / max_column) % max_row + 1 : (pro5 / max_column) % max_row) : (pro5 / max_column) % max_row + 1;
                    int column = (pro5 % max_column == 0) ? max_column : pro5 % max_column;

                    int margin_width_postion = (column - 1) * distance_x;
                    int margin_height_position = (row - 1) * distance_y;

                    string position = $"--window-position={margin_width_postion},{margin_height_position}";
                    options.AddArgument(position);
                }
                #endregion
                chrome = new ChromeDriver(server, options);
                HttpRequest http = new HttpRequest();

                dataGridView1.Rows[i].Cells["status"].Style.ForeColor = Color.Blue;
                dataGridView1.Rows[i].Cells["status"].Value = "Bắt Đầu Xóa Mail Khôi Phục";
                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                {
                    string duoigetna = textBoxduoimailkp.Text;
                    string taomail = chucnang.themmoaktcc(mailkhoiphuc, chromedocma);
                    Thread.Sleep(1000);
                }
                chrome.Url = "https://account.live.com/ResetPassword.aspx?wreply=https://login.live.com/login.srf%3fwa%3dwsignin1.0%26rpsnv%3d13%26ct%3d1647183850%26rver%3d7.0.6737.0%26wp%3dMBI_SSL%26wreply%3dhttps%253a%252f%252foutlook.live.com%252fowa%252f%253fnlp%253d1%2526RpsCsrfState%253d897937cd-fa2d-a0c7-dcdf-ad2152167d6d%26id%3d292841%26aadredir%3d1%26CBCXT%3dout%26lw%3d1%26fl%3ddob%252cflname%252cwld%26cobrandid%3d90015%26contextid%3dAE5294E26DF8973C%26bk%3d1657613166&id=292841&uiflavor=web&cobrandid=90015&uaid=bab9b551ae0d4ec195434568e60b14a4&mkt=EN-US&lc=1033&bk=1657613166&mn=" + email;
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//span[@id=\"textproofOption0\"]")).Click();
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id=\"proofInput0\"]")).SendKeys(mailkhoiphuc);
                Thread.Sleep(1000);
                chrome.FindElement(By.XPath("//input[@id=\"iSelectProofAction\"]")).Click();
                Thread.Sleep(10000);
                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                {
                    try
                    {
                        string maotp = "";
                        chromedocma.Url = "https://www.moakt.com/en/inbox/";
                        Thread.Sleep(1000);
                        string docma = chromedocma.PageSource;
                        Regex m1 = new Regex("<td><a href=\"/en/email/.+\"");
                        Match m2 = m1.Match(docma);

                        for (int t = 0; m2 != Match.Empty; t++)
                        {
                            string uidcandoc = m2.ToString().Replace("<td><a href=\"/en/email/", "").Replace("\"", "");
                            chromedocma.Url = "https://www.moakt.com/en/email/" + uidcandoc + "/content/";
                            Thread.Sleep(1000);
                            Regex url1 = new Regex("https://account.live.com.+\\d");
                            Match url2 = url1.Match(chromedocma.PageSource);
                            if (url2 != Match.Empty)
                            {
                                string urlxoa = url2.ToString();
                                chrome.Url = urlxoa;
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                                Thread.Sleep(5000);
                                dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Blue;
                                dataGridView1.Rows[i].Cells[5].Value = "Xóa Mail Khôi Phục Thành Công";
                                string accok = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                accok = accok.Replace(mailkhoiphuc, "");
                                Thread.Sleep(1000);
                                dataGridView1.Rows[i].Cells[2].Value = accok;
                                File.AppendAllText(Directory.GetCurrentDirectory() + "\\output\\fullacc.txt", accok + "=> Xóa Mail Khôi Phục Thành Công" + Environment.NewLine);
                                break;
                            }
                        }
                    }
                    catch { }
                }
                if (duoimailkhoiphuccu == "@getnada.com")
                {
                    string inboxgetnadaso1 = http.Get("https://getnada.com/api/v1/inboxes/" + mailkhoiphuc).ToString();
                    Regex uid1 = new Regex("uid\":\"\\w+");
                    Match uid2 = uid1.Match(inboxgetnadaso1);
                    for (int u = 0; uid2 != Match.Empty; u++)
                    {
                        string uidgetnada = uid2.ToString().Replace("uid\":\"", "");
                        string laymagetnadaso1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnada).ToString();
                        Regex url1 = new Regex("https://account.live.com.+\\d");
                        Match url2 = url1.Match(laymagetnadaso1);
                        if (url2 != Match.Empty)
                        {
                            string urlxoa = url2.ToString();
                            chrome.Url = urlxoa;
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                            Thread.Sleep(5000);
                            dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Blue;
                            dataGridView1.Rows[i].Cells[5].Value = "Xóa Mail Khôi Phục Thành Công";
                            string accok = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            accok = accok.Replace(mailkhoiphuc, "");
                            Thread.Sleep(1000);
                            dataGridView1.Rows[i].Cells[2].Value = accok;
                            File.AppendAllText(Directory.GetCurrentDirectory() + "\\output\\fullacc.txt", accok + "=> Xóa Mail Khôi Phục Thành Công" + Environment.NewLine);
                            break;
                        }
                        uid2 = uid2.NextMatch();

                    }
                    //string inboxgetso1 = inboxgetnadaso1.Substring(0, inboxgetnadaso1.IndexOf("\",\"ib\":"));
                    //string uidgetnadaso1 = inboxgetso1.Substring(inboxgetso1.LastIndexOf("uid\":\"")).Replace("uid\":\"", "");
                    // string laymagetnadaso1 = http.Get("https://getnada.com/api/v1/messages/html/" + uidgetnadaso1).ToString();
                    //string urlxoamail = laymagetnadaso1.Substring(laymagetnadaso1.IndexOf("https://account.live.com/"));
                    //urlxoamail = urlxoamail.Substring(0, urlxoamail.IndexOf("\""));
                    //chrome.Url = urlxoamail;
                    //Thread.Sleep(1000);
                    //chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                    //Thread.Sleep(5000);
                    //dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Blue;
                    //dataGridView1.Rows[i].Cells[5].Value = "Xóa Mail Khôi Phục Thành Công";
                    //string fullacc = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    //fullacc = fullacc.Replace(mailkhoiphuc, "");
                    //Thread.Sleep(1000);
                    //dataGridView1.Rows[i].Cells[2].Value = fullacc;
                    //File.AppendAllText(Directory.GetCurrentDirectory() + "\\output\\fullacc.txt", fullacc + Environment.NewLine);

                    chrome.Quit();
                }
                if (duoimailkhoiphuccu == "@mailnesia.com")
                {
                    string emailoke = mailkhoiphuc.Replace("@mailnesia.com", "");
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
                        for (int l = 0; b2 != Match.Empty; l++)
                        {
                            idcanlay = b2.ToString().Replace("<tr id=\"", "");
                            client = new RestClient("http://mailnesia.com/mailbox/" + emailoke + "/" + idcanlay + "?noheadernofooter=ajax");
                            client.Timeout = -1;
                            request = new RestRequest(Method.GET);
                            request.AddHeader("Accept", "*/*");
                            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                            request.AddHeader("Connection", "keep-alive");
                            request.AddHeader("Cookie", "language=en; _ga=GA1.2.1507483214.1662135497; _gid=GA1.2.1212599142.1662135497; prefetchAd_4061430=true; mailbox=regcaimail; language=en; mailbox=regcaimail");
                            request.AddHeader("Referer", "http://mailnesia.com/mailbox/regcaimail");
                            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";
                            request.AddHeader("X-Requested-With", "XMLHttpRequest");
                            response = client.Execute(request);
                            string layotp = response.Content;
                            Regex url1 = new Regex("https://account.live.com.+\\d");
                            Match url2 = url1.Match(layotp);
                            if (url2 != Match.Empty)
                            {
                                string urlxoa = url2.ToString();
                                chrome.Url = urlxoa;
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                                Thread.Sleep(5000);
                                dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Blue;
                                dataGridView1.Rows[i].Cells[5].Value = "Xóa Mail Khôi Phục Thành Công";
                                string accok = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                accok = accok.Replace(mailkhoiphuc, "");
                                Thread.Sleep(1000);
                                dataGridView1.Rows[i].Cells[2].Value = accok;
                                File.AppendAllText(Directory.GetCurrentDirectory() + "\\output\\fullacc.txt", accok + "=> Xóa Mail Khôi Phục Thành Công" + Environment.NewLine);
                                break;
                            }
                            b2 = b2.NextMatch();
                        }

                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
                dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Red;
                dataGridView1.Rows[i].Cells[5].Value = "Xóa Mail Khôi Phục Thất Bại";
                chrome.Quit();

            }
            try
            {
                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                {
                    chromedocma.Url = "https://www.moakt.com/en/inbox/";
                    Thread.Sleep(1000);
                    chromedocma.FindElement(By.XPath("//a[@href='/en/inbox/logout']")).Click();
                    Thread.Sleep(1000);
                }

            }
            catch { }
            try
            {
                chrome.Quit();
                chromedocma.Quit();
            }
            catch
            {

            }
            WriteLog();
        }
        private void mkpcosan(int so_luong_dang_chay, int dong, string email, string passmail, string mailkhoiphuc, string mailkhoiphucmoi, int pro5, string urlfile, string duoigetnada)
        {

            string duoimailkhoiphucmoi = "";
            string duoimailkhoiphuccu = "";
            
            try
            {
                duoimailkhoiphucmoi = mailkhoiphucmoi.Substring(mailkhoiphucmoi.IndexOf("@"));
            }
            catch { }
            try
            {
                duoimailkhoiphuccu = mailkhoiphuc.Substring(mailkhoiphuc.IndexOf("@"));
            }
            catch { }
            ChromeDriver chromedocma = null;
            if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
            {
                ChromeDriverService server = ChromeDriverService.CreateDefaultService();
                server.HideCommandPromptWindow = true;
                ChromeOptions options1 = new ChromeOptions();
                options1.AddArguments("headless");
                options1.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                options1.AddArgument("--no-sandbox");
                options1.AddArgument("--disable-web-security");
                options1.AddArgument("--disable-blink-features=AutomationControlled");
                options1.AddArgument("-disable-site-isolation-trials");
                options1.AddArgument("-disable-application-cache");
                options1.AddExcludedArgument("enable-automation");
                options1.AddArgument("--start-maximized");
                options1.AddUserProfilePreference("credentials_enable_service", false);
                options1.AddUserProfilePreference("profile.password_manager_enabled", false);
                options1.AddArguments("--disable-notifications", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage", "--disable-web-security", "--disable-rtc-smoothness-algorithm", "--disable-webrtc-hw-decoding", "--disable-webrtc-hw-encoding", "--disable-webrtc-multiple-routes", "--disable-webrtc-hw-vp8-encoding", "--enforce-webrtc-ip-permission-check", "--force-webrtc-ip-handling-policy", "--ignore-certificate-errors", "--disable-infobars", "--mute-audio", "--disable-popup-blocking");
                options1.AddUserProfilePreference("profile.default_content_setting_values.notifications", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.popups", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.auto_select_certificate", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.mixed_script", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream_mic", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.protocol_handlers", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.midi_sysex", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.push_messaging", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.ssl_cert_decisions", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.metro_switch_to_desktop", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.protected_media_identifier", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.site_engagement", 1);
                options1.AddUserProfilePreference("profile.default_content_setting_values.durable_storage", 1);
                options1.AddUserProfilePreference("profile.managed_default_content_settings.images", 1);
                options1.AddUserProfilePreference("useAutomationExtension", true);

                chromedocma = new ChromeDriver(server, options1);
            }
            string keyctsc = textBoxkeysimthue.Text;
            bool checkkboxxoathu = checkBoxxoathu.Checked;
            bool cbdoipass = checkBoxdoipass.Checked;
            bool cbthemmail = checkBoxaddmail.Checked;
            bool dang1 = checkBoxdang1.Checked;
            bool dang2 = checkBoxdang2.Checked;
            bool dang3 = checkBoxdang3.Checked;
            bool dang4 = checkboxdang4.Checked;
            bool dang5 = checkBoxmailzin.Checked;
            bool imap = checkBoximap.Checked;
            string doipass = textBoxdoipass.Text;
            string thanhcong = "";
            string baoloi = "Lỗi khi thao tác";
            Random rd = new Random();
            Random _r = new Random();
            string inhoa = Convert.ToString((char)rd.Next(65, 90));
            string inthuong = Convert.ToString((char)rd.Next(97, 122));
            int n = _r.Next(111, 99999);
            string inhoa1 = Convert.ToString((char)rd.Next(65, 90));
            string inthuong1 = Convert.ToString((char)rd.Next(97, 122));
            string inhoa2 = Convert.ToString((char)rd.Next(65, 90));
            string inthuong2 = Convert.ToString((char)rd.Next(97, 122));
            string inhoa3 = Convert.ToString((char)rd.Next(65, 90));
            string inthuong3 = Convert.ToString((char)rd.Next(97, 122));
            string mkrandom = inhoa + inhoa2 + inhoa3 + inthuong + inthuong1 + inthuong3 + n;
            string mkmoitudat = textBoxdoipass.Text;
            string passmoi = "";
            string fullacc = "";
            string uidgetnada = "";
            string otpgetnada = "";
            ChromeDriver chrome = null;
            try
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddArguments("--disable-notifications", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage", "--disable-web-security", "--disable-rtc-smoothness-algorithm", "--disable-webrtc-hw-decoding", "--disable-webrtc-hw-encoding", "--disable-webrtc-multiple-routes", "--disable-webrtc-hw-vp8-encoding", "--enforce-webrtc-ip-permission-check", "--force-webrtc-ip-handling-policy", "--ignore-certificate-errors", "--disable-infobars", "--mute-audio", "--disable-popup-blocking");
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.popups", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.auto_select_certificate", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.mixed_script", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_mic", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.protocol_handlers", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.midi_sysex", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.push_messaging", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.ssl_cert_decisions", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.metro_switch_to_desktop", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.protected_media_identifier", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.site_engagement", 1);
                options.AddUserProfilePreference("profile.default_content_setting_values.durable_storage", 1);
                options.AddUserProfilePreference("profile.managed_default_content_settings.images", 1);
                options.AddUserProfilePreference("useAutomationExtension", true);

                options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                options.AddArgument("--disable-gpu");
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--start-maximized");

                options.AddArgument("--disable-web-security");
                options.AddArgument("--disable-blink-features=AutomationControlled");

                options.AddArgument("-disable-site-isolation-trials");
                options.AddArgument("-disable-application-cache");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                //chromeOptions.AddArgument("--blink-settings=imagesEnabled=false");

                // fake useragent
                //chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1");
                //if (!File.Exists(Directory.GetCurrentDirectory() + "\\debug_d.txt"))
                //chromeOptions.AddArgument("--app=https://www.hotmail.com");

                // set position, custome here
                #region calc position for profile
                {
                    // calc size
                    int max_width = Screen.PrimaryScreen.Bounds.Width;
                    int max_height = Screen.PrimaryScreen.Bounds.Height;
                    int width = ConfigInfo.chromeWidth;
                    int height = ConfigInfo.chromeHeight;
                    options.AddArgument($"--window-size={width},{height}");

                    // calc max position for pro5
                    int distance_x = ConfigInfo.chromeDistanceX;
                    int distance_y = ConfigInfo.chromeDistanceY;
                    int max_column = (max_width - width) / distance_x + 1;
                    int max_row = (max_height - height) / distance_y + 1;

                    // calc position (pro5 % max_column == 0) ? (pro5 / max_column) % max_row : 
                    int row = (pro5 % max_column == 0) ? (((pro5 / max_column) % max_row == 0) ? (pro5 / max_column) % max_row + 1 : (pro5 / max_column) % max_row) : (pro5 / max_column) % max_row + 1;
                    int column = (pro5 % max_column == 0) ? max_column : pro5 % max_column;

                    int margin_width_postion = (column - 1) * distance_x;
                    int margin_height_position = (row - 1) * distance_y;

                    string position = $"--window-position={margin_width_postion},{margin_height_position}";
                    options.AddArgument(position);
                }
                #endregion

                chrome = new ChromeDriver(chromeDriverService, options);
                bool checkdang1;
                bool checkdang2;
                bool checkdang3;
                bool checkdang4;
                bool checklgok = false;
                bool checpasss = false;
                bool dinhcapcha = false;
                // login hotmail
                dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Login Hotmail";
                if (chucnang.Login_hotmail(chrome, email, passmail) == true)
                {
                    try
                    {
                        chrome.FindElement(By.XPath("//span[@id='iProofLbl0']")).Click();
                    }
                    catch { }
                    try
                    {
                        IWebElement checklg = chrome.FindElement(By.XPath("//input[@id='idSIButton9']")); // login thẳng
                        checklgok = checklg.Displayed;
                    }
                    catch { }
                    try
                    {
                        IWebElement checksaipass = chrome.FindElement(By.XPath("//a[@id='idA_IL_ForgotPassword0']")); // sai pass
                        checpasss = checksaipass.Displayed;
                    }
                    catch
                    {

                    }
                    try
                    {
                        IWebElement checkkcapcha = chrome.FindElement(By.XPath("//a[@id='idA_PWD_ForgotPassword']")); // dính capcha
                        dinhcapcha = checkkcapcha.Displayed;
                    }
                    catch
                    {

                    }
                    //string checklogin = chrome.Url;
                    //Regex checkok = new Regex("login.live.com/ppsecure");
                    //Match checklg = checkok.Match(checklogin);
                    if (checklgok == true)
                    {
                        if (checpasss == true)
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "Sai Pass Hotmail";
                            chrome.Quit();
                            return;

                        }
                        if (dinhcapcha == true)
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "Login Dính Capcha";
                            chrome.Quit();
                            return;
                        }
                        dataGridView1.Rows[dong].Cells[5].Value = "Login Thành Công";
                    }
                    else
                    {
                        dataGridView1.Rows[dong].Cells[5].Value = "Hotmail CP";
                    }
                }
                else
                {
                    dataGridView1.Rows[dong].Cells[5].Value = "Login hotmail thất bại";
                    chrome.Quit();

                }
                Thread.Sleep(2000);
                // login không cp 
                HttpRequest http = new HttpRequest();
                bool checkloai1 = false;
                bool checkloai2 = false;
                bool checkloai4 = false;
                dataGridView1.Rows[dong].Cells[4].Value = " ";
                string thanhcongok = "";

                try
                {
                    IWebElement check1 = chrome.FindElement(By.XPath("//input[@name='EmailAddress']")); // login thẳng
                    checkloai1 = check1.Displayed;
                }
                catch
                {

                }
                try
                {
                    IWebElement check2 = chrome.FindElement(By.XPath("//input[@id='idSIButton9']")); // login thẳng
                    checkloai2 = check2.Displayed;
                }
                catch
                {
                }
                try
                {
                    IWebElement check4 = chrome.FindElement(By.XPath("//input[@id='StartAction']")); // cp sđt có mail hoặc ko có mail
                    checkloai4 = check4.Displayed;
                }
                catch
                {

                }

                try
                {
                    if (checkloai1 == true || checkloai2 == true || checkloai4 == true)
                    {
                        try
                        {
                            if (checkloai4 == true)
                            {
                                // xác minh sđt
                                chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                                Thread.Sleep(1000);
                                dataGridView1.Rows[dong].Cells[5].Value = "CP SĐT => Bắt Đầu Xác Minh";
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                Thread.Sleep(1000);
                                if (radioButtonctsc.Checked)
                                {
                                    for (int p = 0; p < 5; p++)
                                    {
                                        string uidsimthue = chucnang.layuidctsc(chrome, keyctsc);
                                        if (uidsimthue != "")
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Lấy SĐT Thành Công => Bắt Đầu Lấy Mã OTP";
                                            Thread.Sleep(12000);
                                            if (chucnang.xmsdtsimthue(chrome, keyctsc, uidsimthue) == true)
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thành Công";
                                                Thread.Sleep(2000);
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "Xác Minh SĐT OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                break;
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thất Bại => Chờ Xác Minh Lại Số Mới";

                                            }

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Không Lấy ĐƯợc SĐT => Kiểm Tra Lại API KEY hoặc đã hết tiền trong Tài Khoản sim";
                                            dataGridView1.Rows[dong].Cells[4].Value = "Xác Minh SĐT Không Thành Công";
                                            chrome.Quit();
                                        }
                                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//a[@role=\"button\"]"));
                                        Thread.Sleep(2000);
                                        ele[1].Click();
                                        try
                                        {
                                            chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                                        }
                                        catch
                                        {
                                            chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                                        }
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                        Thread.Sleep(1000);
                                    }

                                }

                                if (radioButtonviotp.Checked)
                                {
                                    for (int p = 0; p < 5; p++)
                                    {
                                        string requet_id = chucnang.layuidviotp(chrome, keyctsc);
                                        if (requet_id != "")
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Lấy SĐT Thành Công => Bắt Đầu Lấy Mã OTP";
                                            Thread.Sleep(12000);
                                            if (chucnang.xmsdtviotp(chrome, keyctsc, requet_id) == true)
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thành Công";
                                                Thread.Sleep(2000);
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "Xác Minh SĐT OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                break;
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thất Bại => Chờ Xác Minh Lại Số Mới";

                                            }

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Không Lấy ĐƯợc SĐT => Kiểm Tra Lại API KEY hoặc đã hết tiền trong Tài Khoản sim";
                                            chrome.Quit();
                                        }
                                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//a[@role=\"button\"]"));
                                        Thread.Sleep(2000);
                                        ele[1].Click();
                                        try
                                        {
                                            chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                                        }
                                        catch
                                        {
                                            chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                                        }
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                        Thread.Sleep(1000);
                                    }

                                }
                            }
                        }
                        catch { }

                        // login thành công ko có mail khôi phục
                        // thêm mail khôi phục 
                        try
                        {
                            try
                            {
                                if (mailkhoiphuc == null) // thêm mail khôi phục mới chưa có mail khôi phục cũ 
                                {
                                    if (checkBoxaddmail.Checked && checkBoxdoipass.Checked)
                                    {
                                        bool checkthemail = false;
                                        chucnang.xmmailkhoiphuc = false;
                                        try
                                        {
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                            {
                                                uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                            }
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Thêm Mail Khôi Phục";
                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                            Thread.Sleep(10000);
                                            if (duoimailkhoiphucmoi == "@getnada.com")
                                            {
                                                uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                            }
                                            if (duoimailkhoiphucmoi == "@mailnesia.com")
                                            {
                                                uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                            }

                                            if (uidgetnada != "")
                                            {
                                                if (duoimailkhoiphucmoi == "@getnada.com")
                                                {
                                                    otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                                }
                                                if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                {
                                                    otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                                }
                                                if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                {
                                                    uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                }
                                                if (otpgetnada != "")
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpgetnada);
                                                    Thread.Sleep(1000);

                                                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục thành công => update vào dòng tài khoản";
                                                    Thread.Sleep(1000);
                                                    fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                    fullacc = fullacc + "|" + mailkhoiphucmoi;
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                    Thread.Sleep(1000);
                                                    thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                    thanhcong = thanhcong + "|" + "Thêm Mail OK";
                                                    dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                    checkthemail = true;
                                                }
                                                else
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => không có mã OTP";
                                                }
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => Không Có Thư";
                                            }
                                        }
                                        catch { }
                                        try
                                        {
                                            if (checkthemail == true)
                                            {
                                                chrome.Url = "https://account.live.com/password/Change";
                                                Thread.Sleep(1000);
                                                if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                {
                                                    uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                }
                                                chrome.FindElement(By.XPath("//img[@src='https://logincdn.msauth.net/shared/1.0/content/images/picker_verify_email_59759b80e24a89c8cd029b14700e646d.svg']")).Click();
                                                Thread.Sleep(1000);
                                                dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Xác Minh Mail Khôi Phục Để Đổi Pass";
                                                chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCS_ProofConfirmation']")).SendKeys(mailkhoiphucmoi);
                                                Thread.Sleep(1000);
                                                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                                                Thread.Sleep(12000);
                                                if (duoimailkhoiphucmoi == "@getnada.com")
                                                {
                                                    uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                                }
                                                if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                {
                                                    uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                                }

                                                if (uidgetnada != "")
                                                {
                                                    if (duoimailkhoiphucmoi == "@getnada.com")
                                                    {
                                                        otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                                    }
                                                    if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                    {
                                                        otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                                    }
                                                    if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                    {
                                                        uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                    }
                                                    if (otpgetnada != "")
                                                    {
                                                        dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                                        Thread.Sleep(1000);
                                                        chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpgetnada);
                                                        Thread.Sleep(1000);
                                                        chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                                                        Thread.Sleep(1000);
                                                        dataGridView1.Rows[dong].Cells[5].Value = "XM Mail Thành Công  => Bắt Đầu Đổi Pass";
                                                        chucnang.xmmailkhoiphuc = true;
                                                    }

                                                }
                                            }
                                            if (chucnang.xmmailkhoiphuc == true)
                                            {
                                                if (checkBoxdoipass.Checked)
                                                {
                                                    chrome.Url = "https://account.live.com/password/Change";
                                                    Thread.Sleep(1000);
                                                    if (textBoxdoipass.Text == "")
                                                    {
                                                        // pass random
                                                        passmoi = chucnang.doipass(chrome, passmail, mkrandom);
                                                        if (passmoi != "")
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                                            Thread.Sleep(1000);
                                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                            Thread.Sleep(1000);
                                                            fullacc = fullacc.Replace(passmail, passmoi);
                                                            Thread.Sleep(1000);
                                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                            Thread.Sleep(1000);
                                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;

                                                        }
                                                        else
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        passmoi = chucnang.doipass(chrome, passmail, mkmoitudat);
                                                        if (passmoi != "")
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                                            Thread.Sleep(1000);
                                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                            fullacc = fullacc.Replace(passmail, passmoi);
                                                            Thread.Sleep(1000);
                                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                            Thread.Sleep(1000);
                                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;

                                                        }
                                                        else
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "XM mail Thất Bại Không Thể Đổi Pass";
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (checkBoxaddmail.Checked && !checkBoxdoipass.Checked)
                                    {
                                        try
                                        {
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            chrome.Url = ("https://account.live.com/proofs/Add");
                                            Thread.Sleep(1000);
                                            if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                            {
                                                uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                            }
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Thêm Mail Khôi Phục";
                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                            Thread.Sleep(12000);
                                            if (duoimailkhoiphucmoi == "@getnada.com")
                                            {
                                                uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                            }
                                            if (duoimailkhoiphucmoi == "@mailnesia.com")
                                            {
                                                uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                            }

                                            if (uidgetnada != "")
                                            {
                                                if (duoimailkhoiphucmoi == "@getnada.com")
                                                {
                                                    otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                                }
                                                if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                {
                                                    otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                                }
                                                if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                {
                                                    uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                }
                                                if (otpgetnada != "")
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpgetnada);
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục thành công => update vào dòng tài khoản";
                                                    Thread.Sleep(1000);
                                                    fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                    fullacc = fullacc + "|" + mailkhoiphucmoi;
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                    Thread.Sleep(1000);
                                                    thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                    thanhcong = thanhcong + "|" + "Thêm Mail OK";
                                                    dataGridView1.Rows[dong].Cells[4].Value = thanhcong;

                                                }
                                                else
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => không có mã OTP";

                                                }
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => Không Có Thư";
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (!checkBoxaddmail.Checked && checkBoxdoipass.Checked)
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Không Có Mail Khôi Phục Không Thể Đổi Pass";
                                    }
                                }
                            }
                            catch { }
                            try
                            {
                                if (mailkhoiphuc != null)
                                {
                                    chucnang.xmmailkhoiphuc = false;
                                    bool themmailmoi = false;
                                    if (checkBoxaddmail.Checked || checkBoxdoipass.Checked)
                                    {
                                        try
                                        {
                                            chrome.Url = "https://account.live.com/password/Change";
                                            Thread.Sleep(1000);
                                            if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                            {
                                                uidgetnada = chucnang.themmoaktcc(mailkhoiphuc, chromedocma);
                                            }
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//img[@src='https://logincdn.msauth.net/shared/1.0/content/images/picker_verify_email_59759b80e24a89c8cd029b14700e646d.svg']")).Click();
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Mở CP mail";
                                            chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                                            Thread.Sleep(12000);
                                            if (duoimailkhoiphuccu == "@getnada.com")
                                            {
                                                uidgetnada = chucnang.layuidgetnada(http, mailkhoiphuc);
                                            }
                                            if (duoimailkhoiphuccu == "@mailnesia.com")
                                            {
                                                uidgetnada = chucnang.layuidmailnesia(mailkhoiphuc);
                                            }

                                            if (uidgetnada != "")
                                            {
                                                if (duoimailkhoiphuccu == "@getnada.com")
                                                {
                                                    otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                                }
                                                if (duoimailkhoiphuccu == "@mailnesia.com")
                                                {
                                                    otpgetnada = chucnang.otpmailnesia(mailkhoiphuc, uidgetnada);
                                                }
                                                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                                {
                                                    otpgetnada = chucnang.getmamoakt(chromedocma);
                                                }
                                                if (otpgetnada != "")
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpgetnada);
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục Thành Công";
                                                    chucnang.xmmailkhoiphuc = true;
                                                }

                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Không Có Thư";
                                                chrome.Quit();
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        try
                                        {
                                            // thêm mail khôi phục mới
                                            if (chucnang.xmmailkhoiphuc == true)
                                            {
                                                if (checkBoxaddmail.Checked)
                                                {
                                                    Thread.Sleep(1000);
                                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                                    Thread.Sleep(1000);
                                                    if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                    {
                                                        uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                    }
                                                    Thread.Sleep(1000);
                                                    chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                                    Thread.Sleep(1000);
                                                    dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Thêm Mail Khôi Phục";
                                                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                                    Thread.Sleep(12000);
                                                    if (duoimailkhoiphucmoi == "@getnada.com")
                                                    {
                                                        uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                                    }
                                                    if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                    {
                                                        uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                                    }
                                                    if (uidgetnada != "")
                                                    {
                                                        if (duoimailkhoiphucmoi == "@getnada.com")
                                                        {
                                                            otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                                        }
                                                        if (duoimailkhoiphucmoi == "@mailnesia.com")
                                                        {
                                                            otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                                        }
                                                        if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                                        {
                                                            uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                                        }
                                                        if (otpgetnada != "")
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                                            Thread.Sleep(1000);
                                                            chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpgetnada);
                                                            Thread.Sleep(1000);
                                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                                            Thread.Sleep(1000);
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục thành công => update vào dòng tài khoản";
                                                            Thread.Sleep(1000);
                                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                            fullacc = fullacc + "|" + mailkhoiphucmoi;
                                                            Thread.Sleep(1000);
                                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                            thanhcong = thanhcong + "|" + "Thêm Mail OK";
                                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                            themmailmoi = true;
                                                        }
                                                        else
                                                        {
                                                            dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => không có mã OTP";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục Thất Bại => Không Có Thư";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục cũ Thất bại => không thể thêm mail và đổi pass";
                                            }
                                            // xm maiil doi pass
                                            try
                                            {
                                                if (chucnang.xmmailkhoiphuc == true)
                                                {
                                                    if (checkBoxdoipass.Checked)
                                                    {
                                                        if (textBoxdoipass.Text == "")
                                                        {
                                                            // pass random
                                                            passmoi = chucnang.doipass(chrome, passmail, mkrandom);
                                                            if (passmoi != "")
                                                            {
                                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                                                Thread.Sleep(1000);
                                                                fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                                fullacc = fullacc.Replace(passmail, passmoi);
                                                                Thread.Sleep(1000);
                                                                dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                                Thread.Sleep(1000);
                                                                Thread.Sleep(1000);
                                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                                thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;

                                                            }
                                                            else
                                                            {
                                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";

                                                            }
                                                        }
                                                        else
                                                        {
                                                            passmoi = chucnang.doipass(chrome, passmail, mkmoitudat);
                                                            if (passmoi != "")
                                                            {
                                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                                                fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                                Thread.Sleep(1000);
                                                                fullacc = fullacc.Replace(passmail, passmoi);
                                                                Thread.Sleep(1000);
                                                                dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                                Thread.Sleep(1000);
                                                                Thread.Sleep(1000);
                                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                                thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;

                                                            }
                                                            else
                                                            {
                                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    dataGridView1.Rows[dong].Cells[5].Value = "XM mail Khôi Phục Thất bại -> không thể đổi pass";
                                                }

                                            }
                                            catch
                                            {
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }

                                }
                            }
                            catch { }

                        }
                        catch { }

                    }
                }
                catch
                {
                }
                try // login cp 
                {
                    bool checkloai3 = false;
                    bool checkloai5 = false;
                    try
                    {
                        IWebElement check3 = chrome.FindElement(By.XPath("//input[@id='iProofEmail']")); // cp mail 
                        checkloai3 = check3.Displayed;
                    }
                    catch { }
                    try
                    {
                        IWebElement check5 = chrome.FindElement(By.XPath("//input[@id='iLandingViewAction']")); // cp mail 
                        checkloai5 = check5.Displayed;
                    }
                    catch
                    {

                    }
                    try
                    {
                        if (mailkhoiphuc != "" && (checkloai5 == true || checkloai3 == true))// cp co mail khôi phục đổi pass luôn
                        {
                            chucnang.xmmailkhoiphuc = false;
                            bool checkdoipass = false;
                            bool checkthemmail = false;
                            chrome.Url = "https://account.live.com/password/Change";
                            Thread.Sleep(1000);
                            if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                            {
                                uidgetnada = chucnang.themmoaktcc(mailkhoiphuc, chromedocma);
                            }
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//img[@src='https://logincdn.msauth.net/shared/1.0/content/images/picker_verify_email_59759b80e24a89c8cd029b14700e646d.svg']")).Click();
                            Thread.Sleep(1000);
                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Mở CP mail";
                            chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                            Thread.Sleep(12000);
                            if (duoimailkhoiphuccu == "@getnada.com")
                            {
                                uidgetnada = chucnang.layuidgetnada(http, mailkhoiphuc);
                            }
                            if (duoimailkhoiphuccu == "@mailnesia.com")
                            {
                                uidgetnada = chucnang.layuidmailnesia(mailkhoiphuc);
                            }

                            if (uidgetnada != "")
                            {
                                if (duoimailkhoiphuccu == "@getnada.com")
                                {
                                    otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                }
                                if (duoimailkhoiphuccu == "@mailnesia.com")
                                {
                                    otpgetnada = chucnang.otpmailnesia(mailkhoiphuc, uidgetnada);
                                }
                                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                {
                                    otpgetnada = chucnang.getmamoakt(chromedocma);
                                    Thread.Sleep(1000);
                                }
                                if (otpgetnada != "")
                                {
                                    dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpgetnada);
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                                    Thread.Sleep(1000);
                                    try
                                    {
                                        chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                    }
                                    catch
                                    {
                                    }
                                    dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục Thành Công";
                                    chucnang.xmmailkhoiphuc = true;
                                }

                            }
                            try
                            {
                                if (checkloai5 == true && chucnang.xmmailkhoiphuc == true)
                                {
                                    chrome.FindElement(By.XPath("//input[@id='iLandingViewAction']")).Click();
                                    try
                                    {
                                        // đổi pass luôn 
                                        if (checkBoxdoipass.Checked)
                                        {

                                            if (textBoxdoipass.Text == "")
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Bắt đầu đổi pass";
                                                chrome.FindElement(By.XPath("//input[@name='Password']")).SendKeys(mkrandom);
                                                Thread.Sleep(1000);
                                                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                                                Thread.Sleep(1000);
                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công";
                                                fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                fullacc = fullacc.Replace(passmail, mkrandom);
                                                Thread.Sleep(1000);
                                                dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                checkdoipass = true;


                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells[5].Value = "Bắt đầu đổi pass";
                                                chrome.FindElement(By.XPath("//input[@name='Password']")).SendKeys(mkmoitudat);
                                                Thread.Sleep(1000);
                                                chrome.FindElement(By.XPath("//input[@id='iPasswordViewAction']")).Click();
                                                Thread.Sleep(1000);
                                                dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công";
                                                fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                                Thread.Sleep(1000);
                                                fullacc = fullacc.Replace(passmail, mkmoitudat);
                                                Thread.Sleep(1000);
                                                dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                                Thread.Sleep(1000);
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                checkdoipass = true;
                                            }

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Yêu Cầu Đổi Pass Luôn => Bạn Không chọn chức năng đổi pass";

                                        }
                                    }
                                    catch
                                    { }
                                }
                            }
                            catch { }
                            // thêm mail khôi phục 
                            try
                            {
                                if (checkBoxaddmail.Checked && chucnang.xmmailkhoiphuc == true)
                                {
                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                    Thread.Sleep(1000);
                                    if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                    {
                                        uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                    }
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                    Thread.Sleep(1000);
                                    dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Thêm Mail Khôi Phục";
                                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                    Thread.Sleep(12000);
                                    if (duoimailkhoiphucmoi == "@getnada.com")
                                    {
                                        uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                    }
                                    if (duoimailkhoiphucmoi == "@mailnesia.com")
                                    {
                                        uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                    }

                                    if (uidgetnada != "")
                                    {
                                        if (duoimailkhoiphucmoi == "@getnada.com")
                                        {
                                            otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                        }
                                        if (duoimailkhoiphucmoi == "@mailnesia.com")
                                        {
                                            otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                        }
                                        if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                        {
                                            uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                        }
                                        if (otpgetnada != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpgetnada);
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục thành công => update vào dòng tài khoản";
                                            Thread.Sleep(1000);
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            fullacc = fullacc + "|" + mailkhoiphucmoi;
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Thêm mail OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkthemmail = true;
                                        }

                                    }
                                }
                            }
                            catch { }
                            if (chucnang.xmmailkhoiphuc == true && checkloai3 == true)
                            {
                                // đổi pass 

                                if (checkBoxdoipass.Checked)
                                {
                                    chrome.Url = "https://account.live.com/password/Change";
                                    Thread.Sleep(1000);

                                    if (textBoxdoipass.Text == "")
                                    {
                                        // pass random
                                        passmoi = chucnang.doipass(chrome, passmail, mkrandom);
                                        if (passmoi != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            Thread.Sleep(1000);
                                            fullacc = fullacc.Replace(passmail, passmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            Thread.Sleep(1000);
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkdoipass = true;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                        }
                                    }
                                    else
                                    {
                                        passmoi = chucnang.doipass(chrome, passmail, mkmoitudat);
                                        if (passmoi != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            Thread.Sleep(1000);
                                            fullacc = fullacc.Replace(passmail, passmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            Thread.Sleep(1000);
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkdoipass = true;

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            // xm mail khoi phuc
                            chucnang.xmmailkhoiphuc = false;
                            bool checkdoipass = false;
                            bool checkthemmail = false;
                            chrome.Url = "https://account.live.com/password/Change";
                            Thread.Sleep(1000);
                            if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                            {
                                uidgetnada = chucnang.themmoaktcc(mailkhoiphuc, chromedocma);
                            }
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//img[@src='https://logincdn.msauth.net/shared/1.0/content/images/picker_verify_email_59759b80e24a89c8cd029b14700e646d.svg']")).Click();
                            Thread.Sleep(1000);
                            dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Mở CP mail";
                            chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                            Thread.Sleep(1000);
                            chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                            Thread.Sleep(12000);
                            if (duoimailkhoiphuccu == "@getnada.com")
                            {
                                uidgetnada = chucnang.layuidgetnada(http, mailkhoiphuc);
                            }
                            if (duoimailkhoiphuccu == "@mailnesia.com")
                            {
                                uidgetnada = chucnang.layuidmailnesia(mailkhoiphuc);
                            }

                            if (uidgetnada != "")
                            {
                                if (duoimailkhoiphuccu == "@getnada.com")
                                {
                                    otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                }
                                if (duoimailkhoiphuccu == "@mailnesia.com")
                                {
                                    otpgetnada = chucnang.otpmailnesia(mailkhoiphuc, uidgetnada);
                                }
                                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                {
                                    otpgetnada = chucnang.getmamoakt(chromedocma);
                                }
                                if (otpgetnada != "")
                                {
                                    dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpgetnada);
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                                    Thread.Sleep(1000);
                                    try
                                    {
                                        chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                    }
                                    catch
                                    {
                                    }
                                    dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục Thành Công";
                                    chucnang.xmmailkhoiphuc = true;
                                }

                            }
                            //
                            if (chucnang.xmmailkhoiphuc == true)
                            {
                                if (checkBoxaddmail.Checked)
                                {
                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                    chrome.Url = ("https://account.live.com/proofs/Add");
                                    Thread.Sleep(1000);
                                    if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                    {
                                        uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                    }
                                    Thread.Sleep(1000);
                                    chrome.FindElement(By.XPath("//input[@name='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                    Thread.Sleep(1000);
                                    dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Thêm Mail Khôi Phục";
                                    chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                    Thread.Sleep(12000);
                                    if (duoimailkhoiphucmoi == "@getnada.com")
                                    {
                                        uidgetnada = chucnang.layuidgetnada(http, mailkhoiphucmoi);
                                    }
                                    if (duoimailkhoiphucmoi == "@mailnesia.com")
                                    {
                                        uidgetnada = chucnang.layuidmailnesia(mailkhoiphucmoi);
                                    }

                                    if (uidgetnada != "")
                                    {
                                        if (duoimailkhoiphucmoi == "@getnada.com")
                                        {
                                            otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                        }
                                        if (duoimailkhoiphucmoi == "@mailnesia.com")
                                        {
                                            otpgetnada = chucnang.otpmailnesia(mailkhoiphucmoi, uidgetnada);
                                        }
                                        if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                                        {
                                            uidgetnada = chucnang.themmoaktcc(mailkhoiphucmoi, chromedocma);
                                        }
                                        if (otpgetnada != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@id='iOttText']")).SendKeys(otpgetnada);
                                            Thread.Sleep(1000);
                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[5].Value = "Thêm Mail Khôi Phục thành công => update vào dòng tài khoản";
                                            Thread.Sleep(1000);
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            fullacc = fullacc + "|" + mailkhoiphucmoi;
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Thêm mail OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkthemmail = true;
                                        }

                                    }
                                }
                                if (checkBoxdoipass.Checked)
                                {
                                    chrome.Url = "https://account.live.com/password/Change";
                                    Thread.Sleep(1000);

                                    if (textBoxdoipass.Text == "")
                                    {
                                        // pass random
                                        passmoi = chucnang.doipass(chrome, passmail, mkrandom);
                                        if (passmoi != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            Thread.Sleep(1000);
                                            fullacc = fullacc.Replace(passmail, passmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            Thread.Sleep(1000);
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkdoipass = true;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                        }
                                    }
                                    else
                                    {
                                        passmoi = chucnang.doipass(chrome, passmail, mkmoitudat);
                                        if (passmoi != "")
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                            Thread.Sleep(1000);
                                            fullacc = fullacc.Replace(passmail, passmoi);
                                            Thread.Sleep(1000);
                                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                            Thread.Sleep(1000);
                                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                            thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                            checkdoipass = true;

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                        }
                                    }
                                }
                            }
                        }

                        if (mailkhoiphuc == "" && dang5 == true)
                        {
                            try
                            {
                                chrome.FindElement(By.XPath("//input[@id='iLandingViewAction']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                Thread.Sleep(1000);
                                // lấy sđt
                                if (radioButtonctsc.Checked)
                                {
                                    for (int p = 0; p < 5; p++)
                                    {
                                        string uidsimthue = chucnang.layuidctsc(chrome, keyctsc);
                                        if (uidsimthue != "")
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Lấy SĐT Thành Công => Bắt Đầu Lấy Mã OTP";
                                            Thread.Sleep(12000);
                                            if (chucnang.xmsdtsimthue(chrome, keyctsc, uidsimthue) == true)
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thành Công";
                                                Thread.Sleep(2000);
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "Xác Minh SĐT OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                break;
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thất Bại => Chờ Xác Minh Lại Số Mới";

                                            }

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Không Lấy ĐƯợc SĐT => Kiểm Tra Lại API KEY hoặc đã hết tiền trong Tài Khoản sim";
                                            dataGridView1.Rows[dong].Cells[4].Value = "Xác Minh SĐT Không Thành Công";
                                            chrome.Quit();
                                        }
                                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//a[@role=\"button\"]"));
                                        Thread.Sleep(2000);
                                        ele[1].Click();
                                        try
                                        {
                                            chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                                        }
                                        catch
                                        {
                                            chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                                        }
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                        Thread.Sleep(1000);
                                    }

                                }

                                if (radioButtonviotp.Checked)
                                {
                                    for (int p = 0; p < 5; p++)
                                    {
                                        string requet_id = chucnang.layuidviotp(chrome, keyctsc);
                                        if (requet_id != "")
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Lấy SĐT Thành Công => Bắt Đầu Lấy Mã OTP";
                                            Thread.Sleep(12000);
                                            if (chucnang.xmsdtviotp(chrome, keyctsc, requet_id) == true)
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thành Công";
                                                Thread.Sleep(2000);
                                                thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                                thanhcong = thanhcong + "Xác Minh SĐT OK";
                                                dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                                break;
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dong].Cells["status"].Value = "Xác Minh SĐT Thất Bại => Chờ Xác Minh Lại Số Mới";

                                            }

                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells["status"].Value = "Không Lấy ĐƯợc SĐT => Kiểm Tra Lại API KEY hoặc đã hết tiền trong Tài Khoản sim";
                                            chrome.Quit();
                                        }
                                        IList<IWebElement> ele = chrome.FindElements(By.XPath("//a[@role=\"button\"]"));
                                        Thread.Sleep(2000);
                                        ele[1].Click();
                                        try
                                        {
                                            chrome.FindElement(By.XPath("//input[@id=\"DisplayPhoneNumber\"]")).Clear();
                                        }
                                        catch
                                        {
                                            chrome.FindElement(By.XPath("//input[@type='text']")).Clear();
                                        }
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//select[@aria-label='Country code']")).Click();
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//option[@countryphonecode='+84']")).Click();
                                        Thread.Sleep(1000);
                                    }

                                }
                                // đổi pass luôn bắt buộc 
                                bool checkdoipass = false;
                                if (textBoxdoipass.Text == "")
                                {
                                    // pass random
                                    passmoi = chucnang.doipassdang5(chrome, passmail, mkrandom);

                                    if (passmoi != "")
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                        fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                        Thread.Sleep(1000);
                                        fullacc = fullacc.Replace(passmail, passmoi);
                                        Thread.Sleep(1000);
                                        dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                        Thread.Sleep(1000);
                                        thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                        thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                        dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                        checkdoipass = true;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                    }
                                }
                                else
                                {
                                    passmoi = chucnang.doipassdang5(chrome, passmail, mkmoitudat);
                                    if (passmoi != "")
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thành Công => update vào dòng tài khoản";
                                        fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                                        Thread.Sleep(1000);
                                        fullacc = fullacc.Replace(passmail, passmoi);
                                        Thread.Sleep(1000);
                                        dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                                        Thread.Sleep(1000);
                                        thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                                        thanhcong = thanhcong + "|" + "Đổi Pass OK";
                                        dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                                        checkdoipass = true;

                                    }
                                    else
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Đổi Pass Thất Bại";
                                    }
                                }

                                // thêm mail khôi phục luôn
                                chrome.FindElement(By.XPath("//input[@id='iReviewProofsViewAction']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='EmailAddress']")).SendKeys(mailkhoiphucmoi);
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='iCollectProofsViewAction']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='iFinishViewAction']")).Click();
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@name='passwd']")).SendKeys(passmoi);
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                                // xm mail lại  + Xóa SĐT 
                                chrome.Url = "https://account.live.com/password/Change";
                                Thread.Sleep(1000);
                                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                {
                                    uidgetnada = chucnang.themmoaktcc(mailkhoiphuc, chromedocma);
                                }
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//img[@src='https://logincdn.msauth.net/shared/1.0/content/images/picker_verify_email_59759b80e24a89c8cd029b14700e646d.svg']")).Click();
                                Thread.Sleep(1000);
                                dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Mở CP mail";
                                chrome.FindElement(By.XPath("//input[@name='ProofConfirmation']")).SendKeys(mailkhoiphuc);
                                Thread.Sleep(1000);
                                chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCS_SendCode']")).Click();
                                Thread.Sleep(12000);
                                if (duoimailkhoiphuccu == "@getnada.com")
                                {
                                    uidgetnada = chucnang.layuidgetnada(http, mailkhoiphuc);
                                }
                                if (duoimailkhoiphuccu == "@mailnesia.com")
                                {
                                    uidgetnada = chucnang.layuidmailnesia(mailkhoiphuc);
                                }

                                if (uidgetnada != "")
                                {
                                    if (duoimailkhoiphuccu == "@getnada.com")
                                    {
                                        otpgetnada = chucnang.laymaotpgetnada(http, uidgetnada);
                                    }
                                    if (duoimailkhoiphuccu == "@mailnesia.com")
                                    {
                                        otpgetnada = chucnang.otpmailnesia(mailkhoiphuc, uidgetnada);
                                    }
                                    if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                                    {
                                        otpgetnada = chucnang.getmamoakt(chromedocma);
                                    }
                                    if (otpgetnada != "")
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "OTP: " + otpgetnada;
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//input[@id='idTxtBx_SAOTCC_OTC']")).SendKeys(otpgetnada);
                                        Thread.Sleep(1000);
                                        chrome.FindElement(By.XPath("//input[@id='idSubmit_SAOTCC_Continue']")).Click();
                                        Thread.Sleep(1000);
                                        try
                                        {
                                            chrome.FindElement(By.XPath("//input[@id='iNext']")).Click();
                                        }
                                        catch
                                        {
                                        }
                                        dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục Thành Công => Xóa SĐT Mới Thêm";
                                        chucnang.xmmailkhoiphuc = true;
                                    }

                                }
                                if (chucnang.xmmailkhoiphuc == false)
                                {
                                    dataGridView1.Rows[dong].Cells[5].Value = "Xác Minh Mail Khôi Phục Thất Bại";

                                }
                                else
                                {
                                    // xóa sđt 

                                    bool checksdt = false;
                                    try
                                    {
                                        chrome.Url = "https://account.live.com/proofs/manage/additional";
                                        IWebElement checklg = chrome.FindElement(By.XPath("//div[@id='SMS0']"));
                                        checksdt = checklg.Displayed;
                                    }
                                    catch { }
                                    if (checksdt == true)
                                    {
                                        if (chucnang.xoasdt(chrome) == true)
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Xóa SĐT Thành Công";
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dong].Cells[5].Value = "Xóa SĐT Thất Bại";
                                        }
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[dong].Cells[5].Value = "Không Có SĐT";
                                    }
                                }

                            }
                            catch { }
                        }
                    }
                    catch { }
                    Thread.Sleep(1000);
                    // xóa mail khôi phục
                    if (checkBoxxoamailkhoiphucmoithem.Checked && checpasss == false && mailkhoiphuc != null && chucnang.xmmailkhoiphuc == true)
                    {
                        if (mailkhoiphuc != null)
                        {
                            chucnang.xoamail(chrome);
                            dataGridView1.Rows[dong].Cells[5].Value = "Xóa Mail Khôi Phục Mới Thêm Thành Công";
                            thanhcong = dataGridView1.Rows[dong].Cells[4].Value.ToString();
                            Thread.Sleep(1000);
                            thanhcong = thanhcong + "|" + "Xóa mail Cũ Mới Thêm OK";
                            fullacc = dataGridView1.Rows[dong].Cells[2].Value.ToString();
                            fullacc = fullacc.Replace(mailkhoiphuc + "|", "");
                            dataGridView1.Rows[dong].Cells[2].Value = fullacc;
                            dataGridView1.Rows[dong].Cells[4].Value = thanhcong;
                        }
                        else
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "không Có Mail Khôi Phục Cũ";

                        }

                    }
                    if (checkBoxxoasdt.Checked && mailkhoiphuc != null)
                    {
                        bool checksdt = false;
                        try
                        {
                            chrome.Url = "https://account.live.com/proofs/manage/additional";
                            IWebElement checklg = chrome.FindElement(By.XPath("//div[@id='SMS0']"));
                            checksdt = checklg.Displayed;
                        }
                        catch { }
                        if (checksdt == true)
                        {
                            if (chucnang.xoasdt(chrome) == true)
                            {
                                dataGridView1.Rows[dong].Cells[5].Value = "Xóa SĐT Thành Công";
                            }
                            else
                            {
                                dataGridView1.Rows[dong].Cells[5].Value = "Xóa SĐT Thất Bại";
                            }
                        }
                        else
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "Không Có SĐT";
                        }
                    }
                    // login lại 
                    //try
                    //{
                    //    if(checpasss == false)
                    //    {
                    //        chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";
                    //        Thread.Sleep(1000);
                    //        chrome.FindElement(By.XPath("//input[@name='passwd']")).SendKeys(passmoi);
                    //        Thread.Sleep(1000);
                    //        chrome.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                    //        dataGridView1.Rows[dong].Cells[5].Value = "Đăng Nhập Lại Thành Công";
                    //    }

                    //}
                    //catch
                    //{
                    //    dataGridView1.Rows[dong].Cells[5].Value = "Đăng Nhập Lại Thất Bại";

                    //}
                }
                catch
                {
                }



                //if (chucnang.check_dang1(chrome) == true && dang1)
                //{
                //    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Đã Các Định CP dạng 1 bắt đầu mở cp ";
                //    mocp_dang1.mpcp_dang1(chrome, keyctsc);
                //    Thread.Sleep(2000);
                //    if (cbdoipass && doipass == "")
                //    {
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Bắt Đầu Đổi Pass thêm mail khôi phục";
                //        if (mocp_dang1.mocp_dang1_doipassrandom(chrome, email, mailkhoiphucmoi) == false)
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:lỗi Khi change ";
                //            Thread.Sleep(2000);
                //        }
                //        else
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Đổi pass thành công";
                //            dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            chucnang.xoamail(chrome);
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Xóa Mail Khôi phục Cũ Thành Công";
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Thay ĐỔi Thông Tin Thành Công";
                //            File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //            chrome.Quit();
                //        }
                //    }
                //    if (cbdoipass && doipass != "")
                //    {
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Bắt Đầu Đổi Pass thêm mail khôi phục";
                //        Thread.Sleep(2000);
                //        if (mocp_dang1.mocp_dang1_doipasstudat(chrome, email, mailkhoiphucmoi) == false)
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:lỗi Khi change ";
                //        }
                //        else
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Đổi pass thành công";
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            chucnang.xoamail(chrome);
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Xóa Mail Khôi phục Cũ Thành Công";
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 1:Thay ĐỔi Thông Tin Thành Công";
                //            File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);


                //            chrome.Quit();
                //        }
                //    }
                //}

                //if (mailkhoiphuc == null && chucnang.check_dang2(chrome) == true && dang2)
                //{
                //    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: Đã Xác Định CP dạng 2 bắt đầu mở cp ";
                //    Thread.Sleep(2000); 
                //    string uidsimthue = mocp_dang2.layuidctsc(chrome, keyctsc);
                //    Thread.Sleep(10000);
                //    if (uidsimthue == "")
                //    {
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: lỗi Khi change ";
                //    }
                //    else
                //    {
                //        if(mocp_dang2.xmsdtsimthue(chrome, keyctsc, uidsimthue)== true)
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: Thêm mail khôi phục mới";
                //            if (mocp_dang2.mocp_dang2_themmailkhoiphuc(chrome, mailkhoiphucmoi) == false && !cbthemmail)
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: lỗi Khi change ";
                //            }
                //            else
                //            {
                //                dataGridView1.Rows[dong].Cells[2].Value = email + "|" + passmail + "|" + mailkhoiphucmoi;
                //            }
                //            if (mocp_dang2.mocp_dang2_xmmaildoipass(chrome, email, mailkhoiphucmoi) == false)
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: lỗi Khi change ";
                //            }
                //            else
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: Đổi pass";
                //                if (cbdoipass && doipass == "")
                //                {
                //                    if (mocp_dang2.mocp_dang2_changepassrandom(chrome, passmail, mkmoi) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: lỗi Khi change ";
                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: đổi pass thành công";
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: bắt đầu xóa mail";
                //                        if (chucnang.xoamail(chrome) == true)
                //                        {
                //                            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2:Xóa Mail Khôi phục Cũ Thành Công";
                //                        }
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        chrome.Quit();
                //                    }
                //                }
                //                if (cbdoipass && doipass != "")
                //                {
                //                    if (mocp_dang2.mocp_dang2_changepasstudatm(chrome, passmail, mkmoitudat) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: lỗi Khi change ";
                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: đổi pass thành công";
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: bắt đầu xóa mail";
                //                        if (chucnang.xoamail(chrome) == true)
                //                        {
                //                            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2:Xóa Mail Khôi phục Cũ Thành Công";
                //                            Thread.Sleep(2000);
                //                        }
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        chrome.Quit();
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            chrome.Quit();
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 2: XM SĐT THất Bại";
                //        }

                //    }

                //}
                //Thread.Sleep(2000);

                //if (chucnang.check_dang3(chrome, mailkhoiphuc) == true && dang3)
                //{
                //    dataGridView1.Rows[dong].Cells[5].Value = "Mail bị cp dạng 3";
                //    Thread.Sleep(2000);
                //    Thread.Sleep(2000);
                //    if (mocp_dang3.mocp_dang3_buoc1(chrome, mailkhoiphuc) == true) // mở cp mail cũ
                //    {
                //        //nếu đổi pass luôn
                //        try
                //        {
                //            chrome.FindElement(By.XPath("//input[@aria-describedby='pNewPwdErrorArea UpdatePasswordTitle iPassHint']")).SendKeys(mkmoi);
                //            if (doipass == "")
                //            {
                //                Thread.Sleep(2000);
                //                if (mocp_dang3.mocp_dang3_neudoipasluonrandom(chrome,mkmoi) == false)
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: đổi pass thành công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphuc;
                //                    Thread.Sleep(2000);
                //                    if (!cbthemmail && mocp_dang3.mocp_dang3_themmailkkhoiphuc(chrome, mailkhoiphucmoi) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                    else
                //                    {
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thêm mail khôi phục thành công";
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        chucnang.xoamail(chrome);
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3:Xóa Mail Khôi phục Cũ Thành Công";
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                }

                //            }
                //            else
                //            {
                //                Thread.Sleep(2000);
                //                if (mocp_dang3.mocp_dang3_neudoipasluonrandom(chrome,mkmoi) == false)
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: đổi pass thành công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphuc;
                //                    Thread.Sleep(2000);
                //                    Thread.Sleep(2000);
                //                    if (!cbthemmail && mocp_dang3.mocp_dang3_themmailkkhoiphuc(chrome, mailkhoiphucmoi) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                        return;
                //                    }
                //                    else
                //                    {
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thêm mail khôi phục thành công";
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        chucnang.xoamail(chrome);
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3:Xóa Mail Khôi phục Cũ Thành Công";
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                }

                //            }


                //        }
                //        catch // không đổi pass 
                //        {
                //            if (cbthemmail && mocp_dang3.mocp_dang3_themmailkkhoiphuc(chrome, mailkhoiphucmoi) == true)
                //            {
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thêm mail khôi phục thành công";
                //                dataGridView1.Rows[dong].Cells[2].Value = email + "|" + passmail + "|" + mailkhoiphucmoi;
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: đổi pass";
                //                if (cbdoipass && doipass == "")
                //                {
                //                    Thread.Sleep(2000);
                //                    if (mocp_dang3.mocp_dang3_changepassrandom(chrome, mailkhoiphuc, passmail, mkmoi) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "đổi pass thành công";
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        chucnang.xoamail(chrome);
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3:Xóa Mail Khôi phục Cũ Thành Công";
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                }
                //                if (cbdoipass && doipass != "")
                //                {
                //                    Thread.Sleep(2000);
                //                    if (mocp_dang3.mocp_dang3_changepasstudat(chrome, mailkhoiphuc, passmail, mkmoitudat) == false)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: đổi pass thành công";
                //                        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                        chucnang.xoamail(chrome);
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3:Xóa Mail Khôi phục Cũ Thành Công";
                //                        Thread.Sleep(2000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: Thay ĐỔi Thông Tin Thành Công";
                //                        File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                }

                //            else
                //            {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 3: lỗi Khi change ";
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }

                //            }

                //        }

                //    }

                //}
                //if (chucnang.check_dang4(chrome, mailkhoiphuc) == true && dang4)
                //{
                //    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: Mail bị cp dạng 4";
                //    if (mocp_dang4.mocp_dang4_Buoc1(chrome, mailkhoiphuc, mailkhoiphucmoi) == false)
                //    {
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: lỗi Khi change ";
                //        Thread.Sleep(2000);
                //        chrome.Quit();
                //    }
                //    else
                //    {
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: Thêm mail thành công";
                //        Thread.Sleep(2000);
                //        dataGridView1.Rows[dong].Cells[2].Value = email + "|" + passmail + "|" + mailkhoiphucmoi;
                //        Thread.Sleep(2000);
                //        if (doipass == "")
                //        {
                //            if (mocp_dang4.mocp_dang4_changepassrandom(chrome, passmail, mkmoi) == false)
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: lỗi Khi change ";
                //                Thread.Sleep(2000);
                //                chrome.Quit();
                //            }
                //            else
                //            {
                //                dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                Thread.Sleep(2000);
                //                chucnang.xoamail(chrome);
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4:Xóa Mail Khôi phục Cũ Thành Công";
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: Thay ĐỔi Thông Tin Thành Công";
                //                Thread.Sleep(2000);
                //                File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                chrome.Quit();
                //            }
                //        }

                //    }
                //    if (doipass != "")
                //    {
                //        if (mocp_dang4.mocp_dang4_changepassmktudat(chrome, passmail, mkmoitudat) == false)
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: lỗi Khi change ";
                //            Thread.Sleep(2000);
                //            chrome.Quit();
                //        }
                //        else
                //        {
                //            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //            Thread.Sleep(2000);
                //            chucnang.xoamail(chrome);
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4:Xóa Mail Khôi phục Cũ Thành Công";
                //            Thread.Sleep(2000);
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 4: Thay ĐỔi Thông Tin Thành Công";
                //            File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //            Thread.Sleep(2000);
                //            chrome.Quit();
                //        }
                //    }
                //}
                //try
                //{
                //    if (dang5)
                //    {
                //        try
                //        {
                //            chrome.Url = "https://login.live.com/login.srf?";
                //            Thread.Sleep(1000);
                //            chrome.FindElement(By.XPath("//input[@id='StartAction']")).Click();
                //            Thread.Sleep(1000);
                //            chrome.FindElement(By.XPath("//select[@class='form-control input-max-width']")).Click();
                //            Thread.Sleep(1000);
                //            chrome.FindElement(By.XPath("//option[@value='VN']")).Click();
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Bắt Đầu Xác Minh SĐT";

                //            string uidsimthue = mocp_dang2.layuidctsc(chrome, keyctsc);
                //            Thread.Sleep(10000);
                //            if (uidsimthue != "")
                //            {
                //                if (mocp_dang2.xmsdtsimthue(chrome, keyctsc, uidsimthue) == true)
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM SĐT Thành Công";
                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM SĐT Thất Bại";
                //                }
                //            }
                //            else
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM SĐT Thất Bại";
                //            }
                //        }
                //        catch { }
                //        Thread.Sleep(2000);
                //        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Bắt Đầu Xác Minh Mail Khôi Phục";
                //        if (mocp_dang2.mocp_dang2_xmmailkhoiphucdoipass(chrome, email, mailkhoiphuc) == true)
                //        {
                //            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5:Xác minh thành công , Bắt Đầu thêm mail";

                //            Thread.Sleep(2000);
                //            if (cbthemmail)
                //            {
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thêm Mail Khôi Phục";
                //                if (mocp_dang2.mocp_dang2_themmailkhoiphuc(chrome, mailkhoiphucmoi) == true)
                //                {
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thêm Mail Thành Công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[2].Value = email + "|" + passmail + "|" + mailkhoiphucmoi;
                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thêm Mail Thất Bại";
                //                }
                //            }
                //            if (cbdoipass && doipass == "")
                //            {
                //                Thread.Sleep(2000);
                //                if (mocp_dang2.mocp_dang2_changepassrandom(chrome, passmail, mkmoi) == false)

                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: lỗi Khi change ";
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay Đổi Mật Khẩu Thành Công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                    Thread.Sleep(2000);
                //                    chucnang.xoamail(chrome);
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5:Xóa Mail Khôi phục Cũ Thành Công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay ĐỔi Thông Tin Thành Công";
                //                    File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }
                //            }
                //            if (cbdoipass && doipass != "")
                //            {
                //                Thread.Sleep(2000);
                //                if (mocp_dang2.mocp_dang2_changepasstudatm(chrome, passmail, mkmoitudat) == false)
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: lỗi Khi change ";
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();

                //                }
                //                else
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay Đổi Mật Khẩu Thành Công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                    Thread.Sleep(2000);
                //                    chucnang.xoamail(chrome);
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5:Xóa Mail Khôi phục Cũ Thành Công";
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay ĐỔi Thông Tin Thành Công";
                //                    File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                    Thread.Sleep(2000);
                //                    chrome.Quit();
                //                }
                //            }

                //        }
                //        else
                //        {
                //            Thread.Sleep(2000);
                //            if (cbthemmail && mocp_dang2.mocp_dang2_themmailkhoiphuc(chrome, mailkhoiphucmoi) == true)
                //            {
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thêm Mail khôi phục Thành Công";
                //                Thread.Sleep(2000);
                //                dataGridView1.Rows[dong].Cells[2].Value = email + "|" + passmail + "|" + mailkhoiphucmoi;
                //                Thread.Sleep(1000);
                //                Thread.Sleep(2000);
                //                if (cbdoipass && doipass == "")
                //                {
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM mail Để Đổi Pass";
                //                    if (mocp_dang2.mocp_dang2_xmmaildoipass(chrome, email, mailkhoiphucmoi) == true)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM mail Thành công ";
                //                        Thread.Sleep(1000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Bắt Đầu Đổi Pass";
                //                        Thread.Sleep(2000);
                //                        if (mocp_dang2.mocp_dang2_changepassrandom(chrome, passmail, mkmoi) == false)

                //                        {
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: lỗi Khi change ";
                //                            Thread.Sleep(2000);
                //                            chrome.Quit();
                //                        }
                //                        else
                //                        {
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay Đổi Mật Khẩu Thành Công";
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoi + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            chucnang.xoamail(chrome);
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5:Xóa Mail Khôi phục Cũ Thành Công";
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay ĐỔi Thông Tin Thành Công";
                //                            File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                            Thread.Sleep(2000);
                //                            chrome.Quit();
                //                        }

                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Không Thể XM mail để đổi pass ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }

                //                }
                //                if (cbdoipass && doipass != "")
                //                {
                //                    Thread.Sleep(2000);
                //                    dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM mail Để Đổi Pass";
                //                    if (mocp_dang2.mocp_dang2_xmmaildoipass(chrome, email, mailkhoiphucmoi) == true)
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: XM mail Thành công ";
                //                        Thread.Sleep(1000);
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Bắt Đầu Đổi Mật Khẩu";
                //                        if (mocp_dang2.mocp_dang2_changepasstudatm(chrome, passmail, mkmoitudat) == false)
                //                        {
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: lỗi Khi change ";
                //                            Thread.Sleep(2000);
                //                            chrome.Quit();
                //                        }
                //                        else
                //                        {
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay Đổi Mật Khẩu Thành Công";
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[2].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[4].Value = email + "|" + mkmoitudat + "|" + mailkhoiphucmoi;
                //                            Thread.Sleep(2000);
                //                            chucnang.xoamail(chrome);
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5:Xóa Mail Khôi phục Cũ Thành Công";
                //                            Thread.Sleep(2000);
                //                            dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Thay ĐỔi Thông Tin Thành Công";
                //                            File.AppendAllText(Directory.GetCurrentDirectory() + "\\filethanhcong.txt", dataGridView1.Rows[dong].Cells[4].Value.ToString() + Environment.NewLine);
                //                            Thread.Sleep(2000);
                //                            chrome.Quit();
                //                        }
                //                    }
                //                    else
                //                    {
                //                        dataGridView1.Rows[dong].Cells[5].Value = "Dạng 5: Không Thể XM mail để đổi pass ";
                //                        Thread.Sleep(2000);
                //                        chrome.Quit();
                //                    }
                //                }

                //            }

                //        }
                //        Thread.Sleep(2000);
                //    }
                //}
                //catch { }
                if (imap && checpasss == false)
                {
                    try
                    {
                        dataGridView1.Rows[dong].Cells[5].Value = "Bắt Đầu Bật IMAP";
                        chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";
                        Thread.Sleep(2000);

                        while (true)
                        {
                            string urlok = chrome.Url;
                            Regex checkurl = new Regex("options/mail/layout");
                            Match checkurl1 = checkurl.Match(urlok);
                            Regex checkurl2 = new Regex("options/mail/accounts");
                            Match checkurl3 = checkurl2.Match(urlok);
                            if (checkurl1 != Match.Empty | checkurl3 != Match.Empty)
                                break;
                        }
                        chrome.Url = "https://outlook.live.com/mail/0/options/mail/accounts";
                        Thread.Sleep(2000);
                        while (true)
                        {
                            string urlok = chrome.Url;
                            Regex checkurl = new Regex("options/mail/layout");
                            Match checkurl1 = checkurl.Match(urlok);
                            Regex checkurl2 = new Regex("options/mail/accounts");
                            Match checkurl3 = checkurl2.Match(urlok);
                            if (checkurl1 != Match.Empty | checkurl3 != Match.Empty)
                                break;
                        }
                        try
                        {
                            chrome.FindElement(By.XPath("/html/body/div[5]/div[3]/div/div/div/div[2]/div[2]/div/div[1]/div[2]/button/span")).Click();
                        }
                        catch
                        {

                        }
                        Thread.Sleep(1000);
                        chrome.FindElement(By.XPath("//input[@type='radio']")).Click();
                        Thread.Sleep(3000);
                        try
                        {
                            chrome.FindElement(By.XPath("/html/body/div[5]/div[1]/div/div/div/div[2]/div[2]/div/div[2]/div[3]/button[1]/span/span/span")).Click();
                            dataGridView1.Rows[dong].Cells[5].Value = "Bật IMAP Thành Công";
                        }
                        catch
                        {
                            dataGridView1.Rows[dong].Cells[5].Value = "Đã bật IMAP";
                        }

                    }
                    catch { }
                }
                if (checkkboxxoathu && mailkhoiphuc != "" && checpasss == false)
                {
                    if (xoathumailcu.xoathu(mailkhoiphuc) == true)
                    {
                        dataGridView1.Rows[dong].Cells[3].Value = "Thư Đã Được Xóa Hết";
                    }
                    else
                    {
                        dataGridView1.Rows[dong].Cells[3].Value = "Xóa Thư Thất Bại";
                    }
                }
            }
            catch
            {
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\output"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\output");
            }
            try
            {
                fullacc = dataGridView1.Rows[dong].Cells["fullacc"].Value.ToString();
                thanhcong = dataGridView1.Rows[dong].Cells["success"].Value.ToString();
                string status = dataGridView1.Rows[dong].Cells["status"].Value.ToString();
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\output\\fullacc.txt", fullacc + "|" + thanhcong + "|" + status + Environment.NewLine); // lưu  OK

            }
            catch { }






            //while (true)
            //{
            //    if (chucnang.checkluufile == true)
            //        chucnang.checkluufile = false;
            //    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\output"))
            //    {
            //        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\output");
            //    }
            //    string[] mangacc = chucnang.accfile.Split('\n');
            //    string dongtrongmang = mangacc[dong];
            //    chucnang.accfile = chucnang.accfile.Replace(dongtrongmang, fullacc + "|" + thanhcong); // lưu lại ok
            //    FileHelper.WriteToFile(urlfile, chucnang.accfile);
            //    Thread.Sleep(3000);
            //    chucnang.checkluufile = true;
            //    break;
            //}
            try
            {
                if (duoimailkhoiphuccu == "@" + "teml.net" | duoimailkhoiphuccu == "@" + "tmpeml.com" | duoimailkhoiphuccu == "@" + "tmpbox.net" | duoimailkhoiphuccu == "@" + "moakt.cc" | duoimailkhoiphuccu == "@" + "disbox.nett" | duoimailkhoiphuccu == "@" + "tmpmail.org" | duoimailkhoiphuccu == "@" + "tmpmail.net" | duoimailkhoiphuccu == "@" + "tmails.net" | duoimailkhoiphuccu == "@" + "disbox.org" | duoimailkhoiphuccu == "@" + "moakt.co" | duoimailkhoiphuccu == "@" + "moakt.ws" | duoimailkhoiphuccu == "@" + "tmail.ws" | duoimailkhoiphuccu == "@" + "bareed.ws")
                {
                    chromedocma.Url = "https://www.moakt.com/en/inbox/";
                    Thread.Sleep(1000);
                    chromedocma.FindElement(By.XPath("//a[@href='/en/inbox/logout']")).Click();
                    Thread.Sleep(1000);
                }
                if (duoimailkhoiphucmoi == "@" + "teml.net" | duoimailkhoiphucmoi == "@" + "tmpeml.com" | duoimailkhoiphucmoi == "@" + "tmpbox.net" | duoimailkhoiphucmoi == "@" + "moakt.cc" | duoimailkhoiphucmoi == "@" + "disbox.nett" | duoimailkhoiphucmoi == "@" + "tmpmail.org" | duoimailkhoiphucmoi == "@" + "tmpmail.net" | duoimailkhoiphucmoi == "@" + "tmails.net" | duoimailkhoiphucmoi == "@" + "disbox.org" | duoimailkhoiphucmoi == "@" + "moakt.co" | duoimailkhoiphucmoi == "@" + "moakt.ws" | duoimailkhoiphucmoi == "@" + "tmail.ws" | duoimailkhoiphucmoi == "@" + "bareed.ws")
                {
                    chromedocma.Url = "https://www.moakt.com/en/inbox/";
                    Thread.Sleep(1000);
                    chromedocma.FindElement(By.XPath("//a[@href='/en/inbox/logout']")).Click();
                    Thread.Sleep(1000);
                }
            }
            catch { }
            try
            {
                chrome.Quit();
                chromedocma.Quit();
            }
            catch { }
            WriteLog();
        }

        void WriteLog()
        {
            try
            {
                // WriteLogData
                List<string> data_log = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string stt = dataGridView1.Rows[i].Cells["stt"].Value != null ? dataGridView1.Rows[i].Cells["stt"].Value.ToString() : "";
                    string tich = dataGridView1.Rows[i].Cells["tick"].Value != null ? dataGridView1.Rows[i].Cells["tick"].Value.ToString() : "";
                    string fullacc = dataGridView1.Rows[i].Cells["fullacc"].Value != null ? dataGridView1.Rows[i].Cells["fullacc"].Value.ToString() : "";
                    string mailkkhoiphucuc = dataGridView1.Rows[i].Cells["mailcu"].Value != null ? dataGridView1.Rows[i].Cells["mailcu"].Value.ToString() : "";
                    string success = dataGridView1.Rows[i].Cells["success"].Value != null ? dataGridView1.Rows[i].Cells["success"].Value.ToString() : "";
                    string status = dataGridView1.Rows[i].Cells["status"].Value != null ? dataGridView1.Rows[i].Cells["status"].Value.ToString() : "";

                    string row = $"{stt}<>{tich}<>{fullacc}<>{mailkkhoiphucuc}<>{success}<>{status.Replace("\n", "").Replace("\r", "")}";
                    data_log.Add(row);
                }
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Log Data"))
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Log Data");
                FileHelper.WriteToFile(Directory.GetCurrentDirectory() + "\\Log Data\\" + $"fullacc.txt", string.Join("\n", data_log));

            }
            catch { }

        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            chucnang.startstop = false;
            buttonStart.Enabled = true;
            Thread.Sleep(2000);
            MessageBox.Show("STOP OK CHỜ CÁC TÀI KHOẢN CÒN LẠI ĐANG CHẠY");
        }
        private void fmain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Thông Báo", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
                e.Cancel = true;
            else
            {
                ProcessHelper.KillAllProcessTree("chromedriver");
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                DialogResult dialogResult;
                dialogResult =
                        MessageBox.Show(
                            $@"CÓ PHIÊN BẢN MỚI {args.CurrentVersion}. PHIÊN BẢN HIỆN TẠI {args.InstalledVersion}. CHỌN YES ĐỂ UPDATE", @"UPDATE TOOL AUTOCHANGE MỞ CP",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //DialogResult dialogResult;
                //dialogResult =
                //       MessageBox.Show(
                //           $@"PHIÊN BẢN HIỆN TẠI {args.InstalledVersion}. LÀ PHIÊN BẢN MỚI NHẤT", @"UPDATE TOOL AUTOCHANGE MỞ CP",
                //           MessageBoxButtons.OK,
                //           MessageBoxIcon.Information);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timecodesim.Value = Settings1.Default.timecodesim;
            timecodemail.Value = Settings1.Default.timecodemail;
            textBoxkeysimthue.Text = Settings1.Default.apikey;
            textBoxduoimailkp.Text = Settings1.Default.duoimailkhoiphuc;
            textBoxdoipass.Text = Settings1.Default.mktudat;
            AppService.rsaForClient = new RSACryptoServiceProvider();
            AppService.rsaForServer = new RSACryptoServiceProvider();
            if (!AppService.LoadServerPublic() || !AppService.LoadClientPrivate())
            {
                MessageBox.Show("Không Kết Nối Được Với sever vui lòng kiếm tra lại đường truyền ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            //string biosSerial =  AppService.getBiosSerial();
            //biosSerial = "hm" + AppService.MD5Hash(biosSerial);
            //biosSerial = biosSerial.ToUpper();
            //var encryptedData = AppService.rsaForServer.Encrypt(Encoding.UTF8.GetBytes(biosSerial), false);
            //string requesData = Convert.ToBase64String(encryptedData);
            //labelkeyok.Text = biosSerial;
            try
            {
                string key = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber()
                                                .AddSystemDriveSerialNumber()
                                                .ToString();
                using (var sha = new System.Security.Cryptography.SHA256Managed())
                {
                    byte[] textData = System.Text.Encoding.UTF8.GetBytes("FBB" + key);
                    byte[] hash = sha.ComputeHash(textData);
                    labelkeyok.Text = "HM" + (BitConverter.ToString(hash).Replace("-", String.Empty)).ToString().Substring(0, 25);
                }
            }
            catch { }
            var encryptedData = AppService.rsaForServer.Encrypt(Encoding.UTF8.GetBytes(labelkeyok.Text), false);
            string requesData = Convert.ToBase64String(encryptedData);
            RestClient restClient = new RestClient("http://quanlytoolndl.vip/keyfb.php");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddParameter("data", requesData);
            IRestResponse response = restClient.Execute(restRequest);
            string responseData = response.Content;
            //
            if (responseData == "nokey")
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = false;
                MessageBox.Show("Key đã hết hạn hoặc chưa đăng ký, liên hệ admin để kích hoạt key, ", "Thông báo", MessageBoxButtons.OKCancel);
                Form2 form2 = new Form2(labelkeyok.Text);
                form2.ShowDialog();



                //      MessageBox.Show("Key Hết hạn Hoặc chưa được gia hạn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var decryptedData = AppService.rsaForClient.Decrypt(Convert.FromBase64String(responseData), false);
                    var ngayhethan = Encoding.UTF8.GetString(decryptedData);
                    // ngayhethan =   $"{("0" + dtExpired.Day).Substring(("0" + dtExpired.Day).Length - 2)}-{("0" + dtExpired.Month).Substring(("0" + dtExpired.Month).Length - 2)}-{dtExpired.Year}";

                    long tsCurrent = Time.ConvertToTimestamp(Time.GetDateTime());
                    DateTime datahh = DateTime.Parse(ngayhethan, System.Globalization.CultureInfo.InvariantCulture);
                    long tsExpired = Time.ConvertToTimestamp(datahh);
                    label1date.Text = "Ngày hết hạn" + ": " + $"{("0" + datahh.Day).Substring(("0" + datahh.Day).Length - 2)}-{("0" + datahh.Month).Substring(("0" + datahh.Month).Length - 2)}-{datahh.Year}";
                    if (tsCurrent >= tsExpired)
                    {
                        buttonStart.Enabled = false;
                        buttonStop.Enabled = false;
                        MessageBox.Show("Key đã hết hạn hoặc chưa đăng ký, liên hệ admin để kích hoạt key, ", "Thông báo", MessageBoxButtons.OKCancel);
                        Form2 form2 = new Form2(labelkeyok.Text);
                        form2.ShowDialog();
                    }
                    else
                    {
                        label1date.Text = "Ngày hết hạn" + ": " + $"{("0" + datahh.Day).Substring(("0" + datahh.Day).Length - 2)}-{("0" + datahh.Month).Substring(("0" + datahh.Month).Length - 2)}-{datahh.Year}";
                        // label1.Text = ngayhethan;
                        buttonStart.Enabled = !false;
                        buttonStop.Enabled = !false;

                    }
                }
                catch
                {
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = false;
                    MessageBox.Show("Key đã hết hạn hoặc chưa đăng ký, liên hệ admin để kích hoạt key, ", "Thông báo", MessageBoxButtons.OKCancel);
                    Form2 form2 = new Form2(labelkeyok.Text);
                    form2.ShowDialog();
                }
                AutoUpdater.Start("https://quanlykey.autos/updatehotmail.xml");
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                label15.Text = "PHIÊN BẢN: " + version;

                AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;


                AutoUpdater.DownloadPath = "update";
                System.Timers.Timer timer = new System.Timers.Timer
                {
                    Interval = 15 * 60 * 1000,
                    SynchronizingObject = this
                };
                timer.Elapsed += delegate
                {
                    AutoUpdater.Start("https://quanlykey.autos/updatehotmail.xml");
                };
                timer.Start();
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelkeyok.Text);
            MessageBox.Show("Đã copy key !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_2(object sender, EventArgs e)
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
            string mailcu = http.Get("https://getnada.com/api/v1/inboxes/enjuliele052022@getnada.com").ToString();
            mailcu = mailcu.Substring(mailcu.IndexOf("msgs\":[")).Replace("msgs\":[", "").Replace("]}", "");
            string[] catmailcu = mailcu.Split('}');
            for (int i = 0; i < catmailcu.Length; i++)
            {
                string mail1 = catmailcu[i] + "}";
                string uidxoa = mail1.Substring(mail1.IndexOf("uid\":\"")).Replace("uid\":\"", "").Substring(0, 30);
                var client = new RestClient("https://getnada.com/api/v1/messages/" + uidxoa);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                IRestResponse response = client.Execute(request);
                string responseData = response.Content;

            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Thread t = new Thread(() => {

                int so_luong_toi_da = (int)numericUpDown1.Value; // số luồng tối đa
                int so_luong_dang_chay = 0; // lưu số luồng đang chạy đồng thời
                qu_position_pro5.Clear();
                // nguyen tac 2: Thằng chạy xong thì gaim sô luong dang chay --
                // nguyên tắc 3: Trước khi chạy thì kiểm tra so_luong_dang_chay < so_luong_toi_da
                //  neu dat max thi cho den khi nao so_luong_dang_chay < so_luong_toi_da đúng [có thằng đang chạy mà hoàn thành]

                // duyệt từ đầu tới cuối
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    // chờ đến khi nào đủ điều kiện được chạy
                    while (true)
                    {
                        if (so_luong_dang_chay < so_luong_toi_da)
                            break;
                    }


                    // đã đạt yc được chạy

                    so_luong_dang_chay++;
                    int dong = i;
                    qu_position_pro5.Enqueue(so_luong_dang_chay);

                    Thread t2 = new Thread(() =>
                    {
                        int pro5 = qu_position_pro5.Dequeue();
                        if ((bool)dataGridView1.Rows[dong].Cells[1].FormattedValue)
                        {
                            try
                            {
                                string xacdinhvitri1 = (string)dataGridView1.Rows[dong].Cells[2].Value;
                                string[] tk1 = xacdinhvitri1.Split('|');
                                NeedInfo acc = new NeedInfo();
                                acc.email = tk1[0];
                                acc.passmail = tk1[1];
                                try
                                {
                                    acc.mailkhoiphuc = tk1[2];
                                }
                                catch (Exception) { }
                                string duoigetnada = textBoxduoimailkp.Text;
                                acc.mailkhoiphucmoi = acc.email.Substring(0, acc.email.IndexOf("@")) + duoigetnada + "@getnada.com";
                                if (xoathumailcu.xoathu(acc.mailkhoiphuc) == true)
                                {
                                    dataGridView1.Rows[dong].Cells[3].Value = "Xóa Mail Thành Công";
                                }
                                else
                                {
                                    dataGridView1.Rows[dong].Cells[3].Value = "Xóa Mail Thất Bại";

                                }
                                Thread.Sleep(2000);

                            }
                            catch (Exception) { }
                        }
                        so_luong_dang_chay--;
                        qu_position_pro5.Enqueue(pro5);
                        dataGridView1.Rows[dong].Cells[1].Value = false;
                        // chạy xong hết dòng này thì update lại số luồng để dòng khác được chạy
                        // khi chạy xong
                    });
                    t2.Start();
                    Thread.Sleep(2000);
                }
            });
            t.Start(); // gọi hàm chạy thread
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string data_init = File.ReadAllText(Directory.GetCurrentDirectory() + "\\init_info.json"); // doc file
                JObject jObject = JObject.Parse(data_init);
                ConfigInfo.chromeWidth = Int32.Parse(jObject["chromeWidth"].ToString());
                ConfigInfo.chromeHeight = Int32.Parse(jObject["chromeHeight"].ToString());
                ConfigInfo.chromeDistanceX = Int32.Parse(jObject["chromeDistanceX"].ToString());
                ConfigInfo.chromeDistanceY = Int32.Parse(jObject["chromeDistanceY"].ToString());
            }
            catch { }
            try
            {
                JObject jObject = new JObject();
                jObject["chromeWidth"] = ConfigInfo.chromeWidth;
                jObject["chromeHeight"] = ConfigInfo.chromeHeight;
                jObject["chromeDistanceX"] = ConfigInfo.chromeDistanceX;
                jObject["chromeDistanceY"] = ConfigInfo.chromeDistanceY;
                string init_info = JsonConvert.SerializeObject(jObject);
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\init_info.json", init_info);

            }
            catch
            {
            }
            int so_luong_dang_chay = 0; // lưu số luồng đang chạy đồng thời
            qu_position_pro5.Clear();
            for (int i = 1; i <= (int)numericUpDown1.Value; i++)
                qu_position_pro5.Enqueue(i);
            chucnang.startstop = true;
            Thread t = new Thread(() => {

                int so_luong_toi_da = (int)numericUpDown1.Value; // số luồng tối đa
                qu_position_pro5.Clear();
                // nguyen tac 2: Thằng chạy xong thì gaim sô luong dang chay --
                // nguyên tắc 3: Trước khi chạy thì kiểm tra so_luong_dang_chay < so_luong_toi_da
                //  neu dat max thi cho den khi nao so_luong_dang_chay < so_luong_toi_da đúng [có thằng đang chạy mà hoàn thành]

                // duyệt từ đầu tới cuối
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    // chờ đến khi nào đủ điều kiện được chạy
                    while (true)
                    {
                        if (so_luong_dang_chay < so_luong_toi_da)
                            break;
                    }


                    // đã đạt yc được chạy

                    so_luong_dang_chay++;
                    int dong = i;
                    qu_position_pro5.Enqueue(so_luong_dang_chay);

                    Thread t2 = new Thread(() =>
                    {
                        int pro5 = qu_position_pro5.Dequeue();
                        if ((bool)dataGridView1.Rows[dong].Cells[1].FormattedValue)
                        {
                            try
                            {
                                string xacdinhvitri1 = (string)dataGridView1.Rows[dong].Cells[2].Value;
                                string[] tk1 = xacdinhvitri1.Split('|');
                                NeedInfo acc = new NeedInfo();
                                acc.email = tk1[0];
                                acc.passmail = tk1[1];
                                try
                                {
                                    acc.mailkhoiphuc = tk1[2];
                                }
                                catch (Exception) { }
                                if (xoathumailcu.xoathu(acc.mailkhoiphuc) == true)
                                {
                                    dataGridView1.Rows[dong].Cells[3].Value = "Xóa Mail Thành Công";
                                }
                                else
                                {
                                    dataGridView1.Rows[dong].Cells[3].Value = "Xóa Mail Thất Bại";

                                }
                                Thread.Sleep(2000);

                            }
                            catch (Exception) { }
                        }
                        so_luong_dang_chay--;
                        qu_position_pro5.Enqueue(pro5);
                        dataGridView1.Rows[dong].Cells[1].Value = false;
                        // chạy xong hết dòng này thì update lại số luồng để dòng khác được chạy
                        // khi chạy xong
                    });
                    t2.Start();
                    Thread.Sleep(2000);
                }
            });
            t.Start(); // gọi hàm chạy thread
        }

        private void button1_Click_4(object sender, EventArgs e)
        {

            try
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\output"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\output");
                }
                string filedata = Directory.GetCurrentDirectory() + "\\output";
                Process.Start(filedata);
            }
            catch (Exception)
            {
                MessageBox.Show("File Không Tồn Tại");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(labelkeyok.Text);
            MessageBox.Show("Đã copy key !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxkeyctsc_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.apikey = textBoxkeysimthue.Text;
            Settings1.Default.Save();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.duoimailkhoiphuc = textBoxduoimailkp.Text;
            Settings1.Default.Save();
        }

        private void checkBoxxoamailkhoiphuc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mailkhoiphucmoi = "test@gmail.com";
            string duoimailkhoiphuc = mailkhoiphucmoi.Substring(mailkhoiphucmoi.IndexOf("@"));
        }

        private void comboBoxmoakt_SelectedIndexChanged(object sender, EventArgs e)
        {
            NeedInfo.duoimoakkt = comboBoxmoakt.Text;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timecodesim_ValueChanged(object sender, EventArgs e)
        {
            Settings1.Default.timecodesim = (int)timecodesim.Value;
            Settings1.Default.Save();
        }

        private void timecodemail_ValueChanged(object sender, EventArgs e)
        {
            Settings1.Default.timecodemail = (int)timecodesim.Value;
            Settings1.Default.Save();
        }
    }
}


