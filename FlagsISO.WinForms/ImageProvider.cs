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

using System.Globalization;
using FlagsISO.Interfaces;
using Svg;

namespace FlagsISO.WinForms;

/// <summary>
/// A class to get country flags for different countries in different sizes.
/// Implements the <see cref="FlagsISO.Interfaces.IImageProvider{TBitmap, TImage}" />.
/// </summary>
/// <seealso cref="FlagsISO.Interfaces.IImageProvider{TBitmap, TImage}" />.
public class ImageProvider : IImageProvider<Bitmap, Image>
{
    /// <summary>
    /// Gets the country flag as a bitmap.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>An instance to <see cref="Bitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    public Bitmap? GetImageBitmap(string countryCode, FlagSizes flagSize, bool shiny)
    {
        try
        {
            using var memoryStream = new MemoryStream(CountryFlagsISO.GetForCountry(countryCode, flagSize, shiny));
            var bitmap = new Bitmap(memoryStream);
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the country flag as an image.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>An instance to <see cref="Image"/> class containing the requested flag or null if the flag was not found.</returns>
    public Image? GetImage(string countryCode, FlagSizes flagSize, bool shiny)
    {
        return GetImageBitmap(countryCode, flagSize, shiny);
    }

    /// <summary>
    /// Gets the country flag as a bitmap scaled to specified width while maintaining the aspect ratio.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <see cref="Bitmap" /> class containing the requested flag or null if the flag was not found.</returns>
    public Bitmap? GetScaledBitmap(string countryCode, float width)
    {
        return GetScaledBitmap(countryCode, width, false);
    }

    private static Bitmap? GetScaledBitmap(string countryCode, float width, bool oneToOne)
    {
        try
        {
            width = (float)Math.Ceiling(width);

            var resourceObject =
                oneToOne
                    ? SvgFlagsBoxRatio.ResourceManager.GetObject(countryCode.ToLowerInvariant(),
                        CultureInfo.InvariantCulture)
                    : SvgFlagsNativeRatio.ResourceManager.GetObject(countryCode.ToLowerInvariant(),
                        CultureInfo.InvariantCulture);

            var bytes = (byte[]?)(resourceObject) ?? Array.Empty<byte>();
            using var memoryStream = new MemoryStream(bytes);

            var image = SvgDocument.Open<SvgDocument>(memoryStream);

            var height = oneToOne ? width : width / 4f * 3f; // 4:3 ratio.

            height = (float)Math.Ceiling(height);

            var result = image.Draw((int)width, (int)height);

            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the country flag as a image scaled to specified width while maintaining the aspect ratio.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <see cref="Image" /> class containing the requested flag or null if the flag was not found.</returns>
    public Image? GetScaledImage(string countryCode, float width)
    {
        return GetScaledBitmap(countryCode, width);
    }

    /// <summary>
    /// Gets the country flag as a platform-supported bitmap scaled to specified width with aspect ratio of 1:1.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <see cref="Bitmap"/> class containing the requested flag or null if the flag was not found.</returns>
    public Bitmap? GetScaledBitmapOneToOne(string countryCode, float width)
    {
        return GetScaledBitmap(countryCode, width, true);
    }

    /// <summary>
    /// Gets the country flag as a platform-supported image scaled to specified width with aspect ratio of 1:1.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <see cref="Image"/> class containing the requested flag or null if the flag was not found.</returns>
    public Image? GetScaledImageOneToOne(string countryCode, float width)
    {
        return GetScaledBitmap(countryCode, width, true);
    }
}