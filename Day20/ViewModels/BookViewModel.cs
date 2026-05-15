using System.ComponentModel.DataAnnotations;

namespace BookLibrary.ViewModels;

/// <summary>
/// ViewModel для формы добавления/редактирования книги
/// </summary>
public class BookViewModel
{
    [Required(ErrorMessage = "Название обязательно")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Название от 1 до 200 символов")]
    [Display(Name = "Название книги")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Автор обязателен")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя автора от 2 до 100 символов")]
    [Display(Name = "Автор")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN обязателен")]
    [RegularExpression(
        @"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$",
        ErrorMessage = "Некорректный формат ISBN (пример: 978-5-17-083086-4)")]
    [Display(Name = "ISBN")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Жанр обязателен")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Жанр от 2 до 50 символов")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Год издания обязателен")]
    [Range(1000, 2100, ErrorMessage = "Год должен быть между 1000 и 2100")]
    [Display(Name = "Год издания")]
    public int Year { get; set; } = DateTime.Now.Year;
}
