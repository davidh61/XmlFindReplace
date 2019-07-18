using Microsoft.Extensions.Configuration;
using System.IO;

namespace FindReplace.Interface
{
	public interface IFileService
	{
		void ParseFile(IConfigurationSection configSection, DirectoryInfo folderDir);
	}
}
