using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace TestFileReceive
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			TcpListener listener = new TcpListener(IPAddress.Any, 7777);
			listener.Start();

			while (true)
			{
				try
				{
					TcpClient socket = listener.AcceptTcpClient();

					NetworkStream ns = socket.GetStream();
					StreamReader reader = new StreamReader(ns);

					string jsonString = reader.ReadToEnd();
					Console.WriteLine("JsonString" + jsonString);

					TransferClass transfered = JsonSerializer.Deserialize<TransferClass>(jsonString);

					File.WriteAllBytes($"c:/PythonVideo/{transfered.VideoName}.h264", transfered.Data);

					Console.Write("saved file: " + transfered.VideoName);

					socket.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}
	}
	class TransferClass
	{
		public string VideoId { get; set; }
		public byte[] Data { get; set; }
		public string VideoName { get; set; }
	}
}