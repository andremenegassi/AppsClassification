using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppsClassification
{
    public class Classification
    {
        DirectoryInfo _dirContainer;
        DirectoryInfo[] _appsRepository;

        //critério para seleção/classificação do framework híbrido.
        List<CriteriaSearch> _criteriasSearch = new List<CriteriaSearch>()
        {
            // exemplo 
            //new CriteriaSearch("nome do framework", "arquivo a ser localizado", new string[] {"string para busca 1 no arquivo", "string para busca 2 no arquivo" }),

            new CriteriaSearch("Apache Cordova", "config.xml", new string[] {"cordova", "org.apache.cordova" }),
            new CriteriaSearch("Apache Cordova", "CordovaActivity.class", new string[] { }),
            new CriteriaSearch("Apache Cordova", "CordovaActivity.java", new string[] { }),
            new CriteriaSearch("Apache Cordova", "*.*", new string[] {"cordova" }),

            new CriteriaSearch("Phonegap", "config.xml", new string[] {"phonegap"}),
            new CriteriaSearch("Phonegap", "CordovaActivity.class", new string[] { }),
            new CriteriaSearch("Phonegap", "CordovaActivity.java", new string[] { }),
            new CriteriaSearch("Phonegap", "*.*", new string[] {"phonegap"}),

            new CriteriaSearch("Enyo", "*.*", new string[] {"enyo.machine", "enyo.kind" }),

            new CriteriaSearch("IBM Worklight", "config.xml", new string[] { "com.worklight.androidgap" }),
            new CriteriaSearch("IBM Worklight", "WLDroidGap.class", new string[] { }),
            new CriteriaSearch("IBM Worklight", "WLDroidGap.java", new string[] { }),

            new CriteriaSearch("IUI", "IUI.class", new string[] { }),
            new CriteriaSearch("IUI", "IUI.java", new string[] { }),

            new CriteriaSearch("Kivy", "AndroidManifest.xml", new string[] {"PythonActivity" }),

            new CriteriaSearch("Mobl", "MoblGap.class", new string[] { }),
            new CriteriaSearch("Mobl", "MoblGap.java", new string[] { }),
            new CriteriaSearch("Mobl", "*.mobl", new string[] { }),

            new CriteriaSearch("MoSync", "AndroidManifest.xml", new string[] {"MoSyncService" }),

            new CriteriaSearch("Next", "NextWebApp.class", new string[] { "nextwebapp" }),
            new CriteriaSearch("Next", "NextWebApp.java", new string[] { "nextwebapp" }),

            new CriteriaSearch("Quick Connect", "*.*", new string[] {"QCNativeFooter", "qc.handleError" }),

            new CriteriaSearch("Rho Mobile", "rho.dat", new string[] { }),

            new CriteriaSearch("Sencha", "*.js", new string[] {"Ext.create", "Ext.application" }),

            new CriteriaSearch("Titanium", "TitaniumModule.class", new string[] { }),
            new CriteriaSearch("Titanium", "TiActivity.class", new string[] { }),
            new CriteriaSearch("Titanium", "TitaniumModule.java", new string[] { }),
            new CriteriaSearch("Titanium", "TiActivity.java", new string[] { }),
            new CriteriaSearch("Titanium", "tiapp.xml", new string[] { }),
            new CriteriaSearch("Titanium", "*.*", new string[] {"appcelerator" }),


            new CriteriaSearch("USE WEBVIEW", "*.*", new string[] {"android.webkit.WebView", "android.permission.INTERNET" }), //, "webview", "web_view", "NATIVE_APP"

        };


        public Classification()
        {
            _dirContainer = new DirectoryInfo(@"C:\apps");
            _appsRepository = _dirContainer.GetDirectories();
        }

        public void Run()
        {
            List<DirectoryInfo> repositoryFilter1 = new List<DirectoryInfo>();
            List<App> apps = new List<App>();

            int i = 1;
            //percorrendo o diretório contendo os repositórios das apps.
            foreach (DirectoryInfo appRep in _appsRepository)
            {
                Console.Write("\n" + i + "  " + appRep.Name);

               
                App app = new App();
                app.Name = appRep.Name;
                app.FilesCSS = appRep.GetFiles("*.css", SearchOption.AllDirectories).Count();
                app.FilesHTML = appRep.GetFiles("*.html", SearchOption.AllDirectories).Count();
                app.FilesJS = appRep.GetFiles("*.js", SearchOption.AllDirectories).Count();

                //para cada app/repositório é aplicado o critério de pesquisa referente ao nome do arquivo
                foreach (CriteriaSearch criteria in _criteriasSearch)
                {
                    criteria.FileName = criteria.FileName.ToLower();

                    //filtrando pelos arquivos do critério 
                    var filesFiltered = from f in appRep.GetFiles(criteria.FileName, SearchOption.AllDirectories)
                                        select f;

                    bool findFile = false;
                    bool findCriteria = false;

                    if (filesFiltered.Count() > 0)
                    {
                        findFile = true;
                        int findCriteriaCount = 0;
                        foreach (FileInfo f in filesFiltered)
                        {

                            //abrindo os arquivos filtrados e aplicando o segundo critério (busca por string dentro dos arquivos)
                            try
                            {
                                string lines = "";
                                using (StreamReader reader = f.OpenText())
                                {
                                    lines = reader.ReadToEnd();
                                }
                                lines = lines.ToLower();
                                //int findCriteriaCount = 0;
                                foreach (string c in criteria.Criterias)
                                {
                                    if (lines.Contains(c.Trim().ToLower()))
                                    {
                                        findCriteriaCount++;
                                    }
                                }

                                //findCriteria = findCriteriaCount > 0 && (app.FilesCSS + app.FilesHTML + app.FilesJS) > 0;

                                ////findCriteria = findCriteriaCount == criteria.Criterias.Length;
                                //if (findCriteria)
                                //{
                                //    app.Framework += " - " + criteria.Framework;
                                //    //break;
                                //}
                            }
                            catch (Exception ex) {

                                Console.WriteLine(appRep.Name + "--> ERROR: " + ex.Message);
                            }
                        }


                        findCriteria = findCriteriaCount > 0 && (app.FilesCSS + app.FilesHTML + app.FilesJS) > 0;

                        //findCriteria = findCriteriaCount == criteria.Criterias.Length;
                        if (findCriteria)
                        {
                            app.Framework += " - " + criteria.Framework;
                            //break;
                        }
                    }

               
                   
                    if (findFile && findCriteria) //é híbrido
                    {
                        Console.Write(" ---------------------> Hybrid");
                        //app.Framework = criteria.Framework;
                        break;
                    }
                    else
                    {
                        app.Framework = "No hybrid";
                    }


                }

                apps.Add(app);
                SalveClassification(apps);

                i++;
            }
        }

        public void SalveClassification(List<App> apps)
        {
            string pathFileSave = @"c:\lixos\apps.json";
            TextWriter tw = new StreamWriter(pathFileSave, false);
            tw.WriteLine(JsonConvert.SerializeObject(apps));
            tw.Close();
        }

    }
}
