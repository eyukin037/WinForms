using System;
using System.Windows.Forms;
using CalculatorMatrix.Services;
namespace CalculatorMatrix
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Глобальная обработка ошибок UI-потока
            Application.ThreadException += Application_ThreadException;
            // Глобальная обработка необработанных ошибок
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        // Обработка необработанных ошибок UI-потока
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Формируем сообщение в зависимости от типа ошибки
            string message = GetErrorMessage(e.Exception);
            try
            {
                LogService.Write("Глобальная ошибка: " + e.Exception.Message);
            }
            catch
            {
                // Ошибка логирования не должна вызвать ещё одну ошибку
            }
            MessageBox.Show( message,  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Формирование сообщения в зависимости от типа исключения
        private static string GetErrorMessage(Exception ex)
        {
            if (ex is OutOfMemoryException)
            {
                return "Недостаточно памяти: " + ex.Message;
            }
            if (ex is InvalidOperationException)
            {
                return "Недопустимая операция: " + ex.Message;
            }
            if (ex is ArgumentException)
            {
                return "Ошибка данных: " + ex.Message;
            }
            return "Произошла непредвиденная ошибка: " + ex.Message;
        }
         // Обработка необработанных ошибок приложения
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            try
            {
                if (ex != null)
                {
                    LogService.Write("Критическая ошибка: " + GetErrorMessage(ex));
                }
                else
                {
                    LogService.Write("Критическая ошибка: неизвестная ошибка.");
                }
            }
            catch
            {
                // Ошибка логирования не должна вызвать ещё одну ошибку
            }
        }
    }
}
