using System.IO;

namespace projectbd
{
    internal class ExcelPackage
    {
        public ExcelPackage(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; }
    }
}