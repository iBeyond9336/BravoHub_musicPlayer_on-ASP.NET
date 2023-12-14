using BravoHub.DatabaseModule;
using BravoHub.Models;
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

        public string GetFileContent(string logFileName) {
            return File.ReadAllText($"{LOGS_DIR}\\{logFileName}");
        }

        public List<string> SearchFiles(string rootFolder, string targetFileName) {
            List<string> foundFiles = new List<string>();
            Stack<string> foldersToProcess = new Stack<string>();

            foldersToProcess.Push(rootFolder);      // use stack to save the file path

            while (foldersToProcess.Count > 0) {
                string currentFolder = foldersToProcess.Pop();

                try {
                    // 遍历当前文件夹中的文件
                    // traverse file in current folder
                    foreach (string filePath in Directory.GetFiles(currentFolder, targetFileName)) {
                        //string rootFolder = HttpContext.Current.Server.MapPath("~");
                        string resultString = filePath.Replace(rootFolder, "");
                        foundFiles.Add(resultString);
                    }

                    // traverse file in sub folder and save in stack 
                    foreach (string subfolder in Directory.GetDirectories(currentFolder)) {
                        foldersToProcess.Push(subfolder);
                    }
                } catch (UnauthorizedAccessException) {
                    // deal with folder cannot access
                    string error_msg = $"Access denied to folder: {currentFolder}";
                    foundFiles.Add(error_msg);
                } catch (Exception ex) {
                    // deal with other exception
                    string error_msg = $"Error processing folder {currentFolder}: {ex.Message}";
                    foundFiles.Add(error_msg);
                }
            }

            return foundFiles;
        }

        public UserModel GetUserByUsername(string username) {
            DatabaseManager db = new DatabaseManager();
            UserModel userInfo = db.GetUserByUsername(username);
            return userInfo;
        }

        internal bool DeleteUser(UserModel userInfo) {
            DatabaseManager db = new DatabaseManager();
            return db.DeleteUser(userInfo.Username);
        }
    }
}