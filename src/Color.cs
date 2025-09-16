using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums.ColorTypes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Dtos.Colors;

/// <summary>
/// A flexible and dynamic color dto that allows for theming as well as custom values.
/// </summary>
public readonly record struct Color
{
    public ColorType? Theme { get; }

    public string? Css { get; }

    public bool IsTheme => Theme is not null;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Color(ColorType? theme, string? css)
    {
        Theme = theme;
        Css = css;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color FromTheme(ColorType theme) => new(theme, null);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color FromCss(string css) => new(null, css);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(ColorType theme) => FromTheme(theme);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(string css) => FromCss(css);

    /// <summary>
    /// Returns a Bootstrap-like class (e.g., "text-primary") or null if this color is a raw CSS value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? BuildClass(string prefix)
    {
        ColorType? t = Theme;

        if (t is null)
            return null;

        return string.Concat(prefix, "-", t.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? CssValueOrNull() => Theme is null ? Css : null;

    // --- Cached instances: avoid constructing a new Color every access ---
    public static readonly Color Primary = FromTheme(ColorType.Primary);
    public static readonly Color Secondary = FromTheme(ColorType.Secondary);
    public static readonly Color Success = FromTheme(ColorType.Success);
    public static readonly Color Danger = FromTheme(ColorType.Danger);
    public static readonly Color Warning = FromTheme(ColorType.Warning);
    public static readonly Color Info = FromTheme(ColorType.Info);
    public static readonly Color Light = FromTheme(ColorType.Light);
    public static readonly Color Dark = FromTheme(ColorType.Dark);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendClass(ref PooledStringBuilder sb, string prefix)
    {
        ColorType? t = Theme;
        if (t is null)
            return;

        sb.Append(prefix);
        sb.Append('-');
        sb.Append(t.Value);
    }
}