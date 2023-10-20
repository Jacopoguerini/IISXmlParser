using System.Text;

namespace File.WriteFile
{
    public class WriteFile
    {
        public static void WriteCsv(string fileName, string content, bool append = true)
        {
            var filePathTemp = $@"./Result/{fileName}.csv";

            Encoding utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

            using var file = new StreamWriter(filePathTemp, append, utf8NoBom);
            file.WriteLine(content);
            file.Flush();
            file.Close();
        }
    }
}