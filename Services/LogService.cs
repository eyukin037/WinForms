using System;
using System.IO;
namespace CalculatorMatrix.Services
{
    // Делегат для уведомления о новой записи в журнале
    public delegate void LogAddedHandler(string message);
    public static class LogService
    {
        // Файл лога
        private static string logFile;
        // Доступ к текущему файлу лога
        public static string LogFile
        {
            get { return logFile; }
        }
        // Событие
        public static event LogAddedHandler LogAdded;
        // Выбор файла для лога
        public static void SetLogFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Не указан файл лога.");
            logFile = fileName;
        }
        // Запись в лог
        public static void Write(string message)
        {
            if (string.IsNullOrEmpty(logFile)) throw new IOException("Не выбран файл лога.");
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message;
            File.AppendAllText(logFile, text + Environment.NewLine);
            LogAdded?.Invoke(text);
        }
    }
}