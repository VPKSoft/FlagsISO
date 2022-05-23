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

using System.ComponentModel;
using System.Globalization;
using FlagsISO.Enumerations;

namespace FlagsISO.WinForms;

/// <summary>
/// A combo box control for country selection.
/// </summary>
public partial class ComboBoxCountrySelect : ComboBox
{
    /// <summary>
    /// The class constructor.
    /// </summary>
    public ComboBoxCountrySelect()
    {
        InitializeComponent();
    }

    private ImageProvider provider = new();

    /// <summary>
    /// Reconstructs the combo box drawing arguments if on a a property change.
    /// </summary>
    private void ReInit()
    {
        Items.Clear();
        IntegralHeight = false;
        List<string> countriesTwoLetter = CountryFlagsISO.GetCountries();
        List<KeyValuePair<string, string>> countries = new List<KeyValuePair<string, string>>();

        int currentIndex = 0, counter = 0;

        foreach (string twoLetter in countriesTwoLetter)
        {
            if (CountryFlagsISO.GetNameByTwoLetterISO(twoLetter) != string.Empty)
            {
                RegionInfo ri;
                try
                {
                    ri = new RegionInfo(twoLetter);
                }
                catch
                {
                    continue;
                }
                countries.Add(new KeyValuePair<string, string>(twoLetter, useNativeName ? ri.NativeName : ri.EnglishName));
            }
        }
        countries.Sort((a, b) => a.Value.CompareTo(b.Value)); // Sort the country list alphabetical order.
        foreach (KeyValuePair<string, string> country in countries)
        {
            try
            {
                RegionInfo ri;
                ri = new RegionInfo(country.Key);
                if (ri.Equals(RegionInfo.CurrentRegion))
                {
                    currentIndex = counter;
                }
                counter++;
            }
            catch
            {

            }
        }

        foreach (KeyValuePair<string, string> country in countries)
        {
            Items.Add(country);
        }

        if (Items.Count > 0 && currentIndex < Items.Count)
        {
            SelectedIndex = currentIndex;
        }
        else if (Items.Count > 0)
        {
            SelectedIndex = 0;
        }

        DropDownStyle = ComboBoxStyle.DropDownList;

        DrawItem -= ComboBoxCountrySelect_DrawItem!; // Lets not stack them

        DrawItem += ComboBoxCountrySelect_DrawItem!; // Attach the drawing event
        DrawMode = DrawMode.OwnerDrawFixed; // Set the drawing mode
    }

    Size stringSize;

    /// <summary>
    /// The custom draw event of the ComboBox.
    /// </summary>
    /// <param name="sender">The sender for the event.</param>
    /// <param name="e">Provides data for the DrawItem event.</param>
    void ComboBoxCountrySelect_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        if (e.Index == -1)
        {
            return;
        }

        string drawString = ((KeyValuePair<string, string>)Items[e.Index]).Value;

        try
        {
            stringSize = e.Graphics.MeasureString(drawString, Font).ToSize();
        }
        catch
        {
            // ignored
        }

        if (FlagSize != FlagSizeType.Custom)
        {
            using var memoryStream =
                new MemoryStream(CountryFlagsISO.GetForCountry(((KeyValuePair<string, string>)Items[e.Index]).Key,
                    CountryFlagsISO.FlagSizeInternal(flagSize), !dontGetShiny));
            using var bitmap = new Bitmap(memoryStream);

            e.Graphics.DrawImage(bitmap, new Point(e.Bounds.Left, e.Bounds.Top));
        }
        else
        {
            using var bitmap = provider.GetScaledImage(((KeyValuePair<string, string>)Items[e.Index]).Key, CustomWidth);
            e.Graphics.DrawImage(bitmap, new Point(e.Bounds.Left, e.Bounds.Top));
        }

