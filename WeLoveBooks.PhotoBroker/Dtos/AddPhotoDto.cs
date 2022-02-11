using System.Text.Json.Serialization;

namespace WeLoveBooks.PhotoBroker.Dtos;

public class AddPhotoDto
{
    public IFormFile File { get; set; }
    public PhotoType PhotoType { get; set; }
    [JsonPropertyName("foreignKeyId")]
    public string? ForeignKeyId { get; set; }
}

public enum PhotoType
{
    Book,
    Author,
    User
}
