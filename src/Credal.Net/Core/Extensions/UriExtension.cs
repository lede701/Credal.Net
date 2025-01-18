namespace Credal.Net.Extensions;

public static class UriExtension
{
    public static Uri Combine(this Uri uri, params string[] paths)
    {
        return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
    }
}
