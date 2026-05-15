using System.ComponentModel.DataAnnotations;

namespace BookLibrary.ViewModels;

public class BookViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название обязательно")]
    [Display(Name = "Название")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Автор обязателен")]
    [Display(Name = "Автор")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN обязателен")]
    [RegularExpression(@"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d$",
        ErrorMessage = "ISBN формат: 978-5-389-00001-7")]
    [Display(Name = "ISBN")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Жанр обязателен")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Год обязателен")]
    [Range(1000, 2100, ErrorMessage = "Год от 1000 до 2100")]
    [Display(Name = "Год")]
    public int Year { get; set; } = DateTime.Now.Year;
}
