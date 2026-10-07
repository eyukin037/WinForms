using System;
using System.Windows.Forms;
using CalculatorMatrix.Models;
using CalculatorMatrix.Helpers;
namespace CalculatorMatrix.Forms
{
    public partial class MatrixForm : Form
    {
        // Результат, который возвращается в Form1
        public Matrix ResultMatrix { get; private set; }
        // Определяет режим формы
        private bool inputMode;
        // Конструктор для ввода матрицы
        public MatrixForm(string title, int fontSize)
        {
            InitializeComponent();
            textBoxRows.Leave += textBoxRows_Leave;
            textBoxCols.Leave += textBoxCols_Leave;
            inputMode = true;
            Text = title;
            MatrixHelper.SetScale(dgvMatrix, fontSize);
        }
        // Конструктор для существующей матрицы:
        // true - ввод или редактирование матрицы
        // false - только просмотр результата
        public MatrixForm(Matrix matrix, string title, int fontSize,bool inputMode,int decimals)
        {
            InitializeComponent();
            this.inputMode = inputMode;
            Text = title;
            if (inputMode)
            {
                // Режим редактирования
                textBoxRows.Text = matrix.Rows.ToString();
                textBoxCols.Text = matrix.Columns.ToString();
                MatrixHelper.CreateTable(dgvMatrix,matrix.Rows, matrix.Columns);
                textBoxRows.Leave += textBoxRows_Leave;
                textBoxCols.Leave += textBoxCols_Leave;
                // Заполняем таблицу существующими значениями
                for (int i = 0; i < matrix.Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        dgvMatrix[j, i].Value = matrix[i, j];
                    }
                }
            }
            else
            {
                // Режим просмотра результата
                labelRows.Visible = false;
                labelCols.Visible = false;
                textBoxRows.Visible = false;
                textBoxCols.Visible = false;
                MatrixHelper.ShowMatrix(dgvMatrix,matrix, decimals);
                btnOK.Visible = false;
                btnCancel.Visible = false;
            }
            MatrixHelper.SetScale(dgvMatrix, fontSize);
        }
        // Изменение размера таблицы
        private void UpdateTableSize()
        {
            if (int.TryParse(textBoxRows.Text, out int rows) && int.TryParse(textBoxCols.Text, out int columns))
            {
                try
                {
                    MatrixHelper.CheckSize(rows, columns);
                    MatrixHelper.CreateTable(dgvMatrix,rows, columns);
                }
                catch (ArgumentException) {
                    // Некорректный размер будет проверен при нажатии ОК
                }
            }
        }
        // Изменение количества строк после завершения ввода
        private void textBoxRows_Leave(object sender, EventArgs e)
        {
            UpdateTableSize();
        }
        // Изменение количества столбцов после завершения ввода
        private void textBoxCols_Leave(object sender, EventArgs e)
        {
            UpdateTableSize();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            // В режиме просмотра просто закрываем окно
            if (!inputMode)
            {
                Close();
                return;
            }
            try
            {
                // Получаем количество строк
                if (!int.TryParse(textBoxRows.Text, out int rows))
                {
                    throw new ArgumentException("Количество строк должно быть целым числом от 1 до 50!");
                }
                // Получаем количество столбцов
                if (!int.TryParse(textBoxCols.Text, out int columns))
                {
                    throw new ArgumentException("Количество столбцов должно быть целым числом от 1 до 50!");
                }
                // Проверяем размеры
                MatrixHelper.CheckSize(rows, columns);
                // Создаём матрицу
                Matrix matrix = new Matrix(rows, columns);
                // Проверяем и считываем ячейки
                for (int i = 0; i < matrix.Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        if (dgvMatrix[j, i].Value == null || string.IsNullOrWhiteSpace(dgvMatrix[j, i].Value.ToString()))
                        {
                            throw new ArgumentException("Все ячейки матрицы должны быть заполнены.");
                        }
                        if (!double.TryParse(dgvMatrix[j, i].Value.ToString(), out double value))
                        {
                            throw new ArgumentException("Матрица содержит некорректные символы!");
                        }
                        matrix[i, j] = value;
                    }
                }
                ResultMatrix = matrix;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show( ex.Message,"Ошибка ввода",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Недостаточно памяти для создания матрицы такого размера. Уменьшите количество строк или столбцов.", "Ошибка памяти", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,"Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}