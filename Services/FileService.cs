using System;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;
using CalculatorMatrix.Models;
namespace CalculatorMatrix.Services
{
    public static class FileService
    {
        // Сохранение
        public static void Save(SaveData data, string fileName)
        {
            if (data == null)  throw new ArgumentException("Нет данных для сохранения.");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Не указано имя файла.");
            string extension = Path.GetExtension(fileName).ToLower();
            if (extension == ".json")
            {
                string json = JsonSerializer.Serialize(data,new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
            else if (extension == ".xml")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                using (FileStream fs =  new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(fs, data);
                }
            }
            else  throw new NotSupportedException("Неверный формат файла.");
        }
        // Загрузка
        public static SaveData Load(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("Файл не найден.");
            if (new FileInfo(fileName).Length == 0) throw new InvalidDataException("Файл пустой или повреждён.");
            string extension = Path.GetExtension(fileName).ToLower();
            SaveData data;
            // JSON
            if (extension == ".json")
            {
                try
                {
                    string json = File.ReadAllText(fileName);
                    data = JsonSerializer.Deserialize<SaveData>(json);
                    if (data == null) throw new InvalidDataException("JSON файл не содержит данных.");
                }
                catch (JsonException)
                {
                    throw new InvalidDataException("Ошибка чтения JSON файла. Файл повреждён или имеет неверный формат.");
                }
            }
            // XML
            else if (extension == ".xml")
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    using (FileStream fs =new FileStream(fileName, FileMode.Open))
                    {
                        data = (SaveData)serializer.Deserialize(fs);
                    }
                    if (data == null) throw new InvalidDataException("XML файл не содержит данных.");
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidDataException("Ошибка чтения XML файла. Файл повреждён или имеет неверный формат.");
                }
            }
            else throw new NotSupportedException("Неверный формат файла.");
            if (data == null) throw new InvalidDataException("Файл не содержит данных.");
            if (data.MatrixA == null && data.MatrixB == null &&  data.MatrixC == null && data.Determinant == null && string.IsNullOrEmpty(data.Operation))
            {
                throw new InvalidDataException("Файл не содержит данных калькулятора.");
            }
            return data;
        }
    }
}