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
using Eto.Forms;
using FlagsISO.Classes;
using FlagsISO.Enumerations;

namespace FlagsISO.EtoForms;

/// <summary>
/// A dropdown to select a country.
/// Implements the <see cref="Eto.Forms.DropDown" />
/// </summary>
/// <seealso cref="Eto.Forms.DropDown" />
public class ComboBoxCountrySelect : DropDown
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComboBoxCountrySelect"/> class.
    /// </summary>
    public ComboBoxCountrySelect()
    {
        ItemImageBinding = new PropertyBinding<Image>(nameof(BindData.Image));
        ItemTextBinding = new PropertyBinding<string>(nameof(BindData.Value));
        ItemKeyBinding = new PropertyBinding<string>(nameof(BindData.Code));

        CreateCountryDataSource();
    }

    private void CreateCountryDataSource()
    {
        var items = new List<BindData>();
        if (DataStore != null)
        {
            items = (List<BindData>)DataStore;
            DataStore = null;
            foreach (var bindData in items)
            {
                bindData.Image?.Dispose();
            }
        }

        items.Clear();


        foreach (var code in CountryFlagsISO.SvgFlagCodes)
        {
            RegionInfo ri;
            try
            {
                ri = new RegionInfo(code);
            }
            catch
            {
                continue;
            }

            Bitmap? image;

            if (flagSizeType == FlagSizeType.Custom)
            {
                image = provider.GetScaledBitmap(code, new PointFloat(flagCustomSize.Width, flagCustomSize.Height), aspectRatioCustom);
            }
            else
            {
                image = provider.GetImageBitmap(code, CountryFlagsISO.FlagSizeInternal(flagSizeType),
                    useGlossyImage);
            }

            var name = NameDisplayType switch
            {
                CountryNameDisplay.Current => ri.DisplayName,
                CountryNameDisplay.English => ri.EnglishName,
                CountryNameDisplay.Native => ri.NativeName,
                _ => ri.NativeName,
            };

            items.Add(new BindData(image, name, code));
        }

        DataStore = items;
    }


    private class BindData
    {
        public BindData(Image? image, string value, string code)
        {
            Image = image;
            Value = value;
            Code = code;
        }

        internal Image? Image { get; }

        internal string Value { get; }

        internal string Code { get; }
    }

    private readonly ImageProvider provider = new();

    #region PublicProperties

    private bool useGlossyImage;
    private FlagSizeType flagSizeType;
    private SizeF flagCustomSize = new(100, 75);
    private bool lockAspectRatio = true;
    private ImageAspectRatio aspectRatioCustom = ImageAspectRatio.FourToThree;
    private CountryNameDisplay nameDisplayType = CountryNameDisplay.Native;

    /// <summary>
    /// Gets or sets a value indicating whether in case of an exact-sized image to use an image with glossy effect.
    /// </summary>
    /// <value><c>true</c> if in case of an exact-sized image to use an image with glossy effect; otherwise, <c>false</c>.</value>
    public bool UseGlossyImage
    {
        get => useGlossyImage;

        set
        {
            if (value != useGlossyImage)
            {
                useGlossyImage = value;
                CreateCountryDataSource();
            }
        }
    }

    /// <summary>
    /// Gets or sets the display type of the country name.
    /// </summary>
    /// <value>The display type of the country name.</value>
    public CountryNameDisplay NameDisplayType
    {
        get => nameDisplayType;

        set
        {
            if (value != nameDisplayType)
            {
                nameDisplayType = value;
                CreateCountryDataSource();
            }
        }
    }

    // TODO::Country name type. e.g. Native/English

    /// <summary>
    /// Gets or sets the type of the flag size.
    /// </summary>
    /// <value>The type of the flag size.</value>
    public FlagSizeType FlagSizeType
    {
        get => flagSizeType;

        set
        {
            if (flagSizeType != value)
            {
                flagSizeType = value;
                CreateCountryDataSource();
            }
        }
    }

    /// <summary>
    /// Gets or sets the custom flag size. The <see cref="FlagSizeType"/> must be set to <see cref="F:FlagSizeType.Custom"/> for this property to have any effect.
    /// </summary>
    /// <value>The custom flag size.</value>
    public SizeF FlagCustomSize
    {
        get => flagCustomSize;

        set
        {
            if (flagCustomSize != value)
            {
                if (lockAspectRatio)
                {
                    switch (aspectRatioCustom)
                    {
                        case ImageAspectRatio.FourToThree:
                            flagCustomSize = new SizeF(value.Width, value.Width / 4f * 3f); break;
                        case ImageAspectRatio.OneToOne:
                            flagCustomSize = new SizeF(value.Width, value.Width); break;
                    }
                }
                else
                {
                    flagCustomSize = value;
                }

                CreateCountryDataSource();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to lock the aspect ratio when using custom-sized country flag images.
    /// </summary>
    /// <value><c>true</c> if to lock aspect ratio for custom-sized images; otherwise, <c>false</c>.</value>
    public bool LockAspectRatio
    {
        get => lockAspectRatio;

        set
        {
            if (lockAspectRatio != value)
            {
                lockAspectRatio = value;
                CreateCountryDataSource();
            }
        }
    }

    /// <summary>
    /// Gets or sets the custom aspect ratio type.
    /// </summary>
    /// <value>The custom aspect ratio type.</value>
    public ImageAspectRatio AspectRatioCustom
    {
        get => aspectRatioCustom;

        set
        {
            if (value != aspectRatioCustom)
            {
                aspectRatioCustom = value;
                CreateCountryDataSource();
            }
        }
    }
    #endregion
}