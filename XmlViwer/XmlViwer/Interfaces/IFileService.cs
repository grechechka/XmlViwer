using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlViwer.Interfaces
{
    public interface IFileService
    {
        string GetText(string filename);

        string Open();
    } 
}
