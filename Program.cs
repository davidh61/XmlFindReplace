using FindReplace.Xml;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FindReplace
{
	public class Program
	{
		public static string FindText = "";
		public static string ReplaceText = "";
		public static string ElementName = "";
		public static string AttributeName = "";

		static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			if (config != null)
			{
				var folderPath = config.GetSection("folderPath").Value;
				var xmlConfig = config.GetSection("xml");

				if (!string.IsNullOrEmpty(folderPath))
				{

					if (xmlConfig.Exists())
					{
						var directory = new DirectoryInfo(folderPath);
						var xmlFileService = new XmlFileService();

						xmlFileService.ParseFile(xmlConfig, directory);

						return;
					}
				}

				Console.WriteLine("Configuration file is not valid");
				return;
			}

			Console.WriteLine("No configuration file present");
			return;
		}
	}
}
