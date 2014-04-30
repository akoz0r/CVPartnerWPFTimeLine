using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVGuru
{
    public static class IOService
    {
        public static void Save(string contents, string filePath)
        {
            System.IO.File.WriteAllText(filePath, contents);
        }

        public static string Load(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
