using System.Xml;
using File.WriteFile;
using XmlParser.Xml;

Console.WriteLine("Start Program");

string iisSiteNodePath = "/configuration/system.applicationHost/sites/site";
string xmlFilePath = @"./Data/Web6applicationHost.xml";
XmlNodeList? sites = Xml.GetXML(iisSiteNodePath, xmlFilePath);

string? serverName = Path.GetFileName(xmlFilePath).Split(new string[] { "applicationHost.xml" }, StringSplitOptions.None).FirstOrDefault();

List<TableRow>? tableRows = new();

if (sites != null && sites.Count > 0)
{
    string siteName = string.Empty;
    string physicalPath = string.Empty;

    int count = 1;
    foreach (XmlNode site in sites)
    {
        string bindingInformation = string.Empty;
        if (site != null)
        {
            XmlAttribute? siteNameAttribute = site.Attributes?["name"];
            if (siteNameAttribute != null)
            {
                siteName = siteNameAttribute.Value;
            }

            XmlNode? virtualDirectoryNode = site.SelectSingleNode("application/virtualDirectory");
            if (virtualDirectoryNode != null)
            {
                XmlAttribute? physicalPathAttribute = virtualDirectoryNode.Attributes?["physicalPath"];
                if (physicalPathAttribute != null)
                {
                    physicalPath = physicalPathAttribute.Value;
                }
            }

            XmlNode? bindingsNode = site.SelectSingleNode("bindings");
            if (bindingsNode != null && bindingsNode.ChildNodes.Count > 0)
            {
                foreach (XmlNode? binding in bindingsNode.ChildNodes)
                {
                    if (binding != null)
                    {
                        XmlAttribute? bindingInformationAttribute = binding.Attributes?["bindingInformation"];
                        if (bindingInformationAttribute != null)
                        {
                            if (string.IsNullOrWhiteSpace(bindingInformation))
                            {
                                bindingInformation = bindingInformationAttribute.Value;
                            }
                            else
                            {
                                bindingInformation += $"~{bindingInformationAttribute.Value}";
                            }
                        }
                    }
                }
            }
        }

        tableRows.Add(new TableRow
        {
            Count = count,
            SiteName = siteName,
            Path = physicalPath,
            Bindings = bindingInformation
        });
        count++;
    }

    string tableText = string.Join(";", "Count", "Server", "Nome sito", "Percorso", "Bindings");
    tableText += Environment.NewLine;

    foreach (TableRow row in tableRows)
    {
        tableText += string.Join(";", row.Count.ToString(), serverName, row.SiteName, row.Path, row.Bindings);
        tableText += Environment.NewLine;
    }

    WriteFile.WriteCsv(serverName!, tableText);
}

Console.WriteLine("End Program");