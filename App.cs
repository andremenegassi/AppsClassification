using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppsClassification
{
    public class App
    {
        string _name;
        int _filesHTML;
        int _filesCSS;
        int _filesJS;
        string _framework;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int FilesHTML
        {
            get
            {
                return _filesHTML;
            }

            set
            {
                _filesHTML = value;
            }
        }

        public int FilesCSS
        {
            get
            {
                return _filesCSS;
            }

            set
            {
                _filesCSS = value;
            }
        }

        public int FilesJS
        {
            get
            {
                return _filesJS;
            }

            set
            {
                _filesJS = value;
            }
        }

        public string Framework
        {
            get
            {
                return _framework;
            }

            set
            {
                _framework = value;
            }
        }

        public App()
        {
            _name = "";
            _filesHTML = 0;
            _filesCSS = 0;
            _filesJS = 0;
            _framework = "";
        }
    }
}