        try
        {
            e.Graphics.DrawString(drawString,
                Font,
                new SolidBrush(ForeColor),
                new PointF(RightToLeft == RightToLeft.Yes ? e.Bounds.Right - stringSize.Width : e.Bounds.Left + CountryFlagsISO.FlagSizePixels(flagSize) + 2,
                    e.Bounds.Top + e.Bounds.Height / 2 - stringSize.Height / 2));
        }
        catch
        {
            // ignored
        }
    }

    /// <summary>
    /// Leaves the given regions to the ComboBox and removes the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to leave to the ComboBox.</param>
    public void LeaveRegions(params RegionInfo[] regions)
    {
        LeaveRegions(new List<RegionInfo>(regions));
    }

    /// <summary>
    /// Leaves the given regions to the ComboBox and removes the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to leave to the ComboBox.</param>
    public void LeaveRegions(List<RegionInfo> regions)
    {
        if (regions.Count == 0)
        {
            return;
        }

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (!regions.Exists(r => r.TwoLetterISORegionName == ((KeyValuePair<string, string>)Items[i]).Key))
            {
                Items.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Removes the given regions from the ComboBox and leaves the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to remove from the ComboBox.</param>
    public void RemoveRegions(params RegionInfo[] regions)
    {
        RemoveRegions(new List<RegionInfo>(regions));
    }

    /// <summary>
    /// Removes the given regions from the ComboBox and leaves the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to remove from the ComboBox.</param>
    public void RemoveRegions(List<RegionInfo> regions)
    {
        if (regions.Count == 0)
        {
            return;
        }

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (regions.Exists(r => r.TwoLetterISORegionName == ((KeyValuePair<string, string>)Items[i]).Key))
            {
                Items.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Fills the ComboBox with default regions.
    /// </summary>
    public void ResetRegions()
    {
        ReInit();
    }


    #region CountryFlagsISO

    /// <summary>
    /// A value indicating wether to use a native or english name of the regions in the ComboBox.
    /// </summary>
    private bool useNativeName;

    /// <summary>
    /// A value indicating wether to use shiny flag icons in the ComboBox.
    /// </summary>
    private bool dontGetShiny;

    /// <summary>
    /// Gets or sets a value indicating wether to use a native or english name of the regions in the ComboBox.
    /// </summary>
    [Description("Gets or sets a value indicating wether to use a native or english name of the regions in the ComboBox."), Category("CountryFlagsISO")]
    public bool UseNativeName
    {
        get => useNativeName;

        set
        {
            useNativeName = value;
            ReInit();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use shiny flag icons in the ComboBox.
    /// </summary>
    [Description("Gets or sets a value indicating wether to use shiny flag icons in the ComboBox."), Category("CountryFlagsISO")]
    public bool DontGetShiny
    {
        get => dontGetShiny;

        set
        {
            dontGetShiny = value;
            ReInit();
        }
    }

    /// <summary>
    /// Gets or sets the selected region of the ComboBox.
    /// </summary>
    [Description("Gets or sets the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public RegionInfo SelectedRegion
    {
        get => SelectedIndex == -1 ? RegionInfo.CurrentRegion : new RegionInfo(((KeyValuePair<string, string>)Items[SelectedIndex]).Key);

        set
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (((KeyValuePair<string, string>)Items[i]).Key == value.TwoLetterISORegionName)
                {
                    SelectedIndex = i;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Gets the selected region's CultureInfo class instance of the ComboBox.
    /// </summary>
    [Description("Gets the selected region's CultureInfo class instance of the ComboBox."), Category("CountryFlagsISO")]
    public CultureInfo SelectedCulture => CultureInfo.GetCultureInfoByIetfLanguageTag(SelectedRegion.TwoLetterISORegionName.ToLower());

    /// <summary>
    /// Gets a two letter ISO country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets a two letter ISO country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedTwoLetterIsoName => SelectedRegion.TwoLetterISORegionName;

    /// <summary>
    /// Gets a native country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets a native country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedNativeName => SelectedRegion.NativeName;

    /// <summary>
    /// Gets an english country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets an english country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedEnglishName => SelectedRegion.EnglishName;

    private FlagSizeType flagSize = FlagSizeType.Size16;

    /// <summary>
    /// Gets or sets the flag size used in the ComboBox.
    /// </summary>
    [Description("Gets or sets the flag size used in the ComboBox."), Category("CountryFlagsISO")]
    public FlagSizeType FlagSize
    {
        get => flagSize;

        set
        {
            flagSize = value;
            switch (value)
            {
                case FlagSizeType.Size16:
                    ItemHeight = 15;
                    break;
                case FlagSizeType.Size24:
                    ItemHeight = 23;
                    break;
                case FlagSizeType.Size32:
                    ItemHeight = 31;
                    break;
                case FlagSizeType.Size48:
                    ItemHeight = 47;
                    break;
                case FlagSizeType.Size64:
                    ItemHeight = 63;
                    break;
                case FlagSizeType.Custom:
                    ItemHeight = (int)(customWidth / 4.0 * 3.0); // 4:3
                    break;
            }
        }
    }
    #endregion

    private int customWidth = 100;

    public int CustomWidth
    {
        get => customWidth;

        set
        {
            if (customWidth != value)
            {
                customWidth = value;
                ItemHeight = (int)(customWidth / 4.0 * 3.0); // 4:3
                if (FlagSize == FlagSizeType.Custom)
                {
                    ReInit();
                }
            }
        }
    }

    #region HideBase

    /// <summary>
    /// Gets or sets a value specifying the style of the combo box.
    /// </summary>
    [Browsable(false)]
    public new ComboBoxStyle DropDownStyle
    {
        get => base.DropDownStyle;

        set => base.DropDownStyle = value;
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    public new string Text
    {
        get => base.Text;

        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.
    /// </summary>
    [Browsable(false)]
    public new DrawMode DrawMode
    {
        get => base.DrawMode;

        set => base.DrawMode = value;
    }

    /// <summary>
    /// Gets a custom System.Collections.Specialized.StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteStringCollection AutoCompleteCustomSource => base.AutoCompleteCustomSource;

    /// <summary>
    /// Gets an option that controls how automatic completion works for the ComboBox.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteMode AutoCompleteMode => base.AutoCompleteMode;

    /// <summary>
    /// Gets a value specifying the source of complete strings used for automatic completion.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteSource AutoCompleteSource => base.AutoCompleteSource;

    /// <summary>
    /// Gets the format-specifier characters that indicate how a value is to be displayed.
    /// </summary>
    [Browsable(false)]
    public new string FormatString => base.FormatString;

    /// <summary>
    /// Gets a value indicating whether formatting is applied to the DisplayMember property of the ListControl.
    /// </summary>
    [Browsable(false)]
    public new bool FormattingEnabled => base.FormattingEnabled;

    /// <summary>
    /// Gets the data source for this ComboBox.
    /// </summary>
    [Browsable(false)]
    public new object DataSource => base.DataSource;

    /// <summary>
    /// Gets the data bindings for the control.
    /// </summary>
    [Browsable(false)]
    public new ControlBindingsCollection DataBindings => base.DataBindings;

    /// <summary>
    /// Gets an object representing the collection of the items contained in this ComboBox.
    /// </summary>
    [Browsable(false)]
    public new ObjectCollection Items => base.Items;

    /// <summary>
    /// Gets the path of the property to use as the actual value for the items in the ListControl.
    /// </summary>
    [Browsable(false)]
    public new string ValueMember => base.ValueMember;

    /// <summary>
    /// Gets the property to display for this ListControl.
    /// </summary>
    [Browsable(false)]
    public new string DisplayMember => base.DisplayMember;

    /// <summary>
    /// Gets or sets the height of an item in the combo box.
    /// </summary>
    [Browsable(false)]
    public new int ItemHeight
    {
        get => base.ItemHeight;

        set => base.ItemHeight = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control should resize to avoid showing partial items.
    /// </summary>
    [Browsable(false)]
    public new bool IntegralHeight
    {
        get => base.IntegralHeight;

        set => base.IntegralHeight = value;
    }

    #endregion
}