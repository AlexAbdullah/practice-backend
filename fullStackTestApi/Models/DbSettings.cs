namespace fullStackTestApi.Models;

public class DbSettings
{
    public string? ConnectionString { get; set; }

    public string DatabaseName { get; set; } = null!;

    public string NamesCollectionName { get; set; } = null!;
}