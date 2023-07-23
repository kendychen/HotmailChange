using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Hotmail_Change_V1._0
{
   public class AppService
    {
        public static RSACryptoServiceProvider rsaForClient;
        public static RSACryptoServiceProvider rsaForServer;
        private static string serverPublic; // nơi lưu public key của server (cặp key client server - 1)
        private static string clientPrivate; // nơi lưu private key của client (cặp key client server - 2)

        public static string RunCommand(string command)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = "/c" + command;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            string outputScreen = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            if (string.IsNullOrEmpty(outputScreen))
            {
                return "error";
            }
            return outputScreen;
        }

        public static string getBiosSerial()
        {
            string biosSerial = RunCommand("wmic bios get serialNumber");
            biosSerial =   biosSerial.Split('\n')[1];
            biosSerial =  biosSerial;
            return biosSerial.Trim();
        }

        public static string getDiskDriveSerial()
        {
            string diskSerial = RunCommand("wmic diskdrive get serialNumber");
            diskSerial = diskSerial.Split('\n')[1];
            return diskSerial.Trim();
        }

        private static string getHwid()
        {
            return WindowsIdentity.GetCurrent().User.Value.ToString();
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool LoadServerPublic() // Load public key của server
        {
            string publicKeyPath = Environment.CurrentDirectory + "/keypairs/serverPublic.pem";
            FileInfo fileInfo = new FileInfo(publicKeyPath);
            if (!fileInfo.Exists) // kiểm tra file có tồn tại hay không
            {
                return false;
            }
            using (StreamReader sr = new StreamReader(publicKeyPath))
            {
                serverPublic = sr.ReadToEnd();
                sr.Close();
            }
            rsaForServer = RSAKey.ImportPublicKey(rsaForServer, serverPublic);
            return true;
        }

        public static bool LoadClientPrivate() // Load private key của client
        {
            // Đọc private key từ file
            string privateKeyPath = Environment.CurrentDirectory + "/keypairs/clientPrivate.pem";
            FileInfo fileInfo = new FileInfo(privateKeyPath);
            if (!fileInfo.Exists) // kiểm tra file có tồn tại hay không
            {
                return false;
            }
            using (StreamReader sr = new StreamReader(privateKeyPath))
            {
                clientPrivate = sr.ReadToEnd();
                sr.Close();
            }
            rsaForClient = RSAKey.ImportPrivateKey(rsaForClient, clientPrivate);
            return true;
        }
    }
}
