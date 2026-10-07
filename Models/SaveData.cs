namespace CalculatorMatrix.Models
{
    // Класс данных для сериализации
    public class SaveData
    {
        public Matrix MatrixA { get; set; }
        public Matrix MatrixB { get; set; }
        public Matrix MatrixC { get; set; }
        public double? Determinant { get; set; }
        public string DeterminantName { get; set; }
        public string Operation { get; set; }
    }
}
