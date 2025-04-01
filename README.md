# AdPlatforms Web Service

Этот проект представляет собой веб-сервис для работы с рекламными площадками. Веб-сервис позволяет загружать данные о рекламных площадках из файла и выполнять поиск площадок по локации.

## Описание

- **Метод загрузки**: Загрузка рекламных площадок из файла. Данные полностью перезаписываются в памяти.
- **Метод поиска**: Поиск рекламных площадок по заданной локации.

Данные хранятся в оперативной памяти (in-memory collection), что позволяет быстро выполнять поиск.

## Структура проекта

Проект состоит из следующих компонентов:

- **AdPlatformController**: Контроллер для обработки HTTP-запросов (методов API).
- **AdPlatformService**: Сервис, который выполняет логику загрузки данных из файла и поиска рекламных площадок по локации.
- **AdPlatform**: Модель данных для рекламной площадки.

## Запуск проекта

1. Клонируйте репозиторий:

    ```
    git clone https://github.com/andryare1/AdPlatform.git
    ```

2. Перейдите в каталог проекта:

    ```
    cd AdPlatforms
    ```

3. Откройте проект в IDE (например, Visual Studio, Rider или VS Code).

4. Соберите проект:

    ```
    dotnet build
    ```

5. Запустите проект:

    ```
    dotnet run
    ```

Теперь веб-сервис будет доступен по адресу `https://localhost:5001`.

## Методы API

### 1. Загрузка данных

**POST** `/api/ads/load-from-file?filePath=<путь к файлу>`

Загружает данные о рекламных площадках из файла. Параметр `filePath` указывает путь к файлу, содержащему данные.

### 2. Поиск по локации

**GET** `/api/ads/search?location=<локация>`

Ищет рекламные площадки по заданной локации.

## Тестирование
Проект содержит юнит-тесты с использованием NUnit.
## Запуск тестов:

1. Установите необходимые зависимости:

    ```
   dotnet restore
    ```

2. Запустите тесты:

    ```
   dotnet test
    ```

## Важные моменты
Данные о рекламных площадках хранятся в памяти, что позволяет обеспечивать быстрый поиск.
В случае некорректных входных данных (например, файл не найден), система возвращает соответствующие сообщения об ошибках.
Загрузка данных из файла осуществляется один раз, и последующие запросы по поиску являются быстрыми.
