using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    /// <summary>Модель книги библиотеки</summary>
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название книги")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название: от 1 до 200 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите имя автора")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Автор: от 2 до 100 символов")]
        [Display(Name = "Автор")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите год издания")]
        [Range(1450, 2100, ErrorMessage = "Год должен быть в диапазоне 1450–2100")]
        [Display(Name = "Год издания")]
        public int Year { get; set; }

        [StringLength(1000, ErrorMessage = "Описание не более 1000 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Жанр")]
        public string? Genre { get; set; }
    }
}
