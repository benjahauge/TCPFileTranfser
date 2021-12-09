using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;

namespace TestFileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the TCPFileTransfer program, sending files to the REST-service");

            string[] files = Directory.GetFiles(@"c:/PythonVideo/");
            List<TransferClass> transferList = new List<TransferClass>();

            foreach (var file in files)
            {
                byte[] bytes = File.ReadAllBytes(file);

                TransferClass myTrans = new() { VideoName = file, Data = bytes };
                transferList.Add(myTrans);
            }

            SendToRest(transferList);
        }

        public static void SendToRest(List<TransferClass> myTrans)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(myTrans);
                HttpResponseMessage result = client.PostAsync("https://camsanctuary.azurewebsites.net/api/receiver/video", content).Result;
                Console.WriteLine(result.StatusCode);
            }
        }
    }

    class TransferClass
    {
        public byte[] Data { get; set; }
        public string VideoName { get; set; }
    }
}