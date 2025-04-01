using AdPlatforms.Services;

namespace AdPlatformTests
{
    public class AdPlatformServiceTests
    {
        private AdPlatformService _adPlatformService;
        private string _tempFilePath;

        [SetUp]
        public void Setup()
        {
            _adPlatformService = new AdPlatformService();

            // Создаем временный файл для тестов
            _tempFilePath = Path.Combine(Path.GetTempPath(), "test_data.txt");
        }

        [Test]
        public void LoadDataFromFile_ValidFile_DataLoadedSuccessfully()
        {
            // Подготовка: создаем временный файл с данными
            var fileContent = "Platform1: /mos, /spb\nPlatform2: /mos\n";
            File.WriteAllText(_tempFilePath, fileContent);

            // Выполнение метода
            _adPlatformService.LoadDataFromFile(_tempFilePath);

            // Проверка: убеждаемся, что данные загружены в память
            var platforms = _adPlatformService.SearchByLocation("/mos");
            Assert.AreEqual(2, platforms.Count, "Площадки не были загружены корректно.");

            // Очистка: удаляем временный файл
            File.Delete(_tempFilePath);
        }

        [Test]
        public void SearchByLocation_ValidLocation_ReturnsCorrectPlatforms()
        {
            // Подготовка: создаем временный файл с данными
            var fileContent = "Platform1: /mos, /spb\nPlatform2: /mos\n";
            File.WriteAllText(_tempFilePath, fileContent);

            // Выполнение метода загрузки данных из файла
            _adPlatformService.LoadDataFromFile(_tempFilePath);

            // Выполнение метода поиска по локации
            var platforms = _adPlatformService.SearchByLocation("/mos");

            // Проверка: убеждаемся, что найдены правильные площадки
            Assert.AreEqual(2, platforms.Count, "Неверное количество рекламных площадок.");
            Assert.IsTrue(platforms.Exists(p => p.Name == "Platform1"), "Площадка Platform1 не найдена.");
            Assert.IsTrue(platforms.Exists(p => p.Name == "Platform2"), "Площадка Platform2 не найдена.");

            // Очистка: удаляем временный файл
            File.Delete(_tempFilePath);
        }
    }
}