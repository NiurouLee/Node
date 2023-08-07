using System;
using System.IO;

namespace Config
{
    public class CSExporter
    {
        public string GenClass(string inExcelPath)
        {
            var extension = Path.GetExtension(inExcelPath);
            if (extension != ".xls" || extension != ".xlsx")
            {
                throw new Exception("unSupport excel type " + extension);
            }

            return string.Empty;
        }
    }
}