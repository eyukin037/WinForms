using System;
using CalculatorMatrix.Helpers;
namespace CalculatorMatrix.Models
{
    public class Matrix
    {
        public int Rows { get; set; } // строка
        public int Columns { get; set; } // столбец
        // Массив элементов матрицы
        public double[][] Data { get; set; }
        // Конструктор с параметрами
        public Matrix(int rows, int columns)
        {
            MatrixHelper.CheckSize(rows, columns);
            Rows = rows;
            Columns = columns;
            Data = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                Data[i] = new double[columns];
            }
        }
        // Конструктор по умолчанию
        public Matrix()
        {
            Rows = 0;
            Columns = 0;
            Data = new double[0][];
        }
        // Индексатор для доступа к элементам матрицы
        public double this[int row, int column]
        {
            get
            {
                MatrixHelper.CheckIndex(row, column, Rows, Columns);
                return Data[row][column];
            }
            set
            {
                MatrixHelper.CheckIndex(row, column, Rows, Columns);
                Data[row][column] = value;
            }
        }
        // Метод создания копии матрицы
        public Matrix Clone()
        {
            Matrix result = new Matrix(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    result[i, j] = this[i, j];
                }
            }
            return result;
        }
        // Математические операции над матрицами
        // A + B
        public static Matrix Add(Matrix a, Matrix b)
        {
            // Проверка одинаковых размеров
            if (a.Rows != b.Rows || a.Columns != b.Columns) throw new ArgumentException("Для сложения матрицы должны иметь одинаковые размеры.");
            // Создаём матрицу для результата
            Matrix result = new Matrix(a.Rows, a.Columns);
            // Перебираем строки матрицы A
            for (int i = 0; i < a.Rows; i++)
            {
                // Перебираем столбцы матрицы A
                for (int j = 0; j < a.Columns; j++)
                {
                    // Складываем соответствующие элементы матриц
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
            // Возвращаем полученную матрицу
            return result;
        }
        // A - B
        public static Matrix SubtractAB(Matrix a, Matrix b)
        {
            // Проверка одинаковых размеров
            if (a.Rows != b.Rows || a.Columns != b.Columns) throw new ArgumentException("Для вычитания матрицы должны иметь одинаковые размеры.");
            // Создаём матрицу для результата
            Matrix result = new Matrix(a.Rows, a.Columns);
            // Перебираем строки матрицы A
            for (int i = 0; i < a.Rows; i++)
            {
                // Перебираем столбцы матрицы A
                for (int j = 0; j < a.Columns; j++)
                {
                    // Вычитаем элементы матрицы B из матрицы A
                    result[i, j] = a[i, j] - b[i, j];
                }
            }
            // Возвращаем полученную матрицу
            return result;
        }
        // B - A
        public static Matrix SubtractBA(Matrix a, Matrix b)
        {
            return SubtractAB(b, a);
        }
        // A * B
        public static Matrix MultiplyAB(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows) throw new ArgumentException("Число столбцов матрицы A должно равняться числу строк матрицы B!");
            // Создаём матрицу результата
            // Количество строк берём у A,  количество столбцов — у B
            Matrix result = new Matrix(a.Rows, b.Columns);
            // Перебираем строки матрицы A
            for (int i = 0; i < a.Rows; i++)
            {
                // Перебираем столбцы матрицы B
                for (int j = 0; j < b.Columns; j++)
                {
                    // Переменная для накопления суммы произведений
                    double sum = 0;
                    // Перебираем элементы строки A и столбца B
                    for (int k = 0; k < a.Columns; k++)
                    {
                        // Умножаем соответствующие элементы  и добавляем их к сумме
                        sum += a[i, k] * b[k, j];
                    }
                    // Записываем полученную сумму в результат
                    result[i, j] = sum;
                }
            }
            // Возвращаем результат умножения
            return result;
        }
        // B * A
        public static Matrix MultiplyBA(Matrix a, Matrix b)
        {
            return MultiplyAB(b, a);
        }
        // Определитель матрицы. Метод Гаусса.
        public static double Determinant(Matrix m)
        {
            if (m.Rows != m.Columns) throw new ArgumentException("Определитель можно вычислить только для квадратной матрицы!");
            // Получаем размер квадратной матрицы
            int n = m.Rows;
            // Создаём копию матрицы, чтобы не изменять исходную
            Matrix temp = m.Clone();
            // Считаем количество перестановок строк
            int swapCount = 0;
            // Последовательно обрабатываем каждый столбец матрицы
            for (int i = 0; i < n; i++)
            {
                // Ищем строку с максимальным элементом в текущем столбце
                int maxRow = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(temp[j, i]) > Math.Abs(temp[maxRow, i])) maxRow = j;
                }
                // Если главный элемент равен нулю, определитель равен нулю
                if (Math.Abs(temp[maxRow, i]) < 1e-9) return 0;
                // Если поменять строки местами, знак определителя меняется
                if (maxRow != i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double value = temp[i, j];
                        temp[i, j] = temp[maxRow, j];
                        temp[maxRow, j] = value;
                    }
                    swapCount++;
                }
                // Обнуляем элементы ниже главного
                for (int j = i + 1; j < n; j++)
                {
                    double factor = temp[j, i] / temp[i, i];
                    for (int k = i; k < n; k++)
                    {
                        temp[j, k] -= factor * temp[i, k];
                    }
                }
            }
            // Начальное значение определителя
            double det = 1;
            // Определитель треугольной матрицы равен произведению элементов главной диагонали
            for (int i = 0; i < n; i++)
            {
                det *= temp[i, i];
            }
            // Если количество перестановок нечётное, меняем знак определителя
            if (swapCount % 2 != 0) det = -det;
            // Возвращаем определитель
            return det;
        }
        // Обратная матрица. Метод Гаусса–Жордана
        public static Matrix Inverse(Matrix m)
        {
            if (m.Rows != m.Columns) throw new ArgumentException("Нельзя найти обратную матрицу: матрица должна быть квадратной.");
            int n = m.Rows;
            // Создаём расширенную матрицу [A | E]
            double[,] temp = new double[n, n * 2];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Левая часть — исходная матрица
                    temp[i, j] = m[i, j];
                    // Правая часть — единичная матрица
                    temp[i, j + n] = (i == j) ? 1 : 0;
                }
            }
            // Приводим левую часть к единичной матрице
            for (int i = 0; i < n; i++)
            {
                // Ищем строку с максимальным элементом
                int maxRow = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(temp[j, i]) > Math.Abs(temp[maxRow, i])) maxRow = j;
                }
                // Меняем строки местами
                if (maxRow != i)
                {
                    for (int j = 0; j < n * 2; j++)
                    {
                        double value = temp[i, j];
                        temp[i, j] = temp[maxRow, j];
                        temp[maxRow, j] = value;
                    }
                }
                // Получаем главный элемент
                double mainElement = temp[i, i];
                // Проверяем главный элемент перед делением
                if (Math.Abs(mainElement) < 1e-9) throw new InvalidOperationException("Матрица вырождена, обратная матрица не существует.");
                // Делим всю строку на главный элемент
                for (int j = 0; j < n * 2; j++)
                {
                    temp[i, j] /= mainElement;
                }
                // Обнуляем элементы в текущем столбце во всех остальных строках
                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;
                    double factor = temp[j, i];
                    for (int k = 0; k < n * 2; k++)
                    {
                        temp[j, k] -= factor * temp[i, k];
                    }
                }
            }
            // Переносим правую часть в результирующую матрицу
            Matrix inverse = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inverse[i, j] = temp[i, j + n];
                }
            }
            // Возвращаем обратную матрицу
            return inverse;
        }
    }
}
