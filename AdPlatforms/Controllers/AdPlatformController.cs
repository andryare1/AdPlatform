// Контроллер для работы с рекламными площадками
// Содержит два метода: загрузка данных из файла и поиск площадок по локации.
using AdPlatforms.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatforms.Controllers
{
    [Route("api/ads")]
    [ApiController]
    public class AdPlatformController : ControllerBase
    {
        // Сервис для работы с рекламными площадками (интерфейс для манипуляции данными)
        private readonly AdPlatformService _adPlatformService;

        // Конструктор контроллера, инжектит зависимость AdPlatformService
        public AdPlatformController(AdPlatformService adPlatformService)
        {
            _adPlatformService = adPlatformService;
        }

        // Метод для загрузки данных из файла (позволяет перезаписать все данные)
        // Ожидается, что файл будет передан в параметре запроса
        [HttpPost("load-from-file")]
        public IActionResult LoadFromFile([FromQuery] string filePath)
        {
            try
            {
                // Вызов метода сервиса для загрузки данных из файла
                _adPlatformService.LoadDataFromFile(filePath);
                return Ok(new { message = "Данные успешно загружены!" });
            }
            catch (FileNotFoundException)
            {
                return NotFound(new { message = "Файл с рекламными площадками не найден." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ошибка при загрузке данных." });
            }
        }

        // Метод для поиска рекламных площадок по локации
        // Возвращает список площадок для заданной локации
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string location)
        {
            var platforms = _adPlatformService.SearchByLocation(location);

            if (platforms.Count == 0)
            {
                return NotFound(new { message = "Нет рекламных площадок для данной локации." });
            }

            return Ok(platforms);
        }
    }
}