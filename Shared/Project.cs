// файл: src/Shared/Project.cs
namespace Shared
{
    public class Project
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;              // уникальный идентификатор в URL
        public string Title { get; set; } = null!;             // название проекта
        public string Description { get; set; } = null!;       // полное описание
        public DateTime CreatedAt { get; set; }                // время создания

        public string? ShortDescription { get; set; }          // краткое описание
        public string? MetaTitle { get; set; }                 // SEO-заголовок страницы
        public string? MetaDescription { get; set; }           // SEO-описание (meta)

        public List<ProjectImage> Gallery { get; set; } = new();// список изображений галереи
        public string? FeaturedImageUrl { get; set; }           // главное превью-изображение

        public List<ProjectSection> Sections { get; set; } = new(); // динамические секции контента
        public List<ProjectLink> Links { get; set; } = new();       // набор внешних ссылок

        public Dictionary<string, string>? Metadata { get; set; }   // произвольные пары ключ→значение
        public string? ContentMarkdown { get; set; }               // контент в Markdown
        public string? CustomCss { get; set; }                     // специальные стили
        public string? CustomScript { get; set; }                  // специальные скрипты

        public bool IsPublished { get; set; } = true;              // публикация на сайте
        public bool IsFeatured { get; set; }                       // главное место на главной
        public int ViewCount { get; set; }                         // счетчик просмотров
        public int Priority { get; set; }                          // порядок в списках
        public DateTime? UpdatedAt { get; set; }                   // время последнего редактирования
    }

    // Одна секция страницы проекта (текст, галерея, особенности и т.п.).
    public class ProjectSection
    {
        public string Title { get; set; } = null!;               // заголовок блока
        public string ContentHtml { get; set; } = null!;         // HTML-содержимое блока
        public string SectionType { get; set; } = null!;         // тип блока ("text", "gallery" и т.п.)
        public int Order { get; set; }                           // порядок вывода на странице
    }

    // Структура одного элемента галереи
    public class ProjectImage
    {
        public string Url { get; set; } = null!;                 // ссылка на картинку
        public string? Caption { get; set; }                     // подпись под картинкой
        public string? Alt { get; set; }                         // alt-текст для доступности
    }

    // Внешние ссылки (репозиторий, демо, статья)
    public class ProjectLink
    {
        public string Title { get; set; } = null!;               // текст ссылки
        public string Url { get; set; } = null!;                 // адрес
        public string? IconCss { get; set; }                     // CSS-класс иконки
    }
}
