namespace WlChallenge.Api.Shared;

internal class Settings
{
    public static JwtSettings Jwt { get; set; } = new JwtSettings();
}

internal class JwtSettings
{
    public string Key { get; set; } = string.Empty;
}