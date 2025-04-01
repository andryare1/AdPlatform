// Сервис для работы с рекламными площадками
// Все данные хранятся в оперативной памяти в коллекции (in-memory)
using AdPlatforms.Models;

namespace AdPlatforms.Services
{
    public class AdPlatformService
    {
        // Словарь для хранения рекламных площадок по локациям
        private readonly Dictionary<string, List<AdPlatform>> _adsByLocation = new();

        // Метод для загрузки данных из файла
        // Перезаписывает все данные в памяти
        public void LoadDataFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл с рекламными площадками не найден.");
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Разделяем строку на имя платформы и локации
                var parts = line.Split(':', 2);
                if (parts.Length < 2) continue;

                string name = parts[0].Trim();
                var locations = parts[1].Split(',').Select(l => l.Trim()).ToList();

                // Создаем объект рекламной площадки
                var platform = new AdPlatform(name, locations);

                // Добавляем платформу в коллекцию по каждой локации
                foreach (var location in locations)
                {
                    if (!_adsByLocation.ContainsKey(location))
                    {
                        _adsByLocation[location] = new List<AdPlatform>();
                    }
                    _adsByLocation[location].Add(platform);
                }
            }
        }

        // Метод для поиска рекламных площадок по локации
        public List<AdPlatform> SearchByLocation(string location)
        {
            var result = new List<AdPlatform>();

            // Перебор всех локаций и добавление площадок, соответствующих локации
            foreach (var kvp in _adsByLocation)
            {
                // Искать точные совпадения или вложенные локации
                if (kvp.Key.Equals(location) || kvp.Key.StartsWith(location + "/"))
                {
                    result.AddRange(kvp.Value);
                }
            }
            // Убираем дубликаты площадок
            return result.Distinct().ToList();
        }
    }
}