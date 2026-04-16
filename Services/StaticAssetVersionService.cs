namespace ZackV2.Services;

public class StaticAssetVersionService
{
    private readonly IWebHostEnvironment _environment;

    public StaticAssetVersionService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public string Versioned(string? assetPath)
    {
        if (string.IsNullOrWhiteSpace(assetPath))
        {
            return string.Empty;
        }

        if (Uri.TryCreate(assetPath, UriKind.Absolute, out var absoluteUri) &&
            (absoluteUri.Scheme == Uri.UriSchemeHttp || absoluteUri.Scheme == Uri.UriSchemeHttps))
        {
            return assetPath;
        }

        var cleanPath = assetPath.Split('?', 2)[0].TrimStart('/');
        var fileInfo = _environment.WebRootFileProvider.GetFileInfo(cleanPath);
        if (!fileInfo.Exists)
        {
            return assetPath;
        }

        var version = $"{fileInfo.LastModified.UtcDateTime.Ticks}-{fileInfo.Length}";
        var separator = assetPath.Contains('?') ? "&" : "?";
        return $"{assetPath}{separator}v={version}";
    }
}
