#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using FlagsISO.Classes;
using FlagsISO.Enumerations;

namespace FlagsISO.Interfaces;

/// <summary>
/// An interface to provide platform-depended bitmaps or images of the country flags.
/// </summary>
/// <typeparam name="TBitmap">The type of the platform bitmap type.</typeparam>
/// <typeparam name="TImage">The type of the platform image type.</typeparam>
public interface IImageProvider<out TBitmap, out TImage> where TBitmap : class where TImage : class
{
    /// <summary>
    /// Gets the country flag as a platform-supported bitmap.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>An instance to <typeparamref name="TBitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    TBitmap? GetImageBitmap(string countryCode, FlagSizes flagSize, bool shiny);

    /// <summary>
    /// Gets the country flag as a platform-supported image.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>An instance to <typeparamref name="TImage"/> class containing the requested flag or null if the flag was not found.</returns>
    TImage? GetImage(string countryCode, FlagSizes flagSize, bool shiny);

    /// <summary>
    /// Gets the country flag as a platform-supported bitmap scaled to specified width while maintaining the aspect ratio.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <typeparamref name="TBitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    TBitmap? GetScaledBitmap(string countryCode, float width);

    /// <summary>
    /// Gets the country flag as a platform-supported image scaled to specified width while maintaining the aspect ratio.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <typeparamref name="TImage"/> class containing the requested flag or null if the flag was not found.</returns>
    TImage? GetScaledImage(string countryCode, float width);

    /// <summary>
    /// Gets the country flag as a platform-supported bitmap scaled to specified width with aspect ratio of 1:1.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <typeparamref name="TBitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    TBitmap? GetScaledBitmapOneToOne(string countryCode, float width);

    /// <summary>
    /// Gets the country flag as a platform-supported image scaled to specified width with aspect ratio of 1:1.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <typeparamref name="TImage"/> class containing the requested flag or null if the flag was not found.</returns>
    TImage? GetScaledImageOneToOne(string countryCode, float width);

    /// <summary>
    /// Gets the country flag as a platform-supported bitmap scaled to specified size.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="size">The size to scale the flag into.</param>
    /// <param name="aspectRatio">The aspect ratio of the original SVG to scale.</param>
    /// <returns>An instance to <typeparamref name="TBitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    TBitmap? GetScaledBitmap(string countryCode, PointFloat size, ImageAspectRatio aspectRatio);

    /// <summary>
    /// Gets the country flag as a platform-supported image scaled to specified size.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="size">The size to scale the flag into.</param>
    /// <param name="aspectRatio">The aspect ratio of the original SVG to scale.</param>
    /// <returns>An instance to <typeparamref name="TImage"/> class containing the requested flag or null if the flag was not found.</returns>
    TImage? GetScaledImage(string countryCode, PointFloat size, ImageAspectRatio aspectRatio);
}

