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

namespace FlagsISO.Enumerations;

/// <summary>
/// An icon size in pixels to use in the ComboBox.
/// </summary>
public enum FlagSizeType
{
    /// <summary>
    /// 16 pixel icon.
    /// </summary>
    Size16,

    /// <summary>
    /// 24 pixel icon.
    /// </summary>
    Size24,

    /// <summary>
    /// 32 pixel icon.
    /// </summary>
    Size32,

    /// <summary>
    /// 48 pixel icon.
    /// </summary>
    Size48,

    /// <summary>
    /// 64 pixel icon.
    /// </summary>
    Size64,

    /// <summary>
    /// The custom size for scalable image resource.
    /// </summary>
    Custom,
}