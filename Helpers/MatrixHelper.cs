using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalculatorMatrix.Models;
namespace CalculatorMatrix.Helpers
{
    public static class MatrixHelper
    {
        //ограничение размера матрицы
        private const int MaxMatrixSize = 50;
        // Метод проверки размера матрицы: на минимальный и максимальный размер
        public static void CheckSize(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)  throw new ArgumentException("Размеры матрицы должны быть больше нуля.");
            if (rows > MaxMatrixSize || columns > MaxMatrixSize) throw new ArgumentException("Слишком большой размер матрицы. Максимум 50x50.");
        }
        // Метод проверки индекса матрицы
        public static void CheckIndex(int row, int column, int rows, int columns)
        {
            if (row < 0 || row >= rows || column < 0 || column >= columns) throw new IndexOutOfRangeException("Индекс выходит за границы матрицы.");
        }
        // Создание таблицы
        public static void CreateTable(DataGridView dgvMatrix, int rows, int columns)
        {
            // Сохраняем старые значения
            string[,] oldData = new string[dgvMatrix.Rows.Count, dgvMatrix.Columns.Count];
            for (int i = 0; i < dgvMatrix.Rows.Count; i++)
            {
                for (int j = 0; j < dgvMatrix.Columns.Count; j++)
                {
                    if (dgvMatrix[j, i].Value != null) oldData[i, j] = dgvMatrix[j, i].Value.ToString();
                    else oldData[i, j] = "";
                }
            }
            // Очищаем старую таблицу
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();
            // Создаём столбцы
            for (int j = 0; j < columns; j++)
            {
                dgvMatrix.Columns.Add("Column" + j, "");
            }
            // Создаём строки
            for (int i = 0; i < rows; i++)
            {
                dgvMatrix.Rows.Add();
            }
            // Возвращаем старые значения
            int oldRows = Math.Min(rows, oldData.GetLength(0));
            int oldColumns = Math.Min(columns, oldData.GetLength(1));
            for (int i = 0; i < oldRows; i++)
            {
                for (int j = 0; j < oldColumns; j++)
                {
                    dgvMatrix[j, i].Value = oldData[i, j];
                }
            }
        }
        // Отображение готовой матрицы
        public static void ShowMatrix(DataGridView dgvMatrix,Matrix matrix,int decimals)
        {
            dgvMatrix.ReadOnly = true;
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();
            // Создаём столбцы
            for (int j = 0; j < matrix.Columns; j++)
            {
                dgvMatrix.Columns.Add("Column" + j, "");
            }
            // Создаём строки
            for (int i = 0; i < matrix.Rows; i++)
            {
                dgvMatrix.Rows.Add();
            }
            // Заполняем значения
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    dgvMatrix[j, i].Value = matrix[i, j].ToString("F" + decimals);
                }
            }
        }
        // Изменение размера шрифта таблицы
        public static void SetScale(DataGridView dgvMatrix, int fontSize)
        {
            dgvMatrix.DefaultCellStyle.Font = new Font(dgvMatrix.Font.FontFamily, fontSize);
        }
        // Метод отображения хода выполнения операции
        public static async Task ShowProgress(ProgressBar progressBar)
        {
            // Начальное значение
            progressBar.Value = 0;
            // Постепенно увеличиваем ProgressBar
            for (int i = 0; i <= 100; i += 10)
            {
                progressBar.Value = i;
                await Task.Delay(100);
            }
        }
    }
}