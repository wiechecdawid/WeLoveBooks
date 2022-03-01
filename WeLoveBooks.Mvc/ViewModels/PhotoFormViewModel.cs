namespace WeLoveBooks.Mvc.ViewModels;

public class PhotoFormViewModel
{
    public int Type { get; set; }
    public string Id { get; set; }
    public IFormFile File { get; set; }
}
