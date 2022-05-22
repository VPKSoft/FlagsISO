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
using Eto.Drawing;
using FlagsISO.Interfaces;
using SkiaSharp;
using Svg.Skia;

namespace FlagsISO.EtoForms;

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
            var data = CountryFlagsISO.GetForCountry(countryCode, flagSize, shiny);
            var result = new Bitmap(data);
            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Creates and instance of the <see cref="Image"/> class from the specified SVG data.
    /// </summary>
    /// <param name="svgBytes">The SVG data bytes.</param>
    /// <param name="desiredSize">Size of the desired <seealso cref="Image"/>.</param>
    /// <param name="rotationAngle">A value to rotate the SVG image to clockwise. The default is <c>null</c> which will ignore rotation.</param>
    /// <returns>An instance to a <see cref="Image"/> class.</returns>
    // (C): Original code: https://gist.github.com/punker76/67bd048ff403c1c73737905183f819a9
    private static Bitmap ImageFromSvg(byte[] svgBytes, Size desiredSize, float? rotationAngle = null)
    {
        if (desiredSize.Width < 1 || desiredSize.Height < 1)
        {
            desiredSize = new Size(1, 1);
        }

        using var memoryStream = new MemoryStream(svgBytes);
        using var svg = new SKSvg();
        svg.Load(memoryStream);

        var imageInfo = new SKImageInfo(desiredSize.Width, desiredSize.Height);
        using var surface = SKSurface.Create(imageInfo);
        using var canvas = surface.Canvas;

        if (rotationAngle != null)
        {
            canvas.RotateDegrees(rotationAngle.Value, desiredSize.Width / 2f, desiredSize.Height / 2f);
        }

        if (svg.Picture != null)
        {
            // Calculate the scaling needed for the desired size.
            var scaleX = desiredSize.Width / svg.Picture.CullRect.Width;
            var scaleY = desiredSize.Height / svg.Picture.CullRect.Height;
            var matrix = SKMatrix.CreateScale(scaleX, scaleY);

            // Draw the SVG to a bitmap.
            canvas.Clear(SKColors.Transparent);
            canvas.DrawPicture(svg.Picture, ref matrix);
        }

        canvas.Flush();

        using var outStream = new MemoryStream();
        using var data = surface.Snapshot();
        using var pngImage = data.Encode(SKEncodedImageFormat.Png, 100);

        pngImage.SaveTo(outStream);

        outStream.Position = 0;
        var bitmap = new Bitmap(outStream);

        return bitmap;
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

            var height = oneToOne ? width : width / 4f * 3f; // 4:3 ratio.

            height = (float)Math.Ceiling(height);

            var result = ImageFromSvg(bytes, new Size(new SizeF(width, height)));

            return result;
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

    /// <summary>
    /// Gets the country flag as a image scaled to specified width while maintaining the aspect ratio.
    /// </summary>
    /// <param name="countryCode">A two letter ISO country code.</param>
    /// <param name="width">The width of the flag .</param>
    /// <returns>An instance to <see cref="Image" /> class containing the requested flag or null if the flag was not found.</returns>
    public Image? GetScaledImage(string countryCode, float width)
    {
        return GetScaledBitmap(countryCode, width, false);
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