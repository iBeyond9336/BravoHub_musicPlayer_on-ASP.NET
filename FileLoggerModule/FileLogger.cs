using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BravoHub.FileLoggerModule {
    public enum MessageType {
        INFO,
        ERROR,
        WARNING,
        DEBUG,
    }

    public class FileLogger {
        private static FileLogger Instance = null;
        private static object _lock = new object();
        private readonly string dirPath;
        private string fileName;
        private int maxFileSize;

        private FileLogger() {
            string logPath = HttpContext.Current.Server.MapPath("~") +         // using app root path + relative path: I save log in “FileLoggerModule\logs”
                          WebConfigurationManager.AppSettings["logDirPath"];
            dirPath = Directory.CreateDirectory($"{logPath}").FullName;
            fileName = GenerateFileName();
            if (int.TryParse(WebConfigurationManager.AppSettings["maxFileSize"], out int size)) {
                maxFileSize = size;
            }
        }

        private static string GenerateFileName() {
            return DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";
        }

        public static FileLogger GetInstance() {
            lock(_lock) {
                if (Instance == null) {
                    Instance = new FileLogger();
                }

                return Instance;
            }
        }

        public void LogMessage(string message, MessageType msgType = MessageType.INFO) {
            lock(_lock) {
                string dateTimeFormatted = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bool isCreated = false;
                // check if the file size is almos the limit
                // if it is over the max size then create a new file, otherwise keep using the same file
                try {
                    byte[] fileBytes = File.ReadAllBytes($"{dirPath}\\{fileName}");
                    if (fileBytes.Length > maxFileSize) {
                        fileName = GenerateFileName();
                    }
                } catch (FileNotFoundException) {
                    // create the file
                    File.AppendAllText($"{dirPath}\\{fileName}", $"{dateTimeFormatted} [{msgType}]: {message}\n");
                    isCreated = true;
                } catch (Exception e) {
                    throw e;
                }

                // Log the message
                if(!isCreated) {
                    File.AppendAllText($"{dirPath}\\{fileName}", $"{dateTimeFormatted} [{msgType}]: {message}\n");
                }
            }
        }
    }
}