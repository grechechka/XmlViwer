using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Mvvm;
using XmlViwer.Annotations;
using XmlViwer.Interfaces;
using XmlViwer.Services;
using Prism.Commands;
using XmlViwer.Models;

namespace XmlViwer.ViewModels
{
    public class MainVM : BindableBase
    {
        private IFileService _fileService;

        private string _filePath;

        private string _xPath;

        private bool _fileIsOppened;

        public XmlTreeVM FullXmlTreeVM { get; set; }
        public XmlTreeVM FilteredXmlTreeVM { get; set; }

        public MainVM()
        {
            _fileService = new FileService();
            FullXmlTreeVM = new XmlTreeVM();
            FilteredXmlTreeVM = new XmlTreeVM();
            OpenFile = new DelegateCommand(() =>
            {
                var pathToFile = _fileService.Open();
                FilePath = pathToFile;
                _fileIsOppened = FullXmlTreeVM.LoadDocument(pathToFile);
                _fileIsOppened = _fileIsOppened && FilteredXmlTreeVM.LoadDocument(pathToFile);
            });
            FilterByXPath = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(XPath) || !_fileIsOppened)
                    return;
                FilteredXmlTreeVM.FilterByXPath(XPath);
            });
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public string XPath
        {
            get => _xPath;
            set
            {
                _xPath = value;
                OnPropertyChanged(nameof(XPath));
            }
        }

        public DelegateCommand OpenFile { get; }

        public DelegateCommand FilterByXPath { get; }
    }
}