# XmlFindReplace

Xml attribute value replacer - built specifically for a TeamCity project import with mismatch of versions and the XML schemas, built for a specific use case but hoping to make more generic in future. 

Configuration is currently done in the `appsettings.json`, properties are defined below:
- `folderPath` The base folder path for the program to scan for your XML files
- `findText` The text that is being searched for
- `replaceText` The text that we are going to replace the findText for
- `elementName` The XML element that we are specifically looking to target, leave blank if you'd like to target all elements
- `attributeName` The attribute that we are looking to target

