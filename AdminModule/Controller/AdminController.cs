using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BravoHub.AdminModule.Controller {
    public class AdminController {
        private string LOGS_DIR = HttpContext.Current.Server.MapPath("~") + WebConfigurationManager.AppSettings["logDirPath"];

        public List<string> GetLogFileNames() {
            string[] filePaths = Directory.GetFiles(LOGS_DIR);

            for(int i = 0; i < filePaths.Length; i++) { 
                string[] pathParts = filePaths[i].Split('\\');
                filePaths[i] = pathParts[pathParts.Length - 1];
            }


            return filePaths.ToList();
        }

        internal string GetFileContent(string logFileName) {
            return File.ReadAllText($"{LOGS_DIR}\\{logFileName}");
        }
    }
}