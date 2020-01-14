using System;

namespace BaphometDecrypt
{
    class Program
    {
        static void Main()
        {
            Decrypt decrypt = new Decrypt();

            var userName = Environment.UserName;
            //Directorios donde los usuarios suelen guardar sus archivos ("Desktop","Documents","Pictures" etc)
            var Dirs = new[] { "\\Downloads" };
            var userDir = "C:\\Users\\" + userName;
            var password = Message();

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var decryp_targetPath = userDir + Dirs[d];
                decrypt.directoryRoad(decryp_targetPath, password);
            }
        }

        static string Message()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Enter your key here:");
            string password = Console.ReadLine();
            return password;
        }//</message>
    }
}
