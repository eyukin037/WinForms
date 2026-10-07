using System;
using System.IO;
using System.Windows.Forms;
using CalculatorMatrix.Forms;
using CalculatorMatrix.Models;
using CalculatorMatrix.Services;
using CalculatorMatrix.Helpers;
namespace CalculatorMatrix
{
    public partial class Form1 : Form
    {
        private SaveData calculatorData = new SaveData();
        // Счётчики статистики вычислений
        private int totalOperations = 0;
        private int successfulOperations = 0;
        private int failedOperations = 0;
        public Form1()
        {
            InitializeComponent();
            // Подписка на событие добавления записи в лог
            LogService.LogAdded += AddLogToRichTextBox;
        }
        // Метод-обработчик нажатия кнопки "Выполнить". Выполняет выбранную операцию над матрицами.
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                btnExecute.Enabled = false;
                if (comboOperation.SelectedItem == null) throw new ArgumentException("Выберите операцию!");
                string operation = comboOperation.SelectedItem.ToString();
                // Увеличиваем количество операций
                totalOperations++;
                // Показываем выполнение операции
                await MatrixHelper.ShowProgress(progressBar);
                // Очищаем предыдущий результат
                calculatorData.MatrixC = null;
                calculatorData.Determinant = null;
                calculatorData.DeterminantName = null;
                // Сохраняем название операции
                calculatorData.Operation = operation;
                WriteLog("Выполнение операции: " + operation);
                switch (operation)
                {
                    case "A + B":
                        if (calculatorData.MatrixA == null || calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицы A и B.");
                        if (calculatorData.MatrixA.Rows != calculatorData.MatrixB.Rows || calculatorData.MatrixA.Columns != calculatorData.MatrixB.Columns)
                            throw new ArgumentException("Для сложения A + B матрицы должны иметь одинаковые размеры.");
                        calculatorData.MatrixC = Matrix.Add(calculatorData.MatrixA, calculatorData.MatrixB);
                        break;
                    case "A - B":
                        if (calculatorData.MatrixA == null || calculatorData.MatrixB == null)
                            throw new ArgumentException("Введите матрицы A и B.");
                        if (calculatorData.MatrixA.Rows != calculatorData.MatrixB.Rows || calculatorData.MatrixA.Columns != calculatorData.MatrixB.Columns)
                            throw new ArgumentException("Для вычитания A - B матрицы должны иметь одинаковые размеры.");
                        calculatorData.MatrixC = Matrix.SubtractAB(calculatorData.MatrixA, calculatorData.MatrixB);
                        break;
                    case "B - A":
                        if (calculatorData.MatrixA == null || calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицы A и B.");
                        if (calculatorData.MatrixA.Rows != calculatorData.MatrixB.Rows || calculatorData.MatrixA.Columns != calculatorData.MatrixB.Columns)
                            throw new ArgumentException("Для вычитания B - A матрицы должны иметь одинаковые размеры.");
                        calculatorData.MatrixC = Matrix.SubtractBA(calculatorData.MatrixA, calculatorData.MatrixB);
                        break;
                    case "A × B":
                        if (calculatorData.MatrixA == null || calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицы A и B.");
                        if (calculatorData.MatrixA.Columns != calculatorData.MatrixB.Rows) throw new ArgumentException("Для умножения A x B число столбцов матрицы A должно равняться числу строк матрицы B.");
                        calculatorData.MatrixC = Matrix.MultiplyAB(calculatorData.MatrixA, calculatorData.MatrixB);
                        break;
                    case "B × A":
                        if (calculatorData.MatrixA == null || calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицы A и B.");
                        if (calculatorData.MatrixB.Columns != calculatorData.MatrixA.Rows) throw new ArgumentException("Для умножения B x A число столбцов матрицы B должно равняться числу строк матрицы A.");
                        calculatorData.MatrixC = Matrix.MultiplyBA(calculatorData.MatrixA, calculatorData.MatrixB);
                        break;
                    case "det(A)":
                        if (calculatorData.MatrixA == null) throw new ArgumentException("Введите матрицу A.");
                        if (calculatorData.MatrixA.Rows != calculatorData.MatrixA.Columns) throw new ArgumentException("Нельзя найти определитель A: матрица должна быть квадратной.");
                        calculatorData.Determinant = Matrix.Determinant(calculatorData.MatrixA);
                        calculatorData.DeterminantName = "det(A)";
                        break;
                    case "det(B)":
                        if (calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицу B.");
                        if (calculatorData.MatrixB.Rows != calculatorData.MatrixB.Columns) throw new ArgumentException("Нельзя найти определитель B: матрица должна быть квадратной.");
                        calculatorData.Determinant = Matrix.Determinant(calculatorData.MatrixB);
                        calculatorData.DeterminantName = "det(B)";
                        break;
                    case "A⁻¹":
                        if (calculatorData.MatrixA == null) throw new ArgumentException("Введите матрицу A.");
                        if (calculatorData.MatrixA.Rows != calculatorData.MatrixA.Columns) throw new ArgumentException("Нельзя найти обратную матрицу A: матрица должна быть квадратной.");
                        double detA = Matrix.Determinant(calculatorData.MatrixA);
                        if (Math.Abs(detA) < 1e-9) throw new InvalidOperationException("Нельзя найти обратную матрицу A: det(A) = " + detA);
                        calculatorData.MatrixC = Matrix.Inverse(calculatorData.MatrixA);
                        break;
                    case "B⁻¹":
                        if (calculatorData.MatrixB == null) throw new ArgumentException("Введите матрицу B.");
                        if (calculatorData.MatrixB.Rows != calculatorData.MatrixB.Columns) throw new ArgumentException("Нельзя найти обратную матрицу B: матрица должна быть квадратной.");
                        double detB = Matrix.Determinant(calculatorData.MatrixB);
                        if (Math.Abs(detB) < 1e-9)  throw new InvalidOperationException("Нельзя найти обратную матрицу B: det(B) = " + detB);
                        calculatorData.MatrixC = Matrix.Inverse(calculatorData.MatrixB);
                        break;
                    default:
                        throw new ArgumentException("Неизвестная операция: " + operation);
                }
                // Увеличиваем количество успешных операций
                successfulOperations++;
                WriteLog("Операция выполнена успешно");
                btnResult_Click(null, EventArgs.Empty);
            }
            catch (ArgumentException ex)
            {
                // Ошибки проверки введённых данных
                failedOperations++;
                WriteLog("Ошибка валидации: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка ввода",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                // Ошибки выполнения математической операции
                failedOperations++;
                WriteLog("Математическая ошибка: " + ex.Message);
                MessageBox.Show(ex.Message,"Математическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                // Критическая ошибка: недостаточно памяти
                failedOperations++;
                WriteLog("Критическая ошибка: недостаточно памяти: " + ex.Message);
                MessageBox.Show("Недостаточно памяти для выполнения операции.\n" +
                    "Попробуйте использовать матрицы меньшего размера или перезапустите программу.",
                    "Недостаточно памяти", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Остальные непредвиденные ошибки
                failedOperations++;
                WriteLog("Непредвиденная ошибка: " + ex.Message);
                MessageBox.Show( "Произошла непредвиденная ошибка: " + ex.Message,"Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExecute.Enabled = true;
            }
        }
        // Добавление сообщения в журнал на форме
        private void AddLogToRichTextBox(string message)
        {
            rtbLog.AppendText(message + Environment.NewLine);
        }
        // Безопасная запись в лог
        private void WriteLog(string message)
        {
            try
            {
                LogService.Write(message);
            }
            catch (IOException ex)
            {
                rtbLog.AppendText("Не удалось записать в лог: " + ex.Message + Environment.NewLine);
            }
            catch (UnauthorizedAccessException ex)
            {
                rtbLog.AppendText("Нет доступа к файлу лога: " + ex.Message + Environment.NewLine);
            }
            catch (OutOfMemoryException ex)
            {
                rtbLog.AppendText("Недостаточно памяти при записи в лог: " +ex.Message + Environment.NewLine);
            }
            catch (NotSupportedException ex)
            {
                rtbLog.AppendText("Недопустимый путь к файлу лога: " +ex.Message + Environment.NewLine);
            }
            catch (ArgumentException ex)
            {
                rtbLog.AppendText("Некорректный путь к файлу лога: " +ex.Message +Environment.NewLine);
            }
            catch (Exception ex)
            {
                rtbLog.AppendText("Ошибка записи в лог: " +  ex.Message + Environment.NewLine);
            }
        }
        // Файл → Новая сессия
        private void новаяСессияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаём новое пустое состояние
            calculatorData = new SaveData();
            // Сбрасываем выбранную операцию
            comboOperation.SelectedIndex = -1;
            // Очищаем журнал на форме
            rtbLog.Clear();
            // Спрашиваем пользователя, нужно ли очистить файл лога
            DialogResult result = MessageBox.Show("Очистить файл журнала при создании новой сессии?", "Новая сессия", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(LogService.LogFile))
                {
                    try
                    {
                        File.WriteAllText(LogService.LogFile, "");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show("Некорректный путь к файлу журнала: " + ex.Message,"Ошибка пути",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("Нет доступа к файлу журнала: " + ex.Message,"Ошибка доступа",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    catch (NotSupportedException ex)
                    {
                        MessageBox.Show("Недопустимый путь к файлу журнала: " + ex.Message,"Ошибка формата",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Ошибка очистки файла журнала. Возможно, файл занят или диск недоступен: " + ex.Message, "Ошибка записи", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    catch (OutOfMemoryException ex)
                    {
                        MessageBox.Show("Недостаточно памяти при очистке файла журнала: " + ex.Message,"Ошибка памяти",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                else  MessageBox.Show("Файл журнала не выбран.","Новая сессия",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            // Сбрасываем ProgressBar
            progressBar.Value = 0;
            // Сбрасываем статистику
            totalOperations = 0;
            successfulOperations = 0;
            failedOperations = 0;
            MessageBox.Show("Новая сессия создана.","Новая сессия",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Файл → Выбрать файл лога
        private void выбратьФайлЛогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                dialog.Title = "Логирование";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LogService.SetLogFile(dialog.FileName);
                        MessageBox.Show("Файл для логирования выбран.","Логирование",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (OutOfMemoryException ex)
                    {
                        MessageBox.Show("Недостаточно памяти при выборе файла лога: " + ex.Message,"Ошибка памяти", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (NotSupportedException ex)
                    {
                        MessageBox.Show("Недопустимый путь к файлу лога: " + ex.Message,"Ошибка формата",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Непредвиденная ошибка при выборе файла лога: " + ex.Message);
                        MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,"Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }
        // Файл → Выход
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)   Close();
        }
        // Данные → Сохранить 
        private async void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (calculatorData.MatrixA == null && calculatorData.MatrixB == null && calculatorData.MatrixC == null && calculatorData.Determinant == null)
                {
                    WriteLog("Ошибка сохранения: нет данных для сохранения");
                    MessageBox.Show("Нет данных для сохранения", "Ошибка сохранения", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "JSON файлы (*.json)|*.json|" + "XML файлы (*.xml)|*.xml";
                saveDialog.Title = "Сохранить состояние";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await MatrixHelper.ShowProgress(progressBar);
                    FileService.Save(calculatorData, saveDialog.FileName);
                    WriteLog("Данные успешно сохранены: " + saveDialog.FileName);
                    MessageBox.Show("Сохранение выполнено","Информация",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                WriteLog("Ошибка данных при сохранении: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка данных", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteLog("Ошибка доступа при сохранении: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка доступа",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                WriteLog("Ошибка записи при сохранении: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка записи",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (NotSupportedException ex)
            {
                WriteLog("Ошибка формата при сохранении: " + ex.Message);
                MessageBox.Show( ex.Message, "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                WriteLog("Критическая ошибка: недостаточно памяти при сохранении: " + ex.Message);
                MessageBox.Show("Недостаточно памяти для сохранения данных.\n" +
                    "Попробуйте сохранить в другой формат или уменьшите размер матриц.",
                    "Недостаточно памяти", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                WriteLog("Непредвиденная ошибка при сохранении: " + ex.Message);
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Данные → Загрузить
        private async void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "JSON файлы (*.json)|*.json|" + "XML файлы (*.xml)|*.xml";
                openDialog.Title = "Загрузить состояние";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    await MatrixHelper.ShowProgress(progressBar);
                    calculatorData = FileService.Load(openDialog.FileName);
                    if (comboOperation.Items.Contains(calculatorData.Operation))
                    {
                        comboOperation.SelectedItem = calculatorData.Operation;
                    }
                    WriteLog("Данные успешно загружены");
                    MessageBox.Show("Загрузка выполнена","Информация",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FileNotFoundException ex)
            {
                WriteLog("Файл не найден: " + ex.Message);
                MessageBox.Show(ex.Message,"Файл не найден",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidDataException ex)
            {
                WriteLog("Ошибка данных файла: " + ex.Message);
                MessageBox.Show( ex.Message, "Ошибка данных",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteLog("Ошибка доступа при загрузке: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка доступа",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (NotSupportedException ex)
            {
                WriteLog("Ошибка формата при загрузке: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                WriteLog("Ошибка чтения при загрузке: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка чтения",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                WriteLog("Критическая ошибка: недостаточно памяти при загрузке: " + ex.Message);
                MessageBox.Show("Недостаточно памяти для загрузки данных.\n" +
                    "Файл слишком велик для доступной оперативной памяти.",
                    "Недостаточно памяти", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                WriteLog("Непредвиденная ошибка при загрузке: " + ex.Message);
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        // Данные → Очистить логи
        private void очиститьЛогиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Очистка лога";
                dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(dialog.FileName, "");
                        rtbLog.Clear();
                        MessageBox.Show("Лог очищен.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show("Некорректный путь к файлу лога: " + ex.Message,"Ошибка пути", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("Нет доступа к файлу лога: " + ex.Message,"Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (NotSupportedException ex)
                    {
                        MessageBox.Show("Недопустимый путь к файлу лога: " + ex.Message,"Ошибка формата", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Ошибка очистки файла лога: " + ex.Message, "Ошибка записи",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (OutOfMemoryException ex)
                    {
                        MessageBox.Show("Недостаточно памяти для очистки файла лога: " + ex.Message, "Ошибка памяти", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,"Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }
        // Отчёт → Статистика вычислений
        private void статистикаВычисленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statistics =
                "Всего операций: " + totalOperations + "\n" +
                "Успешных: " + successfulOperations + "\n" +
                "С ошибками: " + failedOperations;
            MessageBox.Show(statistics,"Статистика вычислений",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        // Справка → О программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Калькулятор матриц\n\n" +
                "Программа предназначена для выполнения операций над матрицами.\n\n" +
                "Доступные операции:\n" +
                "• сложение;\n" +
                "• вычитание;\n" +
                "• умножение;\n" +
                "• определитель;\n" +
                "• обратная матрица.\n\n",
                "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Кнопка "Матрица A"
        private void btnMatrixA_Click(object sender, EventArgs e)
        {
            try
            {
                MatrixForm form;
                // Если матрица уже существует — открываем её для редактирования
                if (calculatorData.MatrixA != null)
                {
                    form = new MatrixForm(calculatorData.MatrixA,"Изменение матрицы A", trkScale.Value,true, (int)trkDecimals.Value);
                }
                else
                {
                    // Если матрицы A нет — создаём новую
                    form = new MatrixForm("Матрица A", trkScale.Value);
                }
                // Если пользователь нажал ОК — сохраняем матрицу
                if (form.ShowDialog() == DialogResult.OK)
                {
                    calculatorData.MatrixA = form.ResultMatrix;
                    // Старый результат не актуален
                    calculatorData.MatrixC = null;
                    calculatorData.Determinant = null;
                    calculatorData.DeterminantName = null;
                }
            }
            catch (ArgumentException ex)
            {
                WriteLog("Ошибка при открытии матрицы A: " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                WriteLog("Недостаточно памяти при открытии матрицы A: " + ex.Message);
                MessageBox.Show( "Недостаточно памяти для открытия матрицы A.",
                    "Ошибка памяти", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                WriteLog("Непредвиденная ошибка при работе с матрицей A: " + ex.Message);
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        // Кнопка "Матрица B"
        private void btnMatrixB_Click(object sender, EventArgs e)
        {
            try
            {
                MatrixForm form;
                // Если матрица B существует — открываем её для редактирования
                if (calculatorData.MatrixB != null)
                {
                    form = new MatrixForm( calculatorData.MatrixB,"Изменение матрицы B", trkScale.Value,true,(int)trkDecimals.Value);
                }
                else
                {
                    // Если матрицы B нет — создаём новую
                    form = new MatrixForm("Матрица B", trkScale.Value);
                }
                // Если пользователь нажал ОК — сохраняем матрицу
                if (form.ShowDialog() == DialogResult.OK)
                {
                    calculatorData.MatrixB = form.ResultMatrix;
                    // Старый результат не актуален
                    calculatorData.MatrixC = null;
                    calculatorData.Determinant = null;
                    calculatorData.DeterminantName = null;
                }
            }
            catch (ArgumentException ex)
            {
                WriteLog("Ошибка при открытии матрицы B: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                WriteLog("Недостаточно памяти при открытии матрицы B: " + ex.Message);
                MessageBox.Show("Недостаточно памяти для открытия матрицы B.",
                    "Ошибка памяти",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                WriteLog("Непредвиденная ошибка при работе с матрицей B: " + ex.Message);
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message, "Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Кнопка "Результат"
        private void btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                // Если есть матричный результат
                if (calculatorData.MatrixC != null)
                {
                    MatrixForm form = new MatrixForm( calculatorData.MatrixC, "Результат",trkScale.Value, false, (int)trkDecimals.Value);
                    form.ShowDialog();
                    return;
                }
                // Если результатом является определитель
                if (calculatorData.Determinant.HasValue)
                {
                    MessageBox.Show(calculatorData.DeterminantName + " = " + calculatorData.Determinant.Value.ToString("F" + trkDecimals.Value), "Результат", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Результат отсутствует.","Информация", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                WriteLog("Ошибка диапазона при отображении результата: " + ex.Message);
                MessageBox.Show("Недопустимое значение параметров отображения результата.",
                    "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                WriteLog("Ошибка параметров при отображении результата: " + ex.Message);
                MessageBox.Show(ex.Message,"Ошибка отображения",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ex)
            {
                WriteLog("Недостаточно памяти при отображении результата: " + ex.Message);
                MessageBox.Show("Недостаточно памяти для отображения результата.",
                    "Ошибка памяти",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                WriteLog("Непредвиденная ошибка при отображении результата: " + ex.Message);
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}