namespace Web.Helpers;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength) =>
        value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
}