using Soenneker.Quark.Enums.ColorTypes;

namespace Soenneker.Quark.Dtos.Colors;

/// <summary>
/// A flexible and dynamic color dto that allows for theming as well as custom values.
/// </summary>
public readonly record struct Color
{
    public ColorType? Theme { get; }

    public string? Css { get; }

    public bool IsTheme => Theme is not null;

    private Color(ColorType? theme, string? css)
    {
        Theme = theme;
        Css = css;
    }

    public static Color FromTheme(ColorType theme) => new(theme, null);

    public static Color FromCss(string css) => new(null, css);

    public static implicit operator Color(ColorType theme) => FromTheme(theme);

    public static implicit operator Color(string css) => FromCss(css);

    public string? BuildClass(string prefix) => IsTheme ? $"{prefix}-{Theme!.Value}" : null;

    public string? CssValueOrNull() => IsTheme ? null : Css;
}