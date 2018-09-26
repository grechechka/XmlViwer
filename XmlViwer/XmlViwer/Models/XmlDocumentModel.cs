using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace XmlViwer.Models
{
    public class XmlDocumentModel
    {
        private XmlDocument _xmldocument;

        public XmlDocument XmlDataDocument
        {
            get => _xmldocument;
            set => _xmldocument = value;
        }

        public XmlDocumentModel()
        {
            _xmldocument = new XmlDocument();
        }

        public XmlDocumentModel(string pathToDocument)
        {
            _xmldocument = new XmlDocument();
            _xmldocument.Load(pathToDocument);
        }

        public XmlDocumentModel(XmlDocument xmlDocument)
        {
            _xmldocument = xmlDocument;
        }

        public XmlDocument FilterXml(string xPath)
        {
            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentNullException(nameof(xPath), "Argument can't be null or empty");
            if (_xmldocument == null)
                throw new NullReferenceException($"{nameof(XmlDocument)} is not initialized");
            XPathNavigator _docNavigator = _xmldocument.CreateNavigator();
            try
            {
                XPathExpression query = _docNavigator.Compile(xPath);
                var rez1 = _docNavigator.Evaluate(query);
                var rDoc = PreparenavigatorResult(rez1);
                return rDoc;
            }
            catch (XPathException)
            {
                return GetWrongXPathDocument();
            }
        }

        private static XmlDocument GetWrongXPathDocument()
        {
            var doc = new XmlDocument();
            var rootNode = doc.AppendChild(doc.CreateNode(XmlNodeType.Element, "Root", ""));
            var rezNode = rootNode.AppendChild(doc.CreateNode(XmlNodeType.Element, "Rezult", ""));
            rezNode.AppendChild(doc.CreateTextNode("WrongXPath"));
            return doc;
        }

        private static XmlDocument PreparenavigatorResult(object result)
        {
            var doc = new XmlDocument();
            var rootNode = doc.AppendChild(doc.CreateNode(XmlNodeType.Element, "Root", ""));
            var rezNode = rootNode.AppendChild(doc.CreateNode(XmlNodeType.Element, "Rezult", ""));


            if (result is bool)
            {
                var attr = doc.CreateAttribute("BoolValue");
                attr.Value = result.ToString();
                rezNode.Attributes.Append(attr);
            }
            else if (result is double)
            {
                var attr = doc.CreateAttribute("DoubleValue");
                attr.Value = result.ToString();
                rezNode.Attributes.Append(attr);
            }
            else if (result is string)
            {
                var attr = doc.CreateAttribute("StringValue");
                attr.Value = result.ToString();
                rezNode.Attributes.Append(attr);
            }
            else
            {
                XPathNavigator navigatorNode = rezNode.CreateNavigator();
                XPathNodeIterator iterator = (XPathNodeIterator) result;
                if (iterator.Count < 1)
                {
                    rezNode.AppendChild(doc.CreateTextNode("EmptyResult"));
                }

                foreach (XPathNavigator val in iterator)
                {
                    navigatorNode.AppendChild(val.Clone());
                }
            }
            return doc;
        }
    }
}