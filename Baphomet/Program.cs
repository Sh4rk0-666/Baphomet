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

            byte[] key = aes.Key; //llave de cifrado auto que auto genero con aes

            Console.WriteLine("Enter message to encrypt:");
            string message = Console.ReadLine();

            //metodo de cifrado
            EncryptText(aes, message, "encryptedData.txt");

           //metodo para decifrar mensage
            var Message =  DecryptData(aes, "encryptedData.txt");

            File.WriteAllText("Decryp.txt", Message);

        }

        static void EncryptText(SymmetricAlgorithm aesAlgorithm, string text, string fileName)
        {
            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(text);
                    }
                }

                byte[] encryptedDataBuffer = ms.ToArray();

                File.WriteAllBytes(fileName, encryptedDataBuffer);
            }
        }

        static string DecryptData(SymmetricAlgorithm aesAlgorithm, string fileName)
        {
            ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            byte[] encryptedDataBuffer = File.ReadAllBytes(fileName);

            using (MemoryStream ms = new MemoryStream(encryptedDataBuffer))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }
        }
    }
}
