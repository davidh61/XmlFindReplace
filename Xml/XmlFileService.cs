using FindReplace.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Xml;

namespace FindReplace.Xml
{
	public class XmlFileService : IFileService
	{
		private static readonly string XmlFileExtension = ".xml";
		private static string FindText = "";
		private static string ReplaceText = "";
		private static string ElementName = "";
		private static string AttributeName = "";

		public void ParseFile(IConfigurationSection configSection, DirectoryInfo folderDir)
		{
			FindText = configSection.GetSection("findText").Value;
			ReplaceText = configSection.GetSection("replaceText").Value;
			ElementName = configSection.GetSection("elementName").Value;
			AttributeName = configSection.GetSection("attributeName").Value;

			if (folderDir.Exists)
			{
				UpdateFiles(folderDir);
			}
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
				var xmlFile = new XmlDocument();
				var isUpdated = false;

				try
				{
					xmlFile.Load(filePath);

					var xmlElements = xmlFile.GetElementsByTagName(ElementName);

					foreach (XmlElement xmlElement in xmlElements)
					{
						var xmlElementAttributeValue = xmlElement.GetAttribute(AttributeName);

						if (xmlElementAttributeValue != null && xmlElementAttributeValue == FindText)
						{
							xmlElement.SetAttribute(AttributeName, ReplaceText);
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
						xmlFile.Save(filePath);
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
