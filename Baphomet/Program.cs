using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet
{
    class Program
    {
        static void Main()
        {
            // creo una instancia del algoritmo aes 
            SymmetricAlgorithm aes = new AesManaged();

            byte[] key = aes.Key;

            //Console.WriteLine("Enter message to encrypt:");
            //string message = Console.ReadLine();

            var path = Path.GetFullPath("files/chucknorris.jpg");
            byte[] message = System.IO.File.ReadAllBytes(path);

            //metodo de cifrado
            EncryptText(aes, message, "encryptedData.txt");

           //metodo para decifrar mensage
            var Message =  DecryptData(aes, "encryptedData.txt.Baphomet");

            File.WriteAllText("Decryp.jpg", Message);

        }

        static void EncryptText(SymmetricAlgorithm aesAlgorithm, byte[] text, string fileName)
        {
           // ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            byte[] encryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms,AES.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV), CryptoStreamMode.Write))
                    {
                        cs.Write(text, 0, text.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                    File.WriteAllBytes(fileName, encryptedBytes);
                    System.IO.File.Move(fileName, fileName + ".Baphomet");

                }
            }
        }

        static string DecryptData(SymmetricAlgorithm aesAlgorithm, string fileName)
        {
            ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            byte[] decryptedBytes = null;
            byte[] encryptedDataBuffer = File.ReadAllBytes(fileName);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    using (var cs = new CryptoStream(ms,decryptor , CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedDataBuffer, 0, encryptedDataBuffer.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                    File.WriteAllBytes(fileName, decryptedBytes);

                    
                }

            }

            return ("fefef");
        }
    }
}
