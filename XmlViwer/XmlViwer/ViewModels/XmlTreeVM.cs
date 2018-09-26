using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Xml.XPath;
using Prism.Mvvm;
using XmlViwer.Models;

namespace XmlViwer.ViewModels
{
    public class XmlTreeVM : BindableBase
    {
        private XmlDocumentModel _documentModel;

        public XmlDataProvider Provider { get; set; }

        public XmlDocumentModel XmlDocument
        {
            get => _documentModel;
            set
            {
                _documentModel = value;
                Provider.Document = value.XmlDataDocument;
            }
        }

        public XmlTreeVM()
        {
            Provider = new XmlDataProvider();
        }

        public bool LoadDocument(string pathToDoc)
        {
            XmlDocument = new XmlDocumentModel(pathToDoc);
            return true;
        }

        public void FilterByXPath(string xPath)
        {
            Provider.Document = XmlDocument.FilterXml(xPath);
        }
    }
}