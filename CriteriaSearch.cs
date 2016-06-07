using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppsClassification
{
    public class CriteriaSearch
    {
        string _framework;
        string _fileName;
        string[] _criterias;

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

        public string[] Criterias
        {
            get
            {
                return _criterias;
            }

            set
            {
                _criterias = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public CriteriaSearch(string framework, string fileName, string[] criterias)
        {
            this._framework = framework;
            this.FileName = fileName;
            this._criterias = criterias;
        }


    }
}
