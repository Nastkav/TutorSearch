using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Helpers;

public static class EnumExtensions
{
    // public static string GetDisplayName(this Enum enumValue) =>
    //     enumValue.GetType()
    //         .GetMember(enumValue.ToString())
    //         .First()
    //         .GetCustomAttribute<DisplayAttribute>()
    //         ?.GetName() ?? "";

    public static ImmutableList<string> GetDisplayNames(this Enum enumValue) =>
        enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .Select(x => x.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? "")
            .ToImmutableList();
}