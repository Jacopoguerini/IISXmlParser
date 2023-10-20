using System.Xml;

namespace XmlParser.Xml
{
    public class Xml
    {
        public static XmlNodeList? GetXML(string nodesString, string xmlPath)
        {
            XmlNodeList? nodes = null;

            if (!string.IsNullOrWhiteSpace(nodesString))
            {
                XmlDocument doc = new();
                doc.Load(xmlPath);
                nodes = doc.DocumentElement?.SelectNodes(nodesString);
            }

            return nodes;
        }
    }
}