using System;
using System.IO;
using System.Xml;

namespace XmlFindReplace
{
	public class Program
	{
		public static string FindText = "";
		public static string ReplaceText = "";
		public static string ElementName = "";
		public static string AttributeName = "";
		public static string XmlFileExtension = ".xml";

		static void Main(string[] args)
		{
			if (args.Length != 5)
			{
				Console.WriteLine("Need some parameters please..");
				Console.WriteLine("1) Folder location");
				Console.WriteLine("2) Text to search for");
				Console.WriteLine("3) Text to replace");
				Console.WriteLine("4) Element name");
				Console.WriteLine("5) Attribute name");
				return;
			}

			var folderPath = args[0];
			FindText = args[1];
			ReplaceText = args[2];
			ElementName = args[3];
			AttributeName = args[4];

			if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrEmpty(FindText))
			{
				var directory = new DirectoryInfo(folderPath);

				if (directory.Exists)
				{
					UpdateFiles(directory);
				}
			}

			return;
		}

		private static void UpdateFiles(DirectoryInfo directory)
		{
			var files = directory.GetFiles();

			foreach (var file in files)
			{
				if (file.Extension == XmlFileExtension)
				{
					UpdateFile(file.FullName);
				}
			}

			var directories = directory.GetDirectories();

			if (directories.Length > 0)
			{
				foreach (var dir in directories)
				{
					UpdateFiles(dir);
				}
			}
		}

		private static void UpdateFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				var xml = new XmlDocument();
				var isUpdated = false;
				try
				{
					xml.Load(filePath);

					var xmlElements = xml.GetElementsByTagName(ElementName);

					foreach (XmlElement xmlElement in xmlElements)
					{
						var xmlElementAttributeValue = xmlElement.GetAttribute(AttributeName);

						if (xmlElementAttributeValue != null && xmlElementAttributeValue == FindText)
						{
							xmlElement.SetAttribute("type", ReplaceText);
							isUpdated = true;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error attemting to parse XML document: {filePath}, {ex.Message}");
				}


				try
				{
					if (isUpdated)
					{
						xml.Save(filePath);
						Console.WriteLine($"File {filePath} has been modfied");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error attemting to save XML document: {filePath}, {ex.Message}");
				}


			}
		}

	}
}
