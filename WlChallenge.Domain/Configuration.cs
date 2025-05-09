namespace WlChallenge.Domain;

public static class Configuration
{
    public static string Environment { get; set; } = string.Empty;
    public static DatabaseConfiguration Database { get; set; } = new();


    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
}