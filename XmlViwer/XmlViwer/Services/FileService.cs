using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlViwer.Interfaces;

namespace XmlViwer.Services
{
    public class FileService: IFileService
    {
        public string GetText(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);
            }
            else
            {
                return string.Empty;
            }
        }

        public string Open()
        {
            string filename = string.Empty;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = string.Format("Xml Documents ({0})|*{0}", ".xml");

            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName;
            }

            return filename;
        }
    }
}